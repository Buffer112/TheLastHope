using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management;
using TheLastHope.Management.Data;
using TheLastHope.Weapons;
using TheLastHope.Helpers;
using TheLastHope.UI;

namespace TheLastHope.Enemies
{
    
    public class BuggyEnemy : AEnemy
    {
        [SerializeField] private float _driftingSpeedDivider;
        [SerializeField] private float _speedSmoother;
        [SerializeField] private ATurret _turret;
        [SerializeField] private float _visionDistance;
		[SerializeField] private Texture _damageTex;
		[SerializeField] private List<GameObject> _frontWheels;
		[SerializeField] private List<GameObject> _rearWheels;
		[SerializeField] private float _visionSpread;
		[SerializeField] private float _turnSpeed;
		[SerializeField] private float _turnRatio;
		[SerializeField] private float _maxDistanceFromPlayer;

		[SerializeField] internal float maxSpeed;
		internal Vector3 currentSpeed;
		[SerializeField] internal float maxAcceleration;
		internal Vector3 currentAcceleration;
		[SerializeField] internal float driftingRadius;
		internal Vector3 currentDriftingPoint;
        [SerializeField] float trainWidthForDrifting = 6f;
        [SerializeField] GameObject targetObject;

        private RaycastHit hit;
		private Renderer[] renderers;
		private Color _initColor;
		private Texture _initTexture;
		private Timer timer;
		private List<GameObject> _wheels;
		private List<Texture> _textures;
		private bool _isAlive;
		private Quaternion _initialRotation;
		private bool _avoiding = false;
		private float _turnSpread;
		private UIOverlayController _uiController;
        UpOrDownType upDownType = UpOrDownType.Up;

		[Header("Sensors")]
		[SerializeField] private float sensorLength = 3f;
		[SerializeField] private Vector3 frontSensorPosition = new Vector3(0f, 0f, 1.0f);
		[SerializeField] private float frontSideSensorPosition = 0.6f;
		[SerializeField] private float frontSensorAngle = 30f;



		/// <summary>
		/// Resets health.
		/// </summary>
		public override void Init()
		{
            if (transform.position.z>0)
            {
                upDownType = UpOrDownType.Up;
                currentDriftingPoint = new Vector3(targetPosition.position.x,
                             targetPosition.position.y,
                             targetPosition.position.z + trainWidthForDrifting * 1.5f);
            }
            else
            {
                upDownType = UpOrDownType.Down;
                currentDriftingPoint = new Vector3(targetPosition.position.x,
                                targetPosition.position.y,
                                targetPosition.position.z - trainWidthForDrifting * 1.5f);
            }
            enemyType = EnemyType.Wheeled;
			_textures = new List<Texture>();
			timer = new Timer();
			renderers = GetComponentsInChildren<Renderer>();
			foreach (Renderer rend in renderers)
			{
				if (rend) _textures.Add(rend.material.mainTexture);
			}
		}

		public override void Init(SceneData sceneData)
        {
			_uiController = GetComponentInChildren<UIOverlayController>();
			if (_uiController)
			{
				sceneData.UiOverlayControllers.Add(_uiController);
				_uiController.Init();
			}
			MaxHealth = maxHealth;
			_turret.Init();
			_isAlive = true;
			IsActive = true;
			MaxHealth = maxHealth;
            Health = MaxHealth;
			int _rndCar = Random.Range(1, sceneData.TrainCars.Count);
			target = sceneData.TrainCars[_rndCar].gameObject.transform;
			targetPosition = target;
			currentDriftingPoint = new Vector3(targetPosition.position.x,
												targetPosition.position.y,
												targetPosition.position.z);
            targetObject = target.gameObject;
			_wheels = new List<GameObject>();
			RotateRightWay();
			//InitVision();
			Init();
		}

		private void RotateRightWay()
		{
			if (transform.rotation == Quaternion.identity) this.transform.Rotate(new Vector3(0, 90, 0));
			_initialRotation = transform.rotation;
		}

		public override void EnemyUpdate(SceneData sceneData, float deltaTime)
		{
			if (timer.Finished()) ChangeTex(false);
			if (Health < 0 && _isAlive)
			{
				Die();
				sceneData.Props.Insert((sceneData.Props.Count-2), this.gameObject);
			}
			if (_isAlive)
			{
				Sensors();
				RotateWheels(sceneData);
				_turret.TurUpdate(sceneData, deltaTime);
				timer.TimerUpdate();
				if (transform.position.z > _maxDistanceFromPlayer) Turn(TurnSide.Right);
				if (transform.position.z > _maxDistanceFromPlayer * -1) Turn(TurnSide.Left);

				Move(sceneData, deltaTime);
			}
		}

		private void Turn(TurnSide turnSide)
		{
			if (IsActive && _isAlive)
			{
				if (turnSide == TurnSide.Left)
				{
					_turnSpread = Mathf.Lerp(0, _turnRatio * -1, Time.deltaTime * _turnSpeed);
					//transform.rotation = new Quaternion(transform.rotation.x, Mathf.Lerp(transform.rotation.y, transform.rotation.y - _turnRatio*5, Time.deltaTime / _turnSpeed), transform.rotation.z, transform.rotation.w);
				}

				if (turnSide == TurnSide.Right)
				{
					_turnSpread = Mathf.Lerp(0, _turnRatio, Time.deltaTime / _turnSpeed);
					//transform.rotation = new Quaternion(transform.rotation.x, Mathf.Lerp(transform.rotation.y, transform.rotation.y + _turnRatio*5, Time.deltaTime / _turnSpeed), transform.rotation.z, transform.rotation.w);
				}

				if (turnSide == TurnSide.Center)
				{
					transform.rotation = _initialRotation;
					//_turnSpread = 0;
				}
			}

			//print("I'm turning to the " + turnSide + " and my pos is " + transform.position);
		}

		private void Sensors()
		{
			RaycastHit hit;
			Vector3 sensorStartPos = transform.position + frontSensorPosition;

			//front center sensor
			if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
			{
				Debug.DrawLine(sensorStartPos, hit.point);
				if (Random.value > 0.5) Turn(TurnSide.Right);
				else Turn(TurnSide.Left);
			}

			//front right sensor
			sensorStartPos.x += frontSideSensorPosition;
			if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
			{
				Debug.DrawLine(sensorStartPos, hit.point);
				if (hit.transform.tag == "Prop" || hit.transform.tag == "Enemy" || hit.transform.tag == "Player")  Turn(TurnSide.Left);
			}

			//front right angle sensor
			if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
			{
				Debug.DrawLine(sensorStartPos, hit.point);
				if (hit.transform.tag == "Player") Turn(TurnSide.Left);
				else Turn(TurnSide.Center);
			}

			//front left sensor
			sensorStartPos.x -= 2 * frontSideSensorPosition;
			if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
			{
				Debug.DrawLine(sensorStartPos, hit.point);
				if (hit.transform.tag == "Prop" || hit.transform.tag == "Enemy" || hit.transform.tag == "Player") Turn(TurnSide.Right);
			}

			//front left angle sensor
			if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
			{
				Debug.DrawLine(sensorStartPos, hit.point);
				if (hit.transform.tag == "Player") Turn(TurnSide.Right);
				else Turn(TurnSide.Center);
			}

			//no sensor
			else
			{
				if (transform.rotation != _initialRotation) Turn(TurnSide.Center);
			}
		}

		/// <summary>
		/// Moves this enemy according it's posibilities and targets.
		/// </summary>
		/// <param name="sceneData"></param>
		/// <param name="deltaTime"></param>
		private void Move(SceneData sceneData, float deltaTime)
		{
			if ((_speedSmoother != 0) || (_driftingSpeedDivider != 0))
			{
                Vector3 speed = GetCurrentSpeed(sceneData, currentSpeed, targetPosition, deltaTime);
                currentSpeed = Vector3.Lerp(currentSpeed, speed, _speedSmoother);
                //transform.position = new Vector3(transform.position.x + currentSpeed.x * deltaTime * Random.RandomRange(0.8f, 1.1f), transform.position.y, transform.position.z + _turnSpread);
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + currentSpeed.x * deltaTime,
                                                       gameObject.transform.position.y + currentSpeed.y * deltaTime,
                                                       gameObject.transform.position.z + currentSpeed.z * deltaTime);
            }
		}

		/// <summary>
		/// Applies damage to an Enemy.
		/// </summary>
		/// <param name="damage"></param>
		public override void SetDamage(float damage)
        {
            Health -= damage;
			ChangeTex(true);
		}

        Vector3 GetCurrentSpeed(SceneData sceneData, Vector3 currentSpeed, Transform targetPosition, float deltaTime)
        {
            if (Mathf.Abs(targetPosition.transform.position.x - this.transform.position.x) < driftingRadius &&
                Mathf.Abs(targetPosition.transform.position.x - this.transform.position.x) < driftingRadius)
                return DriftSpeed(sceneData, deltaTime).normalized * maxSpeed / _driftingSpeedDivider;

            return new Vector3(targetPosition.transform.position.x - this.transform.position.x,
                              0,
                              targetPosition.transform.position.z - this.transform.position.z).normalized * maxSpeed;
        }

		private void ChangeTex(bool isDamage)
		{
			if (isDamage && IsActive)
			{

				foreach (Renderer rend in renderers)
				{
					rend.material.mainTexture = _damageTex;
				}
				timer.Start(0.1f);
			}
			else
			{
				for (int i = 0; i < renderers.Length; i++)
				{
					renderers[i].material.mainTexture = _textures[i];
				}
			}
		}

        Vector3 DriftSpeed(SceneData sceneData, float deltaTime)
        {
            if (Mathf.Abs(currentDriftingPoint.x - transform.position.x) < driftingRadius / 10 &&
                               Mathf.Abs(currentDriftingPoint.z - transform.position.z) < driftingRadius / 10)
            {
                if (upDownType == UpOrDownType.Up)
                {
                    currentDriftingPoint = new Vector3(Random.Range(targetPosition.position.x - driftingRadius,
                                                                    targetPosition.position.x),
                                                                            0,
                                                      Random.Range(targetPosition.position.z + trainWidthForDrifting,
                                                                   targetPosition.position.z + driftingRadius));
                }
                else
                {
                    currentDriftingPoint = new Vector3(Random.Range(targetPosition.position.x - driftingRadius,
                                                                    targetPosition.position.x),
                                                                    0,
                                                       Random.Range(targetPosition.position.z - trainWidthForDrifting,
                                                                    targetPosition.position.z - driftingRadius));
                }

            }
            return new Vector3(currentDriftingPoint.x - transform.position.x, 0, currentDriftingPoint.z - transform.position.z);
        }

        public override void Die()
        {
            var _explosion = this.gameObject.transform.GetChild(1);
            var _buggy = this.gameObject;
			var _snd = this.gameObject.GetComponent<AudioSource>();
			Rigidbody[] _rigidBodies = this.gameObject.GetComponentsInChildren<Rigidbody>();
			//_buggy.gameObject.SetActive(false);
			IsActive = false;
			_isAlive = false;
			foreach (Rigidbody rb in _rigidBodies)
			{
				rb.isKinematic = false;
				rb.AddRelativeForce(transform.forward * 70, ForceMode.Impulse);
				rb.AddExplosionForce(50, transform.position, 100);
			}
            _explosion.gameObject.SetActive(true);
            if (_snd) _snd.Play();
			this.gameObject.GetComponent<Collider>().enabled = false;
			ChangeTex(false);
        }

		public void RotateWheels(SceneData sceneData)
		{
			if (_wheels != null)
			{
				foreach (GameObject wheel in _frontWheels)
				{
					try { wheel.transform.Rotate(sceneData.TrainSpeed * 3, 0, 0); }
					catch { print("No Wheels!"); }
				}

				foreach (GameObject wheel in _rearWheels)
				{
					try { wheel.transform.Rotate(sceneData.TrainSpeed * 3, 0, 0); }
					catch { print("No Wheels!"); }
				}
			}
		}

    }
}


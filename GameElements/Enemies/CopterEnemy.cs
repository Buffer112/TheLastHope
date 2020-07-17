using TheLastHope.Helpers;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Enemies
{


    internal enum EnemyStatus
    {
        Attack,
        Move
    }

    public class CopterEnemy : AEnemy
    {
        [SerializeField] private float driftingSpeedDivider;
        [SerializeField] private float speedSmoother;
        [SerializeField] private float attackAngleSmoother = 0.1f;
        [SerializeField] private float angularSpeed = 150f;
        [SerializeField] private ARangedWeapon weapon;
        [SerializeField] private float visionDistance;
        [SerializeField] private Texture damageTex;
        [SerializeField] private GameObject currentRaycastHistGameObject;

        [SerializeField] internal float maxSpeed;
        internal Vector3 currentSpeed;
        [SerializeField] internal float maxAcceleration;
        internal Vector3 currentAcceleration;
        [SerializeField] internal float driftingRadius;
        internal Vector3 currentDriftingPoint;
        internal UpOrDownType upDownType = UpOrDownType.Up;
        [SerializeField] private float trainWidthForDrifting = 6f;
        private EnemyStatus status = EnemyStatus.Move;
        [SerializeField] private float attackTime = 3f;
        [SerializeField] private float currentAttackTime = 0f;
        [SerializeField] private float criticalAngleToTarget = 5f;

        private RaycastHit hit;
        private Renderer[] renderers;
        private Color initColor;
        private Texture initTexture;
        private Timer timer;

        /// <summary>
        /// Resets health.
        /// </summary>
        public override void Init(SceneData sceneData)
        {
            target = sceneData.TrainCars[0].gameObject.transform;
            targetPosition = sceneData.TrainCars[0].gameObject.transform;
            System.Random r = new System.Random();
            if (transform.position.z < 0) upDownType = UpOrDownType.Down;
            if (upDownType == UpOrDownType.Up)
            {
                currentDriftingPoint = new Vector3(targetPosition.position.x,
                             targetPosition.position.y,
                             targetPosition.position.z + trainWidthForDrifting * 1.5f);
            }
            else
            {
                currentDriftingPoint = new Vector3(targetPosition.position.x,
                                targetPosition.position.y,
                                targetPosition.position.z - trainWidthForDrifting * 1.5f);
            }
            weapon.Init();
            IsActive = true;
            MaxHealth = maxHealth;
            Health = MaxHealth;
            renderers = GetComponentsInChildren<Renderer>();
            initTexture = renderers[2].material.mainTexture;
            Init();
        }

        public override void Init()
        {
            timer = new Timer();
        }

        /// <summary>
        /// Moves this enemy according it's posibilities and targets.
        /// </summary>
        /// <param name="sceneData"></param>
        /// <param name="deltaTime"></param>
        public override void EnemyUpdate(SceneData sceneData, float deltaTime)
        {
            if (Health < 0 && IsActive)
            {
                Die();
                sceneData.Props.Insert((sceneData.Props.Count - 2), gameObject);
            }
            weapon.WeaponUpdate();
            timer.TimerUpdate();
            if (timer.Finished()) ChangeTex(false);
            if ((speedSmoother != 0) || (driftingSpeedDivider != 0))
            {
                //targetPosition = this.targetPosition;
                //print(targetPosition.name);
                Vector3 speed = GetCurrentSpeed(sceneData, currentSpeed, targetPosition, deltaTime);
                currentSpeed = Vector3.Lerp(currentSpeed, speed, speedSmoother);
                if (status == EnemyStatus.Move)
                {
                    //TO METHOD!!!1
                    float eulerTargetRot = Quaternion.FromToRotation(transform.forward,
                        currentDriftingPoint - transform.position).eulerAngles.y;
                    //print("rot: " + eulerTargetRot);
                    float turningDir = 1;
                    if (Mathf.Abs(eulerTargetRot) > 180)
                        turningDir *= -1;
                    if (Mathf.Abs(eulerTargetRot) < angularSpeed * deltaTime)
                    {
                        gameObject.transform.rotation *= Quaternion.AngleAxis(eulerTargetRot * deltaTime, Vector3.up);
                    }
                    else
                    {
                        gameObject.transform.rotation *= Quaternion.AngleAxis(angularSpeed * turningDir * deltaTime, Vector3.up);
                    }


                }
                else if (status == EnemyStatus.Attack)
                {
                    float eulerTargetRot = Quaternion.FromToRotation(transform.forward,
                        target.position - transform.position).eulerAngles.y;
                    //print("rot: " + eulerTargetRot);
                    float turningDir = 1;
                    if (Mathf.Abs(eulerTargetRot) > 180)
                        turningDir *= -1;
                    if (Mathf.Abs(eulerTargetRot) < angularSpeed * deltaTime)
                    {
                        gameObject.transform.rotation *= Quaternion.AngleAxis(eulerTargetRot * deltaTime, Vector3.up);
                    }
                    else
                    {
                        gameObject.transform.rotation *= Quaternion.AngleAxis(angularSpeed * turningDir * deltaTime, Vector3.up);
                    }

                }
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + currentSpeed.x * deltaTime,
                                                        gameObject.transform.position.y + currentSpeed.y * deltaTime,
                                                        gameObject.transform.position.z + currentSpeed.z * deltaTime);
            }

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * visionDistance, Color.red);


            var tempVector = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);
            if (Physics.Raycast(tempVector, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                currentRaycastHistGameObject = hit.collider.gameObject;
                if (hit.transform.tag == "Player" && hit.distance < visionDistance)
                {
                    weapon.FireIntoThePlayer(hit);
                }

                if ((hit.distance < visionDistance) && (hit.transform.gameObject.tag == "Finish"))
                {
                    gameObject.GetComponent<AudioSource>().clip = null;
                    Health = 0;
                }
            }
        }

        /// <summary>
        /// For not it do nothing.
        /// </summary>
        /// <param name="damage"></param>
        public override void SetDamage(float damage)
        {
            Health -= damage;
            ChangeTex(true);
        }

        private Vector3 GetCurrentSpeed(SceneData sceneData, Vector3 currentSpeed, Transform targetPosition, float deltaTime)
        {
            if (Mathf.Abs(targetPosition.position.z - transform.position.z) < driftingRadius &&
                Mathf.Abs(targetPosition.position.z - transform.position.z) < driftingRadius)
                return DriftSpeed(sceneData, deltaTime).normalized * maxSpeed / driftingSpeedDivider;

            return new Vector3(targetPosition.position.z - transform.position.z,
                              0,
                              targetPosition.position.x - transform.position.x).normalized * maxSpeed;
        }

        private void ChangeTex(bool isDamage)
        {
            if (isDamage)
            {

                foreach (Renderer rend in renderers)
                {
                    rend.material.mainTexture = damageTex;
                }
                timer.Start(0.1f);
            }
            else
            {
                foreach (Renderer rend in renderers)
                {
                    rend.material.mainTexture = initTexture;
                }
            }
        }

        //Vector3 GetCurrentSpeed(Vector3 currentSpeed, Vector3 currentAcceleration, float deltaTime)
        //{

        //    return new Vector3(currentSpeed.x + currentAcceleration.x*deltaTime,
        //                        currentSpeed.y + currentAcceleration.y*deltaTime,
        //                        currentSpeed.z + currentAcceleration.z*deltaTime);
        //}

        //Vector3 GetCurrentAcceleration(GameObject targetPosition, float maxAcceleration)
        //{
        //   return new Vector3(targetPosition.transform.position.x - this.transform.position.x,
        //                      0,
        //                      targetPosition.transform.position.z - this.transform.position.z).normalized * maxAcceleration;
        //}

        private Vector3 DriftSpeed(SceneData sceneData, float deltaTime)
        {
            if (status == EnemyStatus.Attack)
            {
                float currentAngleToTarget = Mathf.Abs(Vector3.Angle(transform.forward, target.transform.position - transform.position));
                currentAttackTime += deltaTime;
                if (currentAttackTime > attackTime)
                {
                    status = EnemyStatus.Move;
                    return Vector3.zero;
                }
                if (currentAngleToTarget < criticalAngleToTarget)
                {
                    if (upDownType == UpOrDownType.Up)
                    {
                        if (!(currentDriftingPoint.z < target.position.z + trainWidthForDrifting))
                            return transform.right;
                    }
                    else
                    {
                        if (!(currentDriftingPoint.z < target.position.z + trainWidthForDrifting))
                            return -transform.right;
                    }
                    return Vector3.zero;
                }
            }
            else if (status == EnemyStatus.Move)
            {
                if (Mathf.Abs(currentDriftingPoint.x - transform.position.x) < driftingRadius / 10 &&
                    Mathf.Abs(currentDriftingPoint.z - transform.position.z) < driftingRadius / 10)
                {
                    status = EnemyStatus.Attack;
                    currentAttackTime = 0f;
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

            return Vector3.zero;
        }

        public override void Die()
        {
            var _explosion = gameObject.transform.GetChild(1);
            var _copter = gameObject.transform.GetChild(0);
            _copter.gameObject.SetActive(false);
            _explosion.gameObject.SetActive(true);
            gameObject.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<Collider>().enabled = false;

            IsActive = false;
        }
    }
}

using TheLastHope.Management.AbstractLayer;
using UnityEngine;

public class SimpleRocket : AAmmo
{
    private Transform target;
    /// <summary>
    /// Target for rocket (bullet)
    /// </summary>
    public Transform Target { get => target; set => target = value; }

    #region ObjectPool methods

    public override void OnPopulate()
    {
        throw new System.NotImplementedException();
    }

    public override void OnDepopulate()
    {
        throw new System.NotImplementedException();
    }

    #endregion

    #region Collision methods

    protected override void OnTriggerEnter(Collider collision)
    {
        collision?.gameObject?.GetComponent<AEnemy>()?.SetDamage(damage);
        Die(true);

        if ((!collision?.gameObject?.GetComponent<AEnemy>()) && (!collision?.gameObject?.GetComponent<AAmmo>()))
            Die(false);
    }

    #endregion

    #region Public methods

    /// <summary>
    /// SimpleRocket 'Update'
    /// </summary>
    /// <param name="deltaTime"></param>
    public void UpdateBullet(float deltaTime)
    {
        if (target != null)
        {
            TurnToGoal();
            Vector3 dir = Target.position - transform.position;
            transform.position += dir.normalized * Speed * deltaTime;
        }
        else
        {
            Die(true);
        }
    }
    public void TurnToGoal()
    {
        var direction = (target.position - transform.position).normalized;
        direction.y = 0f;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    #endregion

    private void Die(bool withSnd)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        GetComponent<Renderer>().enabled = false;
        if (withSnd)
            GetComponent<AudioSource>().Play();
        Destroy(gameObject, 1f);
    }
}

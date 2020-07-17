using TheLastHope.Management.AbstractLayer;
using UnityEngine;

namespace TheLastHope.Ammo
{
    public class SimpleBullet : AAmmo
    {
        #region ObjectPool methods

        /// <summary>
        /// for objectPool
        /// </summary>
        public override void OnPopulate()
        {

        }
        /// <summary>
        /// for objectPool
        /// </summary>
        public override void OnDepopulate()
        {

        }

        #endregion

        #region Collision methods

        protected override void OnTriggerEnter(Collider collision)
        {
            collision?.gameObject?.GetComponent<AEnemy>()?.SetDamage(damage);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if ((!collision?.gameObject?.GetComponent<AEnemy>()) && (!collision?.gameObject?.GetComponent<AAmmo>()))
                Die(false);
            if (collision?.gameObject?.GetComponent<AEnemy>())
                Die(true);
        }


        #endregion
        /// <summary>
        /// Умирать, так с музыкой
        /// </summary>
        /// <param name="withSnd"></param>
        private void Die(bool withSnd)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<Renderer>().enabled = false;
            if (withSnd)
                GetComponent<AudioSource>().Play();
            Destroy(gameObject, 1f);
        }
    }

}

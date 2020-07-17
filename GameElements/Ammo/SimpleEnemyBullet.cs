using TheLastHope.Management.AbstractLayer;
using UnityEngine;

namespace TheLastHope.Ammo
{
    public class SimpleEnemyBullet : AAmmo
    {
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
            if (collision.gameObject?.tag == "Player")
            {
                collision.gameObject?.GetComponent<ABaseObject>()?.SetDamage(damage);
                Die(true);
            }

            if ((!collision.gameObject?.GetComponent<AEnemy>()) && (!collision.gameObject?.GetComponent<AAmmo>()))
            {
                Die(false);
            }
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
            Destroy(gameObject);
        }
    }
}



/// Limerence Games
/// The Last Hope
/// Curator: Ilya Mosckinskov
/// Author: Dmitri Kuzmin

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

        public override void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Player")
            {
                try
                {
                    collision.gameObject.GetComponent<ABaseObject>().SetDamage(damage);
                    Die(true);
                }
                catch { };
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if ((!collision.gameObject.GetComponent<AEnemy>()) && (!collision.gameObject.GetComponent<AAmmo>())) Die(false);
        }

        #endregion

        /// <summary>
        /// Умирать, так с музыкой
        /// </summary>
        /// <param name="withSnd"></param>
        private void Die(bool withSnd)
        {
            var _explosion = gameObject.transform.GetChild(0);
            _explosion.gameObject.SetActive(true);
            var snd = GetComponent<AudioSource>();
            gameObject.GetComponent<Renderer>().enabled = false;
            if (snd)
            {
                if (withSnd) snd.Play();
            }
            Destroy(gameObject, 0.0f);
        }
    }
}



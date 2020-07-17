/// Limerence Games
/// The Last Hope
/// Curator: Ilya Mosckinskov
/// Author: Dmitri Kuzmin

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

        public override void OnTriggerEnter(Collider collision)
        {
            try
            {
                collision.gameObject.GetComponent<AEnemy>().SetDamage(damage);
                Die(true);
            }
            catch { };
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
            Destroy(gameObject, 0.3f);
        }
    }

}

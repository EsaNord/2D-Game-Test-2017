using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        private float _CoolDown = 0.5f;

        [SerializeField]
        private Projectile _ProjectilePrefab;

        private float _timeSinceShot = 0;
        private bool _IsInCoolDown = false;

        public bool Shoot()
        {
            if (_IsInCoolDown)
            {
                return false;
            }

            Projectile projectile =
                Instantiate(_ProjectilePrefab, transform.position, transform.rotation);
            projectile.Launch(transform.up);

            _IsInCoolDown = true;
            _timeSinceShot = 0;

            return true;
        }

        // Update is called once per frame
        void Update()
        {
            if (_IsInCoolDown)
            {
                _timeSinceShot += Time.deltaTime;
                if (_timeSinceShot >= _CoolDown)
                {
                    _IsInCoolDown = false;
                }
            }
        }
    }
}
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
        private SpaceShipBase _Owner;

        public void Init(SpaceShipBase owner)
        {
            _Owner = owner;
        }

        public bool Shoot()
        {
            if (_IsInCoolDown)
            {
                return false;
            }

            //Projectile projectile =
            //    Instantiate(_ProjectilePrefab, transform.position, transform.rotation);
            //projectile.Launch(transform.up, this);
            Projectile projectile = LevelController.Current.GetProjectile(_Owner.UnitType);
            if (projectile != null)
            {
                projectile.transform.position = transform.position;
                projectile.transform.rotation = transform.rotation;
                projectile.Launch(transform.up, this);
            }
            _IsInCoolDown = true;
            _timeSinceShot = 0;

            return true;
        }

        public bool DisposePrjectile(Projectile projectile)
        {
            return LevelController.Current.ReturnProjectile(_Owner.UnitType, projectile);
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
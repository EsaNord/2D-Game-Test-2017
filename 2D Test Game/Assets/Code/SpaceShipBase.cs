using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Health))]
    public abstract class SpaceShipBase : MonoBehaviour, IDamageReceiver
    {
        [SerializeField]
        private float _speed = 3f;

        private Weapon[] _weapons;

        public Weapon[] Weapons
        {
            get { return _weapons; }
        }

        public enum Type
        {
            Player,
            Hostile
        }

        public abstract Type UnitType { get; }

        protected virtual void Awake()
        {
            _weapons = GetComponentsInChildren<Weapon>(includeInactive: true);
            foreach (Weapon weapon in _weapons)
            {
                weapon.Init(this);
            }
            Health = GetComponent<IHealth>();
        }
        
        protected void Shoot()
        {
            foreach (Weapon weapon in Weapons)
            {
                weapon.Shoot();
            }
        }

        public IHealth Health { get; protected set; }

        public float Speed
        {
            get { return _speed; }           
        }

        protected abstract void Move();

        protected virtual void Update()
        {
            try
            {
                Move();
            } catch (System.NotImplementedException e)
            {
                Debug.Log(e);
            } catch (System.Exception e)
            {
                Debug.LogException(e);
            }
            
        }

        public void TakeDamage(int amount)
        {
            Health.DecreaseHealth(amount);
            Die();
        }

        protected virtual void Die()
        {
            if (Health.IsDead)
            {
                Destroy(this.gameObject);
            }
        }

        protected Projectile GetPooledProjectile()
        {
            return LevelController.Current.GetProjectile(UnitType);
        }

        protected bool ReturnBooledProjectile(Projectile projectile)
        {
            return LevelController.Current.ReturnProjectile(UnitType, projectile);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{

    public abstract class SpaceShipBase : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 3f;

        private Weapon[] _weapons;

        public Weapon[] Weapons
        {
            get { return _weapons; }
        }

        protected virtual void Awake()
        {
            _weapons = GetComponentsInChildren<Weapon>(includeInactive: true);
        }
        
        protected void Shoot()
        {
            foreach (Weapon weapon in Weapons)
            {
                weapon.Shoot();
            }
        }

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

                
    }
}

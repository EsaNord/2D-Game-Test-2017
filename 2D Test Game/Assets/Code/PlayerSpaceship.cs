using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{

    public class PlayerSpaceship : SpaceShipBase
    {
        [SerializeField]
        private int _PlayerLives = 3;        
        [SerializeField]
        private Weapon _bWeapon1;
        [SerializeField]
        private Weapon _bWeapon2;

        private bool _Died = false;
        private bool _boosterActive = false;
        private float _boosterTime;

        public const string horizontalAxis = "Horizontal";
        public const string verticalAxis = "Vertical";
        public const string fireButtonName = "Fire1";
        
        // Booster time get and set.
        public float PowerUpTime
        {
            get { return _boosterTime; }
            set { _boosterTime = value; }
        }

        // Booster set and get.
        public bool Booster
        {
            get { return _boosterActive; }
            set { _boosterActive = value; }
        }

        public override Type UnitType
        {
            get
            {
                return Type.Player;
            }
        }

        public int Lives
        {
            get
            {
                return _PlayerLives;
            }            
        }

        public bool Died
        {
            get
            {
                return _Died;
            }            
        }

        protected override void Move()
        {            
            Vector3 movementVector = GetInputVector();
            transform.Translate(movementVector * Speed * Time.deltaTime);
        }

        protected override void Update()
        {
            base.Update();
            BoosterStatus();
            if (Input.GetButton(fireButtonName))
            {
                Shoot();
            }            
            Debug.Log("BStatus: " + _boosterActive);
        }

        // Activates extra weapons if weapon booster is collected.
        // Else deactivates them.
        private void BoosterStatus()
        {
            if (_boosterActive)
            {
                _bWeapon1.gameObject.SetActive(true);
                _bWeapon2.gameObject.SetActive(true);
            }
            else
            {
                _bWeapon1.gameObject.SetActive(false);
                _bWeapon2.gameObject.SetActive(false);
            }
        }
        
        protected override void Die()
        {
            if (Health.IsDead)
            {
                Debug.Log("Player Died");
                _Died = true;
                Destroy(this.gameObject);
            }            
        }

        private Vector3 GetInputVector()
        {           
            float horizontalInput = Input.GetAxis(horizontalAxis);
            float verticalInput = Input.GetAxis(verticalAxis);
            
            return new Vector3(horizontalInput, verticalInput, 0);
        }
            
        // Collision check for boosters.
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Entered");            
            if (collision.gameObject.tag != null)
            {
                Debug.Log("This hit: " + collision.gameObject.tag);
                if (collision.gameObject.tag == "WeaponPowerUp")
                {
                    // If object is weapon booster.
                    Debug.Log("Collected Weapon power up");
                    WeaponPowerUp hit = collision.gameObject.GetComponent<WeaponPowerUp>();
                    hit.Collected = true;
                    _boosterTime += hit.BoosterTime;                    
                    _boosterActive = true;
                }
                else if (collision.gameObject.tag == "HealthPowerUp")
                {
                    // If object is health booster.
                    Debug.Log("Collected Health power up");
                    HealthPowerUp hit = collision.gameObject.GetComponent<HealthPowerUp>();
                    Health.IncreaseHealth(hit.Healing);
                    hit.Collected = true;
                }
            }
        }
    }
}
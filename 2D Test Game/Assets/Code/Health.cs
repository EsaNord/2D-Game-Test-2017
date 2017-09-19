using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField]
        private int _StartingHealth = 100;
        [SerializeField]
        private int _MinimumHealth = 0;
        [SerializeField]
        private int _MaximumHealth = 100;
        
        // Update Method for testing
        public void Update()
        {
            // Keypad + increase health
            // works on enemy and player
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                IncreaseHealth(20);
            }
            // Keypad - decrease health
            // works on enemy and player
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                DecreaseHealth(20);
            }

            Debug.Log(gameObject.name + " / " + CurrentHealth);
        }

        // Objects current health
        public int CurrentHealth
        {
            get
            {                
                return _StartingHealth;
            }
        }

        // Decreases objects health by amount of damaga recieved
        public void DecreaseHealth(int amount)
        {
            _StartingHealth -= amount;
            // If objects health drops to or below minimum health object dies
            // and gameobject is destroyed
            if (_StartingHealth <= _MinimumHealth)
            {
                Debug.Log(gameObject.name + " / " + "DEAD");
                Destroy(this.gameObject);
            }
        }

        // Increases objects health by amount recieved
        public void IncreaseHealth(int amount)
        {
            _StartingHealth += amount;
            // If objects healt is same or exceeds maximum health it is set to maximum health
            if (_StartingHealth >= _MaximumHealth)
            {
                _StartingHealth = _MaximumHealth;
            }
        }
    }
}
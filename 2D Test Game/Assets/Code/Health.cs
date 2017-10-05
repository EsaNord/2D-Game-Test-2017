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

        public bool IsDead
        {
            get { return _StartingHealth <= _MinimumHealth; }
        }
        
        // Update Method for testing
        public void Update()
        {
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
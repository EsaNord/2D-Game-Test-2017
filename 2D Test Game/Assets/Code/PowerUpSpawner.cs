using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PowerUpSpawner : MonoBehaviour
    {
        [SerializeField]
        private HealthPowerUp _healthPowerUp;
        [SerializeField]
        private WeaponPowerUp _weaponPowerUp;

        private int _random;

        private bool _health = false;
        private bool _weapon = false;

        public void SpawnPowerUp(Vector2 spawnPosition)
        {

            // First attempt to spawn health powerup.
            _random = (int)Random.Range(0, 100);
            Debug.Log("Health chance: " + _random);
            SpawnHealth(spawnPosition);

            if (!_health)
            {
                // if health power up didn't spawn then try weapon powerup.
                _random = (int)Random.Range(0, 100);
                Debug.Log("Weapon chance: " + _random);
                SpawnWeapon(spawnPosition);
            }
            
            // Debug logs for test purposes.            
            if (!_health && !_weapon)
            {
                Debug.Log("No powerups spawned");
            }     
        } 
        
        // Spawn health powerup if true.
        private void SpawnHealth(Vector2 spawnPosition)
        {
            if (_random <= _healthPowerUp.SpawnChance)
            {
                Debug.Log("Health Spawned");
                _health = true;
                Instantiate(_healthPowerUp, spawnPosition, Quaternion.identity);                
            }
        }

        // Spawn weapon powerup if true.
        private void SpawnWeapon(Vector2 spawnPosition)
        {
            if (_random <= _weaponPowerUp.SpawnChance)
            {
                Debug.Log("Weapon Spawned");
                _weapon = true;
                Instantiate(_weaponPowerUp, spawnPosition, Quaternion.identity);                
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField]
        private Spawner _hostileSpawner;

        [SerializeField]
        private GameObject[] _hostileMovementTargets;

        float time;
        
        protected void Awake()
        {
            if (_hostileSpawner == null)
            {
                Debug.Log("No Reference");
                _hostileSpawner = GetComponentInChildren<Spawner>();                
            }

            SpawnHostileShip();
        }

        //private void Update()
        //{
        //    time += Time.deltaTime;
        //    if (time >= 5f)
        //    {
        //        SpawnHostileShip();
        //        time = 0;
        //    }
        //}

        private HostileShip SpawnHostileShip()
        {
            GameObject spawnShip = _hostileSpawner.Spawn();
            HostileShip hostileShip = spawnShip.GetComponent<HostileShip>();

            if (hostileShip != null)
            {
                hostileShip.SetMovementTargets(_hostileMovementTargets);
            }

            return hostileShip;
        }
    }
}
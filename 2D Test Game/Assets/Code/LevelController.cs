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
        [SerializeField]
        private float _SpawnInterval = 1;
        [SerializeField]
        private int _MaxHostiles = 500;
        [SerializeField, Tooltip ("Wait time before first spawn")]
        private float _WaitToSpawn = 1;
        [SerializeField]
        private GameObjectsPool _PlayerProjectilePool;
        [SerializeField]
        private GameObjectsPool _HostileProjectilePool;

        private int _Hostiles;

        public static LevelController Current
        {
            get; private set;
        }

        public Projectile GetProjectile(SpaceShipBase.Type type)
        {
            GameObject result = null;

            if (type == SpaceShipBase.Type.Player)
            {
                result = _PlayerProjectilePool.GetPoolObject();
            }
            else
            {
                result = _HostileProjectilePool.GetPoolObject();
            }
            if (result != null)
            {
                return result.GetComponent<Projectile>();
            }

            return null;
        }
                
        protected void Awake()
        {
            if (Current == null)
            {
                Current = this;
            }
            else
            {
                Debug.Log("There are multiple Levelcontrollers in the scene");
            }

            if (_hostileSpawner == null)
            {
                Debug.Log("No Reference");
                _hostileSpawner = GetComponentInChildren<Spawner>();                
            }            
        }

        protected void Start()
        {
            StartCoroutine(SpawnRoutine());
        }              

        private IEnumerator SpawnRoutine()
        {
            yield return new WaitForSeconds(_WaitToSpawn);
            while (_Hostiles <= _MaxHostiles)
            {
                HostileShip hostile = SpawnHostileShip();
                if (hostile != null)
                {
                    _Hostiles++;
                }
                else
                {
                    Debug.Log("SPAWN ERROR");
                    yield break;
                }
                yield return new WaitForSeconds(_SpawnInterval);
            }
        }

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

        public bool ReturnProjectile(SpaceShipBase.Type type, Projectile projectile)
        {
            if (type == SpaceShipBase.Type.Player)
            {
                return _PlayerProjectilePool.ReturnObject(projectile.gameObject);
            }
            else
            {
                return _HostileProjectilePool.ReturnObject(projectile.gameObject);
            }            
        }        
    }
}
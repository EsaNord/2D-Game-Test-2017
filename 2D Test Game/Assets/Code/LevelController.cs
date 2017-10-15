using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        [SerializeField]
        private PlayerSpawner _PlayerSpawner;
        [SerializeField]
        private float _ImmortalityTime = 3f;
        [SerializeField]
        private float _AlphaColorChange = 0.05f;
        [SerializeField]
        private string _LevelName;

        private PlayerSpaceship _PlayerShip;
        private GameObject _PlayerObject;
        private int _PlayerLives;
        private int _Hostiles;
        private float _CountDown;
        private bool _JustSpawned;
        private bool _TimerStarted = false;        

        public static LevelController Current
        {
            get; private set;
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
                Debug.Log("No Reference: Hostile Spawner");
                _hostileSpawner = GetComponentInChildren<Spawner>();                
            }
            if (_PlayerSpawner == null)
            {
                Debug.Log("No Reference: Player Spawner");
                _PlayerSpawner = GetComponentInChildren<PlayerSpawner>();
            }
        }

        protected void Start()
        {
            StartCoroutine(SpawnRoutine());
            SpawnPlayer();
        }

        private void Update()
        {
            if (_PlayerShip.Died)
            {
                if (_PlayerShip.Lives > 0)
                {
                    _PlayerLives--;
                    Respawn();
                }
                else
                {
                    SceneManager.LoadScene(_LevelName);
                }
            }

            if (_JustSpawned)
            {                
                _PlayerObject.GetComponent<CircleCollider2D>().enabled = false;
                Color color = _PlayerObject.GetComponent<SpriteRenderer>().color;
                if (color.a < 0.15f || color.a >= 1f)
                {
                    _AlphaColorChange = _AlphaColorChange * -1;
                }
                color.a += _AlphaColorChange;
                _PlayerObject.GetComponent<SpriteRenderer>().color = color;
                Timer();
            }
            if (!_JustSpawned && _PlayerShip.Lives > 0)
            {
                _PlayerObject.GetComponent<CircleCollider2D>().enabled = true;
                Color color = _PlayerObject.GetComponent<SpriteRenderer>().color;
                color.a = 1f;
                _PlayerObject.GetComponent<SpriteRenderer>().color = color;                
            }
            
            Debug.Log(_PlayerShip.Lives);
        }

        // Timer for the immortality.
        private void Timer()
        {
            if (!_TimerStarted)
            {
                _CountDown = _ImmortalityTime;
                _TimerStarted = true;
                Debug.Log("Timer Started");
            }
            if (_TimerStarted)
            {
                if (_CountDown > 0)
                {
                    _CountDown = _CountDown - Time.deltaTime;
                    //Debug.Log("Time: " + _CountDown);
                }
                else
                {
                    _JustSpawned = false;
                    _TimerStarted = false;
                    Debug.Log("Immortality Ended");
                }
            }
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
        
        // Playerships initial spawn method.
        private void SpawnPlayer()
        {
            GameObject spawnPlayer = _PlayerSpawner.Spawn();
            PlayerSpaceship playerShip = spawnPlayer.GetComponent<PlayerSpaceship>();            
            _PlayerShip = playerShip;
            _PlayerObject = spawnPlayer;
            _PlayerLives = _PlayerShip.Lives;
            Debug.Log("Spawned");
        }

        // Moves player to spawn position after dying.
        // Also sets players died parameter to false.
        public void Respawn()
        {
            GameObject spawnPlayer = _PlayerSpawner.Spawn();
            PlayerSpaceship playerShip = spawnPlayer.GetComponent<PlayerSpaceship>();
            _PlayerShip = playerShip;
            _PlayerObject = spawnPlayer;
            _JustSpawned = true;            
            Debug.Log("Respawned");
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
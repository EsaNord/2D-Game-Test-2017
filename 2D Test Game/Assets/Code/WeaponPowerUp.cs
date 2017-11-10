using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class WeaponPowerUp : MonoBehaviour
    {
        [SerializeField]
        private int _spawnChance = 20;
        [SerializeField]
        private float _timeToLive = 10;
        [SerializeField]
        private float _time = 5;
        [SerializeField]
        private float _speed = 1.5f;

        private bool _collected;
        private float _timePassed;
        private Rigidbody2D _RigidBody;

        private void Awake()
        {
            _RigidBody = GetComponent<Rigidbody2D>();
        }

        // Returns spawn chance.
        public int SpawnChance
        {
            get { return _spawnChance; }
        }

        // Returns weapons booster time.
        public float BoosterTime
        {
            get { return _time; }
        }

        // Sets value true when player collects powerup.
        public bool Collected
        {
            set { _collected = value; }
        }


        // Update is called once per frame
        void Update()
        {
            _timePassed += Time.deltaTime;
            Cleared();                     
        }

        private void FixedUpdate()
        {
            Move();
        }

        // Checks if player has collected this powerup or it has timed out.
        // If either is true then this gameobject is destroyed.
        private void Cleared()
        {
            if (_timePassed >= _timeToLive || _collected)
            {
                Destroy(this.gameObject);
            }
        }

        private void Move()
        {
            Vector2 velosity = -transform.up * _speed;
            Vector2 currentPosition = new Vector2(transform.position.x,
                    transform.position.y);
            Vector2 newPosition = currentPosition + velosity * Time.fixedDeltaTime;
            _RigidBody.MovePosition(newPosition);
        }
    }
}
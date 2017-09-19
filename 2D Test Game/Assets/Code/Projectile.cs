using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{

    public class Projectile : MonoBehaviour, IDamageProvider
    {
        [SerializeField]
        private int damage;
        [SerializeField]
        private float speed;

        private Rigidbody2D _RigidBody;
        private Vector2 _Direction;
        private bool _IsLaunched = false;

        protected void Awake()
        {
            _RigidBody = GetComponent<Rigidbody2D>();

            if (_RigidBody == null)
            {
                Debug.LogError("No rigidbody found!");
            }
        }

        public void Launch(Vector2 Direction)
        {
            _Direction = Direction;
            _IsLaunched = true;
        }

        protected void FixedUpdate()
        {
            if (!_IsLaunched)
            {
                return;
            } else
            {
                Vector2 velosity = _Direction * speed;
                Vector2 currentPosition = new Vector2(transform.position.x,
                    transform.position.y);
                Vector2 newPosition = currentPosition + velosity * Time.fixedDeltaTime;
                _RigidBody.MovePosition(newPosition);
            }
        }

        public int GetDamage()
        {
            return damage;
        }
        
    }
}
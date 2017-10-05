using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour, IDamageProvider
    {
        [SerializeField]
        private int damage;
        [SerializeField]
        private float speed;

        private Rigidbody2D _RigidBody;
        private Vector2 _Direction;
        private bool _IsLaunched = false;
        private Weapon _Weapon;
        private AudioSource _Audio;        

        protected virtual void Awake()
        {
            _RigidBody = GetComponent<Rigidbody2D>();
            _Audio = GetComponent<AudioSource>();

            if (_RigidBody == null)
            {
                Debug.LogError("No rigidbody found!");
            }
        }

        public void Launch(Vector2 Direction, Weapon weapon)
        {
            _Weapon = weapon;
            _Direction = Direction;
            _IsLaunched = true;
            _Audio.PlayOneShot(_Audio.clip);
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

        protected void OnTriggerEnter2D(Collider2D other)
        {
            IDamageReceiver damageReceiver = other.GetComponent<IDamageReceiver>();
            if (damageReceiver != null)
            {
                damageReceiver.TakeDamage(GetDamage());                
            }
            if (_Weapon.DisposePrjectile(this) == false)
            {
                Destroy(this.gameObject);
                Debug.LogError("Error in disposing projectile");
            }
        }

        public int GetDamage()
        {
            return damage;
        }        
        
    }
}
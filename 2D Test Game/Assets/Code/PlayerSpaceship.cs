using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{

    public class PlayerSpaceship : MonoBehaviour
    {
        public float speed = 1.5f;

        // Update is called once per frame
        void Update()
        {
            Vector3 movementVector = GetMovementVector();
            transform.Translate(movementVector * speed * Time.deltaTime);
        }

        private Vector3 GetMovementVector()
        {
            Vector3 movementVector = Vector3.zero;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movementVector += Vector3.left;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                movementVector += Vector3.right;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                movementVector += Vector3.up;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                movementVector += Vector3.down;
            }

            return movementVector;
        }
    }
}
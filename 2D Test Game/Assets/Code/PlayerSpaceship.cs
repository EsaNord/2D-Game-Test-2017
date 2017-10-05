using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{

    public class PlayerSpaceship : SpaceShipBase
    {
        
        public const string horizontalAxis = "Horizontal";
        public const string verticalAxis = "Vertical";
        public const string fireButtonName = "Fire1";

        public override Type UnitType
        {
            get
            {
                return Type.Player;
            }
        }

        protected override void Move()
        {            
            Vector3 movementVector = GetInputVector();
            transform.Translate(movementVector * Speed * Time.deltaTime);
        }

        protected override void Update()
        {
            base.Update();

            if (Input.GetButton(fireButtonName))
            {
                Shoot();
            }
        }

        private Vector3 GetInputVector()
        {           
            float horizontalInput = Input.GetAxis(horizontalAxis);
            float verticalInput = Input.GetAxis(verticalAxis);
            
            return new Vector3(horizontalInput, verticalInput, 0);
        }
    }
}
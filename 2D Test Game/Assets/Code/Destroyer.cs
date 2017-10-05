using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Destroyer : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //Destroy(collision.gameObject);
        }
    }
}
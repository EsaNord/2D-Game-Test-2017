using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class BackGroundScrolling : MonoBehaviour
    {
        [SerializeField]
        private GameObject _BG1;
        [SerializeField]
        private GameObject _BG2;
        [SerializeField]
        private float _Speed = 1f;

        private float _BgHeight;
        private float _Distance;

        private void Start()
        {
            _BgHeight = _BG1.GetComponent<SpriteRenderer>().bounds.size.y;
            _Distance = 0;
            Vector3 bg1Pos = _BG1.transform.position;
            _BG2.transform.position = new Vector3(bg1Pos.x, bg1Pos.y + _BgHeight, bg1Pos.z);
        }

        private void Update()
        {
            ScrollDown();

            if (_Distance >= _BgHeight)
            {
                ResetPosition();
            }
        }

        private void ScrollDown()
        {
            _BG1.transform.position += GetSingleFrameMovement();
            _BG2.transform.position += GetSingleFrameMovement();

            _Distance += Mathf.Abs(GetSingleFrameMovement().y);
        }

        private void ResetPosition()
        {
            _Distance = 0;

            GameObject lowerImage = null;

            if (_BG1.transform.position.y < _BG2.transform.position.y)
            {
                lowerImage = _BG1;
            }
            else
            {
                lowerImage = _BG2;
            }

            lowerImage.transform.position += Vector3.up * 2 * _BgHeight;
        }

        private Vector3 GetSingleFrameMovement()
        {
            return (Vector3.down * _Speed * Time.deltaTime);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class GameObjectsPool : MonoBehaviour
    {
        [SerializeField]
        private int _PoolSize = 1000;
        [SerializeField]
        private GameObject _ObjectPrefab;
        [SerializeField]
        private bool _ShouldGrow;

        private List<GameObject> _Pool;

        protected void Awake()
        {
            _Pool = new List<GameObject>();

            for (int i = 0; i < _PoolSize; i++)
            {
                AddObject();
            }
        }

        private GameObject AddObject(bool isActive = false)
        {
            GameObject obj = Instantiate(_ObjectPrefab);
            if (isActive)
            {
                Activate(obj);
            }
            else
            {
                DeActivate(obj);
            }

            _Pool.Add(obj);

            return obj;
        }

        protected virtual void DeActivate (GameObject obj)
        {            
            obj.SetActive(false);
        }

        protected virtual void Activate (GameObject obj)
        {
            obj.SetActive(true);
        }

        public GameObject GetPoolObject()
        {
            GameObject result = null;

            for (int i = 0; i < _Pool.Count; i++)
            {
                if (!_Pool[i].activeInHierarchy)
                {
                    result = _Pool[i];
                    break;
                }
            }

            if (result == null && _ShouldGrow)
            {
                result = AddObject();
            }

            if (result != null)
            {
                Activate(result);
            }
            
            return result;
        }

        public bool ReturnObject(GameObject obj)
        {
            bool result = false;

            foreach (GameObject poolObject in _Pool)
            {
                if (poolObject == obj)
                {
                    DeActivate(obj);
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
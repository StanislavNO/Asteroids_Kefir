using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly Queue<T> _objects;

        public ObjectPool()
        {
            _objects = new();
        }

        public T Get(Func<T> Create)
        {
            T obj;

            if (_objects.Count > 0)
            {
                obj = _objects.Dequeue();
                obj.gameObject.SetActive(true);
            }
            else
            {
                obj = Create.Invoke();
            }

            return obj;
        }

        public void Put(T obj)
        {
            obj.gameObject.SetActive(false);
            _objects.Enqueue(obj);
        }
    }
}

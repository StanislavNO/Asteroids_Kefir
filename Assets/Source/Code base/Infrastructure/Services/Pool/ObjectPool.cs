using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly Queue<T> _objects;
        private readonly IFactory<T> _factory;

        public ObjectPool(IFactory<T> factory)
        {
            _factory = factory;
            _objects = new();
        }

        public T Get()
        {
            T obj;

            if (_objects.Count > 0)
            {
                obj = _objects.Dequeue();
                obj.gameObject.SetActive(true);
            }

            obj = _factory.Create();

            return obj;
        }

        public void Put(T obj)
        {
            obj.gameObject.SetActive(false);
            _objects.Enqueue(obj);
        }
    }
}

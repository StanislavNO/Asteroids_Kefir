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

        public virtual bool TryGet(out T obj)
        {
            if (_objects.Count > 0)
            {
                obj = _objects.Dequeue();
                obj.gameObject.SetActive(true);
                return true;
            }

            obj = null;
            return false;
        }

        public virtual void Put(T obj)
        {
            obj.gameObject.SetActive(false);
            _objects.Enqueue(obj);
        }
    }
}

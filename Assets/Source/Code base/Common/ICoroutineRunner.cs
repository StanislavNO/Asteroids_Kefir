using System.Collections;
using UnityEngine;

namespace Assets.Source.Code_base
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator routine);
        void StopCoroutine(Coroutine routine);
    }
}

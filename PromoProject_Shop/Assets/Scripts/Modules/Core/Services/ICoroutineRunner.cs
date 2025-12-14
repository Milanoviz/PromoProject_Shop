using System.Collections;
using UnityEngine;

namespace Modules.Core.Services
{
    public interface ICoroutineRunner : IService
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}
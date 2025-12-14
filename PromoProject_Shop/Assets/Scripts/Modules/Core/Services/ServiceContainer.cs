using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Core.Services
{
    public class ServiceContainer
    {
        private readonly Dictionary<Type, IService> _container = new();

        public TService Resolve<TService>() where TService : class, IService
        {
            if (_container.TryGetValue(typeof(TService), out var boxedState))
            {
                return boxedState as TService;
            }
            
            Debug.LogError($"Resolve error. Service {typeof(TService)} wasn't found");
            return null;
        }
        
        internal void Bind<TService>(TService implementation) where TService : class, IService
        {
            if (_container.ContainsKey(typeof(TService)))
            {
                Debug.LogError($"Bind error. Service {typeof(TService)} already was registered");
                return;
            }

            _container[typeof(TService)] = implementation;
        }
    }
}
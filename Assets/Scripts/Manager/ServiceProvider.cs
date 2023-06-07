using System;
using System.Collections.Generic;
using Interfaces;
using ObjectPool;
using UnityEngine;

namespace Manager
{
    public static class ServiceProvider
    {
        public static Dictionary<Type, IProvidable> ProvidableServices = new Dictionary<Type, IProvidable>();
    
        public static T GetService<T>() where T : class, IProvidable
        {
            if (ProvidableServices.ContainsKey(typeof(T)))
            {
                return ProvidableServices[typeof(T)] as T;
            }
            else
            {
                Debug.LogError("Service " + typeof(T) + " not found");
                return null;
            }
        }
        public static T Register<T>(T service) where T : class, IProvidable
        {
            ProvidableServices.Add(typeof(T), service);
            return service;
        }
    
        public static DataManager GetDataManager
        {
            get{ return GetService<DataManager>();}
        }
    
        public static BallPool GetBallPool
        {
            get{ return GetService<BallPool>();}
        }
    }
}

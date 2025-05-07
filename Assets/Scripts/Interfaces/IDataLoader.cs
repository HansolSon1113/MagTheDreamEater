using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Interfaces
{
    public interface IDataLoader<in T>
    {
        string path { get; }
        IDataContainer<T> dataContainer { get; }

        void Load();
    }
}

namespace DataLoader
{
    public abstract class DataLoader<T>: MonoBehaviour, IDataLoader<T>
        where T : UnityEngine.Object
    {
        public abstract string path { get; }
        public abstract IDataContainer<T> dataContainer { get; }

        private void Awake()
        {
            Load();
        }

        public virtual void Load()
        {
            try
            {
                var db = Resources.Load<T>(path);
                dataContainer.data = db;
                
                Destroy(gameObject);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace RTCube.Extensions
{
    public abstract class UnityGameObjectPoolGroups<Key> : IDisposable
    {
        protected GameObject go;

        protected abstract Dictionary<Key, string> assetPaths { get; set; }
        protected abstract string parentName { get; }

        private Dictionary<Key, TypePool> poolGroups = new();
        private Dictionary<Key, GameObject> prefabs = new();

        protected Transform parent;


        public virtual void Init()
        {
            go = new GameObject(parentName);
            parent = go.transform;

            foreach (var item in assetPaths)
            {
                var prefab = AssetLoader.Instance.Load<GameObject>(item.Value);
                prefabs.Add(item.Key, prefab);
                var _pool = new TypePool(prefab, parent);
                _pool.Init();
                poolGroups.Add(item.Key, _pool);
            }
        }

        public virtual GameObject Get(Key key)
        {
            poolGroups.TryGetValue(key, out var pool);

            if (pool == null)
                throw new ArgumentOutOfRangeException($"该key{key}的对象池没有创建");

            return pool.Get();
        }

        public virtual void Release(Key key, GameObject obj)
        {
            poolGroups.TryGetValue(key, out var pool);
            pool.Release(obj);
        }

        public virtual void Dispose()
        {
            foreach (var item in poolGroups)
            {
                item.Value.Dispose();
            }
            poolGroups.Clear();
            prefabs.Clear();
        }

        private class TypePool : IDisposable
        {
            public TypePool(GameObject prefab, Transform parent)
            {
                this.prefab = prefab;
                this.parent = parent;
            }


            protected IObjectPool<GameObject> m_Pool;

            protected virtual int maxCount { get; } = 10000;
            protected virtual int defaultCap { get; } = 10;
            public virtual bool collectionChecks { get; } = true;

            private GameObject prefab;

            private Transform parent;
            public void Init()
            {
                m_Pool = new UnityEngine.Pool.ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, defaultCap, maxCount);
            }


            public GameObject Get()
            {
                return m_Pool.Get();
            }

            public void Release(GameObject obj)
            {
                m_Pool.Release(obj);
            }
            public void Dispose()
            {
                m_Pool.Clear();
            }

            private GameObject CreatePooledItem()
            {
                var instanceGO = GameObject.Instantiate(prefab);
                instanceGO.transform.SetParent(parent);
                return instanceGO;
            }
            void OnReturnedToPool(GameObject gameObject)
            {
                gameObject.transform.SetParent(parent);
                gameObject.SetActive(false);
            }

            void OnTakeFromPool(GameObject gameObject)
            {
                gameObject.SetActive(true);
            }

            void OnDestroyPoolObject(GameObject gameObject)
            {
                GameObject.Destroy(gameObject);
            }

        }

    }
}
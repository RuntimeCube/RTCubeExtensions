using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Pool;

namespace RTCube.Extensions
{
    public abstract class MappingGameObjectPool<Key> : UnityGameObjectPool
    {
        public Dictionary<Key, GameObject> map = new();


        [System.Obsolete("请使用Get(Key)", true)]
        public override GameObject Get()
        {
            return null;
        }

        public GameObject Get(Key key)
        {
            var obj = m_Pool.Get();

            map.Add(key, obj);

            return obj;
        }

        [System.Obsolete("请使用Release(Key)", true)]
        public override void Release(GameObject obj)
        {
        }

        public void Release(Key key)
        {
            var value = map.GetValueOrDefault(key);
            m_Pool.Release(value);
            map.Remove(key);
        }

        public void TryRelease(Key key)
        {
            if (map.TryGetValue(key, out var value))
            {
                m_Pool.Release(value);
                map.Remove(key);
            }
        }


        public bool Contains(Key key)
        {
            return map.ContainsKey(key);
        }
    }

    public abstract class UnityGameObjectPool : IDisposable
    {
        protected IObjectPool<GameObject> m_Pool;

        protected abstract string assetPath { get; }
        protected abstract string parentName { get; }
        protected virtual int maxCount { get; } = 10000;
        protected virtual int defaultCap { get; } = 10;
        public virtual bool collectionChecks { get; } = true;


        private GameObject prefab;

        public Transform parent { get; private set; }

        public virtual void Init()
        {
            prefab = AssetLoader.Instance.Load<GameObject>(assetPath);
            parent = new GameObject(parentName).transform;
            m_Pool = new UnityEngine.Pool.ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, defaultCap, maxCount);
        }

        public virtual void Init(Transform parent)
        {
            prefab = AssetLoader.Instance.Load<GameObject>(assetPath);
            this.parent = parent;
            m_Pool = new UnityEngine.Pool.ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, defaultCap, maxCount);
        }


        public virtual GameObject Get()
        {
            return m_Pool.Get();
        }

        public virtual void Release(GameObject obj)
        {
            m_Pool.Release(obj);
        }

        public virtual void Dispose()
        {
            m_Pool.Clear();
            if (parent != null)
                GameObject.Destroy(parent.gameObject);
        }

        protected virtual GameObject CreatePooledItem()
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
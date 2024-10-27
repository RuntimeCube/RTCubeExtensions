using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace RTCube.Extensions
{
    public static class Pool<T> where T : new()
    {
        private static readonly ObjectPool<T> objectPool = new ObjectPool<T>(null, null);

        public static T Get()
        {
            return objectPool.Get();
        }

        public static void Release(T element)
        {
            objectPool.Release(element);
        }
    }

    public class ObjectPool<T> where T : new()
    {
        private Stack<T> stack = new Stack<T>();
        private UnityAction<T> actionOnGet;
        private UnityAction<T> actionOnRelease;

        public int CountAll { get; private set; }

        public int CountActive
        {
            get { return CountAll - CountInactive; }
        }

        public int CountInactive
        {
            get { return stack.Count; }
        }

        public ObjectPool(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease)
        {
            this.actionOnGet = actionOnGet;
            this.actionOnRelease = actionOnRelease;
        }

        public void Clear()
        {
            stack.Clear();
            stack = null;
            actionOnGet = null;
            actionOnRelease = null;
        }

        public T Get()
        {
            T element;
            if (stack.Count == 0)
            {
                element = new T();
                CountAll++;
            }
            else
            {
                element = stack.Pop();
            }

            if (actionOnGet != null)
                actionOnGet(element);


            return element;
        }

        public void Release(T element)
        {
            if (stack.Count > 0 && ReferenceEquals(stack.Peek(), element))
            {
                Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
            }

            actionOnRelease?.Invoke(element);
            stack.Push(element);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTCube.Extensions
{
    public class MonoSingleton<T> : MonoBehaviour where T : Component
    {
        private static T m_Instance;

        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    // Try and find an existing object of this type
                    m_Instance = FindObjectOfType<T>();
                    if (m_Instance == null)
                    {
                        // No existing object; create a new instance
                        GameObject singletonObject = new GameObject { name = typeof(T).Name };
                        m_Instance = singletonObject.AddComponent<T>();
                    }
                }

                return m_Instance;
            }
        }

        public virtual void Awake()
        {
            if (m_Instance == null)
            {
                m_Instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
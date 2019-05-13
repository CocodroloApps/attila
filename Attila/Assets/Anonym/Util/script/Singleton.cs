using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anonym.Util
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T instance
        {
            get
            {
                return IsNull ? null : _instance;
            }
        }

        public static T Instance
        {
            get
            {
                if (IsNull)
                    CreateInstance();

                return  _instance;
            }
        }

        protected static T CreateInstance()
        {
            return _instance = new GameObject(typeof(T).Name, typeof(T)).GetComponent<T>();
        }

        public static bool IsNull{  
            get{
                if (_instance == null)
                    _instance = FindObjectOfType<T>();

                return _instance == null;
            }
        }

        protected virtual void Awake()
        {
            if (this != instance)
            {
                Destroy(this);
                return;
            }
        }

        public static void DestroySingleton(T destroy)
        {
            if (destroy != null)
            {
                GameObject obj = destroy.gameObject;
                DestroyImmediate(destroy);
                if (obj != null)
                    DestroyImmediate(obj);
            }
            return;
        }
    }
}


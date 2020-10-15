﻿using UnityEngine;
using System.Collections;

namespace Framework
{
	public abstract class AutoInstantiateSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
        private static T mInstance = null;
        
        public bool _PersistentOnSceneChange = false;
		
        protected AutoInstantiateSingleton() { }

		public static T pInstance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = FindObjectOfType<T>();
					if (mInstance == null)
					{
						GameObject obj = new GameObject(typeof(T).ToString());
						mInstance = obj.AddComponent<T>();
					}
				}

				return mInstance;
			}
		}

		protected virtual void Awake()
		{
			if (_PersistentOnSceneChange)
				DontDestroyOnLoad(gameObject);
		}

		protected virtual void Start()
		{
			T[] instance = FindObjectsOfType<T>();
			if (instance.Length > 1)
			{
				Debug.Log(gameObject.name + " has been destroyed because another object already has the same component.");
				Destroy(gameObject);
			}
		}

		protected virtual void OnDestroy()
		{
			if (mInstance == this)
				mInstance = null;
		}
	}
}
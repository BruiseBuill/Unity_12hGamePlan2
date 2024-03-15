using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BF 
{
    public class PoolManager : Single<PoolManager>
    {
        [SerializeField] Pool[] bulletPool;
        [SerializeField] Pool[] vfxPool;
        [SerializeField] Pool[] characterPool;

        Dictionary<string, Pool> dictionary = new Dictionary<string, Pool>();

        protected void Awake()
        {
            int i = 0;
            for (i = 0; i < bulletPool.Length; i++)
            {
                bulletPool[i].Initialize(transform);
                dictionary.Add(bulletPool[i].prefab.name, bulletPool[i]);
            }
            for(i = 0; i < vfxPool.Length; i++)
            {
                vfxPool[i].Initialize(transform);
                dictionary.Add(vfxPool[i].prefab.name, vfxPool[i]);
            }
            for (i = 0; i < characterPool.Length; i++)
            {
                characterPool[i].Initialize(transform);
                dictionary.Add(characterPool[i].prefab.name, characterPool[i]);
            }
        }
        public GameObject Release(string a)
        {
            return dictionary[a].GetFromPool();
        }
        public void Recycle(GameObject a)
        {
            dictionary[a.name].BackToPool(a);
        }
        public void RecycleAll()
        {
            int i;
            for (i = 0; i < bulletPool.Length; i++)
            {
                bulletPool[i].RecycleAll();
            }
            for (i = 0; i < vfxPool.Length; i++)
            {
                vfxPool[i].RecycleAll();
            }
            for (i = 0; i < characterPool.Length; i++)
            {
                characterPool[i].RecycleAll();
            }
        }
        [Serializable]
        class Pool
        {
            public GameObject prefab;
            public int size;
            Queue<GameObject> queue = new Queue<GameObject>();
            List<GameObject> list = new List<GameObject>();
            Transform transParent;
            void Create()
            {
                GameObject a = GameObject.Instantiate(prefab);
                a.transform.SetParent(transParent);
                a.SetActive(false);
                a.name = prefab.name;
                queue.Enqueue(a);
            }
            public void Initialize(Transform parent)
            {
                transParent = parent;
                for (int i = 0; i < size; i++)
                {
                    Create();
                }
            }
            public GameObject GetFromPool()
            {
                GameObject a;
                if (queue.Count <= 0)
                {
                    Create();
                }
                a = queue.Dequeue();
                list.Add(a);
                return a;
            }
            public void BackToPool(GameObject a)
            {
                a.SetActive(false);
#if UNITY_EDITOR    
                if (queue.Contains(a))
                {
                    Debug.LogError(a.transform.position);
                    Debug.LogError(a.name);
                }
#endif
                list.Remove(a);
                queue.Enqueue(a);
            }
            public void RecycleAll()
            {
                for (int i = list.Count - 1; i >= 0; i--) 
                {
                    list[i].GetComponent<BaseControl>().Close();
                }
                list.Clear();
            }
        }
    }
}




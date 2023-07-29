using System.Collections.Generic;
using Objects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Manager
{
    public enum FishType
    {
        None,
        S,
        M,
        L
    }
    public class ObjectPoolManager : MonoBehaviour
    {
        [SerializeField] private GameObject sFish;
        [SerializeField] private GameObject mFish;
        [SerializeField] private GameObject lFish;
        
        [SerializeField] private int poolSize = 20;
        
        [SerializeField] private List<GameObject> sFishPool;
        [SerializeField] private List<GameObject> mFishPool;
        [SerializeField] private List<GameObject> lFishPool;

        // Start is called before the first frame update
        private void Start()
        {
            sFishPool = new List<GameObject>();
            mFishPool = new List<GameObject>();
            lFishPool = new List<GameObject>();
            //리스트 생성
            InitPool(sFishPool, sFish, FishType.S);
            InitPool(mFishPool, mFish, FishType.M);
            InitPool(lFishPool, lFish, FishType.L);
        }

        private void InitPool(List<GameObject>pool, GameObject prefab, FishType type)
        {
            for (int i = 0; i < poolSize; i++)
            {
                var newObj = Instantiate(prefab, this.transform, true);
                pool.Add(newObj);
                newObj.GetComponent<Fish>().Init(this, type);
                newObj.SetActive(false);
            }
        }

        private GameObject GetObjFromPool(List<GameObject>pool, GameObject prefab, FishType type)
        {
            if (pool.Count == 0)
            {
                var newObj = Instantiate(prefab, this.transform, true);
                newObj.GetComponent<Fish>().Init(this, type);
                return newObj;
            }
            GameObject returnObj = pool[0];
            pool.Remove(returnObj);
            return returnObj;
        }
        
        public GameObject GetFish(FishType type)
        {
            return type switch
            {
                FishType.S => GetObjFromPool(sFishPool, sFish, type),
                FishType.M => GetObjFromPool(mFishPool, mFish, type),
                FishType.L => GetObjFromPool(lFishPool, lFish, type),
                _ => null
            };
        }
    
        public void ReturnFish(GameObject obj, FishType type)
        {
            obj.SetActive(false);
            switch (type)
            {
                case FishType.S:
                    sFishPool.Add(obj);
                    break;
                case FishType.M:
                    mFishPool.Add(obj);
                    break;
                case FishType.L:
                    lFishPool.Add(obj);
                    break;
            }
        }
    }
}

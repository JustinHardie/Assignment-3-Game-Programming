using System.Collections.Generic;
using UnityEngine;

public class IcebergPool : MonoBehaviour {
    public GameObject icebergPrefab;
    public int poolSize = 10;

    private List<GameObject> pool;

    void Awake() {
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++) {
            GameObject obj = Instantiate(icebergPrefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetPooledIceberg() {
        foreach (var iceberg in pool) {
            if (!iceberg.activeInHierarchy) {
                return iceberg;
            }
        }

    
        GameObject newIceberg = Instantiate(icebergPrefab);
        newIceberg.SetActive(false);
        pool.Add(newIceberg);
        return newIceberg;
    }
}

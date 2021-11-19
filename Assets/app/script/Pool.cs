using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool<T>
{
    public Queue<T> projectilePool = new Queue<T>();
    public List<T> spawnedPool = new List<T>();

    private List<GameObject> prefabs;
    private int count;
    private GameObject holder;

    public Pool(GameObject prefab, int count)
    {
        prefabs = new List<GameObject>();
        prefabs.Add(prefab);
        CreationMethod();
    }

    public Pool(GameObject[] prefabs, int count)
    {
        this.prefabs = prefabs.ToList();
        CreationMethod();
    }

    private void CreationMethod()
    {
        holder = new GameObject("holder");
        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    private void CreateObject()
    {
        GameObject go = GameObject.Instantiate(prefabs[UnityEngine.Random.Range(0, this.prefabs.Count)], Vector3.zero, Quaternion.identity);
        go.SetActive(false);
        go.transform.SetParent(holder.transform);
        iPoolable<T> t = go.GetComponent<T>() as iPoolable<T>;
        t.Init(this);
        projectilePool.Enqueue((T)t);
    }

    public T GetFromPool()
    {
        if (projectilePool.Count == 0)
        {
            CreateObject();
        }
        T proj = projectilePool.Dequeue();
        spawnedPool.Add(proj);
        return proj;
    }

    public void ReturnToPool(T proj)
    {
        spawnedPool.Remove(proj);
        projectilePool.Enqueue(proj);
    }
}

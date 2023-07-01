using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public interface IPoolable {
    void Reset();
}

public class BasePool<T> : MonoBehaviour where T : Component
{
    [SerializeField]  T prefab;
    [SerializeField] int defaultSize = 100;
    [SerializeField] int maxSize = 500;

    public ObjectPool<T> pool;

    public int ActiveCount => pool.CountActive;
    public int InactiveCount => pool.CountInactive;
    public int TotalCount => pool.CountAll;

    public T Prefab { get => prefab; set => prefab = value; }

    public void Initialize()
    {
        pool = new ObjectPool<T>(OnCreatePoolItem, OnGetPoolItem, OnReleasePoolItem, OnDestroyPoolItem, true, defaultSize, maxSize);
    }

    protected virtual T OnCreatePoolItem() => Instantiate(Prefab, transform);
    protected virtual void OnGetPoolItem(T obj) => obj.gameObject.SetActive(true);
    protected virtual void OnReleasePoolItem(T obj) => obj.gameObject.SetActive(false);
    protected virtual void OnDestroyPoolItem(T obj) => Destroy(obj.gameObject);

    public T Get() => pool.Get();
    public void Release(T obj) => pool.Release(obj);
    public void Clear() => pool.Clear();

}

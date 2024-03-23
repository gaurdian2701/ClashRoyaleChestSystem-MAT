using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericObjectPool<T> where T : class
{
    public List<PooledItem> pooledItems = new List<PooledItem>();

    public virtual T GetItem()
    {
        if (pooledItems.Count > 0)
        {
            PooledItem item = pooledItems.Find(item => !item.isUsed);
            if (item != null)
            {
                item.isUsed = true;
                return item.Item;
            }
        }
        return CreateNewPooledItem();
    }

    private T CreateNewPooledItem()
    {
        PooledItem newItem = new PooledItem();
        newItem.Item = CreateItem();
        newItem.isUsed = true;
        pooledItems.Add(newItem);
        return newItem.Item;
    }
    protected virtual T CreateItem()
    {
        throw new NotImplementedException("CreateItem() method not implemented in derived class");
    }
    public virtual void ReturnItem(T item)
    {
        PooledItem pooledItem = pooledItems.Find(i => i.Item.Equals(item));
        pooledItem.isUsed = false;
    }
    public class PooledItem
    {
        public T Item;
        public bool isUsed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class GDictManager<T>
{
    public Dictionary<uint, T> Collection
    { get; private set; } = new Dictionary<uint, T>();

    public List<T> GetComplete => Collection.Values.ToList(); 

    public void Add(T newObj, uint id)
    {
        if (Collection.ContainsKey(id))
        {
#if DEBUG
            Debug.LogError($"Coud not add object with id {id} because it already exist in this collection!"); // Should not happen Id should be unique
#endif
            return;
        }

        Collection[id] = newObj;
    }

    public bool Remove(uint uniqueId)
    {
        if (Collection.ContainsKey(uniqueId))
        {
            Collection.Remove(uniqueId);
            return true;
        }

# if DEBUG
        Debug.LogWarning($"Coud not remove object with id {uniqueId} for this collection because it was not found.");
# endif

        return false;
    }

    public T Get(uint uniqueId)
    {
        T obj;
        if (Collection.TryGetValue(uniqueId, out obj))
        {
            return obj;
        }

        return default(T);
    }
}

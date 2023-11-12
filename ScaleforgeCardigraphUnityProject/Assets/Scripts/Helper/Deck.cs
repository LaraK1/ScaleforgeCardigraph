using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Deck<T> : ScriptableObject
{
    public Dictionary<uint, T> BaseDeck
    { get; protected set; } = null;

    public List<T> GetComplete
    {
        get
        {
            if(BaseDeck == null)
            {
                BaseDeck = Init();
            }
            return BaseDeck.Values.ToList();
        }
    }

    public void Add(T newObj, uint id)
    {
        if (BaseDeck.ContainsKey(id))
        {
#if DEBUG
            Debug.LogError($"Coud not add card with id {id} because it already exist in this deck!"); // Should not happen Id should be unique
#endif
            return;
        }

        BaseDeck[id] = newObj;
    }

    public bool Remove(uint uniqueId)
    {
        if (BaseDeck.ContainsKey(uniqueId))
        {
            BaseDeck.Remove(uniqueId);
            return true;
        }

# if DEBUG
        Debug.LogWarning($"Coud not remove card with id {uniqueId} for this deck because it was not found.");
# endif

        return false;
    }

    public T Get(uint uniqueId)
    {
        T obj;
        if (BaseDeck.TryGetValue(uniqueId, out obj))
        {
            return obj;
        }

        return default(T);
    }

    protected virtual Dictionary<uint, T> Init()
    {
        return new Dictionary<uint, T>();
    }
}


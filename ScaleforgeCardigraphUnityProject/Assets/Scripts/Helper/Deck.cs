using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Deck<T>
{
    public Dictionary<uint, T> BaseDeck
    { get; protected set; } = new Dictionary<uint, T>();

    public List<T> GetComplete => BaseDeck.Values.ToList();

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

    public Deck(List<T> initializeToCollection, string propertyKeyName = "Id")
    {
        if (initializeToCollection == null) return;

        foreach (var item in initializeToCollection)
        {
            var keyProperty = item.GetType().GetProperty(propertyKeyName)?.GetValue(item, null);
            if(keyProperty == null || keyProperty.GetType() != typeof(uint))
            {
#if DEBUG
                Debug.LogWarning($"An item could not be initialized to the deck");
#endif
                continue;
            }
            uint key = (uint)keyProperty;
            if(!BaseDeck.ContainsKey(key))
            {
                BaseDeck[key] = item;
                continue;
            }
#if DEBUG
            Debug.LogWarning($"Duplicate for element with id {key}");
#endif
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericPool<T> : MonoBehaviour where T : Component
{
    [SerializeField] private T _prefab;
    [SerializeField] private Transform _parent = null;
    [SerializeField] private int _startCount = 3;

    // pool
    public static GenericPool<T> Instance { get; private set; }
    private List<T> _objects = new List<T>();
    protected List<T> _objectsInUse = new List<T>();

    private void Awake()
    {    
        // simple generic singleton
        Instance = this;
    }

    private void Start()
    {
       // add all prefabs to the pool
       AddObjects(_startCount);
    }

    /// <summary>Get random object out of pool.</summary>
    public T Get()
    {
        // create new objects if pool is empty
        if (_objects.Count == 0)
        {
            AddObjects(1);
        }

        // remove object out of pool and return it
        T currentObject = _objects[_objects.Count-1];
        _objects.RemoveAt(_objects.Count-1);
        currentObject.gameObject.SetActive(true);
        _objectsInUse.Add(currentObject);
        RemovedFromPool(currentObject);
        return currentObject;
    }

    /// <summary>Objects can return themself to the pool.</summary>
    /// <param name="objectToReturn">Object that should be returned to the pool.</param>
    public void ReturnToPool(T objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        _objectsInUse.Remove(objectToReturn);
        _objects.Add(objectToReturn);
        AddedToPool(objectToReturn);
    }

    /// <summary>Instantiate new object and add it to pool.</summary>
    /// <param name="count">Number of objects to be added.</param>
    private void AddObjects(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var newObject = GameObject.Instantiate(_prefab);
            if(_parent)
                newObject.transform.SetParent(_parent);
            newObject.gameObject.SetActive(false);
            var rect = newObject.GetComponent<RectTransform>();
            // This is for ui elements because they get f* up when instanciated
            if(rect)
            {
                rect.localPosition = new Vector3(0,0,0);
                rect.localScale = Vector3.one;
            }
            _objects.Add(newObject);
            AddedObject(newObject);
        }
    }

    /// <summary>Adds all used objects back to the pool in correct order.</summary>
    public void ResetAll()
    {
        while(_objectsInUse.Count > 0)
        {
            ReturnToPool(_objectsInUse[_objectsInUse.Count-1]);
        }
    }

    protected virtual void AddedObject(T newObject){}
    protected virtual void AddedToPool(T newObject){}
    protected virtual void RemovedFromPool(T newObject){}
}
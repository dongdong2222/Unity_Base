using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Pool
{
    public GameObject Original { get; private set; }
    public Transform Root { get; set; }

    Stack<Poolable> _poolStack = new Stack<Poolable>();

    public void Init(GameObject original, int count = 5)
    {
        Original = original;
        Root = new GameObject().transform;
        Root.name = $"{original.name}_ROOT";

        for (int i = 0; i < count; i++)
            Push(Create());

        }

    //pool에 들어갈 object 생성
    Poolable Create()
    {
        GameObject go = Object.Instantiate<GameObject>(Original);
        go.name = Original.name;
        return go.GetOrAddComponent<Poolable>();
    }

    //오브젝트를 pool에 집어넣는다 (= Destroy)
    public void Push(Poolable poolable)
    {
        if (poolable == null)
            return;

        poolable.transform.parent = Root;
        poolable.gameObject.SetActive(true);
        poolable.IsUsing = false;

        _poolStack.Push(poolable);
    }

    //오브젝트를 pool에서 꺼낸다 (= Instantiate)
    public Poolable Pop(Transform parent)
    {
        Poolable poolable;

        if (_poolStack.Count > 0)
            poolable = _poolStack.Pop();
        else
            poolable = Create();

        poolable.gameObject.SetActive(true);

        //DontDestroyOnLoad 해제용
        //if(parent == null)
        //    poolable.transform.parent = Managers.

        poolable.transform.parent = parent;
        poolable.IsUsing = true;

        return poolable;

    }
}
public class PoolManager
{
    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    Transform _root;

    public void Init()
    {
        if(_root = null)
        {
            _root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = _root;

        _pool.Add(original.name, pool);
    }

    //pool이 있다면 pool에 push 없으면 destroy
    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;
        if(_pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        _pool[name].Push(poolable);
    }
}

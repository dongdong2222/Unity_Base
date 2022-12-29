using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension 
{
    //go.GetOrAddComponent<Component>(); 형식으로 사용할수 있도록 해줌
    public static T GetOrAddComponent<T>(this GameObject go) where T: UnityEngine.Component
    {
        return Util.GetOrAddComponent<T>(go);
    }
}

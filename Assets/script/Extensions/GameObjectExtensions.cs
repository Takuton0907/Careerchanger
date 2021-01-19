using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtension
{
    /// <summary>
    /// 指定されたインターフェイスを実装したコンポーネントを持つオブジェクトを検索します
    /// </summary>
    public static T[] FindObjectsOfInterface<T>() where T : class
    {
        List<T> ts = new List<T>();
        
        foreach (var n in Object.FindObjectsOfType<Component>())
        {
            var component = n as T;
            if (component != null)
            {
                ts.Add(component);
            }
        }
        return ts.ToArray();
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool
{
	public List<GameObject> pool { get; private set; }
	public int currentCount { get; private set; }

	public ObjectPool()
	{
		currentCount = 0;
		pool = new List<GameObject>();
	}

	/// <summary>
	/// 指定した分指定したオブジェクトをプールする対象に追加する
	/// </summary>
	/// <param name="_obj">_obj.</param>
	public void Pool(GameObject parent, GameObject _obj, int _number)
	{
		int count = _number;
		for (int i = 0; i < count; i++)
		{
			var go = Object.Instantiate(_obj, parent.transform.position, Quaternion.identity);
			go.transform.SetParent(parent.transform, false);
			go.SetActive(false);
			pool.Add(go);
		}
	}

	/// <summary>
	/// プールしているオブジェクトを順番に返していく。使えるオブジェクトがなければ作る
	/// </summary>
	public GameObject Instantiate(Vector3 position, Vector3 scale)
	{
		if (pool == null)
		{
			return null;
		}
		GameObject retObj = pool[currentCount];
        if (retObj.activeSelf)
        {
			retObj = Object.Instantiate(pool[0], position, Quaternion.identity, pool[0].transform.parent);
        }

		retObj.transform.localPosition = position;
		retObj.transform.localScale = scale;
		currentCount++;
		if (currentCount >= pool.Count)
		{
			currentCount = 0;
		}
		retObj.SetActive(true);
		return retObj;
	}
}
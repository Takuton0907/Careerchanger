using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutinTest : MonoBehaviour
{
    IEnumerator m_actonCoroutin;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(m_actonCoroutin);
        m_actonCoroutin = jhdksah();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_actonCoroutin);
        jhdksah().MoveNext();
        jhdksah().MoveNext();
        jhdksah().MoveNext();
    }

    IEnumerator jhdksah()
    {
        yield return null;

        Debug.Log("コルーチンスタート");

        yield return hskdhfkshk();

        yield return new WaitForSeconds(3);

        Debug.Log("コルーチンストップ");

    }

    IEnumerator hskdhfkshk()
    {
        StopCoroutine(m_actonCoroutin);

        Debug.Log("一時停止");
        yield return new WaitForSeconds(2);

        StartCoroutine(m_actonCoroutin);

    }
}

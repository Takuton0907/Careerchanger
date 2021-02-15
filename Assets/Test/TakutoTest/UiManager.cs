using UnityEngine;

public class UiManager : MonoBehaviour
{
    IUiHandler[] m_uiHandlers;

    private void Awake()
    {
        LevelManager.Instance.UiManager = this;
    }

    private void Start()
    {
        GetUiHandlers();
    }

    void GetUiHandlers()
    {
        m_uiHandlers = GameObjectExtensions.FindObjectsOfInterface<IUiHandler>();
    }

    /// <summary> 特定のUIを有効化します </summary>
    /// <typeparam name="T">扱いたいクラス(IUiHandlerをしている必要がある)</typeparam>
    public void Enable<T>()
    {
        foreach (var item in m_uiHandlers)
        {
            if (item is T)
            {
                item.Enable();
            }
        }
    }
    /// <summary> 特定のUIだけを有効化し他を無効にします </summary>
    /// <typeparam name="T">扱いたいクラス(IUiHandlerをしている必要がある)</typeparam>
    public void EnableOnlyOne<T>()
    {
        foreach (var item in m_uiHandlers)
        {
            if (item is T)
            {
                item.Enable();
            }
            else
            {
                item.Disable();
            }
        }
    }
    /// <summary> すべてのUIを有効化します </summary>
    public void EnableAll()
    {
        foreach (var item in m_uiHandlers)
        {
            item.Enable();
        }
    }
    /// <summary> 特定のUIを無効化します </summary>
    /// <typeparam name="T">扱いたいクラス(IUiHandlerをしている必要がある)</typeparam>
    public void Disable<T>()
    {
        foreach (var item in m_uiHandlers)
        {
            if (item is T)
            {
                item.Disable();
            }
        }
    }
    /// <summary> すべてのUIを無効化します </summary>
    public void EnableAll<T>()
    {
        foreach (var item in m_uiHandlers)
        {
            item.Disable();
        }
    }
}

public interface IUiHandler
{
    void Enable();
    void Disable();
}
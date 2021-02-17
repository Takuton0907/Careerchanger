using UnityEngine;
using UnityEngine.SceneManagement;

public static class UiManager
{
    static IUiHandler[] m_uiHandlers;

    [RuntimeInitializeOnLoadMethod]
    static void Init()
    {
        SceneManager.sceneLoaded += GetUiHandlers;
        GetUiHandlers();
    }

    static void GetUiHandlers(Scene nextScene, LoadSceneMode mode)
    {
        m_uiHandlers = GameObjectExtensions.FindObjectsOfInterface<IUiHandler>();
    }
    static void GetUiHandlers()
    {
        m_uiHandlers = GameObjectExtensions.FindObjectsOfInterface<IUiHandler>();
    }

    /// <summary> 特定のUIを有効化します </summary>
    /// <typeparam name="T">扱いたいクラス(IUiHandlerをしている必要がある)</typeparam>
    public static void Enable<T>()
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
    public static void EnableOnlyOne<T>()
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
    public static void EnableAll()
    {
        foreach (var item in m_uiHandlers)
        {
            item.Enable();
        }
    }
    /// <summary> 特定のUIを無効化します </summary>
    /// <typeparam name="T">扱いたいクラス(IUiHandlerをしている必要がある)</typeparam>
    public static void Disable<T>()
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
    public static void EnableAll<T>()
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
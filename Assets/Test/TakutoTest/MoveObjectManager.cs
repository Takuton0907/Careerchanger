﻿using System.Collections.Generic;
using UnityEngine;

public class MoveObjectManager : MonoBehaviour, IGameoverEvent, IClearEvent
{
    List<EnemyBase> m_enemies = new List<EnemyBase>();

    PlayerCon m_playerCon = null;

    public int TotalEnemyCount { get; private set; } = 0;

    private void Awake()
    {            
        LevelManager.Instance.MoveObjectManager = this;   
    }

    public int EnemyCount() => m_enemies.Count;
    /// <summary> EnemyBaseの配列を返します </summary>
    public EnemyBase[] GetEnemyData() => m_enemies.ToArray();
    /// <summary> EnemyBaseを持ったオブジェクトを配列に追加します </summary>
    public void AddEnemy(EnemyBase enemy)
    {
        m_enemies.Add(enemy);
        if (enemy.KillCount)
        {
            TotalEnemyCount++;
        }
    }
    /// <summary> EnemyBaseを持ったオブジェクトを配列から削除します </summary>
    public void RemoveEnemy(EnemyBase enemy) => m_enemies.Remove(enemy);
    /// <summary> 管理されているUpdate </summary>
    public void ManagedUpdate()
    {
        foreach (var item in m_enemies)
        {
            item.ManagedUpdate();
        }
        m_playerCon.ManagedUpdate();
    }
    /// <summary> 描画が重ならないようにずらす </summary>
    public void MoveObjectsInit()
    {
        for (int i = 0; i < m_enemies.Count; i++)
        {
            m_enemies[i].transform.position = new Vector3(m_enemies[i].transform.position.x, m_enemies[i].transform.position.y, i / 100f);
        }
        m_playerCon.Init();
    }
    /// <summary> GameOver時の動作 </summary>
    public void GameOver()
    {
        AllStop();
    }
    /// <summary> Clear時の動作 </summary>
    public void Clear()
    {
        AllStop();
    }
    /// <summary> オブジェクトをすべて止めます </summary>
    public void AllStop()
    {
        foreach (var item in m_enemies)
        {
            item.StateChange(MoveState.Stop);
        }
        m_playerCon.MoveStop();
    }
    /// <summary> 止まっているオブジェクトをすべて動かします </summary>
    public void AllMove()
    {
        foreach (var item in m_enemies)
        {
            if (item.gameObject.activeSelf) item.StateChange(MoveState.Action);
        }
        m_playerCon.MoveStart();
    }
    /// <summary> アニメ―ジョンのみ先に動かす </summary>
    public void AllEnemyAnimationOnlyStart()
    {
        foreach (var item in m_enemies)
        {
            item.AnimationOnlyStart();
        }
    }
}
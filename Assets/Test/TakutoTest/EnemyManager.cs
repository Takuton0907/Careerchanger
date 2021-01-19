using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IGameoverEvent, IClearEvent
{
    List<EnemyBase> m_enemies = new List<EnemyBase>();

    private int m_totalEnmeyconter = 0;
    public int TotalEnemyCount { get { return m_totalEnmeyconter; } }

    private void Awake()
    {            
        LevelManager.Instance.EnemyManager = this;   
    }

    public int EnemyCount() => m_enemies.Count;

    public EnemyBase[] GetEnemyData() => m_enemies.ToArray();

    public void AddEnemy(EnemyBase enemy)
    {
        m_enemies.Add(enemy);
        if (enemy.KillCount)
        {
            m_totalEnmeyconter++;
        }
    }

    public void RemoveEnemy(EnemyBase enemy) => m_enemies.Remove(enemy);

    public void ManagedUpdate()
    {
        foreach (var item in m_enemies)
        {
            item.ManagedUpdate();
        }
    }

    public void EnemySetup()
    {
        for (int i = 0; i < m_enemies.Count; i++)
        {
            m_enemies[i].transform.position = new Vector3(m_enemies[i].transform.position.x, m_enemies[i].transform.position.y, i / 100f);
        }
    }

    public void GameOver()
    {
        AllEnemyStop();
    }

    public void Clear()
    {
        AllEnemyStop();
    }

    public void AllEnemyStop()
    {
        foreach (var item in m_enemies)
        {
            item.StateChange(MoveState.Stop);
        }
    }

    public void AllEnemyMove()
    {
        foreach (var item in m_enemies)
        {
            if (item.gameObject.activeSelf) item.StateChange(MoveState.Action);
        }
    }

    public void AllEnemyAnimationOnlyStart()
    {
        foreach (var item in m_enemies)
        {
            item.AnimationOnlyStart();
        }
    }
}


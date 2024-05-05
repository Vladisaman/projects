using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int coins = 0;
    public delegate void CoinCollected(int coins);
    public event CoinCollected collected;

    public int health;
    public delegate void DamageTaken(int health);
    public event DamageTaken damaged;

    private static PlayerData instance;

    public static PlayerData GetInstance()
    {
        if(instance == null)
        {
            instance = new PlayerData();
        }

        return instance;
    }

    public void CollectCoin()
    {
        coins++;
        collected.Invoke(coins);
    }

    public void TakeDamage(int damage = 1)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
        }
        damaged.Invoke(health);
    }
}

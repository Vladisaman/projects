using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text CoinText;
    [SerializeField] TMP_Text HealthText;

    // Start is called before the first frame update
    void Start()
    {
        PlayerData.GetInstance().collected += UpdateCoins;
        PlayerData.GetInstance().damaged += UpdateHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCoins(int coins)
    {
        CoinText.text = "Coins: " + coins.ToString();
    }

    public void UpdateHealth(int health)
    {
        HealthText.text = "health: " + health.ToString();
    }
}

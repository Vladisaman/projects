using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] Rigidbody2D Ball;
    [SerializeField] float Force;
    [SerializeField] TMP_Text PlayerScore;
    [SerializeField] TMP_Text EnemyScore;
    [SerializeField] EnemyScript Enemy;
    public int PlayerScoreCount;
    public int EnemyScoreCount;

    // Start is called before the first frame update
    void Start()
    {
        BallStart();
    }

    private void Update()
    {

    }

    void BallStart()
    {
        StopCoroutine(BallSpeeder());
        if(PlayerScoreCount - EnemyScoreCount >= 2)
        {
            Enemy.AdjustDifficulty(-0.1F);
        } 
        else if (EnemyScoreCount - PlayerScoreCount >= 2)
        {
            Enemy.AdjustDifficulty(0.1F);
        }

        Ball.position = Vector2.zero;
        Ball.velocity = Vector2.zero;
        if (Random.Range(1, 3) == 1)
        {
            Ball.AddForce(Vector2.left * Force);
        } else
        {
            Ball.AddForce(Vector2.right * Force);
        }
        StartCoroutine(BallSpeeder());
    }

    public void GoalPlayer()
    {
        PlayerScoreCount++;
        PlayerScore.text = PlayerScoreCount.ToString();
        BallStart();
    }

    public void GoalEnemy()
    {
        EnemyScoreCount++;
        EnemyScore.text = EnemyScoreCount.ToString();
        BallStart();
    }

    IEnumerator BallSpeeder()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0F);
            Ball.AddForce(Ball.velocity * 2F);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class GameManager : NetworkBehaviour
{
    [SerializeField] GameObject Ball;
    public GameObject GameOverPanel;

    public int LeftScore;
    public int RightScore;

    public TMP_Text LeftScoreText;
    public TMP_Text RightScoreText;
    public TMP_Text LeftScoreTextEnd;
    public TMP_Text RightScoreTextEnd;

    private void Awake()
    {
        RightScoreText = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LibraryScript>().RightScore;
        LeftScoreText = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LibraryScript>().LeftScore;
        RightScoreTextEnd = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LibraryScript>().RightScoreEnd;
        LeftScoreTextEnd = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LibraryScript>().LeftScoreEnd;
        GameOverPanel = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LibraryScript>().GameOverPanel;
        UpdateText();
    }

    [ServerRpc]
    public void SpawnBallServerRpc()
    {
        GameObject ball = Instantiate(Ball);
        ball.GetComponent<NetworkObject>().Spawn();
        ReSpawnBallServerRpc();
    }

    [ServerRpc]
    public void ReSpawnBallServerRpc()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        ball.transform.position = Vector3.zero;
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        StartCoroutine(Wait(ball));
    }

    IEnumerator Wait(GameObject ball)
    {
        yield return new WaitForSeconds(1.0f);
        int dir = Random.Range(1, 3);
        int[] zdirs = { 1, -1 };
        int zdir = Random.Range(0, 2);
        ball.GetComponent<Rigidbody>().AddForce(new Vector3(dir == 2 ? 300f : -300f, 0, zdirs[zdir] * 50));
    }

    // Update is called once per frame
    void UpdateText()
    {
        LeftScoreText.text = LeftScore.ToString();
        RightScoreText.text = RightScore.ToString();

        UpdateTextClientRpc(RightScore, LeftScore);
    }

    [ClientRpc]
    void UpdateTextClientRpc(int RightScore, int LeftScore)
    {
        LeftScoreText.text = LeftScore.ToString();
        RightScoreText.text = RightScore.ToString();
    }

    [ServerRpc]
    public void IncreaseGoalCountServerRpc(bool isRight)
    {
        if (isRight)
        {
            LeftScore++;
        } else
        {
            RightScore++;
        }
        UpdateText();

        if (LeftScore >= 3 || RightScore >= 3)
        {
            EndGameText();
        }
        else
        {
            ReSpawnBallServerRpc();
        }
    }

    void EndGameText()
    {
        GameOverPanel.SetActive(true);
        LeftScoreTextEnd.text = LeftScore.ToString();
        RightScoreTextEnd.text = RightScore.ToString();

        UpdateTextEndClientRpc(RightScore, LeftScore);
    }

    [ClientRpc]
    void UpdateTextEndClientRpc(int RightScore, int LeftScore)
    {
        GameOverPanel.SetActive(true);
        LeftScoreTextEnd.text = LeftScore.ToString();
        RightScoreTextEnd.text = RightScore.ToString();
    }
}

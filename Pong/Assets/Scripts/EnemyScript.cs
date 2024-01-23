using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Rigidbody2D Ball;
    [SerializeField] float MoveSpeed;
    private float LagTime = 0.6F;
    private Vector2 CurrDir;

    private const float MaxLagTime = 1.0F;
    private const float MinLagTime = 0.2F;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(LagTimer());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + CurrDir * Time.deltaTime * MoveSpeed);
    }

    Vector2 CalculateDirection()
    {
        if (rb.position.y < Ball.position.y)
        {
            return Vector2.up;
        }
        else
        {
            return Vector2.down;
        }
    }

    IEnumerator LagTimer()
    {
        while (true)
        {
            CurrDir = CalculateDirection();
            yield return new WaitForSeconds(LagTime);
        }
    }

    public void AdjustDifficulty(float Adjustment)
    {
        LagTime += Adjustment;

        if (LagTime > MaxLagTime)
        {
            LagTime = MaxLagTime;
        } else if(LagTime < MinLagTime)
        {
            LagTime = MinLagTime;
        }
    }
}

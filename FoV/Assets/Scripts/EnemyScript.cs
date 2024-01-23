using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus;

public class EnemyScript : MonoBehaviour
{
    private PlayerScript player;
    private float DistancetoPlayer;
    private bool isRotating;
    [SerializeField] int PatrolIndex;
    private Transform[] PatrolPoints;
    private int moveIndex;
    private NavMeshAgent nva;
    private PatrolState state;
    private IEnumerator LookoutCoroutine;
    private Vector3 TargetRotPos;
    private bool isAudioCheckTrue;

    // Start is called before the first frame update
    void Start()
    {
        state = PatrolState.PATROLING;
        moveIndex = 0;
        nva = GetComponent<NavMeshAgent>();
        nva.updateRotation = false;
        nva.updateUpAxis = false;
        PatrolPoints = new Transform[GameManager.instance.PatrolPoints[PatrolIndex].childCount];
        for (int i = 0; i < PatrolPoints.Length; i++)
        {
            PatrolPoints[i] = GameManager.instance.PatrolPoints[PatrolIndex].GetChild(i);
        }
        isRotating = false;
        player = SeekerScript.player;
        GameManager.instance.CheckPing += ChasePlayer;
        nva.SetDestination(PatrolPoints[moveIndex].position);
        LookoutCoroutine = ContactTimeCheck();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion TargetRot = Quaternion.Euler(transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y,
                 Mathf.Atan2(TargetRotPos.y - transform.position.y, TargetRotPos.x - transform.position.x) * Mathf.Rad2Deg
                );
        transform.rotation = Quaternion.Lerp(transform.rotation, TargetRot, Time.deltaTime * 2);

        if (state == PatrolState.PATROLING)
        {
            if (Vector2.Distance(PatrolPoints[moveIndex].position, transform.position) <= 0.4)
            {
                moveIndex++;
                if (moveIndex >= PatrolPoints.Length)
                {
                    moveIndex = 0;
                }
                nva.SetDestination(PatrolPoints[moveIndex].position);

                TargetRotPos = PatrolPoints[moveIndex].position;
            }
        } else if(state == PatrolState.SEARCHING && isAudioCheckTrue)
        {
            nva.SetDestination(player.transform.position);
        }

        DistancetoPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (AudioCheck() || isRotating)
        {
            TargetRotPos = player.transform.position;
            StopCoroutine(LookoutCoroutine);
            isAudioCheckTrue = true;
        } else if(AudioCheck() == false && isAudioCheckTrue)
        {
            isAudioCheckTrue = false;
            StartCoroutine(LookoutCoroutine);
        }
        if (VisualCheck())
        {
            StopCoroutine(LookoutCoroutine);
            GameManager.instance.CheckPing();
        } else
        {
            isRotating = false;
        }
    }
    bool AudioCheck()
    {
        if (DistancetoPlayer <= 5 && player.isMoving())
        {
            state = PatrolState.SEARCHING;
            return true;
        } else
        {
            return false;
        }
    }
    bool VisualCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right + new Vector3(0, 0.25F, 0), 4);
        if (hit.collider != null)
        {
            if (hit.transform.tag == "Player")
            {
                state = PatrolState.SEARCHING;
                return true;
            }
        }
        return false;
    }
    void ChasePlayer()
    {
        if (DistancetoPlayer <= 7.5)
        {
            isRotating = true;
        }
    }
    IEnumerator ContactTimeCheck()
    {
        Debug.Log("STARTED COROUTINE");
        yield return new WaitForSeconds(5.0F);
        Debug.Log("STOPPED LOOKING");
        state = PatrolState.PATROLING;
        nva.SetDestination(PatrolPoints[moveIndex].position);
        TargetRotPos = PatrolPoints[moveIndex].position;
        LookoutCoroutine = ContactTimeCheck();
    }
}
public enum PatrolState
{
    PATROLING,
    SEARCHING
}

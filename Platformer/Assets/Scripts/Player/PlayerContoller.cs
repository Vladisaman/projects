using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerContoller : MonoBehaviour
{
    private MovementScript movement;
    private AnimationScript animation;
    private PlayerData playerData;
    private GroundCheckScript groundCheck;

    [SerializeField] private float speed;
    [SerializeField] private float jump;


    // Start is called before the first frame update
    void Awake()
    {
        groundCheck = GetComponentInChildren<GroundCheckScript>();
        playerData = PlayerData.GetInstance();
        playerData.health = 5;
        playerData.coins = 0;
        animation = new AnimationScript(GetComponent<Animator>());
        movement = new MovementScript(GetComponent<Rigidbody2D>(), speed, jump);

        PlayerData.GetInstance().damaged += CheckDeath;
    }

    private void Start()
    {
        CheckpointManager.GetInstance().checkpointPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float axis = Input.GetAxisRaw("Horizontal");
        if (axis != 0) {
            movement.Move(axis);
            animation.ChangeState(AnimationStates.RUN);
        } 
        else
        {
            animation.ChangeState(AnimationStates.IDLE);
        }
        if(Input.GetKeyDown(KeyCode.Space) && groundCheck.canJump)
        {
                movement.Jump();
        }
    }

    public void CheckDeath(int health)
    {
        if(health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } 
        else
        {
            transform.position = CheckpointManager.GetInstance().checkpointPos;
        }
    }

    private void OnDestroy()
    {
        PlayerData.GetInstance().damaged -= CheckDeath;
    }
}
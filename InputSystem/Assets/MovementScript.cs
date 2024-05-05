using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    NewControls controls;

    [SerializeField] Rigidbody2D Player1;
    public float speed;
    [SerializeField] Rigidbody2D Player2;
    [SerializeField] GameObject bullet;



    public float reloadTime;
    private bool canShoot;
    public int maxAmmo;
    private int currAmmo;

    // Start is called before the first frame update
    void Awake()
    {
        controls = new NewControls();
        controls.KeyBoardMap.Shoot.performed += x => Shoot();
        controls.GamepadMap.Shoot.performed += x => GamepadShoot();
        canShoot = true;
        currAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        LookAtMouse();
        LookAtStick();
    }

    private void FixedUpdate()
    {
        Move();
        MoveGamepad();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    public void Move()
    {
        Vector3 TEMPVECTOR = controls.KeyBoardMap.MoveXY.ReadValue<Vector2>().normalized * (speed * Time.deltaTime);
        Player1.MovePosition(new Vector3(Player1.position.x, Player1.position.y, 0) + Quaternion.Euler(0, 0, Player1.rotation-90) * TEMPVECTOR);
    }

    public void MoveGamepad()
    {
        Vector3 TEMPVECTOR = controls.GamepadMap.Movement.ReadValue<Vector2>().normalized * (speed * Time.deltaTime);
        Player2.MovePosition(new Vector3(Player2.position.x, Player2.position.y, 0) + TEMPVECTOR);
    }

    public void LookAtMouse()
    {
        Vector2 mousePos = controls.KeyBoardMap.MousePosition.ReadValue<Vector2>();
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 result = (worldPoint - Player1.transform.position).normalized;
        float degree = Mathf.Atan2(result.y, result.x) * Mathf.Rad2Deg;
        Player1.rotation = degree;
    }

    public void LookAtStick()
    {
        Vector2 mousePos = controls.GamepadMap.Rotation.ReadValue<Vector2>().normalized;
        float degree = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        Player2.rotation = degree;
    }
    public void Shoot()
    {
        if (canShoot)
        {
            GameObject tempBullet = Instantiate(bullet, Player1.transform.position, Quaternion.identity);
            tempBullet.transform.rotation = Player1.transform.rotation;

            currAmmo--;
            if (currAmmo <= 0)
            {
                StartCoroutine(Reload());
            }
        }
    }
    public void GamepadShoot()
    {
        GameObject tempBullet = Instantiate(bullet, Player2.transform.position, Quaternion.identity);
        tempBullet.transform.rotation = Player2.transform.rotation;
    }
    public IEnumerator Reload()
    {
        canShoot = false;
        yield return new WaitForSeconds(reloadTime);
        currAmmo = maxAmmo;
        canShoot = true;
    }
}

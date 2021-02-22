using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //Cached Reference
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject bullet;

    //Config
    [SerializeField] float speed = 6;

    //game parameters
    private bool gameStarted = false;
    public bool GameStarted
    {
        get { return gameStarted; }
        set { gameStarted = value; }
    }

    bool alive = true;
    public bool Alive
    {
        get { return alive; }
        set
        {
            if (value == false)
            {
                //doosh unana
                //rb.constraints = new RigidbodyConstraints();
                GameManager.instance.afterDieMenu.SetActive(true);
            }
            else // else means true or alive
            {
                ////rotate iig hyazgaarlana
                //rb.constraints = RigidbodyConstraints.FreezeRotation;
                //transform.rotation = Quaternion.Euler(0, 0, 0);
                //rb.velocity = new Vector3(0, 0, 0);
                if (transform.position.y < -5)
                {
                    Vector3 spawnPos = new Vector3(0, 1, transform.position.z);
                    transform.position = spawnPos;
                }
            }
            alive = value;
        }
    }

    Coroutine firingCoroutine;
    [SerializeField]
    private int remainingBullet;
    public int RemainingBullet
    {
        get { return remainingBullet; }
        set
        {
            if (value < 0)
            {
                remainingBullet = 0;
                return;
            }
            remainingBullet = value;

            GameManager.instance.RefreshBulletText();
        }
    }

    [SerializeField]
    int maxBulletCapacity = 90;
    public int MaxBulletCapacity
    {
        get { return maxBulletCapacity; }
        set
        {
            maxBulletCapacity = value;
        }
    }

    private float distance;
    public float Distance
    {
        get { return distance; }
        set
        {
            distance = value;
            GameManager.instance.RefreshDistanceText();
        }
    }

    //Movement params
    float horizontalInput;
    Vector3 jump;
    float jumpForce = 3f;
    bool isGrounded;

    //Bottle Sizes
    [SerializeField]
    Vector3 bottleSize;
    Vector3 emptyBottle = new Vector3(0.6f, 0.9f, 0.6f),
            bigger1 = new Vector3(0.7f, 0.9f, 0.7f),
            bigger2 = new Vector3(0.8f, 0.9f, 0.8f),
            bigger3 = new Vector3(0.9f, 0.9f, 0.9f),
            bigger4 = new Vector3(1f, 0.9f, 1f),
            bigger5 = new Vector3(1.1f, 0.9f, 1.1f),
            bigger6 = new Vector3(1.2f, 0.9f, 1.2f),
            bigger7 = new Vector3(1.3f, 0.9f, 1.3f),
            bigger8 = new Vector3(1.4f, 0.9f, 1.4f),
            bigger9 = new Vector3(1.5f, 0.9f, 1.5f),
            bigger10 = new Vector3(1.6f, 0.9f, 1.6f);

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Ketchupnii bottleruu handah
        bottleSize = gameObject.transform.GetChild(1).GetChild(0).localScale;
        RemainingBullet = 0;
        StartingPoint();
    }

    public void StartingPoint()
    {
        horizontalInput = 0;
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        GameManager.instance.beforeStartMenu.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (GameStarted)
        {
            if (!Alive)
            {
                return;
            }
            Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
            Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMove + horizontalMove);
        }
    }


    // Update is called once per frame
    void Update()
    {
        Distance = transform.position.z;

        if (GameStarted)
        {
            //player unawal uhne
            Vector3 playerPosition = transform.position;
            if (playerPosition.y < -5)
            {
                Die();
            }
            //PlayerControl();
            //Jump();
            Fire();
            AdjustSize();
        }
    }



    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            if (firingCoroutine != null)
            {
                StopCoroutine(firingCoroutine);
            }
        }
    }

    private IEnumerator FireContinously()
    {
        while (RemainingBullet > 0)
        {
            GameObject createdBullet = Instantiate(bullet, transform.GetChild(0).position, transform.GetChild(0).rotation);
            createdBullet.transform.Rotate(Vector3.left * 90);
            Rigidbody bulletRb = createdBullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(Vector3.forward * 40, ForceMode.Impulse);
            RemainingBullet--;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void AdjustSize()
    {
        if (RemainingBullet == 0)
        {
            bottleSize = emptyBottle;
        }
        else if (RemainingBullet > 0 && RemainingBullet <= 10)
        {
            bottleSize = bigger1;
        }
        else if (RemainingBullet > 10 && RemainingBullet <= 20)
        {
            bottleSize = bigger2;
        }
        else if (RemainingBullet > 20 && RemainingBullet <= 30)
        {
            bottleSize = bigger3;
        }
        else if (RemainingBullet > 30 && RemainingBullet <= 40)
        {
            bottleSize = bigger4;
        }
        else if (RemainingBullet > 40 && RemainingBullet <= 50)
        {
            bottleSize = bigger5;
        }
        else if (RemainingBullet > 50 && RemainingBullet <= 60)
        {
            bottleSize = bigger6;
        }
        else if (RemainingBullet > 60 && RemainingBullet <= 70)
        {
            bottleSize = bigger7;
        }
        else if (RemainingBullet > 70 && RemainingBullet <= 80)
        {
            bottleSize = bigger8;
        }
        else if (RemainingBullet > 80 && RemainingBullet <= 90)
        {
            bottleSize = bigger9;
        }
        else
        {
            bottleSize = bigger10;
        }
        gameObject.transform.GetChild(1).GetChild(0).localScale = bottleSize;
    }

    public void StartGame()
    {
        GameStarted = true;
        Alive = true;
    }

    private void PlayerControl()
    {
        //horizontalInput = Input.GetAxis("Horizontal");
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }
    void OnCollisionExit()
    {
        isGrounded = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
    }
    public void Die()
    {
        GameStarted = false;
        Alive = false;
    }
    void Jump()
    {
        //if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        //{
        //    rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        //    isGrounded = false;
        //}
    }
    public void IncreaseBulletBy10()
    {
        RemainingBullet += 10;
        if (RemainingBullet > MaxBulletCapacity)
        {
            RemainingBullet = MaxBulletCapacity;
        }
        GameManager.instance.RefreshBulletText();
    }
}

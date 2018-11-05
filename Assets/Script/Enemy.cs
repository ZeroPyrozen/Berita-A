using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public enum EState : byte { ROAM, ATTACK, TESTSTATE }; // Enemy's State

    public EState initState;    // Enemy's default state
    private EState currState;   // Current state changed in the programming

    private Animator anim;
    private SpriteRenderer sprRend;

    private Vector3 originPos;
    private Vector2 currDir;

    private RaycastHit2D found;

    public float speed = 1.75f;
    public float roamRange = 2f;
    public float standTime = 2f;
    private float standTimer;

    public float health = 10f;

    private int playerLayer;

    [SerializeField] private bool isFacingRight = true;
    private bool firstDeadCheck = false;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

        sprRend = GetComponent<SpriteRenderer>();

        originPos = transform.position;

        CurrState = initState;

        standTimer = standTime;

        playerLayer = LayerMask.GetMask("Player");
    }


    // Update is called once per frame
    void Update()
    {
        if (isFacingRight)
        {
            currDir = Vector2.right;
            sprRend.flipX = false;
        }
        else
        {
            currDir = Vector2.left;
            sprRend.flipX = true;
        }

        CheckState();
    }

    private void CheckState()
    {
        CheckDead();
        if (!firstDeadCheck)
        {
            switch (CurrState)
            {
                case EState.ROAM:
                    Roam();
                    break;
                case EState.ATTACK:
                    Attack();
                    break;
            }
        }
    }

    private void CheckDead()
    {
        
        if(health <= 0 && !firstDeadCheck)
        {
            firstDeadCheck = true;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            anim.SetTrigger("dead");
            Invoke("DestroyEnemy", 1.5f);
        }

    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    #region State Function
    private void Roam()
    {
        if (PlayerInRange())
        {
            CurrState = EState.ATTACK;
            return;
        }

        if (Mathf.Abs(originPos.x - transform.position.x) < roamRange)
        {
            Move();
        }
        else
        {
            anim.SetBool("isStanding", true);
            if (standTimer > 0)
            {
                
                standTimer -= Time.deltaTime;
                
            }
            else
            {
                standTimer = standTime;
                originPos = transform.position;
                isFacingRight = !isFacingRight; // change direction
                anim.SetBool("isStanding", false);
                Move();
            }
        }
    }

    private void Attack()
    {
        Invoke("FinishAttack", 3f);
    }
    #endregion

    private bool PlayerInRange()
    {
        found = Physics2D.Raycast(transform.position, currDir, 0.20f, playerLayer);

        if (found)
        {
            Debug.Log("Found!");
            return true;
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            Debug.Log("You Lose!");
        }
    }

    private void FinishAttack()
    {
        CurrState = EState.ROAM;
    }
    
    private void Move()
    {
        if (isFacingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }


    /////// PROPERTIES ///////
    public EState CurrState
    {
        get
        {
            return this.currState;
        }
        set
        {
            switch (value)
            {
                case EState.ROAM:
                    anim.SetBool("isWalking", true);
                    break;
                case EState.ATTACK:
                    anim.SetBool("isWalking", false);
                    anim.SetTrigger("attack");
                    break;
            }
            this.currState = value;
        }
    }
}
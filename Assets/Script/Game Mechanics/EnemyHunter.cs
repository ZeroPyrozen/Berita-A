using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHunter : MonoBehaviour
{
    public enum EState : byte { ROAM, ATTACK, TESTSTATE }; // Enemy's State

    public EState initState;    // Enemy's default state
    [SerializeField] private EState currState;   // Current state changed in the programming

    public GameObject topBody;
    public GameObject botBody;

    public EnemyShoot gun;

    private Animator anim;
    private SpriteRenderer sprRendTop;
    private SpriteRenderer sprRendBot;

    private Vector3 originPos;
    private Vector2 currDir;

    private RaycastHit2D found;

    public float speed = 1.75f;
    public float roamRange = 2f;
    public float standTime = 2f;
    [SerializeField] private float standTimer;

    public float health = 10f;

    public int strongDimension;
    public int dmgModif;
    private int playerLayer;

    public bool isFacingRight = true;
    private bool firstDeadCheck = false;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

        sprRendTop = topBody.GetComponent<SpriteRenderer>();
        sprRendBot = botBody.GetComponent<SpriteRenderer>();

        originPos = transform.position;

        CurrState = initState;

        standTimer = standTime;

        dmgModif = 1;

        playerLayer = LayerMask.GetMask("Player");
    }


    // Update is called once per frame
    void Update()
    {
        ChangeDmgModif();
        if (isFacingRight)
        {
            currDir = Vector2.right;
            sprRendTop.flipX = false;
            sprRendBot.flipX = false;
        }
        else
        {
            currDir = Vector2.left;
            sprRendTop.flipX = true;
            sprRendBot.flipX = true;
        }

        CheckState();


    }

    private void ChangeDmgModif()
    {
        if (strongDimension != GameManager.instance.dimensionType)
        {
            dmgModif = 2;
        }
        else
        {
            dmgModif = 1;
        }
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

        if (health <= 0 && !firstDeadCheck)
        {
            firstDeadCheck = true;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            transform.Find("Upper Body").gameObject.SetActive(false);
            transform.Find("Feet").gameObject.SetActive(false);
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
        Invoke("FinishAttack", 1f);
    }
    #endregion

    private bool PlayerInRange()
    {
        found = Physics2D.Raycast(transform.position, currDir, 1f, playerLayer);

        if (found)
        {
            Debug.Log("Found!");
            return true;
        }
        return false;
    }

    private void FinishAttack()
    {
        CurrState = EState.ROAM;
    }

    private void GunShoot()
    {
        gun.Shoot();
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
                    Invoke("GunShoot", 0.3f);
                    break;
            }
            this.currState = value;
        }
    }
}
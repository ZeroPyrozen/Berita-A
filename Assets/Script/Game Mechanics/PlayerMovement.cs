using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    private Animator anim;
    private Rigidbody2D rbPlayer;
    public PlayerSound sound;

    public GameObject firePoint;
    public GameObject upperBody;

    private Vector2 fireOriginPos   = new Vector2 (0.0642f, -0.0458f);
    private Vector2 fireUpPos       = new Vector2 (-0.035f, 0.0935f);
    private Vector2 fireDownPos     = new Vector2 (0.0046f, -0.1365f);
    private Vector2 localUpperBody;


    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float jumpForce = 50f;

    public bool startFaceLeft = false;
    [SerializeField] private bool facingRight = true; //default : true
    [SerializeField] private bool facingUp = false;
    [SerializeField] private bool facingDown = false;
    private bool isJumping = false;
    private bool playerDead = false;

    #region Get
    public bool GetFacingRight()
    {
        return this.facingRight;
    }
    public bool GetFacingUp()
    {
        return this.facingUp;
    }
    public bool GetFacingDown()
    {
        return this.facingDown;
    }
    #endregion

    #region Flip Sprite
    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();
    }
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    #endregion
    
    #region Movement
    protected void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            sound.playJumpSound();
            rbPlayer.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
            isJumping = true;
            anim.SetBool("isJumping", true);
        }
    }

    protected void Move()
    {
        // Kembali ke State Idle
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetBool("isWalking", false);
        }
        
        if (Input.GetKey(KeyCode.LeftArrow)) // Gerak Kiri
        {
            anim.SetBool("isWalking", true); //Animasi Walk
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow)) // Gerak Kanan
        {
            anim.SetBool("isWalking", true); //Animasi Walk
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }
    #endregion

    #region GunDirection
    private void GunDirection() //To check vertical direction that the player is heading
    {
        FirePointDirection();
        //Change state back after releasing the button
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            anim.SetBool("facingUp", false);
            facingUp = false;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            anim.SetBool("facingDown", false);
            facingDown = false;
        }

        // Get Input then change the sprite
        if (Input.GetKey(KeyCode.UpArrow) && !facingDown)
        {
            anim.SetBool("facingUp", true);
            facingUp = true;
        }
        if (Input.GetKey(KeyCode.DownArrow) && !facingUp)
        {
            anim.SetBool("facingDown", true);
            facingDown = true;
        }
    }

    private void FirePointDirection() //Start Checking to change firePoint
    {
        localUpperBody = upperBody.transform.position;
        if (facingRight)
        {
            ChangeFirePoint(1);
        }
        else if (!facingRight)
        {
            ChangeFirePoint(-1);
        }
    }

    private void ChangeFirePoint(int dir)
    {
        Vector2 tempOrigin = fireOriginPos;
        Vector2 tempUp = fireUpPos;
        Vector2 tempDown = fireDownPos;

        tempOrigin.x *= dir;
        tempUp.x *= dir;
        tempDown.x *= dir;

        if (!facingUp && !facingDown)
        {
            firePoint.transform.position = tempOrigin + localUpperBody;
        }
        else
        {
            if (facingUp)
            {
                firePoint.transform.position = tempUp + localUpperBody;
            }
            if (facingDown)
            {
                firePoint.transform.position = tempDown + localUpperBody;
            }
        }
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            anim.SetBool("isJumping", false);
            isJumping = false;
        }
        if (coll.gameObject.tag == "Spike" || coll.gameObject.tag == "Enemy")
        {
            playerDead = true;
            DeadAnim();
            Debug.Log("Mati lu mampus");
            Invoke("RoundEnd", 1.5f);
        }
    }

    private void RoundEnd()
    {
        GameManager.instance.RoundEnd(false);
        CancelInvoke();
    }

    private void DeadAnim()
    {

        transform.Find("Upper Body").gameObject.SetActive(false);
        transform.Find("Feet").gameObject.SetActive(false);
        rbPlayer.gravityScale = 0;
        anim.SetTrigger("dead");
    }
       
    private void Restart()
    {
        if (playerDead && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            playerDead = false;
            //Application.LoadLevel(Application.loadedLevel);
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    private void StartFacingLeft()
    {
        Flip();
    }

    // Use this for initialization
    void Start () {
        rbPlayer = GetComponent<Rigidbody2D>();
        localUpperBody = upperBody.transform.position;
        firePoint.transform.position = fireOriginPos + localUpperBody;
        anim = GetComponent<Animator>();
        anim.SetBool("isWalking", false);
        playerDead = false;
        if (startFaceLeft) StartFacingLeft();
    }
	
	// Update is called once per frame
	void Update () {
        if (!playerDead)
        {
            Move();
            Jump();
            GunDirection();
        }
        Restart();
    }
}

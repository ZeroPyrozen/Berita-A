using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Animator anim;
    private Rigidbody2D rbPlayer;

    [SerializeField] public float speed = 5f;
    [SerializeField] public float velocity = 50f;

    public bool facingRight;
    public bool isJumping;

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
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping)
        {
            rbPlayer.AddForce(Vector2.up * velocity, ForceMode2D.Force);
            isJumping = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            isJumping = false;
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
            //FixedUpdate(); //Arah Kiri
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow)) // Gerak Kanan
        {
            anim.SetBool("isWalking", true); //Animasi Walk
           //FixedUpdate();//Arah Kanan
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }
    #endregion

    // Use this for initialization
    void Start () {
        rbPlayer = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        facingRight = true;
        isJumping = false;
        anim.SetBool("isWalking", false);
    }
	
	// Update is called once per frame
	void Update () {
        Move();
        Jump();
	}
}

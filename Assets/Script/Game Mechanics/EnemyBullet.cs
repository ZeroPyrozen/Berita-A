using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Vector2 dir = new Vector2(0, 0);
    private Vector2 originPos;

    [SerializeField] private float speed = 2.75f;
    [SerializeField] private float range = 2f;
    private float xMove;
    private float yMove;
    private float damage = 1.5f;

    public float speedMod = 1;

    // Use this for initialization
    void Start()
    {
        originPos = transform.position;
        //dir = new Vector2(0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dir.x != 0 || dir.y != 0)
        {
            if (xMove < range && yMove < range)
            {
                transform.Translate(dir * speed  * Time.deltaTime);
            }
        }
    }

    private void Update()
    {
        xMove = Mathf.Abs(transform.position.x - originPos.x);
        yMove = Mathf.Abs(transform.position.y - originPos.y);
        if (xMove >= range || yMove >= range)
        {
            Destroy(gameObject);
        }
    }
    public void Initialise(Vector2 dir)
    {
        //Debug.Log("intialised");
        this.dir = dir;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Upper Body" || collision.gameObject.name == "Feet" 
            ||collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit");
            collision.gameObject.GetComponent<PlayerMovement>().playerDead = true;
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}

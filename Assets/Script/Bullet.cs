using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Vector2 dir = new Vector2(0, 0);
    private Vector2 originPos;

    [SerializeField] private float speed = 2.75f;
    [SerializeField] private float range = 2f;
    private float xMove;
    private float yMove;
    private float damage = 4f;

    // Use this for initialization
    void Start() {
        originPos = transform.position;
        //dir = new Vector2(0, 0);
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (dir.x != 0 || dir.y != 0)
        {
            if (xMove < range && yMove < range)
            {
                transform.Translate(dir * speed * Time.deltaTime);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().health -= damage;
            Destroy(gameObject);
        }
    }
}

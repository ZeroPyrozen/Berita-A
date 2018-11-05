using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public GameObject firePoint;
    public GameObject bulletPrefab;
    private GameObject bullet;

    [SerializeField] private float cooldown = 0.45f;

    private bool facingRight;
    private bool facingUp;
    private bool facingDown;
    private bool canShoot = true;
	
	// Update is called once per frame
	void Update () {
        facingRight = GetComponent<PlayerMovement>().getFacingRight();
        facingUp    = GetComponent<PlayerMovement>().getFacingUp();
        facingDown  = GetComponent<PlayerMovement>().getFacingDown();


        if (Input.GetKeyDown(KeyCode.Z) && canShoot)
        {
            bullet = (GameObject) Instantiate(bulletPrefab, (Vector2)firePoint.transform.position, Quaternion.identity);
            if (!facingUp && !facingDown)
            {
                if(facingRight) bullet.GetComponent<Bullet>().Initialise(Vector2.right);
                else bullet.GetComponent<Bullet>().Initialise(Vector2.left);
            }
            else if (facingUp)
            {
                bullet.GetComponent<Bullet>().Initialise(Vector2.up);

            }
            else if (facingDown)
            {
                bullet.GetComponent<Bullet>().Initialise(Vector2.down);
            }
            StartCoroutine(CanShoot());
        }
	}
     
    private IEnumerator CanShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}

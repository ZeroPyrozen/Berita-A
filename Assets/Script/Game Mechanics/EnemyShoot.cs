using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject firePoint;
    public GameObject bulletPrefab;
    private GameObject bullet;

    [SerializeField] private float cooldown = 1f;
    [SerializeField] private float cd;

    private bool facingRight;
    private bool canShoot = true;

    // Update is called once per frame
    void Update()
    {
        if (cd > 0) cd -= Time.deltaTime;
        facingRight = GetComponent<EnemyHunter>().isFacingRight;
    }

    public void Shoot()
    {
        if (cd <= 0)
        {
            bullet = (GameObject)Instantiate(bulletPrefab, (Vector2)firePoint.transform.position,
                Quaternion.identity);
            if (GetComponent<EnemyHunter>().dmgModif == 1) bullet.GetComponent<EnemyBullet>().speedMod *= 1.5f;
            if (facingRight) bullet.GetComponent<EnemyBullet>().Initialise(Vector2.right);
            else bullet.GetComponent<EnemyBullet>().Initialise(Vector2.left);
            cd = cooldown;
        }
        
    }
}

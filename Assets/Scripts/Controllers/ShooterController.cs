using Global;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    private Queue<GameObject> bullets = new Queue<GameObject>();

    public InputMapping.PlayerTag playerTag;
    public int bulletCount = 10;
    public float bulletSpeed = 300.0f;

    void Update()
    {
        if (Input.GetButtonDown(InputMapping.GetInputName(playerTag, InputMapping.Input.RB)))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet;
        if (bullets.Count < bulletCount)
        {
            bullet = Instantiate(Resources.Load("Bullet", typeof(GameObject))) as GameObject;
        }
        else
        {
            bullet = bullets.Dequeue();
        }

        bullet.transform.position = transform.position + transform.forward;
        Rigidbody bulletBody = bullet.GetComponent<Rigidbody>();
        bulletBody.velocity = Vector3.zero;
        bulletBody.AddForce(transform.forward * bulletSpeed);
        bullets.Enqueue(bullet);
    }
}

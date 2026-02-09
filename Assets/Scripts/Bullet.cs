using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed = 10;

    private void Update()
    {
        transform.Translate(BulletSpeed * Time.deltaTime * Vector3.right);
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerApproacher playerApproacher = collision.gameObject.GetComponent<PlayerApproacher>();
        if (playerApproacher != null)
        {
            playerApproacher.KillEnemy();
            Destroy(gameObject);
        }
    }
}

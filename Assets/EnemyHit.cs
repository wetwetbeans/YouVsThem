using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.tag == "Bullet")
        {
            Debug.Log("YO");
            enemy.GetComponent<EnemyManager>().TakeDamage(collision.gameObject.GetComponent<BulletProjectiles>().GetDamage());
            enemy.GetComponent<EnemyManager>().bulletPlayerGameObject = collision.gameObject.GetComponent<BulletProjectiles>().player;
        }
    }
}

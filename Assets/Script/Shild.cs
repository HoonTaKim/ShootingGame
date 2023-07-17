using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shild : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet") || collision.CompareTag("EnemyBullet(not)"))
        {
            DictionaryPool.Inst.Set(collision.gameObject);
        }
    }
}

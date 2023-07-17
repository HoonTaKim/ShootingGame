using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyBullet : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 0;
    [SerializeField] protected float bPower = 0;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
            DictionaryPool.Inst.Set(this.gameObject);

        if (collision.CompareTag("Player"))
        {
            collision.SendMessage("Hit", bPower, SendMessageOptions.DontRequireReceiver);
            DictionaryPool.Inst.Set(this.gameObject);
        }
    }

}

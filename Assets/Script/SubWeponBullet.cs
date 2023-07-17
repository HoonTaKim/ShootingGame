using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubWeponBullet : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0;
    [SerializeField] int bPower = 0;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        moveSpeed = 10f;
        bPower = 1;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Wall"))
            {
                DictionaryPool.Inst.Set(this.gameObject);
            }

            if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
            {
                collision.SendMessage("Hit", bPower, SendMessageOptions.DontRequireReceiver);
                DictionaryPool.Inst.Set(this.gameObject);
            } 
        }
    }
}

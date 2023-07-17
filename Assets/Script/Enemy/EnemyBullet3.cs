using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet3 : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 0;
    [SerializeField] protected float bPower = 0;
    [SerializeField] Vector2 moveDir;

    [SerializeField] GameObject bulletPrefab = null;
    [SerializeField] GameObject bulletRotate = null;
    [SerializeField] PalyerController target = null;

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        target = FindObjectOfType<PalyerController>();
        moveSpeed = 5f;
        bPower = 1;
        if (target != null)
            moveDir = target.transform.position - transform.position;
    }

    private void Update()
    {
        if (target != null)
            transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);
        else
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Bust();
            DictionaryPool.Inst.Set(this.gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            collision.SendMessage("Hit", bPower, SendMessageOptions.DontRequireReceiver);
            Bust();
            DictionaryPool.Inst.Set(this.gameObject);
        }
    }

    void Bust()
    {
        for (int i = 0; i <= 360; i += 30)
        {
            bulletRotate.transform.rotation = Quaternion.Euler(new Vector3(0, 0, i));
            DictionaryPool.Inst.Get(bulletPrefab, this.transform.position, bulletRotate.transform.rotation);
        }
    }
}

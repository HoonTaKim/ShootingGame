using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomRange : MonoBehaviour
{
    [SerializeField] Transform tr = null;
    [SerializeField] float trX = 0f;
    [SerializeField] float trY = 0f;

    [SerializeField] GameObject target = null;

    private void OnEnable()
    {
        tr = GetComponent<Transform>();
        tr.localScale = Vector2.zero;
        trX = tr.localScale.x;
        trY = tr.localScale.y;

        StartCoroutine(BoomStart());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyBullet"))
            DictionaryPool.Inst.Set(collision.gameObject);
    }

    IEnumerator BoomStart()
    {
        while (trX <= 60 && trY <= 60)
        {
            trX += 0.5f;
            trY += 0.5f;
            yield return null;
            tr.localScale = new Vector2(trX, trY);
        }
        DictionaryPool.Inst.Set(this.gameObject);

    }
}

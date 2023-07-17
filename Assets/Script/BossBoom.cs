using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBoom : MonoBehaviour
{
    [SerializeField] SpriteRenderer sp = null;
    float alpha = 0f;

    [SerializeField] CircleCollider2D col = null;
    [SerializeField] Transform tr = null;
    [SerializeField] float trX = 0f;
    [SerializeField] float trY = 0f;

    [SerializeField] PalyerController player = null;

    private void OnEnable()
    {
        sp = GetComponent<SpriteRenderer>();
        col = GetComponent<CircleCollider2D>();
        tr = GetComponent<Transform>();
        player = FindObjectOfType<PalyerController>();
        trX = tr.localScale.x;
        trY = tr.localScale.y;
        alpha = 1;

        StartCoroutine(AttackBoom());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.CompareTag("Player") && !GameManager.Inst.playerShild)
        {
        Debug.Log("¥Í¿Ω");
            int playerhp = GameManager.Inst.PlayerHPPool();
            Debug.Log(playerhp);
            for (int i = 0; i < playerhp; i++)
            {
                collision.SendMessage("BossHit", 1, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    IEnumerator AttackBoom()
    {
        while (trX <= 20 && trY <= 20)
        {
            trX += 0.4f;
            trY += 0.4f;
            col.radius += 0.4f;
            yield return null;
            tr.localScale = new Vector2(trX, trY);
        }
        while (sp.color.a >= 0)
        {
            sp.color = new Color(1, 1, 1, alpha);
            yield return null;
            alpha -= 0.03f;
        }
        DictionaryPool.Inst.Set(this.gameObject);
    }
}

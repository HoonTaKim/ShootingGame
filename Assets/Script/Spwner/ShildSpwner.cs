using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildSpwner : MonoBehaviour
{
    int shildHP = 0;
    float moveSpeed = 0f;
    [SerializeField] PalyerController player = null;
    SpriteRenderer sp = null;
    Color col;

    private void OnEnable()
    {
        player = FindObjectOfType<PalyerController>();
        sp = GetComponent<SpriteRenderer>();
        col = sp.color;

        Init();
    }

    void Init()
    {
        shildHP = 30;
        moveSpeed = 2f;
        StartCoroutine(Move());
    }

    void Hit()
    {
        shildHP--;
        StartCoroutine(HitEff());
        if (shildHP <= 0)
        {
            player.SendMessage("OnShild", SendMessageOptions.DontRequireReceiver);
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Hit();
            DictionaryPool.Inst.Set(collision.gameObject);
        }
    }

    IEnumerator Move()
    {
        Vector2 pos = new Vector2(0, 0);
        while (this.transform.position.y >= 0)
        {
            yield return null;
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime); 
        }
        yield break;
    }

    IEnumerator HitEff()
    {
        sp.color = Color.gray;
        yield return new WaitForSeconds(0.05f);
        sp.color = col;
    }
}

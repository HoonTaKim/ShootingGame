using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int eHP = 0;

    [SerializeField] protected float moveSpeed = 0f;
    [SerializeField] protected float bulletFireTime = 0f;

    [SerializeField] protected GameObject bulletPrefab = null;
    [SerializeField] protected SpriteRenderer spriteRenderer = null;

    protected Color objColor;

    protected bool notAttack = false;

    protected float timeSave = 0;

    protected void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        objColor = spriteRenderer.color;

        StartCoroutine(Spwn());
    }

    protected void Update()
    {
        Move();
        AttackTime(); 
    }

    protected IEnumerator Spwn()
    {
        notAttack = true;
        yield return new WaitForSeconds(0.5f);
        notAttack = false;
    }

    // 벽 충돌시 파괴
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
            DictionaryPool.Inst.Set(this.gameObject);
    }

    protected virtual void Attack() { }

    protected void AttackTime()
    {
        timeSave += Time.deltaTime;
        if (timeSave > bulletFireTime)
        {
            Attack();
            timeSave = 0;
        }
    }

    protected void Move()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    protected void Hit(int damage)
    {
        if (!notAttack)
        {
            eHP -= damage;
            StartCoroutine(HitEff());
            if (eHP <= 0) Die(); 
        }
    }

    protected void Die()
    {
        ItemSpwner.Inst.ItemSpwn(this.transform.position);
        GameManager.Inst.Stage(1);
        spriteRenderer.color = objColor;
        DictionaryPool.Inst.Set(this.gameObject);
    }

    protected IEnumerator HitEff()
    {
        spriteRenderer.color = Color.gray;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = objColor;
    }
}

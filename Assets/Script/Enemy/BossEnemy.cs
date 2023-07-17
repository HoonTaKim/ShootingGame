using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    [SerializeField] int bossHP = 0;
    [SerializeField] float moveSpeed = 0f;
    [SerializeField] int idx = 0;
    [SerializeField] bool attack2 = false;
    [SerializeField] bool bossIn = false;
    [SerializeField] bool bossBoom = false;
    [SerializeField] bool bossDie = false;

    [SerializeField] SpriteRenderer spriteRenderer = null;
    [SerializeField] Color objColor;

    [SerializeField] Transform subWaponR = null;
    [SerializeField] Transform subWaponL = null;

    [SerializeField] PalyerController target = null;
    [SerializeField] GameObject bulletRotation = null;
    [SerializeField] GameObject attack1BulletPrefab = null;
    [SerializeField] GameObject[] attack2BulletPrefab = null;
    [SerializeField] GameObject attack3BulletPrefab = null;
    [SerializeField] GameObject attack4BulletPrefab = null;
    [SerializeField] GameObject attackFBulletPrefab = null;
    [SerializeField] GameObject shildSpwner = null;

    // Start is called before the first frame update
    void OnEnable()
    {
        Init();
    }


    void Init()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        subWaponL = this.gameObject.transform.GetChild(1);
        subWaponR = this.gameObject.transform.GetChild(2);
        objColor = spriteRenderer.color;
        bossHP = 300;
        moveSpeed = 1.7f;
        target = FindObjectOfType<PalyerController>();
        StartCoroutine(Move());
    }

    void Hit(int damage)
    {
        if (bossIn)
        {
            bossHP -= damage;
            StartCoroutine(HitEff());
            UIManager.Inst.BossDamage(damage);
            if (bossHP <= 0)
            {
                DictionaryPool.Inst.Set(this.gameObject);
                GameManager.Inst.BossDie();
            } 
        }
    }
    IEnumerator HitEff()
    {
        spriteRenderer.color = Color.gray;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = objColor;
    }


    IEnumerator Move()
    {
        while (transform.position.y >= 3f)
        {
            yield return new WaitForSeconds(0.003f);
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime); 
        }
        yield return new WaitForSeconds(1f);
        bossIn = true;
        StartCoroutine(Attack1());
    }

    IEnumerator Attack1()
    {
        
        for (int i = 0; i < 5; i++)
        {
            if (bossHP >= 150 || bossBoom)
            {
                DictionaryPool.Inst.Get(attack1BulletPrefab, this.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1.7f);
            }
            else
            {
                StartCoroutine(AttackF());
                yield break;
            }
        }
        StartCoroutine(Attack2());
    }

    IEnumerator Attack2()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 360; j += 12)
            {
                if (idx == 0) idx = 1;
                else idx = 0;

                if (bossHP >= 150 || bossBoom)
                {
                    if (attack2)
                    {
                        bulletRotation.transform.rotation = Quaternion.Euler(new Vector3(0, 0, j));
                        DictionaryPool.Inst.Get(attack2BulletPrefab[idx], this.transform.position, bulletRotation.transform.rotation);
                    }
                    else
                    {
                        bulletRotation.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 6 + j));
                        DictionaryPool.Inst.Get(attack2BulletPrefab[idx], this.transform.position, bulletRotation.transform.rotation);
                    } 
                }
                else
                {
                    StartCoroutine(AttackF());
                    yield break;
                }
            }
            if (attack2) attack2 = false;
            else attack2 = true;

            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(Attack3());
    }

    IEnumerator Attack3()
    {
        for (int i = 0; i < 10; i++)
        {
            if (bossHP >= 150 || bossBoom)
            {
                yield return new WaitForSeconds(0.5f);
                DictionaryPool.Inst.Get(attack3BulletPrefab, target.transform.position, Quaternion.identity); 
            }
            else
            {
                StartCoroutine(AttackF());
                yield break;
            }
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(Attack1());
    }

    IEnumerator AttackF()
    {
        bossIn = false;
        StartCoroutine(OnSubAttack());
        StartCoroutine(AttackFEff());
        DictionaryPool.Inst.Get(shildSpwner, new Vector2(Random.Range(-2.4f, 2.4f), 5.5f), Quaternion.identity);
        yield return new WaitForSeconds(9f);
        DictionaryPool.Inst.Get(attackFBulletPrefab, this.transform.position, Quaternion.identity);
        if (!GameManager.Inst.playerShild)
        {
            DictionaryPool.Inst.Set(shildSpwner);
        }
        yield return new WaitForSeconds(2f);
        target.SendMessage("OffShild", SendMessageOptions.DontRequireReceiver);
        bossIn = true;
        bossBoom = true;
        StartCoroutine(Attack1());
    }
    IEnumerator AttackFEff()
    {
        float waitTime = 0f;
        while (waitTime <= 8f)
        {
            spriteRenderer.color = Color.blue;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = objColor;
            yield return new WaitForSeconds(0.1f);
            waitTime += Time.deltaTime;
        }
        spriteRenderer.color = objColor;
        yield break;
    }

    IEnumerator OnSubAttack()
    {
        while (!bossDie)
        {
            DictionaryPool.Inst.Get(attack4BulletPrefab, subWaponL.position, Quaternion.identity);
            DictionaryPool.Inst.Get(attack4BulletPrefab, subWaponR.position, Quaternion.identity);
            yield return new WaitForSeconds(3f);
        }
    }
}

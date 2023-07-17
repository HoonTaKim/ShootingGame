using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    [SerializeField] GameObject bulletRotate = null;

    private void OnEnable()
    {
        Init();
        StartCoroutine(Spwn());
    }

    public void Init()
    {
        eHP = 10;
        moveSpeed = 2f;
        spriteRenderer.color = objColor;
        bulletFireTime = 2f;
    }

    protected override void Attack()
    {
        for (int i = 0; i <= 360; i += 45)
        {
            bulletRotate.transform.rotation = Quaternion.Euler(new Vector3(0, 0, i));
            DictionaryPool.Inst.Get(bulletPrefab, this.transform.position, bulletRotate.transform.rotation);
        }
    }
}

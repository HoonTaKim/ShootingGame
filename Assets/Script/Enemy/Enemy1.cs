using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        Init();
        StartCoroutine(Spwn());
    }

    private void Init()
    {
        eHP = 5;
        moveSpeed = 2f;
        bulletFireTime = 1.7f;
    }

    protected override void Attack()
    {
        DictionaryPool.Inst.Get(bulletPrefab, this.transform.position, Quaternion.identity);
    }
}

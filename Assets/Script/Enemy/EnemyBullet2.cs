using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet2 : BaseEnemyBullet
{
    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        moveSpeed = 4.5f;
        bPower = 1;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
}
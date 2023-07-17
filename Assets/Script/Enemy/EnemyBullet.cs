using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BaseEnemyBullet
{ 
    [SerializeField] Vector2 moveDir;
    [SerializeField] PalyerController target = null;
    
    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        target = FindObjectOfType<PalyerController>();
        moveSpeed = 4.5f;
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
}
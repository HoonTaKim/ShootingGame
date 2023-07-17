using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0f;

    // Start is called before the first frame update
    void OnEnable()
    {
        Init();
    }

    void Init()
    {
        moveSpeed = 2.3f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            DictionaryPool.Inst.Set(this.gameObject);
        }
    }
}

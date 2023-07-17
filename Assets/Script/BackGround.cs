using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    float posY = 0f;

    private void OnEnable()
    {
        StartCoroutine(Move());   
    }

    IEnumerator Move()
    {
        posY = this.transform.position.y;

        while (this.transform.position.y >= -13)
        {
            posY -= 0.15f;
            this.transform.position = new Vector2(0, posY);
            yield return new WaitForSeconds(0.01f);
        }
        DictionaryPool.Inst.Set(this.gameObject);
    }
}

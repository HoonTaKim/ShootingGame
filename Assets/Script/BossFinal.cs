using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFinal : MonoBehaviour
{
    [SerializeField] Transform mainTr = null;
    [SerializeField] float mainTrX = 0f;
    [SerializeField] float mainTrY = 0f;

    [SerializeField] GameObject boom = null;

    private void OnEnable()
    {
        mainTr = GetComponent<Transform>();
        mainTr.localScale = new Vector2(15f, 15f);
        mainTrX = mainTr.localScale.x;
        mainTrY = mainTr.localScale.y;

        StartCoroutine(AttackEffStart());
    }

    IEnumerator AttackEffStart()
    {
        while (mainTrX >= 0 && mainTrY >= 0)
        {
            mainTrX -= 0.3f;
            mainTrY -= 0.3f;
            yield return null;
            mainTr.localScale = new Vector2(mainTrX, mainTrY);
        }
        DictionaryPool.Inst.Get(boom, this.transform.position, Quaternion.identity);
        DictionaryPool.Inst.Set(this.gameObject);
    }
}

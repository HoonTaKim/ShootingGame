using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubWeponSpwner : MonoBehaviour
{
    [SerializeField] GameObject bPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BulletSpwn());
    }

    IEnumerator BulletSpwn()
    {
        while (true)
        {
            DictionaryPool.Inst.Get(bPrefab, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f); 
        }
    }
}

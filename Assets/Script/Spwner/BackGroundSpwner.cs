using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSpwner : MonoBehaviour
{
    [SerializeField] GameObject backPrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BackGroundSpwn());
    }

    IEnumerator BackGroundSpwn()
    {
        while (true)
        {
            DictionaryPool.Inst.Get(backPrefab, new Vector2(0, 12), Quaternion.identity);
            yield return new WaitForSeconds(1f); 
        }
    }
}

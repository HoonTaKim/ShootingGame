using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpwner : MonoBehaviour
{
    #region SingleTone
    static ItemSpwner inst;
    public static ItemSpwner Inst
    {
        get
        {
            if (inst == null)
            {
                inst = FindObjectOfType<ItemSpwner>();

                if (inst == null)
                {
                    inst = new GameObject("ItemSpwner").AddComponent<ItemSpwner>();
                }
            }
            return inst;
        }
    }
    #endregion


    [SerializeField] GameObject[] itemPrefab = new GameObject[3];
    [SerializeField] bool itemOn = false;

    public void ItemSpwn(Vector2 pos)
    {
        // 0 = Ã¼·Â, 1 = ºÒ¸´, 2 = ÆøÅº
        if (Random.Range(0, 6) == 3)
        {
            int num = Random.Range(0, 3);
            DictionaryPool.Inst.Get(itemPrefab[num], pos, Quaternion.identity);
        }
        else return;
    }
}

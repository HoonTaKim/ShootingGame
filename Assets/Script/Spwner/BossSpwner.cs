using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpwner : MonoBehaviour
{
    #region SingleTone
    static BossSpwner inst;
    public static BossSpwner Inst
    {
        get
        {
            if (inst == null)
            {
                inst = FindObjectOfType<BossSpwner>();

                if (inst == null)
                {
                    inst = new GameObject("BossSpwner").AddComponent<BossSpwner>();
                }
            }
            return inst;
        }
    }
    #endregion

    [SerializeField] GameObject bossPrefab = null;

    public void BossSpwn()
    {
        DictionaryPool.Inst.Get(bossPrefab, new Vector2(0, 5), Quaternion.identity);
    }

}

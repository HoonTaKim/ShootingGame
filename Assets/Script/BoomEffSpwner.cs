using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEffSpwner : MonoBehaviour
{
    #region SingleTone
    static BoomEffSpwner inst;
    public static BoomEffSpwner Inst
    {
        get
        {
            if (inst == null)
            {
                inst = FindObjectOfType<BoomEffSpwner>();

                if (inst == null)
                {
                    inst = new GameObject("BoomEffSpwner").AddComponent<BoomEffSpwner>();
                }
            }
            return inst;
        }
    }
    #endregion

    [SerializeField] GameObject effPrefab = null;

    public void OnBoomEffect(Vector2 pos)
    {
        DictionaryPool.Inst.Get(effPrefab, pos, Quaternion.identity);
    }
}

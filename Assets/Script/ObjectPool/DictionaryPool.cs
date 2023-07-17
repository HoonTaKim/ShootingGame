using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryPool : MonoBehaviour
{
    #region SingleTone
    static DictionaryPool inst;
    public static DictionaryPool Inst
    {
        get
        {
            if (inst == null)
            {
                inst = FindObjectOfType<DictionaryPool>();

                if (inst == null)
                {
                    inst = new GameObject("DictionaryPool").AddComponent<DictionaryPool>();
                }
            }
            return inst;
        }
    }
    #endregion

    [SerializeField] Dictionary<string, Queue<GameObject>> dic = new Dictionary<string, Queue<GameObject>>();

    public GameObject Get(GameObject keyPrefab, Vector2 pos, Quaternion rotation)
    {
        Queue<GameObject> list = null;
        GameObject bullet = null;
        bool dicChack = dic.TryGetValue(keyPrefab.name, out list);

        if (!dicChack)
        {
            // 존재하지 않는다면 생성 
            list = new Queue<GameObject>();
            dic.Add(keyPrefab.name, list);
        }
        if (list.Count == 0)
        {
            bullet = Instantiate(keyPrefab, pos, rotation);
        }
        else if(list.Count > 0)
        {
            bullet = list.Dequeue();
            bullet.transform.position = pos;
            bullet.transform.rotation = rotation;
            bullet.SetActive(true);
        }

        if(bullet!=null)
            return bullet;
        else
            return null;

    }

    public void Set(GameObject bullet)
    {
        Queue<GameObject> list = null;
        string bulletName = bullet.name.Replace("(Clone)","");

        bool dicdic = dic.TryGetValue(bulletName, out list);

        if(dicdic == false)
        {
            Debug.Log("리스트 없음");
            return;
        }
        bullet.SetActive(false);
        list.Enqueue(bullet);
        
    }
}

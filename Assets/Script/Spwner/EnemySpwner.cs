using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwner : MonoBehaviour
{
    #region SingleTone
    static EnemySpwner inst;
    public static EnemySpwner Inst
    {
        get
        {
            if (inst == null)
            {
                inst = FindObjectOfType<EnemySpwner>();

                if (inst == null)
                {
                    inst = new GameObject("EnemySpwner").AddComponent<EnemySpwner>();
                }
            }
            return inst;
        }
    }
    #endregion

    // 일반몬스터 스폰시간
    [SerializeField] float spwnTime1 = 0;
    // 일반몬스터 수
    [SerializeField] int enemySelect = 0;
    // 이외 스폰시간
    [SerializeField] GameObject[] enemy;

    //[SerializeField] int killCount = 0;
    [SerializeField] public bool nextStage { get; set; }
    [SerializeField] public bool bossStage { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Init();
        StartCoroutine(EnemySpwn1());
    }

    void Init()
    {
        spwnTime1 = 1f;
    }

    IEnumerator EnemySpwn1()
    {
        while(!bossStage)
        {
            if (nextStage)
                enemySelect = Random.Range(0, 2);
            else enemySelect = 0;

            DictionaryPool.Inst.Get(enemy[enemySelect], new Vector2(Random.Range(-2.4f, 2.4f), 5.5f), Quaternion.identity);
            yield return new WaitForSeconds(spwnTime1);  
        }
    }
}

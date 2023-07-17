using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region SingleTone
    static UIManager inst;
    public static UIManager Inst
    {
        get
        {
            if (inst == null)
            {
                inst = FindObjectOfType<UIManager>();

                if (inst == null)
                {
                    inst = new GameObject("UIManager").AddComponent<UIManager>();
                }
            }
            return inst;
        }
    }
    #endregion

    [SerializeField] Image uiSeting = null;

    [SerializeField] TextMeshProUGUI title = null;
    [SerializeField] Button startButton = null;

    [SerializeField] TextMeshProUGUI gameOverText = null;
    [SerializeField] TextMeshProUGUI gameClearText = null;

    [SerializeField] Image[] playerHPI = null;
    [SerializeField] TextMeshProUGUI stageView = null;
    [SerializeField] TextMeshProUGUI boomView = null;
    [SerializeField] Slider bossSlider = null;
    [SerializeField] int stageNum = 1;
    public int CurHp = 3;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        stageView.text = stageView.text + stageNum;
        PlayerBoomView(0);
    }

    public void OnStartButton()
    {
        title.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        uiSeting.gameObject.SetActive(true);
        GameManager.Inst.GameStart();
    }

    public void PlayerHPView(int hp, bool hpSet)
    {
        playerHPI[hp].gameObject.SetActive(hpSet);
    }

    public void PlayerBoomView(int boomNum)
    {
        boomView.text = " ";
        boomView.text = "x " + boomNum;
    }

    public void StageView(int count)
    {
        stageNum += count;
        stageView.text = " ";
        stageView.text = "Stage " + stageNum;
    }

    public void BossStageView()
    {
        stageView.text = " ";
        stageView.text = "BossStage";
    }

    public void BossHpSlider(int hp)
    {
        bossSlider.gameObject.SetActive(true);
        bossSlider.maxValue = hp;
        StartCoroutine(BossHpEff(hp));
    }

    IEnumerator BossHpEff(int hp)
    {
        while (bossSlider.value != hp)
        {
            yield return null;
            bossSlider.value += 5;
        }
    }

    public void BossDamage(int damage)
    {
        bossSlider.value -= damage;
        if (bossSlider.value <= 0)
        {
            bossSlider.gameObject.SetActive(false);
        }
    }

    public void OnGameOverText()
    {
        gameOverText.gameObject.SetActive(true);
    }

    public void OnGameClearText()
    {
        gameClearText.gameObject.SetActive(true);
    }
}

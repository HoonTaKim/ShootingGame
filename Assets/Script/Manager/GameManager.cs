using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SingleTone
    static GameManager inst;
    public static GameManager Inst
    {
        get
        {
            if (inst == null)
            {
                inst = FindObjectOfType<GameManager>();

                if (inst == null)
                {
                    inst = new GameObject("GameManagerObj").AddComponent<GameManager>();
                }
            }
            return inst;
        }
    }
    #endregion

    [SerializeField] int stageCount = 0;
    [SerializeField] int playerHPSave = 0;
    [SerializeField] int playerBoomSave = 0;
    [SerializeField] int playerBulletSave = 0;

    [SerializeField] GameObject spwners = null;
    [SerializeField] public bool playerShild { get; set; }

    [SerializeField] public int killCount { get; set; }
    [SerializeField] bool bossIn = true;
    [SerializeField] int bossCount = 0;

    [SerializeField] UIManager ui = null;

    [SerializeField] public bool gameStart { get; set; }
    [SerializeField] public bool gameClear { get; set; }
    [SerializeField] public bool gameOver { get; set; }

    private void Awake()
    {
        SetScreen();
        ui = FindObjectOfType<UIManager>();
        gameClear = false;
        gameOver = false;
        playerShild = false;
        playerHPSave = 3;
    }

    void SetScreen()
    {
        int width = 1080;
        int height = 1920;

        //Screen.SetResolution(1080, 1920, FullScreenMode.Windowed);
        Screen.SetResolution(width, height, true);
    }

    public void GameStart()
    {
        spwners.gameObject.SetActive(true);
    }
    public void Stage(int count)
    {
        killCount += count;
        if (bossCount >= 40)
        {
            EnemySpwner.Inst.bossStage = true;
            UIManager.Inst.BossStageView();
            BossAppearance();
        }
        if (killCount >= 20 && !EnemySpwner.Inst.bossStage)
        {
            ui.StageView(1);
            bossCount += killCount;
            killCount = 0;
            if (!EnemySpwner.Inst.nextStage) 
                EnemySpwner.Inst.nextStage = true;
        }
    }

    public void PlayerHPSave(int pHP, bool hpSet)
    {
        playerHPSave = pHP;
        ui.PlayerHPView(playerHPSave, hpSet);
        if (playerHPSave <= 0)
        {
            gameOver = true;
            GameOver();
        }
    }

    public int PlayerHPPool()
    {
        return playerHPSave;
    }

    public void PlayerBoomSave(int boom)
    {
        playerBoomSave = boom;
        ui.PlayerBoomView(boom);
    }

    public void PlayerBulletSave(int bullet)
    {
        playerBulletSave = bullet;
    }

    public int PlayerBulletPool()
    {
        return playerBulletSave;
    }

    public void BossAppearance()
    {
        if (bossIn)
        {
            BossSpwner.Inst.BossSpwn();
            UIManager.Inst.BossHpSlider(300);
            bossIn = false;
        }
    }

    public void BossDie()
    {
        gameClear = true;
        GameClear();
    }

    public void GameOver()
    {
        UIManager.Inst.OnGameOverText();
    }

    public void GameClear()
    {
        UIManager.Inst.OnGameClearText();
    }
}

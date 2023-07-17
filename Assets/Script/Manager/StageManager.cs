using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    #region SingleTone
    static StageManager inst;
    public static StageManager Inst
    {
        get
        {
            if (inst == null)
            {
                inst = FindObjectOfType<StageManager>();

                if (inst == null)
                {
                    inst = new GameObject("StageManager").AddComponent<StageManager>();
                }
            }
            return inst;
        }
    }
    #endregion

    [SerializeField] List<bool> stageMemory = new List<bool>();

    private void Start()
    {
        Create();
    }

    public bool ReturnList()
    {
        return stageMemory[0];
    }

    private void Create()
    {
        bool stage = false;
        stageMemory.Add(stage);
    }

    public void BoolSwitchTrue()
    {
        if (stageMemory.Count > 0)
        {
            stageMemory[0] = true;
        }
        else
        {
            Create();
            stageMemory[0] = true;
        }
    }
    public void BoolSwitchFalse()
    {
        stageMemory[0] = false;
    }

    public void ListDel()
    {
        stageMemory.Remove(stageMemory[0]);
    }
}

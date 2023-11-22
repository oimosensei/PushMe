using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInitializer : MonoBehaviour
{

    [SerializeField] private List<GameObject> _stageList;

    public int StageCount => _stageList.Count;

    public bool isDebug = false;
    public void InitializeStage(int stageNumber)
    {
        if (isDebug) return;
        if (stageNumber < 0 || stageNumber >= _stageList.Count)
        {
            Debug.LogError("Invalid stage number");
            return;
        }

        foreach (var stage in _stageList)
        {
            stage.SetActive(false);
        }
        _stageList[stageNumber].SetActive(true);
    }
}

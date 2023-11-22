using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using Zenject;
using KanKikuchi.AudioManager;
public class GameStateManager : MonoBehaviour
{
    // Start is called before the first frame update
    public ReactiveProperty<GameState> gameState = new ReactiveProperty<GameState>(GameState.Initializing);

    public CountDownTimer readyTimer = new CountDownTimer(3);

    [Inject] private StageInitializer _stageInitializer;

    private const string TitleSceneName = "TitleScene";
    void Start()
    {
        GameProcess().Forget();
        BGMManager.Instance.Play(BGMPath.AUTUMN_RAIN);
    }

    private async UniTaskVoid GameProcess()
    {
        _stageInitializer.InitializeStage(CurrentStageInfo.StageNumber);
        gameState.SetValueAndForceNotify(GameState.Ready);

        Debug.Log("Ready");
        readyTimer.StartTimer().Forget();
        SEManager.Instance.Play(SEPath.COUNT_DOWN);
        await readyTimer.TimerValue.Where(x => x == 0).First().ToUniTask();

        gameState.SetValueAndForceNotify(GameState.Playing);

        await gameState.Where(x => x == GameState.Clear || x == GameState.GameOver).First().ToUniTask();
        BGMManager.Instance.Stop();
        await UniTask.WaitForSeconds(3f);
        if (gameState.Value == GameState.Clear)
        {
            Debug.Log("Stage Cleared!");
            if (++CurrentStageInfo.StageNumber == _stageInitializer.StageCount)
            {
                Initiate.Fade(TitleSceneName, Color.black, 1.0f);
            }
        }
        else
        {
            Debug.Log("Game Over...");
        }

        //現在のシーンの名前
        string currentSceeneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        Initiate.Fade(currentSceeneName, Color.black, 1.0f);
    }
}

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    
    private TitleScreenBlaster titleScreenBlaster;
    public TitleScreenBlaster TitleScreenBlaster
    {
        get
        {
            if (titleScreenBlaster == null)
            {
                titleScreenBlaster = GameObject.Find("TitleBlaster").GetComponent<TitleScreenBlaster>();
            }
            return titleScreenBlaster;
        }
    }
    
    private ScoreHandler endScreenScoreHandler;

    public ScoreHandler EndScreenScoreHandler
    {
        get
        {
            if (endScreenScoreHandler == null)
            {
                endScreenScoreHandler = GameObject.Find("EndScreenScoreHandler").GetComponent<ScoreHandler>();
            }
            return endScreenScoreHandler;
        }
    }
    
    public bool IsInputEnabled { private set; get; }

    public void StartLevel()
    {
        StartCoroutine(StartLevelCoroutine());
    }

    private IEnumerator StartLevelCoroutine()
    {
        IsInputEnabled = false;
        yield return TitleScreenBlaster.PlayTitle();
        IsInputEnabled = true;
    }
    
    public void TriggerLevelWin(float waitTime)
    {
        StartCoroutine(WinLevel(waitTime));
    }

    private IEnumerator WinLevel(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        WinLevel();
        EndScreenScoreHandler.StartScoreCount(4000);
        //StartCoroutine(EndScreenScoreHandler.CountUpRoutine());
    }
    
    public void WinLevel()
    {
        Time.timeScale = 0f;
        //Enable Score Canvas
    }
    
    void Update()
    {
        
    }
}

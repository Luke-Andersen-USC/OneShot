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
    
    private TitleScreenBlaster loseScreenBlaster;
    public TitleScreenBlaster LoseScreenBlaster
    {
        get
        {
            if (loseScreenBlaster == null)
            {
                loseScreenBlaster = GameObject.Find("LoseScreenBlaster").GetComponent<TitleScreenBlaster>();
            }
            return loseScreenBlaster;
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
    
    private AudioSource musicPlayer;
    public AudioSource MusicPlayer
    {
        get
        {
            if (musicPlayer == null)
            {
                musicPlayer = GameObject.Find("MusicPlayer").GetComponent<AudioSource>();
            }
            return musicPlayer;
        }
    }

    public bool IsInputEnabled = false;

    public void StartLevel()
    {
        MusicPlayer.Play();
        StartCoroutine(StartLevelCoroutine());
    }

    private IEnumerator StartLevelCoroutine()
    {
        IsInputEnabled = false;
        yield return TitleScreenBlaster.PlayTitle();
        IsInputEnabled = true;
    }
    
    public void TriggerLevelLoss(float waitTime)
    {
        StartCoroutine(LoseLevel(waitTime));
    }

    private IEnumerator LoseLevel(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        LoseLevel();
    }
    
    public void LoseLevel()
    {
        Time.timeScale = 0f;
        MusicPlayer.Pause();
        StartCoroutine(LoseScreenBlaster.PlayTitle());
    }
    
    public void TriggerLevelWin(float waitTime)
    {
        StartCoroutine(WinLevel(waitTime));
    }

    private IEnumerator WinLevel(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        WinLevel();
    }
    
    public void WinLevel()
    {
        Time.timeScale = 0f;
        MusicPlayer.Pause();
        EndScreenScoreHandler.StartScoreCount(4000);
    }
}

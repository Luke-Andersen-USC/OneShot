using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    
    [Header("UIBlasts")] 
    [SerializeField] private UIBlast Score;
    [SerializeField] private UIBlast ScoreCount;
    [SerializeField] private UIBlast Rank;
    [SerializeField] private UIBlast RankAchieved;
    
    [Header("Timing")]
    [SerializeField] private float blastTime = 0.4f;
    [SerializeField] private float scoreWaitTime = 1f;
    
    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int targetScore;
    [SerializeField] private float countSpeed = 800f;

    [Header("Stars")]
    [SerializeField] private List<int> starThresholds;
    [SerializeField] private List<UIBlast> starBlasts;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip tickSFX;
    [SerializeField] private AudioClip finishTickSFX;
    [SerializeField] private int tickInterval = 100;

    private int currentScore;
    private int lastTickScore;
    private HashSet<int> triggeredStars = new HashSet<int>();

    void Awake()
    {
        currentScore = 0;
        lastTickScore = 0;
        UpdateScoreText();
        
        EnableUIBlasters(false);
    }

    private void EnableUIBlasters(bool enable)
    {
        Score.gameObject.SetActive(enable);
        ScoreCount.gameObject.SetActive(enable);
        Rank.gameObject.SetActive(enable);
        RankAchieved.gameObject.SetActive(enable);
        foreach (UIBlast star in starBlasts)
        {
            star.gameObject.SetActive(enable);
        }
    }

    public void StartScoreCount(int finalScore)
    {
        targetScore = finalScore;
        canvas.enabled = true;
        
        StopAllCoroutines();
        StartCoroutine(CountUpRoutine());
    }

    public IEnumerator CountUpRoutine()
    {
        yield return new WaitForSecondsRealtime(blastTime);
        Score.Play();
        yield return new WaitForSecondsRealtime(blastTime);
        ScoreCount.Play();
        yield return new WaitForSecondsRealtime(blastTime);
        
        float displayedScore = currentScore;

        while (displayedScore < targetScore)
        {
            displayedScore += countSpeed * Time.unscaledDeltaTime;
            currentScore = Mathf.Min(Mathf.FloorToInt(displayedScore), targetScore);

            HandleTickSFX();
            HandleStars();
            UpdateScoreText();

            yield return null;
        }

        currentScore = targetScore;
        UpdateScoreText();
        
        audioSource.PlayOneShot(finishTickSFX);
        
        yield return new WaitForSecondsRealtime(scoreWaitTime);
        
        Rank.Play();
        yield return new WaitForSecondsRealtime(blastTime);
        RankAchieved.Play();
    }

    private void HandleTickSFX()
    {
        if (currentScore / tickInterval > lastTickScore / tickInterval)
        {
            lastTickScore = currentScore;
            audioSource.PlayOneShot(tickSFX);
        }
    }

    private void HandleStars()
    {
        for (int i = 0; i < starThresholds.Count; i++)
        {
            if (currentScore >= starThresholds[i] && !triggeredStars.Contains(i))
            {
                triggeredStars.Add(i);
                starBlasts[i].Play();
            }
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = currentScore.ToString();
    }
}

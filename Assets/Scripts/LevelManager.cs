using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
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
        //Enable Score Canvas
    }
    
    void Update()
    {
        
    }
}

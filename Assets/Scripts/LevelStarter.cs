using UnityEngine;

public class LevelStarter : MonoBehaviour
{
    void Start()
    {
        LevelManager.Instance.StartLevel();
    }
}

using UnityEngine;

public class GameStarter : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.Player != null)
            SetupGame();
    }

    private static void SetupGame()
    {
        GameManager.Setup();
    }
}

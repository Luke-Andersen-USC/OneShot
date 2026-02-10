using System.Collections;
using TMPro;
using UnityEngine;

public class TitleScreenBlaster : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Canvas _canvas;
    [SerializeField] protected UIBlast one;
    [SerializeField] protected UIBlast two;
    [Header("Timing")]
    [SerializeField] protected float pauseOne = 0.45f;
    [SerializeField] protected float pauseTwo = 0.45f;
    [SerializeField] protected float pauseTitle = 0.7f;

    public IEnumerator PlayTitle()
    {
        one.gameObject.SetActive(false);
        two.gameObject.SetActive(false);
        _canvas.enabled = true;
        
        yield return new WaitForSecondsRealtime(pauseOne);
        one.Play();
        yield return new WaitForSecondsRealtime(pauseTwo);
        two.Play();
        yield return new WaitForSecondsRealtime(pauseTitle);
        _canvas.enabled = false;
    }
}

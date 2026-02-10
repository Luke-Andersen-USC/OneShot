using System.Collections;
using TMPro;
using UnityEngine;

public class TitleScreenBlaster : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private UIBlast one;
    [SerializeField] private UIBlast two;
    [Header("Timing")]
    [SerializeField] private float pauseOne = 0.45f;
    [SerializeField] private float pauseTwo = 0.45f;
    [SerializeField] private float pauseTitle = 0.7f;
    void Start()
    {
        StartCoroutine(PlayTitle());
    }

    public IEnumerator PlayTitle()
    {
        one.gameObject.SetActive(false);
        two.gameObject.SetActive(false);
        _canvas.enabled = true;
        
        yield return new WaitForSeconds(pauseOne);
        one.Play();
        yield return new WaitForSeconds(pauseTwo);
        two.Play();
        yield return new WaitForSeconds(pauseTitle);
        _canvas.enabled = false;
    }
}

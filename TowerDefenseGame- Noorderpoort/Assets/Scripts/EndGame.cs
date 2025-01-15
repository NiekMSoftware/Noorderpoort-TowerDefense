using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private UnityEvent onEnd;
    [SerializeField] private Camera mainCam;
    private void Start()
    {
        if (blackScreen != null)
        {
            blackScreen.SetActive(false);
        }
    }
    public void BlueScreen()
    {
        StartCoroutine(Waiter(1.5f));
    }
    IEnumerator Waiter(float time)
    {
        waveText.text = FindObjectOfType<WaveSystem>().wavesEnded.ToString() + " waves completed";
        float prevSpeed = Time.timeScale;
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.2f);
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(time);
        DeleteTurrets.instance.FullReset();
        blackScreen.SetActive(false);
        mainCam.gameObject.SetActive(true);
        mainCam.GetComponent<CameraMovement>().ResetToStartPos();
        Healthscript.instance.ended = false;
        BuildingManager.instance.CancelPlace();

        onEnd.Invoke();
        Time.timeScale = prevSpeed;
    }
}

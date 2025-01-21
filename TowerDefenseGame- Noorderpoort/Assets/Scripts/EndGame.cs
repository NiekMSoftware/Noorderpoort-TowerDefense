using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class EndGame : MonoBehaviour
{
    [Header("BSOD UI")]
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private TMP_Text waveText;

    [Header("Resetting")]
    [SerializeField] private UnityEvent onEnd;
    [SerializeField] private Camera mainCam;
    private void Start()
    {
        //Turn the black screen off at the start so the players can see
        if (blackScreen != null){
            blackScreen.SetActive(false);
        }
    }

    /// <summary>
    /// "Bluescreen" the player and reset anything going on
    /// </summary>
    public void BlueScreen()
    {
        //This is so the function can be called more easly
        StartCoroutine(BSOD(1.5f));
    }
    IEnumerator BSOD(float time)
    {
        //Set the text in the BSOD to the amount of waves you completed
        waveText.text = FindObjectOfType<WaveSystem>().wavesEnded.ToString() + " waves completed";

        //Set time to 1 for the duration of the black screen
        float prevSpeed = Time.timeScale;
        Time.timeScale = 1;

        //Wait 0.2 seconds to turn on the black screen, so the player can realize they died
        yield return new WaitForSeconds(0.2f);
        blackScreen.SetActive(true);

        //Wait even longer until the actual BSOD, to simulate a crash
        yield return new WaitForSeconds(time);

        DeleteTurrets.instance.FullReset();

        blackScreen.SetActive(false);
        mainCam.GetComponent<CameraMovement>().ResetToStartPos();

        //Puts the player back to "alive" and stops any building the player was doing
        Healthscript.instance.ended = false;
        BuildingManager.instance.CancelPlace();

        onEnd.Invoke();

        //Set time back to original speed
        Time.timeScale = prevSpeed;
    }
}

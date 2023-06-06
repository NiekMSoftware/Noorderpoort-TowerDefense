using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private TMP_Text waveText;
    private void Start()
    {
        if (blackScreen != null)
        {
            blackScreen.SetActive(false);
        }
        if (waveText != null)
        {
            waveText.text = FindObjectOfType<SaveData>().GetInt("waves").ToString() + " waves complete";
        }
    }
    public void BlueScreen()
    {
        StartCoroutine(Waiter(1.5f));
    }
    IEnumerator Waiter(float time)
    {
        yield return new WaitForSeconds(0.2f);
        blackScreen.SetActive(true);
        //play sound
        yield return new WaitForSeconds(time);
        FindObjectOfType<SceneLoader>().LoadScene("GameOver");
    }
}

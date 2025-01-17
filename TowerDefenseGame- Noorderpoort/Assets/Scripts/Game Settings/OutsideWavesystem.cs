using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OutsideWavesystem : MonoBehaviour
{
    public bool skipWaveTime = false;
    public WaveSystem waveSystem;
    public GameObject waitingUI;
    public TMP_Text time;

    public void SwapBool(bool value)
    {
        skipWaveTime = value;
    }

    public void SkipWait()
    {
        waveSystem.SkipTime();
    }
}

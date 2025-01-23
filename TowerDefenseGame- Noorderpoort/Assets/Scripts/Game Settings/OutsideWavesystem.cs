using TMPro;
using UnityEngine;

public class OutsideWavesystem : MonoBehaviour
{
    [Header("The wave system grabs this")]
    public bool skipWaveTime = false;

    [Header("References")]
    public WaveSystem waveSystem;
    public GameObject waitingUI;
    public TMP_Text time;

    /// <summary>
    /// Swaps the setting for skipping the timer between waves
    /// </summary>
    /// <param name="value"></param>
    public void SwapBool(bool value)
    {
        skipWaveTime = value;
    }

    /// <summary>
    /// skips timer between waves, for 1 wave
    /// </summary>
    public void SkipWait()
    {
        waveSystem.SkipTime();
    }
}

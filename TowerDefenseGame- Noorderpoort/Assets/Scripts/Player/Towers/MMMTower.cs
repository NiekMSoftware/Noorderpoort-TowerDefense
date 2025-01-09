using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMMTower : GeneralTowerScript
{
    [Header("Money Per Upgrade")]
    [SerializeField] private int waveMoney;

    [Header("Sound")]
    [SerializeField] private AudioSource audiosource;
    [SerializeField] private AudioClip printerNoise;

    [Header("Other")]
    Bitscript bitscript;
    WaveSystem waveSystem;
    int lastRound;
    void Start()
    {
        audiosource.clip = printerNoise;
        bitscript = FindObjectOfType<Bitscript>();
        waveSystem = FindObjectOfType<WaveSystem>();

        lastRound = waveSystem.wavesEnded;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingPlaced == false)
        {
            if (waveSystem.wavesEnded > lastRound)
            {
                bitscript.AddBits(waveMoney);
                audiosource.Play();
                lastRound = waveSystem.wavesEnded;
            }
        }
        else
        {
            lastRound = waveSystem.wavesEnded;
        }
    }
    public override void SetStats(TowerScriptable stats)
    {
        waveMoney = stats.moneyPerWave;
        sellValue = stats.sellValue;
        towerStats = stats;
    }
}

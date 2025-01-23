using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMMTower : GeneralTowerScript
{
    [Header("Money Per Upgrade")]
    [SerializeField] private int waveMoney;

    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip printerNoise;

    [Header("Other")]
    [SerializeField] private Bitscript bitscript;
    [SerializeField] private WaveSystem waveSystem;
    [SerializeField] private GameObject particlePrefab;
    private int lastRound;
    void Start()
    {
        audioSource.clip = printerNoise;

        bitscript = FindObjectOfType<Bitscript>();
        waveSystem = FindObjectOfType<WaveSystem>();

        //Dont give money for placing MMM
        lastRound = waveSystem.wavesEnded;
    }

    void Update()
    {
        //Dont give money if placing tower
        if (isBeingPlaced == false)
        {
            if (waveSystem.wavesEnded > lastRound)
            {
                //Gives ya money
                bitscript.AddBits(waveMoney);
                audioSource.Play();
                Instantiate(particlePrefab,transform.position,transform.rotation,transform);

                //So it doesnt spam money. Kind of a bad printer imagine your printer not printing anything else until your have done a task of some kind.
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
        if(bitscript == null) { bitscript = FindObjectOfType<Bitscript>(); }
        waveMoney = stats.moneyPerWave;
        towerStats = stats;
        bitscript.AddDiscount(stats.discount);
    }
}

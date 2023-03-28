using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMMTower : MonoBehaviour
{
    [SerializeField] private int upgrade1Money;
    [SerializeField] private int upgrade2Money;
    Bitscript bitscript;
    WaveSystem waveSystem;
    bool gotMoneyThisRound;
    int lastRound;
    bool upgrade1 = true;
    bool upgrade2 = false;
    bool upgrade3 = false;
    // Start is called before the first frame update
    void Start()
    {
        bitscript = FindObjectOfType<Bitscript>();
        waveSystem = FindObjectOfType<WaveSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (upgrade2)
            {
                upgrade2 = false;
                upgrade3 = true;
            }
            if (upgrade1)
            {
                upgrade1 = false;
                upgrade2 = true;
            }
        }
        if (waveSystem.wavesEnded > lastRound)
        {
            if (upgrade1)
            {
                bitscript.AddBits(upgrade1Money);
            }
            if (upgrade2)
            {
                bitscript.AddBits(upgrade2Money);
            }
            lastRound = waveSystem.wavesEnded;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMMTower : MonoBehaviour
{
    [SerializeField] private int upgrade1Money;
    [SerializeField] private int upgrade2Money;
    [SerializeField] private int upgrade3Money;

    [SerializeField] private GameObject upgrade1Visual;
    [SerializeField] private GameObject upgrade2Visual;
    [SerializeField] private GameObject upgrade3Visual;
    Bitscript bitscript;
    WaveSystem waveSystem;
    int lastRound;
    //What upgrade it is
    bool upgrade1 = true;
    bool upgrade2 = false;
    bool upgrade3 = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshFilter>().mesh = upgrade1Visual.GetComponent<MeshFilter>().sharedMesh;
        gameObject.GetComponent<MeshRenderer>().materials = upgrade1Visual.GetComponent<MeshRenderer>().sharedMaterials;
        bitscript = FindObjectOfType<Bitscript>();
        waveSystem = FindObjectOfType<WaveSystem>();
        lastRound = waveSystem.wavesEnded;
    }

    // Update is called once per frame
    void Update()
    {
        if (upgrade2)
        {
            gameObject.GetComponent<MeshFilter>().mesh = upgrade2Visual.GetComponent<MeshFilter>().sharedMesh;
            gameObject.GetComponent<MeshRenderer>().materials = upgrade2Visual.GetComponent<MeshRenderer>().sharedMaterials;
        }
        if (upgrade3)
        {
            gameObject.GetComponent<MeshFilter>().mesh = upgrade3Visual.GetComponent<MeshFilter>().sharedMesh;
            gameObject.GetComponent<MeshRenderer>().materials = upgrade3Visual.GetComponent<MeshRenderer>().sharedMaterials;
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
            if (upgrade3)
            {
                bitscript.AddBits(upgrade3Money);
                //Discount
            }
            lastRound = waveSystem.wavesEnded;
        }
    }
}

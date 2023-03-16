using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    public class QualityPreset : MonoBehaviour
    {
        public void Quality(int qualityPreset)
        {
            /*
             * Make a switch for different quality settings
             */
            switch (qualityPreset)
            {
                case 0:
                    QualitySettings.SetQualityLevel(1);
                    Debug.Log("Switched to medium quality settings");
                    break;
                case 1:
                    QualitySettings.SetQualityLevel(0);
                    Debug.Log("Switched to low quality settings");
                    break;
                case 2:
                    QualitySettings.SetQualityLevel(2);
                    Debug.Log("Switched to high quality settings");
                    break;
            }
        }
    }
}


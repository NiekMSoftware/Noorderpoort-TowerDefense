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
                    PlayerPrefs.SetInt("quality", 0);
                    QualitySettings.SetQualityLevel(1);
                    PlayerPrefs.Save();
                    Debug.Log("Switched to medium quality settings");
                    break;
                case 1:
                        PlayerPrefs.SetInt("quality", 1);
                    QualitySettings.SetQualityLevel(0);
                    PlayerPrefs.Save();
                    Debug.Log("Switched to low quality settings");
                    break;
                case 2:
                    PlayerPrefs.SetInt("quality", 2);
                    QualitySettings.SetQualityLevel(2);
                    PlayerPrefs.Save();
                    Debug.Log("Switched to high quality settings");
                    break;
            }
        }
    }
}


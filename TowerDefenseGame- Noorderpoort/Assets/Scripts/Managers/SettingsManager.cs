using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Settings
{
    public class SettingsManager : MonoBehaviour
    {
        public void VSyncCountUpdate(bool vSync)
        {
            // VsyncCount = to our bool called vSync the ? operator just checks if vSync is false or true and converts that to a 1 or a 0
            QualitySettings.vSyncCount = vSync ? 1 : 0;
        }

        public void ResolutionUpdate()
        {
            
        }
    }
}
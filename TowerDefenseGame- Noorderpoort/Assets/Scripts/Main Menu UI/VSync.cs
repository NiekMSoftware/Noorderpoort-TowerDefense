using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Settings
{
    public class VSync : SettingsManager
    {
        private SettingsManager settingsManager;

        private void Start()
        {
            if (!settingsManager)
            {
                settingsManager = FindObjectOfType<SettingsManager>();
            }
        }

        private bool vSync = false;

        public void SwitchVsync()
        {
            //vSync becomes what it isn't so false becomes true and true becomes false
            vSync = !vSync;

            settingsManager.VSyncCountUpdate(vSync);
        }
    }
}
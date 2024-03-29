using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Settings
{
    public class VSync : SettingsManager
    {
        [SerializeField]
        private bool vSync = false;

        public void SwitchVsync()
        {
            //vSync becomes what it isn't so false becomes true and true becomes false
            vSync = !vSync;

            VSyncCountUpdate(vSync);
        }
    }
}
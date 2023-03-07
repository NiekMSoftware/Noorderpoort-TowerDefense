using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class SettingsManager : MonoBehaviour
    {
        private Resolution[] resolutions;

        [SerializeField]
        private Dropdown resolutionDropDown;

        [SerializeField] private bool useDropdown;
        
        private void Start()
        {
            resolutions = Screen.resolutions;
            
            if (useDropdown)
            {
                resolutionDropDown.ClearOptions();
            }

            List<string> options = new List<string>();

            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);
            }
        }

        //protected functions because we don't want them accessed everywhere only by the children of this Class
        protected void VSyncCountUpdate(bool vSync)
        {
            // VsyncCount = to our bool called vSync the ? operator just checks if vSync is false or true and converts that to a 1 or a 0
            QualitySettings.vSyncCount = vSync ? 1 : 0;
        }

        protected void WindowModeUpdate(FullScreenMode screenMode)
        {
            Screen.fullScreenMode = screenMode;
        }

        protected void UpdateResolution(Resolution res)
        {
            
        }
    } 
}
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
            //Gets all available resolutions
            resolutions = Screen.resolutions;
            
            //Clears dropsdown
            if (useDropdown)
            {
                resolutionDropDown.ClearOptions();
            }

            //Adds all options to the dropdown
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
            QualitySettings.vSyncCount = vSync ? 1 : 0;
        }

        protected void WindowModeUpdate(FullScreenMode screenMode)
        {
            Screen.fullScreenMode = screenMode;
        }
    } 
}
using UnityEngine;

namespace Settings
{
    public class ResolutionMenu : SettingsManager
    {
        public void ChangeResolution(int res)
        {
            //self explanatory
            switch (res)
            { 
                case 0:
                    Screen.SetResolution(3840,2160,true);
                break;
                
                case 1:
                Screen.SetResolution(2560,1440,true);
                break;
                
                case 2:
                Screen.SetResolution(1920,1080,true);
                break;
                
                case 3:
                Screen.SetResolution(1280, 720, true);
                break;
            }
        }
    }
}
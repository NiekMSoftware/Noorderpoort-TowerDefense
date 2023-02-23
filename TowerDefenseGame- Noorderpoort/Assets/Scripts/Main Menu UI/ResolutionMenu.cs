using Settings;
using Unity.VisualScripting;
using UnityEngine;

namespace Settings
{
    public class ResolutionMenu : SettingsManager
    {
        public void ChangeResolution(int res)
        {
            switch (res)
            {
            case 0:
                Screen.SetResolution(640,480,true);
                break;
            case 1:
                Screen.SetResolution(1280, 720, true);
                break;
            case 2:
                Screen.SetResolution(1920,1080,true);
                break;
            }
        }
    }
}
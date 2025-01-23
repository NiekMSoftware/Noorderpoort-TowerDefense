using UnityEngine;

namespace Settings
{
    public class WindowMode : SettingsManager
    {
        public void SwitchWindowMode(int windowType)
        {
            //Self explanatory
            switch (windowType)
            {
                case 0:
                    WindowModeUpdate(FullScreenMode.FullScreenWindow);
                    break;
                case 1:
                    WindowModeUpdate(FullScreenMode.ExclusiveFullScreen);
                    break;
                case 2:
                    WindowModeUpdate(FullScreenMode.Windowed);
                    break;
            }
        }
    }
}
using UnityEngine;

namespace Settings
{
    public class StartingMenu : MonoBehaviour
    {
        private void Awake()
        {
            //Set a default quality setting
            QualitySettings.SetQualityLevel(2);
            PlayerPrefs.Save();
            Debug.Log("Set to high");
            
            //wake up the method for the windowed setting
            Window(FullScreenMode.MaximizedWindow);
            Debug.Log("Set to borderless window");
            
            //Set a default resolution on Awake
            
        }

        void Window(FullScreenMode screenMode)
        {
            //Set a default Windowed Setting
            Screen.fullScreenMode = screenMode;
        }
    }
}


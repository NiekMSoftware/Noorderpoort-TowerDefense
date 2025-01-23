using UnityEngine;

namespace Settings
{
    public class QualityPreset : MonoBehaviour
    {
        public void Quality(int qualityPreset)
        {
            /*
             * Switches your quality settings.
             */
            switch (qualityPreset)
            {
                case 0:
                    QualitySettings.SetQualityLevel(0);
                    PlayerPrefs.Save();
                    Debug.Log("Switched to low quality settings");
                    break;
                case 1:
                    QualitySettings.SetQualityLevel(1);
                    PlayerPrefs.Save();
                    Debug.Log("Switched to medium quality settings");
                    break;
                case 2:
                    QualitySettings.SetQualityLevel(2);
                    PlayerPrefs.Save();
                    Debug.Log("Switched to high quality settings");
                    break;
            }
        }
    }
}


using UnityEngine;

namespace Settings
{
    public class VSync : SettingsManager
    {
        [SerializeField]
        private bool vSync = false;

        public void SwitchVsync()
        {
            //vSync switches
            vSync = !vSync;

            VSyncCountUpdate(vSync);
        }
    }
}
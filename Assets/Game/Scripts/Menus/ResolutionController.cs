using UnityEngine;

namespace Assets.Game.Scripts.Menus
{
    public class ResolutionController: MonoBehaviour
    {
        public void SetResolutionHD()
        {
            Screen.SetResolution(1280, 720, true);
        }
        public void SetResolutionFullHD()
        {
            Screen.SetResolution(1920, 1080, true);
        }
    }
}
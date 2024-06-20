using UI.MVC.Bases;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MVC
{
    public class BannerView : BaseView
    {
        
        
        [Header("Button")] 
        [SerializeField] private Button settingsButton;

        public Button SettingsButton => settingsButton;

        public override void SetUp()
        {
            settingsButton.onClick.RemoveAllListeners();
        }
    }
}

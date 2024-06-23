using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UI.MVC.Bases;
using UnityEngine.EventSystems;

namespace UI.MVC
{
    public class SettingsView : BaseView
    {
        [Header("Canvas Group")]
        [SerializeField] private CanvasGroup targetCanvasGroup;

        [Header("Buttons")] 
        [SerializeField] private Button closeWindowButton;
        [SerializeField] private Button closeGameButton;
        
        [Header("Sliders")] 
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;
        [SerializeField] private Slider ambientVolumeSlider;

        [Header("Dropdowns")] 
        [SerializeField] private TMP_Dropdown qualityDropdown;
        
        [Header("Event Triggers")] 
        [SerializeField] private EventTrigger panelEventTrigger;

        [Header("Feedbacks")] 
        [SerializeField] private Animator panelAnimator;

        public Button CloseWindowButton => closeWindowButton;
        public Button CloseGameButton => closeGameButton;
        
        public Slider MasterVolumeSlider => masterVolumeSlider;
        public Slider MusicVolumeSlider => musicVolumeSlider;
        public Slider SfxVolumeSlider => sfxVolumeSlider;
        public Slider AmbientVolumeSlider => ambientVolumeSlider;

        public TMP_Dropdown QualityDropdown => qualityDropdown;

        public EventTrigger PanelEventTrigger => panelEventTrigger;
        public Animator PanelAnimator => panelAnimator;

        public override void SetUp()
        {
            mainCanvasGroup = targetCanvasGroup;
            
            CloseWindowButton.onClick.RemoveAllListeners();
            CloseGameButton.onClick.RemoveAllListeners();
            
            MasterVolumeSlider.onValueChanged.RemoveAllListeners();
            MusicVolumeSlider.onValueChanged.RemoveAllListeners();
            SfxVolumeSlider.onValueChanged.RemoveAllListeners();
            AmbientVolumeSlider.onValueChanged.RemoveAllListeners();

            QualityDropdown.onValueChanged.RemoveAllListeners();

            foreach (var element in PanelEventTrigger.triggers)
            {
                element.callback.RemoveAllListeners();
            }
        }
    }
}

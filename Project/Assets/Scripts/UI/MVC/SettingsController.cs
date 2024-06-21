using System;
using UI.MVC.Bases;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility.GameFlow;

namespace UI.MVC
{
    public class SettingsController : BaseController
    {
        bool visible;

        public SettingsController(BaseView view, out Action keyCallBack) : base(view)
        {
            keyCallBack = OnKeyCasted;
        }

        public override void Initialize()
        {
            base.Initialize();

            EventTrigger.Entry pointerClickEntry = ((SettingsView)MyView).PanelEventTrigger.triggers.Find(a => a.eventID == EventTriggerType.PointerClick);
            
            if(pointerClickEntry != null) pointerClickEntry.callback.AddListener(OnPanelClicked);
            
            ((SettingsView)MyView).CloseButton.onClick.AddListener(() => UIManager.ToggleModel(ModelType.Settings, false));

            ((SettingsView)MyView).MasterVolumeSlider.onValueChanged.AddListener(value => OnChangeVolume("MasterVolume", value));
            ((SettingsView)MyView).MusicVolumeSlider.onValueChanged.AddListener(value => OnChangeVolume("MusicVolume", value));
            ((SettingsView)MyView).SfxVolumeSlider.onValueChanged.AddListener(value => OnChangeVolume("SFXVolume", value));
            ((SettingsView)MyView).AmbientVolumeSlider.onValueChanged.AddListener(value => OnChangeVolume("AmbientVolume", value));

            ((SettingsView)MyView).MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
            ((SettingsView)MyView).MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            ((SettingsView)MyView).SfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
            ((SettingsView)MyView).AmbientVolumeSlider.value = PlayerPrefs.GetFloat("AmbientVolume", 0.5f);

            ((SettingsView)MyView).QualityDropdown.onValueChanged.AddListener(OnQualityChanged);
        }

        public override void Show()
        {
            base.Show();

            visible = true;

            ((SettingsView)MyView).PanelAnimator.SetTrigger("Show");
            UIManager.ToggleCanvas(MyView.MainCanvasGroup, true);

            GameManager.Pause?.Invoke(true);
        }
        
        public override void Hide()
        {
            base.Hide();

            visible = false;

            UIManager.ToggleCanvas(MyView.MainCanvasGroup, false);
            ((SettingsView)MyView).PanelAnimator.SetTrigger("Hide");

            GameManager.Pause?.Invoke(false);
        }

        private void OnPanelClicked(BaseEventData eventData)
        {
            UIManager.ToggleModel(ModelType.Settings, false);
        }

        private void OnQualityChanged(int index)
        {
            QualitySettings.SetQualityLevel(index);
        }

        private void OnChangeVolume(string volumeChannel, float value)
        {
            AudioManager.ChangeVolume(volumeChannel, value);
            PlayerPrefs.SetFloat(volumeChannel, value);
        }

        private void OnKeyCasted()
        {
            if (visible) Hide();
            else Show();

            AudioManager.PlayClipOneShot(AudioManager.GetClipData("ButtonClick"));
        }
    }
}

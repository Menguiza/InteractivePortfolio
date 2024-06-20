using UI.MVC.Bases;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.MVC
{
    public class SettingsController : BaseController
    {
        public SettingsController(BaseView view) : base(view)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            EventTrigger.Entry pointerClickEntry = ((SettingsView)MyView).PanelEventTrigger.triggers.Find(a => a.eventID == EventTriggerType.PointerClick);
            
            if(pointerClickEntry != null) pointerClickEntry.callback.AddListener(OnPanelClicked);
            
            ((SettingsView)MyView).CloseButton.onClick.AddListener(() => UIManager.ToggleModel(ModelType.Settings, false));
            
            ((SettingsView)MyView).QualityDropdown.onValueChanged.AddListener(OnQualityChanged);
        }

        public override void Show()
        {
            base.Show();

            ((SettingsView)MyView).PanelAnimator.SetTrigger("Show");
            UIManager.ToggleCanvas(MyView.MainCanvasGroup, true);
        }
        
        public override void Hide()
        {
            base.Show();

            UIManager.ToggleCanvas(MyView.MainCanvasGroup, false);
            ((SettingsView)MyView).PanelAnimator.SetTrigger("Hide");
        }

        private void OnPanelClicked(BaseEventData eventData)
        {
            UIManager.ToggleModel(ModelType.Settings, false);
        }

        private void OnQualityChanged(int index)
        {
            QualitySettings.SetQualityLevel(index);
        }
    }
}

using UI.MVC.Bases;
using UnityEngine;

namespace UI.MVC
{
    public class SettingsUIModel : UIModel
    {
        [Header("View")] 
        [SerializeField] private SettingsView settingsView;

        private void Awake()
        {
            Controller = new SettingsController(settingsView);
        }

        protected override void BindModel()
        {
            UIManager.AddModel(ModelType.Settings, this);
            Controller.Initialize();
        }
    }
}

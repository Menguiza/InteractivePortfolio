using System;
using UI.MVC.Bases;
using UnityEngine;

namespace UI.MVC
{
    public class SettingsUIModel : UIModel
    {
        [Header("View")] 
        [SerializeField] private SettingsView settingsView;

        [Header("Parameters")]
        [SerializeField] private KeyCode settingsKey;

        Action keyCallBack;

        private void Awake()
        {
            Controller = new SettingsController(settingsView, out keyCallBack);
        }

        private void Update()
        {
            if(Input.GetKeyDown(settingsKey))
            {
                keyCallBack?.Invoke();
            }
        }

        protected override void BindModel()
        {
            UIManager.AddModel(ModelType.Settings, this);
            Controller.Initialize();
        }
    }
}

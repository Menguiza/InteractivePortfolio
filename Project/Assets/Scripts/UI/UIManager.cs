using System;
using System.Collections.Generic;
using UI.MVC.Bases;
using UnityEngine;

namespace UI
{
    public enum ModelType
    {
        Loading,
        Menu,
        Settings
    }
    
    public class UIManager : MonoBehaviour
    {
        public static bool Available { get; private set; }
        
        private static Action<ModelType, bool> _toggleModelHelper;
        private static Action<ModelType, UIModel> _addModelHelper;

        private readonly Dictionary<ModelType, UIModel> _uiModels = new Dictionary<ModelType, UIModel>();

        private void Awake()
        {
            _addModelHelper += OnAddModel;
            _toggleModelHelper += OnToggleModel;

            Available = true;
        }

        public static void AddModel(ModelType modelType, UIModel newModel)
        {
            _addModelHelper?.Invoke(modelType, newModel);
        }
        
        public static void ToggleModel(ModelType modelType, bool visible)
        {
            _toggleModelHelper?.Invoke(modelType, visible);
        }
        
        public static void ToggleCanvas(CanvasGroup targetCanvasGroup, bool visible, bool interactable = true, bool raycastBlock = true)
        {
            if(targetCanvasGroup == null) return;

            targetCanvasGroup.alpha = visible ? 1 : 0;
            targetCanvasGroup.interactable = visible && interactable;
            targetCanvasGroup.blocksRaycasts = visible && raycastBlock;
        }

        private void OnAddModel(ModelType modelType, UIModel newModel)
        {
            _uiModels.TryAdd(modelType, newModel);
        }

        private void OnToggleModel(ModelType modelType, bool visible)
        {
            if(visible) _uiModels[modelType].Controller.Show();
            else _uiModels[modelType].Controller.Hide();
        }
    }
}

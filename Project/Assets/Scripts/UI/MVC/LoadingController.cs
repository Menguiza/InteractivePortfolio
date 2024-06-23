using UI.MVC.Bases;
using UnityEngine;
using Utility.GameFlow;

namespace UI.MVC
{
    [System.Serializable]
    public class LoadingController : BaseController
    {
        private Camera loadingCamera;

        public LoadingController(BaseView view, Camera targetCamera) : base(view)
        {
            loadingCamera = targetCamera;
        }
        
        public override void Initialize()
        {
            base.Initialize();
            
            SceneController.OnLoadingScene += Show;
            SceneController.OnNextSceneLoaded += Hide;
        }

        public override void Show()
        {
            base.Show();

            UIManager.ToggleCanvas(MyView.MainCanvasGroup, true);
            loadingCamera.enabled = true;
        }

        public override void Hide()
        {
            base.Hide();
            
            UIManager.ToggleCanvas(MyView.MainCanvasGroup, false);
            loadingCamera.enabled = false;
        }
    }
}

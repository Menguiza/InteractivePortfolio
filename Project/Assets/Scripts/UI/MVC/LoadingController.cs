using UI.MVC.Bases;
using Utility.GameFlow;

namespace UI.MVC
{
    [System.Serializable]
    public class LoadingController : BaseController
    {
        public LoadingController(BaseView view) : base(view)
        {
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
        }

        public override void Hide()
        {
            base.Hide();
            
            UIManager.ToggleCanvas(MyView.MainCanvasGroup, false);
        }
    }
}

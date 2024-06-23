using UI.MVC.Bases;
using UnityEngine;

namespace UI.MVC
{
    public class LoadingUIModel : UIModel
    {
        [Header("View")] 
        [SerializeField] private LoadingView loadingView;

        [Header("Camera")]
        [SerializeField] private Camera loadingCamera;

        private void Awake()
        {
            Controller = new LoadingController(loadingView, loadingCamera);
        }

        protected override void BindModel()
        {
            UIManager.AddModel(ModelType.Loading, this);
            Controller.Initialize();
        }
    }
}

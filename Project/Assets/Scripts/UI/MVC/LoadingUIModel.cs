using UI.MVC.Bases;
using UnityEngine;

namespace UI.MVC
{
    public class LoadingUIModel : UIModel
    {
        [Header("View")] 
        [SerializeField] private LoadingView loadingView;

        private void Awake()
        {
            Controller = new LoadingController(loadingView);
        }

        protected override void BindModel()
        {
            UIManager.AddModel(ModelType.Loading, this);
            Controller.Initialize();
        }
    }
}

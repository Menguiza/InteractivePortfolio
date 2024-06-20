using UI.MVC.Bases;
using UnityEngine;

namespace UI.MVC
{
    public class BannerUIModel : UIModel
    {
        [Header("View")] 
        [SerializeField] private BannerView bannerView;

        private void Awake()
        {
            Controller = new BannerController(bannerView);
        }

        protected override void BindModel()
        {
            Controller.Initialize();
        }
    }
}

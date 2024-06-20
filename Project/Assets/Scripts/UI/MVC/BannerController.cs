using UI.MVC.Bases;

namespace UI.MVC
{
    public class BannerController : BaseController
    {
        public BannerController(BaseView view) : base(view)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            
            ((BannerView)MyView).SettingsButton.onClick.AddListener(() => UIManager.ToggleModel(ModelType.Settings, true));
        }
    }
}

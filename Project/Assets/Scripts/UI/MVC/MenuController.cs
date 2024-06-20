using UI.MVC.Bases;
using UnityEngine.Device;

namespace UI.MVC
{
    public class MenuController : BaseController
    {
        private readonly MenuUIModel _menuUIModel;
        
        public MenuController(UIModel uiModel, BaseView view) : base(view)
        {
            _menuUIModel = (MenuUIModel)uiModel;
        }

        public override void Initialize()
        {
            base.Initialize();
            
            ((MenuView)MyView).PlayButton.onClick.AddListener(CallForEndState);
            
            ((MenuView)MyView).LinkedInButton.onClick.AddListener(() => Application.OpenURL(_menuUIModel.LinkedInURL));
            ((MenuView)MyView).GithubButton.onClick.AddListener(() => Application.OpenURL(_menuUIModel.GitHubURL));
            ((MenuView)MyView).MailButton.onClick.AddListener(() => Application.OpenURL(_menuUIModel.MailURl));
        }
    }
}

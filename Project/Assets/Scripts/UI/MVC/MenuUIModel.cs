using UI.MVC.Bases;
using UnityEngine;

namespace UI.MVC
{
    public class MenuUIModel : UIModel
    {
        [Header("View")] 
        [SerializeField] private MenuView menuView;

        [Header("Parameters")] 
        [SerializeField] private string linkedInURL;
        [SerializeField] private string gitHubURL;
        [SerializeField] private string mailURL;

        public string LinkedInURL => linkedInURL;
        public string GitHubURL => gitHubURL;
        public string MailURl => mailURL;
        
        private void Awake()
        {
            Controller = new MenuController(this, menuView);
        }

        protected override void BindModel()
        {
            UIManager.AddModel(ModelType.Menu, this);
            Controller.Initialize();
        }
    }
}

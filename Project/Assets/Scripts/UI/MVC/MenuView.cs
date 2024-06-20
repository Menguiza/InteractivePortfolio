using UI.MVC.Bases;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MVC
{
    public class MenuView : BaseView
    {
        [Header("Buttons")]
        [SerializeField] private Button playButton; 
        [SerializeField] private Button linkedInButton; 
        [SerializeField] private Button githubButton; 
        [SerializeField] private Button mailButton;

        public Button PlayButton => playButton;
        public Button LinkedInButton => linkedInButton;
        public Button GithubButton => githubButton;
        public Button MailButton => mailButton;
    
        public override void SetUp()
        {
            PlayButton.onClick.RemoveAllListeners();
            LinkedInButton.onClick.RemoveAllListeners();
            GithubButton.onClick.RemoveAllListeners();
            MailButton.onClick.RemoveAllListeners();
        }
    }
}

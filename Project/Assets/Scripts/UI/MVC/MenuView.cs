using UnityEngine;
using UnityEngine.UI;

public class MenuView : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button playButton; 
    [SerializeField] private Button linkedInButton; 
    [SerializeField] private Button githubButton; 
    [SerializeField] private Button mailButton;

    public Button PlayButton { get => playButton; }
    public Button LinkedInButton { get => linkedInButton; }
    public Button GithubButton { get => githubButton; }
    public Button MailButton { get => mailButton; }
}

using UnityEngine;
using Utility.GameFlow;

public class MenuController : MonoBehaviour
{
    [SerializeField] private MenuView menuView;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (menuView == null) return;

        menuView.PlayButton?.onClick.AddListener(CallForEndState);
    }

    private void CallForEndState()
    {
        GameManager.OnMoveOn?.Invoke();
    }
}

using UI.MVC.Bases;
using UnityEngine;

namespace UI.MVC
{
    public class LoadingView : BaseView
    {
        [Header("Canvas Group")]
        [SerializeField] private CanvasGroup targetCanvasGroup;

        public override void SetUp()
        {
            mainCanvasGroup = targetCanvasGroup;
        }
    }
}

using UnityEngine;

namespace UI.MVC.Bases
{
    public interface IView
    {
        public CanvasGroup MainCanvasGroup { get; }
    }
    
    public abstract class BaseView : MonoBehaviour, IView
    {
        protected CanvasGroup mainCanvasGroup;
        
        public CanvasGroup MainCanvasGroup => mainCanvasGroup;

        public abstract void SetUp();
    }
}

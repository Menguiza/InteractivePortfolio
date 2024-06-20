using UnityEngine;
using Utility.GameFlow;

namespace UI.MVC.Bases
{
    public interface IController
    {
        BaseView MyView { get; set; }
        void Initialize();
    }
    
    public abstract class BaseController : IController
    {
        BaseView IController.MyView { get; set; }
        
        protected BaseView MyView
        {
            get => ((IController)this).MyView;
            set => ((IController)this).MyView = value;
        }

        protected BaseController(BaseView view)
        {
            MyView = view;
        }

        public virtual void Initialize()
        {
            if (MyView == null) return;
            
            MyView.SetUp();
        }
        
        public virtual void Show()
        {
            if (MyView == null) return;
        }
        
        public virtual void Hide()
        {
            if (MyView == null) return;
        }
        
        protected void CallForEndState()
        {
            GameManager.OnMoveOn?.Invoke();
        }
    }
}

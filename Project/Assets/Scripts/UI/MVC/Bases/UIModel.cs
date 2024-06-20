using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace UI.MVC.Bases
{
    public abstract class UIModel : MonoBehaviour
    {
        public BaseController Controller { get; protected set; }

        private void OnEnable()
        {
            BindTask();
        }

        protected abstract void BindModel();

        private async void BindTask()
        {
            while (!UIManager.Available)
            {
                await Task.Yield();
            }
            
            BindModel();
        }
    }
}

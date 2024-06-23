using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility.GameFlow
{
    public class SceneController : MonoBehaviour
    {
        public delegate void SceneLoad();
        public static event SceneLoad OnLoadingScene;
        public static event SceneLoad OnNextSceneLoaded;
        public static event SceneLoad OnLastSceneUnloaded;
        
        [Header("Parameters")]
        [SerializeField] private LoadSceneMode defaultSceneLoadMode = LoadSceneMode.Additive;
        
        private byte _currentSceneIndex;
        
        private void Awake()
        {
            GameManager.OnStateChanged += SceneChange;
        }

        private async void SceneChange(GameStates state)
        {
            sbyte targetIndex = -1;

            switch (state)
            {
                case GameStates.Menu:
                    targetIndex = 1;
                    break;
                case GameStates.Entrance:
                    targetIndex = 2;
                    break;
                case GameStates.Map:
                    targetIndex = 3;
                    break;
            }

            if(targetIndex != -1)
            {
                await UnloadScene(_currentSceneIndex);
                await LoadScene((byte)targetIndex, defaultSceneLoadMode);
            }
        }

        private async Task LoadScene(byte targetSceneIndex, LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            if(targetSceneIndex == 0) return;
            
            OnLoadingScene?.Invoke();
            
            AsyncOperation asyncSceneLoad = SceneManager.LoadSceneAsync(targetSceneIndex, loadMode);

            while (!asyncSceneLoad.isDone)
            {
                await Task.Yield();
            }

            _currentSceneIndex = targetSceneIndex;

            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(targetSceneIndex));
            
            OnNextSceneLoaded?.Invoke();
        }

        private async Task UnloadScene(byte targetSceneIndex)
        {
            if(targetSceneIndex == 0) return;
            
            AsyncOperation asyncSceneUnload = SceneManager.UnloadSceneAsync(targetSceneIndex);

            while (!asyncSceneUnload.isDone)
            {
                await Task.Yield();
            }
            
            OnLastSceneUnloaded?.Invoke();
        }
    }
}

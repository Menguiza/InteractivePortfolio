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

        private void SceneChange(GameStates state)
        {
            switch (state)
            {
                case GameStates.Menu:
                    UnloadScene(_currentSceneIndex);
                    LoadScene(1, defaultSceneLoadMode);
                    break;
                case GameStates.Entrance:
                    UnloadScene(_currentSceneIndex);
                    LoadScene(2, defaultSceneLoadMode);
                    break;
                case GameStates.Map:
                    UnloadScene(_currentSceneIndex);
                    LoadScene(3, defaultSceneLoadMode);
                    break;
            }
        }

        private async void LoadScene(byte targetSceneIndex, LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            if(targetSceneIndex == 0) return;
            
            OnLoadingScene?.Invoke();
            
            AsyncOperation asyncSceneLoad = SceneManager.LoadSceneAsync(targetSceneIndex, loadMode);

            while (!asyncSceneLoad.isDone)
            {
                await Task.Yield();
            }

            await Task.Delay(1000);

            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(targetSceneIndex));

            _currentSceneIndex = targetSceneIndex;
            
            OnNextSceneLoaded?.Invoke();
        }

        private async void UnloadScene(byte targetSceneIndex)
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

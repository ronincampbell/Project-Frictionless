using UnityEngine;
using UnityEngine.SceneManagement;

namespace RetroAesthetics.Demos {

	public class ContinueFunction : MonoBehaviour {

		// Declare necessary variables
		public SceneField loadingScene;
		public bool fadeInMenu = true;
		public bool fadeOutMenu = true;
		private int continueTo;
		private RetroCameraEffect _cameraEffect;
		private AsyncOperation _loadingSceneAsync;

		// Define continue function as most recent level
		void Start() 
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			continueTo = PlayerPrefs.GetInt("UnlockedLevels");
			// Fade in menu
			if (fadeInMenu) {
				_cameraEffect = GameObject.FindObjectOfType<RetroCameraEffect>();
				if (_cameraEffect != null) {
					_cameraEffect.FadeIn();
				}
			}
		}

		// Start level and load loading scene in the interim
		virtual public void StartLevel() {
			Time.timeScale = 1f;
				if (_cameraEffect != null) {
					if (loadingScene != null) {
						_loadingSceneAsync = SceneManager.LoadSceneAsync(loadingScene);
						if (_loadingSceneAsync == null) {
							Debug.LogWarning(string.Format(
								"Please add scene `{0}` to the built scenes in the Build Settings.",
								loadingScene.SceneName));
							return;
						}
						_loadingSceneAsync.allowSceneActivation = false; 
					}
				
					_cameraEffect.FadeOut(0.5f, LoadNextScene);
				} else {
					LoadNextScene();
				}
		}

		// Load next scene in build order
		private void LoadNextScene() {
			if (_loadingSceneAsync != null) {
				_loadingSceneAsync.allowSceneActivation = true;
			}

			if (continueTo == 0) {
				SceneManager.LoadSceneAsync("Level1");
			}
			else if (continueTo == 1) {
				SceneManager.LoadSceneAsync("Level2");
			}
			else if (continueTo == 2) {
				SceneManager.LoadSceneAsync("Level2");
			}
		}
	}
}

using UnityEngine;
using UnityEngine.SceneManagement;

namespace RetroAesthetics.Demos {

	public class ContinueFunction : MonoBehaviour {
		public SceneField loadingScene;
		public SceneField levelScene1;

		public bool fadeInMenu = true;
		public bool fadeOutMenu = true;

		private int continueTo;

		private RetroCameraEffect _cameraEffect;
		private AsyncOperation _loadingSceneAsync;

		void Start() 
		{
			continueTo = PlayerPrefs.GetInt("UnlockedLevels");
			if (fadeInMenu) {
				_cameraEffect = GameObject.FindObjectOfType<RetroCameraEffect>();
				if (_cameraEffect != null) {
					_cameraEffect.FadeIn();
				}
			}
		}

		virtual public void StartLevel() {
			Time.timeScale = 1f;
			if (levelScene1 != null) {
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
			} else {
				Debug.LogWarning("Level scene is not set.");
			}
		}

		private void LoadNextScene() {
			if (_loadingSceneAsync != null) {
				_loadingSceneAsync.allowSceneActivation = true;
			}

			if (continueTo == 1) {
				SceneManager.LoadSceneAsync(levelScene1);
			}
		}
	}
}

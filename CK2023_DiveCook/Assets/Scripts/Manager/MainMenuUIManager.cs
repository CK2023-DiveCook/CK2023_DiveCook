using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
	public class MainMenuUIManager : MonoBehaviour
	{
		[SerializeField] private GameObject settingUI;
        [SerializeField] private GameObject HowToUI;
		[SerializeField] private GameObject LogOutUI;

		[SerializeField] private GameObject YesButton;
        [SerializeField] private GameObject NoButton;
		[SerializeField] private GameObject howToExitButton;

        private void Start()
		{
			settingUI.SetActive(false);
            HowToUI.SetActive(false);
			LogOutUI.SetActive(false);
            YesButton.SetActive(false);
			NoButton.SetActive(false);
			howToExitButton.SetActive(false);
        }

		public void StartGame()
		{
			SceneManager.LoadScene("Game");
		}
		public void OpenHowto()
		{
			HowToUI.SetActive(true);
            howToExitButton.SetActive(true);
        }
		public void CloseHowto()
		{
            HowToUI.SetActive(false);
			howToExitButton.SetActive(false);

        }
		public void OpenLogOut()
		{
			LogOutUI.SetActive(true);
            YesButton.SetActive(true);
            NoButton.SetActive(true);
        }
		public void CloseLogOut()
		{
            LogOutUI.SetActive(false);
            YesButton.SetActive(false);
            NoButton.SetActive(false);

        }

        public void OpenSetting()
		{
			settingUI.SetActive(true);
			DataManager.Instance.LoadAudioData();
			AudioManager.Instance.InitSliderData();
		}

		public void CloseSetting()
		{
			settingUI.SetActive(false);
			DataManager.Instance.SaveAudioData();
		}
		public void ExitGame()
		{
            Application.Quit();
        }
	}
}

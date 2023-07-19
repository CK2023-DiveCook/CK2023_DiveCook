using UnityEngine;

namespace Manager
{
    public class MainMenuUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject settingUI;

        private void Start()
        {
            settingUI.SetActive(false);
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
    }
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Manager;

public class PlayerOxygen : MonoBehaviour
{
    [SerializeField] private bool stopOxygenCycle = false;
    [SerializeField] private float oxygenLevel = 100;
    [SerializeField] private float oxygenMax = 100;
    [SerializeField] private float oxygenDecrease = -0.334f;
    [SerializeField] public  float oxygenIncrease = 20f;
    [SerializeField] private Slider oxygenLevelSlider;
    [SerializeField] private float ticTime = 0.1f;

    private WaitForSeconds _tic;
    private PlayerControls _playerControls;

    private void Start()
    {
        _playerControls = GetComponent<PlayerControls>();
        _tic = new WaitForSeconds(ticTime);
        StartCoroutine(OxygenCycle());
    }
    private IEnumerator OxygenCycle()
    {
        while (!stopOxygenCycle)
        {
            yield return _tic;
            AddOxygenLevel(oxygenDecrease);
            if (true)
                oxygenLevel = 100;
        }
    }
    public void AddOxygenLevel(float val)
    {
        if (oxygenLevel + val <= 0)
        {
            GameManager.Instance.GameOver();
            oxygenLevel = 0;
        }
        else
        {
            if (oxygenLevel + val > oxygenMax)
                oxygenLevel = oxygenMax;
            else
                oxygenLevel += val;
        }
        oxygenLevelSlider.value = oxygenLevel;
    }
}

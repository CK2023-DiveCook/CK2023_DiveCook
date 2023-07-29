using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishBag : MonoBehaviour
{
    [SerializeField] private Sprite fishEmpty;
    [SerializeField] private Sprite fishS;
    [SerializeField] private Sprite fishM;
    [SerializeField] private Sprite fishL;

    private Image _fishBagImage;
    // Start is called before the first frame update
    void Start()
    {
        _fishBagImage = GetComponent<Image>();
    }

    public void SetImage(Manager.FishType fishType)
    {
        switch (fishType)
        {
            case Manager.FishType.None :
                _fishBagImage.sprite = fishEmpty;
                return;
            case Manager.FishType.L :
                _fishBagImage.sprite = fishL;
                return;
            case Manager.FishType.M :
                _fishBagImage.sprite = fishM;
                return;
            case Manager.FishType.S :
                _fishBagImage.sprite = fishS;
                return;
            default:
                throw new ArgumentOutOfRangeException(nameof(fishType), fishType, null);
        }
    }
}

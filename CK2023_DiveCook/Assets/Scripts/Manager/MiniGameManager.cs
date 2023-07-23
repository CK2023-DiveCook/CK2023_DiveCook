using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] ChangeImage Fish;
    [SerializeField] ChangeImage Gage;
    private int CuttingGage = 0;
    public Line[] Lines;
    private int CuttingNumber = 1;

    public void ChangeImage()
    {
        Fish.ChangeToNextSprite();
    }

    public int GetCuttingNumber()
    {
        return CuttingNumber;
    }

    public void CuttingNumberUp()
    {
        CuttingNumber++;
    }

    public void CuttingGageUp()
    {
        CuttingGage++;
        Gage.ChangeToNextSprite();
    }
    public int GetCuttingGage()
    {
        return CuttingGage;
    }
    public void CuttingGageReset()
    {
        CuttingGage = 0;
        Gage.FirstSprite();
    }


}

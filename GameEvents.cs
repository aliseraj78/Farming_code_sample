using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameEvents current;
    void Awake()
    {
        current = this;
    }

    // Update is called once per frame
    public event Action onCoinCollect;
    public event Action<CropType,FarmManager> onCropCollect;
    public void CoinCollect()
    {
        onCoinCollect?.Invoke();
    }

    public void CollectCrop(CropType type,FarmManager farm)
    {
        onCropCollect?.Invoke(type,farm);
//        Debug.Log("inside game event");
    }
}

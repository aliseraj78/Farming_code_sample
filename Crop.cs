using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crop")]
public class Crop : ScriptableObject
{
    public float timeToGrow;
    
    public  Sprite[] seedTogrowth;
    public Sprite icon;
    
    public CropType CropType;
    
    public bool isAvailable;

    public int amount;
    public int numberOfSeeds;


}



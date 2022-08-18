using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField]
    private Crop[] Crops;

    public int coin;
    public void AddSeed(CropType type,int number)
    {
        for(int i=0;i<Crops.Length;i++)
        {
            if (Crops[i].CropType == type)
            {
                if(!Crops[i].isAvailable)
                    Crops[i].isAvailable = true;
                Crops[i].numberOfSeeds+=number;
                return;
            }
        }
    }

    public void RemoveOneSeed(Crop temp)
    {
        foreach (Crop crop in Crops)
        {
            if (crop == temp)
            {
                crop.numberOfSeeds--;
            }
        }
    }

    public List<Crop> getAvailableCrops()
    {
        List<Crop> temp = new List<Crop>();
        foreach (Crop crop in Crops)
        {
            if (crop.isAvailable && crop.numberOfSeeds!=0)
            {
                temp.Add(crop);
            }

            if (crop.numberOfSeeds == 0)
                crop.isAvailable = false;
        }

        return temp;
    }

    public List<Crop> getAllCrops()
    {
        List<Crop> temp = new List<Crop>();
        foreach (Crop crop in Crops)
        {
            temp.Add(crop);
        }

        return temp;
    }
}

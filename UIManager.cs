using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]private TMP_Text coinAmount;
    [SerializeField] private Inventory inventory;
    [SerializeField] private EffectsManager effectsManager;

    [SerializeField] private GameObject[] cropUIs;

    private GameObject[] textGameObjects;

    private GameObject[] cropIconGameObjects;

    private List<Crop> tempCropList;
    // Start is called before the first frame update
    void Start()
    {
        tempCropList = new List<Crop>();
        tempCropList = inventory.getAllCrops();
        textGameObjects = GameObject.FindGameObjectsWithTag("CropAmountUI");
        cropIconGameObjects = GameObject.FindGameObjectsWithTag("CropIconUI");
        
        GameEvents.current.onCoinCollect += SetCoinText;
        GameEvents.current.onCropCollect += SetCropText;
        
        int i = 0;
        foreach (GameObject cropUI in cropUIs)
        {
            
            foreach (var VARIABLE in cropIconGameObjects)
            {
                if (VARIABLE.transform.IsChildOf(cropUI.transform))
                {
                    VARIABLE.GetComponent<Image>().sprite = tempCropList[i].icon;
                }
            }
            foreach (var VARIABLE in textGameObjects)
            {
                if (VARIABLE.transform.IsChildOf(cropUI.transform))
                {
                    VARIABLE.GetComponent<TMP_Text>().text = tempCropList[i].amount.ToString();
                }
            }

            i++;
        }
    }
    
    private void SetCropText(CropType type,FarmManager farm)
    {
        int i = 0;
//        Debug.Log("inside ui manager");
        foreach (GameObject cropUI in cropUIs)
        {
            if (tempCropList[i].CropType == type)
            {            
                foreach (var VARIABLE in textGameObjects)
                {
                    if (VARIABLE.transform.IsChildOf(cropUI.transform))
                    {
                        VARIABLE.GetComponent<TMP_Text>().text = tempCropList[i].amount.ToString();
                        foreach (var VARIABLE2 in cropIconGameObjects)
                        {
                            if (VARIABLE2.transform.IsChildOf(cropUI.transform))
                            {
                                effectsManager.HarvestEffect(farm.GetFarmCenter(),VARIABLE2.gameObject.transform.position,farm.getCurrentCrop());
                            }
                        }
                        
                        
                        break;
                    }
                }
            }
            i++;
        }
    }
    private void SetCoinText()
    {
        coinAmount.text = inventory.coin.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

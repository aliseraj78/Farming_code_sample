using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCircleContainer : MonoBehaviour
{
    // Start is called before the first frame update
    private FarmManager currentFarm;
    [SerializeField] private Inventory inventory;
    [SerializeField]private GameObject cropCirclePrefab;
    [SerializeField] private Transform Circletop;
    [SerializeField] private Transform mainCircle;
    private List<GameObject> circleArr; 
    private float raduis;
    void Start()
    {
        
        
    }

    private void OnEnable()
    {

        raduis = Vector3.Distance(mainCircle.position, Circletop.position);
        int availableCropsSize = inventory.getAvailableCrops().Count;
        int i = 1;
        float x,y;
        Vector3 position ;
        foreach (Crop crop in inventory.getAvailableCrops())
        {
            float teta = Mathf.RoundToInt(360 / availableCropsSize * i);
            //Debug.Log(teta);
            teta = Mathf.Deg2Rad*teta;
            //Debug.Log(teta+"--"+availableCropsSize);
            x = raduis * Mathf.Sin(teta);
            y = raduis * Mathf.Cos(teta);
            position = new Vector3(x,y,-50);
//            Debug.Log(mainCircle.localPosition);
            i++;
            GameObject cropCircle = Instantiate(cropCirclePrefab, position, Quaternion.identity);
            cropCircle.transform.parent = transform;
            cropCircle.transform.localPosition = position;
            cropCircle.GetComponent<CropCircle>().SetCurrentCropAndFarm(crop,currentFarm);
            cropCircle.name = cropCircle.name + "(" + (i - 1).ToString() + ")";
            //DestroyCircles();

        }
//        Debug.Log("enable");
    }


        public void DestroyCircles()
        {
            foreach (var VARIABLE in GameObject.FindGameObjectsWithTag("CropCircle"))
            {
                Destroy(VARIABLE);
            }
            gameObject.SetActive(false);
        }

        

        public void getFarm(FarmManager farmManager)
    {
        currentFarm = farmManager;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

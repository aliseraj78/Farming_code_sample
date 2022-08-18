using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CropCircle : MonoBehaviour
{
    // Start is called before the first frame update
    private Crop currentCrop;
    private FarmManager currentFarm;
    private GameObject parent;
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject child;
    [SerializeField] private TMP_Text numberOfSeedsTexTmPro;
    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
    }
    public void SetCurrentCropAndFarm(Crop crop,FarmManager farm)
    {
        currentCrop = crop;
        currentFarm = farm;
        child.GetComponent<SpriteRenderer>().sprite = crop.seedTogrowth[4];
        numberOfSeedsTexTmPro.text = currentCrop.numberOfSeeds.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 MousePosition = Input.mousePosition;

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(MousePosition), Vector2.zero);
            //container.SetActive(false);
            if (hit)
            {
                if (hit.collider.gameObject.name == gameObject.name)
                {
//                    Debug.Log("herere");
                    currentFarm.PlantCrop(currentCrop);
                    inventory.RemoveOneSeed(currentCrop);
                    parent.GetComponent<PlantCircleContainer>().DestroyCircles();

                }
                else if(!hit.collider.gameObject.CompareTag("CropCircle"))
                {
                    parent.GetComponent<PlantCircleContainer>().DestroyCircles();
                    parent.SetActive(true);
                }
            }
            else
            {
                parent.GetComponent<PlantCircleContainer>().DestroyCircles();
            }

            

        }
    }
    
}

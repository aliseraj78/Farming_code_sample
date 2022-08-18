using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject container;

    [SerializeField] private Inventory inventory;

    [SerializeField]
    private PlantCircleContainer containerScript;
    void Start()
    {
        inventory.AddSeed(CropType.Corrato,5);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 MousePosition = Input.mousePosition;

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(MousePosition), Vector2.zero);
            //container.SetActive(false);
            if (hit && hit.collider.CompareTag("Farm"))
            {
                FarmManager clickedFarm = hit.collider.gameObject.GetComponent<FarmManager>();
                if(inventory.getAvailableCrops().Count==0 || !clickedFarm.isAvailable() )
                    return;
                //Debug.Log(inventory.getAvailableCrops());
                containerScript.getFarm(clickedFarm);
                container.transform.position = clickedFarm.GetFarmCenter();
                container.SetActive(true);
            }

        }
    }
    
}

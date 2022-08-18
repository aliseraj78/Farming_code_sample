using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class FarmManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Tilemap tileMap,cropsTilemap;
    private Vector3Int[] tilesToFarm;
    private Vector3 farmCenter;
    private float timer = 0;
    private bool readyToHarvest;

    private int cropGrowState=0;
    private Crop currentCrop;

    public Crop getCurrentCrop()
    {
        return currentCrop;
    }
    void Start ()
    {
        farmCenter = new Vector3();
        cropsTilemap = GameObject.FindGameObjectWithTag("Crops").GetComponent<Tilemap>();
        tileMap = gameObject.GetComponent<Tilemap>();
        FillTilesToFarm();
        //PlantCrop(CropType.Corrato);

    }

    public bool isAvailable()
    {
        if (currentCrop)
        {
            return false;
        }

        return true;
    }
    private void FillTilesToFarm()
    {
        tilesToFarm = new Vector3Int[4];
        foreach (var position in tileMap.cellBounds.allPositionsWithin) 
        {

            //Vector3 place = tileMap.CellToWorld(position);
            if (tileMap.GetTile(position))
            {
                int temp = 0;
                //Tile at "place"
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        tilesToFarm[temp]=position + new Vector3Int(i + 1, j + 1, 0);
                        //Debug.Log( tileMap.GetCellCenterLocal(tilesToFarm[temp]));
                        temp++;
                    }
                }

                //Debug.Log(position+"---"+gameObject.name);
                break;
            }

            
        }

        farmCenter.x = tileMap.GetCellCenterWorld(tilesToFarm[3]).x + tileMap.GetCellCenterWorld(tilesToFarm[0]).x;
        farmCenter.y = tileMap.GetCellCenterWorld(tilesToFarm[0]).y + tileMap.GetCellCenterWorld(tilesToFarm[3]).y;
        farmCenter.z = 0;
        farmCenter = farmCenter / 2;
//        Debug.Log(farmCenter);
    }

    public Vector3 GetFarmCenter()
    {
        return farmCenter;
    }
    // Update is called once per frame
    void Update()
    {
        if (readyToHarvest)
        {
            Harvest();
        }
    }

    private void Harvest()
    {
        CancelInvoke("GrowCrop");
        currentCrop.amount += 5;
        GameEvents.current.CollectCrop(currentCrop.CropType,this);
        ClearCrops();
        currentCrop = null;
        readyToHarvest = false;
    }

    public void GrowCrop()
    {
        cropGrowState++;
        PlaceCropsInTiles(currentCrop.seedTogrowth[cropGrowState]);
        if (cropGrowState == 4)
        {
            //Todo harvest
            readyToHarvest = true;
            cropGrowState = 0;
        }
    }
    public void PlaceCropInTile(Sprite sprite,Vector3Int localPos)
    {
        Tile cropTile = ScriptableObject.CreateInstance<Tile>();
        cropTile.sprite = sprite;
        cropsTilemap.SetTile(localPos,cropTile);
        
    }

    void PlaceCropsInTiles(Sprite sprite)
    {
        foreach (Vector3Int i in tilesToFarm)
        {
            PlaceCropInTile(sprite,i);
        }
    }
    void ClearCrops()
    {
        foreach (Vector3Int i in tilesToFarm)
        {
            cropsTilemap.SetTile(i,null);
        }
    }
    public void PlantCrop(Crop crop)
    {
        currentCrop = crop;
        PlaceCropsInTiles(currentCrop.seedTogrowth[0]);
        float timeToInvoke = currentCrop.timeToGrow / 4;
        InvokeRepeating("GrowCrop", timeToInvoke, timeToInvoke);
//        Debug.Log("woooooow");


 
    }
}

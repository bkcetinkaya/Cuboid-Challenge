using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelCreator : MonoBehaviour
{
    
    public GameObject blockGO;
    public GameObject player;
    public GameObject MapBorder;
    public List<GameObject> objectsGO;

    public Transform root;
   
    public float GridWidth;
    public float GridHeight;
    public float NoiseHeight;

    public float GridOffset;
  
    private List<Vector3> blockPositions;

    public void GenerateLevel()
    {
              
        blockPositions = new List<Vector3>();
        DestroyPreviousLevelIfExists();


        for (int i = 0; i < GridHeight; i++)
        {
            for (int j = 0; j < GridWidth; j++)
            {


                Vector3 pos = new Vector3(i * GridOffset, GenerateNoise(i,j,8f) * NoiseHeight, j * GridOffset);
                GameObject block = Instantiate(blockGO, pos, Quaternion.identity);
                blockPositions.Add(block.transform.position);
                block.transform.SetParent(root);
               
            }
        }

        SpawnMapBorderFor();
        SpawnObject();

    }

    private void SpawnMapBorderFor()
    {
        //Left border position 5 + 0.4
        Vector3 leftPos = new Vector3((GridHeight-1 + ((GridHeight-1) * 0.1f))/2,root.position.y+1f, GridWidth + (GridWidth - 2)*0.1f);
        //Right border position
        Vector3 rightPos = new Vector3((GridHeight - 1 + ((GridHeight - 1) * 0.1f)) / 2, root.position.y+1f, -0.9f);
        //Up border position
        Vector3 upPos = new Vector3(GridHeight + (GridHeight-2)*0.1f, root.position.y + 1f, (GridWidth-1 + (GridWidth-1)*0.1f)/2);
        Vector3 backPos = new Vector3(-0.9f, root.position.y + 1f, (GridWidth-1 + (GridWidth-1)*0.1f)/2);
        
        GameObject mapBorderL = Instantiate(MapBorder, leftPos, Quaternion.identity);
        GameObject mapBorderR = Instantiate(MapBorder, rightPos, Quaternion.identity);
        GameObject mapBorderU = Instantiate(MapBorder, upPos, Quaternion.Euler(0,90,0));
        GameObject mapBorderB = Instantiate(MapBorder, backPos, Quaternion.Euler(0,90,0));
        GameObject borderGameObject = new GameObject("Borders");

        mapBorderL.name = "Border";
        mapBorderR.name = "Border";
        mapBorderU.name = "Border";
        mapBorderB.name = "Border";

        mapBorderL.tag = "Border";
        mapBorderR.tag = "Border";
        mapBorderU.tag = "Border";
        mapBorderB.tag = "Border";

        borderGameObject.transform.SetParent(root);
        mapBorderL.transform.SetParent(borderGameObject.transform);
        mapBorderR.transform.SetParent(borderGameObject.transform);
        mapBorderU.transform.SetParent(borderGameObject.transform);
        mapBorderB.transform.SetParent(borderGameObject.transform);

        mapBorderL.GetComponent<BoxCollider>().size=new Vector3(GridHeight + (GridHeight - 1) * 0.1f - 0.3f,3f, 0.5f);
        
        mapBorderR.GetComponent<BoxCollider>().size=new Vector3(GridHeight + (GridHeight - 1) * 0.1f - 0.3f,3f, 0.5f);
        mapBorderU.GetComponent<BoxCollider>().size=new Vector3(GridWidth + (GridWidth-1) * 0.1f-0.3f,3f,0.5f);
        mapBorderB.GetComponent<BoxCollider>().size=new Vector3(GridWidth + (GridWidth-1) * 0.1f-0.3f,3f, 0.5f);
    }

    private void SpawnObject()
    {

        if (objectsGO.Count > 0)
        {
            for (int i = 0; i < (GridHeight * GridWidth) / 4; i++)
            {

                int index = UnityEngine.Random.Range(0, objectsGO.Count);


                GameObject objectToPlace = Instantiate(objectsGO[index], ObjectSpawnLocation(), Quaternion.identity);
                objectToPlace.transform.SetParent(root);
            }
        }

       
    }

    private Vector3 ObjectSpawnLocation()
    {
        int rndIndex = UnityEngine.Random.Range(0, blockPositions.Count);

        Vector3 pos = new Vector3(
            blockPositions[rndIndex].x,
            blockPositions[rndIndex].y + 1f,
            blockPositions[rndIndex].z
            );

        blockPositions.RemoveAt(rndIndex);

        return pos;
    }

    private void DestroyPreviousLevelIfExists()
    {
        // There are already level
        if (root.childCount > 1)
        {
            DestroyCurrentLevel();
        }
   
    }

    public void DestroyCurrentLevel()
    {
        int i = 0;

        //Array to hold all child obj
        GameObject[] allChildren = new GameObject[root.childCount];

        //Find all child obj and store to that array
        foreach (Transform child in root)
        {
           
            allChildren[i] = child.gameObject;
            i += 1;
        }

        foreach (GameObject child in allChildren)
        {
            
            DestroyImmediate(child.gameObject);
        }
    }

    private float GenerateNoise(int x,int z,float detailScale)
    {
        float xNoise = (x) / detailScale;
        float zNoise = (z) / detailScale;

        return Mathf.PerlinNoise(xNoise, zNoise);
    }
}

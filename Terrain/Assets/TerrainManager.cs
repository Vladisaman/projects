using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject terrainObj;
    GameObject[,] TerrainMatrix;
    public float space;


    // Start is called before the first frame update
    void Start()
    {
        TerrainMatrix = new GameObject[11, 11];
        TerrainMatrix[5, 5] = GenerateTerrain(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        int zOffset = (int) Mathf.Floor(Player.transform.position.z / terrainObj.GetComponent<TerrainScript>().width) + 5;
        int xOffset = (int) Mathf.Floor(Player.transform.position.x / terrainObj.GetComponent<TerrainScript>().width) + 5;

        if (Player.transform.position.x >= TerrainMatrix[zOffset, xOffset].transform.position.x + terrainObj.GetComponent<TerrainScript>().width - space)
        {
            if (xOffset + 1 <= TerrainMatrix.GetLength(1))
            {
                if (TerrainMatrix[zOffset, xOffset + 1] == null)
                {
                    TerrainMatrix[zOffset, xOffset + 1] = GenerateTerrain(TerrainMatrix[zOffset, xOffset].transform.position.x + terrainObj.GetComponent<TerrainScript>().width - 6, TerrainMatrix[zOffset, xOffset].transform.position.z);
                }
                else
                {
                    TerrainMatrix[zOffset, xOffset + 1].SetActive(true);
                }

                TurnOffTerain(zOffset, xOffset);
            }
        }
        else if (Player.transform.position.x <= TerrainMatrix[zOffset, xOffset].transform.position.x + space)
        {
            if (xOffset - 1 >= 0)
            {
                if (TerrainMatrix[zOffset, xOffset - 1] == null)
                {
                    TerrainMatrix[zOffset, xOffset - 1] = GenerateTerrain(TerrainMatrix[zOffset, xOffset].transform.position.x - terrainObj.GetComponent<TerrainScript>().width + 6, TerrainMatrix[zOffset, xOffset].transform.position.z);
                }
                else
                {
                    TerrainMatrix[zOffset, xOffset - 1].SetActive(true);
                }

                TurnOffTerain(zOffset, xOffset);
            }
        }

        if (Player.transform.position.z >= TerrainMatrix[zOffset, xOffset].transform.position.z + terrainObj.GetComponent<TerrainScript>().width - space)
        {
            if (zOffset + 1 <= TerrainMatrix.GetLength(0))
            {
                if (TerrainMatrix[zOffset + 1, xOffset] == null)
                {
                    TerrainMatrix[zOffset + 1, xOffset] = GenerateTerrain(TerrainMatrix[zOffset, xOffset].transform.position.x, TerrainMatrix[zOffset, xOffset].transform.position.z + terrainObj.GetComponent<TerrainScript>().width - 6);
                }
                else
                {
                    TerrainMatrix[zOffset + 1, xOffset].SetActive(true);
                }

                TurnOffTerain(zOffset, xOffset);
            }
        }
        else if (Player.transform.position.z <= TerrainMatrix[zOffset, xOffset].transform.position.z + space)
        {
            if (zOffset - 1 >= 0)
            {
                if (TerrainMatrix[zOffset - 1, xOffset] == null)
                {
                    TerrainMatrix[zOffset - 1, xOffset] = GenerateTerrain(TerrainMatrix[zOffset, xOffset].transform.position.x, TerrainMatrix[zOffset, xOffset].transform.position.z - terrainObj.GetComponent<TerrainScript>().width + 6);
                }
                else
                {
                    TerrainMatrix[zOffset - 1, xOffset].SetActive(true);
                }

                TurnOffTerain(zOffset, xOffset);
            }
        }
    }

    public GameObject GenerateTerrain(float x, float z)
    {
        return Instantiate(terrainObj, new Vector3(x, 0, z), Quaternion.identity);
    }

    public void TurnOffTerain(int zPos, int xPos)
    {
        for (int z = 0; z < TerrainMatrix.GetLength(0); z++)
        {
            for (int x = 0; x < TerrainMatrix.GetLength(1); x++)
            {
                if(Mathf.Abs(zPos - z) > 1 || Mathf.Abs(xPos - x) > 1)
                {
                    if (TerrainMatrix[z, x] != null)
                    {
                        TerrainMatrix[z, x].SetActive(false);
                    }
                }
            }
        }
    }
}

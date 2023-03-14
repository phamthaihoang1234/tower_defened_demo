using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject mapTile;
    
    [SerializeField] private int mapWidth;
    [SerializeField] private int mapHeight;

    public static List<GameObject> mapTiles = new List<GameObject>();
    public static List<GameObject> pathTiles = new List<GameObject>();

    

    public static GameObject startTile;
    public static GameObject endTile;

    private bool reachedX = false;
    private bool reachedY = false;
    private GameObject currentTile;
    private int currentIndex;
    private int nextIndex;

    private void Start()
    {
        generateMap();
    }

    private List<GameObject> getTopEdgeTiles()
    {
        List<GameObject> edgeTiles = new List<GameObject>();
        for(int i = mapWidth * (mapHeight-1); i< mapWidth * mapHeight; i++)
        {

            edgeTiles.Add(mapTiles[i]);
        }

        return edgeTiles;
    }


    public Color pathColor;


    private List<GameObject> getBottomEdgeTiles()
    {
        List <GameObject> edgeTiles = new List<GameObject>();

        for (int i = 0; i < mapWidth; i++)
        {
            edgeTiles.Add(mapTiles[i]);
        }

        return edgeTiles;
    }

    private void moveDown()
    {
        pathTiles.Add(currentTile);
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex - mapWidth;
        currentTile = mapTiles[nextIndex];
    }

    private void moveLeft()
    {
        pathTiles.Add(currentTile);
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex - 1;
        currentTile = mapTiles[nextIndex];
    }

    private void moveRight()
    {
        pathTiles.Add(currentTile);
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex + 1;
        currentTile = mapTiles[nextIndex];
    }


    private void generateMap()
    {
        for(int y= 0; y < mapHeight; y++)
        {
            for(int x=0; x < mapWidth; x++)
            {
                GameObject newTile = Instantiate(mapTile);
                mapTiles.Add(newTile);
                newTile.transform.position = new Vector2(x, y);
            }




        }

    List<GameObject> topEdgeTiles = getTopEdgeTiles();
        List<GameObject> bottomEdgeTiles = getBottomEdgeTiles();

        

        int rand1 = Random.Range(0,mapWidth);
        int rand2 = Random.Range(0, mapWidth);
        startTile = topEdgeTiles[rand1];
        endTile = bottomEdgeTiles[rand2];

        currentTile = startTile;

        moveDown();

        int loopCount = 0;

        while (reachedX == false)
        {
            loopCount++;
            if(loopCount > 100)
            {
                break;
            }

            if(currentTile.transform.position.x > endTile.transform.position.x)
            {
                moveLeft();
            }
            else if (currentTile.transform.position.x < endTile.transform.position.x)
            {
                moveRight();
            }
            else
            {
                reachedX = true;
            }

        }

        while (reachedY == false)
        {
            

            if (currentTile.transform.position.y > endTile.transform.position.y)
            {
               moveDown();
            }
            else 
            {
               reachedY = true;
            }


        }

        pathTiles.Add(endTile);


        
        foreach (GameObject obj in pathTiles)
        {
            obj.GetComponent<SpriteRenderer>().color = pathColor;
        }


    }
}

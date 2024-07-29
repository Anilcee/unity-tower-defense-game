using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
   private PathGenerator pathGenerator;

   public int minPathLength=30;
   public int gridWidth=16;
   public int gridHeight=8;

    private EnemyWaweManager waweManager;
    public GridCellObject[] gridCells;
    public GridCellObject[] sceneryCellObjects;

    void Start()
    {
        pathGenerator = new PathGenerator(gridWidth, gridHeight);
        waweManager=GetComponent<EnemyWaweManager>();
        List<Vector2Int> pathCells = pathGenerator.GeneratePath();
        int pathSize = pathCells.Count;
        while (pathSize < minPathLength) {
            pathCells = pathGenerator.GeneratePath();
            while(pathGenerator.GenerateCrossRoad());
            pathSize = pathCells.Count;
        }
        
        StartCoroutine(CreateGrid(pathCells));
        waweManager.SetPathCells(pathCells);
        
    }
    IEnumerator CreateGrid(List<Vector2Int> pathCells)
    {
        yield return LayPathCells(pathCells);
        yield return LaySceneryCells();
        
    }
    IEnumerator LayPathCells(List<Vector2Int> pathCells)
    {
        foreach (Vector2Int pathCell in pathCells)
        {
            int neighbourValue = pathGenerator.getCellNeighbourValue(pathCell.x, pathCell.y);
            GameObject pathTile = gridCells[neighbourValue].cellPrefab;
            GameObject pathTileCell =Instantiate(pathTile, new Vector3(pathCell.x, 0f, pathCell.y), Quaternion.identity);
            pathTileCell.transform.Rotate(0f, gridCells[neighbourValue].yRotation, 0f,Space.Self);
            //Debug.Log("Tile "+pathCell.x+","+pathCell.y+" neighbour value = "+ neighbourValue);
            yield return new WaitForSeconds(0.04f);
        }
        yield return null;
    }


    IEnumerator LaySceneryCells()
    {
        for (int y = gridHeight-1; y >= 0; y--)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                if (pathGenerator.CellIsFree(x, y))
                {
                    int randomSceneryCellIndex =Random.Range(0, sceneryCellObjects.Length);  
                    Instantiate(sceneryCellObjects[randomSceneryCellIndex].cellPrefab, new Vector3(x, 0f, y), Quaternion.identity);
                    yield return new WaitForSeconds(0.01f);
                }
            }
        }
        yield return null ;
    }
}

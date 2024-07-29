using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    private int width,height;
    public List<Vector2Int> pathCells;
    public PathGenerator(int width, int height){
        this.width=width;
        this.height=height;
    }
    public List<Vector2Int> GeneratePath(){
        pathCells=new List<Vector2Int>();
        int y=(int)(height/2);
        int x=0;
        while (x<width)
        {
            pathCells.Add(new Vector2Int(x,y));
            bool validMode=false;
            while(!validMode){
                int move= Random.Range(0,3);
                if(move==0 || x%2==0|| x>(width-2)){   //right
                    x++;
                    validMode=true;
                }
                else if(move==1 && CellIsFree(x,y+1) && y<(height-2)){    //up
                    y++;
                    validMode=true;
                }
                else if(move==2 && CellIsFree(x,y-1) && y>2){    //down
                    y--;
                    validMode=true;
                }
            }
        }
        return pathCells;
    }

    public bool GenerateCrossRoad()
    {
        for (int i = 0; i < pathCells.Count; i++)
        {
            Vector2Int pathCell=pathCells[i];
            if(pathCell.x > 3 && pathCell.x < width - 4 && pathCell.y > 4 && pathCell.y < height - 3)
            {
                if(CellIsFree(pathCell.x,pathCell.y+3)&&CellIsFree(pathCell.x+1,pathCell.y+3)&&CellIsFree(pathCell.x+2,pathCell.y+3)
                &&CellIsFree(pathCell.x-1,pathCell.y+2)&&CellIsFree(pathCell.x,pathCell.y+2)&&CellIsFree(pathCell.x+1,pathCell.y+2)&&CellIsFree(pathCell.x+2,pathCell.y+2)&&CellIsFree(pathCell.x+3,pathCell.y+2)
                &&CellIsFree(pathCell.x-1,pathCell.y+1)&&CellIsFree(pathCell.x,pathCell.y+1)&&CellIsFree(pathCell.x+1,pathCell.y+1)&&CellIsFree(pathCell.x+2,pathCell.y+1)&&CellIsFree(pathCell.x+3,pathCell.y+1)
                &&CellIsFree(pathCell.x+1,pathCell.y)&&CellIsFree(pathCell.x+2,pathCell.y)&&CellIsFree(pathCell.x+3,pathCell.y)
                &&CellIsFree(pathCell.x+1,pathCell.y-1)&&CellIsFree(pathCell.x+2,pathCell.y-1))
            {
                pathCells.InsertRange(i+1,new List<Vector2Int>{new Vector2Int(pathCell.x+1,pathCell.y),new Vector2Int(pathCell.x+2,pathCell.y),new Vector2Int(pathCell.x+2,pathCell.y+1),new Vector2Int(pathCell.x+2,pathCell.y+2),new Vector2Int(pathCell.x+1,pathCell.y+2),new Vector2Int(pathCell.x,pathCell.y+2),new Vector2Int(pathCell.x,pathCell.y+1)});
                return true;
            }
            

            if(CellIsFree(pathCell.x+1,pathCell.y+1)&&CellIsFree(pathCell.x+2,pathCell.y+1)
            &&CellIsFree(pathCell.x+1,pathCell.y)&&CellIsFree(pathCell.x+2,pathCell.y)&&CellIsFree(pathCell.x+3,pathCell.y)
            &&CellIsFree(pathCell.x-1,pathCell.y-1)&&CellIsFree(pathCell.x,pathCell.y-1)&&CellIsFree(pathCell.x+1,pathCell.y-1)&&CellIsFree(pathCell.x+2,pathCell.y-1)&&CellIsFree(pathCell.x+3,pathCell.y-1)
            &&CellIsFree(pathCell.x-1,pathCell.y-2)&&CellIsFree(pathCell.x,pathCell.y-2)&&CellIsFree(pathCell.x+1,pathCell.y-2)&&CellIsFree(pathCell.x+2,pathCell.y-2)&&CellIsFree(pathCell.x+3,pathCell.y-2)
            &&CellIsFree(pathCell.x,pathCell.y-3)&&CellIsFree(pathCell.x+1,pathCell.y-3)&&CellIsFree(pathCell.x+2,pathCell.y-3))
            {
                pathCells.InsertRange(i+1,new List<Vector2Int>{new Vector2Int(pathCell.x+1,pathCell.y),new Vector2Int(pathCell.x+2  ,pathCell.y),new Vector2Int(pathCell.x+2,pathCell.y-1),new Vector2Int(pathCell.x+2,pathCell.y-2),new Vector2Int(pathCell.x+1,pathCell.y-2),new Vector2Int(pathCell.x,pathCell.y-2),new Vector2Int(pathCell.x,pathCell.y-1)});
                return true;            
            }
        }
    }
        
        return false;
    }

    public bool CellIsFree(int x, int y){
        return !pathCells.Contains(new Vector2Int(x,y));
    }

    public bool CellIsTaken(int x, int y)
    {
        return pathCells.Contains(new Vector2Int(x, y));
    }

    public int getCellNeighbourValue(int x, int y)
    {
        int returnValue = 0;
        if(CellIsTaken(x, y - 1))
        {
            returnValue += 1;
        }
        if (CellIsTaken(x - 1, y))
        {
            returnValue += 2;
        }
        if (CellIsTaken(x+1, y))
        {
            returnValue += 4;
        }
        if (CellIsTaken(x, y + 1))
        {
            returnValue += 8;
        }
        return returnValue;
    }

}

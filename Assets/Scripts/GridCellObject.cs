using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName="GridCell", menuName="TowerDefense/GridCell")]

public class GridCellObject : ScriptableObject
{
    public enum CellType { Path , Ground}

    public CellType cellType;
    public GameObject cellPrefab;
    public int yRotation;

    void Start()
    {
        cellPrefab.gameObject.tag="Ground";
    }
}

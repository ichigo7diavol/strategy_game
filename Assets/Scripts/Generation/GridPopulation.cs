using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPopulation : MonoBehaviour{

    public Material hatch;

    public void PopulateGrid(HexGrid grid)
    {
        for (int y = 0; y < grid.Height; ++y)
        {
            for (int x = 0; x < grid.Width; ++x)
            {
                HexCell cell = grid.GetCell(x, y);
                cell.cellInfo.perlinValue = Util.PerlinNoise(x, grid.Width, y, grid.Height, 3.17f);
                cell.HatchMaterial = hatch;
            }
        }
    }
}

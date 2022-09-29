using Scripts.CellLogic;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.Factories
{
    public class GameboardFactory : IFactory<Cell, Vector2Int, float, Transform, List<Cell>>
    {
        public List<Cell> Create(Cell prefab, Vector2Int size, float spacing, Transform root)
        {
            List<Cell> cells = new();
            int index = 0;

            for (int x = 0; x < size.x; x++)
            {
                index++;

                for (int y = 0; y < size.y; y++)
                {
                    index++;

                    Vector3 position = new(x * spacing, y * spacing);

                    Cell cell = Object.Instantiate(prefab, position + root.position, Quaternion.identity, root);

                    cell.Initialize(new Vector2Int(x, y), index % 2 == 1 ? CellColor.White : CellColor.Black);

                    cells.Add(cell);
                }

               // index++;
            }

            return cells;
        }
    }

}
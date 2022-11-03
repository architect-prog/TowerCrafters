using UnityEngine;

namespace Source.Kernel.Entities
{
    public sealed class Cell : Entity<Vector2Int>
    {
        private bool isFree;
        private readonly Vector2Int position;

        public bool IsFree => isFree;
        public Vector2Int Position => position;
        public override Vector2Int Id => position;

        public Cell(int x, int y) : this(new Vector2Int(x, y))
        {
        }

        public Cell(Vector2Int position)
        {
            this.position = position;
            isFree = true;
        }

        public void FreeCell()
        {
            isFree = true;
        }

        public void FillCell()
        {
            isFree = false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniExtract2024
{
    public class OutOccupyArea
    {
        public CellOffset[] _UnrotatedOccupiedCellsOffsets;
        public CellOffset[] _RotatedOccupiedCellsOffsets;
        public List<string> objectLayers = new List<string>();

        public OutOccupyArea(OccupyArea obj)
        {
            this._UnrotatedOccupiedCellsOffsets = obj._UnrotatedOccupiedCellsOffsets;
            this._RotatedOccupiedCellsOffsets = obj._RotatedOccupiedCellsOffsets;
            foreach(ObjectLayer objectLayer in obj.objectLayers)
            {
                this.objectLayers.Add(objectLayer.GetType().Name);
            }
        }
    }
}

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
        public ObjectLayer[] objectLayers = new ObjectLayer[0];

        public OutOccupyArea(OccupyArea obj)
        {
            this._UnrotatedOccupiedCellsOffsets = obj._UnrotatedOccupiedCellsOffsets;
            this._RotatedOccupiedCellsOffsets = obj._RotatedOccupiedCellsOffsets;
            this.objectLayers = obj.objectLayers;
        }
    }
}

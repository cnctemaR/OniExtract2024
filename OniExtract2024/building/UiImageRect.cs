namespace OniExtract2024.building
{
    /// <summary>
    /// The rendered ui_image PNG's rectangle in cell units, relative to the building
    /// footprint. The footprint occupies (0,0) bottom-left to (widthInCells, heightInCells)
    /// top-right; +x right, +y up; units = cells.
    ///
    /// (x, y) is the bottom-left corner of the PNG in that space; (w, h) is the PNG size
    /// in cells. Overhang is expressed by going outside the footprint — e.g. y &lt; 0 means
    /// the art hangs below the footprint (SteamTurbine2's exhaust). The PNG maps linearly
    /// onto this rectangle, so its pixel aspect equals w:h (true automatically for a
    /// tight crop). See WEBSITE_POSTPROCESSING.md "The contract: uiImageRect".
    /// </summary>
    public struct UiImageRect
    {
        public float x;
        public float y;
        public float w;
        public float h;

        /// <summary>
        /// Maps an opaque-crop bounding box back to a footprint-relative rectangle in cells.
        /// The snapshot camera centres the footprint on the texture centre at
        /// <paramref name="pixelsPerCell"/> px/cell, so a pixel (px, py) — with py = 0 at the
        /// texture bottom, matching GetPixels' bottom-up order — maps to footprint space
        /// ((px - texW/2)/ppc + cellW/2, (py - texH/2)/ppc + cellH/2). The camera-position
        /// terms cancel, so only the texture and footprint sizes are needed. A crop whose
        /// bottom row (minY) sits below the footprint bottom yields a negative y (overhang).
        /// </summary>
        public static UiImageRect FromCrop(
            int minX, int minY, int cropW, int cropH,
            int texW, int texH, int cellW, int cellH, float pixelsPerCell)
        {
            return new UiImageRect
            {
                x = (minX - texW / 2f) / pixelsPerCell + cellW / 2f,
                y = (minY - texH / 2f) / pixelsPerCell + cellH / 2f,
                w = cropW / pixelsPerCell,
                h = cropH / pixelsPerCell,
            };
        }
    }
}

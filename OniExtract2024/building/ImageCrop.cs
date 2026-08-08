using UnityEngine;

namespace OniExtract2024.building
{
    /// <summary>
    /// Pure pixel-level crop helpers. Extracted from BuildingImageSnapshotter so
    /// unit tests can reach them without triggering a load of KMonoBehaviour or any
    /// other Assembly-CSharp-firstpass type (which the .NET 4.8 test host rejects
    /// due to default interface method implementations Unity uses in those DLLs).
    /// </summary>
    internal static class ImageCrop
    {
        /// <summary>
        /// Finds the tightest bounding box of pixels whose alpha exceeds the threshold.
        /// Returns false when the whole image is transparent.
        /// </summary>
        internal static bool FindOpaqueBBox(Color[] px, int w, int h,
            out int minX, out int minY, out int maxX, out int maxY,
            float alphaThreshold = 0.03f)
        {
            minX = w; maxX = -1; minY = h; maxY = -1;
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (px[y * w + x].a > alphaThreshold)
                    {
                        if (x < minX) minX = x;
                        if (x > maxX) maxX = x;
                        if (y < minY) minY = y;
                        if (y > maxY) maxY = y;
                    }
                }
            }
            return maxX >= minX && maxY >= minY;
        }

        internal static Color[] CropPixels(Color[] px, int srcW, int minX, int minY, int cw, int ch)
        {
            Color[] outPx = new Color[cw * ch];
            for (int oy = 0; oy < ch; oy++)
                for (int ox = 0; ox < cw; ox++)
                    outPx[oy * cw + ox] = px[(minY + oy) * srcW + (minX + ox)];
            return outPx;
        }
    }
}

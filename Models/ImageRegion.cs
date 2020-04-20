using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models
{
    public class ImageRegion
    {
        #region Properties

        public readonly SplittedImageRegion BaseRegion;

        public readonly List<SplittedImageRegion> Neighboors = new List<SplittedImageRegion>();

        #endregion

        #region Constructors

        private ImageRegion(SplittedImageRegion splittedImageRegion)
        {
            this.BaseRegion = splittedImageRegion;
        }

        #endregion

        #region Methods

        public List<Pixel> GetPixels()
        {
            List<Pixel> points = new List<Pixel>();
            points.AddRange(this.BaseRegion.GetPixels());
            for (int i = 0; i < this.Neighboors.Count; i++)
            {
                points.AddRange(this.Neighboors[i].GetPixels());
            }
            return points;
        }

        #endregion

        #region Static methods

        public static List<ImageRegion> MergeImageRegions(List<SplittedImageRegion> splittedImageRegions, int mergePixelsRange)
        {
            List<ImageRegion> regions = new List<ImageRegion>();
            while (splittedImageRegions.Count > 0)
            {
                SplittedImageRegion splittedImageRegion = splittedImageRegions[0];
                ImageRegion imageRegion = new ImageRegion(splittedImageRegion);
                splittedImageRegions.Remove(splittedImageRegion);

                // Finding neighboors for base region
                for (int i = 0; i < splittedImageRegions.Count; i++)
                {
                    if (AreImageRegionsInNeighborhood(splittedImageRegion, splittedImageRegions[i]) && AreImageRegionsUniform(splittedImageRegion, splittedImageRegions[i], mergePixelsRange))
                    {
                        SplittedImageRegion neighboor = splittedImageRegions[i];
                        imageRegion.Neighboors.Add(neighboor);
                        splittedImageRegions.Remove(neighboor);
                        i--;
                    }
                }

                // Finding neighboors for neighboors
                bool foundNextNeighboor;
                int lastCount = 0;
                do
                {
                    foundNextNeighboor = false;
                    int first = lastCount;
                    lastCount = imageRegion.Neighboors.Count;

                    for (int i = first; i < lastCount; i++)
                    {
                        for (int j = 0; j < splittedImageRegions.Count; j++)
                        {
                            if (AreImageRegionsInNeighborhood(imageRegion.Neighboors[i], splittedImageRegions[j]) && AreImageRegionsUniform(splittedImageRegion, splittedImageRegions[j], mergePixelsRange))
                            {
                                SplittedImageRegion neighboor = splittedImageRegions[j];
                                imageRegion.Neighboors.Add(neighboor);
                                splittedImageRegions.Remove(neighboor);
                                j--;
                                foundNextNeighboor = true;
                            }
                        }
                    }
                } while (foundNextNeighboor);

                regions.Add(imageRegion);
            }
            return regions;
        }


        private static bool AreImageRegionsUniform(SplittedImageRegion ir1, SplittedImageRegion ir2, int mergePixelsRange)
        {
            return ir1.MaxR - ir2.MinR <= mergePixelsRange && ir2.MaxR - ir1.MinR <= mergePixelsRange
                && ir1.MaxG - ir2.MinG <= mergePixelsRange && ir2.MaxG - ir1.MinG <= mergePixelsRange
                && ir1.MaxB - ir2.MinB <= mergePixelsRange && ir2.MaxB - ir1.MinB <= mergePixelsRange;
        }

        private static bool AreImageRegionsInNeighborhood(SplittedImageRegion ir1, SplittedImageRegion ir2)
        {
            if ((ir1.X == ir2.X + ir2.Width) || (ir1.X + ir1.Width == ir2.Width))
            {
                if((ir1.Y + ir1.Height >= ir2.Y && ir1.Y <= ir2.Y) || (ir1.Y + ir1.Height >= ir2.Y + ir2.Height && ir1.Y <= ir2.Y + ir2.Height))
                {
                    return true;
                }
                if ((ir2.Y + ir2.Height >= ir1.Y && ir2.Y <= ir1.Y) || (ir2.Y + ir2.Height >= ir1.Y + ir1.Height && ir2.Y <= ir1.Y + ir1.Height))
                {
                    return true;
                }
            }
            if ((ir1.Y == ir2.Y + ir2.Height) || (ir1.Y + ir1.Height == ir2.Y))
            {
                if ((ir1.X + ir1.Width >= ir2.X && ir1.X <= ir2.X) || (ir1.X + ir1.Width >= ir2.X + ir2.Width && ir1.X <= ir2.X + ir2.Width))
                {
                    return true;
                }
                if ((ir2.X + ir2.Width >= ir1.X && ir2.X <= ir1.X) || (ir2.X + ir2.Width >= ir1.X + ir1.Width && ir2.X <= ir1.X + ir1.Width))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}

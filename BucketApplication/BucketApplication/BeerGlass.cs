using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketApplication
{
    public class BeerGlass : Bucket
    {
        /// <summary>
        /// Creates a BeerGlass of a size within 0.2 and 0.5 
        /// </summary>
        /// <param name="size">Should be within 0.2 and 0.5</param>
        public BeerGlass(double size)
        {
            if(size >= 0.2 && size <= 0.5)
            {
                this.BucketMaxAmount = size;
            }
        }

        public static BeerGlass operator +(BeerGlass beerGlass1, Bucket bucket)
        {
            beerGlass1.FillBucket(bucket.BucketFilledAmount);
            bucket.EmptyBucket();
            return beerGlass1;
        }
    }
}

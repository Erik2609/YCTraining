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
				//JB: this the Bucket should be responsible for setting this property, not each derived type. 
				//In fact, you've got designated ctor for that purpose.... Why not call that?
			}
			//JB: So new BeerGlass(0.1) would create a glass with size 0. That shouldn't be possible
        }

        public static BeerGlass operator +(BeerGlass beerGlass1, Bucket bucket)
        {
            beerGlass1.FillBucket(bucket.BucketFilledAmount);
            bucket.EmptyBucket();
            return beerGlass1;
        }
    }
}

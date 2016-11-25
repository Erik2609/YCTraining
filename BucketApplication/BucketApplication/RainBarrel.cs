using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketApplication
{
    public class RainBarrel : Bucket
    {
        public enum Size
        {
            Small = 80,
            Medium = 120,
            Large = 160,
        };

        public RainBarrel(Size size) : base((int) size)
        {
			//JB: This still allow for creating a rainbarrel with an invalid size: e.g. new RainBarrel((RainBarrel.Size)81);
		}

		public static RainBarrel operator +(RainBarrel rainBarrel1, Bucket bucket)
        {
            rainBarrel1.FillBucket(bucket.BucketFilledAmount);
            bucket.EmptyBucket();
            return rainBarrel1;
        }
    }
}

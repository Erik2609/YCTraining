namespace BucketApplication
{
    public class OilBarrel : Bucket
    {
        public OilBarrel() : base(159)
        {
            
        }

        public static OilBarrel operator +(OilBarrel oilBarrel1, OilBarrel oilBarrel2)
        {
            oilBarrel1.FillBucket(oilBarrel2.BucketFilledAmount);
            oilBarrel2.EmptyBucket();
            return oilBarrel1;
        }

        public static OilBarrel operator +(OilBarrel oilBarrel1, Bucket bucket)
        {
            oilBarrel1.FillBucket(bucket.BucketFilledAmount);
            bucket.EmptyBucket();
            return oilBarrel1;
        }
    }
}

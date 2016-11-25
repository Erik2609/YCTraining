namespace BucketApplication
{
    public class OilBarrel : Bucket
    {
        public OilBarrel() : base(159)
        {
            
        }

        public static OilBarrel operator +(OilBarrel oilBarrel1, Bucket bucket)
        {
			//JB: The implementation of the + operator is pretty much identical each time. Can't you think of a way to do so according to the principle of reusability?
            oilBarrel1.FillBucket(bucket.BucketFilledAmount); 
			bucket.EmptyBucket();
            return oilBarrel1;
        }
    }
}

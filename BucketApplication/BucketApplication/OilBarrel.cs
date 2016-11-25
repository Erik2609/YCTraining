namespace BucketApplication
{
    public class OilBarrel : Container
    {
        public OilBarrel() : base(159, null, null)
        {
            
        }

        public static OilBarrel operator +(OilBarrel oilBarrel1, Container container)
        {
			FillContainer1InContainer2(oilBarrel1, container);
            return oilBarrel1;
        }
    }
}

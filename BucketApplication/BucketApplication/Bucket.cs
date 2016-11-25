using System;

namespace BucketApplication
{

    public class Bucket : Container
    {
        #region Properties
        private const double DefaultBucketSize = 12.0;

        #endregion

        #region Constructors

        public Bucket(double size = DefaultBucketSize) : base(size, 10.0, null)
        {
        }

        #endregion

        #region Methods


        public static Bucket operator +(Bucket bucket1, Container container)
        {
            FillContainer1InContainer2(bucket1, container);
            return bucket1;
        }
        #endregion


    }
}

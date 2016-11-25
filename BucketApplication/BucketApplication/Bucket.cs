using System;

namespace BucketApplication
{
    public delegate void BucketOverflowDelegate(Bucket bucket, double amount);

    public class Bucket
    {
        #region Properties

        public double BucketMaxAmount { get; protected set; }

        private double _bucketFilledAmount;

        public double BucketFilledAmount
        {
            get { return _bucketFilledAmount; }
            private set
            {
                if (value <= BucketMaxAmount && value >= 0)
                {
                    _bucketFilledAmount = value;
                }
                else
                {
                    throw new NotSupportedException("The value exceeded the BucketMaxAmount, please put the logic in the FillBucket Method");
                }
            }
        }

        #endregion

        #region Constructors

        public Bucket()
        {
            this.BucketMaxAmount = 10.0;
        }

        public Bucket(double maximumSize)
        {
            if (maximumSize >= 10.0)
            {
                this.BucketMaxAmount = maximumSize;
            }
            else
            {
                Console.WriteLine("Minimum size of a bucket is 10");
                this.BucketMaxAmount = 10.0;
            }
        }

        #endregion

        #region Methods

        public void FillBucket(double fillAmount)
        {
            if (BucketFilledAmount + fillAmount < BucketMaxAmount)
            {
                BucketFilledAmount += fillAmount;
            }
            else
            {
                double spillAmount = fillAmount - (BucketMaxAmount - BucketFilledAmount);
                BucketFilledAmount = BucketMaxAmount;
                BucketOverflowEvent?.Invoke(this, spillAmount);
            }
        }

        public void EmptyBucket()
        {
            this.BucketFilledAmount = 0;
        }
        public void EmptyBucket(double amount)
        {
            if (amount > BucketFilledAmount)
            {
                this.BucketFilledAmount = 0;
            }
            else
            {
                this.BucketFilledAmount -= amount;
            }
        }

        public static Bucket operator +(Bucket bucket1, Bucket bucket2)
        {
            bucket1.FillBucket(bucket2.BucketFilledAmount);
            bucket2.EmptyBucket();
            return bucket1;
        }
        #endregion

        #region Events

        public event BucketOverflowDelegate BucketOverflowEvent;

        #endregion

    }
}

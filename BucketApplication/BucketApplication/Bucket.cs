using System;

namespace BucketApplication
{
    public delegate void BucketOverflowDelegate(Bucket bucket, double amount);

    public class Bucket
    {
		#region Properties

		public double BucketMaxAmount { get; protected set; } //JB: Should have a private set accessor. Furthermore, this is a terribly chosen name for the way you seem to be using this property.
		//First of all, it contains the letters 'Bucket'. That's redundant, because we're already in the type 'Bucket'.
		//Secondly, I read 'BucketMaxAmount' as the maximum amount of a bucket. This is subletly different from 'the maximum amount of this bucket'. 
		//For instance, the minimum size of any bucket is 10 and that could arguably be called 'BucketMinSize' or 'MinSize'. 
		//'BucketMinSize' or 'MinSize' in the .NET naming convention feels like it should be static/for all buckets. Please choose a different name
	

		private double _bucketFilledAmount;

        public double BucketFilledAmount
        {
            get { return _bucketFilledAmount; }
            private set
            {
                if (value <= BucketMaxAmount && value >= 0) //JB: A more readable and conventional way of writing this is 0 <= value && value <= BucketMaxAmount (which is akin to math: 0 <= value <= BucketMaxAmount)
				{
                    _bucketFilledAmount = value;
                }
                else
                {
					//JB: NotSupportedException is not the right type of Exception you want to throw here. See page 79 in the book
					throw new NotSupportedException("The value exceeded the BucketMaxAmount, please put the logic in the FillBucket Method");
                }
            }
        }

        #endregion

        #region Constructors

        public Bucket()
        {
            this.BucketMaxAmount = 10.0;
			//JB: Suppose the specifications change: Suddenly the maximum allowed contents of a bucket should be 11. 
			//Now you have to search for the literal 10.0 all over your code base which is kind of trivial here, but not even that trivial. 
			//If many people work on this code base, different people are going to have specified 10.0 differently. For instance, I'd write 10 rather than 10.0.
			//Let alone if this constant value is coincidentally used for another purpose. Then for each occurrence you have to decide whether you have to update it to 11 or not
			//It is therefore important that a constant is defined once, and referred to for each usage. 
			//That way, if the specs change, we can just update it once and be sure that everything is updated correctly
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

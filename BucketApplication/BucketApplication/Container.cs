using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketApplication
{
    public delegate void ContainerOverflowDelegate(Container bucket, double amount);

    public class Container
    {
        #region Properties
        
        private double _size;

        public double Size
        {
            get { return _size; }
            protected set { _size = value; }
        }


        private double _bucketFilledAmount;

        public double BucketFilledAmount
        {
            get { return _bucketFilledAmount; }
            private set
            {
                if (0 <= value && value <= Size)
                {
                    _bucketFilledAmount = value;
                }
                else
                {
                    throw new ArgumentException("The value exceeded the Size, please put the logic in the FillBucket Method");
                }
            }
        }
        #endregion

        #region Constructors
        public Container(double size, double? minimumSize, double? maximumSize)
        {
            if ((minimumSize == null || minimumSize <= size) && (maximumSize == null || size <= maximumSize))
            {
                this.Size = size;
            }
            else
            {
                throw new ArgumentException("Invalid input given");
            }
        }
        #endregion

        #region Methods

        public void FillBucket(double fillAmount)
        {
            if (BucketFilledAmount + fillAmount < Size)
            {
                BucketFilledAmount += fillAmount;
            }
            else
            {
                double spillAmount = fillAmount - (Size - BucketFilledAmount);
                BucketFilledAmount = Size;
                ContainerOverflowEvent?.Invoke(this, spillAmount);
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

        protected static void FillContainer1InContainer2(Container container1, Container container2)
        {
            container1.FillBucket(container2.BucketFilledAmount);
            container2.EmptyBucket();
        }

        #endregion


        #region Events

        public event ContainerOverflowDelegate ContainerOverflowEvent;

        #endregion
    }
}

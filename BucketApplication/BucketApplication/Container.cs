using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketApplication
{
    public delegate bool ShouldContainerOverflowDelegate(Container bucket);

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

        public double FillBucket(double fillAmount)
        {
            if (BucketFilledAmount + fillAmount < Size)
            {
                BucketFilledAmount += fillAmount;
                return 0.0;
            }
            else
            {
                double spillAmount = fillAmount - (Size - BucketFilledAmount);
                BucketFilledAmount = Size;
                bool? shouldSpill = ShouldContainerOverflowEvent?.Invoke(this);
                if (shouldSpill != null && !shouldSpill.Value)
                {
                    return spillAmount;
                }
                else
                {
                    ContainerOverflowEvent?.Invoke(this, spillAmount);
                    return 0;
                }
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

        protected static void FillContainer2InContainer1(Container container1, Container container2)
        {
            double restAmount = container1.FillBucket(container2.BucketFilledAmount);
            container2.EmptyBucket(container2.BucketFilledAmount - restAmount);
        }

        #endregion


        #region Events

        public event ShouldContainerOverflowDelegate ShouldContainerOverflowEvent;

        public event ContainerOverflowDelegate ContainerOverflowEvent;

        #endregion
    }
}

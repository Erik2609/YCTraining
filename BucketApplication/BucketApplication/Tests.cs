using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketApplication
{
    public static class Tests
	{
        public static void ManualTest()
        {
            var bucket1 = new Bucket();
            var bucket2 = new Bucket();

            Console.WriteLine("Fill Bucket 1 with size 12 with n liters:");
            double d1 = double.Parse(Console.ReadLine());

            Console.WriteLine("Fill Bucket 2 with size 12 n liters:");
            double d2 = double.Parse(Console.ReadLine());

            bucket1.FillBucket(d1);
            bucket2.FillBucket(d2);
            bucket1.ShouldContainerOverflowEvent += ShouldContainerOverflowEvent;
            bucket1.ContainerOverflowEvent += OnOverflowShouldSpillEvent;

            bucket1 += bucket2;

            Console.WriteLine($"Bucket1 current: {bucket1.BucketFilledAmount}, max size {bucket1.Size}");
            Console.WriteLine($"Bucket2 current: {bucket2.BucketFilledAmount}, max size {bucket2.Size}");
            Console.WriteLine();
        }

        private static bool ShouldContainerOverflowEvent(Container container)
        {
            Console.WriteLine($"Should the container of size: {container} overflow? Y|N?");
            if (Console.ReadLine()?.ToUpper() == "Y")
            {
                return true;
            }
            return false;
        }

        private static void OnOverflowShouldSpillEvent(Container bucket, double amount)
        {
            Console.WriteLine($"A container of size {bucket.Size} spilled {amount}");
        }

        public static bool TestTypes()
        {
            var rainBarrel1 = new RainBarrel(RainBarrel.Type.Large);
            var rainBarrel2 = new RainBarrel(RainBarrel.Type.Medium);

            rainBarrel1.FillBucket(100);
            rainBarrel2.FillBucket(100);

            rainBarrel1.ContainerOverflowEvent += OnContainerShouldOverflowEvent;

            Console.WriteLine("Expected output: " 
                + $"A bucket of size {rainBarrel1.Size}, overflowed 40");

            rainBarrel1 += rainBarrel2;

            if (rainBarrel1.BucketFilledAmount != 160 || rainBarrel1.Size != 160 ||
                rainBarrel2.BucketFilledAmount != 0 || rainBarrel2.Size != 120)
            {
                Console.WriteLine("Rainbarrels failed to be added correctly");
                return false;
            }
            
            Console.WriteLine("Rainbarrels added succesfully");
            Console.WriteLine();

            var beerGlass = new BeerGlass(0.5);
            var oilbarrel = new OilBarrel();
            oilbarrel.FillBucket(150);

            Console.WriteLine("Expected output: " 
                + $"A bucket of size {beerGlass.Size}, overflowed 149.5");
            beerGlass.ContainerOverflowEvent += OnContainerShouldOverflowEvent;
            beerGlass += oilbarrel;

            if (beerGlass.BucketFilledAmount != 0.5 || beerGlass.Size != 0.5 ||
                oilbarrel.BucketFilledAmount != 0 || oilbarrel.Size != 159)
            {
                Console.WriteLine("BeerGlass filled with OilBarrel Failed");
                return false;
            }
            Console.WriteLine("BeerGlass succesfully filled with OilBarrel");
            
            Console.WriteLine("Type tests were run succesfully");
            Console.WriteLine();
            return true;
        }

        public static bool TestCreateBucket()
        {
            var bucket1 = new Bucket();

            if (bucket1.Size != 12 || bucket1.BucketFilledAmount != 0)
            {
                Console.WriteLine("Bucket creation test 1 failed, unexpected outcome");
                return false;
            }

            Console.WriteLine("Expected output: " + "Minimum size of a container is 10");
            try
            {
                var bucket2 = new Bucket(5);
                Console.WriteLine("Bucket creation test 2 failed, unexpected outcome");
                return false;
            }
            catch (ArgumentException)
            {
            }

            var bucket3 = new Bucket(20);

            if (bucket3.Size != 20 || bucket3.BucketFilledAmount != 0)
            {
                Console.WriteLine("Bucket creation test 3 failed, unexpected outcome");
                return false;
            }

            Console.WriteLine("Bucket creation tests succes");
            Console.WriteLine();
            return true;

        }

        public static bool TestFillBucket()
        {
            Bucket bucket1 = new Bucket(40);
            bucket1.FillBucket(10);

            if (bucket1.Size != 40 || bucket1.BucketFilledAmount != 10)
            {
                Console.WriteLine("Bucket fill test 1 failed, unexpected outcome");
                return false;
            }

            Bucket bucket2 = new Bucket(20);
            bucket2.ContainerOverflowEvent += Bucket2OnContainerShouldOverflowEventTest;
            bucket2.FillBucket(30);

            if (bucket2.Size != 20 || bucket2.BucketFilledAmount != 20)
            {
                Console.WriteLine("Bucket fill test 2 failed, unexpected outcome");
                return false;
            }

            bucket1 += bucket2;
            if (bucket2.Size != 20 || bucket2.BucketFilledAmount != 0 || bucket1.Size != 40 || bucket1.BucketFilledAmount != 30)
            {
                Console.WriteLine("Bucket fill test 3 failed, unexpected outcome");
                return false;
            }

            bucket2 += bucket1;
            if (bucket2.Size != 20 || bucket2.BucketFilledAmount != 20 || bucket1.Size != 40 || bucket1.BucketFilledAmount != 0)
            {
                Console.WriteLine("Bucket fill test 4 failed, unexpected outcome");
                return false;
            }


            Console.WriteLine("Bucket fill tests succes");
            Console.WriteLine();
            return true;
        }

        public static bool Test3AddedBuckets()
        {
            var bucket1 = new Bucket(15);
            bucket1.ContainerOverflowEvent += OnContainerShouldOverflowEvent;
            var bucket2 = new Bucket(20);
            bucket2.ContainerOverflowEvent += OnContainerShouldOverflowEvent;
            var bucket3 = new Bucket(25);
            bucket3.ContainerOverflowEvent += OnContainerShouldOverflowEvent;

            bucket3.FillBucket(20);
            Console.WriteLine("Expected output: A container of size 15 overflowed 5");
            bucket1 = bucket1 + bucket2 + bucket3;

            if (bucket1.BucketFilledAmount != 15 || bucket2.BucketFilledAmount != 0 || bucket3.BucketFilledAmount != 0)
            {
                Console.WriteLine("Test3AddedBuckets test 1 failed");
                return false;
            }
            Console.WriteLine();

            bucket2.FillBucket(15);
            bucket3.FillBucket(15);
            Console.WriteLine("Expected output: A container of size 25 overflowed 5 \n and A container of size 25 overflowed 15");

            bucket3 = bucket3 + bucket2 + bucket1;
            if (bucket1.BucketFilledAmount != 0 || bucket2.BucketFilledAmount != 0 || bucket3.BucketFilledAmount != 25)
            {
                Console.WriteLine("Test3AddedBuckets test 2 failed");
                return false;
            }


            Console.WriteLine("Test3AddedBuckets tests succesful");
            Console.WriteLine();
            return true;
        }

        private static void OnContainerShouldOverflowEvent(Container container, double amount)
        {
            Console.WriteLine($"A container of size {container.Size}, overflowed {amount}");
        }


        private static void Bucket2OnContainerShouldOverflowEventTest(Container container, double amount)
        {
            if (amount != 10)
            {
                Console.WriteLine("Overflow test failed");
            }
            else
            {
                Console.WriteLine($"Overflow test succes, spilled amount = {amount}");
            }
        }
    }
}

using System;

namespace BucketApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            BucketUnitTest.TestCreateBucket();
            BucketUnitTest.TestFillBucket();
            BucketUnitTest.Test3AddedBuckets();
            BucketUnitTest.TestTypes();
            Console.ReadLine();
        }
    }
}

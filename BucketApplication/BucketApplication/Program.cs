using System;

namespace BucketApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Tests.TestCreateBucket();
            Tests.TestFillBucket();
            Tests.Test3AddedBuckets();
            Tests.TestTypes();
            Tests.ManualTest();
            Console.ReadLine();
        }
    }
}

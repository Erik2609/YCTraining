using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            BucketUnitTest.TestCreateBucket();
            BucketUnitTest.TestFillBucket();
            BucketUnitTest.Test3AddedBuckets();
            Console.ReadLine();
        }
    }
}

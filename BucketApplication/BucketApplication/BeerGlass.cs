using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketApplication
{
    public class BeerGlass : Container
    {
        /// <summary>
        /// Creates a BeerGlass of a size within 0.2 and 0.5 
        /// </summary>
        /// <param name="size">Should be within 0.2 and 0.5</param>
        public BeerGlass(double size) : base(size, 0.2, 0.5)
        {
        }

        public static BeerGlass operator +(BeerGlass beerGlass1, Container container)
        {
            FillContainer1InContainer2(beerGlass1, container);
            return beerGlass1;
        }
    }
}

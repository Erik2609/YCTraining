using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketApplication
{
    public class RainBarrel : Container
    {
        public enum Type
        {
            Small = 80,
            Medium = 120,
            Large = 160,
        };

        public RainBarrel(Type type) : base((int) type, 80, 160)
        {
            if (type != Type.Small && type != Type.Medium && type != Type.Large)
            {
                throw new ArgumentException("Invalid type");
            }
        }

        public static RainBarrel operator +(RainBarrel rainBarrel1, Container container)
        {
            FillContainer1InContainer2(rainBarrel1, container);
            return rainBarrel1;
        }
    }
}

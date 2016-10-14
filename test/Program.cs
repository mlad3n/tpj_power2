using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            Presentation ppt = new Presentation();
            ISlide slide = ppt.Slides[0];
            Console.WriteLine(ppt.Slides.GetType());
        }
    }
}

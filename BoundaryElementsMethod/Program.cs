using System;
using System.Diagnostics;
using System.Threading;

using BEM.Papers;

namespace BEM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            var sw = new Stopwatch();
            sw.Start();
           // var p = new PaperSpherePowExp();
             var p = new PaperTwoSphere();
            //  var p = new PaperNewtonMethod();
            //   var p = new PaperCollocation();
            //  var p = new Paper6Program();
            p.DoWork();
            Console.WriteLine("Total time: " + sw.ElapsedMilliseconds);
        }
    }
}
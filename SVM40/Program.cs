using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using SVMProject;

namespace SVM40
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] train = new double[,]{
                {175,140},
                {173,135},
                {160,130},
                {185,160},
                {165,110},
                {167,115},
                {160,100},
                {155,103}
            };

            double[] tlabel = new double[] { 1, 1, 1, 1, 2, 2, 2, 2 };

            double[,] test = new double[,]{
                {170,135},
                {165,95}
            };

            TimeSpan ts, ts1, ts2, ts3, ts4;



            ts1 = new TimeSpan(DateTime.Now.Ticks);
            SVMClass cc = new SVMClass();
            ts2 = new TimeSpan(DateTime.Now.Ticks);


            ts = ts1.Subtract(ts2).Duration();
            Console.WriteLine(ts.Days.ToString() + "天"
                        + ts.Hours.ToString() + "小时"
                        + ts.Minutes.ToString() + "分钟"
                        + ts.Seconds.ToString() + "秒");




            cc.SVM((MWNumericArray)train, (MWNumericArray)tlabel);
            ts3 = new TimeSpan(DateTime.Now.Ticks);

            ts = ts2.Subtract(ts3).Duration();
            Console.WriteLine(ts.Days.ToString() + "天"
                        + ts.Hours.ToString() + "小时"
                        + ts.Minutes.ToString() + "分钟"
                        + ts.Seconds.ToString() + "秒");


            MWNumericArray temp = (MWNumericArray)cc.SVMPredict((MWNumericArray)test);
            ts4 = new TimeSpan(DateTime.Now.Ticks);


            ts = ts3.Subtract(ts4).Duration();
            Console.WriteLine(ts.Days.ToString() + "天"
                        + ts.Hours.ToString() + "小时"
                        + ts.Minutes.ToString() + "分钟"
                        + ts.Seconds.ToString() + "秒");

            double[,] result = (double[,])temp.ToArray();
            Console.ReadLine();













        }
    }
}

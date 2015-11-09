using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using SVMProject;

namespace Microsoft.Samples.Kinect.ControlsBasics
{
    class MYSVM
    {
        public double[,] result;

        public void SVMmethod()
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

            SVMClass cc = new SVMClass();
            cc.SVM((MWNumericArray)train, (MWNumericArray)tlabel);

            MWNumericArray temp = (MWNumericArray)cc.SVMPredict((MWNumericArray)test);
           result = (double[,])temp.ToArray();

           





        }

    }
}

//------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.Samples.Kinect.ControlsBasics
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using Microsoft.Kinect;
    using Microsoft.Kinect.Toolkit;
    using Microsoft.Kinect.Toolkit.Controls;
    using System.Windows.Media.Imaging;
    using System.Windows.Media;
    using System.Threading;
    using System.Linq;
    using System.Windows.Shapes;
    using System.Windows.Threading;
    using System.Collections;
    using System.IO;
    using SVMProject;
    using MathWorks.MATLAB.NET.Arrays;
    using MathWorks.MATLAB.NET.Utility;
    using Visifire.Charts;
    using Visifire.Gauges;
    using people_walk;
    using System.Collections.Generic;




    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class MainWindow
    {

        private const double ScrollErrorMargin = 0.001;

        private const int PixelScrollByAmount = 20;

        private readonly KinectSensorChooser sensorChooser;

        private WriteableBitmap depthImageBitmap;
        //private Int32Rect colorImageBitmapRect;
        Thread depthimageThread = null;

       
        DispatcherTimer initThread = null;
      

      



        short[] depthdata = null;
        Skeleton[] skeletondata = null;

        Floor floor = new Floor();

        Queue dhip = new Queue(2);
        Queue vhip = new Queue(2);

        SVMClass cc = null;

        private string currentStateLebel = "若移动设备，请先初始化";


        // StreamWriter fs = new StreamWriter(@"F:\kanhudata\fall2.txt", true);

        DispatcherTimer falltimer = null;

        //=======================================

        Skeleton[] row_data = new Skeleton[5];
        normalVector save = null;//坐标对象
        List<int> total_data = new List<int>();//存储判定姿势结果的容器
        List<float> allSpeedInformation = new List<float>();//存储速度的容器

        DispatcherTimer calculateSpeed, zntimer;
        int i = 0, j = 0;
        bool isStart = false;//计时器是否启动
        float currentDistinationInformationZ = 0;//实时获取骨骼架Z轴坐标
        float currentDistinationInformationY = 0;//实时获取骨骼架Y轴坐标
        float currentDistinationInformationX = 0;//实时获取骨骼架X轴坐标
        public static Queue<float> calculateIntervalZ = new Queue<float>(2);//存骨骼架Z轴坐标
        public static Queue<float> calculateIntervalY = new Queue<float>(2);//存骨骼架Y轴坐标
        public static Queue<float> calculateIntervalX = new Queue<float>(2);//存骨骼架X轴坐标
        public Mychart mychart;//曲线的对象
        public Mychart mychart2;//指针的对象
        Mychart mychartSpeed;
        Mychart myPosturePieChart;
        Object lockAllObject = new Object();





        //======================================

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class. 
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            this.WindowState = System.Windows.WindowState.Maximized;
            this.WindowStyle = System.Windows.WindowStyle.None;
            // initialize the sensor chooser and UI

            Console.Write("SVM开始声明");
            cc = new SVMClass();
            Console.Write("SVM结束声明");

            //===========================
            chartSpeedStatistic.Opacity = 0;
            chartPostureStatistic.Opacity = 0;

            mychart = new Mychart(SpeedChart);
            mychart2 = new Mychart(gauge);
            mychart.CreateLineChart();
            mychart2.CreateGauge();
            //===========================


            this.sensorChooser = new KinectSensorChooser();
            this.sensorChooser.KinectChanged += SensorChooserOnKinectChanged;
            this.sensorChooserUi.KinectSensorChooser = this.sensorChooser;
            this.sensorChooser.Start();


            this.kinectRegion.KinectSensor = sensorChooser.Kinect;
            this.kinectRegion.IsCursorVisible = false;
            this.depthImageBitmap = new WriteableBitmap(640, 480, 96.0, 96.0, PixelFormats.Gray16, null);


            this.depthImage.Source = this.depthImageBitmap;
            this.Loaded += MainWindow_Loaded;


        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
           

        }


      

    
       

       

        private static void SensorChooserOnKinectChanged(object sender, KinectChangedEventArgs args)
        {
            if (args.OldSensor != null)
            {
                try
                {
                    args.OldSensor.DepthStream.Range = DepthRange.Default;
                    args.OldSensor.SkeletonStream.EnableTrackingInNearRange = false;
                    args.OldSensor.DepthStream.Disable();
                    args.OldSensor.SkeletonStream.Disable();
                }
                catch (InvalidOperationException)
                {
                    // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
                    // E.g.: sensor might be abruptly unplugged.
                }
            }

            if (args.NewSensor != null)
            {
                try
                {
                    args.NewSensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                    args.NewSensor.SkeletonStream.Enable();

                    try
                    {
                        args.NewSensor.DepthStream.Range = DepthRange.Near;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = true;
                    }
                    catch (InvalidOperationException)
                    {
                        // Non Kinect for Windows devices do not support Near mode, so reset back to default mode.
                        args.NewSensor.DepthStream.Range = DepthRange.Default;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = false;
                    }
                }
                catch (InvalidOperationException)
                {
                    // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
                    // E.g.: sensor might be abruptly unplugged.
                }

            }
        }



        private void menuclick(object sender, RoutedEventArgs e)
        {
            var button = (KinectTileButton)e.OriginalSource;
            switch (button.Name)
            {
                case "fall":
                    //  this.sensorChooser.Stop();
                    // falldetect fall = new falldetect(this.sensorChooser);
                    // fall.Show();
                    // this.Close();
                    this.fallgrid.Visibility = Visibility.Visible;
                    this.menugrid.Visibility = Visibility.Collapsed;
                    //this.kinectRegionGrid.Visibility = Visibility.Collapsed;
                    if (depthimageThread == null)
                    {
                        depthimageThread = new Thread(new ThreadStart(depthimage));
                        depthimageThread.Start();
                    }

                    // falldetect();
                    break;
                case "zhineng":
                    //===========================================================================
                    //if (zhinengThread == null)
                    //{
                    //    zhinengThread = new Thread(new ThreadStart(zh));
                    //    zhinengThread.Start();
                    //}

                    this.zhinenggrid.Visibility = Visibility.Visible;
                    this.menugrid.Visibility = Visibility.Collapsed;











                    //===========================================================================






                    // MessageBox.Show("2");
                    break;

            }



        }


        private void depthimage()
        {


            while (depthimageThread.ThreadState == ThreadState.Running)
            {
                depthdata = this.kinectRegion.DepthDataOutput();
                if (depthdata != null)
                {
                    this.depthImage.Dispatcher.Invoke(new Action(showdepthimage));


                }
                Thread.Sleep(50);//每秒20帧
            }

        }

        void showdepthimage()
        {
            this.depthImageBitmap.WritePixels(
                    new Int32Rect(0, 0, 640, 480),
                    depthdata,
                  640 * 2,
                    0);
        }

        private void close(object sender, RoutedEventArgs e)
        {
            if (this.sensorChooser != null)
            {
                if (this.sensorChooser.Kinect.IsRunning)
                {
                    this.sensorChooser.Stop();
                }
            }

            try
            {
                depthimageThread.Abort();
            }
            catch (Exception)
            {

            }




            this.Close();
            App.Current.Shutdown();

        }

        private void fallback(object sender, RoutedEventArgs e)
        {
            this.fallgrid.Visibility = Visibility.Hidden;
            this.menugrid.Visibility = Visibility.Visible;

            try
            {
                depthimageThread.Abort();

            }
            catch (Exception)
            {

            }


        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void KinectTileButton_Click(object sender, RoutedEventArgs e)
        {
            if (falltimer == null)
            {
                this.currentstate.Content = "已开启智能监测...";

                this.zhinengButton.Content = "关闭智能监测";
                //MyProperties mp = new MyProperties();
                //string path = System.Environment.CurrentDirectory + "\\floor.txt";
                //mp.SetPath(path);

                //floor.A = float.Parse(mp.ReadProperties("a"));
                //floor.B = float.Parse(mp.ReadProperties("b"));
                //floor.C = float.Parse(mp.ReadProperties("c"));
                //floor.D = float.Parse(mp.ReadProperties("d"));
                floor.A = Properties.Settings.Default.A;
                floor.B = Properties.Settings.Default.B;
                floor.C = Properties.Settings.Default.C;
                floor.D = Properties.Settings.Default.D;

                if (floor.A * floor.B * floor.C == 0)
                {
                    this.currentstate.Content = "请初始化...";
                }
                else
                {
                    falltimer = new DispatcherTimer();
                    falltimer.Interval = new TimeSpan(0, 0, 0, 0, 40);
                    falltimer.Tick += falltimer_Tick;
                    falltimer.Start();
                }

               
            }
            else
            {
                this.zhinengButton.Content = "开启智能监测";
                falltimer.Stop();
                falltimer.Tick -= falltimer_Tick;
                falltimer = null;
                this.currentstate.Content = "已关闭智能监测...";

            }



            //if (falldetect == null)
            //{
            //    falldetect = new Thread(new ThreadStart(fallcal));
            //    falldetect.Start();
            //}




        }


        void falltimer_Tick(object sender, EventArgs e)
        {



            this.skeletondata = kinectRegion.SkeletonDataOutput();


            if (this.skeletondata != null)
            {
                Skeleton currentSkeleton = this.skeletondata.Where(p => p.TrackingState == SkeletonTrackingState.Tracked).FirstOrDefault();
                if (currentSkeleton != null)
                {
                    Joint hipcenter = currentSkeleton.Joints[JointType.HipCenter];
                    float x = hipcenter.Position.X;
                    float y = hipcenter.Position.Y;
                    float z = hipcenter.Position.Z;

                    Joint shouldercenter = currentSkeleton.Joints[JointType.ShoulderCenter];
                    float xx = shouldercenter.Position.X;
                    float yy = shouldercenter.Position.Y;
                    float zz = shouldercenter.Position.Z;

                    float xxx = xx - x;
                    float yyy = yy - y;
                    float zzz = zz - z;


                    //  Console.WriteLine(hipcenter.Position.X+"||"+hipcenter.Position.Y+"||"+hipcenter.Position.Z);


                    float distance = (floor.A * x + floor.B * y + floor.C * z + floor.D);

                    float cos = (xxx * floor.A + yyy * floor.B + zzz * floor.C) / ((float)Math.Sqrt(xxx * xxx + yyy * yyy + zzz * zzz));

                    dhip.Enqueue(distance);

                    double result = 0;

                    if (dhip.Count == 2)
                    {
                        float d1 = (float)dhip.Dequeue();
                        float d2 = (float)dhip.Dequeue();
                        float v = Math.Abs((d1 - d2) * 30);

                        vhip.Enqueue(v);
                        //  Console.WriteLine(v + " " + d1);
                        //  fs.WriteLine(v + " " + d1 + " " + cos2);
                    }

                    if (vhip.Count == 2)
                    {
                        float a = Math.Abs(((float)vhip.Dequeue() - (float)vhip.Dequeue()) * 30);
                        // fs.WriteLine(a + " " + distance + " " + cos);
                        // Console.WriteLine(a + " " + distance + " " + cos);


                        //if (a > 100&distance<0.5)
                        //{
                        //    double[] test = new double[] { a, distance, cos };





                        //    MWNumericArray temp = (MWNumericArray)cc.SVMPredict((MWNumericArray)test);
                        //    double[,] tt = (double[,])temp.ToArray();
                        //    result=tt[0, 0];


                        //    Console.WriteLine(result);
                        //}


                        double[] test = new double[] { a, distance, cos };



                        MWNumericArray temp = (MWNumericArray)cc.SVMPredict((MWNumericArray)test);
                        double[,] tt = (double[,])temp.ToArray();
                        result = tt[0, 0];


                        Console.WriteLine(result + "-----------" + a + " " + distance + " " + cos);
























                    }
                }
            }





        }


        private delegate void DoTask();

        private void KinectTileButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.kinectRegion.KinectSensor = null;
            this.sensorChooser.Kinect.ElevationAngle += 3;
            //this.up.IsEnabled = false;
            //this.down.IsEnabled = false;

            //this.up.IsEnabled = true;
            //this.down.IsEnabled = true;
            //  Thread.Sleep(800);
            this.kinectRegion.KinectSensor = sensorChooser.Kinect;
            Properties.Settings.Default.A = 0;
            Properties.Settings.Default.B = 0;
            Properties.Settings.Default.C = 0;
            Properties.Settings.Default.D = 0;
            Properties.Settings.Default.Save();



        }


        private void down_Click(object sender, RoutedEventArgs e)
        {
            this.kinectRegion.KinectSensor = null;
            this.sensorChooser.Kinect.ElevationAngle -= 3;
            // Thread.Sleep(800);
            this.kinectRegion.KinectSensor = sensorChooser.Kinect;
            Properties.Settings.Default.A = 0;
            Properties.Settings.Default.B = 0;
            Properties.Settings.Default.C = 0;
            Properties.Settings.Default.D = 0;
            Properties.Settings.Default.Save();
        }

        private void init(object sender, RoutedEventArgs e)
        {

           
            initThread = new DispatcherTimer();
            initThread.Interval = new TimeSpan(0, 0, 0, 0, 20);
            initThread.Tick += initThread_Tick;
           

            try
            {
                initThread.Start();
                this.currentstate.Content = "正在检测环境....";
            }
            catch (Exception)
            {
                this.currentstate.Content = "初始化失败，请检查设备";
            }



        }

     

        void initThread_Tick(object sender, EventArgs e)
        {
           // Console.WriteLine(Properties.Settings.Default.A + "///" + Properties.Settings.Default.B);
                this.floor = kinectRegion.FloorDataOutput();
                if (floor.A * floor.B * floor.C != 0)
                {

                   
                    Properties.Settings.Default.A = floor.A;
                    Properties.Settings.Default.B = floor.B;
                    Properties.Settings.Default.C = floor.C;
                    Properties.Settings.Default.D = floor.D;
                    Properties.Settings.Default.Save();
                    Console.WriteLine(Properties.Settings.Default.A + "///" + Properties.Settings.Default.B);

                    //MyProperties mp = new MyProperties();
                    //string path = System.Environment.CurrentDirectory + "\\floor.txt";

                    //mp.SetPath(path);
                    //mp.clear();
                    //mp.WriteProperties("a=" + floor.A);
                    //mp.WriteProperties("b=" + floor.B);
                    //mp.WriteProperties("c=" + floor.C);
                    //mp.WriteProperties("d=" + floor.D);

                    this.currentstate.Content = "环境检测完毕，可正常运行";
                    initThread.Tick -= initThread_Tick;
                    initThread.Stop();
                   
                }

            



        }

      


        //=============================================

        void calculateSpeed_Tick(object sender, EventArgs e)
        {

            if (calculateIntervalZ.Count < 2 && calculateIntervalX.Count < 2 && calculateIntervalY.Count < 2)
            {
                calculateIntervalZ.Enqueue(currentDistinationInformationZ);
                calculateIntervalY.Enqueue(currentDistinationInformationY);
                calculateIntervalX.Enqueue(currentDistinationInformationX);

            }
            else if (calculateIntervalZ.Count == 2 && calculateIntervalX.Count == 2 && calculateIntervalY.Count == 2)
            {
                float BeforeX = calculateIntervalX.Dequeue();
                float BeforeY = calculateIntervalY.Dequeue();
                float BeforeZ = calculateIntervalZ.Dequeue();
                float NowX = calculateIntervalX.Peek();
                float NowY = calculateIntervalY.Peek();
                float NowZ = calculateIntervalZ.Peek();
                float distination = (float)Math.Sqrt((NowX - BeforeX) * (NowX - BeforeX) + (NowY - BeforeY) * (NowY - BeforeY) + (NowZ - BeforeZ) * (NowZ - BeforeZ));

                float Speed = distination / 0.05F;
                allSpeedInformation.Add(Speed);
                calculateIntervalZ.Enqueue(currentDistinationInformationZ);
                calculateIntervalY.Enqueue(currentDistinationInformationY);
                calculateIntervalX.Enqueue(currentDistinationInformationX);
                if (i++ == 10)
                {

                    showSpeed.Content = "Speed: " + Math.Round(Speed, 2) + "   m/s";
                    i = 0;
                    if (Speed <= 5)

                        mychart.UpdateSpeed(Speed);

                }
            }

        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            this.currentStateLebel = "正在测试中...";
            calculateSpeed = new DispatcherTimer();//计时器
            calculateSpeed.Interval = new TimeSpan(0, 0, 0, 0, 50);
            calculateSpeed.Tick += new EventHandler(calculateSpeed_Tick);



            //MyProperties mp = new MyProperties();
            //string path = System.Environment.CurrentDirectory + "\\floor.txt";
            //mp.SetPath(path);

            //floor.A = float.Parse(mp.ReadProperties("a"));
            //floor.B = float.Parse(mp.ReadProperties("b"));
            //floor.C = float.Parse(mp.ReadProperties("c"));
            //floor.D = float.Parse(mp.ReadProperties("d"));

            floor.A = Properties.Settings.Default.A;
            floor.B = Properties.Settings.Default.B;
            floor.C = Properties.Settings.Default.C;
            floor.D = Properties.Settings.Default.D;

            save = new normalVector(floor.A, floor.B, floor.C);
            save.Height = floor.D;
            zntimer = new DispatcherTimer();
            zntimer.Interval = new TimeSpan(0, 0, 0, 0, 35);
            zntimer.Tick += zn;
            zntimer.Start();



        }




        private void ShowFinalPostureReault(List<int> posture)
        {//速度最终结果函数
            int length = total_data.Capacity;
            int[] array = new int[length];
            float normal = 0, slight = 0, medium = 0, serious = 0, other = 0, total = 0;

            total_data.CopyTo(array);
            for (int i = 0; i < length; i++)
            {
                if (array[i] == 1 || array[i] == 2 || array[i] == 3 || array[i] == 4)
                {
                    switch (array[i])
                    {
                        case 1: normal++; break;
                        case 2: slight++; break;
                        case 3: medium++; break;
                        case 4: serious++; break;
                        default: other++; break;

                    }
                    total++;
                }

            }
            Dictionary<string, float> dt = new Dictionary<string, float>();
            dt.Add("正常", (float)Math.Round((normal / total), 2));
            dt.Add("轻微摇晃", (float)Math.Round((slight / total), 2));
            dt.Add("中度摇晃", (float)Math.Round((medium / total), 2));
            dt.Add("严重摇晃", (float)Math.Round((serious / total), 2));
            myPosturePieChart = new Mychart(chartPostureStatistic);
            string mystring = "人行走姿态统计图";
            myPosturePieChart.CreatePieChart(mystring, dt);


        }

        private void showAllSpeedFinalResult(List<float> Result)
        {//姿态最总结果函数
            int i;
            float[] speedArray = new float[Result.Capacity];
            Result.CopyTo(speedArray);
            float verySlow = 0, slightSlow = 0, normal = 0, fast = 0;
            float total = 0;


            for (i = 0; i < Result.Capacity; i++)
            {
                if (speedArray[i] <= 0.5)
                {
                    verySlow++;
                    total++;
                }
                else if (0.5 < speedArray[i] && speedArray[i] <= 1)
                {
                    slightSlow++;
                    total++;
                }
                else if (1 < speedArray[i] && speedArray[i] <= 1.5)
                {
                    normal++;
                    total++;
                }
                else if (1.5 < speedArray[i] && speedArray[i] <= 2)
                {
                    fast++;
                    total++;
                }
            }
            Dictionary<string, float> dt = new Dictionary<string, float>();
            dt.Add("非常慢", (float)Math.Round((verySlow / total), 2));
            dt.Add("稍慢", (float)Math.Round((slightSlow / total), 2));
            dt.Add("正常", (float)Math.Round((normal / total), 2));
            dt.Add("身手矫健", (float)Math.Round((fast / total), 2));
            string mystring = "行走速度统计图";
            mychartSpeed = new Mychart(chartSpeedStatistic);
            mychartSpeed.CreatePieChart(mystring, dt);

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.currentStateLebel = "测试已结束...";
            zntimer.Stop();
            calculateSpeed.Stop();
            ShowFinalPostureReault(total_data);
            showAllSpeedFinalResult(allSpeedInformation);

        }


        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            mychart.ResetSplineChart();
            mychartSpeed.removePieChartResult();
            myPosturePieChart.removePieChartResult();
            calculateIntervalX.Clear();
            calculateIntervalY.Clear();
            calculateIntervalZ.Clear();
            allSpeedInformation.Clear();
            total_data.Clear();
            isStart = false;
            chartSpeedStatistic.Opacity = 0;
            chartPostureStatistic.Opacity = 0;
            this.currentStateLebel = "测试已初始化,请继续测试...";
        }




        private void zn(object sender, EventArgs e)
        {



            Skeleton[] skeletons = new Skeleton[0];
            Judgement judgement = new Judgement();
            Skeleton[] skeletonFrame = kinectRegion.SkeletonDataOutput();
            if (skeletonFrame != null)
            {
                Skeleton currentSkeleton = skeletonFrame.Where(p => p.TrackingState == SkeletonTrackingState.Tracked).FirstOrDefault();
                if (currentSkeleton != null)
                {
                    lock (lockAllObject)
                    {
                        currentDistinationInformationZ = currentSkeleton.Joints[JointType.Spine].Position.Z;
                        currentDistinationInformationY = currentSkeleton.Joints[JointType.Spine].Position.Y;
                        currentDistinationInformationX = currentSkeleton.Joints[JointType.Spine].Position.X;
                    }
                    if (!isStart)
                    {
                        calculateSpeed.Start();
                        isStart = true;
                        calculateIntervalZ.Enqueue(currentDistinationInformationZ);
                        calculateIntervalY.Enqueue(currentDistinationInformationY);
                        calculateIntervalX.Enqueue(currentDistinationInformationX);
                    }
                    float result = judgement.Criterion(currentSkeleton, save);
                    total_data.Add(judgement.CompareAllResult(result));
                    if (j++ == 5)
                    {
                        mychart2.UpdateGauge(judgement.CompareAngle(result));
                        state.Content = judgement.CompareResult(result);
                        j = 0;
                    }

                }
            }






        }

        private void cursormode_Click(object sender, RoutedEventArgs e)
        {
            if (this.kinectRegion.IsCursorVisible)
            {

                this.kinectRegion.IsCursorVisible = false;
                this.currentStateLebel = "已打开鼠标模式";
                this.cursormode.Content = "鼠标模式";

            }
            else
            {
                this.kinectRegion.IsCursorVisible = true;
                this.currentStateLebel = "已打开感应模式";
                this.cursormode.Content = "感应模式";
            }
        }

        private void zhineng_back(object sender, RoutedEventArgs e)
        {
            try
            {
                zntimer.Stop();
            }
            catch (Exception)
            {

            }

            try
            {
                calculateSpeed.Stop();
            }
            catch (Exception)
            {

            }

            this.zhinenggrid.Visibility = Visibility.Hidden;
            this.menugrid.Visibility = Visibility.Visible;




        }



        //=============================================






    }
}

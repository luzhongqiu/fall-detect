using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Visifire.Charts;
using Visifire.Gauges;
using System.Windows.Media;
namespace people_walk
{
   public  class Mychart
    {
        Chart _chart;
        Gauge _gauge;
        Queue<float> SpeedData = new Queue<float>(15);
        NeedleIndicator circularNeedle;
        BarIndicator circularBar;
    
        public Mychart(Chart chart) {
           
            _chart = chart;
            
        }
        public Mychart(Gauge gauge) {
            _gauge = gauge;
        }
        
        public void CreateLineChart()
        {
            _chart.AnimationEnabled = false;//不绘制
            _chart.ScrollingEnabled = false;//不卷动
            _chart.Background = new SolidColorBrush(Colors.Black);//背景黑色

            Axis axisX = new Axis();          //new一个轴线
            ChartGrid grid = new ChartGrid(); //网格
            grid.LineColor = new SolidColorBrush(System.Windows.Media.Color.FromArgb((Byte)0xff, (Byte)0x00, (Byte)0x80, (Byte)0x40));//颜色
            grid.LineThickness = 1;           //线的厚度
            axisX.Grids.Add(grid);            //把网格添加到轴线
            Visifire.Charts.Ticks tickX = new Visifire.Charts.Ticks();        //new个十字叉
            tickX.Enabled = false;
            axisX.Ticks.Add(tickX);           //把十字叉加到轴
            _chart.AxesX.Add(axisX);//为图表添加X轴

            Axis axisY = new Axis();
            ChartGrid gridY = new ChartGrid();
            gridY.LineColor = new SolidColorBrush(System.Windows.Media.Color.FromArgb((Byte)0xff, (Byte)0x00, (Byte)0x80, (Byte)0x40));
            gridY.LineThickness = 1;
            axisY.Grids.Add(gridY);
            Visifire.Charts.Ticks tickY = new Visifire.Charts.Ticks();
            tickY.Enabled = true;
            axisY.Ticks.Add(tickY);
            _chart.AxesY.Add(axisY);//为图表添加Y轴

            _chart.Series.Add(new DataSeries() { RenderAs = RenderAs.Spline });//线形显示
            _chart.Series[0].Color = new SolidColorBrush(System.Windows.Media.Color.FromArgb((Byte)0xff, (Byte)0x00, (Byte)0xff, (Byte)0x00));
            for (int i = 0; i < 15; i++)//循环赋值
                _chart.Series[0].DataPoints.Add(new DataPoint());
        }
        public void  UpdateSpeed(float CurrentSpeed){
            if (SpeedData.Count < 15)
            {
                SpeedData.Enqueue(CurrentSpeed);
                float[] allSpeedData = new float[SpeedData.Count];
                allSpeedData = SpeedData.ToArray();
                for (int i = 0; i < SpeedData.Count; i++)
                {
                    _chart.Series[0].DataPoints[i].YValue = (double)allSpeedData[i];
                }
            }
            else if(SpeedData.Count == 15){
                SpeedData.Dequeue();
                SpeedData.Enqueue(CurrentSpeed);
                float[] allSpeedData = new float[SpeedData.Count];
                allSpeedData = SpeedData.ToArray();
                for (int i = 0; i < SpeedData.Count; i++)
                {
                    _chart.Series[0].DataPoints[i].YValue = allSpeedData[i];
                }
            }

          
        }
        public void CreateGauge()
        {
          
            _gauge.Type = GaugeTypes.Circular;//圆形
            circularNeedle = new NeedleIndicator();//指示针
            circularNeedle.Value = 0;//指示针的范围
            _gauge.Indicators.Add(circularNeedle);
            circularBar = new BarIndicator();      //指示条
            circularBar.Value = 0;
            _gauge.Indicators.Add(circularBar);
        }
        public void UpdateGauge(float value) {
        
            circularNeedle.Value = value;//指示针的范围
            circularBar.Value = value;

        }
        public void CreatePieChart(string mystring,Dictionary<string,float> data) {
            Title title = new Title();
            title.Text = mystring;
            _chart.Titles.Add(title);
            DataSeries ds = new DataSeries();
            ds.RenderAs = RenderAs.Pie;
            ds.LabelStyle = Visifire.Charts.LabelStyles.OutSide;
            _chart.Opacity = 1;
            foreach (KeyValuePair<string, float> a in data) {
                ds.DataPoints.Add(new DataPoint { AxisXLabel = a.Key, YValue = a.Value });
            }
            _chart.Series.Add(ds);
            
        }
       public void removePieChartResult(){
           _chart.Series.Clear();
           _chart.Titles.Clear();
       }
       public void ResetSplineChart() {
           for (int i = 0; i < 15; i++)
               _chart.Series[0].DataPoints[i].YValue = 0;
           SpeedData.Clear();
       }
    }
        

   }


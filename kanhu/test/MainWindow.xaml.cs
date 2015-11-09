using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using SVMProject;
using System.IO;
using System.Text.RegularExpressions;
using test;

namespace test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            MyProperties a = new MyProperties();
            a.SetPath("E:\\aaa.txt");
            //a.WriteProperties("a=12");
            a.WriteProperties("b=222222");
           // string aa=a.ReadProperties("a");
            //string asa = a.ReadProperties("a");
        }









       









    }
}

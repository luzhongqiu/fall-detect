   M S B A M L   `   `   `  ����M  IControlsBasics-WPF, Version=0.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:Microsoft.Samples.Kinect.ControlsBasics'Microsoft.Samples.Kinect.ControlsBasics  5   	   Y UWPFVisifire.Charts, Version=4.5.2.1, Culture=neutral, PublicKeyToken=0cd785983078370bM9clr-namespace:Visifire.Charts;assembly=WPFVisifire.ChartsVisifire.Charts 5   
   Y UWPFVisifire.Gauges, Version=4.5.2.1, Culture=neutral, PublicKeyToken=bf0af9b99ac47981M9clr-namespace:Visifire.Gauges;assembly=WPFVisifire.GaugesVisifire.Gauges 5	   	   8    2Microsoft.Samples.Kinect.ControlsBasics.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   \ XMicrosoft.Kinect.Toolkit.Controls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=nullS OMicrosoft.Kinect.Toolkit, Version=1.8.0.0, Culture=neutral, PublicKeyToken=null2k(http://schemas.microsoft.com/kinect/2013   5   	   <d2http://schemas.microsoft.com/expression/blend/2008   5   	   Fmc;http://schemas.openxmlformats.org/markup-compatibility/2006   5   	   Clocal5clr-namespace:Microsoft.Samples.Kinect.ControlsBasics   5   	   Dmy9clr-namespace:Visifire.Charts;assembly=WPFVisifire.Charts   5   
   Emy19clr-namespace:Visifire.Gauges;assembly=WPFVisifire.Gauges   5	   	   -   5        $��ControlsBasicsWindow��5   	     � Title$
  fall��5   	   $
��1366��5   	   $	��768��5   	   $��Segoe UI)�5   	   $
��1366��5   	   $	��768��5   	   .��5      � ��5   
   �� 5       �� ImageSource$ Images/bg.gif��6    j� Stretch$
 None=�65   .+�5   
   4  .Microsoft.Kinect.Toolkit.Controls.KinectRegion  -   5          Name$ kinectRegion��5      $��-10,0,10,10q�6/   .��5      � .+�5      � $	��100��6   $��10,10,-10,618q�6%   .+�5       �� $
��Left=�6   $��/Images/Logo.png��67   $��71��6S   $��	26,10,0,0q�6_   $	��Top=�6r   $	��139��6�   �� 5!      $��当前状态:��6   $
��Left=�6,   $��26,670,0,-640q�6G   $
��Left=�6^   $��Center=�6�   $��28(�6�   $��Center=�6�   $��70��6�   $	��134��6�   �� 5"      -   5        $��currentstate��5"      $'��!若移动设备，请先初始化��60   $
��Left=�6F   $��165,670,0,-640q�6a   $
��Left=�6y   $��Center=�6�   $��28(�6�   $��Center=�6�   $��70��6�   $	��850��6�   8  2Microsoft.Kinect.Toolkit.Controls.KinectUserViewer  5#      $��90��6j   $��Center=�6v   $	��Top=�6�   $��612,0,630,0q�6�   $	��116��6�      KinectRegion 6)   ��  �� ElementName$ kinectRegion��6�   4  .Microsoft.Kinect.Toolkit.KinectSensorChooserUI  5$      -   5        $��sensorChooserUi��5$   c   $��Center=�6.   $	��Top=�6K   6c   8  2Microsoft.Kinect.Toolkit.Controls.KinectTileButton  5%      -   5        $��	初始化��5%   )   $
��Left=�67   $��1067,10,0,0q�6R   $	��Top=�6g   $��71��6   $	��125��6�   ���	#FFD2C7AB6�     5&      -   5        $��
cursormode��5&   )   $��鼠标模式��6;   $
��Left=�6J   $��1197,10,0,0q�6e   $	��Top=�6z   $��71��6�   $	��125��6�   ���	#FFD2C7AB J� RenderTransformOrigin$ 
0.52,0.535&�6�   5'      � 5(      -   5         �Name$ kinectRegionGrid��5(      .+�5-      � -   5        $ menugrid��5-      .+�5.        -	   5        $
��fall��5.   -   $
��Left=�69   $��摔倒识别��6U   ���	#FFD2C7AB$	��200��6{   $	��400��6�   $��118,195,0,333q�6�   6�     5/      -
   5        $��zhineng��5/   -   $��Right=�6<   $��智能检测��6X   ���	#FFD2C7AB$	��200��6~   $	��400��6�   $��0,400,850,128q�6�   6�   50      � 54      -   5        $��fallgrid��54   /   $y�Hidden=�6   $	��480��6?   $��2��6L   $��	9,21,10,0q�6`   .+�55      �� -   5        $��
depthImage��55      $
��Left=�6    $	��480��6;   $��5,8,0,-8q�6H   $	��Top=�6Z   $	��640��6r   6   :  4Microsoft.Kinect.Toolkit.Controls.KinectCircleButton  56      -   5        $��返回��56   @   $
��Left=�6M   $��1238,-18,-7,0q�6h   $	��Top=�6   $	��115��6�   $	��108��6�     57      -   5        $��up��57   -   $	��上��67   $
��Left=�6C   $��
777,30,0,0q�6^   $	��Top=�6r   $��94��6�   $��91��6�   ���	#FFD2C7AB$��22(�6�   6�     58      -   5        $
��down��58   -   $	��下��69   $
��Left=�6E   $��
873,29,0,0q�6`   $	��Top=�6t   $��94��6�   $��91��6�   ���	#FFD2C7AB$��22(�6�   6�   �� 59      $��角度
调整��6    $
��Left=�69   $��
680,26,0,0q�6T   $	��Top=�6h   $	��101��6�   $��82��6�   $��35(�6�     5:      -   5        $��zhinengButton��5:   -   $��开启智能监测��6B   $
��Left=�6S   $��680,154,0,0q�6n   $	��Top=�6�   $��94��6�   $	��284��6�   ���	#FFD2C7AB$��22(�6�   $ -2.147,0.319&�6�   5<      � 5=      -   5        $��zhinenggrid��5=   0   $y�Hidden=�6   $��10,100,-10,-100q�6C   $��2��6\    � RowDefinitions 5?      �� 5@      $
��Auto �6,   6<   �� 5A      $	��403 �6,   6;   �� 5B      $	��169 �6,   6;   �� 5C      $
��Auto �6,   6=   �� 5D      $
��218* �6,   .+�5H        Visifire.Charts.Chart  -   5        $��
SpeedChart��5H   >   $
��Left=�6#   $	��Top=�6P   $	��225��6h   $	��779��6u   $��	10,20,0,0q�6�   &   Visifire.Commons.VisifireControl	   	Watermark	 .  6�   �� 5I      -   5        $��	showSpeed��5I   8   $��689,22,572,318q�6    $��3��6J   $�� 准备开始��6X   $��29��6h   
 J� Opacity$
 1Q�6t   ���	#DC1E1A1A���	#FF11BA116�     Visifire.Gauges.Gauge  5J      -   5        $��gauge��5J   ?   $
��Left=�6$   $	��238��6L   $��
2147483647��6X      CornerRadius$ 10��6n   $��794,18,0,122q�6�   $	��229��6�   �� 5K      -   5        $��state��5K   �   $��当前姿态��6    $��26��6/   $
��Left=�6;   $��871,143,0,0q�6V   $	��Top=�6k   $��90��6�   ���	#FFFBAD42$��Normal&�6�   $ 0.473,-1.52&�6�   ��5L      �� 5M   "    �� EndPoint$ 0.5,1&�66    �� 
StartPoint$ 0.5,0&�6G   .��5N   &   �  � Color$ 	#FF212125��63    � Offset$ 0Q�6E   � 5O   &   $ 	#FF2E2E35��63   $ 1Q�6E   5P   #   5R        5V      -   5        $��chartSpeedStatistic��5V   >   $
��Left=�6#   $	��Top=�6Y   $	��326��6q   $	��496��6~      View3D .  	 .  $��	5,266,0,0q�6�   $��2��6�     5W      -   5        $��chartPostureStatistic��5W   S   $
��Left=�6#   $��502,266,0,0q�6>   $	��Top=�6p   $	��326��6�   $	��499��6�   $��2��6�     5]      -   5        $��Start��5]   :   $��开始��6-   $	��130��6\   $��1164,39,64,200q�6i   $	��130��6�   $ 0.579,0.476&�6�   ���	#FFD2C7AB6�     5^      -   5        $��Restart��5^   :   $��清空��6-   $	��130��6`   $��1164,341,64,301q�6m   $	��130��6�   $ 0.579,0.476&�6�   ���	#FFD2C7AB$��2��6�     5_      -   5        $��
showresult��5_   <   $��显示结果��6-   $	��130��6e   $��1164,189,64,50q�6r   $	��130��6�   $ 0.579,0.476&�6�   ���	#FFD2C7AB6�   �� 5c      -   5        $��label1��5c   l   $��最后统计结果��6    $��28��61   $
��Left=�6=   $��
15,236,0,0q�6X   $	��Top=�6z   $	��282��6�     5e      -   5        $��返回��5e   1   $	��102��6@   $	��126��6M   $��1122,128,110,173q�6Y   $��1��6s   6�   5f      5g        5i      -   5        $��退出��5i   )   $
��Left=�66   $��1204,598,0,0q�6Q   $	��Top=�6g   $	��102��6   $	��126��6�   6�   5j      5k      5n      5o      
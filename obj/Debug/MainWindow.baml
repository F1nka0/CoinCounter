   M S B A M L   `   `   `  ����U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  5   	   E AArduinoApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ArduinoApp
ArduinoApp 5   	      ArduinoApp.MainWindow   5   	   2xctk)http://schemas.xceed.com/wpf/xaml/toolkit  W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5   	   *localclr-namespace:ArduinoApp     5   	     � Title$  
MainWindow��5	   	   $	��450��6   $	��800��6)   .��5
      � .+�5   
   �� ���	#FF767676$
��Left=�6.   $	��150��6I   .+�5      9� -   5        $
��exp1��5   0   $#��Доступные порты��6   .��5      �� -   5        $��ListOfPorts��5      60   5      �� 5      -   5         ��Name$ getData��5      .��6<   !Загрузить данные6N   �� 5      -   5        $ UpdatePorts��5      .��6D   ?=Обновить список доступных портов6f   �� 5      -   5        $��tbtest��5      $	��206��6$    �� TextWrapping$
 Wrap=�61   $��TextBox��6E   $	��120��6T   �� 5      $
��Wrap=�6   $��Бета v1.01 build 3��6,    J� RenderTransformOrigin$ 0.527,9.188&�6F   $��Bottom=�6j   5      #   LiveCharts.Wpf.CartesianChart  5   
   -   5          Name$ Graph��5      $��Right=�6,   $	��637��6H   &    LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$ Right=�6{      Series 6W   �� *SeriesCollection+   AxisY 5         LiveCharts.Wpf.Axis     Title$C =Количеств монет каждого номинала��6   	   Labels	 6D   �� *
YFormatter+6d   
   AxisX
 5        $ 
Время��6   	 6)   �� *Labels+6E   5      5"      5#      
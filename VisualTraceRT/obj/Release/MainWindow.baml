   M S B A M L   `   `   `  ���� H  DVisualTraceRT, Version=1.6.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:VisualTraceRTVisualTraceRT      VisualTraceRT.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:VisualTraceRT       � Title$  VisualTraceRT 1.6��$��724.5��$��927.5�� � WindowStartupLocation$ CenterScreen=�.��� .+�&   System.Windows.Controls.DataGrid  -     Name$ 	TraceGrid��$��
10,77,10,0q�   AutoGenerateColumns .     CanUserAddRows .    CanUserSortColumns .     
IsReadOnly . $	��245��$	��Top=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Distance��1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 �� *
Distance+  $ IP��
   Width5  /System.Windows.Controls.DataGridLengthConverter$	
 100 	 �� *IP+  $	 Geo��$	
 150 	 �� *Geo+  $ AS��$	
 150 	 �� *AS+  $	 ISP��$	
 140 	 �� *ISP+  $	 Org��$	
 140 	 �� *Org+  $ 	Delay(ms)��$	
 140 	 �� *Delay+�� -    ��Name$ Trace��$��TraceRT��$��62��$��598,10,10,0q�$	��Top=� �� 	IsDefault . {�.  �� -    ��Name$ targetIP��$
��Left=�$��40��$��	25,24,0,0q� �� TextWrapping$
 Wrap=�$��docs.microsoft.com��$	��Top=�$	��398��$��18(� � ToolTip$ IP / Domain Here��$��Center=�(  "System.Windows.Controls.WebBrowser  -     Name$ traceMap��$��
10,0,10,10q�$��Hidden=�$��Hidden=�$	��351��$��Bottom=� J� OpacityMask �	#FF2B2828�� -    ��Name$ IsCMD��$��Use Windows tracert��$
��Left=�$��
438,33,0,0q�$	��Top=�
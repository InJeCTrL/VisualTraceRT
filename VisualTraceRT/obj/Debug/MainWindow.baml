   M S B A M L   `   `   `  ����H  DVisualTraceRT, Version=1.6.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:VisualTraceRTVisualTraceRT  5   	       VisualTraceRT.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:VisualTraceRT     5   	     � Title$  VisualTraceRT 1.6��5   	   $��724.5��6#   $��927.5��62    � WindowStartupLocation$ CenterScreen=�6@   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ 	TraceGrid��5
      $��
10,77,10,0q�6&      AutoGenerateColumns .     CanUserAddRows .    CanUserSortColumns .     
IsReadOnly . $	��245��6�   $	��Top=�6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Distance��6%   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 67   �� *
Distance+  5      $ IP��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$	
 100 61   	 6=   �� *IP+  5      $	 Geo��6%   $	
 150 62   	 6>   �� *Geo+  5      $ AS��6%   $	
 150 61   	 6=   �� *AS+  5      $	 ISP��6%   $	
 140 62   	 6>   �� *ISP+  5      $	 Org��6%   $	
 140 62   	 6>   �� *Org+  5      $ 	Delay(ms)��6%   $	
 140 68   	 6D   �� *Delay+5      �� 5   
   -   5         ��Name$ Trace��5      $��TraceRT��6    $��62��62   $��598,10,10,0q�6>   $	��Top=�6S    �� 	IsDefault . {�.  6�   �� 5   
   -   5         ��Name$ targetIP��5      $
��Left=�6$   $��40��6?   $��	25,24,0,0q�6K    �� TextWrapping$
 Wrap=�6^   $��docs.microsoft.com��6r   $	��Top=�6�   $	��398��6�   $��18(�6�    � ToolTip$ IP / Domain Here��6�   $��Center=�6�   (  "System.Windows.Controls.WebBrowser  5   
   -   5          Name$ traceMap��5      $��
10,0,10,10q�6'   $��Hidden=�6;   $��Hidden=�6o   $	��351��6�   $��Bottom=�6�    J� OpacityMask �	#FF2B28286�   �� 5   
   -   5         ��Name$ IsCMD��5      $��Use Windows tracert��6"   $
��Left=�6@   $��
438,33,0,0q�6[   $	��Top=�6o   5      5      
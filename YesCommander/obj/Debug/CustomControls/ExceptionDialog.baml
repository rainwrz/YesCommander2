   M S B A M L   `   `   `  ����  YesCommander1    +YesCommander.CustomControls.ExceptionDialog   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � WindowStartupLocation$  CenterScreen=�5   	   $	��530��6.   $	��530��6=    J� SnapsToDevicePixels .  � SizeToContent$ Height=�5   	    � Title 5   I   ��  �� Path$ DialogTitle� �� Mode$ OneTime=�.��5      � -   5        $��
RootLayout��5      $��0q�6    � ColumnDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   � $��0��6   $��0��6   $��0q�6,   
�������.+�5      �� $��48��6L   $��48��6X   $��25,25,10,25q�5      $	��Top=�6)   ��5      �� $ DialogImageSource�$ OneTime=�5   )   5      � 5   
   $��1��6   $��0��6   $��0q�6,   
�������.+�5      �� $��0,0,10,0q�6M   $
��Wrap=�5      $��Center=�6,   $
��Left=�6G   ��5      �� $ ExceptionMessage�$ OneTime=�5   G   5      9� 5   
   -   5         9�Name$ expander��5   4   $��1��6   $��2��6    $��	10,0,10,0q�6F   ��5      �� $ ExpanderTitle�$ OneWay=���5      � 5      -   5        	 �Name$	 detailPanel��5   "   $��0q�6   .+�5      �� -   5        
 ��Name$
 
detailText��5      
������ $ �� HorizontalScrollBarVisibility$
 Auto=�5      " �� VerticalScrollBarVisibility$
 Auto=�6C    �� TextWrapping$ NoWrap=�5        �� 
IsReadOnly . ��5   2   �� $ ExceptionDetail�$ OneTime=���5      �� $ ActualWidth� �� ElementName$ detailPanel����5      �� $ ActualHeight�$ detailPanel��5    4   5!      5#      5$      5%      
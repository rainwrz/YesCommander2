   M S B A M L   `   `   `  ����   YesCommander1    +YesCommander.CustomControls.ExceptionDialog   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � WindowStartupLocation$  CenterScreen=�$	��530��$	��530�� J� SnapsToDevicePixels .  � SizeToContent$ Height=� � Title ��  �� Path$ DialogTitle� �� Mode$ OneTime=�.��� -   $��
RootLayout��$��0q� � ColumnDefinitions �� $
��Auto ���  � RowDefinitions �� $
��Auto ��� .+�� $��0��$��0��$��0q�
�������.+��� $��48��$��48��$��25,25,10,25q�$	��Top=����� $ DialogImageSource�$ OneTime=�� $��1��$��0��$��0q�
�������.+��� $��0,0,10,0q�$
��Wrap=�$��Center=�$
��Left=����� $ ExceptionMessage�$ OneTime=�9� -    9�Name$ expander��$��1��$��2��$��	10,0,10,0q����� $ ExpanderTitle�$ OneWay=���� -   	 �Name$	 detailPanel��$��0q�.+��� -   
 ��Name$
 
detailText��
������ $ �� HorizontalScrollBarVisibility$
 Auto=�" �� VerticalScrollBarVisibility$
 Auto=� �� TextWrapping$ NoWrap=� �� 
IsReadOnly . ���� $ ExceptionDetail�$ OneTime=����� $ ActualWidth� �� ElementName$ detailPanel������ $ ActualHeight�$ detailPanel��
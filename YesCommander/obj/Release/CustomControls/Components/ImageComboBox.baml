   M S B A M L   `   `   `  ���� G  CYesCommander, Version=1.6.7.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:YesCommander.CustomControls.Components&YesCommander.CustomControls.Components  ?$clr-namespace:YesCommander.ConverterYesCommander.Converter  :    4YesCommander.CustomControls.Components.ImageComboBox   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Flocal4clr-namespace:YesCommander.CustomControls.Components     :	converter$clr-namespace:YesCommander.Converter        CustomComboBoxStyle#���   $��Visible=�
������ $��30��  �� IsTextSearchEnabled  .   �� IsTextSearchCaseSensitive .   w� TextPath$ Id�� � 	Resources J  DYesCommander.Converter.IsImageComboBoxItemHighlightColorSetConverter 1 -IsImageComboBoxItemHighlightColorSetConverter��%   &	         ����  �� DataType>   8YesCommander.CustomControls.Components.ImageComboBoxItem"  .R�� ����  �� Path$ HighlightColor� �� Mode$ TwoWay=� � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� .+���  ��Name$ idText��$��0��$��Center=�$��2,0,4,0q�
�������$��13(����� $ Id�$ OneTime=��� $��1��$��0,0,4,0q�$��Center=�$
��None=����� $ Image�$ OneTime=��� $ nameText��$��2��$��CharacterEllipsis=�$��Center=�
�������$��13(����� $
 Name�$ OneTime=�	 �� Triggers	 �� 
 �� Value$

 True�� �� Binding �� $ HighlightColor�$ OneWay=� �� 	Converter# [ .a���  �� 
TargetName$ nameText�� �� Property � �� �� Value
 �����
   M S B A M L   `   `   `  ���� G  CYesCommander, Version=1.6.4.0, Culture=neutral, PublicKeyToken=null+clr-namespace:YesCommanderYesCommander  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   DownArrowGeometry < 8MetroThemeComboBoxToggleButtonMouseOverTextBoxBackground  �� Color : 6MetroThemeComboBoxToggleButtonPressedTextBoxBackground % !MetroThemeComboBoxEditableTextBox �� 
TargetType �� Property �� Value �� 
TargetType ��Name * &MetroThemeComboBoxReadonlyToggleButton J� SnapsToDevicePixels � ColumnDefinitions z� SharedSizeGroup	 �� Triggers
 P� Property P� Value �� 
TargetName  
MetroBrush # MetroHighlightedForegroundBrush P� 
SourceName  MetroHoverBrush ]� 
Conditions �� Property �� Value  ComboBoxFocusVisual �� StrokeDashArray 	 CustomComboBoxStyle 
 MetroBorderBrush  MetroPanelBackgroundBrush  MetroForegroundBrush d�Name �� 	ClickMode d� 	IsChecked �� Path �� Mode �� RelativeSource ��Name �� SelectionBoxItem �� SelectionBoxItemTemplate J� IsHitTestVisible ��Name �� 
IsReadOnly �� 
IsReadOnly �Name  �� IsDropDownOpen! � AllowsTransparency" ��Name# �Name$ �� MaxDropDownHeight% � ActualWidth& � RowDefinitions' $�Name( K� IsItemsHost) $� IsVirtualizing* $� VirtualizationMode+ �� Triggers, � ContextMenu  MetroContextMenuStyle- m� Command�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     $localclr-namespace:YesCommander  %�  &	        &	      &	 4     &	 K     &	 W    7   7  7  7  7  &	 (    &		 �    7  7  7  7  7  �M 0 0 L 3.5 4 L 7 0 Z��� $  	#FFFFFFFF���� $  Transparent���� " ��.���  �@�OverridesDefaultStyle . ��  �@J�	AllowDrop . ��  � ��$ 0����  � ��$ 0����  �@�FocusVisualStyle Z� ��  � �� �� " ��.R��� $ PART_ContentHost��|�.  $��Hidden=�$��Hidden=�
������ �� " d�.���  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � ��$ 1q���  �@��	ClickMode$ Press=���  � �� �� " d�.R��� $��Chrome1��#��z��#��z��#��z�� . ._��  �� $��* ��� $
��Auto �$ ComboBoxButton��.+��� $��Chrome��#��z��$��1,0,0,0q�$��0q�#��z��$��1��$��16��._�F� $��Arrow��
�������8��  $��Center=�$��Center=�	 P� 
 �@d�	IsChecked$
 TrueY�. ��� $ Chrome��# �   � ���� $ Chrome1��8   � ���� $ Arrow��# �   � ��P� $ Chrome1��
 �@J�IsMouseOver . . ��� $ Chrome��8   � ���� $ Chrome1��# �   � ��P� $ Chrome1��
 �@J�IsKeyboardFocusWithin . . ��� $ Chrome��# �   � ���� $ Chrome1��8   � ��]�  ��  �@d�	IsChecked$
 TrueY���  �@J�IsMouseOver . ./��� $ Chrome��# �   � ���� $ Chrome1��8   � ���� $ Arrow��# �   � ���� .���  � �� �� .R��� $��4,4,21,4q�$��1��
���   �$	 1 2R� . �� " ��.���  �@�FocusVisualStyle8   ��  � ��# � 
 ��  � ��# �  ��  � ��# �  ��  � ��$
 Auto=���  � ��$
 Auto=���  � ��$ 1q���  � ��$ 3,3,23,3q���  � �� �� " ��.R�� .+�d� $ ToggleButton��$��2��#��z��#��z��|�.  $ Press=�8��  �� $ IsDropDownOpen�$ TwoWay=� �� *TemplatedParent+�� $ ContentSite��$
��Left=�$��3,3,23,3q�$��Center=�#��z #��z #��z�� .  �� $ PART_EditableTextBox��$y�Hidden=�8�� $
��Left=�$��3,3,23,3q�$��Center=�|�. 
������ # z � $ Popup��#��z  $��Bottom=�|�.  ! . $��Slide=�.���� $" DropDownBorder��#��� 
 #���  $��1q�._�� $# DropDown��#��z$ #��z%  . & �� $��* ��� $
��Auto �.+��� $ PART_ScrollViewer��$��4,6,4,6q� . ��. $��Hidden=�#��z��.��$� $' ItemsPresenter��( . ) . $* 	Recycling=�# z 	 P� 
 �@��HasItems .  . ��� $ DropDownBorder�� � ��$ 95��P� 
 �@��
IsGrouping . . ���  � �� .  P� $ Popup��
 �@�AllowsTransparency . . ��� $ DropDownBorder�� � ��$ 0,0,0,0q�P� 
 �@��
IsEditable . . ���  � �� .  �� $ PART_EditableTextBox�� � y�$ Visible=��� $ ContentSite�� � y�$ Hidden=�]�  ��  �@��
IsEditable .  ��  � {� .  + P� 
 �@��
IsEditable . . ���  � ��$ 1q���  � ��# � 
 ��  � ��# �  ��  � ��# �  ��  � �� .  ��  � ��$ 3q���  � �� �� " ��.R�� .+�d� $ ToggleButton��$��2��#��z��#��z��$ Press=�8�� |�.   �� $ IsDropDownOpen�$ TwoWay=� �� *TemplatedParent+�� $ ContentSite��$
��Left=�$��3,3,23,3q�$��Center=�#��z #��z #��z�� .  �� $ PART_EditableTextBox��$y�Hidden=�8�� $
��Left=�$��3,3,23,3q�$��Center=�|�. 
������ # z .��, �� #���  .@�m� $	��Cut��$	- Cut��m� $
��Copy��$
- Copy��m� $��Paste��$- Paste��.��� $ Popup��#��z  $��Bottom=�|�.  ! . $��Slide=�.��� $# DropDown��#��z$ #��z%  . .+��� $" DropDownBorder��#��z��#��z��$��1q��� $��4,6,4,6q� . ��. #��z��#��z��.��$� $' ItemsPresenter��( . ) . $* 	Recycling=�# z 	 P� 
 �@J�IsKeyboardFocusWithin . . ��� $ ToggleButton��# �   � ��P� 
 �@��HasItems .  . ��� $ DropDownBorder�� � ��$ 95��P� 
 �@��
IsGrouping . . ���  � �� .  P� $ Popup��
 �@�AllowsTransparency . . ��� $ DropDownBorder�� � ��$ 0,0,0,0q�P� 
 �@��
IsEditable . . ���  � �� .  �� $ PART_EditableTextBox�� � y�$ Visible=��� $ ContentSite�� � y�$ Hidden=�]�  ��  �@��
IsEditable . ��  � {� .  
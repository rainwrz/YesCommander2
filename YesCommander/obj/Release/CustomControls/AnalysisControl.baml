   M S B A M L   `   `   `  ���� G  CYesCommander, Version=1.6.7.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:YesCommander.CustomControls.Components&YesCommander.CustomControls.Components  1    +YesCommander.CustomControls.AnalysisControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     P LElysium, Version=2.0.976.4, Culture=neutral, PublicKeyToken=afa220db249e5b15/metro#http://schemas.codeplex.com/elysium  7params*http://schemas.codeplex.com/elysium/params  E
components4clr-namespace:YesCommander.CustomControls.Components  -     �� BitmapScalingMode$  NearestNeighbor=�.���  � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� .+��� $��0�� �� Orientation$ 
Horizontal=�$��4q�$��Center=�.+��� -   $��missionWindowImage��$��/Resources/icon32.ico��$
��None=��� -   $��classAnalysis��$��职业分析��$��18(����#ffe8ce$��Bottom=�$��2q��� -   $��abilityAnalysis��$��技能分析��$��15(����#ffe8ce$��Bottom=�$��2q��� -   $��traitAnalysis��$��特长分析��$��15(����#ffe8ce$��Bottom=�$��2q��� -   $��raceAnalysis��$��种族分析��$��15(����#ffe8ce$��Bottom=�$��2q��� -   $��specIconPanel��$��1��$��0,8,0,4q�$ 
Horizontal=�$��Center=�$y�	Collapsed=��� -   $��classString��$��1��$��0,8,0,4q�$
��Wrap=�$��Center=� �� TextAlignment$ Center=����#ffe8ce$y�	Collapsed=��� -	   $��abilityCheckBoxPanel��$��2��$	��0,2q�$ 
Horizontal=�$��2��$��Center=�$��Center=�$y�Hidden=�.+�<   6YesCommander.CustomControls.Components.AbilityCheckBox                    �� -
   $��traitCheckBoxPanel��$��2��$	��0,2q�$ 
Horizontal=�$��Center=�$��Center=�$y�Hidden=��� -   $��raceRadioPanel��$��2��$	��0,2q�$ 
Horizontal=�$��Center=�$��Center=�$y�Hidden=� � 	Resources  �� 
TargetType �� Property �� Value��%�   '	�       '�� B     �� " 	�.���  � ��$ 8,0,0,0q���  � ��$ Center=��� " ��.���  � ��$ Uniform=���  � ��$ 18����  � ��$ 18����  � ��$ 2,0,4,0q�.+�	� -   $��侏儒���� 	� -   $��人类���� 	� -   $��矮人���� 	� -   $��暗夜精灵���� 	� -   $��	德莱尼���� 	� -   $��狼人���� 	� -   $��兽人���� 	� -   $��被遗忘者���� 	� -   $��	牛头人���� 	� -   $��巨魔���� 	� -   $��	血精灵���� 	� -   $��地精���� 	� -   $��	熊猫人���� 	� -   $��其他��$��其他��
����·��� �� -   $��
classPanel��$ 
Horizontal=�$��2��$��Center=�$��Center=� 6   0YesCommander.CustomControls.Components.TinyImage��%�   '��       '  C     �� " ��.���  � ��$ 10,4,0,4q���  � ��$ Center=��� "  .���  � ��$ 4,4,0,4q���  � ��$ 18����  � ��$ 18��.+�:   4YesCommander.CustomControls.Components.ImageComboBox  -     Name$ classComboBox��:   4YesCommander.CustomControls.Components.ImageComboBox	   SelectionChanged$	 ComboBox_SelectionChanged��$	��8,4q�$	��150��$
��Left=��� $C��=此天赋职业可以习得以下技能（任意俩组合）:���� -   $��abilityPanel��$ 
Horizontal=�$	��0,0q�$��Center=�.+�  -   
  Name$
 ability1��  -   $
 ability2��  -   $
 ability3��  -    $
 ability4��  -!   $
 ability5���� $��3��$��Center=�$ 
Horizontal=�.+��� -"   $��titleMy��$��麾下��$��22(�
����+��$��Bottom=�$��2q�$��Center=��� -#   $��titleAli��$��联盟��$��18(�
������$��Bottom=�$��2q�$��Center=��� -$   $��titleHrd��$��1��$��部落��$��18(�
���  ��$��Bottom=�$��2q�$��Center=��� $��4��$)��#/Resources/gear-bonus-divider_2.jpg��$
��None=�$	��150��$��0,0,0,4q�� -%   $��myScroll��$��5�� �� $
��auto ��� .+�� $��Center=�$	��892��$��-4,0,0,0q� ��%n   '��       �� " ��.���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��$ 4q� � ColumnDefinitions �� $��30 ��� $	��202 ��� $	��110 ��� $	��160 ��� $��40 ��� $��40 ��� $��* ��� $��2* ��� $
��1.5* ��� $��40 � �� �� .+��� $��偏好��$��2��$
��Left=��� $��1��$��姓名���� $��2��$��种族���� $��3��$��职业���� $��4��$��等级���� $��5��$��装等���� $��6��$��技能���� $��7��$��可能习得���� $��8��$��特长���� $��9��$��冻结���� $��1��$��Hidden=�
���   �$
��Auto=�$	��0,4q�.���� -&   $��myPanel��� -'   $��	aliScroll��$��5��$y�Hidden=� �� $
��auto ��� .+�� $	��952��$��Center=� �� $��32 ��� �� �� �� $��90 ��� $��40 ��� $��50 ��� $��50 ��� $��50 ��� $��50 � ��%\   '��       �� " ��.���  � ��$ 4q���  � ��$ 12(���  � ��$ Center=�.+��� $��	英文名��$��1���� $��	简体名��$��2���� $��	繁体名��$��3���� $��种族��$��4���� $��职业��$��5���� $��	始等级��$��6���� $��	始技能��$��7���� $��	始特长��$��8���� $��	已拥有��$��9���� $��1��$��Hidden=�
���   �$
��Auto=�$��0,4,0,8q�.���� -(   $��aliPanel��� -)   $��	hrdScroll��$��5��$y�Hidden=� �� $
��auto ��� .+�� $	��952��$��Center=� �� $��32 ��� �� �� �� $��90 ��� $��40 ��� $��50 ��� $��50 ��� $��50 ��� $��50 � ��%\   '��       �� " ��.���  � ��$ 4q���  � ��$ 12(���  � ��$ Center=�.+��� $��	英文名��$��1���� $��	简体名��$��2���� $��	繁体名��$��3���� $��种族��$��4���� $��职业��$��5���� $��	始等级��$��6���� $��	始技能��$��7���� $��	始特长��$��8���� $��	已拥有��$��9���� $��1��$��Hidden=�
���   �$
��Auto=�$��0,4,0,8q�.���� -*   $��hrdPanel��
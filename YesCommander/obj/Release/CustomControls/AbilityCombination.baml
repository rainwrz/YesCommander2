   M S B A M L   `   `   `  ���� G  CYesCommander, Version=1.6.7.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:YesCommander.CustomControls.Components&YesCommander.CustomControls.Components  4    .YesCommander.CustomControls.AbilityCombination   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     P LElysium, Version=2.0.976.4, Culture=neutral, PublicKeyToken=afa220db249e5b15/metro#http://schemas.codeplex.com/elysium  7params*http://schemas.codeplex.com/elysium/params  E
components4clr-namespace:YesCommander.CustomControls.Components  -     �� BitmapScalingMode$  NearestNeighbor=�.���  � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� .+��� $��0�� �� Orientation$ 
Horizontal=�$��4q�$��Center=�.+��� -   $��missionWindowImage��$��/Resources/icon32.ico��$
��None=��� -   $��existingFollower��$��检阅技能组合��$��18(����#ffe8ce$��Bottom=�$��2q��� $��1��$��4q�$ 
Horizontal=�$��Center=�.+��� $
��Wrap=�$��技能组合收藏度：��$��14(� �� TextAlignment$ Center=�
����+���� -   $
��rate��$��20/37��
��� � �$��14(�� -   $��myScroll��$��5�� �� $
��auto ��� .+�� $��Center=�$
��80,0q� � 	Resources  �� 
TargetType �� Property �� Value��%�   '��       �� " ��.���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��$ 4q���  � �� �#ffe8ce��  � ��$ 14(� � ColumnDefinitions �� $��30 ��� $��60 ��� $��80 ��� $��80 ��� $��80 ��� $��* ��� $��* � �� �� .+��� $��序号��$
��Left=�$��4,4,0,4q��� $��1��$��技能���� $��2��$��职业数量���� $��3��$��联盟随从数���� $��4��$��部落随从数���� $��5��$��	已收集���� $��6��$��潜力���� $��0q�$��1��$��Hidden=�
���   �$
��Auto=�.���� -   $��combinationPanel��
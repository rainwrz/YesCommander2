   M S B A M L   `   `   `  ���� G  CYesCommander, Version=1.6.4.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:YesCommander.CustomControlsYesCommander.CustomControls  _4clr-namespace:YesCommander.CustomControls.Components&YesCommander.CustomControls.Components      YesCommander.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     P LElysium, Version=2.0.976.4, Culture=neutral, PublicKeyToken=afa220db249e5b15/metro#http://schemas.codeplex.com/elysium  7params*http://schemas.codeplex.com/elysium/params  6custom)clr-namespace:YesCommander.CustomControls  D	component4clr-namespace:YesCommander.CustomControls.Components  -     �� BitmapScalingMode$  NearestNeighbor=� � Title$ YesCommander��$	��580��$
��1050��$	��580��$
��1020�� � 
ResizeMode$ CanResizeWithGrip=� � Icon$ /Resources/icon32.ico�� � WindowStartupLocation$ CenterScreen=�  Elysium.Parameters.Design   Theme$
 DarkX�   Elysium.Parameters.Manager   Theme$
 DarkX�.���  � RowDefinitions �� $
��auto ��� .+��� -   $��about��$��0��$
��10,0q�$��/Resources/help-icon.png��$
��None=�$��Right=���  �� Orientation$ 
Horizontal=�$	��4,0q�$
��Left=�.+�	� -   $��radioFollowers��$��随从一览��$��4q�$y�Hidden=�	� -   $��radioMissions��$��任务模拟��$��4q�$y�Hidden=�	� -   $��radioAnalysis��$��数据分析��$��4q�$y�Hidden=�	� -   $��radioCombination��$��检阅��$��4q�$y�Hidden=�� -   $��
questPanel��$��1��$��2��$y�Visible=�$��8,-8,0,8q� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� .+��� $��0��$ 
Horizontal=�$��4q�$��Center=�.+��� -   $��titleHighMaul��$��团队本任务��$��18(����#ffe8ce$��Bottom=�$��2q��� -	   $��	titleRing��$��橙戒任务��$��15(����#ffe8ce$��Bottom=�$��2q��� -
   $��	titleElse��$��其他三人任务��$��15(����#ffe8ce$��Bottom=�$��2q��� -   $��missionWindowImage��$��/Resources/icon32.ico��$
��None=��� -   $��maxNumberOfPartyComboBox��$��0��$��2��$��Bottom=�$	��200��$��30��$��0,4,60,0q�$��Right=��� $��1��$)��#/Resources/gear-bonus-divider_2.jpg��$
��None=��� $ 
Horizontal=�$��2��$��Center=�	 � 	Resources	 
 �� 
TargetType �� Property �� Value6   0YesCommander.CustomControls.Components.TinyImage��%�   '��       '  C     �� "
 ��.���  � ��$ 10,4,0,4q���  � ��$ Center=��� "
  .���  � ��$ 4,4,0,4q���  � ��$ 18����  � ��$ 18��.+�:   4YesCommander.CustomControls.Components.ImageComboBox  -     Name$ missionsComboBox��:   4YesCommander.CustomControls.Components.ImageComboBox   SelectionChanged$ ComboBox_SelectionChanged��$	��8,4q�$	��200��$
��Left=��� $��装等要求:���� -   $��textItemLevel��
���2�2��� $��任务时间:���� -   $��textMissionTime���� $��技能要求:���� -   $��abilityPanel��$ 
Horizontal=�$	��0,0q�$��Center=�.+�  -     Name$ ability1��  -   $ ability2��  -   $ ability3��  -   $ ability4��  -   $ ability5��  -   $ ability6��  -   $ ability7���� $��场景:��  -   $ trait���� $��奖励:���� -   $��
textReward��
���2�2��� $��基础胜率:���� -   $��textBasicChance���� $��备注:���� -   $��
textRemark��� -   $��	titleGrid��$��3��$��Center=�$
��Left=�$��4q�$y�Hidden=�����  �� ElementName$ 
questPanel�� �� Path$ ActualWidth�	 ��%n   '��       �� "
 ��.���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��$ 4q� � ColumnDefinitions �� $��50 ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��2.5* ��� $��* ��� $	��100 �.+��� $��	成功率���� -   $��checkboxMaxiLevel��$��1��$5��/使用675装等(不影响无装等要求任务)��$��Center=��� -   $��checkboxUsingFavorite��$��2��$��仅使用偏好随从��$��Center=�$	��4,0q��� $��3��$B��<注意：括号中的红色数字表示不满足装等要求��
���  ���� $��4��$��
团队buff��$��Center=��� $��5��$��缺少技能���� $��6��$��耗时���� $��5��$��Hidden=�
���   �$
��Auto=�$	��0.8q�.���� -   $��
partyPanel��$��4��$	��0,8q�$
��Left=����� $ 	titleGrid��$ Width�	 '   !YesCommander.CustomControls.Party��%2   '        �� "
  .���  � ��$ 4,4,0,4q�� -    $��followerPanel��$��1��$y�Hidden=�$��8,-8,0,8q� �� $
��auto ��� �� $
��auto �.+�� -!   $��
titleGrid2��$��0��$��4q�$y�Hidden=� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+��� $��0��$ 
Horizontal=�$��Center=�.+��� -"   $��titleAll��$��全部��$��22(����#ffe8ce$��Bottom=�$��2q��� -#   $��titleFavorite��$��偏好��$��18(����#ffe8ce$��Bottom=�$��2q��� -$   $��	titleShow��$��展示��$��18(����#ffe8ce$��Bottom=�$��2q��� $��1��$)��#/Resources/gear-bonus-divider_1.jpg��$
��None=�$	��140��� -%   $��suggestinoTitle��$��0,4,50,0q�$��2��$y�	Collapsed=�$	��800��$��Right=� �� ��  �� �� $
��1.2* ��� �� �� �� �� .+��� $��5��$��2��$ 
Horizontal=�.+��� -&   $��simulateButton��$��推荐偏好��$��80��$��30��{�.  $	��8,4q��� -'   $��simulateMyButton��$��计算概率��$��80��$��30��{�.  $	��8,4q��� -(   $��solutionComboBox��$��0��$��5��$��4q�$y�	Collapsed=��� -)   $��highMaulQuestCheckBox��$��悬槌堡任务��$��Center=�$��0���� -*   $��blackFoundryQuestCheckBox��$��黑石铸造厂任务��$��Center=�$��1���� -+   $��ringQuestStage1CheckBox��$��消磨之石任务��$��Center=�$��2���� -,   $��ringQuestStage2CheckBox��$��元素符文任务��$��Center=�$��3���� --   $��equipment645CheckBox��$��645装备任务��$��Center=�$��4���� -.   $��textHighmaulQuest��$��1��$��0��$
��Wrap=��� -/   $��textBlackFoundryQuest��$��1��$��1��$
��Wrap=��� -0   $��textQuestRing1��$��1��$��2��$
��Wrap=��� -1   $��textQuestRing2��$��1��$��3��$
��Wrap=��� -2   $��textEquipment645��$��1��$��4��$
��Wrap=�� -3   $��scrollTextTitle��$��3��$��Center=�$	��892��$��	-4,10,0,0q�	 ��%n   '��       �� "
 ��.���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��$ 4q� �� $��30 ��� $	��202 ��� $	��110 ��� $	��160 ��� $��40 ��� $��40 ��� $��* ��� $��2* ��� $
��1.5* ��� $��40 �.+��� $��偏好��$��2��$
��Left=��� $��1��$��姓名���� $��2��$��种族���� $��3��$��职业���� $��4��$��等级���� $��5��$��装等���� $��6��$��技能���� $��7��$��可能习得���� $��8��$��特长���� $��9��$��冻结���� -4   $��	allScroll��$��1��$��Hidden=�
���   �$��Visible=�$��4q�$	��Top=�.���� -5   $��followerRows��$ Vertical=�$��Center=�$	��900���� -6   $��favoriteScroll��$��1��$y�Hidden=�$��Hidden=�
���   �$��Visible=�$��4q�$	��Top=�.���� -7   $��favoriteRows��$ Vertical=�$��Center=�$	��900��� -8   $��showGrid��$��1��$y�Hidden=�$��	0,-10,0,0q� �� �� $
��auto ��� �� �� �� �� �� �� ��  �� �� 	 ��%-   '��       �� "
 ��.���  � ��$ 14(�.+��� -9   $��toCommanderText��$��0��$��2��$��30(�
����·�$"��Yes, Commander! 尊贵的：��$��Center=��� -:   $��achievementText��$��1��$��2��$��40(�
��� ���$��0,0,0,15q�$��Center=��� $��2�����#ffe8ce$��Right=�$��一共拥有随从数：���� $��3�����#ffe8ce$��Right=�$��史诗随从数：���� $��4�����#ffe8ce$��Right=�$��精良随从数：���� $��5�����#ffe8ce$��Right=�$��优秀随从数：���� $��6�����#ffe8ce$��Right=�$$��悬槌堡任务完成情况：���� $��7�����#ffe8ce$��Right=�$*��$黑石铸造厂任务完成情况：���� $��8�����#ffe8ce$��Right=�$'��!橙戒一阶任务完成情况：���� $��9�����#ffe8ce$��Right=�$'��!橙戒二阶任务完成情况：���� -;   $��totalFollowerText��$��2��$��1���� -<   $��epicFollowerText��$��3��$��1��
����+���� -=   $��blueFollowerText��$��4��$��1��
�������� ->   $��greenFollowerText��$��5��$��1��
��� � ��� -?   $��showHighmaulText��$��6��$��1���� -@   $��showBlackFoundryText��$��7��$��1���� -A   $��showRing1Text��$��8��$��1���� -B   $��showRing2Text��$��9��$��1���� $��2��$ 
Horizontal=�$��8q�$��Center=�.+��� -C   $��90��$��8q�$
��Read���� -D   $��90��$��8q�$��Input���� -E   $��followerImage��$��8q�$��/Resources/icon32.ico��$y�Hidden=�$
��None=�1   +YesCommander.CustomControls.AnalysisControl  -F     Name$ analysisPanel��$��1��$��8,-8,0,8q�$y�Hidden=�4   .YesCommander.CustomControls.AbilityCombination  -G     Name$ combinationPanel��$��1��$��8,-8,0,8q�$y�Hidden=�
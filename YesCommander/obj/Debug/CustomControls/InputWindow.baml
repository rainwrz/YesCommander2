   M S B A M L   `   `   `  ����G  CYesCommander, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:YesCommanderYesCommander  5   	   -    'YesCommander.CustomControls.InputWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   P LElysium, Version=2.0.976.4, Culture=neutral, PublicKeyToken=afa220db249e5b15/metro#http://schemas.codeplex.com/elysium  5   	   7params*http://schemas.codeplex.com/elysium/params  5   	   'customclr-namespace:YesCommander  5   	     �� BitmapScalingMode$  NearestNeighbor=�5   	    � Title$ Input��5   	   $	��380��6   $	��420��6$    � 
ResizeMode$ CanMinimize=�61    � Icon$ /Resources/icon32.ico��6J    � WindowStartupLocation$ CenterOwner=�5	   	     Elysium.Parameters.Design   Theme$ LightX�6-    � Topmost .     Elysium.Parameters.Manager   Theme$ LightX�5
   	   .��5      �  � RowDefinitions 5   
   �� 5      �� 5      $
��auto �6   .+�5   
   �� -   5        $��inputTextBox��5      $	��8,2q�6&   $	��Top=�63   $	 �� HorizontalScrollBarVisibility$	 Hidden=�6R   "
 �� VerticalScrollBarVisibility$

 Auto=�6y    �� AcceptsReturn .  �� TextWrapping$
 Wrap=�5   &   6:   �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   .+�5      �� -   5        $��okButton��5      $��30��6%   $��80��61   $��4q�6<   $��OK��6G   {�.  6k   �� 5      $��30��6   $��80��6!   $��4q�6,   $��Cancel��67    �� IsCancel . 6H   5      5      5      
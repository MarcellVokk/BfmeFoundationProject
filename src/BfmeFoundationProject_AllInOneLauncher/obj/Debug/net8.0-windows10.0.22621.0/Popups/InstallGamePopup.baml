   M S B A M L   `   `   `  ����K  GAllInOneLauncher, Version=2.0.2.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:AllInOneLauncher.PopupsAllInOneLauncher.Popups  5      E'clr-namespace:AllInOneLauncher.ElementsAllInOneLauncher.Elements  5      R Nnetstandard, Version=2.1.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd514)clr-namespace:System;assembly=netstandardSystem 5      U/clr-namespace:AllInOneLauncher.Elements.Generic!AllInOneLauncher.Elements.Generic  5	      .    (AllInOneLauncher.Popups.InstallGamePopup   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:AllInOneLauncher.Popups     5      <elements'clr-namespace:AllInOneLauncher.Elements     5      <system)clr-namespace:System;assembly=netstandard     5      Cgeneric/clr-namespace:AllInOneLauncher.Elements.Generic     5	      $	��700��5      $��Center=�6   $��Center=�67   .��5      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $��* �6   �� 5      $��20 �6   �� 5      $
��Auto �6   .+�5   
   �� -   5        $��TextTitleInstall��5      $��0��6,      InstallGamePopupTitle#���   69   
�������$��20(�6{   $��Medium"�6�   $
��Left=�6�   $��Center=�6�   6�   �� 5   
   $��1��6   
������� J� Opacity$	 0.2Q�6.   $��0,10,0,0q�6<   $	��Top=�6N   $��1��6f   � 5   
   $��2��6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��20 �6#   �� 5      $��* �6#   .+�5      � $��0��6   $��Center=�6#   $��Center=�6@   .+�5      �� $0��*/Resources/Images/BFME1LanguageSupport.png��6   $
��None=�6L   $	��450��6[   $	��270��6g    J� OpacityMask 5      !� 5       !� Visual 5       �� 5!   "   
���   � J� SnapsToDevicePixels$
 True��6<    �� CornerRadius 6W   �� *CornerRadius+ �� RelativeSource ��  �� AncestorType
 Border��6�   �� *ActualWidth+ �� 
 Border��6  �� *ActualHeight+ �� 
 Border5#      5%      5&      � 5'      $��2��6   .+�5(      �   5)      �� 5*      $
��Auto �6(   �� 5+      $��5 �6(   �� 5,      $
��Auto �6(   �� 5-      $��10 �6(   �� 5.      $
��Auto �6(   �� 5/      $��5 �6(   �� 50      $��* �6(   .+�52      �� $��0��6    $��14(�6-   $��SemiBold"�6;     InstallGamePopupLanguage#���  6Q   
�������6�   .   (AllInOneLauncher.Elements.DropdownPicker  53      -   5        	  Name$	 LanguageDropdown��53   ;   $��2��6.   $��Center=�6U   $��20(�6p   $��40��6~   
   
ColorStyle$

 Navy=�6�      Corners$ 4Q�6�      Options 55      ��English��65   ��56      
French��64   ��57      
German��64   ��58      Italian��65   ��59      Spanish��65   ��5:      Swedish��65   ��5;      	Dutch��63   ��5<      
Polish��64   ��5=      	Norwegian��67   5?      �� 5@      $��4��6    $��14(�6-   $��SemiBold"�6;     InstallGamePopupLocation#���  6Q   
�������6�   :   4AllInOneLauncher.Elements.Generic.SmoothScrollViewer  5A      $��6��61   .��5B      �� -   5         ��Name$ 	locations��5B   %   5C      5D      5E      5F      �� 5G   
   $��4��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   .+�5H      �� -   5         ��Name$ ButtonAccept��5H   2   $��0��6   $��2��6%     HiglightButton#��[ 6H   $��Center=�6p   $	��150��6�   $��0,0,10,0q�6�     GenericInstall#���  6�   �� 5I      -   5        $ ButtonCancel��5I   2   $��1��6   $��2��6%     GenericCancel#���  6H   $��Center=�6r   $	��150��6�   5J      5K      5L      
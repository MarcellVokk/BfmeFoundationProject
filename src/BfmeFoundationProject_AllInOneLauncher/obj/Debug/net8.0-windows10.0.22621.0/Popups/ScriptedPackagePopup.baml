   M S B A M L   `   `   `  ����K  GAllInOneLauncher, Version=2.0.2.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:AllInOneLauncher.PopupsAllInOneLauncher.Popups  5      E'clr-namespace:AllInOneLauncher.ElementsAllInOneLauncher.Elements  5      U/clr-namespace:AllInOneLauncher.Elements.Generic!AllInOneLauncher.Elements.Generic  5      2    ,AllInOneLauncher.Popups.ScriptedPackagePopup   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:AllInOneLauncher.Popups     5      <elements'clr-namespace:AllInOneLauncher.Elements     5      Cgeneric/clr-namespace:AllInOneLauncher.Elements.Generic     5      1   +AllInOneLauncher.Elements.Generic.PopupBody    Loaded$  OnLoad��5
   b   $	��700��6   $	��500��6   $��Center=�6*   $��Center=�6G   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��20 �6   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $��* �6   �� 5      $��30 �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��* �6#   �� 5      $
��Auto �6#   .+�5      �� $��0��6    �� CornerRadius$ 7��6%   ���	#1EFFFFFF$	��104��6M   $	��120��6Y   $	��Top=�6f    J� OpacityMask 5      !� 5       !� Visual 5      �� 5       J� SnapsToDevicePixels$
 True��6%   
���   � 6@   �� *CornerRadius+ �� RelativeSource ��  �� AncestorType
 Border��6�   �� *ActualWidth+ �� 
 Border��6�   �� *ActualHeight+ �� 
 Border6C  5!      ._�5#      �� -   5        	 ��Name$
	 icon��5#      $g��ahttps://workshop-files.bfmeladder.com/bdc6bc60-73f9-4833-9069-3076aff26418/official-0-artwork.png��6&   $��Center=�6�   $��UniformToFill=�6�   5$      �� 5%      $��1��6   $��20,0,0,0q�6)   $��Center=�6;   .+�5&      �� -   5        
 ��Name$
 title��5&      $��
Patch 2.22��6+   $��24(�6=   $��Black"�6K   
�������$	��Top=�6q   �� 5'      -   5        $
 author��5'      $��The 2.22 Team��6,   $��14(�6A   $��Medium"�6O   ���	#99FFFFFF$
��Left=�6z   $��0,5,0,0q�6�   5(      5)      �� 5*   
   $��2��6    $   ScriptedPackagePopupRequirements#���   6!   $��22(�6[   $��Black"�6i   
�������6|   �� 5+   
   $��3��6   
������� J� Opacity$	 0.2Q�6.   $��0,10,0,0q�6<   $	��Top=�6N   $��1��6f   :   4AllInOneLauncher.Elements.Generic.SmoothScrollViewer  5,   
   $��4��6%   .��5-      �� -   5         ��Name$ requirements��5-      .+�5.      �� $��23��6   $ 5��6%   ���	#19FFFFFF$��0,0,0,20q�6M   �� 5/      $��23��6   $ 5��6%   ���	#19FFFFFF$��0,0,0,20q�6M   �� 50      $��23��6   $ 5��6%   ���	#19FFFFFF$��0,0,0,20q�6M   51      52      �� 53   
   $��6��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   .+�54      �� -   5          GenericCancel#���  54      $��Center=�6?   $	��150��6r   55      56      57      
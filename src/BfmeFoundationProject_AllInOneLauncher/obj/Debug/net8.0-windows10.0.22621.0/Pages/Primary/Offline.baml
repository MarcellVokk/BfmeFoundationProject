   M S B A M L   `   `   `  ����K  GAllInOneLauncher, Version=2.0.2.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:AllInOneLauncher.ElementsAllInOneLauncher.Elements  5      a5clr-namespace:AllInOneLauncher.Pages.Subpages.Offline'AllInOneLauncher.Pages.Subpages.Offline  5      R Nnetstandard, Version=2.1.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd514)clr-namespace:System;assembly=netstandardSystem 5      U/clr-namespace:AllInOneLauncher.Elements.Generic!AllInOneLauncher.Elements.Generic  5	      U/clr-namespace:AllInOneLauncher.Elements.Library!AllInOneLauncher.Elements.Library  5
      U/clr-namespace:AllInOneLauncher.Elements.Offline!AllInOneLauncher.Elements.Offline  5      ,    &AllInOneLauncher.Pages.Primary.Offline   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <elements'clr-namespace:AllInOneLauncher.Elements     5      Jsubpages5clr-namespace:AllInOneLauncher.Pages.Subpages.Offline     5      <system)clr-namespace:System;assembly=netstandard     5      Cgeneric/clr-namespace:AllInOneLauncher.Elements.Generic     5	      Clibrary/clr-namespace:AllInOneLauncher.Elements.Library     5
      Coffline/clr-namespace:AllInOneLauncher.Elements.Offline     5      .��5      � .+�5   
   �   � RowDefinitions  5      �� 5      $
��Auto �6    �� 5      $��20 �6    �� 5      $��* �6    .+�5      -   'AllInOneLauncher.Elements.Generic.HTabs  -   5          Name$ gameTabs��5   )   -   'AllInOneLauncher.Elements.Generic.HTabs   SelectedIndexChanged$ 
TabChanged��6;   $��0��6      Tabs 5      ��  �� 	UriSource$7 1pack://application:,,,/Resources/Images/bfme1.png?�6"   �� 5      $7 1pack://application:,,,/Resources/Images/bfme2.png?�6"   �� 5      $7 1pack://application:,,,/Resources/Images/RotWK.png?�6"   5      � 5      $��2��6    � ColumnDefinitions 5      �� 5      $	��360 �6'   �� 5      $��25 �6'   �� 5       $��* �6'   .+�5"      � $��0��6     5#      �� 5$      $
��Auto �6(   �� 5%      $��15 �6(   �� 5&      $
��Auto �6(   �� 5'      $��15 �6(   �� 5(      $��* �6(   �� 5)      $��15 �6(   �� 5*      $
��Auto �6(   .+�5,      �� $��0��6    �� CornerRadius$ 15��6*    J� OpacityMask 5-      !� 5.       !� Visual 5/   "   �� 50   &   
���   �	 J� SnapsToDevicePixels$
	 True��6@    6[   �� *CornerRadius+
 �� RelativeSource
 ��  �� AncestorType
 Border��6�   �� *ActualWidth+
 �� 
 Border��6	  �� *ActualHeight+
 �� 
 Border52      ._�54      � .+�55      /   )AllInOneLauncher.Elements.Generic.Acrylic  �� 56      $��20q�6)   .+�57   "   �� -   5         ��Name$ 
titleImage��57   (   $��Center=�6<   $	��300��6Y   $	��150��6e    �� BitmapScalingMode$ HighQuality=�6r   58      59      5:      =   7AllInOneLauncher.Elements.Library.LibraryTileHorizontal  5;      -   5          Name$ activeEntry��5;   A   $��2��64   6A   �� 5<      $��4��6   $ 15��6*    5=      !� 5>       5?   "   �� 5@   &   
���   �$
	 True��6@    6[   �� *CornerRadius+
 �� 
 Border��6�   �� *ActualWidth+
 �� 
 Border��6	  �� *ActualHeight+
 �� 
 Border5B      ._�5D      � .+�5E        � 5F      $��5q�6#     5G   "   �� 5H   &   $
��Auto �64   �� 5I   &   $��10 �64   �� 5J   &   $��* �64   .+�5L   "   �� $��0��6,    "  OfflinePageEnabledEnhancements#���   69   
�������$��18(�6�   $��Black"�6�   $��
10,10,10,0q�6�   6�   :   4AllInOneLauncher.Elements.Generic.SmoothScrollViewer  5M   "   $��2��6=   .��5N   &   �� -   5         ��Name$ enabledEnhancements��5N   1   $��5q�6N   5O   #   �� 5P   "   -   5         ��Name$% activeEnhancementsNullIndicator��5P   9   $��2��6,   $��Center=�6b   $��Center=�6   ���	#1EFFFFFF # OfflinePageNoEnhancementsActive#���  6�   $��16(�6�   $��Medium"�6�   5Q      5R      5S      �� 5T      $��6��6   $ 15��6*    5U      !� 5V       5W   "   �� 5X   &   
���   �$
	 True��6@    6[   �� *CornerRadius+
 �� 
 Border��6�   �� *ActualWidth+
 �� 
 Border��6	  �� *ActualHeight+
 �� 
 Border5Z      ._�5\      � .+�5]        � 5^      $��10q�6#   .+�5_   "   �� $��Bottom=�6-   .+�5`   &   4   .AllInOneLauncher.Elements.Offline.LaunchButton  -   5          Name$ launchButton��5`   ;   4   .AllInOneLauncher.Elements.Offline.LaunchButton   OnLaunchClicked$ OnLaunchGameClicked��6b      OnInstallClicked$ OnInstallGameClicked��6�   $��0,0,0,0q�6Q   6�   5a   #   5b      5c      5d      5e      � 5f      $��2��6     5g      �� 5h      $��48.62 �6(   �� 5i      $��10 �6(   �� 5j      $��* �6(   .+�5l      �� $��0��6   $ 23��6*    J� Effect 5m      3  -System.Windows.Media.Effects.DropShadowEffect  5n         
BlurRadius$ 6Q�6/      Opacity$	 0.3Q�6>      Color$ Black��6L      ShadowDepth$ 2Q�6Z      	Direction$	 270Q�6j    5p      !� 5q       5r   "   �� 5s   &   
���   �$
	 True��6@    6[   �� *CornerRadius+
 �� 
 Border��6�   �� *ActualWidth+
 �� 
 Border��6	  �� *ActualHeight+
 �� 
 Border5u      ._�5w      � .+�5x        �� 5y      -   5        $ 	innerTabs��5y   )    �� Orientation$ 
Horizontal=�6<   $��5q�6U   .+�5z   "   �� -   5         ��Name$ 
libraryTab��5z   )   $��0,0,2,0q�6=   ���	#1EFFFFFF$ 18��6e    � Cursor$
 Hand��6w   ._�5{   &   ��   OfflinePageLibraryTab#���  60   $��14(�6_   $
��Bold"�6m   $��20,10,20,10q�6   
�������6�   5|   #   �� 5}   "   -	   5        $ workshopTab��5}   )   $��0,0,2,0q�6>   
������ $ 18��6h   $
 Hand��6z   ._�5~   &   ��   OfflinePageWorkshopTab#���  60   $��14(�6`   $
��Bold"�6n   $��20,10,20,10q�6�   
�������6�   5   #   5�      5�      5�      <	   6AllInOneLauncher.Pages.Subpages.Offline.OfflineLibrary	  5�      -
   5         	 Name$ library��5�   ;   $��2��6.   $y�Visible=�6b   =
   7AllInOneLauncher.Pages.Subpages.Offline.OfflineWorkshop
  5�      -   5         
 Name$ workshop��5�   <   $��2��6/   $y�Hidden=�6d   5�      5�      5�      5�      5�      
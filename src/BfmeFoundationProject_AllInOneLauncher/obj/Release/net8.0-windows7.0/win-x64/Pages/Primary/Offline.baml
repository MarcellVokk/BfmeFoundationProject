   M S B A M L   `   `   `  ���� K  GAllInOneLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullq=clr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements/BfmeFoundationProject.AllInOneLauncher.Elements  �Kclr-namespace:BfmeFoundationProject.AllInOneLauncher.Pages.Subpages.Offline=BfmeFoundationProject.AllInOneLauncher.Pages.Subpages.Offline  R Nnetstandard, Version=2.1.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd514)clr-namespace:System;assembly=netstandardSystem �Eclr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements.Generic7BfmeFoundationProject.AllInOneLauncher.Elements.Generic  �Eclr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements.Library7BfmeFoundationProject.AllInOneLauncher.Elements.Library  �Eclr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements.Offline7BfmeFoundationProject.AllInOneLauncher.Elements.Offline  B    <BfmeFoundationProject.AllInOneLauncher.Pages.Primary.Offline   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Relements=clr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements     `subpagesKclr-namespace:BfmeFoundationProject.AllInOneLauncher.Pages.Subpages.Offline     <system)clr-namespace:System;assembly=netstandard     YgenericEclr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements.Generic     YlibraryEclr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements.Library     YofflineEclr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements.Offline     .��� .+��   � RowDefinitions  �� $
��Auto ��� $��20 ��� $��* �.+�C   =BfmeFoundationProject.AllInOneLauncher.Elements.Generic.HTabs  -     Name$ gameTabs��C   =BfmeFoundationProject.AllInOneLauncher.Elements.Generic.HTabs   SelectedIndexChanged$ 
TabChanged��$��0��   Tabs ��  �� 	UriSource$7 1pack://application:,,,/Resources/Images/bfme1.png?��� $7 1pack://application:,,,/Resources/Images/bfme2.png?��� $7 1pack://application:,,,/Resources/Images/RotWK.png?�� $��2�� � ColumnDefinitions �� $	��360 ��� $��25 ��� $��* �.+�� $��0��  �� $
��Auto ��� $��15 ��� $
��Auto ��� $��15 ��� $��* ��� $��15 ��� $
��Auto �.+��� $��0�� �� CornerRadius$ 15�� J� OpacityMask !�  !� Visual �� 
���   �	 J� SnapsToDevicePixels$
	 True�� �� *CornerRadius+
 �� RelativeSource
 ��  �� AncestorType
 Border���� *ActualWidth+
 �� 
 Border���� *ActualHeight+
 �� 
 Border._�� .+�E   ?BfmeFoundationProject.AllInOneLauncher.Elements.Generic.Acrylic  �� $��20q�.+��� -    ��Name$ 
titleImage��$��Center=�$	��300��$	��150�� �� BitmapScalingMode$ HighQuality=�S   MBfmeFoundationProject.AllInOneLauncher.Elements.Library.LibraryTileHorizontal  -     Name$ activeEntry��$��2���� $��4��$ 15�� !�  �� 
���   �$
	 True�� �� *CornerRadius+
 �� 
 Border���� *ActualWidth+
 �� 
 Border���� *ActualHeight+
 �� 
 Border._�� .+�  � $��5q�  �� $
��Auto ��� $��10 ��� $��* �.+��� $��0�� "  OfflinePageEnabledEnhancements#���   
�������$��18(�$��Black"�$��
10,10,10,0q�P   JBfmeFoundationProject.AllInOneLauncher.Elements.Generic.SmoothScrollViewer  $��2��.���� -    ��Name$ enabledEnhancements��$��5q��� -    ��Name$% activeEnhancementsNullIndicator��$��2��$��Center=�$��Center=����	#1EFFFFFF # OfflinePageNoEnhancementsActive#���  $��16(�$��Medium"��� $��6��$ 15�� !�  �� 
���   �$
	 True�� �� *CornerRadius+
 �� 
 Border���� *ActualWidth+
 �� 
 Border���� *ActualHeight+
 �� 
 Border._�� .+�  � $��10q�.+��� $��Bottom=�.+�J   DBfmeFoundationProject.AllInOneLauncher.Elements.Offline.LaunchButton  -     Name$ launchButton��J   DBfmeFoundationProject.AllInOneLauncher.Elements.Offline.LaunchButton   OnLaunchClicked$ OnLaunchGameClicked��   OnInstallClicked$ OnInstallGameClicked��$��0,0,0,0q�� $��2��  �� $��48.62 ��� $��10 ��� $��* �.+��� $��0��$ 23�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 6Q�   Opacity$	 0.3Q�   Color$ Black��   ShadowDepth$ 2Q�   	Direction$	 270Q� !�  �� 
���   �$
	 True�� �� *CornerRadius+
 �� 
 Border���� *ActualWidth+
 �� 
 Border���� *ActualHeight+
 �� 
 Border._�� .+�  �� -   $ 	innerTabs�� �� Orientation$ 
Horizontal=�$��5q�.+��� -    ��Name$ 
libraryTab��$��0,0,2,0q����	#1EFFFFFF$ 18�� � Cursor$
 Hand��._���   OfflinePageLibraryTab#���  $��14(�$
��Bold"�$��20,10,20,10q�
��������� -	   $ workshopTab��$��0,0,2,0q�
������ $ 18��$
 Hand��._���   OfflinePageWorkshopTab#���  $��14(�$
��Bold"�$��20,10,20,10q�
�������R	   LBfmeFoundationProject.AllInOneLauncher.Pages.Subpages.Offline.OfflineLibrary	  -
    	 Name$ library��$��2��$y�Visible=�S
   MBfmeFoundationProject.AllInOneLauncher.Pages.Subpages.Offline.OfflineWorkshop
  -    
 Name$ workshop��$��2��$y�Hidden=�
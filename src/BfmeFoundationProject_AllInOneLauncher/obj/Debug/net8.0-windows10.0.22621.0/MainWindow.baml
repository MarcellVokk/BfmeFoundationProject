   M S B A M L   `   `   `  ����K  GAllInOneLauncher, Version=2.0.2.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:AllInOneLauncher.ElementsAllInOneLauncher.Elements  5   	   U/clr-namespace:AllInOneLauncher.Elements.Generic!AllInOneLauncher.Elements.Generic  5   	   !    AllInOneLauncher.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [Hardcodet.NotifyIcon.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=682384a853a08aad)tb http://www.hardcodet.net/taskbar  5   	   6elements'clr-namespace:AllInOneLauncher.Elements  5   	   =generic/clr-namespace:AllInOneLauncher.Elements.Generic  5   	   -   5          �Name$  LauncherMainWindow��5   	    � Title$ All in One Bfme Launcher��5
   	   $��14(�6*    � WindowStartupLocation$ CenterScreen=�68    � WindowStyle$ ThreeDBorderWindow=�6y   $y�Visible=�6�    J� SnapsToDevicePixels$
 True��6�    �� BitmapScalingMode$ HighQuality=�6�   .��5      � -   5         �Name$ 
windowGrid��5      ��5   
   �� 5       �� ImageSource$7 1pack://application:,,,/Resources/Images/BGMap.png��6   .+�5   
   �� -   5         ��Name$ 
background��5      $7��1pack://application:,,,/Resources/Images/BGMap.png��6$   $
��Fill=�6_   3  -Hardcodet.Wpf.TaskbarNotification.TaskbarIcon  5   
   -   5        	  Name$	 TrayIcon��5      
   
IconSource$)
 #pack://application:,,,/MainIcon.ico��6+      ToolTipText$ Patch 2.22 Launcher��6\      MenuActivation$ 
RightClick=�6~   6�   � 5   
   -   5        $ outerContent��5      $��20q�6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��20 �6    �� 5      $��* �6    .+�5      � $��0��6   .+�5      �� -   5         ��Name$
 tabs��5       �� Orientation$ 
Horizontal=�6+   $��Center=�6D   .+�5      �� -   5         ��Name$ 
offlineTab��5          TextBlockHover#���   64     MainWindowSingleplayerTab#���  6]   $��16(�6�   $
��Bold"�6�   $��0,0,20,0q�6�   
������  � Cursor$
 Hand��6�   6�   �� 5      -   5        $ 	onlineTab��5       #���   63     MainWindowMultiplayerTab#���  6\   $��16(�6�   $
��Bold"�6�   $��0,0,0,0q�6�   
������ $
 Hand��6�   6�   �� 5      -	   5        $ 	guidesTab��5       #���   63     MainWindowGuidesTab#���  6\   $��16(�6�   $
��Bold"�6�   $��20,0,0,0q�6�   
������ $
 Hand��6�   6�   �� 5      -
   5        $ aboutTab��5       #���   62   $��ABOUT��6[   $��16(�6h   $
��Bold"�6v   $��20,0,0,0q�6�   
������ $
 Hand��6�   6�   5      �� 5      -   5        $ icons��5      $ 
Horizontal=�6,   $��Right=�6E   .+�5      �� -   5        
 � Tag$. (https://www.youtube.com/@BeyondStandards��5      $9��3pack://application:,,,/Resources/Images/youtube.png��6K   $��32��6�   $��0,0,5,0q�6�   $
 Hand��6�   6�   �� 5       -   5        $+ %https://www.twitch.tv/beyondstandards��5       $8��2pack://application:,,,/Resources/Images/twitch.png��6H   $��32��6�   $��0,0,5,0q�6�   $
 Hand��6�   6�   �� 5!      -   5        $+ %https://discord.com/invite/Q5Yyy3XCuu��5!      $9��3pack://application:,,,/Resources/Images/discord.png��6H   $��32��6�   $��0,0,5,0q�6�   $
 Hand��6�   6�   �� 5"      -   5        $B <https://www.moddb.com/mods/battle-for-middle-earth-patch-222��5"      $7��1pack://application:,,,/Resources/Images/moddb.png��6_   $��32��6�   $��0,0,5,0q�6�   $
 Hand��6�   6�   �� 5#      -   5        $1 +https://github.com/MarcellVokk/aio-launcher��5#      $8��2pack://application:,,,/Resources/Images/github.png��6N   $��32��6�   $��0,0,5,0q�6�   $
 Hand��6�   6�   �� 5$      -   5        $, &https://www.patreon.com/bfmefoundation��5$      $9��3pack://application:,,,/Resources/Images/patreon.png��6I   $��32��6�   $��0,0,5,0q�6�   $
 Hand��6�   6�   � 5%      -   5        $ settingsIcon��5%      
������ $
 Hand��6J   $��Right=�6X   .+�5&      �� -   5        $ color��5&   $   $y�	Collapsed=�63   F� 5'       �� Stretch$ Uniform=�6S   $��32��6e   �����H�� � S 5� dfffffֿ�x�&1@dfffffֿ�z�Ga@� z�G��@>
ףp=�?����(\�@��" ��V �V �Q���Q@�� �K7�A`@�����M�?�����M�?ȭ* V9��v� @8�A`�P@}j�t�@���(\�@B�l���@أp=
�@F�z�G@��C��@:
ףp=�?|�G��@�������?�z�Ga@tfffffֿ�x�&1@tfffffֿ 5� �������?���(\�"@�:
ףp=�?�� }j�t�@���Q8'@��V �V !��~j� @أp=
�'@�����M�?-���g*@�K7�A`@��ʡE6-@6�A`�P@���Mb�+@���(\�@a��"�y+@z�G��@���Q8-@��V �V �z�Ga@gffff�-@�x�&1@�����Y0@ 5� �����Y0@H�� gffff�-@��(\�#@���Q8-@W�'� a��"�y+@��V �V Q�� 8�A`��+@S8v� ��ʡE6-@��� -���g*@84� e;�O��'@Q�� ���Q8'@�P�� �� ��V �V Qp� ���(\�"@������Y0@ 5� �����Y0@�x�&1@p� �z�Ga@QP�� |�G��@W�� ���(\�@��V �V Q 8� S���Q@S��� �K7�A`@8v� �����M�?h� Z9��v� @Q�'� �j�t�@��(\�#@B
ףp=�?��V �V QH�� �������?� z PǦ W z G�z�G@�h�, h�, W z A`��"�%@�h�, h�, 	��6   ��  �� ElementName$ color�� �� Path$ 
Foreground�6p   ��5(      �� 5)       �� 
TargetType" �6$   .�5*   "   ��  �� Property � �� �� Value
 �����6K    �� Triggers 5+   "   P� 5,   &    P� Property �@J�IsMouseOver P� Value$
 True��6E   . �5-   *   ��  � �� �#15A7E96S   5.   '   50      52      53      54      �� 55      -   5         ��Name$ content��55   "   $��2��6   6"   56      �� 57   
   -   5        $ fullContent��57      $y�	Collapsed=�6&   7   1AllInOneLauncher.Elements.Generic.PopupVisualizer  58   
   6   0AllInOneLauncher.Elements.Generic.MenuVisualizer  59   
   5;      5<      
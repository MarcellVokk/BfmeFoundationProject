   M S B A M L   `   `   `  ���� K  GAllInOneLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullq=clr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements/BfmeFoundationProject.AllInOneLauncher.Elements  �Eclr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements.Generic7BfmeFoundationProject.AllInOneLauncher.Elements.Generic  7    1BfmeFoundationProject.AllInOneLauncher.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     _ [Hardcodet.NotifyIcon.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=682384a853a08aad)tb http://www.hardcodet.net/taskbar  Lelements=clr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements  SgenericEclr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements.Generic  -     �Name$  LauncherMainWindow�� � Title$ All in One Bfme Launcher��$��14(� � WindowStartupLocation$ CenterScreen=� � WindowStyle$ ThreeDBorderWindow=�$y�Visible=� J� SnapsToDevicePixels$
 True�� �� BitmapScalingMode$ HighQuality=�.��� -    �Name$ 
windowGrid������  �� ImageSource$7 1pack://application:,,,/Resources/Images/BGMap.png��.+��� -    ��Name$ 
background��$7��1pack://application:,,,/Resources/Images/BGMap.png��$
��Fill=�3  -Hardcodet.Wpf.TaskbarNotification.TaskbarIcon  -   	  Name$	 TrayIcon��
   
IconSource$5
 /pack://application:,,,/allinonelaunchericon.ico��   ToolTipText$ Patch 2.22 Launcher��   MenuActivation$ 
RightClick=�� -   $ outerContent��$��20q� � RowDefinitions �� $
��Auto ��� $��20 ��� $��* �.+�� $��0��.+��� -    ��Name$
 tabs�� �� Orientation$ 
Horizontal=�$��Center=�.+��� -    ��Name$ 
offlineTab��   TextBlockHover#���     MainWindowSingleplayerTab#���  $��16(�$
��Bold"�$��0,0,20,0q�
������  � Cursor$
 Hand���� -   $ 	onlineTab��#���     MainWindowMultiplayerTab#���  $��16(�$
��Bold"�$��0,0,0,0q�
������ $
 Hand���� -	   $ 	guidesTab��#���     MainWindowGuidesTab#���  $��16(�$
��Bold"�$��20,0,0,0q�
������ $
 Hand���� -
   $ aboutTab��#���   $��ABOUT��$��16(�$
��Bold"�$��20,0,0,0q�
������ $
 Hand���� -   $ icons��$ 
Horizontal=�$��Right=�.+��� -   
 � Tag$. (https://www.youtube.com/@BeyondStandards��$9��3pack://application:,,,/Resources/Images/youtube.png��$��32��$��0,0,5,0q�$
 Hand���� -   $+ %https://www.twitch.tv/beyondstandards��$8��2pack://application:,,,/Resources/Images/twitch.png��$��32��$��0,0,5,0q�$
 Hand���� -   $+ %https://discord.com/invite/Q5Yyy3XCuu��$9��3pack://application:,,,/Resources/Images/discord.png��$��32��$��0,0,5,0q�$
 Hand���� -   $B <https://www.moddb.com/mods/battle-for-middle-earth-patch-222��$7��1pack://application:,,,/Resources/Images/moddb.png��$��32��$��0,0,5,0q�$
 Hand���� -   $1 +https://github.com/MarcellVokk/aio-launcher��$8��2pack://application:,,,/Resources/Images/github.png��$��32��$��0,0,5,0q�$
 Hand���� -   $, &https://www.patreon.com/bfmefoundation��$9��3pack://application:,,,/Resources/Images/patreon.png��$��32��$��0,0,5,0q�$
 Hand��� -   $ settingsIcon��
������ $
 Hand��$��Right=�.+��� -   $ color��$y�	Collapsed=�F�  �� Stretch$ Uniform=�$��32�������H�� � S 5� dfffffֿ�x�&1@dfffffֿ�z�Ga@� z�G��@>
ףp=�?����(\�@��" ��V �V �Q���Q@�� �K7�A`@�����M�?�����M�?ȭ* V9��v� @8�A`�P@}j�t�@���(\�@B�l���@أp=
�@F�z�G@��C��@:
ףp=�?|�G��@�������?�z�Ga@tfffffֿ�x�&1@tfffffֿ 5� �������?���(\�"@�:
ףp=�?�� }j�t�@���Q8'@��V �V !��~j� @أp=
�'@�����M�?-���g*@�K7�A`@��ʡE6-@6�A`�P@���Mb�+@���(\�@a��"�y+@z�G��@���Q8-@��V �V �z�Ga@gffff�-@�x�&1@�����Y0@ 5� �����Y0@H�� gffff�-@��(\�#@���Q8-@W�'� a��"�y+@��V �V Q�� 8�A`��+@S8v� ��ʡE6-@��� -���g*@84� e;�O��'@Q�� ���Q8'@�P�� �� ��V �V Qp� ���(\�"@������Y0@ 5� �����Y0@�x�&1@p� �z�Ga@QP�� |�G��@W�� ���(\�@��V �V Q 8� S���Q@S��� �K7�A`@8v� �����M�?h� Z9��v� @Q�'� �j�t�@��(\�#@B
ףp=�?��V �V QH�� �������?� z PǦ W z G�z�G@�h�, h�, W z A`��"�%@�h�, h�, 	����  �� ElementName$ color�� �� Path$ 
Foreground�����  �� 
TargetType" �.���  �� Property � �� �� Value
 ����� �� Triggers P�  P� Property �@J�IsMouseOver P� Value$
 True��. ���  � �� �#15A7E9�� -    ��Name$ content��$��2���� -   $ fullContent��$y�	Collapsed=�M   GBfmeFoundationProject.AllInOneLauncher.Elements.Generic.PopupVisualizer  L   FBfmeFoundationProject.AllInOneLauncher.Elements.Generic.MenuVisualizer  
   M S B A M L   `   `   `  ���� K  GAllInOneLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullm;clr-namespace:BfmeFoundationProject.AllInOneLauncher.Popups-BfmeFoundationProject.AllInOneLauncher.Popups  q=clr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements/BfmeFoundationProject.AllInOneLauncher.Elements  R Nnetstandard, Version=2.1.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd514)clr-namespace:System;assembly=netstandardSystem �Eclr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements.Generic7BfmeFoundationProject.AllInOneLauncher.Elements.Generic  D    >BfmeFoundationProject.AllInOneLauncher.Popups.InstallGamePopup   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Mlocal;clr-namespace:BfmeFoundationProject.AllInOneLauncher.Popups     Relements=clr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements     <system)clr-namespace:System;assembly=netstandard     YgenericEclr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements.Generic     $	��700��$��Center=�$��Center=�.���   � RowDefinitions  �� $
��Auto ��� $��30 ��� $��* ��� $��20 ��� $
��Auto �.+��� -   $��TextTitleInstall��$��0��   InstallGamePopupTitle#���   
�������$��20(�$��Medium"�$
��Left=�$��Center=��� $��1��
������� J� Opacity$	 0.2Q�$��0,10,0,0q�$	��Top=�$��1��� $��2�� � ColumnDefinitions �� $
��Auto ��� $��20 ��� $��* �.+�� $��0��$��Center=�$��Center=�.+��� $0��*/Resources/Images/BFME1LanguageSupport.png��$
��None=�$	��450��$	��270�� J� OpacityMask !�  !� Visual �� 
���   � J� SnapsToDevicePixels$
 True�� �� CornerRadius �� *CornerRadius+ �� RelativeSource ��  �� AncestorType
 Border���� *ActualWidth+ �� 
 Border���� *ActualHeight+ �� 
 Border� $��2��.+��   �� $
��Auto ��� $��5 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��5 ��� $��* �.+��� $��0��$��14(�$��SemiBold"�  InstallGamePopupLanguage#���  
�������D   >BfmeFoundationProject.AllInOneLauncher.Elements.DropdownPicker  -   	  Name$	 LanguageDropdown��$��2��$��Center=�$��20(�$��40��
   
ColorStyle$

 Navy=�   Corners$ 4Q�   Options ��English����
French����
German����Italian����Spanish����Swedish����	Dutch����
Polish����	Norwegian���� $��4��$��14(�$��SemiBold"�  InstallGamePopupLocation#���  
�������P   JBfmeFoundationProject.AllInOneLauncher.Elements.Generic.SmoothScrollViewer  $��6��.���� -    ��Name$ 	locations���� $��4�� �� Orientation$ 
Horizontal=�$��Right=�.+��� -    ��Name$ ButtonAccept��$��0��$��2��  HiglightButton#��[ $��Center=�$	��150��$��0,0,10,0q�  GenericInstall#���  �� -   $ ButtonCancel��$��1��$��2��  GenericCancel#���  $��Center=�$	��150��
   M S B A M L   `   `   `  ���� Z  VBfmeFoundationProject.OnlineKit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:BfmeFoundationProject.OnlineKitBfmeFoundationProject.OnlineKit  c6clr-namespace:BfmeFoundationProject.OnlineKit.Elements(BfmeFoundationProject.OnlineKit.Elements  0    *BfmeFoundationProject.OnlineKit.OnlineMenu   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ?local-clr-namespace:BfmeFoundationProject.OnlineKit     Kelements6clr-namespace:BfmeFoundationProject.OnlineKit.Elements     -     >�Name$  userControl��$
}�True�� � 	Resources  �� MergedDictionaries �� Source   Key��  �� $N H/BfmeFoundationProject.OnlineKit;component/Styles/NormalButtonStyle.xaml?��� *�Segoe UI  OK/BfmeFoundationProject.OnlineKit;component/Fonts/#Segoe UI Variable Display)�%    .��� -    �Name$ 
windowGrid��
���   � J� OpacityMask !�  !� Visual �� -    ��Name$
 mask�� �� RadiusX$ 0��	 �� RadiusY$	 0��
�������$��20��
������ ���� 
 �� ElementName$
 userControl�� �� Path$ ActualWidth����� $
 userControl��$ ActualHeight�.+��� -    ��Name$ 
background��$��UniformToFill=� J� Opacity$	 0.4Q�$	��-10q� J� Effect -  'System.Windows.Media.Effects.BlurEffect     
KernelType$ Gaussian=�   Radius$ 10Q�� -   $ content��.+�� -   $ grid_status��.+�� -   $ status��$y�Visible=�.+��� -    ��Name$ textblock_status��$��Center=�$��Center=�  Segoe UI#��[ $��30(�$
��Thin"�$��DESIGN MODE��
�������� -	   $ loading��$y�	Collapsed=�.+��� -
   $ loading_mode��$��Center=�$��Center=�#��[ $��30(�$
��Thin"�$��LOADING��
�������=  7BfmeFoundationProject.OnlineKit.Elements.LoadingSpinner  -     Name$ status_loading��   	IsLoading$ False��$��Center=�$��Bottom=�$��0,0,0,80q�� -   $ download��$y�	Collapsed=�.+��� -   $ download_mode��$��Center=�$��Center=�#��[ $��30(�$
��Thin"�$��UPDATING��
�������:  4BfmeFoundationProject.OnlineKit.Elements.ProgressBar  -     Name$ download_progressBar��$��Center=�$��Bottom=�$��	0,0,0,100q�$��35���� $��	0,0,0,100q�$��Bottom=�$��Center=�.+��� -    ��Name$ reload_button��  NormalButton#���  $
��LOAD��$y�	Collapsed=��� -   $ repair_button��#���  $��REPAIR��$��0,20,0,0q�$y�	Collapsed=�>  8BfmeFoundationProject.OnlineKit.Elements.PopupVisualizer  
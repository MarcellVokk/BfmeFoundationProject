   M S B A M L   `   `   `  ����Z  VBfmeFoundationProject.OnlineKit, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:BfmeFoundationProject.OnlineKitBfmeFoundationProject.OnlineKit  5      c6clr-namespace:BfmeFoundationProject.OnlineKit.Elements(BfmeFoundationProject.OnlineKit.Elements  5      0    *BfmeFoundationProject.OnlineKit.OnlineMenu   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?local-clr-namespace:BfmeFoundationProject.OnlineKit     5      Kelements6clr-namespace:BfmeFoundationProject.OnlineKit.Elements     5      -   5          >�Name$  userControl��5	   -   $
}�True��6^    � 	Resources 5
       �� MergedDictionaries �� Source   Key�� 5   
    5      �� 5      $N H/BfmeFoundationProject.OnlineKit;component/Styles/NormalButtonStyle.xaml?�6%   �� 5      *�5      Segoe UI  OK/BfmeFoundationProject.OnlineKit;component/Fonts/#Segoe UI Variable Display)�6   5      %    5      .��5      � -   5         �Name$ 
windowGrid��5      
���   � J� OpacityMask 5   
   !� 5       !� Visual 5      �� 5      -   5         ��Name$
 mask��5        �� RadiusX$ 0��6.   	 �� RadiusY$	 0��6:   
�������$��20��6S   
������ ��6}   �� 
 �� ElementName$
 userControl�� �� Path$ ActualWidth���6�   �� $
 userControl��$ ActualHeight�5      .+�5   
   �� -   5         ��Name$ 
background��5      $��UniformToFill=�6$    J� Opacity$	 0.4Q�6<   $	��-10q�6J    J� Effect 5      -  'System.Windows.Media.Effects.BlurEffect  5         
KernelType$ Gaussian=�6      Radius$ 10Q�63   5       � 5!   
   -   5        $ content��5!      .+�5"      � -   5        $ grid_status��5"      .+�5#      � -   5        $ status��5#      $y�Visible=�6'   .+�5$      �� -   5         ��Name$ textblock_status��5$       $��Center=�6:   $��Center=�6W     Segoe UI#��[ 6r   $��30(�6�   $
��Thin"�6�   $��DESIGN MODE��6�   
�������6�   5%      � 5&      -	   5        $ loading��5&      $y�	Collapsed=�6(   .+�5'      �� -
   5        $ loading_mode��5'       $��Center=�66   $��Center=�6S   #��[ 6n   $��30(�6�   $
��Thin"�6�   $��LOADING��6�   
�������6�   =  7BfmeFoundationProject.OnlineKit.Elements.LoadingSpinner  5)      -   5          Name$ status_loading��5)   �      	IsLoading$ False��6.   $��Center=�6@   $��Bottom=�6]   $��0,0,0,80q�6x   6�   5*      � 5+      -   5        $ download��5+      $y�	Collapsed=�6)   .+�5,      �� -   5        $ download_mode��5,       $��Center=�67   $��Center=�6T   #��[ 6o   $��30(�6�   $
��Thin"�6�   $��UPDATING��6�   
�������6�   :  4BfmeFoundationProject.OnlineKit.Elements.ProgressBar  5-      -   5          Name$ download_progressBar��5-   +   $��Center=�6I   $��Bottom=�6f   $��	0,0,0,100q�6�   $��35��6�   5.      �� 5/      $��	0,0,0,100q�6   $��Bottom=�60   $��Center=�6K   .+�50      �� -   5         ��Name$ reload_button��50   D     NormalButton#���  6   $
��LOAD��6[   $y�	Collapsed=�6�   �� 51      -   5        $ repair_button��51   D   #���  6   $��REPAIR��6[   $��0,20,0,0q�6�   $y�	Collapsed=�6�   52      53      54      >  8BfmeFoundationProject.OnlineKit.Elements.PopupVisualizer  55   
   56      57      
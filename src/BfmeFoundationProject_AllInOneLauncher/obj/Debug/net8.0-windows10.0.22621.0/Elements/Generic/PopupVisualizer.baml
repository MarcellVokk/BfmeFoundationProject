   M S B A M L   `   `   `  ����K  GAllInOneLauncher, Version=2.0.2.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:AllInOneLauncher.ElementsAllInOneLauncher.Elements  5      U/clr-namespace:AllInOneLauncher.Elements.Generic!AllInOneLauncher.Elements.Generic  5      7    1AllInOneLauncher.Elements.Generic.PopupVisualizer   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:AllInOneLauncher.Elements     5      Cgeneric/clr-namespace:AllInOneLauncher.Elements.Generic     5        � 	Resources  5
      ;   5AllInOneLauncher.Elements.Generic.BorderSizeConverter   BorderSizeConverter�� 5   
   %   &	          5      6*   5      .��5      � -   5         �Name$
 root��5       J� Opacity$ 0Q�6    J� IsHitTestVisible$ False��63   .+�5   
   � -   5        $ 
background��5      ���	#CC0A0A0C6:   :   4AllInOneLauncher.Elements.Generic.SmoothScrollViewer  5   
   -   5          Name$ scrollViewer��5   %   .��5      � -   5        $ 	popupBody��5      .+�5      ��  �� CornerRadius$ 5��6    J� SnapsToDevicePixels$
 True��6*   $	��-19q�6E   
���   � J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 50Q�6+   	   Opacity$		 0.5Q�6;   
   Color$
 Black��6I      ShadowDepth$ 8Q�6W      	Direction$	 270Q�6g   5      �� 5      $ 6��6   $
 True��6*    J� OpacityMask 5      !� 5       !� Visual 5      �� 5   "   
���   �$
 True��6<    6W   �� *CornerRadius+ �� RelativeSource ��  �� AncestorType
 Border��6�   �� *ActualWidth+ �� 
 Border��6  �� *ActualHeight+ �� 
 Border5      ._�5       � $
}�True��6   $	��-20q�6/   .+�5!      �� -   5         ��Name$ 	navyStyle��5!   !   $ 6��64   $y�Visible=�6E   ��5"      �� 5#   "    �� EndPoint$	 1,1&�66    �� 
StartPoint$	 0,0&�6E   .��5$   &   �  � Color$ 	#FF232831��63    � Offset$ 1Q�6E   � 5%   &   $ 	#FF292D37��63   5&   #   5(      � 5)      -   5        $ acrylicStyle��5)      $y�	Collapsed=�65   .+�5*      /   )AllInOneLauncher.Elements.Generic.Acrylic  $
}�True��6.   5+      �� 5,      -   5        $ content��5,   !   $��20q�62   5-      5.      5/      50      51      52      
   M S B A M L   `   `   `  ����K  GAllInOneLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullq=clr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements/BfmeFoundationProject.AllInOneLauncher.Elements  5      �Eclr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements.Generic7BfmeFoundationProject.AllInOneLauncher.Elements.Generic  5      I    CBfmeFoundationProject.AllInOneLauncher.Elements.Library.LibraryTile   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Relements=clr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements     5      YgenericEclr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements.Generic     5      $	��370��5	      $	��250��6   .��5
      �� -   5          �� CornerRadius$  15��5
       � Cursor$
 Hand��6    J� OpacityMask 5   
   !� 5       !� Visual 5      �� 5      
���   � J� SnapsToDevicePixels$
 True��60     6K   �� *CornerRadius+ �� RelativeSource ��  �� AncestorType
 Border��6�   �� *ActualWidth+ �� 
 Border��6�   �� *ActualHeight+ �� 
 Border5      ._�5   
   � .+�5      E   ?BfmeFoundationProject.AllInOneLauncher.Elements.Generic.Acrylic  � 5      -   5         �Name$ content��5      $��12q�6    � RowDefinitions 5      �� 5      $	��260 �6$   �� 5      $��10 �6$   �� 5      $��* �6$   .+�5      �� $��0��6   $  8��6&   ���	#1EFFFFFF	 J� Effect	 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      
   
BlurRadius$
 4Q�6+      Opacity$	 0.5Q�6:      Color$ 	#FF424242��6H      ShadowDepth$ 2Q�6Z      	Direction$	 270Q�6j    5      !� 5       5       �� 5!   "   $
 True��6)   
���   �  6D   �� *CornerRadius+ �� 
 Border��6�   �� *ActualWidth+ �� 
 Border��6�   �� *ActualHeight+ �� 
 Border6G  5#      ._�5%      �� -   5         ��Name$
 icon��5%      $��Center=�6*   $��UniformToFill=�6E   5&      � 5'      $��2��6    � UseLayoutRounding$
 True��6$   $
 True��6=   .+�5(      �� $��5,0,5,0q�6!   .+�5)      �� -   5         ��Name$ title��5)   $   $��Title��63   $��18(�6@   $��Black"�6N   $
��Wrap=�6a   
�������6u   �� 5*      $��0,5,0,0q�6%    �� Orientation$ 
Horizontal=�66   .+�5+      �� -   5        $ version��5+   (   $��version��69   $��11(�6H   $��Medium"�6V   ���	#59FFFFFF6j   5,      5-      � 5.      $��Bottom=�6   $��23��66    � ColumnDefinitions 5/      �� 50      $��* �6/   �� 51      $��10 �6/   �� 52      $
��Auto �6/   .+�54      �� -   5         ��Name$
 tags��54   5   $��0��6%   $ 
Horizontal=�6C   .+�55      �� $��0,0,6,0q�6%   ���	#1EFFFFFF$  12��6M   ._�56   "   �� -   5        $ 	entryType��56   ,   $
��Type��6?   $��10(�6K   $
��Bold"�6Y   $��	10,5,10,5q�6k   
�������6~   57      �� 58      $��0,0,6,0q�6%   ���	#1EFFFFFF$  12��6M   ._�59   "   �� -   5        $ author��59   ,   $��Author��6<   $��10(�6J   $
��Bold"�6X   $��	10,5,10,5q�6j   
�������$��88��6�   $��CharacterEllipsis=�6�   5:      5;      � 5<      -	   5        $ 
loadingBar��5<   /   $��0��6   $��Bottom=�6C   $��23��6^   $y�Hidden=�6j   .+�5=      �� ���	#33FFFFFF$  12��6<   �� 5>      ���#028BDB$  12��6:    5?   "   �� 5@   &    �� 
StartPoint$ 0,0.5&�6:    �� EndPoint$ 1,0.5&�6M   .��5A   *   �  � Color$ Black��67    � Offset$ 0Q�6E   � 5B   *   -
   5        progressGradientStop1��$ Black��5B   V   $ 0Q�6d   � 5C   *   -   5        progressGradientStop2��$ Transparent��5C   V   $ 0Q�6j   5D   '   5F      �� 5G      -   5        $ progressText��5G   (   $�� ��6>   $��10(�6F   $
��Bold"�6T   $��Center=�6f   $��Center=�6�   
�������6�   5H      �� 5I      -   5         ��Name$ isActiveIcon��5I   1   $��2��6!   ���#016FB3$  12��6\   $
��Left=�6n   $��Bottom=�6�    J� Opacity$ 0Q�6�   ._�5J      �� $��8,5,8,5q�6)   $ 
Horizontal=�6:   $��13��6S   .+�5K   "   F� $��Right=�6'   $��Center=�6C   
�������
�������$	��0.3��6z    �� Stretch$ Uniform=�6�   $��6��6�   ����� V� Г< �8�A`�+@Г< �H/ H/ ��� ��@ ��� �H �t��+@`�L �@=x B� ���(\�B@0�� �H/ H/ �h1  � �h1 `�o �� � �@A `�o �H/ H/ ��$��C@h� �<�O��n)@��< � V� Г< ��� �� 	6�   �� 5L   "   -   5        $ 
activeText��5L   ,      LibraryTileActive#���   6@   $��10(�6k   $
��Bold"�6y   $��5,0,2,0q�6�   $��Center=�6�   
�������$y�Visible=�6�   5M      5N      I   CBfmeFoundationProject.AllInOneLauncher.Elements.Generic.MiniSpinner  5O      -   5          Name$ loadingIcon��5O   >   $��2��6.   $��Right=�6S   $��Center=�6o      	IsLoading$ False��6�   5P      5Q      5R      �� 5S      -   5        $ hoverEffect��5S      ���	#0CFFFFFF$ 0Q�6A   5T      5U      5V      
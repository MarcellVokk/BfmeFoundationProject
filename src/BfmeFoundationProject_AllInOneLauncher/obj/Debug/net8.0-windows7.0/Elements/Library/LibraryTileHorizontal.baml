   M S B A M L   `   `   `  ����K  GAllInOneLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullq=clr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements/BfmeFoundationProject.AllInOneLauncher.Elements  5      �Eclr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements.Generic7BfmeFoundationProject.AllInOneLauncher.Elements.Generic  5      S    MBfmeFoundationProject.AllInOneLauncher.Elements.Library.LibraryTileHorizontal   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Relements=clr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements     5      YgenericEclr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements.Generic     5      .��5
      �� $��2��6     �� CornerRadius$  15��6    J� OpacityMask 5   
   !� 5       !� Visual 5      �� 5      
���   � J� SnapsToDevicePixels$
 True��60     6K   �� *CornerRadius+ �� RelativeSource ��  �� AncestorType
 Border��6�   �� *ActualWidth+ �� 
 Border��6�   �� *ActualHeight+ �� 
 Border5      ._�5   
   � .+�5      E   ?BfmeFoundationProject.AllInOneLauncher.Elements.Generic.Acrylic  � 5      -   5         �Name$ activeEntry��5      $��10q�6    � UseLayoutRounding$
 True��64   $
 True��6M   $y�Hidden=�6h    � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $��10 �6'   �� 5      $��* �6'   .+�5      �� $��0��6   $  9��6)   ���	#1EFFFFFF$
��Left=�6Q   $	��Top=�6l   $��104.3��6�   $	��120��6�   $
 True��6�    5      !� 5       5      �� 5   "   $
 True��6)   
���   �  6D   �� *CornerRadius+ �� 
 Border��6�   �� *ActualWidth+ �� 
 Border��6�   �� *ActualHeight+ �� 
 Border6G  5       ._�5"      �� -   5        	 ��Name$	 activeEntryIcon��5"      $��Center=�65   $��UniformToFill=�6P   $E��?https://bfmeladder.com/resources/workshop-files/222-artwork.png��6h   5#      � 5$      $��2��6   .+�5%      �� -   5        
 ��Name$
 availableTitleArea��5%   ,   $��5q�6!   $
}�True��6H   .+�5&      �� -   5        $
 
titleStack��5&   %    �� Orientation$ 
Horizontal=�69   $
��Left=�6R   .+�5'      �� -   5         ��Name$ activeEntryTitle��5'   (   $
��2.22��6B   $��18(�6N   $��Black"�6\   $
��Wrap=�6o   
�������6�   �� 5(      -   5        $ activeEntryVersion��5(   (   $��1.0.0��6D   $��18(�6Q   $��Regular"�6_   ���	#59FFFFFF$��Center=�6�   $��7,0,0,0q�6�   5)      �� 5*      -   5        $

 tags��5*   %   $ 
Horizontal=�63   $��0,10,0,0q�6L   .+�5+      �� $��0,0,6,0q�6%   ���	#1EFFFFFF$  14��6M   ._�5,   "   �� -   5        $ 	entryType��5,   ,   $
��Type��6?   $��12(�6K   $
��Bold"�6Y   $��	12,6,12,6q�6k   
�������6~   5-      �� 5.      $��0,0,6,0q�6%   ���	#1EFFFFFF$  14��6M   ._�5/   "   �� -	   5        $ activeEntryAuthor��5/   ,   $��Author��6G   $��12(�6U   $
��Bold"�6c   $��	12,6,12,6q�6u   
�������$��88��6�   $��CharacterEllipsis=�6�   50      51      52      �� 53      -
   5         ��Name$ activeEntryActive��53      ���#016FB3$  14��6M   $
��Left=�6_   $��Bottom=�6z   $y�Hidden=�6�   ._�54      �� $��	10,6,12,6q�6%   $ 
Horizontal=�68   .+�55      F� $��Right=�6#   $��Center=�6?   
�������
�������$	��0.3��6v    �� Stretch$ Uniform=�6�   $��8��6�   ����� V� Г< �8�A`�+@Г< �H/ H/ ��� ��@ ��� �H �t��+@`�L �@=x B� ���(\�B@0�� �H/ H/ �h1  � �h1 `�o �� � �@A `�o �H/ H/ ��$��C@h� �<�O��n)@��< � V� Г< ��� �� 	6�   �� 56         LibraryTileActive#���   6(   $��12(�6S   $
��Bold"�6a   $��7,0,0,0q�6s   $��Center=�6�   
�������6�   57      58      �� 59      -   5        $ activeEntryLoading��59      ���#016FB3$  14��6N   $
��Left=�6`   $��Bottom=�6{   ._�5:      �� $��	10,6,12,6q�6%   $ 
Horizontal=�68   .+�5;      I   CBfmeFoundationProject.AllInOneLauncher.Elements.Generic.NanoSpinner  -   5          Name$ loadingSpinner��5;   2   $��Right=�6J   $��Center=�6f      	IsLoading$ False��6�   �� 5<        LibraryTileSyncing#���  6(   $��12(�6T   $
��Bold"�6b   $��7,0,0,0q�6t   $��Center=�6�   
�������6�   5=      5>      �� 5?      -   5         ��Name$ activeEntryReloadButton��5?        RoundButton#��[ 6>   $��Bottom=�6c   $��Right=�6~   .��5@      �� $��	10,6,12,6q�6%   $ 
Horizontal=�68   .+�5A      F� -   5         F�Name$ syncAgainIcon��5A   #   $��Right=�6:   $��Center=�6V   
�������
�������$	��0.3��6�   $ Uniform=�6�   $��10��6�   ����� "; X���z�G�w@ X���z�G�w@ �� H� H�� �� �� h� ��� H� H�� h� 0�
����(\�p@ 0�
� H� H�����(\�p@ ��gfffff�@��K7	�@� `� `�����x�f�@ ty5� `� `���S㥛a@��K7	�@� `� `���S㥛i@��K7	�@W �[2��K7	�@ � ��O��nr@&��C�r@  � �	6�   F� 5B      -   5        $ 
updateIcon��5B   #   $��Right=�67   $��Center=�6S   
�������
�������$	��0.3��6�   $ UniformToFill=�6�   $��10��6�   ����� z  � � �� �y�   �  � � �� �W Q�q� �"��~j@i��|?�&@.�$��@  �  � i��|?� @.�$��@0�$��@.�$��@  �  � 0�$��@.�$��@�"��~j@�"��~j@ �  � ��pr �W ��pr �y� � z  �   �  � 	$y�	Collapsed=�6Y  �� 5C      -   5        $ syncAgainText��5C   (    " LibraryTileHorizontalSyncAgain#���  6?   $��12(�6w   $
��Bold"�6�   $��7,0,0,0q�6�   $��Center=�6�   
�������6�   �� 5D      -   5        $ 
updateText��5D   (     LibraryTileHorizontalUpdate#���  6<   $��12(�6q   $
��Bold"�6   $��7,0,0,0q�6�   $��Center=�6�   
�������$y�	Collapsed=�6�   5E      5F      5G      5H      �� 5I      -   5        $ activeEntryNullIndicator��5I      $��Center=�6:   $��Center=�6W   ���	#1EFFFFFF ( $LibraryTileHorizontalNoPatchSelected#���  6�   $��16(�6�   $��Medium"�6�   5J      5K      5L      
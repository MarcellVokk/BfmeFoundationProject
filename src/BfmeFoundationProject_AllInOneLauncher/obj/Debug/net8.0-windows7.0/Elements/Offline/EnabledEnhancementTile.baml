   M S B A M L   `   `   `  ����K  GAllInOneLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullq=clr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements/BfmeFoundationProject.AllInOneLauncher.Elements  5      T    NBfmeFoundationProject.AllInOneLauncher.Elements.Offline.EnabledEnhancementTile   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Relements=clr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements     5      .��5	      �� $��2��6     �� CornerRadius$  9��6   ���	#0CFFFFFF._�5
   
   � .+�5      � -   5         �Name$ activeEntry��5      $��7q�6    J� SnapsToDevicePixels$
 True��63    � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $��9 �6'   �� 5      $��* �6'   �� 5      $
��Auto �6'   .+�5      �� $��0��6   $  8��6)   ���	#1EFFFFFF$
��Left=�6Q   $	��Top=�6l   $
��56.5��6�   $��65��6�    J� OpacityMask 5      !� 5       !� Visual 5      �� 5   "   $
 True��6)   
���   �  6D   �� *CornerRadius+ �� RelativeSource ��  �� AncestorType
 Border��6�   �� *ActualWidth+ �� 
 Border��6�   �� *ActualHeight+ �� 
 Border6G  5      ._�5      �� -   5         ��Name$ activeEntryIcon��5      $��Center=�65   $��UniformToFill=�6P   $E��?https://bfmeladder.com/resources/workshop-files/222-artwork.png��6h   5      � 5      $��2��6   $	��Top=�6'   .+�5      �� -   5        	 ��Name$	 availableTitleArea��5   ,   $��2q�6!   $
}�True��6H   .+�5      �� -   5        $	 
titleStack��5   %   
 �� Orientation$
 
Horizontal=�69   $
��Left=�6R   .+�5      �� -   5         ��Name$ activeEntryTitle��5   (   $
��2.22��6B   $��15(�6N   $
��Bold"�6\   $
��Wrap=�6n   
�������6�   �� 5       -   5        $ activeEntryVersion��5    (   $��1.0.0��6D   $��15(�6Q   $��Regular"�6_   ���	#59FFFFFF$��Center=�6�   $��7,0,0,0q�6�   5!      �� 5"      -   5        $
	 tags��5"   %   $
 
Horizontal=�63   $��0,10,0,0q�6L   .+�5#      �� $��0,0,6,0q�6%   ���	#1EFFFFFF$  14��6M   ._�5$   "   �� -   5        $ 	entryType��5$   ,   $
��Type��6?   $��10(�6K   $
��Bold"�6Y   $��	12,6,12,6q�6k   
�������6~   5%      �� 5&      $��0,0,6,0q�6%   ���	#1EFFFFFF$  14��6M   ._�5'   "   �� -	   5        $ activeEntryAuthor��5'   ,   $��Author��6G   $��10(�6U   $
��Bold"�6c   $��	12,6,12,6q�6u   
�������$��88��6�   $��CharacterEllipsis=�6�   5(      5)      5*      �� 5+      -
   5         ��Name$ activeEntryActive��5+      ���#016FB3$  14��6M   $
��Left=�6_   $��Bottom=�6z   $y�Hidden=�6�   ._�5,      �� $��	10,6,12,6q�6%   $
 
Horizontal=�68   .+�5-      F� $��Right=�6#   $��Center=�6?   
�������
�������$	��0.3��6v    �� Stretch$ Uniform=�6�   $��8��6�   ����� V� Г< �8�A`�+@Г< �H/ H/ ��� ��@ ��� �H �t��+@`�L �@=x B� ���(\�B@0�� �H/ H/ �h1  � �h1 `�o �� � �@A `�o �H/ H/ ��$��C@h� �<�O��n)@��< � V� Г< ��� �� 	6�   �� 5.         LibraryTileButtonActive#���   6(   $��12(�6Y   $
��Bold"�6g   $��7,0,0,0q�6y   $��Center=�6�   
�������6�   5/      50      51      � 52      $��3��6   .+�53      �� -   5          DestructiveButton#��[ 53      $��Center=�6H   $��Right=�6c   $��0,0,5,0q�6�   54      55      56      57      58      
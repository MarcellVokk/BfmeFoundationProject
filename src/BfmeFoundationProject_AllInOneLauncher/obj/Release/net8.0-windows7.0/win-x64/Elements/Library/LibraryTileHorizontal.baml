   M S B A M L   `   `   `  ���� K  GAllInOneLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullq=clr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements/BfmeFoundationProject.AllInOneLauncher.Elements  �Eclr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements.Generic7BfmeFoundationProject.AllInOneLauncher.Elements.Generic  S    MBfmeFoundationProject.AllInOneLauncher.Elements.Library.LibraryTileHorizontal   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Relements=clr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements     YgenericEclr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements.Generic     .���� $��2��  �� CornerRadius$  15�� J� OpacityMask !�  !� Visual �� 
���   � J� SnapsToDevicePixels$
 True��  �� *CornerRadius+ �� RelativeSource ��  �� AncestorType
 Border���� *ActualWidth+ �� 
 Border���� *ActualHeight+ �� 
 Border._�� .+�E   ?BfmeFoundationProject.AllInOneLauncher.Elements.Generic.Acrylic  � -    �Name$ activeEntry��$��10q� � UseLayoutRounding$
 True��$
 True��$y�Hidden=� � ColumnDefinitions �� $
��Auto ��� $��10 ��� $��* �.+��� $��0��$  9�����	#1EFFFFFF$
��Left=�$	��Top=�$��104.3��$	��120��$
 True�� !�  �� $
 True��
���   �  �� *CornerRadius+ �� 
 Border���� *ActualWidth+ �� 
 Border���� *ActualHeight+ �� 
 Border._��� -   	 ��Name$	 activeEntryIcon��$��Center=�$��UniformToFill=�$E��?https://bfmeladder.com/resources/workshop-files/222-artwork.png��� $��2��.+��� -   
 ��Name$
 availableTitleArea��$��5q�$
}�True��.+��� -   $
 
titleStack�� �� Orientation$ 
Horizontal=�$
��Left=�.+��� -    ��Name$ activeEntryTitle��$
��2.22��$��18(�$��Black"�$
��Wrap=�
��������� -   $ activeEntryVersion��$��1.0.0��$��18(�$��Regular"����	#59FFFFFF$��Center=�$��7,0,0,0q��� -   $

 tags��$ 
Horizontal=�$��0,10,0,0q�.+��� $��0,0,6,0q����	#1EFFFFFF$  14��._��� -   $ 	entryType��$
��Type��$��12(�$
��Bold"�$��	12,6,12,6q�
��������� $��0,0,6,0q����	#1EFFFFFF$  14��._��� -	   $ activeEntryAuthor��$��Author��$��12(�$
��Bold"�$��	12,6,12,6q�
�������$��88��$��CharacterEllipsis=��� -
    ��Name$ activeEntryActive�����#016FB3$  14��$
��Left=�$��Bottom=�$y�Hidden=�._��� $��	10,6,12,6q�$ 
Horizontal=�.+�F� $��Right=�$��Center=�
�������
�������$	��0.3�� �� Stretch$ Uniform=�$��8������� V� Г< �8�A`�+@Г< �H/ H/ ��� ��@ ��� �H �t��+@`�L �@=x B� ���(\�B@0�� �H/ H/ �h1  � �h1 `�o �� � �@A `�o �H/ H/ ��$��C@h� �<�O��n)@��< � V� Г< ��� �� 	��    LibraryTileActive#���   $��12(�$
��Bold"�$��7,0,0,0q�$��Center=�
��������� -   $ activeEntryLoading�����#016FB3$  14��$
��Left=�$��Bottom=�._��� $��	10,6,12,6q�$ 
Horizontal=�.+�I   CBfmeFoundationProject.AllInOneLauncher.Elements.Generic.NanoSpinner  -     Name$ loadingSpinner��$��Right=�$��Center=�   	IsLoading$ False����   LibraryTileSyncing#���  $��12(�$
��Bold"�$��7,0,0,0q�$��Center=�
��������� -    ��Name$ activeEntryReloadButton��  RoundButton#��[ $��Bottom=�$��Right=�.���� $��	10,6,12,6q�$ 
Horizontal=�.+�F� -    F�Name$ syncAgainIcon��$��Right=�$��Center=�
�������
�������$	��0.3��$ Uniform=�$��10������� "; X���z�G�w@ X���z�G�w@ �� H� H�� �� �� h� ��� H� H�� h� 0�
����(\�p@ 0�
� H� H�����(\�p@ ��gfffff�@��K7	�@� `� `�����x�f�@ ty5� `� `���S㥛a@��K7	�@� `� `���S㥛i@��K7	�@W �[2��K7	�@ � ��O��nr@&��C�r@  � �	F� -   $ 
updateIcon��$��Right=�$��Center=�
�������
�������$	��0.3��$ UniformToFill=�$��10������� z  � � �� �y�   �  � � �� �W Q�q� �"��~j@i��|?�&@.�$��@  �  � i��|?� @.�$��@0�$��@.�$��@  �  � 0�$��@.�$��@�"��~j@�"��~j@ �  � ��pr �W ��pr �y� � z  �   �  � 	$y�	Collapsed=��� -   $ syncAgainText�� " LibraryTileHorizontalSyncAgain#���  $��12(�$
��Bold"�$��7,0,0,0q�$��Center=�
��������� -   $ 
updateText��  LibraryTileHorizontalUpdate#���  $��12(�$
��Bold"�$��7,0,0,0q�$��Center=�
�������$y�	Collapsed=��� -   $ activeEntryNullIndicator��$��Center=�$��Center=����	#1EFFFFFF ( $LibraryTileHorizontalNoPatchSelected#���  $��16(�$��Medium"�
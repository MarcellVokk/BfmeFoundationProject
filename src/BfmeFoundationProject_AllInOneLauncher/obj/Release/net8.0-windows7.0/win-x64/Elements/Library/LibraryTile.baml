   M S B A M L   `   `   `  ���� K  GAllInOneLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullq=clr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements/BfmeFoundationProject.AllInOneLauncher.Elements  �Eclr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements.Generic7BfmeFoundationProject.AllInOneLauncher.Elements.Generic  I    CBfmeFoundationProject.AllInOneLauncher.Elements.Library.LibraryTile   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Relements=clr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements     YgenericEclr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements.Generic     $	��370��$	��250��.���� -     �� CornerRadius$  15�� � Cursor$
 Hand�� J� OpacityMask !�  !� Visual �� 
���   � J� SnapsToDevicePixels$
 True��  �� *CornerRadius+ �� RelativeSource ��  �� AncestorType
 Border���� *ActualWidth+ �� 
 Border���� *ActualHeight+ �� 
 Border._�� .+�E   ?BfmeFoundationProject.AllInOneLauncher.Elements.Generic.Acrylic  � -    �Name$ content��$��12q� � RowDefinitions �� $	��260 ��� $��10 ��� $��* �.+��� $��0��$  8�����	#1EFFFFFF	 J� Effect	 3  -System.Windows.Media.Effects.DropShadowEffect  
   
BlurRadius$
 4Q�   Opacity$	 0.5Q�   Color$ 	#FF424242��   ShadowDepth$ 2Q�   	Direction$	 270Q� !�  �� $
 True��
���   �  �� *CornerRadius+ �� 
 Border���� *ActualWidth+ �� 
 Border���� *ActualHeight+ �� 
 Border._��� -    ��Name$
 icon��$��Center=�$��UniformToFill=�� $��2�� � UseLayoutRounding$
 True��$
 True��.+��� $��5,0,5,0q�.+��� -    ��Name$ title��$��Title��$��18(�$��Black"�$
��Wrap=�
��������� $��0,5,0,0q� �� Orientation$ 
Horizontal=�.+��� -   $ version��$��version��$��11(�$��Medium"����	#59FFFFFF� $��Bottom=�$��23�� � ColumnDefinitions �� $��* ��� $��10 ��� $
��Auto �.+��� -    ��Name$
 tags��$��0��$ 
Horizontal=�.+��� $��0,0,6,0q����	#1EFFFFFF$  12��._��� -   $ 	entryType��$
��Type��$��10(�$
��Bold"�$��	10,5,10,5q�
��������� $��0,0,6,0q����	#1EFFFFFF$  12��._��� -   $ author��$��Author��$��10(�$
��Bold"�$��	10,5,10,5q�
�������$��88��$��CharacterEllipsis=�� -	   $ 
loadingBar��$��0��$��Bottom=�$��23��$y�Hidden=�.+��� ���	#33FFFFFF$  12���� ���#028BDB$  12�� ��  �� 
StartPoint$ 0,0.5&� �� EndPoint$ 1,0.5&�.���  � Color$ Black�� � Offset$ 0Q�� -
   progressGradientStop1��$ Black��$ 0Q�� -   progressGradientStop2��$ Transparent��$ 0Q��� -   $ progressText��$�� ��$��10(�$
��Bold"�$��Center=�$��Center=�
��������� -    ��Name$ isActiveIcon��$��2�����#016FB3$  12��$
��Left=�$��Bottom=� J� Opacity$ 0Q�._��� $��8,5,8,5q�$ 
Horizontal=�$��13��.+�F� $��Right=�$��Center=�
�������
�������$	��0.3�� �� Stretch$ Uniform=�$��6������� V� Г< �8�A`�+@Г< �H/ H/ ��� ��@ ��� �H �t��+@`�L �@=x B� ���(\�B@0�� �H/ H/ �h1  � �h1 `�o �� � �@A `�o �H/ H/ ��$��C@h� �<�O��n)@��< � V� Г< ��� �� 	�� -   $ 
activeText��   LibraryTileActive#���   $��10(�$
��Bold"�$��5,0,2,0q�$��Center=�
�������$y�Visible=�I   CBfmeFoundationProject.AllInOneLauncher.Elements.Generic.MiniSpinner  -     Name$ loadingIcon��$��2��$��Right=�$��Center=�   	IsLoading$ False���� -   $ hoverEffect�����	#0CFFFFFF$ 0Q�
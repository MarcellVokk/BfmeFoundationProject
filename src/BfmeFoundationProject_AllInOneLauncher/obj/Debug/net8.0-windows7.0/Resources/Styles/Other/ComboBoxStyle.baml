   M S B A M L   `   `   `  ����b  ^PresentationFramework.Aero2, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35hKclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero2Microsoft.Windows.Themes  5      K GAllInOneLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullq=clr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements/BfmeFoundationProject.AllInOneLauncher.Elements 5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   TextBox.Static.Background  �� Color  ComboBoxEditableTextBox �� 
TargetType �� Property �� Value �� 
TargetType ��Name  ComboBox.Static.Background  ComboBox.Static.Border  ComboBox.Static.Glyph ' #ComboBox.Static.Editable.Background # ComboBox.Static.Editable.Border . *ComboBox.Static.Editable.Button.Background * &ComboBox.Static.Editable.Button.Border !	 ComboBox.MouseOver.Background 
 ComboBox.MouseOver.Border * &ComboBoxItem.ItemsviewHover.Background & "ComboBoxItem.ItemsviewHover.Border - )ComboBoxItem.ItemsviewSelected.Background ) %ComboBoxItem.ItemsviewSelected.Border 2 .ComboBoxItem.ItemsviewSelectedHover.Background . *ComboBoxItem.ItemsviewSelectedHover.Border 4 0ComboBoxItem.ItemsviewSelectedNoFocus.Background 0 ,ComboBoxItem.ItemsviewSelectedNoFocus.Border & "ComboBoxItem.ItemsviewFocus.Border / +ComboBoxItem.ItemsviewHoverFocus.Background + 'ComboBoxItem.ItemsviewHoverFocus.Border �� RelativeSource �� AncestorType  FocusVisual ��Name	 �� Padding
 J� SnapsToDevicePixels �� Triggers P� Property P� Value �� 
TargetName!   System.Windows.SystemColors    GrayTextBrushKey ]� 
Conditions �� Property �� Value  ComboBox.MouseOver.Glyph * &ComboBox.MouseOver.Editable.Background & "ComboBox.MouseOver.Editable.Border 1 -ComboBox.MouseOver.Editable.Button.Background �� EndPoint �� 
StartPoint � Color � Offset - )ComboBox.MouseOver.Editable.Button.Border  ComboBox.Pressed.Background  ComboBox.Pressed.Border  ComboBox.Pressed.Glyph ( $ComboBox.Pressed.Editable.Background $   ComboBox.Pressed.Editable.Border /! +ComboBox.Pressed.Editable.Button.Background +" 'ComboBox.Pressed.Editable.Button.Border  # ComboBox.Disabled.Background $ ComboBox.Disabled.Border % ComboBox.Disabled.Glyph )& %ComboBox.Disabled.Editable.Background %' !ComboBox.Disabled.Editable.Border 0( ,ComboBox.Disabled.Editable.Button.Background ,) (ComboBox.Disabled.Editable.Button.Border * ComboBoxToggleButton �� CornerRadius F�Name �� Stretch ^� 
Conditions �� Binding + ComboBoxEditableTemplate �Name � ColumnDefinitions �Name � AllowsTransparency  �� Mode5   /Microsoft.Windows.Themes.SystemDropShadowChrome!  Name"   Color# �� MaxDropDownHeight$ �� ElementName% �� ClearTypeHint& ��Name' ��Name( ��Name-  'System.Windows.Input.KeyboardNavigation)   DirectionalNavigation* d�Name+ d� 	IsChecked, ��Name- �� 
CaretBrush. �� 
IsReadOnly/ P� 
SourceName , ComboBoxTemplate0 ��Name#1 �� SelectionBoxItemStringFormat2 �� ContentStringFormat3 �� SelectionBoxItemTemplate4 �� SelectionBoxItem5 J� IsHitTestVisible6 �� Triggers�� 5      H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml     5      UthemeKclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero2  5      Jelements=clr-namespace:BfmeFoundationProject.AllInOneLauncher.Elements  5      %.7  &	        &	 #     &	 Z    &	 }    &	 �    &	 �    &	 �    &	 	    &	 .    &		 S    &	
 v    &	 �    &	 �    &	 �    &	 �    &	 !    &	 D    &	 g    &	 �    &	 �    &	 �    &	 �    '��     7  7  7  7  7  7  7  7  7  7  7  &	 �    &	 �    &	 �    &	 �    &	 �    &	 �    &	 �    &	 �    &	     &	  B    &	! e    &	"     &	# $    &	$ G    &	% j    &	& �    &	' �    &	( �    &	) �    &	*     7  7  7  7  7 	 7 
 7 	 7 
 7  7  7  7  7 # 7 $ 7 & 7 ' &	+ �    7 * 7  &	, �(    7 * '�� ]1    7  7  7 , 7 + �� 5      $  	#FFFFFFFF��68   �� 5      " ��6,   .�5   
   ��  �@�OverridesDefaultStyle$
 true��62   �� 5   
    �@J�	AllowDrop$
 true��6&   �� 5	   
    � ��$ 0��6%   �� 5
   
    � ��$ 0��6&   �� 5   
    �@�FocusVisualStyle 6-   Z� �� 5   
    �@��PanningMode$ VerticalFirst=�65   �� 5   
    �@��IsFlicksEnabled$ False��63   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      �� $ PART_ContentHost��6#   
������ $|�false��6V   $��Hidden=�6h   $��Hidden=�6�   5      5      5      �� 5      $  	#19FFFFFF��69   �� 5      $  	#FF583306��65   �� 5      $  	#FF606060��64   �� 5      $  	#FFFFFFFF��6B   �� 5      $  	#FFABADB3��6>   �� 5      $  Transparent��6I   �� 5      $  Transparent��6E   �� 5      $  	#19FFFFFF��6<   �� 5      $  	#FF583306��68   �� 5       $  Black��6E   �� 5!      $  	#FFFF0000��6A   �� 5"      $  	#33FFFFFF��6H   �� 5#      $  	#FFFF0000��6D   �� 5$      $  	#33FFFFFF��6M   �� 5%      $  	#FFFF0000��6I   �� 5&      $  Transparent��6O   �� 5'      $  	#FFFF0000��6K   �� 5(      $  	#FFFF0000��6A   �� 5)      $  	#33FFFFFF��6J   �� 5*      $  	#FFFF0000��6F   �� 5+      " ��6   .�5,   
   ��  �@J�SnapsToDevicePixels$
 True��60   �� 5-   
    � ��$	 4,1q�6$   �� 5.   
    � �� 67   �� *HorizontalContentAlignment+ �� " ���� 5/   
    � �� 65   �� *VerticalContentAlignment+ �� " ���� 50   
    � ��
 ���� 6'   �� 51   
    � ��
 ���� 6(   �� 52   
    � ��$ 1q�6,   �� 53   
    �@�FocusVisualStyle# �  6-   �� 54   
    � �� 55      �� 56      " ��6"   .R�57      �� $ Bd��6   #��z��6)   #��z��6S   $��0q�6   #	 z��6�   $

 true��6�   ._�58      �� #��z��6+   #
 z
 6n   #��z��6�   59       5:      P� 5;       � {�$ False��67   . �5<      �� $ Bd��6G   # �  6W    � ��5=      ]� 5>       5?      �� 5@   "    �@��
IsSelected$ False��6B   �� 5A   "    �@J�IsMouseOver$
 True��6C   �� 5B   "    �@J�IsKeyboardFocused$ False��6I   ./�5D      �� $ Bd��6;   8    � ��6K   �� 5E      $ Bd��6<   8   � ��6L   5F      ]� 5G       5H      �� 5I   "    �@��
IsSelected$
 True��6B   �� 5J   "    �@J�IsMouseOver$ False��6C   �� 5K   "    �@J�IsKeyboardFocused$
 True��6I   ./�5M      �� $ Bd��6;   8   � ��6K   �� 5N      $ Bd��6<   8   � ��6L   5O      ]� 5P       5Q      �� 5R   "    �@��
IsSelected$
 True��6B   �� 5S   "    �@J�IsMouseOver$
 True��6C   ./�5U      �� $ Bd��6;   8   � ��6K   �� 5V      $ Bd��6<   8   � ��6L   5W      ]� 5X       5Y      �� 5Z   "    �@��
IsSelected$
 True��6B   �� 5[   "    �@J�IsMouseOver$ False��6C   �� 5\   "    �@J�IsKeyboardFocused$ False��6I   ./�5^      �� $ Bd��6;   8   � ��6K   �� 5_      $ Bd��6<   8   � ��6L   5`      ]� 5a       5b      �� 5c   "    �@��
IsSelected$ False��6B   �� 5d   "    �@J�IsMouseOver$ False��6C   �� 5e   "    �@J�IsKeyboardFocused$
 True��6I   ./�5g      �� $ Bd��6<   8   � ��6L   5h      ]� 5i       5j      �� 5k   "    �@��
IsSelected$ False��6B   �� 5l   "    �@J�IsMouseOver$
 True��6C   �� 5m   "    �@J�IsKeyboardFocused$
 True��6I   ./�5o      �� $ Bd��6;   8 	  � ��6K   �� 5p      $ Bd��6<   8 
  � ��6L   5q      5s      5u      5v      �� 5x      $  	#19FFFFFF��67   �� 5y      $  	#FFFFFFFF��6E   �� 5z      $  	#FF7EB4EA��6A   �� 5{      $	 0,1&�6P   $	 0,0&�6_   .��5|   
   � $ 	#FFEBF4FC��6   $	 0.0Q�6)   � 5}   
   $ 	#FFDCECFC��6   $	 1.0Q�6)   5~      �� 5      $  	#FF7EB4EA��6H   �� 5�      $  	#19FFFFFF��6:   �� 5�      $  	#FF583306��66   �� 5�      $  	#FF000000��65   �� 5�      $  	#FFFFFFFF��6C   �� 5�      $  	#FF569DE5��6?   �� 5�      $	 0,1&�6N   $	 0,0&�6]   .��5�   
   � $ 	#FFDAEBFC��6   $	 0.0Q�6)   � 5�   
   $ 	#FFC4E0FC��6   $	 1.0Q�6)   5�      �� 5�      $  	#FF569DE5��6F   �� 5�      $  	#FFF0F0F0��6;   �� 5�      $  	#FFD9D9D9��67   �� 5�      $  	#FFBFBFBF��66   �� 5�      $  	#FFFFFFFF��6D   �� 5�      $  	#FFBFBFBF��6@   �� 5�      $  Transparent��6K   �� 5�      $  Transparent��6G   �� 5�      " d�6)   .�5�   
   ��  �@�OverridesDefaultStyle$
 true��62   �� 5�   
    � ��$ false��6&   �� 5�   
    � |�$ false��6&   �� 5�   
    �@��	ClickMode$ Press=�6&   �� 5�   
    � �� 5�      �� 5�      " d�6"   .R�5�      �� $ templateRoot��6   8��  $ 4��6l   8�� #��z��6�   $

 true��6�   ._�5�      �� $ splitBorder��6!   
������ $	 5,0,10,0q�6P   $��Right=�6c   $��0q�6   $

 true��6�   $��30��6�   ._�5�      F� $ arrow��6#   $ Uniform=�62   $��Center=�6D   $��Center=�6_   ���	#7FFFFFFF����� p�F �P�# p�F � �  � � z � Q�8� .�$��@i��|?�,@�"��~j@� �  � �i��|?� @�?� �0�$��@�?� � �  � �t�V�?�"��~j@�t�V�?.�$��@� �  � 	6�   5�      5�       5�      P� 5�       �@J�IsMouseOver$
 true��69   . �5�      �� $ arrow��65    � ��
 �����6H   5�      ^� 5�       5�      �� 5�   "    6,   �� *
IsEditable+ �� " �� true6�   �� 5�   "    6,   �� *IsMouseOver+ �� *Self+	 false6r   �� 5�   "    6,   �� *	IsPressed+ �� *Self+	 false6p   �� 5�   "    6,   �� *	IsEnabled+ �� *Self+ true6p   .0�5�      �� $ templateRoot��6;   8   � ��6U   �� 5�      $ templateRoot��6<   8   � ��6V   5�      ^� 5�       5�      �� 5�   "    6,   �� *IsMouseOver+ �� *Self+ true6r   �� 5�   "    6,   �� *
IsEditable+ �� " ��	 false6�   .0�5�      �� $ templateRoot��6;   8   � ��6U   �� 5�      $ templateRoot��6<   8   � ��6V   5�      ^� 5�       5�      �� 5�   "    6,   �� *IsMouseOver+ �� *Self+ true6r   �� 5�   "    6,   �� *
IsEditable+ �� " �� true6�   .0�5�      �� $ templateRoot��6;   8   � ��6U   �� 5�      $ templateRoot��6<   8   � ��6V   5�      ^� 5�       5�      �� 5�   "    6,   �� *	IsPressed+ �� *Self+ true6p   �� 5�   "    6,   �� *
IsEditable+ �� " ��	 false6�   .0�5�      �� $ templateRoot��6;   8   � ��6U   �� 5�      $ templateRoot��6<   8 	  � ��6V   5�      ^� 5�       5�      �� 5�   "    6,   �� *	IsPressed+ �� *Self+ true6p   �� 5�   "    6,   �� *
IsEditable+ �� " �� true6�   .0�5�      �� $ templateRoot��6;   8 
  � ��6U   �� 5�      $ templateRoot��6<   8   � ��6V   5�      ^� 5�       5�      �� 5�   "    6,   �� *	IsEnabled+ �� *Self+	 false6p   �� 5�   "    6,   �� *
IsEditable+ �� " ��	 false6�   .0�5�      �� $ templateRoot��6;   8   � ��6U   �� 5�      $ templateRoot��6<   8   � ��6V   5�      ^� 5�       5�      �� 5�   "    6,   �� *	IsEnabled+ �� *Self+	 false6p   �� 5�   "    6,   �� *
IsEditable+ �� " �� true6�   .0�5�      �� $ templateRoot��6;   8   � ��6U   �� 5�      $ templateRoot��6<   8   � ��6V   5�      5�      5�      5�      �� 5�      " ��67   .R�5�   
   � $ templateRoot��6   $

 true��6%    5�      �� 5�      $��* �6#   �� 5�      $��30 �6#   .+�5�      � $ 
PART_Popup��6   $
 true��6(   $��2��6B   $��1q�6�   $��Bottom=�6�   #��� .�6�   ��6V   �� *IsDropDownOpen+$  TwoWay=� �� *TemplatedParent+.��5�        $! shadow��6/   $" Transparent��6?   #��z# 6�   ��6S   �� *ActualWidth+$$ templateRoot��._�5�      �� $ dropDownBorder��6   ���#32343B$��0q�6K   ���#32343B._�5�      �� $ DropDownScrollViewer��6'   .��5�      � $
 grid��6#   $% Enabled=�61   .+�5�   "   �� $& canvas��6)   $
��Left=�69   $��0��6T   $	��Top=�6_   $��0��6w   .+�5�   &   �� $' 
opaqueRect��60   ��6D   �� *
Background+$$ dropDownBorder����6|   �� *ActualHeight+$$ dropDownBorder����6�   �� *ActualWidth+$$ dropDownBorder��5�   #   �� 5�   "   $( ItemsPresenter��61   $) 	Contained=�6I   #
 z
 6~   5�      5�      5�      5�      5�      d� 5�      $* toggleButton��6   $��Right=�61   #��z��6M   #��z��6y   $��2��6�   8��  + 6�   �� *IsDropDownOpen+$  TwoWay=� �� *TemplatedParent+6$  �� 5�      $ border��6   ���	#19FFFFFF._�5�      �� $, PART_EditableTextBox��6   $��10q�68   
�������
- �����#��z��6k   #��z��6  8�� #��z��6\  . 6�   �� *
IsReadOnly+ �� *TemplatedParent+6\  5�      5�       5�   
   P� 5�       � {�$ false��6+   . �5�      �� $ border��6,    �@J�Opacity$
 0.56Q�6@   5�      P� 5�       �@J�IsKeyboardFocusWithin$
 true��67   . �5�      ��  � ��
 �   �6/   5      P� 5     $/ 
PART_Popup��6/    �@�HasDropShadow$
 true��6G   . �5     �� $ shadow��6+    � ��$ 0,0,5,5q�6?   �� 5     $ shadow��6*    �@ Color$ 	#71000000��6>   5     P� 5      �@��HasItems$ false��6*   . �5     �� $ dropDownBorder��6+    � ��$ 95��6G   5     ]� 5      5	     �� 5
      �@��
IsGrouping$
 true��66   �� 5     " �@%�IsVirtualizingWhenGrouping$ false��6X   ./�5     ��  � ��$ false��6B   5     P� 5     $/ DropDownScrollViewer��6?    � ��$ false��6a   . �5     �� $ 
opaqueRect��6/    6G   �� *VerticalOffset+$$ DropDownScrollViewer�� �@��Top�� 5     $ 
opaqueRect��60    6H   �� *HorizontalOffset+$$ DropDownScrollViewer�� �@��Left5     5     �� 5     " ��6/   .R�5  
   � $ templateRoot��6   $

 true��6%    5     �� 5     $��* �6#   �� 5     $��30 �6#   .+�5     � $ 
PART_Popup��6   $
 true��6(   $��2��6B   $��1q�6�   $��Bottom=�6�   #��� .�6�   ��6V   �� *IsDropDownOpen+$  TwoWay=� �� *TemplatedParent+.��5       $! shadow��6/   $" Transparent��6?   #��z# 6�   ��6S   �� *ActualWidth+$$ templateRoot��._�5     �� $ dropDownBorder��6   ���#32343B$��0q�6K   ���#32343B._�5     �� $ DropDownScrollViewer��6'   .��5     � $
 grid��6#   $% Enabled=�61   .+�5   "   �� $& canvas��6)   $
��Left=�69   $��0��6T   $	��Top=�6_   $��0��6w   .+�5!  &   �� $' 
opaqueRect��60   ��6D   �� *
Background+$$ dropDownBorder����6|   �� *ActualHeight+$$ dropDownBorder����6�   �� *ActualWidth+$$ dropDownBorder��5"  #   �� 5#  "   $( ItemsPresenter��61   $) 	Contained=�6I   #
 z
 6~   5$     5%     5&     5'     5(     d� 5)     $* toggleButton��6   #��z��61   #��z��6]   $��2��6�   8��  + 6�   �� *IsDropDownOpen+$  TwoWay=� �� *TemplatedParent+6  �� 5*     $0 contentPresenter��6   $��15, 00, 00, 00q�69   #2 z1 6Q   #��z3 6�   #��z4 6�   #��z��6   #��z��6A  $5 false��6�  #
 z
 6�  #��z��6�  5+      5,  
   P� 5-     $/ 
PART_Popup��6/    �@�HasDropShadow$
 true��6G   . �5.     �� $ shadow��6+    � ��$ 0,0,5,5q�6?   �� 5/     $ shadow��6*    �@ Color$ 	#71000000��6>   50     P� 51      �@��HasItems$ false��6*   . �52     �� $ dropDownBorder��6+    � ��$ 95��6G   53     ]� 54      55     �� 56      �@��
IsGrouping$
 true��66   �� 57     " �@%�IsVirtualizingWhenGrouping$ false��6X   ./�59     ��  � ��$ false��6B   5:     P� 5;     $/ DropDownScrollViewer��6?    � ��$ false��6a   . �5<     �� $ 
opaqueRect��6/    6G   �� *VerticalOffset+$$ DropDownScrollViewer�� �@��Top�� 5=     $ 
opaqueRect��60    6H   �� *HorizontalOffset+$$ DropDownScrollViewer�� �@��Left5>     5@     �� 5B     " ��6   .�5C  
   ��  �@�FocusVisualStyle# �  6-   �� 5D  
    � ��8   6'   �� 5E  
    � ��8  6(   �� 5F  
    � ��
 �����6'   �� 5G  
    � ��$ 0q�6,   �� 5H  
    � ��$
 Auto=�6G   �� 5I  
    � ��$
 Auto=�6E   �� 5J  
    � ��$ 0,0,0,0q�6$   �� 5K  
    �@�Cursor$
 Hand��6#   �� 5L  
    � ��$ 38��6#   �� 5M  
    � ��$ Center=�65   �� 5N  
    � ��$
 true��6:   �� 5O  
    �@��PanningMode$
 Both=�65   �� 5P  
    �@��IsFlicksEnabled$ False��63   �� 5Q  
    � ��8  6%   �� 5R  
    � ��$ 12(�6%   �� 5S  
    � ��$ Medium"�6'   6 5T  
   P� 5U      �@��
IsEditable$
 true��6,   . �5V     ��  � ��$ false��6.   �� 5W      � ��$ 2q�6,   �� 5X      � ��8  6-   5Y     5[     5\     
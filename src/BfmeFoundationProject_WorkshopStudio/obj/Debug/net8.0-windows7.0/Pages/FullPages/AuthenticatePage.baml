   M S B A M L   `   `   `  ����Y  UBfmeFoundationProject.SharedUi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�]clr-namespace:BfmeFoundationProject.SharedUi.Elements;assembly=BfmeFoundationProject.SharedUi'BfmeFoundationProject.SharedUi.Elements  5      ( $BfmeFoundationProject_WorkshopStudioA   ;BfmeFoundationProject.WorkshopStudio.Pages.AuthenticatePage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      rsharedUi]clr-namespace:BfmeFoundationProject.SharedUi.Elements;assembly=BfmeFoundationProject.SharedUi     5      .��5	      � .+�5
   
   �� $��WORKSHOP STUDIO��6   $	��Top=�6+   $��Center=�6C   
�������$��24(�6s   $
��Bold"�6�   $��0,40,0,0q�6�   �� 5   
   $��LOG IN��6   $	��Top=�6"   $��Center=�6:   
�������$��30(�6j   $
��Thin"�6x   $��	0,160,0,0q�6�   � 5   
   -   5          �Name$  	loginInfo��5      $��Center=�6"   $��Center=�6=   .+�5      ?   9BfmeFoundationProject.SharedUi.Elements.CornerAccentFrame     Darkness$	 0.5Q�6)   �� 5      $	��600��6   $��30q�6%   .+�5      �� .+�5      �� $��Bottom=�6    $��Center=�6;   $��30(�6X   $
��Thin"�6f   $
��Wrap=�6x    �� TextAlignment$ Center=�6�   $��EMAIL��6�   
�������6�   �� 5      -   5         ��Name$ textbox_email��5      $��0,30,0,0q�65   $��28(�6G   $��57��6U   5      �� 5      $��0,60,0,0q�6   .+�5      �� $��Bottom=�6    $��Center=�6;   $��30(�6X   $
��Thin"�6f   $
��Wrap=�6x   $ Center=�6�   $��PASSWORD��6�   
�������6�   G� 5      -   5         G�Name$ textbox_password��5   "   $��0,30,0,0q�6<   $��28(�6N   $��57��6\    G� Password$  ��6h   5      � 5      $��0,60,0,0q�6   .+�5      �� -   5         ��Name$ textblock_createAccount��5       $��Bottom=�6A   $
��Left=�6\   $��15(�6w   $��Regular"�6�   $
��Wrap=�6�   $ Center=�6�   $��Create an account��6�    � Cursor$
 Hand��6�   ���	#FF00AFF9��5      �� 5       �� 
TargetType" ��6$   	 �� Triggers	 5   "   P� 5   &   
 P� Property
 �@J�IsMouseOver P� Value$
 True��6E   . �5   *   ��  �� Property � �� �� Value$ 	Underline{�6V   5   '   5       5"      �� 5#      -   5        $ textblock_resetPassword��5#       $��Bottom=�6A   $��Right=�6\   $��15(�6x   $��Regular"�6�   $
��Wrap=�6�   $ Center=�6�   $��I forgot my password��6�   $
 Hand��6�   ���	#FF00AFF9��5$      �� 5%      " ��6$   	 5&   "   P� 5'   &   
 �@J�IsMouseOver$
 True��6E   . �5(   *   ��  � ��$ 	Underline{�6V   5)   '   5+      5-      5.      5/      50      �� 51   
   -   5        $ status��51      $��Center=�6$   $��Center=�6?   
�������$��30(�6o   $
��Thin"�6}   $y�	Collapsed=�6�   �� 52   
   -   5        $ error��52      $��Bottom=�6#   $��Center=�6>   $ Center=�6[   ���	#FFF7B7B7$��20(�6�   $
��Thin"�6�   $��	0,0,0,180q�6�   �� 53   
   -   5         ��Name$ button_connect��53   9      PrimaryButton#���   6   $��0,0,0,60q�6Q   $��Center=�6c   $��Bottom=�6�   $��LOG IN��6�   <   6BfmeFoundationProject.SharedUi.Elements.LoadingSpinner  54   
   -	   5          Name$ button_connect_loading��54   "   $��Bottom=�6B   $��0,0,0,80q�6]   $y�Hidden=�6o      	IsLoading$ False��6�   55      56      
   M S B A M L   `   `   `  ���� Y  UBfmeFoundationProject.SharedUi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�]clr-namespace:BfmeFoundationProject.SharedUi.Elements;assembly=BfmeFoundationProject.SharedUi'BfmeFoundationProject.SharedUi.Elements  ( $BfmeFoundationProject_WorkshopStudioA   ;BfmeFoundationProject.WorkshopStudio.Pages.AuthenticatePage   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     rsharedUi]clr-namespace:BfmeFoundationProject.SharedUi.Elements;assembly=BfmeFoundationProject.SharedUi     .��� .+��� $��WORKSHOP STUDIO��$	��Top=�$��Center=�
�������$��24(�$
��Bold"�$��0,40,0,0q��� $��LOG IN��$	��Top=�$��Center=�
�������$��30(�$
��Thin"�$��	0,160,0,0q�� -     �Name$  	loginInfo��$��Center=�$��Center=�.+�?   9BfmeFoundationProject.SharedUi.Elements.CornerAccentFrame     Darkness$	 0.5Q��� $	��600��$��30q�.+��� .+��� $��Bottom=�$��Center=�$��30(�$
��Thin"�$
��Wrap=� �� TextAlignment$ Center=�$��EMAIL��
��������� -    ��Name$ textbox_email��$��0,30,0,0q�$��28(�$��57���� $��0,60,0,0q�.+��� $��Bottom=�$��Center=�$��30(�$
��Thin"�$
��Wrap=�$ Center=�$��PASSWORD��
�������G� -    G�Name$ textbox_password��$��0,30,0,0q�$��28(�$��57�� G� Password$  ��� $��0,60,0,0q�.+��� -    ��Name$ textblock_createAccount��$��Bottom=�$
��Left=�$��15(�$��Regular"�$
��Wrap=�$ Center=�$��Create an account�� � Cursor$
 Hand�����	#FF00AFF9����  �� 
TargetType" ��	 �� Triggers	 P� 
 P� Property
 �@J�IsMouseOver P� Value$
 True��. ���  �� Property � �� �� Value$ 	Underline{��� -   $ textblock_resetPassword��$��Bottom=�$��Right=�$��15(�$��Regular"�$
��Wrap=�$ Center=�$��I forgot my password��$
 Hand�����	#FF00AFF9���� " ��	 P� 
 �@J�IsMouseOver$
 True��. ���  � ��$ 	Underline{��� -   $ status��$��Center=�$��Center=�
�������$��30(�$
��Thin"�$y�	Collapsed=��� -   $ error��$��Bottom=�$��Center=�$ Center=����	#FFF7B7B7$��20(�$
��Thin"�$��	0,0,0,180q��� -    ��Name$ button_connect��   PrimaryButton#���   $��0,0,0,60q�$��Center=�$��Bottom=�$��LOG IN��<   6BfmeFoundationProject.SharedUi.Elements.LoadingSpinner  -	     Name$ button_connect_loading��$��Bottom=�$��0,0,0,80q�$y�Hidden=�   	IsLoading$ False��
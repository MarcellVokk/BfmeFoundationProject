   M S B A M L   `   `   `  ���� _  [BfmeFoundationProject_WorkshopStudio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulli9clr-namespace:BfmeFoundationProject.WorkshopStudio.Popups+BfmeFoundationProject.WorkshopStudio.Popups  Y UBfmeFoundationProject.SharedUi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�]clr-namespace:BfmeFoundationProject.SharedUi.Elements;assembly=BfmeFoundationProject.SharedUi'BfmeFoundationProject.SharedUi.Elements m;clr-namespace:BfmeFoundationProject.WorkshopStudio.Elements-BfmeFoundationProject.WorkshopStudio.Elements  U QSystem.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a7,clr-namespace:System;assembly=System.RuntimeSystem J    DBfmeFoundationProject.WorkshopStudio.Popups.WorkshopEntryEditorPopup   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Klocal9clr-namespace:BfmeFoundationProject.WorkshopStudio.Popups     rsharedUi]clr-namespace:BfmeFoundationProject.SharedUi.Elements;assembly=BfmeFoundationProject.SharedUi     Pelements;clr-namespace:BfmeFoundationProject.WorkshopStudio.Elements     ?System,clr-namespace:System;assembly=System.Runtime     $
��1150��$��Center=�$��Stretch=�$��	0,60,0,60q�.��� -     �Name$
  root��$��20q� � RowDefinitions �� $
��Auto ��� $��20 ��� $��* ��� $��20 ��� $
��Auto �.+�� $��0��.+�3  -BfmeFoundationProject.SharedUi.Elements.HTabs  -     Name$
 tabs��$��Bottom=�$
��Left=�   Tabs ��INFO����
IMAGES����	FILES����MAPS����FACTIONS����  �� Orientation$ 
Horizontal=�$��Right=�.+�� .+��� -    ��Name$ button_sync��   
MiniButton#���   $	��110��.���� $ 
Horizontal=�$��4q�.+�� .+�@  :BfmeFoundationProject.SharedUi.Elements.MiniLoadingSpinner  -     Name$ sync_loading��   	IsLoading$ False��F� -    F�Name$ 	sync_icon��	 �� Stretch$	 Uniform=����	#A5FFFFFF$	��1.4��
 �� StrokeStartLineCap$
 Round=� �� StrokeEndLineCap$ Round=� �� StrokeLineJoin$ Round=�y���� -1�ا � �D @T�  ��{ ��{ � 	= @KL � 	= @T� � z @T� � 	= @]� ���)���  ��{ ��{ � -1��!� -1��� � $� ��� 	$��15��$��13���� $
��SYNC�����	#A5FFFFFF$��15(�$��Normal"�$��Center=�$��8,0,0,0q�9  3BfmeFoundationProject.SharedUi.Elements.ProgressBar  -     Name$ sync_progress��$	��110��   BackgroundFillBrush
 ����    ShowProgressText$ False��   Progress$ 0Q� J� IsHitTestVisible$ False���� -   $ button_import��#���   $��10,0,0,0q�.���� $ 
Horizontal=�$��4q�.+�F� $	 Uniform=����	#A5FFFFFF$	��1.4��$
 Round=�$ Round=�$ Round=������ � @o@� 	=  �� � 	= �pr � � ��- � -1�pr � -1 � � �  � � -1�pr � �  � � � @o@� �  � � 	= �pr Ѐ�O������ ��Ѐ���� ���� ��р�@o@	$��15���� $��IMPPORT�����	#A5FFFFFF$��15(�$��Normal"�$��Center=�$��8,0,0,0q��� -   $ button_export��#���   $��10,0,0,0q�.���� $ 
Horizontal=�$��4q�.+�F� $	 Uniform=����	#A5FFFFFF$	��1.4��$
 Round=�$ Round=�$ Round=������ � @o@� 	=  �� � 	= �pr � � ��- � -1�pr � -1 � � �  � � -1�pr � �  � � � @o@� �  � � 	= �pr ���� ��р�O�����!��� р�O�����!@o@	$��15���� $��EXPORT�����	#A5FFFFFF$��15(�$��Normal"�$��Center=�$��8,0,0,0q�� -	   $  generalInfoPage��$��2��.+�@  :BfmeFoundationProject.SharedUi.Elements.SmoothScrollViewer  .���� .+��� -
    ��Name$ error��$��0,0,0,15q����	#FF5F2A21���	#FFA53B30$��1q�$y�	Collapsed=�._�� $��15q� � ColumnDefinitions �� $
��Auto ��� $��* �.+�F� $��0��$	 Uniform=����	#FFEA4242���	#FFEA4242$	��0.4��������� 0� �I+�@0�  > > ш� h�� 䥛� �ҿ��ʡ,@ � ��� �x ��� ����K�-@��� �W9��v~/@��� @�� ��� 0�� h�� ��� 0� � z @KL ��;� @KL ��� �WS �͇ �y[ �pv� 0�� �������@0�� �@l @l �`Vl �y[ � z @KL �(� (� �/�$ @�ا �/�$ @@]� ��/�$ @�ا �	$��15��$��15���� -    ��Name$ 	errorText��$��1��$)��#An error occured while publishing: �����	#CCFFFFFF$��15(�$��Normal"�$��Center=�$
��Wrap=�$��8,0,0,0q�� $��0,0,0,0q� �� $
��Auto ��� $��15 ��� $��* �.+��� $��0��$
��Left=�.+�N   HBfmeFoundationProject.WorkshopStudio.Elements.WorkshopEntryArtworkEditor  -     Name$ artwork��$	��Top=�� $��2�� �� �� $��15 ���  �� $
��Auto ��� $��20 ��� $
��Auto ��� $��20 ��� $
��Auto �.+��� $��0��$��0��.+��� $��0��$
��GUID��$��14(�>  8BfmeFoundationProject.SharedUi.Elements.StringTypeEditor  -     Name$
 guid��$��0,10,0,0q�$��38��$��Stretch=�   
IsReadOnly$
 True���� $��0��$��2��.+��� $��0��$
��NAME��$��14(�  -   $
 name��$��0,10,0,0q�$��38��$��Stretch=��� $��2��$��0��.+��� $��0��$��VERSION��$��14(�  -   $ version��$��0,10,0,0q�$��Stretch=��� $��2��$��2��.+��� $��0��$��AUTHOR��$��14(�  -   $ author��$��0,10,0,0q�$��Stretch=��� $��4��$��0��.+��� $��0��$
��GAME��$��14(�  -   $
 game��   	InputMode$ Options=�$��0,10,0,0q�$��Stretch=��� $��4��$��2��.+��� $��0��$
��TYPE��$��14(�  -   $
 type��$ Options=�$��0,10,0,0q�$��Stretch=��� $��DESCRIPTION��$��14(�$��0,10,0,0q��� -    ��Name$ description��$��0,10,0,0q�$��85��$��Stretch=� �� AcceptsReturn$
 True��$	��Top=�$��10q�" �� VerticalScrollBarVisibility$
 Auto=��� $��VERSION CHANGELOG��$��14(�$��0,10,0,0q��� -   $ 	changelog��$��0,10,0,0q�$��85��$��Stretch=�$
 True��$	��Top=�$��10q�$
 Auto=�� $��0,15,0,5q�.+�>   8BfmeFoundationProject.WorkshopStudio.Elements.ListHeader  -     Name$ dependenciesHeader��>   8BfmeFoundationProject.WorkshopStudio.Elements.ListHeader   OnAdd$ OnAddDependency��   Title$ DEPENDENCIES��$��14(�   AddFolderEnabled$ False��  -   $ dependenciesHeaderGuid��$��Right=�$	��200��$��0,0,37,0q�    PlaceholderText$  GUID of dependency to add��$��32���� -   ! ��Name$! dependencies��� -   $  
imagesPage��$��2��$y�Hidden=�.+�R	   LBfmeFoundationProject.WorkshopStudio.Elements.WorkshopEntryScreenshotLibrary	  -   " 	 Name$" screenshotLibrary��� -   $  	filesPage��$��2��$y�Hidden=�.+�L
   FBfmeFoundationProject.WorkshopStudio.Elements.WorkshopEntryFileBrowser
  -   # 
 Name$# fileBrowser��L   FBfmeFoundationProject.WorkshopStudio.Elements.WorkshopEntryFileBrowser$   OnFilesChanged$$ OnFileBrowserFilesChanged��� -   $  mapsPage��$��2��$y�Hidden=�.+�K   EBfmeFoundationProject.WorkshopStudio.Elements.WorkshopEntryMapLibrary  -   %  Name$% 
mapLibrary��� -   $  factionsPage��$��2��$y�Hidden=�.+�O   IBfmeFoundationProject.WorkshopStudio.Elements.WorkshopEntryFactionLibrary  -   &  Name$& factionLibrary��� $��4��.+��� -    $! buttons��$��Center=�$ 
Horizontal=�.+��� -!   $ buttonSubmit��  NormalButton#���  $��SUBMIT��$	��152��$��0,0,10,0q��� -"   $ buttonCancel��#���  $��CANCEL��$	��152��� -#   $  progressFrame��$y�Hidden=�.+�  -$   $ progressBar��$	��600��$ False���� -%   $ progressText��$�� ��$��Center=�$��Center=�$��15(�
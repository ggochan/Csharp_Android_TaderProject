   M S B A M L   `   `   `  ���� G  CTaderProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:TaderProjectTaderProject  )    #TaderProject.OrderList_OptionAddDlg   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:TaderProject     $	��225��$	��370��  � Icon$   Resources/tdicon_white.png�� � 
ResizeMode$ CanMinimize=� � WindowStyle$
 None=� � 	Resources    BlueButtonStyle �� 
TargetType  BasedButtonStyle �� BasedOn �� Property �� Value �� 
TargetType	 ��Name
 �� CornerRadius �� Triggers  WhiteButtonStyle  BasedButtonStyle2��%�  &	        7  &	 �     7  '�� >    �� " ��8   .���  � �� �� " ��.R��� $	 Border��$
 2��$��1q����#355ea9
������ ._��� $��2q�$��Center=�$��Center=���.  �� " ��8   .���  � �� �� " ��.R��� $	 Border��$
 2��$��1q�
�������
������ ._��� $��2q�$��Center=�$��Center=���.  �� " ��.���  � ��$
 Left=���  � ��$ Center=�.���  � RowDefinitions �� $��30 ��� $��* �.+�� $��0��.+��� -   $
��Left=�$��30��$��5,5,0,0q�$	��Top=�$��30��.+��� $ ��Resources/tdicon_white.png���� -   
�������$��	35,0,80,0q�]� $��Right=�.+��� -    ��Name$ ToMiniButton��$��40��$	��ㅡ��$��30��#��[ $
��Bold"����� *	ButtonCmd+�� -   $ CloseButton��$��40��$��X��$��30��#��[ $
��Bold"����� *	ButtonCmd+� $��1 ��.+��� -   $��Dlg_OrderMenuTB�� �� 
IsReadOnly . $
��Left=�$��22��$��	30,18,0,0q� �� TextWrapping$ NoWrap=�$	��Top=�$	��315��.����O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem �� 6sys&clr-namespace:System;assembly=mscorlib     " �� �� 	Resources   CueBannerBrush j� 
AlignmentX j� 
AlignmentY j� Stretch !� Visual��%k   &	       !� $
 Left=�$ Center=�$
 None=� �� $$��주문자 메뉴(직접입력)��
������� �� Triggers P�  P� Property � �� �� Empty P� Value# Z . ���  � ��# [ P�  � �� Z� . ���  � ��# [ P�  �@J�IsKeyboardFocused . . ���  � ��
 �����.���� -   $��Dlg_OptionTB��$
��Left=�$��22��$��	30,72,0,0q�$ NoWrap=�$	��Top=�$	��195��.����1&clr-namespace:System;assembly=mscorlibSystem �� 6sys&clr-namespace:System;assembly=mscorlib     " �� ��%a   &	       !� $
 Left=�$ Center=�$
 None=� �� $��옵션(직접입력)��
������� P�  � ��# Z . ���  � ��# [ P�  � �� Z� . ���  � ��# [ P�  �@J�IsKeyboardFocused . . ���  � ��
 �����.���� -   $��Dlg_OptionCB�� �� DisplayMemberPath$ OptionCombo��$��22��$��225,72,23,0q�$	��Top=� J� RenderTransformOrigin$ 0.142,0.867&� �� 
IsEditable .  ��.  �� -   $��Dlg_TakeOrderbtn��$��주문완료��$
��Left=�$��
54,130,0,0q�$	��Top=�$	��265��$��40��#��[  $
��Bold"����� *	ButtonCmd+�� ���#355ea9$��1q�$
��Left=�$	��225��$	��Top=�$	��370��$��4��$��3��
   M S B A M L   `   `   `  ���� G  CTaderProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:TaderProjectTaderProject      TaderProject.OrderReceipt   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:TaderProject     $	��525��$	��370��  � 
ResizeMode$  CanMinimize=� � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � 	Resources    YellowButtonStyle �� 
TargetType  BasedButtonStyle �� BasedOn �� Property �� Value �� 
TargetType	 ��Name
 �� CornerRadius �� Triggers  WhiteButtonStyle  BasedButtonStyle2��%Z  &	        7  &	 �     7  �� " ��8   .���  � �� �� " ��.R��� $	 Border��$
 2��$��1q����#ffe433
������ ._��� $��2q�$��Center=�$��Center=���.  �� " ��8   .���  � �� �� " ��.R��� $	 Border��$
 2��$��1q�
�������
������ ._��� $��2q�$��Center=�$��Center=���.  .���  � RowDefinitions �� $��30 ��� $��* �.+�� $��0��.+��� -   $
��Left=�$��30��$��5,5,0,0q�$	��Top=�$��30��.+��� $ ��Resources/tdicon_white.png���� -   
�������$��	35,0,80,0q�]� $��Right=�.+��� -    ��Name$ ToMiniButton��$��40��$	��ㅡ��$��30��#��[ $
��Bold"����� *	ButtonCmd+�� -   $ CloseButton��$��40��$��X��$��30��#��[ $
��Bold"����� *	ButtonCmd+�� ���#355ea9$��1q�$
��Left=�$	��525��$	��Top=�$	��370��$��2��� $��1�� �� $
��0.8* ��� $
��0.2* �.+�� $��0��$��Center=�$��Center=�.+��� .+��� $��맑은고딕)�$
��Bold"����#355ea9$��20(�$��Center=�$��	0,10,0,20q�.��주문 완료�� -   $��recTB��" �� VerticalScrollBarVisibility$
 Auto=� �� 
IsReadOnly . $	��260��$	��300�� �� TextWrapping$
 Wrap=�� $��1��$��Center=�$��Right=�$��	0,0,30,50q�.+���  �� Orientation$ 
Horizontal=�.+��� -    ��Name$ 	rec_id_TB��$��1��$
��Left=�$��22��$ NoWrap=�$��Center=�$	��200��.����O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem �� 6sys&clr-namespace:System;assembly=mscorlib     " �� �� 	Resources   CueBannerBrush j� 
AlignmentX j� 
AlignmentY j� Stretch !� Visual��%|   &	       !� $
 Left=�$ Center=�$
 None=� �� $5��/메시지 전송 받을 채팅방 이름 입력��
������� �� Triggers P�  P� Property � �� �� Empty P� Value# Z . ���  � ��# [ P�  � �� Z� . ���  � ��# [ P�  �@J�IsKeyboardFocused . . ���  � ��
 �����.���� -   $��Dlg_TakeOrderbtn��$��2��$��10,0,0,0q�$��맑은고딕)�$��카카오톡 전송��$
��Left=�$	��Top=�$	��100��$��28��#��[  $
��Bold"����� *	ButtonCmd+
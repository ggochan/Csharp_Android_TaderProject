   M S B A M L   `   `   `  ���� G  CTaderProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:TaderProjectTaderProject      TaderProject.OrderMain   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:TaderProject     -   $	��525��$	��370��  � 
ResizeMode$  CanMinimize=� � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � 	Resources    RedButtonStyle �� 
TargetType  BasedButtonStyle �� BasedOn �� Property �� Value �� 
TargetType	 ��Name
 �� CornerRadius �� Triggers  BlueButtonStyle  WhiteButtonStyle  BasedButtonStyle2��%	  &	        7  &	 �     7  &	 B    7  �� " ��8   .���  � �� �� " ��.R��� $	 Border��$
 2��$��1q����#e46612
������ ._��� $��2q�$��Center=�$��Center=���.  �� " ��8   .���  � �� �� " ��.R��� $	 Border��$
 2��$��1q����#355ea9
������ ._��� $��2q�$��Center=�$��Center=���.  �� " ��8   .���  � �� �� " ��.R��� $	 Border��$
 2��$��1q�
�������
������ ._��� $��2q�$��Center=�$��Center=���.  .���  � RowDefinitions �� $��30 ��� $��* �.+�� $��0��.+��� -   
�������$��0,0,80,0q�]� $��Right=�.+��� -    ��Name$ ToMiniButton��$��40��$	��ㅡ��$��30��#��[ $
��Bold"����� *	ButtonCmd+�� -   $ CloseButton��$��40��$��X��$��30��#��[ $
��Bold"����� *	ButtonCmd+�� ���#355ea9$��1q�$
��Left=�$	��525��$	��Top=�$	��370��$��2��� $��1��$��0,2,0,-2q� � ColumnDefinitions �� $
��153* ��� $
��217* �.+��� -    ��Name$ 	IdTextBox��$
��Left=�$��22��$��
55,294,0,0q� �� TextWrapping$ NoWrap=�$	��Top=�$	��197��$��2��.����O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem �� 6sys&clr-namespace:System;assembly=mscorlib     " �� �� 	Resources   CueBannerBrush j� 
AlignmentX j� 
AlignmentY j� Stretch !� Visual��%`   &	       !� $
 Left=�$ Center=�$
 None=� �� $��사용자명 입력��
������� �� Triggers P�  P� Property � �� �� Empty P� Value# Z . ���  � ��# [ P�  � �� Z� . ���  � ��# [ P�  �@J�IsKeyboardFocused . . ���  � ��
 �����.���� $��Stretch=�$	��100��$	��Top=�$	��150��$��115,111,105,0q�$��2��.+��� $ ��Resources/tdicon_white.png��$��UniformToFill=�$��79��$��91�� �� Left$ 29��
 �� Top$ 10���� -   $ TakeOrderbtn��$��주문받기��$
��Left=�$��
54,328,0,0q�$	��Top=�$	��265��$��40��#��[ $
��Bold"�$��2������ *	ButtonCmd+�� -   $ Orderingbtn��$��주문하기��$
��Left=�$��
54,378,0,0q�$	��Top=�$	��265��$��40��#��[  $
��Bold"�$��2������ *	ButtonCmd+�� -   $ Setbtn��$��주문관리��$
��Left=�$��
12,-24,0,0q�$	��Top=�$��69��$��23��#��[ $
��Bold"�$��10(����� *	ButtonCmd+�� -	   $ IPbtn��$��서버 IP 설정��$
��Left=�$��
86,-24,0,0q�$	��Top=�$��69��$��23��#��[ $
��Bold"�$��10(�$��2������ *	ButtonCmd+�� -
   $ IDupdatebtn��$��ID확인��$
��Left=�$��104,293,0,0q�$	��Top=�$��62��$��23��#��[ $
��Bold"�$��1��$��10(����� *	ButtonCmd+�� -   $ receipt_btn��$y�Hidden=�$��주문완료��$
��Left=�$��
86,-24,0,0q�$	��Top=�$��69��$��23��#��[ $
��Bold"�$��10(�$��2������ *	ButtonCmd+
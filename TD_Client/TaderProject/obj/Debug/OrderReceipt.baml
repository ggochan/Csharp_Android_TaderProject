   M S B A M L   `   `   `  ����G  CTaderProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:TaderProjectTaderProject  5   	       TaderProject.OrderReceipt   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:TaderProject     5   	   $	��525��5   	   $	��370��6     � 
ResizeMode$  CanMinimize=�6"    � WindowStyle$
 None=�6;    � WindowStartupLocation$ CenterScreen=�6N    � 	Resources 5	         YellowButtonStyle �� 
TargetType  BasedButtonStyle �� BasedOn �� Property �� Value �� 
TargetType	 ��Name
 �� CornerRadius �� Triggers  WhiteButtonStyle  BasedButtonStyle2��%�  &	        7  &	 M    7  �� 5
   
   " ��6*   8   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� $	 Border��5      $
 2��5      $��1q�5      ���#ffe433
������ ._�5      �� $��2q�5      $��Center=�5      $��Center=�5      ��. 5      5       5      5      5      5      �� 5    
   " ��6)   8   .�5!      ��  � �� 5"      �� 5#      " ��6&   .R�5$      �� $	 Border��5%      $
 2��5&      $��1q�5'      
�������
������ ._�5*      �� $��2q�5+      $��Center=�5,      $��Center=�5-      ��. 5.      5/       50      52      54      55      56      .��57      �  � RowDefinitions 58   
   �� 59      $��30 �6   �� 5:      $��* �6   .+�5<   
   � $��0��6   .+�5=      �� -   5        $
��Left=�5=      $��30��60   $��5,5,0,0q�6<   $	��Top=�6M   $��30��6e   .+�5?      �� $ ��Resources/tdicon_white.png��6   5@      �� 5A      -   5        
�������$��	35,0,80,0q�5B   $   5C      ]� 5D      $��Right=�6   .+�5E      �� -   5         ��Name$ ToMiniButton��5E      $��40��5G      $	��ㅡ��5H      $��30��5I      #��[ 6C   $
��Bold"�6m   ��6%   �� *	ButtonCmd+6�   �� 5J      -   5        $ CloseButton��5J      $��40��5L      $��X��5M      $��30��5N      #��[ 6C   $
��Bold"�6m   ��6%   �� *	ButtonCmd+6�   5O      5P      �� 5Q   
   ���#355ea9$��1q�6'   $
��Left=�6;   $	��525��6V   $	��Top=�6c   $	��370��6{   $��2��6�   � 5R   
   $��1��6    5S      �� 5T      $
��0.8* �6    �� 5U      $
��0.2* �6    .+�5W      � $��0��6   $��Center=�6    $��Center=�6;   .+�5X      �� .+�5Y      �� $��맑은고딕)�6   $
��Bold"�6.   ���#355ea9$��20(�6U   $��Center=�6c   $��	0,10,0,20q�6�   .��6�   주문 완료6�   �� 5Z      -   5        $��recTB��5Z   S   " �� VerticalScrollBarVisibility$
 Auto=�6    �� 
IsReadOnly . $	��260��6`   $	��300��6l    �� TextWrapping$
 Wrap=�6y   5[      5\      � 5]      $��1��6   $��Center=�6!   $��Right=�6<   $��	0,0,30,50q�6X   .+�5^      ��  �� Orientation$ 
Horizontal=�6   .+�5_      �� -   5         ��Name$ 	rec_id_TB��5_   '   $��1��6   $
��Left=�6;   $��22��6V   $ NoWrap=�6b   $��Center=�6x   $	��200��6�   .��6�   ��5`      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5a   !   �� 6   6sys&clr-namespace:System;assembly=mscorlib     6!   " ��6T    �� 	Resources 5b        CueBannerBrush j� 
AlignmentX j� 
AlignmentY j� Stretch !� Visual��%�   &	       !� 5c   "   $
 Left=�6E   $ Center=�6W   $
 None=�6k    5d   &   �� 5e   *   $5��/메시지 전송 받을 채팅방 이름 입력��60   
�������6N   5h   #   5i       �� Triggers 5j      P� 5k   "    P� Property � �� �� Empty P� Value# Z 6:   . �5l   &   ��  � ��# [ 6C   5m   #   P� 5n   "    � �� 6:   Z� . �5o   &   ��  � ��# [ 6C   5p   #   P� 5q   "    �@J�IsKeyboardFocused . . �5r   &   ��  � ��
 �����6C   5s   #   5u      .��5v   %   5w      �� 5x      -   5        $��Dlg_TakeOrderbtn��5x   &   $��2��6   $��10,0,0,0q�6>   $��맑은고딕)�6r   $��카카오톡 전송��6�   $
��Left=�6�   $	��Top=�6�   $	��100��6�   $��28��6�   #��[  6�   $
��Bold"�6*  ��6�   �� *	ButtonCmd+6*  5y      5z      5{      5|      5}      
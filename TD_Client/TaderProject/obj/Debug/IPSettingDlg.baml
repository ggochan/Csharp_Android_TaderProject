   M S B A M L   `   `   `  ����G  CTaderProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:TaderProjectTaderProject  5   	       TaderProject.IPSettingDlg   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:TaderProject     5   	   $	��125��5   	   $	��370��6     � Icon$   Resources/tdicon_white.png��6"    � 
ResizeMode$ CanMinimize=�6D    � WindowStyle$
 None=�6]    � 	Resources 5	         BlueButtonStyle �� 
TargetType  BasedButtonStyle �� BasedOn �� Property �� Value �� 
TargetType	 ��Name
 �� CornerRadius �� Triggers  WhiteButtonStyle  BasedButtonStyle2��%�  &	        7  &	 M    7  �� 5
   
   " ��6(   8   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� $	 Border��5      $
 2��5      $��1q�5      ���#355ea9
������ ._�5      �� $��2q�5      $��Center=�5      $��Center=�5      ��. 5      5       5      5      5      5      �� 5    
   " ��6)   8   .�5!      ��  � �� 5"      �� 5#      " ��6&   .R�5$      �� $	 Border��5%      $
 2��5&      $��1q�5'      
�������
������ ._�5*      �� $��2q�5+      $��Center=�5,      $��Center=�5-      ��. 5.      5/       50      52      54      55      57      .��58      �  � RowDefinitions 59   
   �� 5:      $��30 �6   �� 5;      $��* �6   .+�5=   
   � $��0��6   .+�5?      �� -   5        $
��Left=�5?      $��30��60   $��5,5,0,0q�6<   $	��Top=�6M   $��30��6e   .+�5A      �� $ ��Resources/tdicon_white.png��6   5B      �� 5C      -   5        
�������$��	35,0,80,0q�5D   $   5F      ]� 5G      $��Right=�6   .+�5H      �� -   5         ��Name$ ToMiniButton��5H      $��40��5J      $	��ㅡ��5K      $��30��5L      #��[ 6C   $
��Bold"�6m   ��6%   �� *	ButtonCmd+6�   �� 5M      -   5        $ CloseButton��5M      $��40��5O      $��X��5P      $��30��5Q      #��[ 6C   $
��Bold"�6m   ��6%   �� *	ButtonCmd+6�   5S      5U      � 5V   
   $��1 ��6   .+�5W      �� $��10(�6   $
��Bold"�6"   $6��0서버 IP 주소 입력 예시) 175.32.4.51:5656��64   $
��Left=�6_   $��27��6z   $��	27,13,0,0q�6�   $	��Top=�6�   $	��216��6�   �� 5X      -   5         ��Name$ IPSettingTB��5X   #   $��1��6   $
��Left=�69   $��22��6T   $��	27,45,0,0q�6`    �� TextWrapping$ NoWrap=�6s   $	��Top=�6�   $	��216��6�   .��6�   ��5Y      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5Z      �� 6   6sys&clr-namespace:System;assembly=mscorlib     6   " ��6P    �� 	Resources 5[        CueBannerBrush j� 
AlignmentX j� 
AlignmentY j� Stretch !� Visual��%�   &	       !� 5\      $
 Left=�6A   $ Center=�6S   $
 None=�6g    5]   "   �� 5^   &   $-��'서버 IP 주소를 입력해주세요.��6,   
�������6H   5a      5b       �� Triggers 5c      P� 5d       P� Property � �� �� Empty P� Value# Z 66   . �5e   "   ��  � ��# [ 6?   5f      P� 5g       � �� 66   Z� . �5h   "   ��  � ��# [ 6?   5i      P� 5j       �@J�IsKeyboardFocused . . �5k   "   ��  � ��
 �����6?   5l      5n      .��5o   !   5p      �� 5q      -   5        $ IPSettingBtn��5q      $��확인��6+   $
��Left=�6S   $��
262,45,0,0q�6n   $	��Top=�6�   $��79��6�   $��22��6�   #��[  6�   $
��Bold"�6�   ��6�   �� *	ButtonCmd+6�   5r      �� 5s   
   ���#355ea9$��1q�6'   $��2��6;   $
��Left=�6O   $	��125��6j   $	��Top=�6w   $	��370��6�   $��2��6�   5t      5u      
   M S B A M L   `   `   `  ���� G  CTaderProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:TaderProjectTaderProject      TaderProject.IPSettingDlg   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:TaderProject     $	��125��$	��370��  � Icon$   Resources/tdicon_white.png�� � 
ResizeMode$ CanMinimize=� � WindowStyle$
 None=� � 	Resources    BlueButtonStyle �� 
TargetType  BasedButtonStyle �� BasedOn �� Property �� Value �� 
TargetType	 ��Name
 �� CornerRadius �� Triggers  WhiteButtonStyle  BasedButtonStyle2��%Z  &	        7  &	 �     7  �� " ��8   .���  � �� �� " ��.R��� $	 Border��$
 2��$��1q����#355ea9
������ ._��� $��2q�$��Center=�$��Center=���.  �� " ��8   .���  � �� �� " ��.R��� $	 Border��$
 2��$��1q�
�������
������ ._��� $��2q�$��Center=�$��Center=���.  .���  � RowDefinitions �� $��30 ��� $��* �.+�� $��0��.+��� -   $
��Left=�$��30��$��5,5,0,0q�$	��Top=�$��30��.+��� $ ��Resources/tdicon_white.png���� -   
�������$��	35,0,80,0q�]� $��Right=�.+��� -    ��Name$ ToMiniButton��$��40��$	��ㅡ��$��30��#��[ $
��Bold"����� *	ButtonCmd+�� -   $ CloseButton��$��40��$��X��$��30��#��[ $
��Bold"����� *	ButtonCmd+� $��1 ��.+��� $��10(�$
��Bold"�$6��0서버 IP 주소 입력 예시) 175.32.4.51:5656��$
��Left=�$��27��$��	27,13,0,0q�$	��Top=�$	��216���� -    ��Name$ IPSettingTB��$��1��$
��Left=�$��22��$��	27,45,0,0q� �� TextWrapping$ NoWrap=�$	��Top=�$	��216��.����O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem �� 6sys&clr-namespace:System;assembly=mscorlib     " �� �� 	Resources   CueBannerBrush j� 
AlignmentX j� 
AlignmentY j� Stretch !� Visual��%t   &	       !� $
 Left=�$ Center=�$
 None=� �� $-��'서버 IP 주소를 입력해주세요.��
������� �� Triggers P�  P� Property � �� �� Empty P� Value# Z . ���  � ��# [ P�  � �� Z� . ���  � ��# [ P�  �@J�IsKeyboardFocused . . ���  � ��
 �����.���� -   $ IPSettingBtn��$��확인��$
��Left=�$��
262,45,0,0q�$	��Top=�$��79��$��22��#��[  $
��Bold"����� *	ButtonCmd+�� ���#355ea9$��1q�$��2��$
��Left=�$	��125��$	��Top=�$	��370��$��2��
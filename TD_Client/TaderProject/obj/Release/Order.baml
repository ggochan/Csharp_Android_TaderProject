   M S B A M L   `   `   `  ���� G  CTaderProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:TaderProjectTaderProject      TaderProject.Order   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:TaderProject     $	��525��$	��370��  � Icon$   Resources/tdicon_white.png�� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � 	Resources    BlueButtonStyle �� 
TargetType  BasedButtonStyle �� BasedOn �� Property �� Value	 �� 
TargetType
 ��Name �� CornerRadius �� Triggers  WhiteButtonStyle  BasedButtonStyle2��%�  &	        7  &	 �     7  '�� >    �� " ��8   .���  � �� �� "	 ��.R��� $
 Border��$ 2��$��1q����#355ea9
������ ._��� $��2q�$��Center=�$��Center=���.  �� " ��8   .���  � �� �� "	 ��.R��� $
 Border��$ 2��$��1q�
�������
������ ._��� $��2q�$��Center=�$��Center=���.  �� " ��.���  � ��$ Stretch=���  � ��$ Center=�.���  � ColumnDefinitions �� $��0* ���  � RowDefinitions �� $��30 ��� $��* �.+�� $��0��$��2��.+��� -   $
��Left=�$��30��$��5,5,0,0q�$	��Top=�$��30��.+��� $ ��Resources/tdicon_white.png���� -   
�������$��	35,0,80,0q�]� $��Right=�.+��� -    ��Name$ ToMiniButton����.  $��40��$	��ㅡ��$��30��#��[ $
��Bold"����� *	ButtonCmd+�� -   $ CloseButton����.  $��40��$��X��$��30��#��[ $
��Bold"����� *	ButtonCmd+�� ���#355ea9$��1q�$��2��$
��Left=�$	��525��$	��Top=�$	��370��$��2��� $��1��$��2�� �� $
��172* ��� $��6* ��� $��7* �.+��� $��10(�$
��Bold"�$9��3※ 입력 후 Enter를 치면 검색이 됩니다.��$
��Left=�$��27��$��	30,28,0,0q�$	��Top=�$	��231���� $��10(�$
��Bold"�$��전체��$
��Left=�$��27��$��
317,28,0,0q�$	��Top=�$��32��$��2���� -   $ TakeOrderbtn��$��주문��$
��Left=�$��
54,394,0,0q�$	��Top=�$	��265��$��40��#��[  $
��Bold"����� *	ButtonCmd+�� -    ��Name$ 
FoodKindTB��$��1��$
��Left=�$��22��$��	31,54,0,0q� �� TextWrapping$ NoWrap=�$	��Top=�$	��216������  �� Path$ 	ComboKind� �� UpdateSourceTrigger$ PropertyChanged=�.����O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem �� 6sys&clr-namespace:System;assembly=mscorlib     " �� �� 	Resources   CueBannerBrush j� 
AlignmentX j� 
AlignmentY j� Stretch !� Visual��%h   &	       !� $
 Left=�$ Center=�$
 None=� �� $!��음식 종류(직접입력)��
������� �� Triggers P�  P� Property � �� �� Empty P� Value# Z . ���  � ��# [ P�  � �� Z� . ���  � ��# [ P�  �@J�IsKeyboardFocused . . ���  � ��
 �����.���� -   $ StoreNameTB��$��2��$
��Left=�$��22��$��
31,108,0,0q�$ NoWrap=�$	��Top=�$	��216��.����1&clr-namespace:System;assembly=mscorlibSystem �� 6sys&clr-namespace:System;assembly=mscorlib     " �� ��%d   &	       !� $
 Left=�$ Center=�$
 None=� �� $��가게명(직접입력)��
������� P�  � ��# Z . ���  � ��# [ P�  � �� Z� . ���  � ��# [ P�  �@J�IsKeyboardFocused . . ���  � ��
 �����.���� -   $ OrderMenuTB��$��3��$
��Left=�$��22��$��
30,162,0,0q�$ NoWrap=�$	��Top=�$	��217��.����1&clr-namespace:System;assembly=mscorlibSystem �� 6sys&clr-namespace:System;assembly=mscorlib     " �� ��%k   &	       !� $
 Left=�$ Center=�$
 None=� �� $$��주문자 메뉴(직접입력)��
������� P�  � ��# Z . ���  � ��# [ P�  � �� Z� . ���  � ��# [ P�  �@J�IsKeyboardFocused . . ���  � ��
 �����.���� -	   $ OptionTB��$��4��$
��Left=�$��22��$��
30,216,0,0q�$ NoWrap=�$	��Top=�$	��217��.����1&clr-namespace:System;assembly=mscorlibSystem �� 6sys&clr-namespace:System;assembly=mscorlib     " �� ��%a   &	       !� $
 Left=�$ Center=�$
 None=� �� $��옵션(직접입력)��
������� P�  � ��# Z . ���  � ��# [ P�  � �� Z� . ���  � ��# [ P�  �@J�IsKeyboardFocused . . ���  � ��
 �����.���� -
    ��Name$ 
FoodKindCB����.   �� 
IsEditable .   �� DisplayMemberPath$ 	ComboKind��$��22��$��
246,54,9,0q�$	��Top=�  J� RenderTransformOrigin$  0.142,0.867&����#355ea9$��2���� -   $ StoreNameCB����.  $ 
ComboStore��$��22��$��246,108,9,0q�$	��Top=�$  0.142,0.867&� .  $��2���� -   $ 
OderMenuCB����.  $ 	MenuCombo��$��22��$��246,162,9,0q�$	��Top=�$  0.142,0.867&� .  $��2���� -   $ OptionCB����.  $ OptionCombo��$��22��$��246,216,9,0q�$	��Top=�$  0.142,0.867&� .  $��2���� -   $ UserCountCB��$��5��$��22��$��287,271,9.412,0q�$	��Top=�$  0.142,0.867&� .  $��2���� $��10(�$
��Bold"�$��	인원수��$
��Left=�$��27��$��245,270,0,0q�$	��Top=�$��44��
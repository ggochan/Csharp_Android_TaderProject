   M S B A M L   `   `   `  ���� G  CTaderProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:TaderProjectTaderProject  "    TaderProject.OrderManagement   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:TaderProject     $	��525��$��1331.5��  � 
ResizeMode$  CanMinimize=� � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � 	Resources    RedButtonStyle �� 
TargetType  BasedButtonStyle �� BasedOn �� Property �� Value �� 
TargetType	 ��Name
 �� CornerRadius �� Triggers  BlueButtonStyle  WhiteButtonStyle  BasedButtonStyle2��%�  &	        7  &	 �     7  &	 B    7  '�� �    '�� %    �� " ��8   .���  � �� �� " ��.R��� $	 Border��$
 2��$��1q����#e46612
������ ._��� $��2q�$��Center=�$��Center=���.  �� " ��8   .���  � �� �� " ��.R��� $	 Border��$
 2��$��1q����#355ea9
������ ._��� $��2q�$��Center=�$��Center=���.  �� " ��8   .���  � �� �� " ��.R��� $	 Border��$
 2��$��1q�
�������
������ ._��� $��2q�$��Center=�$��Center=���.  �� " ��.���  � ��$ Center=���  � ��$ Center=�-   �� " ��.���  � ��$ Stretch=���  � ��$ Center=�.���  � ColumnDefinitions �� $
��213* ��� $	��41* � � RowDefinitions �� $��30 ��� $��* �.+�� $��0��$��2��.+��� -   $
��Left=�$��30��$��5,5,0,0q�$	��Top=�$��30��.+��� $ ��Resources/tdicon_white.png���� -   
�������$��	35,0,80,0q�]� $��Right=�.+��� -    ��Name$ ToMiniButton��$��40��$	��ㅡ��$��30��#��[ $
��Bold"����� *	ButtonCmd+�� -   $ CloseButton��$��40��$��X��$��30��#��[ $
��Bold"����� *	ButtonCmd+� $��1��$��0,2,0,-2q�$��2��.+��� -   $ M_FoodKindDel��$��삭제��$
��Left=�$��	26,16,0,0q�$	��Top=�$��44��$��23��#��[  $
��Bold"�$��10(����� *	ButtonCmd+�� -   $ M_FoodKindAdd��$��추가��$
��Left=�$��
127,16,0,0q�$	��Top=�$��44��$��23��#��[ $
��Bold"�$��10(� J� RenderTransformOrigin$ -0.477,0.522&����� *	ButtonCmd+�� -   $ M_FoodKindUpd��$��수정��$
��Left=�$��	76,16,0,0q�$	��Top=�$��44��$��23��#��[ $
��Bold"�$��10(����� *	ButtonCmd+�� -	    ��Name$ M_FoodKindTB��$
��Left=�$��22��$��	26,47,0,0q� �� TextWrapping$ NoWrap=�$	��Top=�$	��145��.����O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem �� 6sys&clr-namespace:System;assembly=mscorlib     " �� �� 	Resources   CueBannerBrush j� 
AlignmentX j� 
AlignmentY j� Stretch !� Visual��%h   &	       !� $
 Left=�$ Center=�$
 None=� �� $!��음식 종류(직접입력)��
������� �� Triggers P�  P� Property � �� �� Empty P� Value# Z . ���  � ��# [ P�  � �� Z� . ���  � ��# [ P�  �@J�IsKeyboardFocused . . ���  � ��
 �����.���� -
    ��Name$ M_FoodKindLi��$	��390��$��26,81,1161,0q�$	��Top=�$��Disabled=�$��Disabled=� �� View �� ! �� ColumnHeaderContainerStyle �� # [B��" ��.���  �@J�IsHitTestVisible .  ��  � ��$ Center=�.P��� $��종류�� �� Width$	 140�� �� DisplayMemberBinding �� *	ComboKind+�� -   $ M_StoreNameDel��$��삭제��$
��Left=�$��
194,16,0,0q�$	��Top=�$��44��$��23��#��[  $
��Bold"�$��10(����� *	ButtonCmd+�� -   $ M_StoreNameAdd��$��추가��$
��Left=�$��
295,16,0,0q�$	��Top=�$��44��$��23��#��[ $
��Bold"�$��10(�$ -0.477,0.522&����� *	ButtonCmd+�� -   $ M_StoreNameUpd��$��수정��$
��Left=�$��
244,16,0,0q�$	��Top=�$��44��$��23��#��[ $
��Bold"�$��10(����� *	ButtonCmd+�� -   $ M_StoreNameTB��$
��Left=�$��22��$��
194,47,0,0q�$ NoWrap=�$	��Top=�$	��145��.����1&clr-namespace:System;assembly=mscorlibSystem �� 6sys&clr-namespace:System;assembly=mscorlib     " �� ��%d   &	       !� $
 Left=�$ Center=�$
 None=� �� $��가게명(직접입력)��
������� P�  � ��# Z . ���  � ��# [ P�  � �� Z� . ���  � ��# [ P�  �@J�IsKeyboardFocused . . ���  � ��
 �����.���� -   $ M_StoreNameLi��$	��390��$��194,81,993,0q�$	��Top=�$��Disabled=�$��Disabled=� ��  �� # [B��" ��.���  �@J�IsHitTestVisible .  ��  � ��$ Center=�.P��� $��	가게명��$	 140�� �� *
ComboStore+�� -   $ M_MenuNameDel��$��삭제��$
��Left=�$��
404,16,0,0q�$	��Top=�$��44��$��23��#��[  $
��Bold"�$��10(����� *	ButtonCmd+�� -   $ M_MenuNameAdd��$��추가��$
��Left=�$��
505,16,0,0q�$	��Top=�$��44��$��23��#��[ $
��Bold"�$��10(�$ -0.477,0.522&����� *	ButtonCmd+�� -   $ M_MenuNameUpd��$��수정��$
��Left=�$��
454,16,0,0q�$	��Top=�$��44��$��23��#��[ $
��Bold"�$��10(����� *	ButtonCmd+�� -   $ M_MenuNameTB��$
��Left=�$��22��$��
364,47,0,0q�$ NoWrap=�$	��Top=�$	��120��.����1&clr-namespace:System;assembly=mscorlibSystem �� 6sys&clr-namespace:System;assembly=mscorlib     " �� ��%a   &	       !� $
 Left=�$ Center=�$
 None=� �� $��메뉴(직접입력)��
������� P�  � ��# Z . ���  � ��# [ P�  � �� Z� . ���  � ��# [ P�  �@J�IsKeyboardFocused . . ���  � ��
 �����.���� -   $ M_MenuPriceTB��$
��Left=�$��22��$��
487,47,0,0q�$ NoWrap=�$	��Top=�$��62��$ 0.5,0.5&�.��z�V� .~��� �� ��   �� Angle$  0.166Q�T� .����1&clr-namespace:System;assembly=mscorlibSystem �� 6sys&clr-namespace:System;assembly=mscorlib     " �� ��%S   &	       !� $
 Left=�$ Center=�$
 None=� �� $��가격��
������� P�  � ��# Z . ���  � ��# [ P�  � �� Z� . ���  � ��# [ P�  �@J�IsKeyboardFocused . . ���  � ��
 �����.���� -   $ M_MenuNameLi��$	��185��$	��390��$��364,81,783,0q�$	��Top=�$��Disabled=�$��Disabled=� ��  �� # [B��" ��.���  �@J�IsHitTestVisible .  ��  � ��$ Center=�.P��� $��메뉴��$	 120�� �� *	ComboMenu+�� $��가격��$ 60�� �� *ComboMenuPrice+�� -   $ M_OptionDel��$��삭제��$
��Left=�$��
635,16,0,0q�$	��Top=�$��44��$��23��#��[  $
��Bold"�$��10(����� *	ButtonCmd+�� -   $ M_OptionAdd��$��추가��$
��Left=�$��
736,16,0,0q�$	��Top=�$��44��$��23��#��[ $
��Bold"�$��10(�$ -0.477,0.522&����� *	ButtonCmd+�� -   $ M_OptionUpd��$��수정��$
��Left=�$��
685,16,0,0q�$	��Top=�$��44��$��23��#��[ $
��Bold"�$��10(����� *	ButtonCmd+�� -   $ 
M_OptionTB��$
��Left=�$��22��$��
575,47,0,0q�$ NoWrap=�$	��Top=�$	��140��.����1&clr-namespace:System;assembly=mscorlibSystem �� 6sys&clr-namespace:System;assembly=mscorlib     " �� ��%a   &	       !� $
 Left=�$ Center=�$
 None=� �� $��옵션(직접입력)��
������� P�  � ��# Z . ���  � ��# [ P�  � �� Z� . ���  � ��# [ P�  �@J�IsKeyboardFocused . . ���  � ��
 �����.���� -   $ M_OptionPriceTB��$
��Left=�$��22��$��
718,47,0,0q�$ NoWrap=�$	��Top=�$��62��$ 0.5,0.5&�.��z�V� .~��� �� �� $  0.166Q�T� .����1&clr-namespace:System;assembly=mscorlibSystem �� 6sys&clr-namespace:System;assembly=mscorlib     " �� ��%S   &	       !� $
 Left=�$ Center=�$
 None=� �� $��가격��
������� P�  � ��# Z . ���  � ��# [ P�  � �� Z� . ���  � ��# [ P�  �@J�IsKeyboardFocused . . ���  � ��
 �����.���� -   $ 
M_OptionLi��$	��390��$��575,81,552,0q�$	��Top=�$��Disabled=�$��Disabled=� ��  �� # [B��" ��.���  �@J�IsHitTestVisible .  ��  � ��$ Center=�.P��� $��옵션��$	 140�� �� *ComboOption+�� $��가격��$ 60�� �� *ComboOptionPrice+�� -   $ M_PastAllTB��$
��Left=�$��22��$��1104,48,0,0q�$ NoWrap=�$	��Top=�$	��156��.����1&clr-namespace:System;assembly=mscorlibSystem �� 6sys&clr-namespace:System;assembly=mscorlib     " �� ��%d   &	       !� $
 Left=�$ Center=�$
 None=� �� $��기준에 따른 입력��
������� P�  � ��# Z . ���  � ��# [ P�  � �� Z� . ���  � ��# [ P�  �@J�IsKeyboardFocused . . ���  � ��
 �����.���� -   ! ��Name$! M_PastAllCB����.  $��22��$��991,48,233,0q�$	��Top=�$ 0.142,0.867&�" �� 
IsEditable" .  �� -   $ M_PastAllLI��$	��390��$��808,81,23,0q�$	��Top=�$��Disabled=�$��Disabled=� ��  �� # [B��" ��.���  �@J�IsHitTestVisible .  ��  � ��$ Center=�.P��� $��종류��$	 100�� �� *PastKindName+�� $��	가게명��$	 100�� �� *PastStoreName+�� $��메뉴��$	 146�� �� *PastMenuName+�� $��옵션��$	 150�� �� *PastOptionDes+�� -   $ M_PastAllbtn��$��찾기��$
��Left=�$��1263,47,0,0q�$	��Top=�$��44��$��23��#��[ $
��Bold"�$��10(�$ -0.477,0.522&����� *	ButtonCmd+�� $��10(�$
��Bold"�$C��=※직접입력시 문자를 완성 후 Enter를 쳐주세요��$
��Left=�$��27��$��	26,-6,0,0q�$	��Top=�$	��231���� ���#355ea9$��1q�$
��Left=�$	��525��$	��Top=�$��1331.5��$��2��$��2��
   M S B A M L   `   `   `  ���� G  CTaderProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:TaderProjectTaderProject      TaderProject.OrderList   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:TaderProject     $	��525��$	��370��  � Icon$   Resources/tdicon_white.png�� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � 	Resources    BlueButtonStyle �� 
TargetType  BasedButtonStyle �� BasedOn �� Property �� Value	 �� 
TargetType
 ��Name �� CornerRadius �� Triggers  WhiteButtonStyle  BasedButtonStyle2��%�  &	        7  &	 �     7  '�� >    �� " ��8   .���  � �� �� "	 ��.R��� $
 Border��$ 2��$��1q����#355ea9
������ ._��� $��0q�$��Center=�$��Center=���.  �� " ��8   .���  � �� �� "	 ��.R��� $
 Border��$ 2��$��1q�
�������
������ ._��� $��2q�$��Center=�$��Center=���.  �� " ��.���  � ��$ Center=���  � ��$ Center=�.���  � ColumnDefinitions �� $
��281* ��� �� $	��88* � � RowDefinitions �� $��30 ��� $��5 ��� $��80 ��� $��* �.+�� $��0��$��3��.+��� -   $
��Left=�$��30��$��5,5,0,0q�$	��Top=�$��30��.+��� $ ��Resources/tdicon_white.png���� -   
�������$��	35,0,80,0q�]� $��Right=�.+��� -    ��Name$ ToMiniButton��$��40��$	��ㅡ��$��30��#��[ $
��Bold"����� *	ButtonCmd+�� -   $ CloseButton��$��40��$��X��$��30��#��[ $
��Bold"����� *	ButtonCmd+� $��2�����#355ea9$��3��.+��� 
�������$
��Bold"�$��음식 종류 :��$
��Left=�$��	21,12,0,0q�$	��Top=�$��29��$��83��$��13(��� 
�������$
��Bold"�$��
가게명:��$
��Left=�$��	21,41,0,0q�$	��Top=�$��29��$��66��$��13(��� -    ��Name$ li_FoodKindLA��$��Right=�
�������$
��Bold"�$
��Left=�$��
283,12,0,0q�$	��Top=�$��29��$��66�� J� RenderTransformOrigin$ -0.066,3.79&�$��13(��� -   $ li_StoreNameLA��$��Right=�
�������$
��Bold"�$��	0,41,21,0q�$	��Top=�$��29��$��13(�$��Right=�$��66��� $��3��$��3��.+��� -    ��Name$ li_OrderMenuList��$
��Left=�$	��345��$��	10,10,0,0q�$	��Top=�$	��350��$��Disabled=�$��Disabled=� �� View �� ! �� ColumnHeaderContainerStyle �� # [B��" ��.���  �@J�IsHitTestVisible .  ��  � ��$ Center=�.P��� $��번호�� �� Width$ 45�� �� DisplayMemberBinding �� *Num+�� $��메뉴��$	 223�� �� *Menu+�� $��수량��$ 45�� �� *
Menu_Count+�� $��+��$ 32�� �� CellTemplate �� .R��� -   $��+��$��15��$��15��$	��Top=�$��Center=�$��9(�#��[  ���� *	ButtonCmd+�� -	   $ 
MenuAddbtn��$��메뉴 추가��$
��Left=�$��266,365,0,0q�$	��Top=�$��94��$��35��#��[  $
��Bold"����� *	ButtonCmd+�� ���#355ea9$��1q�$
��Left=�$	��525��$	��Top=�$	��370��$��4��$��3��
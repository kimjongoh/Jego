   M S B A M L   `   `   `  ����  Jego     Jego.Dialogs.TypeAddDialog   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  AlertDialog��5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency .  � 
ResizeMode$ NoResize=�5   	   
������ .��5
      � $	��350��6   $��Center=�6   
������� � RowDefinitions 5   
   �� 5      $��45 �6   �� 5      $��* �6   �� 5      $��45 �6   .+�5   
   �� -   5        $��title_textBlock��5      $)��#새 카테고리를 입력하세요��6+      mainBoldTextBlock15#��[  6@   $��	10,0,10,0q�6m   �� 5   
   -   5        $��message_textBlock��5      $��1��6   $��Stretch=�68   $��Center=�6W   $��10,15q�6r   � 5   
   $��2��6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   .+�5      �� -   5        $��positive_button��5      $��0��6,   $
��auto��6<   $
��auto��6J   $��0q�6W   $��Stretch=�6l   $��Stretch=�6�   $��확인��6�   6�   �� 5      -   5        $��negative_button��5      $��1��6,   $
��auto��6<   $
��auto��6J   $��0q�6W   $��Stretch=�6k   $��Stretch=�6�   $��취소��6�   6�   5      5      5      
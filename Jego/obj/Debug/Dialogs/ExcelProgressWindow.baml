   M S B A M L   `   `   `  ����  Jego&     Jego.Dialogs.ExcelProgressWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � WindowStartupLocation$  Manual=�5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency .  � 
ResizeMode$ NoResize=�5   	   
������  � Title$ ExcelProgressWindow��5	   	   $	��200��6%   $	��400��62   .��5
      � .+�5   
   � $��Center=�6   $��80��6*   $	��400��66   
������� � RowDefinitions 5      �� 5      $��20 �6    �� 5      $
��auto �6    .+�5      �� $1��+엑셀 문서를 생성하고 있습니다.��6   $
��10,0q�61   � 5      -   5        $��progressbar��5      $��1��6-   $	��350��6:   $��20��6F    � Minimum$ 0Q�6R    � Maximum$	 100Q�6^   $
��0,10q�6m   5      5      5      
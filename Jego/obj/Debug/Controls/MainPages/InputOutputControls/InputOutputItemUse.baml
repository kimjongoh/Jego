   M S B A M L   `   `   `  ����?  ;Jego, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulli9clr-namespace:Jego.Controls.MainPages.InputOutputControls+Jego.Controls.MainPages.InputOutputControls  5      -clr-namespace:Jego.CommandsJego.Commands  5      D    >Jego.Controls.MainPages.InputOutputControls.InputOutputItemUse   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jthis9clr-namespace:Jego.Controls.MainPages.InputOutputControls     5      /commandclr-namespace:Jego.Commands     5      -   5          >�Name$  thisControl��5       � 	Resources 5
      "   Jego.Commands.NewFoodCommand   newFoodCommand��%   &	          5   
   6!   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   �� -   5        $��amount_TextBox��5   "   $��0��6      foodAmount_InputOutput_width#��[ 68   $��Center=�6n   $��25��6�   $	��5,5q�6�   .��6   J� InputBindings 5      �� 5      
 �� Key$ Enter��6    �� 	Modifiers$ Controlf�6)    �� Command# [  6=    �� CommandParameter 6g   ��  �� ElementName$ thisControl��.��5   %   	 � ContextMenu	 5      �� 5      .@�5      m� -   5        $��삭제��5      6+   5      .��5   #   5      �� 5   
   -   5        $��unitPrice_TextBlock��5   $   $��1��6     foodPrice_InputOutput_width#��[ 6?   $��Center=�6t   $��25��6�   
�������$	��5,5q�6�   
 �� TextAlignment$
 Right=�6�   �� 5   
   -   5        $��totalPrice_TextBlock��5   $   $��2��6    $  foodTotalPrice_InputOutput_width#��[ 6@   $��Center=�6z   $��25��6�   
�������$	��5,5q�6�   $
 Right=�6�   5       5!      
   M S B A M L   `   `   `  ����A  =Sudoku, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:SudokuSudoku  5   	       Sudoku.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Sudoku     5   	     � Title$  
MainWindow��5   	   $	��600��6   $	��800��6)   .��5
      �  � RowDefinitions 5   
   �� 5      $��30 �6   6*   �� 5      $��40 �6   6*   �� 5      $��* �6   6)   �� 5      $��40 �6   6*    � ColumnDefinitions 5   
   �� 5      $��* �6   6+   �� 5      $	��360 �6   6-   �� 5      $��* �6   6+   .+�5   
   �� $��0��6   $��1��6!   $��Sudoku��61   $��20(�6?    �� TextAlignment$ Center=�6M   6f   �� 5   
   -   5        $��SelectValue��5      $��1��6&   $��1��63   $��Center=�6C   $
��Left=�6^   $��30��6y   $��40��6�   
�������$y�Hidden=�6�    �� SelectedIndex$ 0��6�   5      �� 5   
   -   5         ��Name$ SudokuItems��5      $��2��6,   $��1��69   $��Center=�6I   ��6d   �� *Puzzle+��5      �� 5       .R�5!      A�  A� Columns$ 9��6"   60   5"      ��5%      �� 5&      .R�5'      �� $ 
SudokuRows��6#   $��Center=�67   ��6R   �� ��5(      �� 5)      .R�5*   "   A�  A� Rows$ 9��6.   5+   #   5,      ��5.      �� 5/      .R�50   "   �� $��
SudokuCell��6)   -   5        
�������$��40��50   f   $��40��6q   ��6;   �� 6�   51      53      54      57      �� 59   
   -   5        $��ButtonsList��59      $��3��6%   $��1��62   $��30q�6B   .@�5:      �� -   5        $
��Save��5:      
���P��$
��Save��64   $��20q�6C   6d   �� 5;      -   5        $��Reveal��5;      
���P��$��Reveal��66   $��20q�6G   6j   �� 5<      -   5        $��Validate��5<      
���P��$��Validate��68   $��20q�6K   6p   5=      5>       � 	Resources 5A         blah	 �� DataType��%I   &	        �� 5B   
   $	 string��6$   .R�5C      �� 6   5D      5E      5b      
   M S B A M L   `   `   `  ����A  =Sudoku, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:SudokuSudoku  5   	       Sudoku.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Sudoku     5   	     � Title$  
MainWindow��5   	   $	��600��6   $	��800��6)   .��5
      �  � RowDefinitions 5   
   �� 5      $��30 �6   6*   �� 5      $��40 �6   6*   �� 5      $��* �6   6)   �� 5      $	��100 �6   6+    � ColumnDefinitions 5   
   �� 5      $��* �6   6+   �� 5      $	��360 �6   6-   �� 5      $��* �6   6+   .+�5   
   �� $��0��6   $��1��6!   $=��7Click on box and pick value from combo box that appears��61   6r   �� 5   
   $��1��6   $��1��6!   $��Sudoku��61   $��20(�6?   $��Center=�6M   $��Center=�6j    �� TextAlignment$ Center=�6�   6�   �� 5   
   -   5        $
��Save��5      $��1��6   $��1��6*   $��Center=�6:   $��Right=�6U   
�������$��2q�6�   $��Save Puzzle��6�   $��35��6�   $��80��6�   $��2q�6�   6�   �� 5   
   -   5        $��SelectValue��5      $��1��6&   $��1��63   $��Center=�6C   $
��Left=�6^   $��30��6y   $��40��6�   
�������$y�Hidden=�6�    �� SelectedItem$ 0��6�   5       �� 5"   
   -   5         ��Name$ SudokuItems��5"      $��2��6,   $��1��69   $��Center=�6I   ��6d   �� *Puzzle+ �� Mode$ TwoWay=���5#      �� 5$      .R�5%      A� $��
ColumnGrid��6"    A� Rows$ 9��64   6?   5&      ��5)      �� 5*      .R�5+      �� $ 
SudokuRows��6#   $��Center=�67   ��6R   �� ��5,      �� 5-      .R�5.   "   A� $��RowGrid��6.    A� Columns$ 9��6=   5/   #   50      ��52      �� 53      .R�54   "   �� $��
SudokuCell��6)   -   5        $��40��54   �   $��40��6�   ��6;   �� 	 �� Path$	 Digit�$ TwoWay=���6g   �� $	 Color�$ TwoWay=�6�   55      57      58      5;      �� 5=   
   -   5        $��	NewPuzzle��5=      $��3��6"   $��1��6/   $
��Left=�6?   
�������$��2q�6r   $��
New Puzzle��6�   $��32��6�   $��70��6�   $��20q�6�   6�   �� 5>   
   -   5        $��Reveal��5>      $��3��6   $��1��6,   $��Center=�6<   
�������$��2q�6q   $��Reveal��6�   $��20q�6�   $��32��6�   $��50��6�   6�   �� 5?   
   -   5        $��Validate��5?      $��3��6!   $��1��6.   $��Right=�6>   
�������$��2q�6r   $��Validate��6�   $��20q�6�   $��32��6�   $��60��6�   6�   5@      5B      
   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	   -   5          �Name$  
mainWindow��5   	    � Title$ 
MainWindow��5   	   $	��450��6   $	��800��6)    � WindowStartupLocation$ CenterScreen=�6L   .��5	      � -   5         �Name$ GrdMain��5	       � RowDefinitions 5
   
   �� 5      $��64 �6   �� 5      �� 5      $��28.984 �6   �� 5      $��55.016 �6    � ColumnDefinitions 5   
   �� 5      -   5         ��Name$ 	gdColLeft��5      �� 5      .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dgLeft��5      $��	10,45,4,0q�6#   $��1��66      AutoGenerateColumns .  	   GridLinesVisibility$
	 None=�6�   $
 �� IsSynchronizedWithCurrentItem$

 TrueY�6�      
IsReadOnly .    SelectionMode$ Single=�61  ��6[   ��  �� Source$ listLeft��   Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 30 6L      ClipboardContentBinding 6)   Z�    CellTemplate 5      �� 5      .R�5      �� $
��None=�6B   ��6$   �� *
SistemIcon+6B   5      5      0  *System.Windows.Controls.DataGridTextColumn  5         Header$ Filename��6h   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *DisplayName+   ClipboardContentBinding 6E   Z� 6h     5      $	 Ext��6f    6%   �� *	Extension+ 6C   Z� 6f     5      $
 Size��6c    6%   �� *Lenght+ 6@   Z� 6c     5       $ Modified��6j    6%   �� *LastWriteTime+ 6G   Z� 6j   5"        5#   
   -   5        $ dgRight��5#      $��1��6$   $��	5,45,10,0q�64   $��1��6G    .  $
	 None=�6�    . $ Single=�6   �� SelectedIndex$ 0��63  $

 TrueY�6E  ��6k   �� $ listLeft�� 5$        5%      $ Filename��6h    6%   �� *DisplayName+ 6E   Z� 6h     5&      $	 Ext��6f    6%   �� *	Extension+ 6C   Z� 6f     5'      $
 Size��6c    6%   �� *Lenght+ 6@   Z� 6c     5(      $ Modified��6j    6%   �� *LastWriteTime+ 6G   Z� 6j   5*      �� 5+   
   -   5         ��Name$ gdVerticalSpliter��5+      $��1��62   $��1��6?   $��0q�6I   $
��Left=�6T   $��1��6o   � 5,   
   $��2��6   $��30��6#   $��9,0,11,8q�6/   $��3��6A   $��Bottom=�6N    5-      �� 5.      �� 5/      �� 50      �� 51      �� 52      �� 53      .+�55      �� -   5         ��Name$ Btnview��55      $
��View��6&   $��0q�65   6@   �� 56      -   5        $ BtnEdit��56      $
��Edit��6&   $��0q�65   $��1��6@   6P   �� 57      -	   5        $ BtnCopy��57      $
��Copy��6&   $��0q�65   $��2��6@   6P   �� 58      -
   5        $ BtnMove��58      $
��Move��6&   $��0q�65   $��3��6@   6P   �� 59      -   5        $ BtnNew��59      $��
New Folder��6%   $��0q�6:   $��4��6E   6U   �� 5:      -   5        $ 	BtnDelete��5:      $��Delete��6(   $��0q�69   $��5��6D   6T   5;      �� 5<   
   $��296,0,10,53q�6   $��2��6'    �� TextWrapping$
 Wrap=�64   $��TextBox��6H   $��23��6W   $��Bottom=�6c   $��2��6~   $��2��6�   �� 5>   
   -   5         ��Name$ TblComandPath��5>      $��	153,0,0,0q�6+   $��2��6>   $
��Wrap=�6K   $
��path��6_   $��Bottom=�6k   $��19��6�    �� TextAlignment$ Right=�6�   $
��Left=�6�   $	��138��6�   $��2��6�   �� 5@   
   -   5         ��Name$ CmbRightDrive��5@      $��1��6*   $
��Left=�6:   $��7,3,0,0q�6U   $��1��6f   $	��Top=�6s   $��45��6�   6�   �� 5C   
   -   5        $ CmbLeftDrive��5C      $
��Left=�6)   $��10,2,0,0q�6D   $��1��6V   $	��Top=�6c   $��45��6{   6�   �� 5D   
   -   5        $ TblDriveNameLeft��5D      $
��Left=�6.   $��22��6I   $��55,3,0,0q�6U   $��1��6g   $
��Wrap=�6t   $��TblDriveNameLeft��6�   $	��Top=�6�   $	��331��6�   �� 5E   
   -   5        $ TblDriveNameRight��5E      $��1��6/   $
��Left=�6?   $��22��6Z   $��55,3,0,0q�6f   $��1��6x   $
��Wrap=�6�   $��TblDriveNameRight��6�   $	��Top=�6�   $	��330��6�   �� 5G   
   -   5        $ TblPathLeft��5G      $��20��6)   $��	10,25,5,0q�65   $��1��6H   $
��Wrap=�6U   $��TblPathLeft��6i   $	��Top=�6|   ���	#FFAA9EFF6�   �� 5H   
   -   5        $ TblPathRight��5H      $��1��6*   $��21��6:   $��	7,25,11,0q�6F   $��1��6Y   $
��Wrap=�6f   $��TblPathRight��6z   $	��Top=�6�   ���	#FFAA9EFF6�   o� 5I   
   $��2��6   $
��Left=�6#   $��20��6>   $��10,0,0,0q�6J   $	��Top=�6\   $	��771��6t   .@�5J      m� $
��File��6   .@�5K      m� $
��Pack��6   m� 5L      $��Unpak��6   �� 5M      m� 5N      $��Compare by content��6   �� 5O      m� 5P      -   5         m�Name$ MnuEdit��5P      $
��Edit��6,   6:   m� 5Q      -   5        $ MnView��5Q      $
��View��6+   69   �� 5R      m� 5S      -   5        $ MnuExit��5S      $
��Exit��6,   6:   5T      m� 5U      $
��Show��6   .@�5V      m� $
��Full��6   m� 5W      $
��Tree��6   m� 5X      $��Veritcal Arrangement��6   5Y      m� 5Z      $
��Help��6   .@�5[      m� $��About��6   5\      5]      c� 5^   
   $��2��6   $
��Left=�6&   $��39��6A   $��9,20,0,0q�6M   $	��Top=�6_   $	��772��6w   � 5_   
   $��1��6   $
��Left=�6$   $��22��6?   $��	1.2,3,0,0q�6K   $��1��6^   $	��Top=�6k   $	��383��6�   5`      5a      
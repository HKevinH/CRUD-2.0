   M S B A M L   `   `   `  ����C  ?CRUD-2.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CRUD_2._0	CRUD_2._0  5          CRUD_2._0.UISearcher   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:CRUD_2._0     5      -   5          >�Name$  userControl��5      $	��850��5   7   $	��560��6C   .��5	      �  � RowDefinitions 5
   
   �� 5      $��2* �6   �� 5      $	��73* �6    � ColumnDefinitions 5   
   �� 5      $
��567* �6   �� 5      $
��233* �6   .+�5   
   �� -   5         ��Name$ comboBoxInner��5      $��98,53,42,355q�6*   $��30��6V   $��1��6b   $��1��6�   
���   ���6@   Z� ��6�   Z� 6�   �� 5   
   -   5        $ comboBoxFrom��5      $��276,54,107,354q�6)   $��30��6W   $��1��6�   
���   ���6A   Z� ��6�   Z� 6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGridFilter��5      $	��209��6V   $��276,108,50,120q�6c   $��2��6�   $��1��6�   ���	#FFC5AD386�   �� 5   
   -   5         ��Name$ label��5      $��Columnas��6   $��114,69,361,337q�62   $��Friz Quadrata TT)�6J   $��20(�6h   $��1��6v   �� 5   
   -   5        $ 
label_Copy��5      $��Tabla��6$   $��336,28,167,378q�64   $��Friz Quadrata TT)�6L   $��20(�6j   $��1��6x   �� 5   
   -   5        $ label_Copy1��5      $��
Inner Join��6%   $��39,23,92,385q�6:   $��Friz Quadrata TT)�6P   $��20(�6n   $��1��6|   $��1��6�   �� 5   
   -   5         ��Name$ 	btnBuscar��5      $��Friz Quadrata TT)�6$   $��20(�6B   $	��100��6P   $	��310��6]   $��245,328,12,10q�6�   $��BUSCAR��6�   ���	#FFFFE500 J� RenderTransformOrigin$ 0.822,0.463&�6�   $��1��6#  ��6�   Z� ��5      �� 5       �� ImageSource$# /Images/gb_button_default.png��6   	 j� Stretch$
	 Fill=�6I   5      �� 5   
   -	   5        
 ��Name$
 
listBoxCol��5      $��70,108,317,298q�6&   $	��180��6>   $��Friz Quadrata TT)�6J    �� SelectionMode$ Multiple=�6h   $��1��6�   
���   ���6�   Z� 6�   �� 5   
   -
   5        $ comboBoxInnerTa��5      $��516,54,-42,354q�6,   $��30��6Z   $��1��6f   
���   ���6D   Z� 6s   5      5       
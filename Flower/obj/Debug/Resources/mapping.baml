   M S B A M L   `   `   `  ����A  =Flower, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Flower.CommonFlower.Common  5      5clr-namespace:Flower.ViewModelsFlower.ViewModels  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35b ^GalaSoft.MvvmLight.Platform, Version=5.4.1.0, Culture=neutral, PublicKeyToken=5f873c45e98af8a1  �� MergedDictionaries �� Source'   !Flower.Common.VisibilityConverter   VisibilityConverter  ComponentTemplate �� DataType*  $Flower.ViewModels.ComponentViewModel �� Path.  (System.Windows.Interactivity.Interaction   Triggers/  )System.Windows.Interactivity.EventTrigger   	EventName.  (System.Windows.Interactivity.TriggerBase   Actions/  )GalaSoft.MvvmLight.Command.EventToCommand   Command n�Name  SelectionThumb	 � Cursor
 �� 	Converter   PassEventArgsToCommand  ResizeThumbStyle�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      &commonclr-namespace:Flower.Common  5      .
viewmodelsclr-namespace:Flower.ViewModels  5      Bi:http://schemas.microsoft.com/expression/2010/interactivity  6H   .command http://www.galasoft.ch/mvvmlight  6�     5      �� 5   
   $ styles.xaml?�6   %  &	        &	      7  7   7  7   7  7   7  7   7  7   7  7   7  7   7  7   7  7      5	      6!   �� 5      "  6-   .R�5   
   � ��5      �� $ Width���5      �� $ Height���5      �� $ 
Background� 5        5      $ PreviewMouseDown��6!   . 5         6-   �� $ SelectCommand�5      .+�5      �� ��6   �� $ Content�n� 5      $ SelectionThumb��6   8��  $	 SizeAll��5      y�5      �� $ 
IsSelected�8
   5        5      $ 	DragDelta��6%   . 5         .  61   �� $ MoveCommand�5   1   5      5!      n� 5"      $ TopLeftThumb��6   8�� $
��Left=�5$      $	��Top=�6/   $��10��5%      $��10��6   $��	-5,-5,0,0q�6+   $	 SizeNWSE��5&      y�5'      �� $ 
IsSelected�8
   5(        5)      $ 	DragDelta��6%   . 5*         .  61   �� $ ResizeTopLeftCommand�5+   1   5,      5.      n� 5/      $ TopThumb��6   8�� $��Center=�51      $	��Top=�61   $��10��52      $��10��6   $��0,-5,0,0q�6+   $	 SizeNS��53      y�54      �� $ 
IsSelected�8
   55        56      $ 	DragDelta��6%   . 57         .  61   �� $ ResizeTopCommand�58   1   59      5;      n� 5<      $ TopRightThumb��6   8�� $��Right=�5>      $	��Top=�60   $��10��5?      $��10��6   $��	0,-5,-5,0q�6+   $	 SizeNESW��5@      y�5A      �� $ 
IsSelected�8
   5B        5C      $ 	DragDelta��6%   . 5D         .  61   �� $ ResizeTopRightCommand�5E   1   5F      5H      n� 5J      $ 	LeftThumb��6   8�� $
��Left=�5L      $��Center=�6/   $��10��5M      $��10��6   $��-5,0,0,0q�6+   $	 SizeWE��5N      y�5O      �� $ 
IsSelected�8
 	  5P        5Q      $ 	DragDelta��6%   . 5R         .  61   �� $ ResizeLeftCommand�5S   1   5T      5V      n� 5W      $ 
RightThumb��6   8��
 $��Right=�5Y      $��Center=�60   $��10��5Z      $��10��6   $��0,0,-5,0q�6+   $	 SizeWE��5[      y�5\      �� $ 
IsSelected�8
   5]        5^      $ 	DragDelta��6%   . 5_         .  61   �� $ ResizeRightCommand�5`   1   5a      5c      n� 5e      $ BottomLeftThumb��6   8�� $
��Left=�5g      $��Bottom=�6/   $��10��5h      $��10��6   $��	-5,0,0,-5q�6+   $	 SizeNESW��5i      y�5j      �� $ 
IsSelected�8
   5k        5l      $ 	DragDelta��6%   . 5m         .  61   �� $ ResizeBottomLeftCommand�5n   1   5o      5q      n� 5r      $ BottomThumb��6   8�� $��Center=�5t      $��Bottom=�61   $��10��5u      $��10��6   $��0,0,0,-5q�6+   $	 SizeNS��5v      y�5w      �� $ 
IsSelected�8
   5x        5y      $ 	DragDelta��6%   . 5z         .  61   �� $ ResizeBottomCommand�5{   1   5|      5~      n� 5      $ BottomRightThumb��6   8�� $��Right=�5�      $��Bottom=�60   $��10��5�      $��10��6   $��	0,0,-5,-5q�6+   $	 SizeNWSE��5�      y�5�      �� $ 
IsSelected�8
   5�        5�      $ 	DragDelta��6%   . 5�         .  61   �� $ ResizeBottomRightCommand�5�   1   5�      5�      5�      5�      5�      
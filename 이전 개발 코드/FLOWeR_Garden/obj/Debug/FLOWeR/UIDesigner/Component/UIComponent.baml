   M S B A M L   `   `   `  ����N  JFLOWeR_Garden_Ver02, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullq=clr-namespace:FLOWeR_Garden_Ver02.FLOWeR.UIDesigner.Component/FLOWeR_Garden_Ver02.FLOWeR.UIDesigner.Component  5      A    ;FLOWeR_Garden_Ver02.FLOWeR.UIDesigner.Component.UIComponent   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      >d2http://schemas.microsoft.com/expression/blend/2008    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      Olocal=clr-namespace:FLOWeR_Garden_Ver02.FLOWeR.UIDesigner.Component     5      -   5        $
��Auto��5   7   $
��Auto��6D     � 	Resources  5
       �� Source�� 5   
   $/ )../../../Resources/ComponentResource.xaml?�6   %    .��5      � -   5         �Name$
 grid��5      .+�5   
   �� -   5         ��Name$ contentPresenter��5      �� 5   
   -   5         ��Name$ controlText��5      $��Center=�6H   $��Center=�6c   ��6)   ��  �� Path$ TypeName�6c   n� 5   
   -   5         n�Name$ SelectionThumb��5         SelectionThumbStyle#��[  5      $��Stretch=�5      $��Stretch=�6.    � Cursor$ SizeAll��5      y�5      �� $ 
IsSelected�  visibilityConverter �� 	Converter# [ 	 �� RelativeSource	 �� 
 �� AncestorType"
 >�n� 5   
   -   5        $ TopLeftThumb��5        ResizeThumbStyle#��[ 5      $
��Left=�5      $	��Top=�6+   $��10��5      $��10��6   $��	-5,-5,0,0q�6'   $ SizeNWSE��5       y�5!      `� 5"        multiVisibilityConverter `� 	Converter# [ 6   .1�5#      �� $ 
IsSelected�6   	 60   �� "
 >��� 5$      $ 
CanHResize�6   	 60   �� "
 >��� 5%      $ 
CanVResize�6   	 60   �� "
 >�5&      5(      n� 5)   
   -   5        $ TopThumb��5)      #��[ 5*      $��Center=�5,      $	��Top=�6-   $��10��5-      $��10��6   $��0,-5,0,0q�6'   $ SizeNS��5.      y�5/      `� 50      # [ 6   .1�51      �� $ 
IsSelected�6   	 60   �� "
 >��� 52      $ 
CanVResize�6   	 60   �� "
 >�53      55      n� 56   
   -   5        $ TopRightThumb��56      #��[ 57      $��Right=�59      $	��Top=�6,   $��10��5:      $��10��6   $��	0,-5,-5,0q�6'   $ SizeNESW��5;      y�5<      `� 5=      # [ 6   .1�5>      �� $ 
IsSelected�6   	 60   �� "
 >��� 5?      $ 
CanHResize�6   	 60   �� "
 >��� 5@      $ 
CanVResize�6   	 60   �� "
 >�5A      5C      n� 5D   
   -	   5        $ 	LeftThumb��5D      #��[ 5E      $
��Left=�5G      $��Center=�6+   $��10��5H      $��10��6   $��-5,0,0,0q�6'   $ SizeWE��5I      y�5J      `� 5K      # [ 6   .1�5L      �� $ 
IsSelected�6   	 60   �� "
 >��� 5M      $ 
CanHResize�6   	 60   �� "
 >�5N      5P      n� 5Q   
   -
   5        $ 
RightThumb��5Q      #��[ 5R      $��Right=�5T      $��Center=�6,   $��10��5U      $��10��6   $��0,0,-5,0q�6'   $ SizeWE��5V      y�5W      `� 5X      # [ 6   .1�5Y      �� $ 
IsSelected�6   	 60   �� "
 >��� 5Z      $ 
CanHResize�6   	 60   �� "
 >�5[      5]      n� 5^   
   -   5        $ BottomLeftThumb��5^      #��[ 5_      $
��Left=�5a      $��Bottom=�6+   $��10��5b      $��10��6   $��	-5,0,0,-5q�6'   $ SizeNESW��5c      y�5d      `� 5e      # [ 6   .1�5f      �� $ 
IsSelected�6   	 60   �� "
 >��� 5g      $ 
CanHResize�6   	 60   �� "
 >��� 5h      $ 
CanVResize�6   	 60   �� "
 >�5i      5k      n� 5l   
   -   5        $ BottomThumb��5l      #��[ 5m      $��Center=�5o      $��Bottom=�6-   $��10��5p      $��10��6   $��0,0,0,-5q�6'   $ SizeNS��5q      y�5r      `� 5s      # [ 6   .1�5t      �� $ 
IsSelected�6   	 60   �� "
 >��� 5u      $ 
CanVResize�6   	 60   �� "
 >�5v      5x      n� 5y   
   -   5        $ BottomRightThumb��5y      #��[ 5z      $��Right=�5|      $��Bottom=�6,   $��10��5}      $��10��6   $��	0,0,-5,-5q�6'   $ SizeNWSE��5~      y�5      `� 5�      # [ 6   .1�5�      �� $ 
IsSelected�6   	 60   �� "
 >��� 5�      $ 
CanHResize�6   	 60   �� "
 >��� 5�      $ 
CanVResize�6   	 60   �� "
 >�5�      5�      5�      5�      
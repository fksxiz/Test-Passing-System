   M S B A M L   `   `   `  ����C  ?QQCourse, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:QQCourse.PagesQQCourse.Pages  5      %    QQCourse.Pages.TestsBrowserPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:QQCourse.Pages     5        N� Title$  TestsBrowserPage��5	         page#���   5
      .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $
��Auto �6   �� 5      $	��11* �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ��   	Rectangle#���  6   $��2��68   $��0��6B   $��0��6R   $��3��6_   � 5   
    � Orientation$ 
Horizontal=�6   $��Center=�6-   $��0��6J   $��	0,10,0,10q�6W   $��Center=�6j   $��1��6�   .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      ��  
 Search#���  6    �� TextAlignment$ Right=�6<     
HeaderText#���  6R   $��0,0,10,0q�6w   �� 5      -   5         ��Name$ SearchTextBox��5        PrimaryTextBox#���  61   $	��175��6Z   $��0,0,10,0q�6�   5      5      �� 5!   
   $��5��6   $��1��6    #���  6-   $��1��6Q   �� 5#   
   -   5         ��Name$ TestsListView��5#      $��2��6*     ListView#���  67   $��1��6Z   $��Disabled=�5$      $��Center=�6I   ��5&      �� 5'      .R�5(      � $ 
Horizontal=�6    $��Center=�69   5)      ��5+      �� 5,      .R�5-      � $��20q�6    5.      �� 5/      $��4* �6,   �� 50      $��2* �6,   �� 51      $��1* �6,   �� 52      $��1* �6,   .+�54      ��  ��Name$ 	TestImage��6    $	��150��63   $��UniformToFill=�6?   $��Center=�6W   $��5q�6t   $��0��6   ��55      �� 56   "   	 �� Path$	 Image�6*   
 �� TargetNullValue
 57   &   ��58   *   Images/TestImageBlue.png��6P   5:   #   5<      �� 5=       ��Name$ TestNameTextBlock��5>   $   $��Center=�5=   :   $��Center=�6U   $��1��6r   $	��150��6   #���  5>   ?   ��5=   $   �� *Name+5>   ?   �� 5?      $��2��6%   $ 
Horizontal=�62   $��Center=�6K   .+�5@      ��   
TimeToPass#���  6(     
LittleText#���  6L   �� 5A      $ TimeTextBlock��5B   $   $��Center=�5A   >   $��Center=�6Y   #���  5B   ;   ��5A   (   �� *Time+5B   ;   5C      �� 5D      $��3��6%   $ 
Horizontal=�62   $��Center=�6K   .+�5E      ��   MinScoreToPass#���  6(   #���  6P   �� 5F      $ ScoreTextBlock��5G   $   $��Center=�5F   B   $��Center=�6]   #���  5G   <   ��5F   (   �� *
MinScore+5G   <   5H      5I      5J      5L       � 5M   
   -   5          �Name$ 	TestFrame��5M      $��0��6#   $��3��60   $��1��6A   6Q   5N      5O      
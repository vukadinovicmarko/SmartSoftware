   M S B A M L   `   `   `  ���� H  DSmartSoftware, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:SmartSoftware.ViewModelSmartSoftware.ViewModel  ,    &SmartSoftware.RezervacijePregledProzor   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     i eXceed.Wpf.AvalonDock.Themes.VS2010, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4g cXceed.Wpf.AvalonDock.Themes.Aero, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4h dXceed.Wpf.AvalonDock.Themes.Metro, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4[ WXceed.Wpf.AvalonDock, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4?avalon,http://schemas.xceed.com/wpf/xaml/avalondock     X	 TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4:
wpfToolkit)http://schemas.xceed.com/wpf/xaml/toolkit 	 9smartSoftware%clr-namespace:SmartSoftware.ViewModel 	 -     � Title$  REZERVACIJE��$	��700��$
��1000�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$ SingleBorderWindow=� � 	Resources    ctpGlavniProzorTitle 
� RadiusY � Color � Offset � ColumnDefinitions ��Name	 �� Padding
 ��Name  sveSlike �� Source �� Path �Name � RowDefinitions �� RelativeSource �� AncestorType �� FallbackValue �� UpdateSourceTrigger ) %stilDugmiciIzbrisiOpremuIzRezervacije �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers P� Property P� Value 
 crveno  dugmeIzbrisi ) %dugmeIsprazniTrenutnuListuRezervacija �� Padding  dugmeIzbrisiRezervaciju ! dugmePrebaciRezervacijuUKorpu  dugmeRezervisi 	 zeleno2 
 stilDugmici  stilDugmiciKliknuto��%Z$  &	        7  7  &	 �    7  7  &	 �    &	 �	    7  7  &	 �    &	 ?    &	 >    7  7  7  &	 U    &		      &	
 !    &	 j"    �� .R��� $��Stretch=���
� $ 2Q�.��� $ 	#FF2F5778��$ 1Q�� $ 	#FF17405E��._�� $��Stretch=�$��Stretch=� �� $��* ��� $
��Auto ��� $��* �.+��� $ grdSlikaINazivAplikacije��$��0��$
��Left=�$	��Top=�$	 	15,10,0,0q�._��  �� $��15 ��� $��5 ��� $
��Auto �.+��� $��0��$��Center=�._��� $
 	slikaLogo��$��15��$��15��$��Center=�$��Center=����� 8   $  PutanjaDoSlikeLogoNormalno��� $��2��$��Center=�._��� $��tbNazivTitle��$��SmartSoftware��
�������$��11(�� $) #grdSlikaINazivTitlaIPodaciOProdavcu��$��1��$��Stretch=� �� $��40 ��� $��5 ��� $
��Auto �.+�� $��0��$��Center=�$��Bottom=� �� $��25 ��� $��10 ��� $
��Auto �.+��� $��0��$��Center=�._��� $
 slikaTitlaLogo��$��25��$��25��$��Center=�$��Center=����� 8  $ PutanjaDoSlikeRezervacije��� $��2��$��Center=�._��� $��tbNazivTitla��
�������$��14(����� *Title+ �� *FindAncestor+$ WindowL�$ Title��� $��2��$��3��.+��� $��4��$��1��$��Stretch=�._�� $��Center=� �� $
��Auto ��� $��5 ��� $
��Auto �.+��� $��0��
�������$��11(�����  �� *FindAncestor+" �$/ )DataContext.TrenutniProdavac.ImeKorisnika�$ PropertyChanged=��� $��2��
�������$��11(�����  �� *FindAncestor+" �$3 -DataContext.TrenutniProdavac.PrezimeKorisnika�$ PropertyChanged=��� " ��.���  � ��
 �������  � ��
 �  ����  � ��$ Stretch=���  � �� �� " ��.R��� #��z��$��1q�
�������._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� $��15��$��15������ 8   $ PutanjaDoSlikeDelete�$ PropertyChanged=��� $��2��$��Izbriši ovaj proizvod��$��Center=�$
��Left=� P�  �@J�IsMouseOver . . ���  � ��
 �  ����  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
���  ��._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� $��15��$��15������ 8  $$ PutanjaDoSlikeDeleteHoverovano�$ PropertyChanged=��� $��2��$��Izbriši ovaj proizvod��$��Center=�$
��Left=��� " ��.���  � ��
 �������  � ��
 �  ����  � �� �� " ��.R��� #��z��$��1q�
�������._��� $��Center=�$��Center=�$
��Auto�� P�  �@J�IsMouseOver . . ���  � ��
 �������  � ��
 �  ���� " ��.���  � ��
 ���� ��  � ��
 �  ����  � �� �� " ��.R��� #��z��$��1q�
���  ��._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� 8   $ PutanjaDoSlikeDelete�$ PropertyChanged=��� $��2��$��Izbriši��$��Center=�$
��Left=� P�  �@J�IsMouseOver . . ���  � ��
 �  ����  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� 8  $$ PutanjaDoSlikeDeleteHoverovano�$ PropertyChanged=��� $��2��$��Izbriši��$��Center=�$
��Left=��� " ��.���  � ��
 �������  � ��
 �  ����  � �� �� " ��.R��� #��z��$��1q�
���  ��._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� *)'DataContext.PutanjaDoSlikeIsprazniKorpu+$ PropertyChanged=��� $��2��$ 0,0,5,0q�$��Isprazni ovu listu��$��Center=�$
��Left=� P�  �@J�IsMouseOver . . ���  � ��
 �  ����  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� *31DataContext.PutanjaDoSlikeIsprazniKorpuHoverovano+$ PropertyChanged=��� $��2��$ 0,0,5,0q�$��Isprazni ovu listu��$��Center=�$
��Left=�P�  � {� .  . ���  � ��
 �������  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� *42DataContext.PutanjaDoSlikeIsprazniKorpuOnemoguceno+$ PropertyChanged=��� $��2��$ 0,0,5,0q�$��Isprazni ovu listu��$��Center=�$
��Left=��� " ��.���  � ��$ 30����  � ��
 ���� ��  � ��
 �  ����  � �� �� " ��.R��� #��z��$��1q�
���  ��$	 10,0,0,0q�._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� *.,DataContext.PutanjaDoSlikePonistiRezervaciju+$ PropertyChanged=��� $��2��$ 0,0,15,0q�$��Izbriši ovu rezervaciju��$��Center=�$
��Left=� P�  �@J�IsMouseOver . . ���  � ��
 �  ����  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������$	 10,0,0,0q�._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� *86DataContext.PutanjaDoSlikePonistiRezervacijuHoverovano+$ PropertyChanged=��� $��2��$ 0,0,15,0q�$��Izbriši ovu rezervaciju��$��Center=�$
��Left=�P�  � {� .  . ���  � ��
 �������  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������$	 10,0,0,0q�._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� *97DataContext.PutanjaDoSlikePonistiRezervacijuOnemoguceno+$ PropertyChanged=��� $��2��$ 0,0,15,0q�$��Izbriši ovu rezervaciju��$��Center=�$
��Left=��� " ��.���  � ��
 ���� ��  � ��
 � d ���  � ��$ 30����  � �� �� " ��.R��� #��z��$��1q�
��� d �$	 10,0,0,0q�._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� 8   $, &PutanjaDoSlikePrebaciRezervacijuUKorpu�$ PropertyChanged=��� $��2��$ 0,0,15,0q�$%��Prebaci ovu rezervaciju u korpu��$��Center=�$
��Left=� P�  �@J�IsMouseOver . . ���  � ��
 � d ���  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������$	 10,0,0,0q�._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� 8  $6 0PutanjaDoSlikePrebaciRezervacijuUKorpuHoverovano�$ PropertyChanged=��� $��2��$ 0,0,15,0q�$%��Prebaci ovu rezervaciju u korpu��$��Center=�$
��Left=�P�  � {� .  . ���  � ��
 �������  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������$	 10,0,0,0q�._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� 8  $7 1PutanjaDoSlikePrebaciRezervacijuUKorpuOnemoguceno�$ PropertyChanged=��� $��2��$ 0,0,15,0q�$%��Prebaci ovu rezervaciju u korpu��$��Center=�$
��Left=��� " ��.���  � ��
 ���� ��  � ��
 � d ���  � ��$ 30����  � �� �� " ��.R��� #��z��$��1q�
��� d �$	 10,0,0,0q�._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� *%#DataContext.PutanjaDoSlikeRezervisi+$ PropertyChanged=��� $��2��$ 0,0,15,0q�$��
Rezerviši��$��Center=�$
��Left=� P�  �@J�IsMouseOver . . ���  � ��
 � d ���  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������$	 10,0,0,0q�._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� */-DataContext.PutanjaDoSlikeRezervisiHoverovano+$ PropertyChanged=��� $��2��$ 0,0,15,0q�$��
Rezerviši��$��Center=�$
��Left=�P�  � {� .  . ���  � ��
 �������  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������$	 10,0,0,0q�._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� *0.DataContext.PutanjaDoSlikeRezervisiOnemoguceno+$ PropertyChanged=��� $��2��$ 0,0,15,0q�$��
Rezerviši��$��Center=�$
��Left=��� " ��.���  � ��
 � d ���  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������._��� $��Center=�$��Center=�$
��Auto�� P�  �@J�IsMouseOver . . ���  � ��
 �������  � ��
 � d ��� " ��.���  � ��
 ���� ��  � ��$ 30����  � ��
 �������  � ��$
 Auto����  � ��$ 12(���  � ��$ 0q���  � �� �� " ��.R��� #��z��$��0q�$
	 10,0q�._��� $��Center=�$��Center=�$
��Auto�� P�  �@J�IsMouseOver . . ���  � ��
 �������  � �� �	#FF18486A�� " ��.���  � �� �#128689��  � ��$ 30����  � ��
 �������  � ��$	 135����  � ��$ 12(���  � ��$ 0q���  � �� �� " ��.R��� #��z��$��0q�$
	 10,0q�._��� $��Center=�$��Center=�$
��Auto�� P�  �@J�IsMouseOver . . ���  � �� �#128689��  � ��
 �����'  !System.Windows.Shell.WindowChrome   WindowChrome      CaptionHeight$ 0Q�   GlassFrameThickness$ 0,0,0,1q�   ResizeBorderThickness$ 1q�.��]� .+�� -   $��grdCeoSadrzaj�� �� $
��Auto ��� $��* �.+��� -   $��titleBar��$��0��$	��Top=�$��75��
���   ���
� $ 2Q�.��� $ 	#FF2F5778��$ 1Q�� $ 	#FF17405E���  �� $
��Auto ��� $
��Auto �.+��� -    ��Name$ ctlTitleBar��$��0��$	��Top=�#���   $��Stretch=�$��Center=�$��Stretch=�$��Center=�$��75����.  � -   $��grdDugmiciZaProzor��$��3��$��Right=�$	��Top=� �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -     ��Name$  dugmeMinimize��$��0��  dugmeMinimize#��[ ��.  �� -   $  dugmeMaximize��$��1��  dugmeMaximize#��[ ��.  �� -   $  dugmeZatvori��$��2��  
dugmeClose#��[ ��.  �� $��1��
�������$��0,5,0,0q���
� ! 
� GradientOrigin$! 0.5,0.5&�" 
� Center$" 0.5,0.5&�$ 2Q�# 
� RadiusX$	# 0.7Q�.��� $ 	#FF270000��$ 0.945Q�� $ White��$ 0.207Q�� $ 	#FFEBF0F4��$ 0.425Q�� $ 	#FFE9EBEE��$ 0.702Q�$ J� Effect$ 3  -System.Windows.Media.Effects.DropShadowEffect  %   Color$% Black��&   	Direction$& 90Q�'   
BlurRadius$' 10Q�(   ShadowDepth$( 2Q�)   RenderingBias$) Quality=�._�)  #Xceed.Wpf.AvalonDock.DockingManager  
���   �*   Theme* ,  &Xceed.Wpf.AvalonDock.Themes.MetroTheme  ���� + �� Color$+ Transparent��,   Layout., ,  &Xceed.Wpf.AvalonDock.Layout.LayoutRoot  -   	RootPanel.- -  'Xceed.Wpf.AvalonDock.Layout.LayoutPanel  .   Orientation$. 
Horizontal=��  �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutPanelElement, Xceed.Wpf.AvalonDock, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]/   Children./ 9  3Xceed.Wpf.AvalonDock.Layout.LayoutDocumentPaneGroup  �	  �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutDocumentPane, Xceed.Wpf.AvalonDock, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]0 	  Children.0 4
  .Xceed.Wpf.AvalonDock.Layout.LayoutDocumentPane
  �  �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.LayoutContent, Xceed.Wpf.AvalonDock, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]1   Children.1 0  *Xceed.Wpf.AvalonDock.Layout.LayoutDocument  -	   listaOpremeZaRezervisanje��/  )Xceed.Wpf.AvalonDock.Layout.LayoutContent2   Title$"2 LISTA OPREME ZA REZERVISANJE��3   Content.3 �� $��0q�$��	0,-5,0,-5q���
� $! 0.5,0.5&�$" 0.5,0.5&�$ 2Q�$	# 0.7Q�.��� $ 	#FF270000��$ 0.945Q�� $ White��$ 0.207Q�� $ 	#FFEBF0F4��$ 0.425Q�� $ 	#FFE9EBEE��$ 0.702Q�._��  �� $��5* ��� $��10 ��� $	��100 � �� $��* �.+�� -
   $%��grdListaSaRezervacijamaJePrazna��$��0��$y�Hidden=�.+��� $��Center=�$��Center=�._��� $%��Lista za rezervisanje je prazna��$��Italic$����	#FF2F5778$��12(��� -   $��scrolVjuverRezervacije��$��0��$��Stretch=�$y�Visible=�$
��Auto=�|�.  .���� $��Stretch=�$		 0,5q�
�������$	��0,5q�._�� $��Center=�$	��950�� �� $��30 ��� $
��Auto ��� $��1 � �� $	��410 ��� $��10 ��� $	��100 ��� $��10 ��� $	��150 ��� $��10 ��� $��95 ��� $��10 ��� $	��125 � ��%   '��       �� " ��.���  � ��$ 12(���  � �� �	#FF2F5778��  � ��$ Center=���  � ��$ Center=�.+��� $��0��$��Stretch=�
�������$��0,0,0,5q����	#06000000._�� $��Center=�.+��� $��Proizvod���� $��2��$��Stretch=�
�������$��0,0,0,5q����	#06000000._�� $��Center=�.+��� $
��Cena���� $��4��$��Stretch=�
�������$��0,0,0,5q����	#06000000._�� $��Center=�.+��� $��Ukupna cena���� $��6��$��Stretch=�
�������$��0,0,0,5q����	#06000000._�� $��Center=�.+��� $��	Količina���� $��8��$��Stretch=�
�������$��0,0,0,5q����	#06000000._�� $��Center=�.+��� $��Izbriši���� -   $��itcProizvodi��$��1��$��0��$��Stretch=�$��Center=�$	��Top=�$��Stretch=�$��0,10,0,0q����� *ListaTrenutnihRezervacija+$ PropertyChanged=����� .R�� $��60�� �� $��60 ��� $��10 ��� $	��340 �.+��� $��0��
�������$��0,1,0,0q�
�������._��� $��50��$��50������ *Slika+�� $��2��
�������$��0,1,0,0q�
������ ._�� .+�� $��Center=�.+��� $
��Wrap=�
���   �$
 10,0q�$��11(�$��Center=�$��Center=����� *Name+�� -   $��itcCenaProizvoda��$��1��$��2��$��Stretch=�$��Center=�$	��Top=�$��Stretch=�$��0,10,0,0q����� *ListaTrenutnihRezervacija+$ PropertyChanged=����� .R�� $��60��.+��� 
�������$��0,1,0,0q�
������ ._�� .+�� $��Center=�.+��� $
��Wrap=�
���   �$
 10,0q�$��11(�$��Center=�$��Center=����� *Cena+4 �� StringFormat$4  {0} RSD��$ PropertyChanged=��� -   $��itcUkupnaCenaProizvoda��$��1��$��4��$��Stretch=�$��Center=�$	��Top=�$��Stretch=�$��0,10,0,0q����� *ListaTrenutnihRezervacija+$ PropertyChanged=����� .R�� $��60��.+��� $��0,1,0,0q�
�������
������ ._�� .+�� $��Center=�.+��� $	 5,0q�
���   �$��Center=�$��11(�$��Center=����� $ SumCenaZaRezervisanje�$4  {0} RSD��$ PropertyChanged=��� -   $��itcKolicina��$��1��$��6��$��Stretch=�$��Center=�$	��Top=�$��Stretch=�$��0,10,0,0q����� *ListaTrenutnihRezervacija+$ PropertyChanged=����� .R�� $��60��.+��� $��0,1,0,0q�
�������
������ ._�� $��Center=�$��Center=�.+��� $��	skrivenId��$y�Hidden=����� *
IdOprema+�� $��btnObrisiIzKorpe��$��30��$��Obriši iz korpe��$��0��$��0��$��2��
���  ��
��������� $
��Wrap=�$��11(�$��2��$��0��$��Količina :��$��Center=�$��Center=�% 	 Xceed.Wpf.Toolkit.IntegerUpDown  $��kolicina��-   $��2��$��1��$	��150��� 	 �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]5   Minimum$5 1X�6   Value6 �� * IzabranaKolicinaZaRezervisanje+$ PropertyChanged=�7   Maximum7 �� *KolicinaNaLageru+$ PropertyChanged=��� -   $��itcDugmeIzbrisi��$��1��$��8��$��Stretch=�$��Center=�$	��Top=�$��Stretch=�$��0,10,0,0q����� *ListaTrenutnihRezervacija+$ PropertyChanged=����� .R�� $��60��$��Center=�.+��� $��0,1,0,0q�
�������
������ ._�� $��Stretch=�$��Center=�.+��� $��	skrivenId��$y�Hidden=����� *
IdOprema+�� $��btnObrisiIzRezervacije��-   $��Center=�$��30��$��0��$��0��$��2��#��[ $��11(�8 � Cursor$
8 Hand���� $��2��$��0��$��Stretch=�._��  �� $��60 ��� $��10 ��� $	��340 �.+��� -   $��borderSlika��$��0��
�������$��0,1,0,0q�
��������� -   $��borderNaslov��$��2��
�������$��0,1,0,0q�
������ �� -   $��	bordeCena��$��2��$��2��$��Stretch=�
�������$��0,0,0,1q��� -   $��borderUkupnaCena��$��2��$��4��$��Stretch=�
�������$��0,0,0,1q��� -   $��borderKolicina��$��2��$��6��$��Stretch=�
�������$��0,0,0,1q��� -   $��borderDugme��$��2��$��8��$��Stretch=�
�������$��0,0,0,1q��� -   $��gridDugmici��$��3��$	��100��$��Center=����	#06000000
�������$��0,5,0,0q�._�� $	��850�� �� $��* ��� $��* ��� $��* � �� $��* �.+�� $��0��$��Center=�.+��� -   $��btnDodajURezervacije��#��[ $
8 Hand��{��� *86DataContext.DaLiImaNestoUTrenutnojListiSaRezervacijama+$ PropertyChanged=�� $��1��$��Center=�$��Center=�.+�� $��0��.+��� $��0��
�������$��1q�._��  �� $��35 ��� $��* �.+�� $��0�����	#06000000.+��� $
��Wrap=�$��12(����	#FF2F5778$
 30,0q�$!��Ukupna cena za rezervisanje��$��Center=�$��Center=�$��Italic$�� $��1��$��Stretch=�
�������.+��� $
��Wrap=�
���   �$ 10q�$��13(�$��Center=�$��Center=����� $* $DataContext.UkupnaCenaURezervacijama�$4  {0} RSD��$ PropertyChanged=�� $��2��$��Center=�$��Center=�.+��� -   $)��#btnIsprazniTrenutnuListuRezervacija��#��[ $
8 Hand��{��� *86DataContext.DaLiImaNestoUTrenutnojListiSaRezervacijama+$ PropertyChanged=�  -   sacuvaneRezervacije��$"2 LISTA SAČUVANIH REZERVACIJA��.3 �� $��1��$��	0,-5,0,-5q�$��0,5,0,0q�
�������$��Stretch=���
� $! 0.5,0.5&�$" 0.5,0.5&�$ 2Q�$	# 0.7Q�.��� $ 	#FF270000��$ 0.945Q�� $ White��$ 0.207Q�� $ 	#FFEBF0F4��$ 0.425Q�� $ 	#FFE9EBEE��$ 0.702Q�._�� $��Stretch=� �� $
��Auto ��� $��0 ��� $��40 ��� $��20 ��� $��* �.+�� $��2�����	#FF2F5778$��Stretch=� �� $��5 ��� $
��Auto ��� $��5 �.+�� $��0��$��5�����	#FF2F5778�� $��1��$
	 20,0q�$��Stretch=�._�� -   $��
grdDugmici��$��1�� �� $	��550 ��� $��* �.+�� $��0��$��Center=�$
��Left=� �� $��30 � �� $	��550 �.+�� $��0��$��1��$	��480��$
��Left=�.+��� -   9 ��Name$9 
tbPretraga��$��30��$��Center=�$��Stretch=�$��Center=�$��8,0,0,0q��� $��0��$��1��: J� IsHitTestVisible: .  $ ��Pretražite rezervacije...��$��Center=�$��Stretch=�$��10,0,0,0q����	#FF7E7E7E$��10(����� " ��.���  � y�$ 	Collapsed=� �� ; �� Binding; �� *Text+< �� ElementName$< 
tbPretraga��= �� Value=  .a���  � y�$ Visible=�� $��1��$��Right=� �� $
��Auto ��� $��10 �� $��2��$��5�����	#FF2F5778�� $��4��$	��700��._��  �� $��5* ��� $��5 ��� $	��100 � �� $	��520 ��� $��15 ��� $��* �.+��� $	��Top=�
����  �$��0q�._��  �� $��* ��� $
��Auto ��� $��* �.+�� -   $��grdKupci��$��0��$��1��$
��Auto��$��Center=�$��Center=� �� $
��Auto ��� $��15 ��� $��* ��� $��1 ��� $
��Auto � �� $	��140 ��� $��5 ��� $	��115 ��� $��5 ��� $	��115 ��� $��5 ��� $	��115 ��� $��20 � ��%{  '��       '�� �     �� " ��.���  � ��$ Center=���  � ��$ Center=���  � ��$ Normal$���  � ��$
 Auto����  � ��$ 10(���  � ��
 �������  � �� �	#FF2F5778�� " ��.���  � ��$ Center=���  � ��$ Center=���  � ��$ Normal$���  � ��$
 Auto����  � ��$ 10(���  �@��Padding$	 5,0q�.+��� -    $��	skrivenId��$��0��$��1��$��Marko��$y�Hidden=��� $��Ime i br. telefona��$��0��$��0���� $��Datum ažuriranja��$��0��$��2���� $��Datum isteka��$��0��$��4���� $��Datum rezervisanja��$��0��$��6���� $��2��$��0��$��8��$
��Auto=�$��Stretch=�.��� -!   $��	gridKupci�� �� $��* ��� $
��Auto � �� $	��140 ��� $��5 ��� $	��115 ��� $��5 ��� $	��115 ��� $��5 ��� $	��115 ��� $��20 �.+��� -"   $��lsbKupci��$��0��$��8��$��Center=�$	��Top=�$��Stretch=����� *Rezervacije+���� .R�� $��grdPrikazRezervacija��-#   $��50��
������ $
8 Hand�� ��%{  '��       '�� �     �� " ��.���  � ��$ Center=���  � ��$ Center=���  � ��$ Italic$���  � ��$
 Auto����  � ��$ 10(���  � �� �	#FF2F5778��  � ��
 ������� " ��.���  � ��$ Center=���  � ��$ Center=���  � ��$ Italic$���  � ��$
 Auto����  � ��$ 10(���  �@��Padding$	 5,0q� �� $	��140 ��� $��5 ��� $	��115 ��� $��5 ��� $	��115 ��� $��5 ��� $	��115 ��� $��20 �.+��� $��rezervacijeSkrivenId��$y�Hidden=����� *IdRerezervacije+�� $ probaBorder��$��2��$��0��
�������$��0,1,0,0q�$��Stretch=�
������ $��Stretch=�._��  �� $��* ��� $��* �.+��� $��0��._��� $��tblIme��
���   �$
��Wrap=�$��Normal$����� *Ime+�� $��1��._��� $��tblBrojTelefona��
���   �$��10(�$��Italic$�$
��Wrap=�$��064 / 000 - 555���� $ probaBorder1��$��2��$��6��
�������$��0,1,0,0q�$��Stretch=�
������ $��Stretch=�._��� $��tblDatumRezervacije��
���   ����� *DatumRezervacije+�� $ probaBorder2��$��2��$��4��
�������$��0,1,0,0q�$��Stretch=�
������ $��Stretch=�._��� $��tblDatumIstekaRezervacije��
���   �$��1��$��2������ *DatumIstekaRezervacije+�� $ probaBorder3��$��2��$��2��
�������$��0,1,0,0q�$��Stretch=�
������ $��Stretch=�._��� $#��tblDatumAzuriranjaRezervacije��
���   �$��1��$��3������ *DatumAzuriranjaRezervacije+�� $��1��$��0��
�������$��0,0,0,1q��� $��1��$��2��
�������$��0,0,0,1q��� $��1��$��4��
�������$��0,0,0,1q��� $��1��$��6��
�������$��0,0,0,1q��� > �� Orientation$> 
Horizontal=�$��0��$��1��$��Stretch=�$��Center=�.+��� #��["��� -$   $��skrolVjuverSveRezervacije��$��2��$
��Auto=�$y�Hidden=�.���� -%   $��itcSveRezervacije��$��2��$	��Top=�$��0��$��Stretch=�|�.  ���� *CurrentOprema+$ PropertyChanged=����� .R��� $ 	brdOprema��$��Stretch=�$��Center=����	#FFFAFAFA$��0,0,0,20q�
�������$��1q�._�� $��Stretch=� �� $��30 ��� $	��180 ��� $��30 � �� $��65 ��� $	��205 � ? � DataContext@ �� SelfA �� 
LineHeight��%r  '��       '�� �     �� " ��.���  � ��$ 10(���  � ��$ Stretch=���  � ��$ Right=���  � ��$ Center=���  � ��$ Center=���  � ��
 ������� " ��.���  �@��
LineHeight$ 30����  �@^�	Placement$ 
MousePoint=���  �@^�ShowDuration$ 12000����  �@��Padding$	 0,6q���  � ��$	 Top=���  � ��$ 10(���  � ��$ Italic$���  � ��
 �   ���  � ��$ Stretch=���  �@�ToolTip _� ? �� $ PlacementTarget�# Z@ .���� $	��150��._��� $��Center=�$A 5��$
��Wrap=�$��Normal$�$��10(����� *Text+.+��� $ 
brdBorderi��$��0��$��2��$��3��$��Stretch=�$��Stretch=��� $��	skrivenId��$��0��$y�Hidden=����� *
IdOprema+�� $ brdOpremaNaziv��$��0��$��0��$��2��
�������$��Center=�
�������$��0,0,0,1q�$��30��$
	 10,0q�$��Stretch=�$	��268��._�� $��Center=� �� $��* ��� $
��Auto �.+��� $��tblckOpremaNaziv��$��0��$��12(�$
��Left=�$	 0,7q�$��Normal$����� *Name+B ^� 	IsEnabledB �� *#!DaLiTekstNaslovaZauzimaViseRedova+$ PropertyChanged=�C � ToolTipC �� $	��270��._��� $��Center=�$A 5��$
��Wrap=�$��Normal$�$��10(����� *Name+�� $ ��tblckOpremaNazivDetaljnije��$��1��$	��...��$��12(�$��Right=�$	 0,7q�$��Normal$�$y�Hidden=�$��0��B �� *#!DaLiTekstNaslovaZauzimaViseRedova+$ PropertyChanged=�C �� $	��270��._��� $��Center=�$A 5��$
��Wrap=�$��Normal$�$��10(����� *Name+�� $ brdOpremaSlika��$��1��$��0��$��75��$		 0,5q�._��� $��55��$��55��$��Center=�$��Center=����� *Slika+�� $ brdOpremaSredina��$��1��$��1��$	��150��$	 0,0,10,0q�._��  �� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 �.+��� $��0��$��Stretch=�$��Stretch=�._��  �� $	��100 ��� $��* ��� $
��Auto �.+��� D ��Name$D lblProizvodjac��$��0��$��Proizvodjač : ���� E ��Name$E tblckOpremaProizvodjac��$��1������ *Proizvodjac+B �� *(&DaLiTekstProizvodjacaZauzimaViseRedova+$ PropertyChanged=��� $&�� tblckOpremaProizvodjacDetaljnije��$��2��$��. . .��$��Normal$�$y�Hidden=�$��0��$��Right=�B �� *!DaLiTekstOpisaZauzimaViseRedova+$ PropertyChanged=�C �� $	��150��._��� $��Center=�$A 5��$
��Wrap=�$��Normal$�$��10(����� *Proizvodjac+�� $��1��$��Stretch=�$��Stretch=�._��  �� $	��100 ��� $��* ��� $
��Auto �.+��� $D lblModel��$��0��$��Model : ���� $E tblckOpremaModel��$��1������ *Model+B �� *" DaLiTekstModelaZauzimaViseRedova+$ PropertyChanged=��� $ ��tblckOpremaModelDetaljnije��$��2��$��. . .��$��Normal$�$y�Hidden=�$��0��$��Right=�B �� *!DaLiTekstOpisaZauzimaViseRedova+$ PropertyChanged=�C �� $	��150��._��� $��Center=�$A 5��$
��Wrap=�$��Normal$�$��10(����� *Model+�� $��2��$��Stretch=�$��Stretch=�._�� $��Center=� �� $	��100 ��� $��* ��� $
��Auto �.+��� $D lblOpis��$��0��$��Opis : ���� $E tblckOpremaOpis��$��1������ *Opis+B �� *!DaLiTekstOpisaZauzimaViseRedova+$ PropertyChanged=��� $��tblckOpremaOpisDetaljnije��$��2��$	��...��$��Normal$�$y�Hidden=�$��0��$��Right=�B �� *!DaLiTekstOpisaZauzimaViseRedova+$ PropertyChanged=�C �� $	��150��._��� $��Center=�$A 5��$
��Wrap=�$��Normal$�$��10(����� *Opis+�� $��3��$��Stretch=�$��Stretch=�._�� $��Center=� �� $	��100 ��� $��* ��� $
��Auto �.+��� $D lblCena��$��0��$��Cena po proizvodu : ���� $E tblckOpremaCena��$��1������ *Cena+B �� * DaLiTekstCeneZauzimaViseRedova+$ PropertyChanged=��� $��tblckOpremaCenaDetaljnije��$��2��$	��...��$��Normal$�$y�Hidden=�$��0��$��Right=�B �� * DaLiTekstCeneZauzimaViseRedova+$ PropertyChanged=�C �� $	��150��._��� $��Center=�$A 5��$
��Wrap=�$��Normal$�$��10(����� *Cena+�� $��4��$��Stretch=�$��Stretch=�._�� $��Center=� �� $	��100 ��� $��* ��� $
��Auto �.+��� $D lblKolicina��$��0��$��Količina : ���� $E tblckOpremaKolicina��$��1������ *TmpIzabranaKolicina+B �� *$"DaLiTekstKolicineZauzimaViseRedova+$ PropertyChanged=��� $#��tblckOpremaKolicinaDetaljnije��$��2��$	��...��$��Normal$�$y�Hidden=�$��0��$��Right=�B �� *$"DaLiTekstKolicineZauzimaViseRedova+$ PropertyChanged=�C �� $	��150��._��� $��Center=�$A 5��$
��Wrap=�$��Normal$�$��10(����� *TmpIzabranaKolicina+�� $��5��$��Stretch=�$��Stretch=�._�� $��Center=� �� $	��100 ��� $��* ��� $
��Auto �.+��� $D lblUkupnaCena��$��0��$��Ukupna cena : ���� $E tblckOpremaUkupnaCena��$��1������ *	SumCena+B �� *&$DaLiTekstUkupneCeneZauzimaViseRedova+$ PropertyChanged=��� $%��tblckOpremaUkupnaCenaDetaljnije��$��2��$	��...��$��Normal$�$y�Hidden=�$��0��$��Right=�B �� *&$DaLiTekstUkupneCeneZauzimaViseRedova+$ PropertyChanged=�C �� $	��150��._��� $��Center=�$A 5��$
��Wrap=�$��Normal$�$��10(����� *	SumCena+�� $ brdOpremaCena��$��2��$��0��$��2��
�������
�������$��0,1,0,0q�$��Stretch=�._��� $��btnObrisiIzRezervacija��$��0��$��30��#���  $
8 Hand���� $��2��$��0��$��3�����	#06000000
�������$��0,5,0,0q�._�� -&   $��gridDugmiciSveRezervacije�� �� $��* ��� $
��Auto ��� $��* � �� $��* ��� $��* �.+�� $��1��$��0��$��Center=�$��Center=�.+��� -'   $!��btnPrebaciRezervacijuUKorpu��#��[ $
8 Hand��{��� * TempRezervacija.KliknutoNaGrid+$ PropertyChanged=�� $��1��$��1��$��Center=�$��Center=�.+��� -(   $#��btnIzbrisiIzabranuRezervaciju��#��[ $
8 Hand��{��� * TempRezervacija.KliknutoNaGrid+$ PropertyChanged=�
   M S B A M L   `   `   `  ����   SmartSoftware    SmartSoftware.KorpaProzor   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     i eXceed.Wpf.AvalonDock.Themes.VS2010, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4g cXceed.Wpf.AvalonDock.Themes.Aero, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4h dXceed.Wpf.AvalonDock.Themes.Metro, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4[ WXceed.Wpf.AvalonDock, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4?avalon,http://schemas.xceed.com/wpf/xaml/avalondock     X	 TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4:
wpfToolkit)http://schemas.xceed.com/wpf/xaml/toolkit 	 $asdMicrosoft.Windows.Controls 	 -     � Title$  KORPA��$	��700��$
��1000�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$ SingleBorderWindow=� � 	Resources    ctpGlavniProzorTitle 
� RadiusY � Color � Offset � ColumnDefinitions ��Name	 �� Padding
 ��Name  sveSlike �� Source �� Path �Name � RowDefinitions �� RelativeSource �� AncestorType �� FallbackValue �� UpdateSourceTrigger 
 crveno �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers P� Property P� Value  dugmeIzbrisi  dugmeIsprazniKorpu �� Padding  	dugmeKupi  zeleno2  stilDugmici  stilDugmiciKliknuto��%r  &	        7  7  &	 �    &	 �    7  7  &	 �	    7  7  7  &	 �    7  7  7  &	 A    &	 ?    &	 �    �� .R��� $��Stretch=���
� $ 2Q�.��� $ 	#FF2F5778��$ 1Q�� $ 	#FF17405E��._�� $��Stretch=�$��Stretch=� �� $��* ��� $
��Auto ��� $��* �.+��� $ grdSlikaINazivAplikacije��$��0��$
��Left=�$	��Top=�$	 	15,10,0,0q�._��  �� $��15 ��� $��5 ��� $
��Auto �.+��� $��0��$��Center=�._��� $
 	slikaLogo��$��15��$��15��$��Center=�$��Center=����� 8   $  PutanjaDoSlikeLogoNormalno��� $��2��$��Center=�._��� $��tbNazivTitle��$��SmartSoftware��
�������$��11(�� $) #grdSlikaINazivTitlaIPodaciOProdavcu��$��1��$��Stretch=� �� $��40 ��� $��5 ��� $
��Auto �.+�� $��0��$��Center=�$��Bottom=� �� $��25 ��� $��10 ��� $
��Auto �.+��� $��0��$��Center=�._��� $
 slikaTitlaLogo��$��25��$��25��$��Center=�$��Center=����� 8  $ PutanjaDoSlikeKorpa��� $��2��$��Center=�._��� $��tbNazivTitla��
�������$��14(����� *Title+ �� *FindAncestor+$ WindowL�$ Title��� $��2��$��3��.+��� $��4��$��1��$��Stretch=�._�� $��Center=� �� $
��Auto ��� $��5 ��� $
��Auto �.+��� $��0��
�������$��11(�����  �� *FindAncestor+" �$/ )DataContext.TrenutniProdavac.ImeKorisnika�$ PropertyChanged=��� $��2��
�������$��11(�����  �� *FindAncestor+" �$3 -DataContext.TrenutniProdavac.PrezimeKorisnika�$ PropertyChanged=��� " ��.���  � ��
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
 ���� ��  � ��
 �  ����  � �� �� " ��.R��� #��z��$��1q�
���  ��._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� 8   $! PutanjaDoSlikeIsprazniKorpu�$ PropertyChanged=��� $��2��$ 0,0,5,0q�$��Isprazni korpu��$��Center=�$
��Left=� P�  �@J�IsMouseOver . . ���  � ��
 �  ����  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� 8  $+ %PutanjaDoSlikeIsprazniKorpuHoverovano�$ PropertyChanged=��� $��2��$ 0,0,5,0q�$��Isprazni korpu��$��Center=�$
��Left=�P�  � {� .  . ���  � ��
 �������  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� 8  $, &PutanjaDoSlikeIsprazniKorpuOnemoguceno�$ PropertyChanged=��� $��2��$ 0,0,5,0q�$��Isprazni korpu��$��Center=�$
��Left=��� " ��.���  � ��
 ���� ��  � ��
 � d ���  � �� �� " ��.R��� #��z��$��1q�
��� d �._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� 8   $ PutanjaDoSlikeKupi�$ PropertyChanged=��� $��2��$ 0,0,5,0q�$
��Kasa��$��Center=�$
��Left=� P�  �@J�IsMouseOver . . ���  � ��
 � d ���  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� 8  $" PutanjaDoSlikeKupiHoverovano�$ PropertyChanged=��� $��2��$ 0,0,5,0q�$
��Kasa��$��Center=�$
��Left=�P�  � {� .  . ���  � ��
 �������  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� 8  $# PutanjaDoSlikeKupiOnemoguceno�$ PropertyChanged=��� $��2��$ 0,0,5,0q�$
��Kupi��$��Center=�$
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
��Auto ��� $��* �.+��� -    ��Name$ ctlTitleBar��$��0��$	��Top=�#���   $��Stretch=�$��Center=�$��Stretch=�$��Center=�$��75����.  � -   $��grdDugmiciZaProzor��$��3��$��Right=�$	��Top=� �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -     ��Name$  dugmeMinimize��$��0�� 	 dugmeMinimize#��[	 ��.  �� -   $  dugmeMaximize��$��1�� 
 dugmeMaximize#��[
 ��.  �� -   $  dugmeZatvori��$��2��  
dugmeClose#��[ ��.  �� $��1����
� ! 
� GradientOrigin$! 0.5,0.5&�" 
� Center$" 0.5,0.5&�$ 2Q�# 
� RadiusX$	# 0.7Q�.��� $ 	#FF270000��$ 0.945Q�� $ White��$ 0.207Q�� $ 	#FFEBF0F4��$ 0.425Q�� $ 	#FFE9EBEE��$ 0.702Q�$ J� Effect$ 3  -System.Windows.Media.Effects.DropShadowEffect  %   Color$% Black��&   	Direction$& 90Q�'   
BlurRadius$' 10Q�(   ShadowDepth$( 2Q�)   RenderingBias$) Quality=�._��� 
�������$��0,5,0,0q�$	 0,5,0,0q�._��  �� $��5* ��� $��10 ��� $	��100 � �� $��* �.+�� -	   $��grdKorpaJePrazna��$��0��$y�Hidden=�.+��� $��Center=�$��Center=�._��� $��Korpa je prazna��$��Italic$����	#FF2F5778$��14(��� -
   $ scrolVjuverKorpa��$��0��$��Stretch=�$y�Visible=�$��0,0,0,5q�
�������._��  �� $
��Auto ��� $��* �.+�� -   $ grdHederKorpa��$��0��$��Center=� �� $��30 � �� $	��370 ��� $��10 ��� $	��100 ��� $��10 ��� $	��150 ��� $��10 ��� $��95 ��� $��10 ��� $	��125 � ��%   '��       �� " ��.���  � ��$ 12(���  � �� �	#FF2F5778��  � ��$ Center=���  � ��$ Center=�.+��� $��0��$��Stretch=�
�������$��0,0,0,5q����	#06000000._�� $��Center=�.+��� $��Proizvod���� $��2��$��Stretch=�
�������$��0,0,0,5q����	#06000000._�� $��Center=�.+��� $
��Cena���� $��4��$��Stretch=�
�������$��0,0,0,5q����	#06000000._�� $��Center=�.+��� $��Ukupna cena���� $��6��$��Stretch=�
�������$��0,0,0,5q����	#06000000._�� $��Center=�.+��� $��	Količina���� $��8��$��Stretch=�
�������$��0,0,0,5q����	#06000000._�� $��Center=�.+��� $��Izbriši���� -   $��scrolVjuverKorpaSadrzaj��$��1��$��Stretch=�$y�Visible=�$
��Auto=�.���� $��Center=�$		 0,5q�._�� $��Center=� �� $��0 ��� $
��Auto ��� $��1 � �� $	��370 ��� $��10 ��� $	��100 ��� $��10 ��� $	��150 ��� $��10 ��� $��95 ��� $��10 ��� $	��125 �.+��� -   $��itcProizvodi��$��1��$��0��$��Stretch=�$��Center=�$	��Top=�$��Stretch=�$��0,10,0,0q����� *Korpa+���� .R�� $��60�� �� $��60 ��� $��10 ��� $	��300 �.+��� $��0��
�������$��0,1,0,0q�
�������._��� $��50��$��50������ *Slika+�� $��2��
�������$��0,1,0,0q�
������ ._�� .+�� $��Center=�$��50��.+�� $��Center=�$��Center=� �� $
��Auto ��� $
��Auto �.+��� * ��Name$$* tblckNaslovProizvodaNarudzbine��$��0��$
��Wrap=�$	 5,0q�$��11(�$��Center=�$	��Top=����	#FF2F5778&   System.Windows.Media.TextOptions+   TextFormattingMode$+ Ideal=�$��33������ *Name+, ^� 	IsEnabled, �� *64DaLiTekstNaslovaOpremeUNarudzbinamaZauzimaViseRedova+$ PropertyChanged=�- � ToolTip- �� $	��200��._��� $
��Left=�. �� 
LineHeight$. 5��$
��Wrap=�$��Normal$�$��10(����� *Name+$ PropertyChanged=��� $.��(tblckNaslovProizvodaNarudzbineDetaljnije��$��1��$	��...�����	#FF2F5778$
��Left=�$��Center=�$y�Hidden=�$��0��$��20��$	 5,0q�, �� *64DaLiTekstNaslovaOpremeUNarudzbinamaZauzimaViseRedova+$ PropertyChanged=�- �� $	��200��._��� $��Center=�$. 5��$
��Wrap=�$��Normal$�$��10(����� *Name+$ PropertyChanged=��� -   $��itcCenaProizvoda��$��1��$��2��$��Stretch=�$��Center=�$	��Top=�$��Stretch=�$��0,10,0,0q����� *Korpa+���� .R�� $��60��.+��� 
�������$��0,1,0,0q�
������ ._�� .+�� $��Center=�.+��� $
��Wrap=����	#FF2F5778$
 10,0q�$��11(�$��Center=�$��Center=����� *Cena+/ �� StringFormat$/  {0} RSD���� -   $��itcUkupnaCenaProizvoda��$��1��$��4��$��Stretch=�$��Center=�$	��Top=�$��Stretch=�$��0,10,0,0q����� *Korpa+���� .R�� $��60��.+��� $��0,1,0,0q�
�������
������ ._�� .+�� $��Center=�.+��� $	 5,0q����	#FF2F5778$��Center=�$��11(�$��Center=����� $ SumCena�$/  {0} RSD���� -   $��itcKolicina��$��1��$��6��$��Stretch=�$��Center=�$	��Top=�$��Stretch=�$��0,10,0,0q����� *Korpa+���� .R�� $��60��.+��� $��0,1,0,0q�
�������
������ ._�� $��Center=�$��Center=�.+��� $��	skrivenId��$y�Hidden=����� *
IdOprema+�� $
��Wrap=�$��11(�$��2��$��0��$��Količina :��$��Center=�$��Center=�% 	 Xceed.Wpf.Toolkit.IntegerUpDown  $��kolicina��-   $��11(�$��30��$��2��$��1��$��80��, 	 &Xceed.Wpf.Toolkit.Primitives.InputBase0   AllowTextInput0 .  � 	 �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]1   Minimum$1 1X�2   Value2 �� *IzabranaKolicina+$ PropertyChanged=�3   Maximum3 �� *KolicinaNaLageru+$ PropertyChanged=��� -   $��itcDugmeIzbrisi��$��1��$��8��$��Stretch=�$��Center=�$	��Top=�$��Stretch=�$��0,10,0,0q����� *Korpa+���� .R�� $��60��$��Center=�.+��� $��0,1,0,0q�
�������
������ ._�� $��Stretch=�$��Center=�.+��� $��	skrivenId��$y�Hidden=����� *
IdOprema+�� $��btnObrisiIzKorpe��-   $��Center=�$��30��$��0��$��0��$��2��#��[ $��11(�4 � Cursor$
4 Hand���� $��2��$��0��$��Stretch=�._��  �� $��60 ��� $��10 ��� $	��300 �.+��� -   $��borderSlika��$��0��
�������$��0,1,0,0q�
��������� -   $��borderNaslov��$��2��
�������$��0,1,0,0q�
������ �� -   $��	bordeCena��$��2��$��2��$��Stretch=�
�������$��0,0,0,1q��� -   $��borderUkupnaCena��$��2��$��4��$��Stretch=�
�������$��0,0,0,1q��� -   $��borderKolicina��$��2��$��6��$��Stretch=�
�������$��0,0,0,1q��� -   $��borderDugme��$��2��$��8��$��Stretch=�
�������$��0,0,0,1q��� -   $��gridDugmici��$��2��$	��100��$��Center=����	#06000000
�������$��0,5,0,0q�._�� $	��850�� �� $��* ��� $��* ��� $��* � �� $��* �.+�� $��0��.+��� -   $��btnKupi��#��[ $	��100��$��30��$��0��$
��Kupi��$��12(�$��Center=�$��Center=�$
4 Hand��{��� * DataContext.DaLiImaNestoUKorpi+$ PropertyChanged=�� $��1��$��Center=�$��Center=�.+�� $��0��.+��� $��0��
�������$��1q�._��  �� $��35 ��� $��* �.+�� $��0�����	#06000000.+��� $
��Wrap=�$��12(����	#FF2F5778$
 30,0q�$��Ukupna cena u korpi��$��Center=�$��Center=�$��Italic$�� $��1��$��Stretch=�
�������.+��� $
��Wrap=�
���   �$ 10q�$��13(�$��Center=�$��Center=����� $ UkupnaCenaUKorpi�$/  {0} RSD��� $��2��$��Center=�$��Center=�.+��� -   $��btnObrisiCeluKorpu��#��[ $	��120��$��30��$��0��$��Isprazni korpu��$��12(�$
4 Hand��{��� * DataContext.DaLiImaNestoUKorpi+$ PropertyChanged=�
   M S B A M L   `   `   `  ����   SmartSoftware&     SmartSoftware.PodaciOKupcuProzor   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     i eXceed.Wpf.AvalonDock.Themes.VS2010, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4g cXceed.Wpf.AvalonDock.Themes.Aero, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4h dXceed.Wpf.AvalonDock.Themes.Metro, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4[ WXceed.Wpf.AvalonDock, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4?avalon,http://schemas.xceed.com/wpf/xaml/avalondock     -     � Title$  ODABIR KUPCA��$	��700��$	��900�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � WindowStyle$ SingleBorderWindow=� � 	Resources    probaAvalon  stilDugmiciDodavanje �� 
TargetType �� Property �� Value �� 
TargetType	 �� Padding
 � ColumnDefinitions  sveSlike �� Source �� Path �� UpdateSourceTrigger �� Triggers P� Property P� Value   stilDugmiciOpremaSacuvajUnos  stilDugmiciOpremaOtkaziUnos ( $ctpPrikazDetaljaDugmiciKorisniciUnos % !stilDugmiciOpremaIzaberiKorisnika � Cursor . *ctpPrikazDetaljaDugmiciKorisniciAzuriranje  ctpPretragaIUnos ��Name ��Name � ToolTip 	 ctpGlavniProzorTitle 
� RadiusY � Color � Offset �Name � RowDefinitions �� RelativeSource �� AncestorType �� FallbackValue 
 ctpPrikazDetaljaKorisnici
 � Tag ��Name  ctpHederImePrezimeKorisnici&   System.Windows.Media.TextOptions ��Name  ��Name! ��Name" ^� 	IsEnabled# �� 
LineHeight$ �� Padding ' #ctpHederImePrezimeKorisniciKliknuto��%�3  &	        &	 1     7  7  &	 �    7  7  &	 &    7  7  &	 �
    &	 x    7  7  &	      &	 �    7  7  7  &		 �    7  7  &	
 �    &	  %    7  7  &	 �-    7  �� .R��� $��30��._��� ���� *Title+�� " ��.���  � ��
 ���� ��  � ��
 �������  � ��$ 35����  � ��$
 Auto����  � ��$ 10(���  � �� �� " ��.R��� #��z��$��1q�$
	 10,0q����	#FF2F5778._�� $��Center=�
 �� $��20 ��� $��10 ��� $
��Auto �.+��� $��0��$��Center=�._��� $��15��$��15������ 8   $! PutanjaDoSlikeNaruciNaLager�$ PropertyChanged=��� $��2��$��Center=�._��� #��z��$��Center=����	#FF2F5778 P�  �@J�IsMouseOver . . ���  � �� �	#FF2F5778��  � ��
 �������  � �� �� " ��.R��� #��z��$��0q�$
	 10,0q�
�������._�� $��Center=�
 �� $��20 ��� $��10 ��� $
��Auto �.+��� $��0��$��Center=�._��� $��15��$��15������ 8  $+ %PutanjaDoSlikeNaruciNaLagerHoverovano�$ PropertyChanged=��� $��2��$��Center=�._��� #��z��$��Center=�
��������� " ��.���  � ��
 ���� ��  � ��
 � d ���  � ��$ 40����  � ��$	 135����  � ��$
 20,0q���  � �� �� " ��.R��� #��z��$��1q�
��� d �$	 5q�._�� $��Center=�
 �� $��20 ��� $��5 ��� $
��Auto �.+��� $��0��._��� $��20��$��20��$��Center=�$��Center=����� 8   $! PutanjaDoSlikeSacuvajIzmene��� $��2��._��� $��Sačuvaj��$��Stretch=�$��Center=�$��10(�
��� d � P�  �@J�IsMouseOver . . ���  � ��
 � d ���  � �� �� " ��.R��� #��z��$��1q�
�������$	 5q�._�� $��Center=�
 �� $��20 ��� $��5 ��� $
��Auto �.+��� $��0��._��� $��20��$��20��$��Center=�$��Center=����� 8  $+ %PutanjaDoSlikeSacuvajIzmeneHoverovano��� $��2��._��� $��Sačuvaj��$��Stretch=�$��Center=�$��10(�
��������� " ��.���  � ��
 ���� ��  � ��
 �  ����  � ��$ 40����  � ��$	 135����  � ��$
 20,0q���  � �� �� " ��.R��� #��z��$��1q�
���  ��$		 5,0q�._�� $��Center=�
 �� $��20 ��� $��5 ��� $
��Auto �.+��� $��0��._��� $��20��$��20��$��Center=�$��Center=����� 8   $! PutanjaDoSlikeSkiniSaLagera��� $��2��._��� $��Otkaži��$��Stretch=�$��Center=�$��10(�
���  �� P�  �@J�IsMouseOver . . ���  � ��
 �  ����  � �� �� " ��.R��� #��z��$��1q�
�������$		 5,0q�._�� $��Center=�
 �� $��20 ��� $��5 ��� $
��Auto �.+��� $��0��._��� $��20��$��20��$��Center=�$��Center=����� 8  $+ %PutanjaDoSlikeSkiniSaLageraHoverovano��� $��2��._��� $��Otkaži��$��Stretch=�$��Center=�$��10(�
��������� .R�� $��0��$��Center=�
 �� $��* ��� $��* �.+��� $��0��$��Center=�._��� $��btnKorisniciSacuvajUnos��-   #���  $��11���� $��1��$��Center=�._��� $��btnOtkaziIzmene��-   #���  $��12���� " ��.���  � ��
 ���� ��  � ��
 � d ���  � ��$ 40����  � ��$	 160����  � �� �� " ��.R��� #��z��$��1q�
��� d �$		 5,0q�._�� $��Center=�
 �� $��20 ��� $��5 ��� $
��Auto �.+��� $��0��._��� $��20��$��20��$��Center=�$��Center=����� 8   $&  PutanjaDoSlikePrihvatiNarudzbinu��� $��2��._��� $��Odaberi ovog korisnika��$��Stretch=�$��Center=�$��10(�
��� d �$
 Hand�� P�  �@J�IsMouseOver . . ���  � ��
 � d ���  � �� �� " ��.R��� #��z��$��1q�
�������$		 5,0q�._�� $��Center=�
 �� $��20 ��� $��5 ��� $
��Auto �.+��� $��0��._��� $��20��$��20��$��Center=�$��Center=����� 8  $0 *PutanjaDoSlikePrihvatiNarudzbinuHoverovano��� $��2��._��� $��Odaberi ovog korisnika��$��Stretch=�$��Center=�$��10(�
��������� .R�� $��0��$��Center=�
 �� $��* ��� $��2* ��� $��* �.+��� $��1��$��Center=�._��� $"��btnOtkaziIzmeneKorisniciDole��-   #���  $��12��$
 Hand���� .R��� $ 
probaproba��$	 5,5,5,5q�$��Stretch=�$��0,0,0,3q�
����������	#0C000000._�� $��Center=�
 �� $��0 ��� $��0 ��� $
��Auto ��� $��25 ��� $
��Auto ��� $��25 ��� $
��Auto �.+��� $��2��._��� $ imgOblastOpremeSearch��-   $��20��$��20��$��Center=�$��Center=�$4 .Kliknite ovde kako biste pretražili korisnike��$
 Hand������ 8   $ PutanjaDoSlikeDugmeSearch��� $��4��._��� $ imgOblastOpremeDodaj��-   $��20��$��20��$��Center=�$��Center=�$9 3Kliknite ovde kako biste pretražili oblasti opreme��$
 Hand������ 8  $! PutanjaDoSlikeNaruciNaLager��� $��6��._��� $  imgOblastOpremeOsveziListu��-   $��20��$��20��$��Center=�$��Center=�$ Osveži listu��$
 Hand������ 8  $$ PutanjaDoSlikeOsveziHoverovano��� .R��� $��Stretch=���
� $ 2Q�.��� $ 	#FF2F5778��$ 1Q�� $ 	#FF17405E��._�� $��Stretch=�$��Stretch=�
 �� $��* ��� $
��Auto ��� $��* �.+��� $ grdSlikaINazivAplikacije��$��0��$
��Left=�$	��Top=�$	 	15,10,0,0q�._�� 
 �� $��15 ��� $��5 ��� $
��Auto �.+��� $��0��$��Center=�._��� $ 	slikaLogo��$��15��$��15��$��Center=�$��Center=����� 8   $  PutanjaDoSlikeLogoNormalno��� $��2��$��Center=�._��� $��tbNazivTitle��$��SmartSoftware��
�������$��11(�� $) #grdSlikaINazivTitlaIPodaciOProdavcu��$��1��$��Stretch=� �� $��40 ��� $��5 ��� $
��Auto �.+�� $��0��$��Center=�$��Bottom=�
 �� $��25 ��� $��10 ��� $
��Auto �.+��� $��0��$��Center=�._��� $ slikaTitlaLogo��$��25��$��25��$��Center=�$��Center=����� 8  $ PutanjaDoSlikeOdabirKupca��� $��2��$��Center=�._��� $��tbNazivTitla��
�������$��14(����� *Title+ �� *FindAncestor+$ WindowL�$ Title��� $��2��$��3��.+��� $��4��$��1��$��Stretch=�._�� $��Center=�
 �� $
��Auto ��� $��5 ��� $
��Auto �.+��� $��0��
�������$��11(�����  �� *FindAncestor+" �$/ )DataContext.TrenutniProdavac.ImeKorisnika�$ PropertyChanged=��� $��2��
�������$��11(�����  �� *FindAncestor+" �$3 -DataContext.TrenutniProdavac.PrezimeKorisnika�$ PropertyChanged=��� .R�� $��grdPrikazDetaljaKorisnici��$y�Visible=�$ 1�� �� $
��Auto ��� $��* ��� $
��Auto �.+��� $# brdKorisniciImeIPrezimeNaslov��-   $��0��
�������$��0,0,0,2q�$��Stretch=����	#0C000000$
 Hand��._��� $( "cclPrikazHederaImePrezimeKorisnici��#���  ��.  �� $
��Auto=�$
��Auto=�$��1��$	��Top=�$��Stretch=�|�.  .���� $&  brdPrikazDetaljaKorisniciSadrzaj��$
	 0,20q�$	��460��._��  �� $
��Auto ��� $��0 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto � ���� '��       " ��.���  � ��$
 Left=���  � ��$ Center=���  � ��$ 10(���  � ��$
 Auto����  � ��$
 Wrap=���  � ��
 �   ���  �@ TextFormattingMode$ Ideal=���  � ��$ SemiBold"��� '��       " ��.���  � ��$
 Left=���  � ��$ Bottom=���  � ��$ Center=���  � ��$ 11(���  � ��$	 250����  �@��TextWrapping$
 Wrap=���  � ��
 �   ���  � ��
 �������  � ��$ 0,0,0,1q���  � ��$	 0,2q���  �@��
IsReadOnly . ��  � ��
 ���� .+�� $ grdPromenaVisine��$��0��$��0��$��Stretch=�$��Stretch=�� $��2��$��Center=�
 �� $	��190 ��� $��20 ��� $	��250 �.+��� $ brdImeKorisnika��$��0��._��� $ tblckImeKorisnika��$	��IME���� $ brdImeKorisnikaTxtBox��-	   $��2�����	#FF2F5778$
��Auto��
������ $��Stretch=�._��� $  tboxImeKorisnika��-
   $��1������ *CurrentKorisnici.ImeKorisnika+$ PropertyChanged=�� $��4��$��Center=�
 �� $	��190 ��� $��20 ��� $	��250 �.+��� $ brdPrezimeKorisnika��$��0��._��� $ tblckPrezimeKorisnika��$��PREZIME���� $ brdPrezimeKorisnikaTxtBox��-   $��2�����	#FF2F5778$
��Auto��
������ $��Stretch=�._��� $  tboxPrezimeKorisnika��-   $��2������ *#!CurrentKorisnici.PrezimeKorisnika+$ PropertyChanged=�� $��14��$��Center=�
 �� $	��190 ��� $��20 ��� $	��250 �.+��� $$ brdBrojTelefonaKorisnikaHeader��$��0��._��� $  tblckBrojTelefonaKorisnika��$��BROJ TELEFONA���� $$ brdBrojTelefonaKorisnikaTxtBox��-   $��2�����	#FF2F5778$
��Auto��
������ ._��� $  tboxBrojTelefona��-   $��8������ *(&CurrentKorisnici.BrojTelefonaKorisnika+$ PropertyChanged=�� $��16��$��Center=�
 �� $	��190 ��� $��20 ��� $	��250 �.+��� $ brdMejlKorisnikaHeader��$��0��._��� $ tblckMejlKorisnika��$��ELEKTRONSKA POŠTA���� $ brdMejlKorisnikaTxtBox��-   $��2�����	#FF2F5778$
��Auto��
������ ._��� $  	tboxEmail��-   $��9������ * CurrentKorisnici.MejlKorisnika+$ PropertyChanged=��� $��2��$��0��$��2��
�������$��0,5,0,0q����	#0C000000$	��100��$��Stretch=�._�� $ brdDugmiciDole��$��Center=�$��Center=�$	��460��.+��� $&  cclPrikazDetaljaDugmiciKorisnici��#���  �� .R��� $	 	0,11,0,10q�._�� $" brdImeIUlogaKorisnikaDetalji��$	��Top=�$	��460��$	��460�� �� $
��Auto ��� $��5 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 � ���� '��       " ��.���  � ��$ 20(���  � ��$
 Auto����  � ��$	 130����  � �� �	#FF2F5778��  � ��$ Center=���  � ��$
 Left=���  � ��$ SemiBold"���  � ��$
 Wrap=���  �@ TextFormattingMode$ Ideal=�.+��� $! 	skrivenId��$��1��$y�Hidden=�� $��0��$��Stretch=�$��Stretch=� �� $��* ��� $
��Auto �.+��� $ tblckImePrezime��$��0������ *'%CurrentKorisnici.ImeIPrezimeKorisnika+$ PropertyChanged=�" �� *ECCurrentKorisnici.DaLiTekstImenaIPrezimenaKorisniciZauzimaViseRedova+$ PropertyChanged=� �� $	��200��._��� $
��Left=�$# 5��$
��Wrap=�$��Normal$�$��10(����� *'%CurrentKorisnici.ImeIPrezimeKorisnika+$ PropertyChanged=��� $.��(tblckImeIPrezimeKorisnikaDetaljnijeHeder��$��1��$	��...��$
��Left=�$��Center=�$y�Hidden=�$��0��$��20��" �� *ECCurrentKorisnici.DaLiTekstImenaIPrezimenaKorisniciZauzimaViseRedova+$ PropertyChanged=� �� $	��200��._��� $��Center=�$# 5��$
��Wrap=�$��Normal$�$��10(����� *'%CurrentKorisnici.ImeIPrezimeKorisnika+$ PropertyChanged=��� $��12(�$��2��$��0��$��2��
�������$$ 0,0,0,1q�$	��240��$��Bottom=�����  �� *FindAncestor+" �$$ DataContext.IzabraniNazivUloge�$ PropertyChanged=�� $2 ,grdPrikazObavestenjaZbogArhiviranogKorisnika��$��4��$��0��$��2������ *A?CurrentKorisnici.VisinaSakrivenogBorderaZbogPorukeZaArhiviranje+y��� *0.CurrentKorisnici.VisibilityPorukeZaArhiviranje+
 �� $��20 ��� $��5 ��� $
��Auto �.+��� $��17��$��17��$��Center=����� 8   $ PutanjaDoSlikeAlert��� $��2��$��12(�
���  ��$��OVAJ NALOG JE ARHIVIRAN��$
��Left=�$��Stretch=�$��Center=�$��SemiBold"�$��Italic$��� $+ %slikaStrelicaHederImePrezimeKorisnika��$��5��$��10��$��10��$��Bottom=�$��Center=�$y�Hidden=����� 8  $% PutanjaDoSlikeSakrijHederNaGore��� .R��� $	 0,9,0,6q�._�� $" brdImeIUlogaKorisnikaDetalji��$��Center=�$	��460��$	��460�� �� $��20 ��� $��10 � ���� '��       " ��.���  � ��$ 15(���  � �� �� *75CurrentKorisnici.BojaTekstaZaTrivjuItemKorisniciHeder+$ PropertyChanged=���  � ��$	 Top=���  � ��$
 Left=���  � ��$ SemiBold"���  � ��$
 Wrap=���  �@ TextFormattingMode$ Ideal=�.+��� $! 	skrivenId��$��2��$y�Hidden=�� $��0��$	��Top=�$
��Left=�
 �� $��* ��� $��15 �.+��� $ tblckImePrezime��$��0������ *'%CurrentKorisnici.ImeIPrezimeKorisnika+$ PropertyChanged=�" �� *ECCurrentKorisnici.DaLiTekstImenaIPrezimenaKorisniciZauzimaViseRedova+$ PropertyChanged=� �� ._��� $��Center=�$# 5��$
��Wrap=�$��Normal$�$��10(����� *'%CurrentKorisnici.ImeIPrezimeKorisnika+$ PropertyChanged=��� $.��(tblckImeIPrezimeKorisnikaDetaljnijeHeder��$��1��$	��...��$
��Left=�$��Center=�$y�Hidden=�$��0��" �� *ECCurrentKorisnici.DaLiTekstImenaIPrezimenaKorisniciZauzimaViseRedova+$ PropertyChanged=� �� $	��200��._��� $��Center=�$# 5��$
��Wrap=�$��Normal$�$��10(����� *'%CurrentKorisnici.ImeIPrezimeKorisnika+$ PropertyChanged=��� $+ %slikaStrelicaHederImePrezimeKorisnika��$��1��$��10��$��10��$��Bottom=�$��Center=�$y�Hidden=����� 8   $% PutanjaDoSlikeSakrijHederNaDole�'  !System.Windows.Shell.WindowChrome%   WindowChrome%   &   CaptionHeight$& 0Q�'   GlassFrameThickness$' 0,0,0,1q�(   ResizeBorderThickness$( 1q�.��]� -   $��dockPanelCeoSadrzaj��.+�� -   $��grdCeoSadrzaj�� �� $
��Auto ��� $��* �.+��� -   $��titleBar��$��0��$	��Top=�$��75����
� $ 2Q�.��� $ 	#FF2F5778��$ 1Q�� $ 	#FF17405E���  �� $
��Auto ��� $
��Auto �.+��� -   $ ctlTitleBar��$��0��$	��Top=�#��� 	 $��Stretch=�$��Center=�$��Stretch=�$��Center=�$��75����.  � -   $��grdDugmiciZaProzor��$��3��$��Right=�$	��Top=�
 �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -   ) ��Name$) dugmeMinimize��$��0��  dugmeMinimize#��[ ��.  �� -   $) dugmeMaximize��$��1��  dugmeMaximize#��[ ��.  �� -   $) dugmeZatvori��$��2��  
dugmeClose#��[ ��.  �� $��1����
� * 
� GradientOrigin$* 0.5,0.5&�+ 
� Center$+ 0.5,0.5&�$ 2Q�, 
� RadiusX$	, 0.7Q�.��� $ 	#FF270000��$ 0.945Q�� $ White��$ 0.207Q�� $ 	#FFEBF0F4��$ 0.425Q�� $ 	#FFE9EBEE��$ 0.702Q�- J� Effect- 3  -System.Windows.Media.Effects.DropShadowEffect  .   Color$. Black��/   	Direction$/ 90Q�0   
BlurRadius$0 10Q�1   ShadowDepth$1 2Q�2   RenderingBias$2 Quality=�._��  �� $��0 ��� $��0 ��� $��* �.+��� -   $��a��$��0��$��Stretch=�$
	 10,0q�
�������$	��0,5q���
� $ 2Q�.��� $ 	#FF2F5778��$ 1Q�� $ 	#FF17405E��� $��2��.+��� -   $ brdKorisniciCeoSadrzaj��$��2��$��0��._�� 
 �� $��* ��� $��2* � �� $
��Auto ��� $��5 ��� $��* �.+��� $��2��$��0��$��2��$��0q�$��Stretch=�$��Stretch=�$	 	10,0,10,0q�$y�Visible=�._�)  #Xceed.Wpf.AvalonDock.DockingManager  -   3  Name$3 DockingManagerKorisnici��$��1��
������ 4   AnchorableTitleTemplate#4 [  5   Theme5 ,  &Xceed.Wpf.AvalonDock.Themes.MetroTheme  6   Layout.6 ,  &Xceed.Wpf.AvalonDock.Layout.LayoutRoot  7   	RootPanel.7 -  'Xceed.Wpf.AvalonDock.Layout.LayoutPanel  8   Orientation$8 
Horizontal=��  �Xceed.Wpf.AvalonDock.Layout.LayoutPositionableGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutPanelElement, Xceed.Wpf.AvalonDock, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]9   
DockHeight$9 * ��	  �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutPanelElement, Xceed.Wpf.AvalonDock, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]: 	  Children.: ;
  5Xceed.Wpf.AvalonDock.Layout.LayoutAnchorablePaneGroup
  �  �Xceed.Wpf.AvalonDock.Layout.LayoutPositionableGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutAnchorablePane, Xceed.Wpf.AvalonDock, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]];   	DockWidth$	; 275 �<   
DockHeight$< * ��  �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutAnchorablePane, Xceed.Wpf.AvalonDock, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]=   Children.= 6  0Xceed.Wpf.AvalonDock.Layout.LayoutAnchorablePane  �  �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.LayoutAnchorable, Xceed.Wpf.AvalonDock, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]>   Children.> 2  ,Xceed.Wpf.AvalonDock.Layout.LayoutAnchorable  -   
probal��/  )Xceed.Wpf.AvalonDock.Layout.LayoutContent?   Title$? PRIKAZ LISTE��@   TextRenderingMode$@ 	ClearType=�A �� TextAlignment$A Center=�B   AutoHideWidth$	B 275Q�C   CanCloseC .  D   CanHideD .  $��10(�$��Italic$����	#FF2F5778E   Content.E �� 
�������$��1,5,1,5q�$��0,-5,0,0q�$��Stretch=�$	 0,0,0,0 q�._��  �� $��45 ��� $
��Auto ��� $��* �.+��� #��[ �� -   $ brdPretragaIUnosKorisnici��$��1��$��0,0,0,3q�
�������$��Stretch=�$��0��$	 5q����	#0C000000._�� .+��� -   $  tbPretragaKorisnici��$��0��$��Pretažite korisnike��$��30��$��Center=�$
��10,0q�$��10(�R� -   F R�Name$F trivjuKorisnici��$��2��
������ $��0q�
������ $��Stretch=�$��Stretch=�$��0,5,0,0q�$��Hidden=����� *ListaKorisnika+$ PropertyChanged=����� .R�� $ grdListaKorisnici��-    $��Stretch=�$��40��$��0,0,0,-1q�$
 Hand��$��Stretch=�
������ 
 �� $��40 ��� $��3 ��� $��* �.+�� $ grdHijerarhikalSlikaITgb��$��0��.+��� $  brdTriVjuHierarchicalSlika��
�������$	��0,1q�$��40��$��Stretch=�$��Stretch=�
�������._��� $��Center=�$��Center=�$��40��$��40������ *SlikaKorisnika+�� $ brdNazivIUlogaKorisnika��$��2��
�������$	��0,1q�$��40��$��Stretch=�$
��Left=�._��  �� $��* ��� $��5 ��� $��* �.+��� $��	skrivenId��$��Center=�$
$ 10,0q�$��11(�$y�Hidden=����� *IdKorisnici+���� *!BojaTekstaZaTrivjuItemKorisnici+���� **(PozadinskaBojaZaTrivjuItemKorisniciUloga+�� $/��)brdTriVjuHierarchicalImeIPrezimeKorisnika��$��0��$��2��$��Stretch=�$
	 10,0q�$��Bottom=�._�� $
��Left=�$��Bottom=�
 �� $��* ��� $
��Auto � ��%�   '��       �� " ��.���  � ��$ Stretch=���  � ��$ Center=���  � �� �� *!BojaTekstaZaTrivjuItemKorisnici+��  � ��$ 11(���  � �� �� * StilFontaZaTrivjuItemKorisnici+��  � ��$
 Auto��.+��� $ tbImeIPrezimeKorisnika��$��0������ *ImeIPrezimeKorisnika+" �� *+)DaLiTekstImenaIPrezimenaZauzimaViseRedova+$ PropertyChanged=� �� $	��200��._��� $��Center=�$# 5��$
��Wrap=�$��Normal$�$��10(����� *ImeIPrezimeKorisnika+�� $)��#tblckImeIPrezimeKorisnikaDetaljnije��$��1��$	��...��$��Right=�$y�Hidden=�$��0��" �� *+)DaLiTekstImenaIPrezimenaZauzimaViseRedova+$ PropertyChanged=� �� $	��200��._��� $��Center=�$# 5��$
��Wrap=�$��Normal$�$��10(����� *ImeIPrezimeKorisnika+�� $/��)brdTriVjuHierarchicalKorisnikPripadaUlozi��$��2��$��2��$��Stretch=�$
	 10,0q�$	��Top=�._�� $
��Left=�$	��Top=�
 �� $��* ��� $��15 � ��%�   '��       �� " ��.���  � ��$ Stretch=���  � ��$ Center=���  � �� �� *!BojaTekstaZaTrivjuItemKorisnici+��  � ��$ 9(���  � ��$
 Auto����  � ��$ Italic$���  � ��
 �����.+��� $ tbIzabranaUlogaKorisnika��$��0������ *
NazivUloge+" �� *)'DaLiTekstIzabraneUlogeZauzimaViseRedova+$ PropertyChanged=����� */-PozadinskaBojaZaTrivjuItemKorisniciUlogaKorpa+ �� $	��200��._��� $��Center=�$# 5��$
��Wrap=�$��Normal$�$��10(����� *
NazivUloge+�� $+��%tblckIzabranaUlogaKorisnikaDetaljnije��$��1��$	��...��$��Right=�$y�Hidden=�$��0��" �� *)'DaLiTekstIzabraneUlogeZauzimaViseRedova+$ PropertyChanged=����� */-PozadinskaBojaZaTrivjuItemKorisniciUlogaKorpa+ �� $	��200��._��� $��Center=�$# 5��$
��Wrap=�$��Normal$�$��10(����� *
NazivUloge+���� " Q�.���  � �� �� " Q�.R�� $��Stretch=�
 �� $��* � �� $
��Auto ��� $
��Auto �.+��� $��Bd��#��z��#	 z��#��z��#��z��G J� SnapsToDevicePixelsG . $��0��._��� $��PART_Header��#��z��#��z��H �� HeaderStringFormatI �� ContentStringFormat#I zH $��Header��#��z��#G zG �� $��	ItemsHost��$��0��$��1��$��2��$��0q�J �� TriggersJ P�  �@Q�
IsSelected . . ��� K �� 
TargetName$K Bd�� � ��
 ���� ��  � �� C� L C� ResourceKey#L Z��P�  � {� .  . ���  � �� C� #L Z��9  3Xceed.Wpf.AvalonDock.Layout.LayoutDocumentPaneGroup  �  �Xceed.Wpf.AvalonDock.Layout.LayoutPositionableGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutDocumentPane, Xceed.Wpf.AvalonDock, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]M   	DockWidth$M * �N   
DockHeight$N * ��  �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutDocumentPane, Xceed.Wpf.AvalonDock, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]O   Children.O 4  .Xceed.Wpf.AvalonDock.Layout.LayoutDocumentPane  �  �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.LayoutContent, Xceed.Wpf.AvalonDock, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]P   Children.P 0  *Xceed.Wpf.AvalonDock.Layout.LayoutDocument  $? PRIKAZ DETALJA��$@ 	ClearType=����	#FF2F5778C .  -!   -)LejautDokumentTitlePrikazDetaljaKorisnici��.E � .+��� -"   $ brdPrikazDetaljaKorisnici��
�������$��1,5,1,5q�$��	0,-5,0,-5q�$��Stretch=�$	 0q�Q J� Opacity$Q 0Q�R K� ZIndex$R 0������ *1/CurrentKorisnici.BojaCentralnogPrikazaKorisnici+._��� -#   $ cclPrikazDetaljaKorisnici��#��� 
 �� -$   $% brdPrazanPrikazDetaljaKorisnici��
�������$��1,5,1,5q�$��	0,-5,0,-5q�$��Stretch=�$	 0q�$Q 1Q�$R 1����
� $* 0.5,0.5&�$+ 0.5,0.5&�$ 4Q�$	, 1.3Q�.��� $ 	#FF270000��$ 0.945Q�� $ White��$ 0.207Q�� $ 	#FFEBF0F4��$ 0.425Q�� $ 	#FFE9EBEE��$ 0.702Q�._�� .+��� -%   $, &brdPrazanPrikazDetaljaKorisniciSadrzaj��$��Center=�$��Center=�._��  �� $
��Auto ��� $��5 ��� $
��Auto ��� $��15 ��� $
��Auto � ��%   '��       �� " ��.���  � ��$ Italic$���  � �� �	#FF2F5778��  � ��$ 12(���  � ��$ Center=�.+��� $��0��$��Odaberite korisnika���� $��2��$	��ili���� $��4��$��Center=�._��� -&   $��btnUnesiNovogKorisnika��$��UNOS NOVOG KORISNIKA��#���  $y�Visible=�
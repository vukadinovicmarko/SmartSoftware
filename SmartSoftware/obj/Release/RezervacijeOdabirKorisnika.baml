   M S B A M L   `   `   `  ���� H  DSmartSoftware, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:SmartSoftware.ViewModelSmartSoftware.ViewModel  .    (SmartSoftware.RezervacijeOdabirKorisnika   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     i eXceed.Wpf.AvalonDock.Themes.VS2010, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4g cXceed.Wpf.AvalonDock.Themes.Aero, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4h dXceed.Wpf.AvalonDock.Themes.Metro, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4[ WXceed.Wpf.AvalonDock, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4?avalon,http://schemas.xceed.com/wpf/xaml/avalondock     X	 TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4:
wpfToolkit)http://schemas.xceed.com/wpf/xaml/toolkit 	 9smartSoftware%clr-namespace:SmartSoftware.ViewModel 	 -     � Title$&   ODABIR KORISNIKA ZA REZERVISANJE��$	��650��$
��1000�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$ SingleBorderWindow=� � 	Resources    ctpGlavniProzorTitle 
� RadiusY � Color � Offset � ColumnDefinitions ��Name	 �� Padding
 ��Name  sveSlike �� Source �� Path �Name � RowDefinitions �� RelativeSource �� AncestorType �� FallbackValue �� UpdateSourceTrigger ) %stilDugmiciIzbrisiOpremuIzRezervacije �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers P� Property P� Value  	dugmeInfo  dugmeInfoKliknuto  dugmeOtkazi 
 crveno  dugmeIzbrisi �� Mode ) %dugmeIsprazniTrenutnuListuRezervacija �� Padding 	 dugmeIzbrisiRezervaciju 
 dugmeRezervisi $  dugmeAzurirajOdabranuRezervaciju  zeleno2  stilDugmici  stilDugmiciKliknuto��%=-  &	        7  7  &	 �    7  7  &	 �    &	 	    &	 /    &	 �    &	 �    &	      &		 �    &	
 �    &	 `!    &	 V&    &	 T'    &	 C+    �� .R��� $��Stretch=���
� $ 2Q�.��� $ 	#FF2F5778��$ 1Q�� $ 	#FF17405E��._�� $��Stretch=�$��Stretch=� �� $��* ��� $
��Auto ��� $��* �.+��� $ grdSlikaINazivAplikacije��$��0��$
��Left=�$	��Top=�$	 	15,10,0,0q�._��  �� $��15 ��� $��5 ��� $
��Auto �.+��� $��0��$��Center=�._��� $
 	slikaLogo��$��15��$��15��$��Center=�$��Center=����� 8   $  PutanjaDoSlikeLogoNormalno��� $��2��$��Center=�._��� $��tbNazivTitle��$��SmartSoftware��
�������$��11(�� $) #grdSlikaINazivTitlaIPodaciOProdavcu��$��1��$��Stretch=� �� $��40 ��� $��5 ��� $
��Auto �.+�� $��0��$��Center=�$��Bottom=� �� $��25 ��� $��10 ��� $
��Auto �.+��� $��0��$��Center=�._��� $
 slikaTitlaLogo��$��25��$��25��$��Center=�$��Center=����� 8  $ PutanjaDoSlikeOdabirKupca��� $��2��$��Center=�._��� $��tbNazivTitla��
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
 �������  � �� �	#FF17405E��  � �� �� " ��.R��� #��z��$��1q����	#FF17405E._�� $��Center=�$��Center=�.+��� $��20��$��20������ * DataContext.PutanjaDoSlikeInfo+$ PropertyChanged=� P�  �@J�IsMouseOver . . ���  � �� �	#FF17405E��  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������._�� $��Center=�$��Center=�.+��� $��20��$��20������ **(DataContext.PutanjaDoSlikeInfoHoverovano+$ PropertyChanged=��� " ��.���  � �� �	#FF17405E��  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������._�� $��Center=�$��Center=�.+��� $��20��$��20������ **(DataContext.PutanjaDoSlikeInfoHoverovano+$ PropertyChanged=� P�  �@J�IsMouseOver . . ���  � ��
 �������  � �� �	#FF17405E��  � �� �� " ��.R��� #��z��$��1q����	#FF17405E._�� $��Center=�$��Center=�.+��� $��20��$��20������ * DataContext.PutanjaDoSlikeInfo+$ PropertyChanged=��� " ��.���  � ��
 �������  � ��
 �  ����  � ��$	 100����  � �� �� " ��.R��� #��z��$��1q�
���  ��$
	 15,0q�._�� $��Center=�$��Center=�.+��� $��Otkaži��
���  �� P�  �@J�IsMouseOver . . ���  � ��
 �  ����  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������$
	 15,0q�._�� $��Center=�$��Center=�.+��� $��Otkaži��
��������� " ��.���  � ��
 �������  � ��
 �  ����  � �� �� " ��.R��� #��z��$��1q�
�������._��� $��Center=�$��Center=�$
��Auto�� P�  �@J�IsMouseOver . . ���  � ��
 �������  � ��
 �  ���� " ��.���  � ��
 �������  � ��
 �  ����  � �� �� " ��.R��� #��z��$��1q�
�������._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� *" DataContext.PutanjaDoSlikeDelete+ �� $ FindAncestor=�" ��$ PropertyChanged=��� $��2��$��Izbriši��$��Center=�$
��Left=� P�  �@J�IsMouseOver . . ���  � ��
 �  ����  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
���  ��._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� *,*DataContext.PutanjaDoSlikeDeleteHoverovano+ �� $ FindAncestor=�" ��$ PropertyChanged=��� $��2��$��Izbriši��$��Center=�$
��Left=��� " ��.���  � ��
 �������  � ��
 �  ����  � �� �� " ��.R��� #��z��$��1q�
���  ��._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� *)'DataContext.PutanjaDoSlikeIsprazniKorpu+$ PropertyChanged=��� $��2��$ 0,0,5,0q�$��Isprazni ovu listu��$��Center=�$
��Left=� P�  �@J�IsMouseOver . . ���  � ��
 �  ����  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� *31DataContext.PutanjaDoSlikeIsprazniKorpuHoverovano+$ PropertyChanged=��� $��2��$ 0,0,5,0q�$��Isprazni ovu listu��$��Center=�$
��Left=�P�  � {� .  . ���  � ��
 �������  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� *42DataContext.PutanjaDoSlikeIsprazniKorpuOnemoguceno+$ PropertyChanged=��� $��2��$ 0,0,5,0q�$��Isprazni ovu listu��$��Center=�$
��Left=��� " ��.���  � ��$ 30����  � ��
 ���� ��  � ��
 �  ����  � �� �� " ��.R��� #��z��$��1q�
���  ��$	 10,0,0,0q�._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� *.,DataContext.PutanjaDoSlikePonistiRezervaciju+$ PropertyChanged=��� $��2��$ 0,0,15,0q�$#��Izbriši odabranu rezervaciju��$��Center=�$
��Left=� P�  �@J�IsMouseOver . . ���  � ��
 �  ����  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������$	 10,0,0,0q�._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� *86DataContext.PutanjaDoSlikePonistiRezervacijuHoverovano+$ PropertyChanged=��� $��2��$ 0,0,15,0q�$#��Izbriši odabranu rezervaciju��$��Center=�$
��Left=�P�  � {� .  . ���  � ��
 �������  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������$	 10,0,0,0q�._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� *97DataContext.PutanjaDoSlikePonistiRezervacijuOnemoguceno+$ PropertyChanged=��� $��2��$ 0,0,15,0q�$#��Izbriši odabranu rezervaciju��$��Center=�$
��Left=��� " ��.���  � ��
 �������  � ��
 � d ���  � ��$ 30����  � �� �� " ��.R��� #��z��$��1q�
��� d �$	 10,0,0,0q�._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� *%#DataContext.PutanjaDoSlikeRezervisi+$ PropertyChanged=��� $��2��$ 0,0,15,0q�$��
Rezerviši��$��Center=�$
��Left=� P�  �@J�IsMouseOver . . ���  � ��
 � d ���  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������$	 10,0,0,0q�._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� */-DataContext.PutanjaDoSlikeRezervisiHoverovano+$ PropertyChanged=��� $��2��$ 0,0,15,0q�$��
Rezerviši��$��Center=�$
��Left=�P�  � {� .  . ���  � ��
 �������  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������$	 10,0,0,0q�._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� *0.DataContext.PutanjaDoSlikeRezervisiOnemoguceno+$ PropertyChanged=��� $��2��$ 0,0,15,0q�$��
Rezerviši��$��Center=�$
��Left=��� " ��.���  � ��
 ���� ��  � ��
 � d ���  � ��$ 30����  � �� �� " ��.R��� #��z��$��1q�
��� d �$	 10,0,0,0q�._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� *%#DataContext.PutanjaDoSlikeRezervisi+$ PropertyChanged=��� $��2��$ 0,0,15,0q�$$��Ažuriraj odabranu rezervaciju��$��Center=�$
��Left=� P�  �@J�IsMouseOver . . ���  � ��
 � d ���  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������$	 10,0,0,0q�._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� */-DataContext.PutanjaDoSlikeRezervisiHoverovano+$ PropertyChanged=��� $��2��$ 0,0,15,0q�$$��Ažuriraj odabranu rezervaciju��$��Center=�$
��Left=�P�  � {� .  . ���  � ��
 �������  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������$	 10,0,0,0q�._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� *0.DataContext.PutanjaDoSlikeRezervisiOnemoguceno+$ PropertyChanged=��� $��2��$ 0,0,15,0q�$$��Ažuriraj odabranu rezervaciju��$��Center=�$
��Left=��� " ��.���  � ��
 � d ���  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������._��� $��Center=�$��Center=�$
��Auto�� P�  �@J�IsMouseOver . . ���  � ��
 �������  � ��
 � d ��� " ��.���  � ��
 ���� ��  � ��$ 40����  � ��
 �������  � ��$
 Auto����  � ��$ 10(���  � ��$ 0q���  � ��
 �������  � ��$
 Auto����  � �� �� " ��.R��� #��z��$��0q�$
	 10,0q�
�������._�� $��Center=� �� $��20 ��� $��10 ��� $
��Auto �.+��� $��0��$��Center=�._��� $��15��$��15������ *,*DataContext.PutanjaDoSlikeDodajRezervaciju+$ PropertyChanged=��� $��2��$��Center=�._��� $��NOVA REZERVACIJA��$��10(�$��Center=� P�  �@J�IsMouseOver . . ���  � ��
 �������  � �� �	#FF18486A��  � �� �� " ��.R��� #��z��$��0q�$
	 10,0q�
�������._�� $��Center=� �� $��20 ��� $��10 ��� $
��Auto �.+��� $��0��$��Center=�._��� $��15��$��15������ *64DataContext.PutanjaDoSlikeDodajRezervacijuHoverovano+$ PropertyChanged=��� $��2��$��Center=�._��� $��NOVA REZERVACIJA��$��10(�$��Center=��� " ��.���  � �� �#128689��  � ��$ 30����  � ��
 �������  � ��$	 135����  � ��$ 12(���  � ��$ 0q���  � �� �� " ��.R��� #��z��$��0q�$
	 10,0q�._��� $��Center=�$��Center=�$
��Auto�� P�  �@J�IsMouseOver . . ���  � �� �#128689��  � ��
 �����'  !System.Windows.Shell.WindowChrome   WindowChrome      CaptionHeight$ 0Q�   GlassFrameThickness$ 0,0,0,1q�   ResizeBorderThickness$ 1q�.��]� .+�� -   $��grdCeoSadrzaj�� �� $
��Auto ��� $��* �.+��� -   $��titleBar��$��0��$	��Top=�$��75��
���   ���
� $ 2Q�.��� $ 	#FF2F5778��$ 1Q�� $ 	#FF17405E���  �� $
��Auto ��� $
��Auto �.+��� -     ��Name$  ctlTitleBar��$��0��$	��Top=�#���   $��Stretch=�$��Center=�$��Stretch=�$��Center=�$��75����.  � -   $��grdDugmiciZaProzor��$��3��$��Right=�$	��Top=� �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -   ! ��Name$! dugmeMinimize��$��0��  dugmeMinimize#��[ ��.  �� -   $! dugmeMaximize��$��1��  dugmeMaximize#��[ ��.  �� -   $! dugmeZatvori��$��2��  
dugmeClose#��[ ��.  �� $��1��
�������$��0,5,0,0q���
� " 
� GradientOrigin$" 0.5,0.5&�# 
� Center$# 0.5,0.5&�$ 2Q�$ 
� RadiusX$	$ 0.7Q�.��� $ 	#FF270000��$ 0.945Q�� $ White��$ 0.207Q�� $ 	#FFEBF0F4��$ 0.425Q�� $ 	#FFE9EBEE��$ 0.702Q�% J� Effect% 3  -System.Windows.Media.Effects.DropShadowEffect  &   Color$& Black��'   	Direction$' 90Q�(   
BlurRadius$( 10Q�)   ShadowDepth$) 2Q�*   RenderingBias$* Quality=�._��  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* �.+�� -	   $��grdGornjiDeo��$��2��$y�Visible=�$��45�� �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��1��$
	 20,0q�$��45��
�������$��0,0,0,5q���
� $ 2Q�.��� $ 	#FF2F5778��$ 1Q�� $ 	#FF17405E��._�� -
   $��
grdDugmici��$��0��$��40�� �� $	��550 ��� $��* �.+�� $��0��$��Center=�$
��Left=� �� $
��Auto � �� $	��550 �.+�� -   $��gridPretraga��$��0��$��1��$	��480��$
��Left=�$y�Visible=�$��30��.+��� -   + ��Name$+ 
tbPretraga��$��30��$��Center=�$��Stretch=�$��Center=�$��8,0,0,0q��� $��0��$��1��, J� IsHitTestVisible, .  $ ��Pretražite rezervacije...��$��Center=�$��Stretch=�$��10,0,0,0q����	#FF7E7E7E$��10(����� " ��.���  � y�$ 	Collapsed=� �� - �� Binding- �� *Text+. �� ElementName$. 
tbPretraga��/ �� Value/  .a���  � y�$ Visible=�� $��1��$��Right=� �� $
��Auto ��� $��10 �.+��� -   $��btnNapraviRezervaciju��$��0��$��NOVA REZERVACIJU��#���  $y�Visible=�0 � Cursor$
0 Hand���� -   $��sadrzajUnosNovogKorisnika��$��4��$	��700��$y�Hidden=�._��  �� $��5* ��� $��5 ��� $	��100 �.+��� $��0��
����  �$��0q�$��Center=�$	��335��$��Center=�._�� $��Center=� �� $
��Auto ��� $��0 ��� $
��Auto � �� $	��300 ��� $��5 ��� $��30 � ��%�   '��       �� " ��.���  � ��
 ���� ��  � �� �	#FF17405E��  � ��$	 300����  � ��$ 30����  � ��$ Center=���  � ��$ Center=���  � ��$ Stretch=���  � ��$ 5,0,0,0q���  � ��$ 30��.+��� -   $��brdIme��$��0��$��0��$��0q�
���  ��$��Center=�._��  �� $
��Auto ��� $
��Auto �.+��� -   $��brdTextBoxIme��$��Stretch=�._��� -   $+ 
textBoxIme��$��0�����	#FF2F5778$��0��1 � ToolTip$$1 Morate uneti minimum dva slova��$��1q��� $��0��, .  $	��Ime��$��Center=�$
��Left=�$��10,0,0,0q����	#7F17405E$��Italic$����� " ��.���  � y�$ 	Collapsed=� �� - �� *Text+$. 
textBoxIme��/  .a���  � y�$ Visible=��� -   $��
brdInfoIme��$��2��$��Center=�$��Center=�$	��298��$	 10q�$��0q�
���   �$��0��$y�Hidden=�._��� $$��Morate uneti minimum dva slova��$
��Wrap=����	#FF17405E$��Center=��� $��2��$��0��$��Stretch=�$��Stretch=�
�������2 K� ZIndex$2 -1��$	��298��%   $) 2Q�$( 3Q�$	' 270Q�$
& Gray��$* Quality=��� -   $��
btnInfoIme��$��0��$��2��#��[ $��30��$	��Top=�$
0 Hand���� -   $��brdBrojTelefona��$��2��$��0q�
���  ��$��Center=�$��0,5,0,0q�._��  �� $
��Auto ��� $
��Auto �.+��� -   $��brdTextBoxBrTelefona��._��� -   $+ textBoxBrojTelefona�����	#FF2F5778$��1��$��1q�.��1 �� (&Morate uneti broj telefona u formatu :�� �� 1. korak - (+381xx ili 0xx)�� �� 532. korak (xx-xx-xxx) ili (xxx-xx-xx) ili (xxx-xxxx)�� �� 86Umesto crte (-), dozvoljeno je unošenje kose crte (/)�� �� .,Nije dozvoljeno korišćenje razmaka (SPACE).���� , .  $��Broj telefona��$��Center=�$
��Left=�$��10,0,0,0q����	#7F17405E$��Italic$����� " ��.���  � y�$ 	Collapsed=� �� - �� *Text+$. textBoxBrojTelefona��/  .a���  � y�$ Visible=��� -   $��brdInfoBrTelefona��$��2��$��Center=�$��Center=�$	��298��$	 10q�$��0q�
���   �$��0��$y�Hidden=�._��� $����Morate uneti broj telefona u formatu :

 1. korak -  (+381xx ili 0xx)
 2. korak (xx-xx-xxx) ili (xxx-xx-xx) ili (xxx-xxxx)
 Umesto crte (-), dozvoljeno je unošenje kose crte (/)

 Nije dozvoljeno korišćenje razmaka (SPACE)��$
��Wrap=����	#FF17405E�� $��2��$��0��$��Stretch=�$��Stretch=�
�������$2 -1��$	��298��%   $) 2Q�$( 3Q�$	' 270Q�$
& Gray��$* Quality=��� -   $��btnInfoBrTelefona��$��2��$��2��#��[ $��30��$	��Top=�$��0,5,0,0q�$
0 Hand���� $��2�����	#06000000
�������$��0,5,0,0q�._�� -   $,��&gridDugmiciSveRezervacijeUnosKorisnika�� �� $��* ��� $
��Auto ��� $��* �.+�� $��1��$��Center=�$��Center=�$��30�� �� $
��Auto ��� $	��100 ��� $
��Auto �.+��� -   $��btnRezervisiNaIme��$��0��#��[
 3 �� 	IsDefault3 . $
0 Hand��{��� *
Validacija+$ PropertyChanged=��� -   $/��)btnOtkaziUnosNovogKorisnikaZaRezervisanje��$��2��#��[ $
0 Hand���� -   $��sadrzajOdabirKorisnika��$��4��$	��700��$y�Visible=�$��0,5,0,0q�._��  �� $��5* ��� $��5 ��� $	��100 � �� $	��520 ��� $��15 ��� $��* �.+��� $	��Top=�
����  �$��0q�._��  �� $��* ��� $
��Auto ��� $��* �.+�� -   $��grdKupci��$��0��$��1��$
��Auto��$��Center=�$��Center=� �� $
��Auto ��� $��15 ��� $��* ��� $��1 ��� $
��Auto � �� $	��140 ��� $��5 ��� $	��115 ��� $��5 ��� $	��115 ��� $��5 ��� $	��115 ��� $��20 � ��%{  '��       '�� �     �� " ��.���  � ��$ Center=���  � ��$ Center=���  � ��$ Normal$���  � ��$
 Auto����  � ��$ 10(���  � ��
 �������  � �� �	#FF2F5778�� " ��.���  � ��$ Center=���  � ��$ Center=���  � ��$ Normal$���  � ��$
 Auto����  � ��$ 10(���  �@��Padding$	 5,0q�.+��� -   $��	skrivenId��$��0��$��1��$��Marko��$y�Hidden=��� $��Ime i br. telefona��$��0��$��0���� $��Datum ažuriranja��$��0��$��2���� $��Datum isteka��$��0��$��4���� $��Datum rezervisanja��$��0��$��6���� $��2��$��0��$��8��$
��Auto=�$��Stretch=�.��� -   $��	gridKupci�� �� $��* ��� $
��Auto � �� $	��140 ��� $��5 ��� $	��115 ��� $��5 ��� $	��115 ��� $��5 ��� $	��115 ��� $��20 �.+��� -    $��lsbKupci��$��0��$��8��$��Center=�$	��Top=�$��Stretch=����� *Rezervacije+���� .R�� $��grdPrikazRezervacija��-!   $��50��
������ $
0 Hand�� ��%{  '��       '�� �     �� " ��.���  � ��$ Center=���  � ��$ Center=���  � ��$ Italic$���  � ��$
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
�������$��0,0,0,1q��� 4 �� Orientation$4 
Horizontal=�$��0��$��1��$��Stretch=�$��Center=�.+��� #��["��� -"   $��skrolVjuverSveRezervacije��$��2��$
��Auto=�$y�Hidden=�.���� -#   $��itcSveRezervacije��$��2��$	��Top=�$��0��$��Stretch=�|�.  ���� *CurrentOprema+$ PropertyChanged=����� .R��� $ 	brdOprema��$��Stretch=�$��Center=����	#FFFAFAFA$��0,0,0,20q�
�������$��1q�._�� $��Stretch=� �� $��30 ��� $	��180 ��� $��30 � �� $��65 ��� $	��205 � 5 � DataContext6 �� Self7 �� 
LineHeight��%r  '��       '�� �     �� " ��.���  � ��$ 10(���  � ��$ Stretch=���  � ��$ Right=���  � ��$ Center=���  � ��$ Center=���  � ��
 ������� " ��.���  �@��
LineHeight$ 30����  �@^�	Placement$ 
MousePoint=���  �@^�ShowDuration$ 12000����  �@��Padding$	 0,6q���  � ��$	 Top=���  � ��$ 10(���  � ��$ Italic$���  � ��
 �   ���  � ��$ Stretch=���  �@�ToolTip _� 5 �� $ PlacementTarget�# Z6 .���� $	��150��._��� $��Center=�$7 5��$
��Wrap=�$��Normal$�$��10(����� *Text+.+��� $ 
brdBorderi��$��0��$��2��$��3��$��Stretch=�$��Stretch=��� $��	skrivenId��$��0��$y�Hidden=����� *
IdOprema+�� $ brdOpremaNaziv��$��0��$��0��$��2��
�������$��Center=�
�������$��0,0,0,1q�$��30��$
	 10,0q�$��Stretch=�$	��268��._�� $��Center=� �� $��* ��� $
��Auto �.+��� $��tblckOpremaNaziv��$��0��$��12(�$
��Left=�$	 0,7q�$��Normal$����� *Name+8 ^� 	IsEnabled8 �� *#!DaLiTekstNaslovaZauzimaViseRedova+$ PropertyChanged=�1 �� $	��270��._��� $��Center=�$7 5��$
��Wrap=�$��Normal$�$��10(����� *Name+�� $ ��tblckOpremaNazivDetaljnije��$��1��$	��...��$��12(�$��Right=�$	 0,7q�$��Normal$�$y�Hidden=�$��0��8 �� *#!DaLiTekstNaslovaZauzimaViseRedova+$ PropertyChanged=�1 �� $	��270��._��� $��Center=�$7 5��$
��Wrap=�$��Normal$�$��10(����� *Name+�� $ brdOpremaSlika��$��1��$��0��$��75��$		 0,5q�._��� $��55��$��55��$��Center=�$��Center=����� *Slika+�� $ brdOpremaSredina��$��1��$��1��$	��150��$	 0,0,10,0q�._��  �� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 �.+��� $��0��$��Stretch=�$��Stretch=�._��  �� $	��100 ��� $��* ��� $
��Auto �.+��� 9 ��Name$9 lblProizvodjac��$��0��$��Proizvodjač : ���� : ��Name$: tblckOpremaProizvodjac��$��1������ *Proizvodjac+8 �� *(&DaLiTekstProizvodjacaZauzimaViseRedova+$ PropertyChanged=��� $&�� tblckOpremaProizvodjacDetaljnije��$��2��$��. . .��$��Normal$�$y�Hidden=�$��0��$��Right=�8 �� *!DaLiTekstOpisaZauzimaViseRedova+$ PropertyChanged=�1 �� $	��150��._��� $��Center=�$7 5��$
��Wrap=�$��Normal$�$��10(����� *Proizvodjac+�� $��1��$��Stretch=�$��Stretch=�._��  �� $	��100 ��� $��* ��� $
��Auto �.+��� $9 lblModel��$��0��$��Model : ���� $: tblckOpremaModel��$��1������ *Model+8 �� *" DaLiTekstModelaZauzimaViseRedova+$ PropertyChanged=��� $ ��tblckOpremaModelDetaljnije��$��2��$��. . .��$��Normal$�$y�Hidden=�$��0��$��Right=�8 �� *!DaLiTekstOpisaZauzimaViseRedova+$ PropertyChanged=�1 �� $	��150��._��� $��Center=�$7 5��$
��Wrap=�$��Normal$�$��10(����� *Model+�� $��2��$��Stretch=�$��Stretch=�._�� $��Center=� �� $	��100 ��� $��* ��� $
��Auto �.+��� $9 lblOpis��$��0��$��Opis : ���� $: tblckOpremaOpis��$��1������ *Opis+8 �� *!DaLiTekstOpisaZauzimaViseRedova+$ PropertyChanged=��� $��tblckOpremaOpisDetaljnije��$��2��$	��...��$��Normal$�$y�Hidden=�$��0��$��Right=�8 �� *!DaLiTekstOpisaZauzimaViseRedova+$ PropertyChanged=�1 �� $	��150��._��� $��Center=�$7 5��$
��Wrap=�$��Normal$�$��10(����� *Opis+�� $��3��$��Stretch=�$��Stretch=�._�� $��Center=� �� $	��100 ��� $��* ��� $
��Auto �.+��� $9 lblCena��$��0��$��Cena po proizvodu : ���� $: tblckOpremaCena��$��1������ *Cena+8 �� * DaLiTekstCeneZauzimaViseRedova+$ PropertyChanged=��� $��tblckOpremaCenaDetaljnije��$��2��$	��...��$��Normal$�$y�Hidden=�$��0��$��Right=�8 �� * DaLiTekstCeneZauzimaViseRedova+$ PropertyChanged=�1 �� $	��150��._��� $��Center=�$7 5��$
��Wrap=�$��Normal$�$��10(����� *Cena+�� $��4��$��Stretch=�$��Stretch=�._�� $��Center=� �� $	��100 ��� $��* ��� $
��Auto �.+��� $9 lblKolicina��$��0��$��Količina : ���� $: tblckOpremaKolicina��$��1������ *TmpIzabranaKolicina+8 �� *$"DaLiTekstKolicineZauzimaViseRedova+$ PropertyChanged=��� $#��tblckOpremaKolicinaDetaljnije��$��2��$	��...��$��Normal$�$y�Hidden=�$��0��$��Right=�8 �� *$"DaLiTekstKolicineZauzimaViseRedova+$ PropertyChanged=�1 �� $	��150��._��� $��Center=�$7 5��$
��Wrap=�$��Normal$�$��10(����� *TmpIzabranaKolicina+�� $��5��$��Stretch=�$��Stretch=�._�� $��Center=� �� $	��100 ��� $��* ��� $
��Auto �.+��� $9 lblUkupnaCena��$��0��$��Ukupna cena : ���� $: tblckOpremaUkupnaCena��$��1������ *	SumCena+8 �� *&$DaLiTekstUkupneCeneZauzimaViseRedova+$ PropertyChanged=��� $%��tblckOpremaUkupnaCenaDetaljnije��$��2��$	��...��$��Normal$�$y�Hidden=�$��0��$��Right=�8 �� *&$DaLiTekstUkupneCeneZauzimaViseRedova+$ PropertyChanged=�1 �� $	��150��._��� $��Center=�$7 5��$
��Wrap=�$��Normal$�$��10(����� *	SumCena+�� $ brdOpremaCena��$��2��$��0��$��2��
�������
�������$��0,1,0,0q�$��Stretch=�._��� $��btnObrisiIzRezervacija��$��0��$��30��#���  �� $��2��$��0��$��3�����	#06000000
�������$��0,5,0,0q�._�� -$   $��gridDugmiciSveRezervacije�� �� $��* ��� $
��Auto ��� $��* �.+�� $��1��$��0��$��Center=�$��Center=�.+��� -%   $��btnRezervisiUpdate��#��[ 3 . $
0 Hand��{��� * TempRezervacija.KliknutoNaGrid+$ PropertyChanged=�
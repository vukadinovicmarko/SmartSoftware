   M S B A M L   `   `   `  ����   SmartSoftware"    SmartSoftware.KupovinaProzor   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4:
wpfToolkit)http://schemas.xceed.com/wpf/xaml/toolkit  -     � Title$
  KASA��$	��700��$	��800�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$ SingleBorderWindow=� � 	Resources    ctpGlavniProzorTitle 
� RadiusY � Color � Offset � ColumnDefinitions ��Name	 �� Padding
 ��Name  sveSlike �� Source �� Path �Name � RowDefinitions �� UpdateSourceTrigger �� RelativeSource �� AncestorType �� FallbackValue  ctemplateBezPopusta �� Padding �� StringFormat  ctemplateSaPopustom �� 
TargetType  kupovinaLabel �� Property �� Value  grdLabel  grdLabelPopust  grdLabelNijeCekirano  grdLabelPoeni 	 	dugmeBelo �� 
TargetType �� Triggers P� Property P� Value 
 
dugmePlavo 
 crveno  	dugmeKupi  dugmePreskociIKupi  stilDugmici  stilDugmiciKliknuto��%�  &	        7  &	 �    &	 �    &	     '�� �    &	 V	    &	 �	    &	 �	    &	 
    &		 V
    &	
 �    &	 P    &	 �    &	     &	 3    &	 �    �� .R��� $��Stretch=���
� $ 2Q�.��� $ 	#FF2F5778��$ 1Q�� $ 	#FF17405E��._�� $��Stretch=�$��Stretch=� �� $��* ��� $
��Auto ��� $��* �.+��� $ grdSlikaINazivAplikacije��$��0��$
��Left=�$	��Top=�$	 	15,10,0,0q�._��  �� $��15 ��� $��5 ��� $
��Auto �.+��� $��0��$��Center=�._��� $
 	slikaLogo��$��15��$��15��$��Center=�$��Center=����� 8   $  PutanjaDoSlikeLogoNormalno��� $��2��$��Center=�._��� $��tbNazivTitle��$��SmartSoftware��
�������$��11(�� $) #grdSlikaINazivTitlaIPodaciOProdavcu��$��1��$��Stretch=� �� $��40 ��� $��5 ��� $
��Auto �.+�� $��0��$��Center=�$��Bottom=� �� $��25 ��� $��10 ��� $
��Auto �.+��� $��0��$��Center=�._��� $
 slikaTitlaLogo��$��25��$��25��$��Center=�$��Center=����� *31DataContext.PutanjaDoSlikePreskociIKupiHoverovano+$ PropertyChanged=��� $��2��$��Center=�._��� $��tbNazivTitla��
�������$��14(����� *Title+ �� *FindAncestor+$ WindowL�$ Title��� $��2��$��3��.+��� $��4��$��1��$��Stretch=�._�� $��Center=� �� $
��Auto ��� $��5 ��� $
��Auto �.+��� $��0��
�������$��11(�����  �� *FindAncestor+" �$/ )DataContext.TrenutniProdavac.ImeKorisnika�$ PropertyChanged=��� $��2��
�������$��11(�����  �� *FindAncestor+" �$3 -DataContext.TrenutniProdavac.PrezimeKorisnika�$ PropertyChanged=��� .R��� $��txtBlockCenaBezPopusta��$��1��$
��Wrap=�$��12(����	#FF2F5778$
 10,5q�$��Center=�$��Center=����� *
UkupnaCena+$  {0} RSD���� .R��  �� $
��Auto ��� $
��Auto �.+��� $��txtBlockCenaSaPopustom��$��0��$
��Wrap=�$��11(�
�������$��StrikeThrough{�$
 10,5q�$��Center=�$��Center=�$��Oblique$����� *
UkupnaCena+$  {0} RSD���� $��txtBlockCenaBezPopusta��$��1��$
��Wrap=�$��12(����	#FF2F5778$
 10,5q�$��Center=�$��Center=����� *UkupnaCenaSaPopustom+$  {0} RSD���� " ��.���  � �� �	#FF17405E��  � ��$ Center=���  � ��$ 11(���  � ��$ Center=���  � ��$
 25,5q��� " ��.���  � �� �	#FF17405E��  � ��$ Center=���  � ��$ 12(���  � ��$ Center=���  � ��$
 Wrap=���  �@��Padding$
 10,5q��� " �.���  � ��
 �������  � ��$ Stretch=��� " �.���  � ��
 � d ���  � ��$ Stretch=��� " �.���  � ��
 ���� ��  � ��$ Stretch=��� " �.���  � ��
 �������  � ��$ Stretch=��� " ��.���  � ��
 �������  � �� �	#FF17405E��  � �� �� " ��.R��� #��z��$��1q����	#FF17405E._��� $��Center=�$��Center=�$
��Auto�� P�  �@J�IsMouseOver . . ���  � �� �	#FF17405E��  � ��
 �������  � �� �� " ��.R��� #��z��$��1q����	#FF17405E._��� $��Center=�$��Center=�$
��Auto���� " ��.���  � �� �	#FF2F5778��  � ��
 �������  � �� �� " ��.R��� #��z��$��0q����	#FF17405E._��� $��Center=�$��Center=�$
��Auto�� P�  �@J�IsMouseOver . . ���  � ��
 �������  � �� �	#FF2F5778��  � �� �� " ��.R��� #��z��$��0q�
�������._��� $��Center=�$��Center=�$
��Auto���� " ��.���  � ��
 ���� ��  � ��
 �  ����  � �� �� " ��.R��� #��z��$��1q�
���  ��._��� $��Center=�$��Center=�$
��Auto�� P�  �@J�IsMouseOver . . ���  � ��
 �  ����  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������._��� $��Center=�$��Center=�$
��Auto���� " ��.���  � ��
 ���� ��  � ��
 � d ���  � �� �� " ��.R��� #��z��$��1q�
��� d �._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� * DataContext.PutanjaDoSlikeKupi+$ PropertyChanged=��� $��2��$ 0,0,5,0q�$��Prodaj��$��Center=�$
��Left=� P�  �@J�IsMouseOver . . ���  � ��
 � d ���  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� **(DataContext.PutanjaDoSlikeKupiHoverovano+$ PropertyChanged=��� $��2��$ 0,0,5,0q�$��Prodaj��$��Center=�$
��Left=�P�  � {� .  . ���  � ��
 �������  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$	 5q�._��� ���� *+)DataContext.PutanjaDoSlikeKupiOnemoguceno+$ PropertyChanged=��� $��2��$ 0,0,5,0q�$��Prodaj��$��Center=�$
��Left=��� " ��.���  � ��
 ���� ��  � ��
 � d ���  � �� �� " ��.R��� #��z��$��1q�
��� d �$		 5,0q�._�� $��Center=� �� $��* ��� $��25 ��� $��5 ��� $
��Auto ��� $��* �.+��� $��1��._��� ���� *)'DataContext.PutanjaDoSlikePreskociIKupi+$ PropertyChanged=��� $��3��$��Preskoči i prodaj��$��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � ��
 � d ���  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������._�� $��Center=� �� $��* ��� $��25 ��� $��5 ��� $
��Auto ��� $��* �.+��� $��1��._��� ���� *31DataContext.PutanjaDoSlikePreskociIKupiHoverovano+$ PropertyChanged=��� $��3��$��Preskoči i prodaj��$��Center=�$
��Left=��� " ��.���  � ��
 ���� ��  � ��$ 30����  � ��
 �������  � ��$
 Auto����  � ��$ 12(���  � ��$ 0q���  � �� �� " ��.R��� #��z��$��0q�$
	 10,0q�._��� $��Center=�$��Center=�$
��Auto�� P�  �@J�IsMouseOver . . ���  � ��
 �������  � �� �	#FF18486A�� " ��.���  � �� �#128689��  � ��$ 30����  � ��
 �������  � ��$	 135����  � ��$ 12(���  � ��$ 0q���  � �� �� " ��.R��� #��z��$��0q�$
	 10,0q�._��� $��Center=�$��Center=�$
��Auto�� P�  �@J�IsMouseOver . . ���  � �� �#128689��  � ��
 �����'  !System.Windows.Shell.WindowChrome   WindowChrome      CaptionHeight$ 0Q�   GlassFrameThickness$ 0,0,0,1q�   ResizeBorderThickness$ 1q�.��]� .+�� -   $��grdCeoSadrzaj�� �� $
��Auto ��� $��* �.+��� -   $��titleBar��$��0��$	��Top=�$��75��
���   ���
� $ 2Q�.��� $ 	#FF2F5778��$ 1Q�� $ 	#FF17405E���  �� $
��Auto ��� $
��Auto �.+��� -     ��Name$  ctlTitleBar��$��0��$	��Top=�#���   $��Stretch=�$��Center=�$��Stretch=�$��Center=�$��75����.  � -   $��grdDugmiciZaProzor��$��3��$��Right=�$	��Top=� �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -   ! ��Name$! dugmeMinimize��$��0��  dugmeMinimize#��[ ��.  �� -   $! dugmeMaximize��$��1��  dugmeMaximize#��[ ��.  �� -   $! dugmeZatvori��$��2��  
dugmeClose#��[ ��.  �� $��1��$��0,5,0,0q�
���������
� " 
� GradientOrigin$" 0.5,0.5&�# 
� Center$# 0.5,0.5&�$ 2Q�$ 
� RadiusX$	$ 0.7Q�.��� $ 	#FF270000��$ 0.945Q�� $ White��$ 0.207Q�� $ 	#FFEBF0F4��$ 0.425Q�� $ 	#FFE9EBEE��$ 0.702Q�% J� Effect% 3  -System.Windows.Media.Effects.DropShadowEffect  &   Color$& Black��'   	Direction$' 90Q�(   
BlurRadius$( 5Q�)   ShadowDepth$) 2Q�*   RenderingBias$* Quality=�._��  �� $��7* ��� $��5 ��� $	��100 �.+�� $��0��$��Center=�$	��700�� �� $
��Auto ��� $��50 ��� $
��Auto � �� $��* �.+��� -	   $��borderNemaPodataka��$��0��$y�Visible=�$	��500��$��Center=�._��  �� $
��Auto ��� $
��Auto � �� $��* ��� $	��200 ��� $��* �.+��� $��0��$��1��$��1q����	#FF2F5778$��30��$��Stretch=����	#FF2F5778._��� $#��Niste odabrali nijednog kupca��$��Italic$�$��12(�
�������$��Center=��� -
   $��btnUcitajPodatkeOKupcu��$��1��$��1��$��Učitaj podatke o kupcu��$��30��#��[	 + � Cursor$
+ Hand���� -   $��borderPodaciOKupcu��$��0��$y�Hidden=����	#FF2F5778$��1q�$��Center=�$	��700��._�� $��Center=�$	��700�� �� $��70 ��� $
��Auto ��� $��25 ��� $
��Auto ��� $
��Auto �.+�� -   $��grdPrivremeniGrid��$��4��$��70��� $��0�� �� $��30 ��� $��40 �.+�� $��0�����	#FF2F5778$��Stretch=� �� $��7* ��� $��5 ��� $��3* �.+��� $��0��$��3��$��PODACI O KUPCU��$
��15,0q�$��Center=�$��Center=�
�������$��13(��� -   $��btnUcitajPodatkeOKupcu2��$��2��$
��15,0q�$��Učitaj podatke o kupcu��$��30��#��[
 $
+ Hand��� $��1��$��3��$��Center=�$	��650�� �� $
��Auto ��� $��25 ��� $
��Auto �.+�� $��0��$��Center=� �� $	��125 ��� $��10 ��� $	��125 ��� $��10 ��� $	��200 ��� $��10 ��� $	��150 �.+��� $��0��$��1q�
�������._��  �� $��35 ��� $��* �.+�� $��0��#��[ .+��� $	��Ime��$��1��$��0��#��[ � $��1��$��Stretch=�$��Center=�.+��� $��12(����� *TmpKorisnik.ImeKorisnika+�� $��1��$��2��$��1q�
�������._��  �� $��35 ��� $��* �.+�� $��0��#��[ .+��� $��Prezime��$��1��$��0��#��[ � $��1��$��Stretch=�$��Center=�.+��� $
��Wrap=�$��12(�$
 10,5q�$��Center=�$��Center=����� *TmpKorisnik.PrezimeKorisnika+�� $��1��$��4��$��1q�
�������._��  �� $��35 ��� $��* �.+�� $��0��#��[ .+��� $��e-mail��$��1��$��0��#��[ � $��1��$��Stretch=�$��Center=�.+��� $
��Wrap=�$��12(�$
 10,5q�$��Center=�$��Center=����� *TmpKorisnik.MejlKorisnika+�� $��1��$��6��$��1q�
�������._��  �� $��35 ��� $��* �.+�� $��0��#��[ .+��� $��Broj telefona��$��1��$��0��#��[ � $��1��$��Stretch=�$��Center=�.+��� $
��Wrap=�$��12(�$
 10,5q�$��Center=�$��Center=����� *#!TmpKorisnik.BrojTelefonaKorisnika+� $��2�� �� $��* ��� $
��Auto ��� $��* �.+��� $��1��$��1q�
�������._��  �� $��35 ��� $��* �.+�� $��0��#��[ .+��� $��Broj ostvarenih poena��$��1��$��0��#��[ � $��1��$��Stretch=�$��Center=� �� $��* ��� $��35 ��� $��* �.+��� $
��Wrap=�$��12(�$
 10,5q�$��Center=�$��Center=����� *!TmpKorisnik.BrojOstvarenihPoena+� -   $��grdLabelKoriscenjePoena��$��1��#���   �� $��* ��� $
��Auto ��� $
��Auto ��� $��* �.+��� -   $��lblIskoristiPoeneZaPopust��$��1��$#��Iskoristi ove poene za popust�����	#FF17405E#��[ $��Italic$��� -   $��PoeniKaoPopust��$��2��$
��10,0q�$��Center=�� -   $��grdKoriscenjePoena��$��2��$y�Hidden=�$��0�� �� $��* ��� $
��Auto ��� $
��Auto ��� $��* �.+��� -   $��	skrivenId��$y�Hidden=����� *TmpKorisnik.IdKorisnici+�� $��1��$.��(Broj poena koje je moguće iskoristiti :��$��Italic$�%  Xceed.Wpf.Toolkit.ButtonSpinner  -   $��btnVrednostPoena��$��15,0,5,0q�$
��15,0q�$y�Visible=�$��2��$��Center=�$
��Auto��  Xceed.Wpf.Toolkit.Spinner,   ValidSpinDirection$, Decrease=�$��Stretch=�$��0q�-   Content.- �� -   $!��tblIzabranBrojPoenaZaPopust��$��Stretch=����� *&$TmpKorisnik.IzabranBrojPoenaZaPopust+$ PropertyChanged=�� $��2�� �� $��* ��� $
��Auto ��� $��* �.+��� -   $��borderBezPopusta��$��1��$��1q�
�������._��  �� $
��Auto � �� $��35 ��� $��* �.+�� -   $��grdCenaBezPopustaLabel��$��0��#��[ ���	#06000000.+��� -   $��lblCenaBezPopusta��$��Ukupna cena��$
��70,0q����	#FF2F5778$��1��$��0��#��[ � $��1��$��Stretch=�$��Center=�.+��� -   $��ccontrolUkupnaCena��#��[ �� $��2��$��0��$��Stretch=�$��Center=����	#06000000
�������$��0,5,0,0q�$	��100��._�� $	��600�� �� $��* ��� $��* ��� $��* ��� $
��Auto �.+��� -   $! btnKupi��{�.  $��0��$	��125��$��30��#��[ $
+ Hand���� -   $��btnPreskoci��$��1��$��30��$	��135��#��[ $
+ Hand���� -   $��	btnOtkazi��$��Otkaži��$��30��$��2��$��Center=�$	��125��#��[ $
+ Hand��
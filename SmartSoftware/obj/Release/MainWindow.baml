   M S B A M L   `   `   `  ����   SmartSoftware    SmartSoftware.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � 
ResizeMode$  NoResize=� � Title$ SmartSoftware��$	��375��$	��525�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$ SingleBorderWindow=� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$ Black��   	Direction$	 270Q�   
BlurRadius$ 15Q�   ShadowDepth$ 5Q�	   RenderingBias$	 Quality=�
 � 	Resources
    ctpAnimacijaPocetak 
� GradientOrigin 
� Center 
� RadiusY 
� RadiusX � Color � Offset � ColumnDefinitions �Name J� Opacity ��Name  sveSlike �� Source �� Path �� RelativeSource �� AncestorType �� FallbackValue  ctpAnimacijaKraj  dugmeUlogujSe �� 
TargetType �� Property �� Value �� 
TargetType �� Padding �� UpdateSourceTrigger  �� Padding! �� Triggers" P� Property# P� Value��%o	  &	        7  &	 �    7  &	 �    7  7  7  �� .R��� $��Stretch=���
� $ 0.5,0.5&�$ 0.5,0.5&�$ 2Q�$	 0.7Q�.��� $ 	#FF270000��$ 0.945Q�� $ White��$ 0.207Q�� $ 	#FFEBF0F4��$ 0.425Q�� $ 	#FFE9EBEE��$ 0.702Q�._�� $��Stretch=� �� $��* ��� $
��Auto ��� $��* �.+�� $ grdSlikaINazivTitla��$��1��$ 0Q� �� $��25 ��� $��10 ��� $
��Auto �.+��� $��0��$��Center=�._��� $ 	slikaLogo��$��25��$��25��$��Center=�$��Center=����� 8   $ PutanjaDoSlikeLogo��� $��2��$��Center=�._��� $��tbNazivTitle�����	#FF2F5778$��14(����� *Title+ �� *FindAncestor+$ WindowL�$ Title���� .R��� $��Stretch=���
� $ 2Q�.��� $ 	#FF2F5778��$ 1Q�� $ 	#FF17405E��._�� $��Stretch=� �� $��* ��� $
��Auto ��� $��* �.+�� $��1�� �� $��25 ��� $��10 ��� $
��Auto �.+��� $��0��$��Center=�._��� $ 	slikaLogo��$��25��$��25��$��Center=�$��Center=����� 8   $  PutanjaDoSlikeLogoNormalno��� $��2��$��Center=�._��� $��tbNazivTitle��
�������$��14(����� *Title+ �� *FindAncestor+$ WindowL�$ Title���� " ��.���  � ��
 �������  � �� �	#FF17405E��  � ��$	 100����  � ��$ 30����  � ��$ Center=���  � ��$ 0q���  � �� �� " ��.R��� #��z��$��1q����	#FF17405E._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$ 5q�._��� ���� 8   $ PutanjaDoSlikeLogin�$ PropertyChanged=��� $��2��$  0,0,5,0q�$��
Prijavi se��$��Center=�$
��Left=�! P� " �@J�IsMouseOver# . . ���  � �� �	#FF17405E��  � ��
 �������  � �� �� " ��.R��� #��z��$��1q����	#FF17405E._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$ 5q�._��� ���� 8  $# PutanjaDoSlikeLoginHoverovano��� $��2��$  0,0,5,0q�$��
Prijavi se��$��Center=�$
��Left=�P� " � {�# .  . ���  � ��
 ���� ��  � ��
 �������  � �� �� " ��.R��� #��z��$��1q�
�������._�� $��Center=� �� $��25 ��� $��5 ��� $
��Auto �.+��� $��0��$ 5q�._��� ���� 8  $$ PutanjaDoSlikeLoginOnemoguceno��� $��2��$  0,0,5,0q�$��
Prijavi se��$��Center=�$
��Left=�'  !System.Windows.Shell.WindowChrome$   WindowChrome$   %   CaptionHeight$% 0Q�&   GlassFrameThickness$& 0,0,0,1q�'   ResizeBorderThickness$' 0q�.��]� ��. .+�� ( � RowDefinitions( �� $
��Auto ��� $
��Auto �.+��� -   ) ��Name$) ctlTitleBar��$��0��$	��Top=�#���   $��Stretch=�$��Center=�$��Stretch=�$��Center=�$	��450����.  � -   $��grdDugmiciZaProzor��$��3��$��Right=�$	��Top=�$ 0Q�$y�Hidden=�$��
0,-100,0,0q� �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -   * ��Name$* btnClose��$��2��  
dugmeClose#��[ ��.  �� -   $* btnMinimize��$��1��  dugmeMinimize#��[ ��.  � -   $ 
brdSadrzaj��$��1��$	��300��$	��525��$y�Visible=�$��0,0,0,0q�$��Center=�( �� $��* � �� $��* �.+��� -   + ��Name$+ 	rctShadow��$��0��$��0��$��Stretch=�$��Stretch=�$y�Hidden=�
���   �$ 0Q�, K� ZIndex$, -1��   $ 10Q�$ 90Q�$	 Quality=�$ 2Q�� $��0��$��0��$��Stretch=�$��Stretch=�$, 1����
� $ 0.5,0.5&�$ 0.5,0.5&�$ 2Q�$	 0.7Q�.��� $ 	#FF270000��$ 0.945Q�� $ White��$ 0.207Q�� $ 	#FFEBF0F4��$ 0.425Q�� $ 	#FFE9EBEE��$ 0.702Q�.+�� $��Center=�$��Center=�.+�� -   $ 
grdSadrzaj��$��Center=�$��Center=�$��0,0,0,0q�( �� $
��Auto ��� $��50 ��� $
��Auto �.+��� $��0��
����  �$��0q�$��Center=�$	��300��$��Center=�._�� $��Center=�( �� $
��Auto ��� $��0 ��� $
��Auto �
 ��%�  '��       'G� �     �� " ��.���  � ��
 ���� ��  � �� �	#FF17405E��  � ��$	 300����  � ��$ 30����  � ��$ Center=���  � ��$ Center=���  � ��$ Stretch=���  � ��$ 5,0,0,0q���  � ��$ 0,0,0,1q��� " G�.���  � ��
 ���� ��  � �� �	#FF17405E��  � ��$	 300����  � ��$ 30����  � ��$ 5,0,0,0q���  � ��$ Center=���  � ��$ Stretch=���  � ��$ Center=���  � ��$ 0,0,0,1q�.+��� -	   $��brdKorisnickoIme��$��0��$��0��$��0q�
���  ��$��Center=�._�� ( �� $
��Auto ��� $
��Auto �.+��� -
   $��brdTextBoxIme��$��Stretch=�._��� -   - ��Name$- textBoxKorisnickoIme��$��0�����	#FF2F5778$��0��. � ToolTip$*. $Morate uneti minimum jednan karakter���� -   / ��Name$/ tblckKorisnickoIme��$��0��0 J� IsHitTestVisible0 .  $��Korisničko ime��$��Center=�$
��Left=�$��10,0,0,0q����	#7F17405E$��Italic$��� $��2��$��0��$��Stretch=�$��Stretch=�
�������$, -1��$	��298��   $ 2Q�$ 3Q�$	 270Q�$
 Gray��$	 Quality=��� -   $��
brdLozinka��$��2��$��0q�
���  ��$��Center=�$��0,5,0,0q�._�� ( �� $
��Auto ��� $
��Auto �.+��� -   $��brdTextBoxBrTelefona��._�G� -   1 G�Name$1 passwordBoxLozinka�����	#FF2F5778$��1��$*. $Morate uneti minimum jednan karakter���� -   $/ tblckLozinka��0 .  $��Lozinka��$��Center=�$
��Left=�$��10,0,0,0q����	#7F17405E$��Italic$�$y�Visible=��� $��2��$��0��$��Stretch=�$��Stretch=�
�������$, -1��$	��298��   $ 2Q�$ 3Q�$	 270Q�$
 Gray��$	 Quality=��� -   2 ��Name$2 brdGreskaLogin��$��1��$	��Top=�$
��Left=�$y�Hidden=�$ 5,5,5,0q�$	��290��._��  �� $
��Auto ��� $��10 ��� $
��Auto �.+��� $��0��$��30��$��30��$��Center=�$��Center=����� # [ $ PutanjaDoSlikeAlert��� $��2��
���  ��$J��DUneti podaci za polja 'Korisnicko ime' i/ili 'Lozinku' nisu ispravni��$
��Wrap=�$	��230���� -   $* btnUlogujSe��$��2��$��0��$��2��$��	Uloguj se��#��[ 3 �� 	IsDefault3 . $��2��{��� *LogovanjeOmoguceno+� -   $ grdCestitke��$��Center=�$��Center=�$��	525,0,0,0q�.+��� ���	#FF2F5778$��12(�$��Italic$����� $ TekstZaDobrodoslicu�$ PropertyChanged=�
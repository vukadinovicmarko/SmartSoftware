   M S B A M L   `   `   `  ����   SmartSoftware,    &SmartSoftware.TrenutnaListaRezervacija   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4:
wpfToolkit)http://schemas.xceed.com/wpf/xaml/toolkit    � Title$  RezervacijeProzor��$	��300��$	��700��.���  � RowDefinitions �� $��5* ��� $��* � � ColumnDefinitions �� $��* �.+��� -   $
��pera��$ �� IsSynchronizedWithCurrentItem$
 TrueY�$��Stretch=�$��Center=�$��0��$	��Top=�$��0��$��Center=����� *Rezervacije+���� .R��� $��element��
�������$
��Auto��$��Center=�.+�� $��10,10,10,10q� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � �� $	��120 ��� $��25 ��� $	��300 ��� $
��Auto ��� $��40 ��� $��25 ��� $	��150 ��� $��25 ��� $	��100 �.+��� $��0��$��0��$��75��$��75������ *Slika+�� $��0��$��2��$��12(�$��Center=�$��Center=����� *Name+�� $��	skrivenId��$
��Wrap=�$��10(�$��0��$��3��$y�Hidden=�$��Center=�$��Center=����� *
IdOprema+�� $
��Wrap=�$��12(�$��0��$��3��$��Cena : ��$��Right=�$��Center=��� $
��Wrap=�$��0��$��4��$��12(�$
��Left=�$��Center=����� *Cena+��  �� Orientation$ 
Horizontal=�$��0��$��5��$��Stretch=�$��Center=�.+��� #��["�� $��6��$��0�� �� $
��Auto ��� $��20 ��� $
��Auto � �� $��* ��� $��* �.+��� $��btnObrisiIzKorpe��$��30��$��Obriši iz korpe��$��0��$��0��$��2��
���  ��
��������� $
��Wrap=�$��12(�$��2��$��0��$��Količina :��$��Center=�$��Center=�%  Xceed.Wpf.Toolkit.ButtonSpinner  $��kolicina��$��2��$��1��$	��150��   Content. �� $��Right=����� *TmpIzabranaKolicina+�� 
��� � �
�������$��0��$��8��$��Center=�����  �� Path$ SumCena� �� StringFormat$ ukupna cena: {0}��� $��1��$��0��$��2��� -   $��gridDugmici��$��1��
������� �� $��4* ��� $��* � �� $��* �.+�� $��0��$��Center=�$��Center=� �� $
��Auto ��� $��50 ��� $
��Auto �.+��� -   $��txbUkupnaCena��$��0��$��Center=�$��Center=����� $ UkupnaCenaUKorpi�$   Ukupna cena u korpi : {0}���� -   $��btnRezervisi��$��2��
��� � �
�������$	��100��$��30��$��0��$��
Rezerviši��� $��1��$��Center=�$��Center=�.+��� -   $��btnObrisiCeluKorpu��
���  ��
�������$	��120��$��30��$��0��$��Obriši celu korpu��
using SmartSoftware.Model;
using SmartSoftware.SmartSoftwareServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartSoftware.ViewModel
{
    public class SmartSoftwareDocumentVM : IViewModel<SmartSoftwareDocument>
    {

        private SmartSoftwareDocument smartSoftwareDocument;

        public SmartSoftwareDocument Model
        {
            get { return smartSoftwareDocument; }
        }

        

        private SmartSoftwareGlavnaOblastVM currentSmartSoftwareGlavnaOblast;

        public SmartSoftwareGlavnaOblastVM CurrentSmartSoftwareGlavnaOblastVM
        {
            get { return currentSmartSoftwareGlavnaOblast; }
            set { SetAndNotify(ref currentSmartSoftwareGlavnaOblast, value); }
        }

        private OblastiOpremeVM rootVM;

        public OblastiOpremeVM RootVM
        {
            get { return rootVM; }
        }


        public SmartSoftwareDocumentVM(SmartSoftwareDocument document)
        {
            this.refresujOblastiITipove(document);
        }



        private void refresujOblastiITipove(SmartSoftwareDocument document)
        {
            this.smartSoftwareDocument = document;
            this.rootVM = new OblastiOpremeVM(document.Root);
            this.rootVM = ViewModelUtility.viewModelFactory(document.Root) as OblastiOpremeVM;



            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOblastiOpreme[] nizOblasti = service.OblastiOpreme();
            SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovi = service.TipOpreme();

            if (nizOblasti != null && nizOblasti.Length > 0)
            {

                for (int i = 0; i < nizOblasti.Length; i++)
                {


                    OblastiOpreme o = new OblastiOpreme(this.smartSoftwareDocument.Root) { Name = nizOblasti[i].naziv_oblasti_opreme.ToUpper(), IdOblastiOpreme = nizOblasti[i].id_oblasti_opreme, Picture = nizOblasti[i].picture };
                    if (!File.Exists(nizOblasti[i].picture))
                    {
                        o.Picture = App.PutanjaDoSlikeNoImage;
                    }


                    if (nizTipovi != null && nizTipovi.Length > 0)
                    {

                        for (int j = 0; j < nizTipovi.Length; j++)
                        {
                            if (nizTipovi[j].id_oblasti_opreme == nizOblasti[i].id_oblasti_opreme)
                            {
                                string ime = nizTipovi[j].naziv_tipa;
                                //string niz[] = ime.Split;
                                TipoviOpreme t = new TipoviOpreme(o)
                                {

                                    Name = nizTipovi[j].naziv_tipa,
                                    Picture = nizTipovi[j].slika_tipa,
                                    IdTipOpreme = nizTipovi[j].id_tip_opreme,
                                    IdOblastiOpreme = nizTipovi[j].id_oblasti_opreme
                                };

                                if (!File.Exists(nizTipovi[j].slika_tipa))
                                {
                                    t.Picture = App.PutanjaDoSlikeNoImage;
                                }

                                o.Items.Add(t);
                            }
                        }
                    }
                    this.smartSoftwareDocument.Root.Items.Add(o);


                }
            }
        }

            #region PropertyChangedImpl
        protected void SetAndNotify<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                NotifyPropertyChanged(propertyName);
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        }


        
    
}

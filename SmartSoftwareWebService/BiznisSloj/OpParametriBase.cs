using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SmartSoftwareWebService.BiznisSloj
{
    public abstract class OpParametriBase : Operation
    {
        public DbItemParametri DataSelect { get; set; }

        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {

            DbItemParametri[] niz =
            (
                from parametar in entities.parametris
                join parametarOprema in entities.parametarOpremas
                on parametar.id_parametri equals parametarOprema.id_parametri
                select new DbItemParametri()
                {
                    default_vrednost = parametar.default_vrednost,
                    id_parametri = parametar.id_parametri,
                    id_tip_opreme = parametar.id_tip_opreme,
                    naziv_parametra = parametar.naziv_parametra,
                    vrednost_parametra = parametarOprema.vrednost_parametra,
                    za_filter = parametar.za_filter
                }
            ).ToArray();

            foreach (var item in niz)
            {
                item.ListaVrednostiZaFiltere = (entities.VrednostiParametra(item.id_parametri)).ToList();
            }


            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;
        }


    }

    public class OpParametriSelect : OpParametriBase
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            if (this.DataSelect == null)
            {
                return base.execute(entities);
            }
            else
            {
                DbItemParametri[] niz =
                           (
                               from parametar in entities.parametris
                               where parametar.id_tip_opreme == this.DataSelect.id_tip_opreme && parametar.za_filter == true
                               select new DbItemParametri()
                               {
                                   default_vrednost = parametar.default_vrednost,
                                   id_parametri = parametar.id_parametri,
                                   id_tip_opreme = parametar.id_tip_opreme,
                                   naziv_parametra = parametar.naziv_parametra,
                                   za_filter = parametar.za_filter
                               }
                           ).ToArray();

                foreach (var item in niz)
                {
                    item.ListaVrednostiZaFiltere = (entities.VrednostiParametra(item.id_parametri)).ToList();
                }

                OperationObject opObj = new OperationObject();
                opObj.Niz = niz;
                opObj.Success = true;
                return opObj;
            }
        }

    }

    public abstract class OpParametriGlavniProzorBase : Operation
    {
        public DbItemParametri DataSelect { get; set; }

        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {

            DbItemParametri[] niz =
            (
                from parametar in entities.parametris
                join parametarOprema in entities.parametarOpremas
                on parametar.id_parametri equals parametarOprema.id_parametri 
                where parametarOprema.deletedField == false
                select new DbItemParametri()
                {
                    default_vrednost = parametar.default_vrednost,
                    id_parametri = parametar.id_parametri,
                    id_tip_opreme = parametar.id_tip_opreme,
                    naziv_parametra = parametar.naziv_parametra,
                    vrednost_parametra = parametarOprema.vrednost_parametra,
                    za_filter = parametar.za_filter
                }
            ).ToArray();

            foreach (var item in niz)
            {
                item.ListaVrednostiZaFiltere = (entities.VrednostiParametra(item.id_parametri)).ToList();
            }


            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;
        }


    }

    public class OpParametriGlavniProzorSelect : OpParametriGlavniProzorBase
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            if (this.DataSelect == null)
            {
                return base.execute(entities);
            }
            else
            {
                DbItemParametri[] niz =
                           (
                               from parametar in entities.parametris
                               where parametar.id_tip_opreme == this.DataSelect.id_tip_opreme && parametar.za_filter == true
                               select new DbItemParametri()
                               {
                                   default_vrednost = parametar.default_vrednost,
                                   id_parametri = parametar.id_parametri,
                                   id_tip_opreme = parametar.id_tip_opreme,
                                   naziv_parametra = parametar.naziv_parametra,
                                   za_filter = parametar.za_filter
                               }
                           ).ToArray();

                foreach (var item in niz)
                {
                    item.ListaVrednostiZaFiltere = (entities.VrednostiParametraGlavniProzor(item.id_parametri)).ToList();
                }

                OperationObject opObj = new OperationObject();
                opObj.Niz = niz;
                opObj.Success = true;
                return opObj;
            }
        }

    }

    public class OpParametriInsert : OpSelectAdminPanelParametriSelect
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            if(this.DataSelect != null)
            {
                entities.ParametriInsert(this.DataSelect.naziv_parametra, this.DataSelect.default_vrednost, this.DataSelect.id_tip_opreme, this.DataSelect.za_filter, this.DataSelect.tipParametra);
            }
            return base.execute(entities);
        }
    }

    public class OpParametriUpdate : OpSelectAdminPanelParametriSelect
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            if (this.DataSelect != null)
            {
                entities.ParametriUpdate(this.DataSelect.id_parametri, this.DataSelect.naziv_parametra, this.DataSelect.default_vrednost, this.DataSelect.id_tip_opreme, this.DataSelect.za_filter, this.DataSelect.tipParametra);
            }
            return base.execute(entities);
        }
    }

    public class OpParametriDelete : OpSelectAdminPanelParametriSelect
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            if (this.DataSelect != null)
            {
                entities.ParametriDelete(this.DataSelect.id_parametri);
            }
            return base.execute(entities);
        }
    }

    public class OpParametriRestore : OpSelectAdminPanelParametriSelect
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            if (this.DataSelect != null)
            {
                entities.RestoreIzbrisanParametar(this.DataSelect.id_parametri);
            }
            return base.execute(entities);
        }
    }

    

   

    public class OpParametriSelectWhereIdTipOpreme : OpParametriBase
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            DbItemParametri[] nizSvihParametara =
                (
                    from parametar in entities.parametris
                    where parametar.id_tip_opreme == DataSelect.id_tip_opreme
                    select new DbItemParametri()
                    {
                        default_vrednost = parametar.default_vrednost,
                        id_parametri = parametar.id_parametri,
                        id_tip_opreme = parametar.id_tip_opreme,
                        naziv_parametra = parametar.naziv_parametra,
                        tipParametra = parametar.tipParametra
                    }
                 ).ToArray();
            OperationObject opObj = new OperationObject();
            opObj.Niz = nizSvihParametara;
            opObj.Success = true;
            return opObj;
            
        }
    }



    public class OpSelectAdminPanelParametriSelect : OpParametriBase
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            DbItemParametri[] nizSvihParametara =
                (
                    from parametar in entities.parametris
                    select new DbItemParametri()
                    {
                        default_vrednost = parametar.default_vrednost,
                        id_parametri = parametar.id_parametri,
                        id_tip_opreme = parametar.id_tip_opreme,
                        naziv_parametra = parametar.naziv_parametra,
                        tipParametra = parametar.tipParametra,
                        za_filter = parametar.za_filter,
                        deletedField = parametar.deletedField
                    }
                 ).ToArray();
            OperationObject opObj = new OperationObject();
            opObj.Niz = nizSvihParametara;
            opObj.Success = true;
            return opObj;
        }
    }


    public class OpParametriPretraga : OpSelectAdminPanelParametriSelect
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
           if(this.DataSelect.zaPretragu != null || this.DataSelect.zaPretragu != "")
           {
               DbItemParametri[] nizSvihParametara =
               (
                   from parametar in entities.parametris
                   where parametar.naziv_parametra.Contains(this.DataSelect.zaPretragu)
                   select new DbItemParametri()
                   {
                       default_vrednost = parametar.default_vrednost,
                       id_parametri = parametar.id_parametri,
                       id_tip_opreme = parametar.id_tip_opreme,
                       naziv_parametra = parametar.naziv_parametra,
                       tipParametra = parametar.tipParametra,
                       za_filter = parametar.za_filter
                   }
                ).ToArray();
               OperationObject opObj = new OperationObject();
               opObj.Niz = nizSvihParametara;
               opObj.Success = true;
               return opObj;
           }
           else
           {
               return base.execute(entities);
           }
        }
    }

}
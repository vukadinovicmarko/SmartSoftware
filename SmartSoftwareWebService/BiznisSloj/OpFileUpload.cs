using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SmartSoftwareWebService.BiznisSloj
{
    public class OpFileUpload : Operation
    {
        public DbItemOpremaSaParametrima DataSelect { get; set; }

        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            FileUpload f = new FileUpload();
            f.SaveAs("pera");

            return null;

        }


    }
}
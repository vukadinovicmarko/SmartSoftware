using SmartSoftwareWebService.DataSloj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSoftwareWebService.BiznisSloj
{
    public abstract class Operation
    {
        public abstract OperationObject execute(SmartSoftwareBazaEntities entities);
    }
}
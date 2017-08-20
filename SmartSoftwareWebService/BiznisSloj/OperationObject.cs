using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SmartSoftwareWebService.BiznisSloj
{
    [DataContract]
    public class OperationObject
    {
        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        [DataMember]
        public Object[] Niz { get; set; }
    }
}
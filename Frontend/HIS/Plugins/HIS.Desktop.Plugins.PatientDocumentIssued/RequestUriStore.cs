using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PatientDocumentIssued
{
    class RequestUriStore
    {
        //internal const string EMR_DOCUMENT_TYPE_CREATE = "api/EmrDocumentType/Create";
        //internal const string EMR_DOCUMENT_TYPE_DELETE = "api/EmrDocumentType/Delete";
        //internal const string EMR_DOCUMENT_TYPE_UPDATE = "api/EmrDocumentType/Update";
        //internal const string EMR_DOCUMENT_TYPE_CHANGE_LOCK = "api/EmrDocumentType/ChangeLock";
        internal const string EMR_DOCUMENT_TYPE_GET = "api/EmrDocumentType/Get";
        internal const string V_EMR_DOCUMENT_GET = "api/EmrDocument/GetView";
        
        internal const string EMR_DOCUMENT_ISSUED_UPDATE = "api/EmrDocument/Issued";
        internal const string EMR_DOCUMENT_DISISSUED_UPDATE = "api/EmrDocument/Disissued";
    }
}

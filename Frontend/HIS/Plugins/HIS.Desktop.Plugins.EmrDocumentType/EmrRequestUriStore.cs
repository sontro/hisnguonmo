using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.EmrDocumentType
{
    class EmrRequestUriStore
    {
        internal const string EMR_DOCUMENT_TYPE_CREATE = "api/EmrDocumentType/Create";
        internal const string EMR_DOCUMENT_TYPE_DELETE = "api/EmrDocumentType/Delete";
        internal const string EMR_DOCUMENT_TYPE_UPDATE = "api/EmrDocumentType/Update";
        internal const string EMR_DOCUMENT_TYPE_GET = "api/EmrDocumentType/Get";
        internal const string EMR_DOCUMENT_TYPE_CHANGE_LOCK = "api/EmrDocumentType/ChangeLock";
    }
}

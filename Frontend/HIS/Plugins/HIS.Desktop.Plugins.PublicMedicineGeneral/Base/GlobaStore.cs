using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PublicMedicineGeneral.Base
{
    public class GlobaStore
    {
        internal const string HisPatientGetview = "api/HisPatient/GetView";
        internal const string HisPrescriptionGetview1 = "api/HisPrescription/GetView1";
        internal const string HisTreatmentGetView = "api/HisTreatment/GetView";
        internal const string HisTreatmentBedRoomGetview = "api/HisTreatmentBedRoom/GetView";
        internal const string HisAggrExpMestGetview = "api/HisAggrExpMest/GetView";
        internal const string HisExpMestGet = "api/HisExpMest/Get";
        internal const string HisBedRoomGetView = "api/HisBedRoom/GetView";
        internal const string HisSereServGetView = "api/HisSereServ/GetView";

        internal const string HisMedicineGet = "api/HisMedicine/Get";
        internal const string HisMaterialGet = "api/HisMaterial/Get";
        internal const int MAX_REQUEST_LENGTH_PARAM = 100;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ApproveAggrExpMest.Base
{
    public static class RequestUri
    {
        public const string HIS_AGGR_IMP_MEST_GETVIEW = "api/HisAggrImpMest/GetView";
        public const string HIS_IMP_MEST_GETVIEW_WITHOUT_DATA_DOMAIN = "api/HisImpMest/GetViewWithoutDataDomainFilter";
        public const string HIS_EXP_MEST_MEDICINE_GETVIEW = "api/HisExpMestMedicine/GetView";
        public const string HIS_EXP_MEST_MATERIAL_GETVIEW = "api/HisExpMestMaterial/GetView";
        public const string HIS_EXP_MEST_GETVIEW = "api/HisExpMest/GetView";
        public const string HIS_IMP_MEST_REMOVEAGGR = "api/HisImpmest/RemoveAggr";
        public const string HIS_PATIENT_GETVIEW = "api/HisPatient/GetView";
        public const string HIS_MEDICINE_USE_FORM_GET = "api/HisMedicineUseForm/Get";
        public const string HIS_IMP_MEST_MEDICINE_GETVIEW_BY_AGGR_IMPMEST_ID_GROUPBY_MEDICINE = "api/HisImpMestMedicine/GetViewByAggrImpMestIdAndGroupByMedicine";
        public const string HIS_IMP_MEST_MATERIAL_GETVIEW_BY_AGGR_IMPMEST_ID_GROUPBY_MATERIAL = "api/HisImpMestMaterial/GetViewByAggrImpMestIdAndGroupByMaterial";
        public const string HIS_SERVICE_UNIT_GET = "api/HisServiceUnit/Get";
        public const string HIS_MOBA_IMP_MEST_GETVIEW = "api/HisMobaImpMest/GetView";
        public const string HIS_SERVICE_REQ_GETVIEW = "api/HisServiceReq/GetView";
        public const string HIS_MATERIAL_TYPE_GETVIEW = "api/HisMaterialType/GetView";
        public const string HIS_SERVICE_GETVIEW = "api/HisService/GetView";
        public const string HIS_PRESCRIPTION_GETVIEW = "api/HisPrescription/GetView";
        public const string HIS_TREATMENT_BED_ROOM_GETVIEW = "/api/HisTreatmentBedRoom/GetView";
    }
}

using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTrackingDataSDO
    {
        public HIS_TREATMENT Treatment {get; set;}
        public List<HIS_SERVICE_REQ> ServiceReqs {get; set;}
        public List<HIS_EXP_MEST> ExpMests {get; set;}
        public List<V_HIS_IMP_MEST_2> vImpMests2 {get; set;}
        public List<HIS_EXP_MEST_MEDICINE> ExpMestMedicines {get; set;}
        public List<HIS_EXP_MEST_MATERIAL> ExpMestMaterials {get; set;}
        public List<V_HIS_IMP_MEST_MEDICINE> vImpMestMedicines {get; set;}
        public List<V_HIS_IMP_MEST_MATERIAL> vImpMestMaterials {get; set;}
        public List<V_HIS_IMP_MEST_BLOOD> vImpMestBloods {get; set;}
        public List<HIS_SERVICE_REQ_METY> ServiceReqMetys {get; set;}
        public List<HIS_SERVICE_REQ_MATY> ServiceReqMatys {get; set;}
        public List<V_HIS_SERE_SERV_RATION> vSereServRations {get; set;}

        public List<V_HIS_EXP_MEST_BLTY_REQ_2> vExpMestBityReqs2 {get; set;}
        public List<HIS_SERE_SERV> SereServs {get; set;}
        public List<HIS_SERE_SERV_EXT> SereServExts {get; set;}
        public List<V_HIS_TREATMENT_BED_ROOM> TreatmentBedRooms {get; set;}
        public List<HIS_DHST> HisDHSTs {get; set;}
        public List<HIS_CARE> HisCares {get; set;}
        public List<V_HIS_CARE_DETAIL> CareDetails { get; set; }

    }
}

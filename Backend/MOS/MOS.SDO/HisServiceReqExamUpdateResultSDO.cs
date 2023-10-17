using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisServiceReqExamUpdateResultSDO
    {
        public HIS_SERVICE_REQ ServiceReq { get; set; }

        //Thong tin ket qua ket thuc dieu tri, trong truong hop xu ly kem ket thuc dieu tri
        public HIS_TREATMENT TreatmentFinishResult { get; set; }
        //Thong tin ket qua nhap vien
        public HisDepartmentTranHospitalizeResultSDO HospitalizeResult { get; set; }
        //Thong tin ket qua kham them
        public V_HIS_SERVICE_REQ AdditionExamResult { get; set; }

        public HIS_MEDI_RECORD MediRecord { get; set; }

        public List<HIS_SERE_SERV_DEPOSIT> SereServDeposits { get; set; }
        public V_HIS_TRANSACTION Transaction { get; set; }
    }
}

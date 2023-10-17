using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisPatientProfileSDO
    {
        //Thong tin benh nhan
        public HIS_PATIENT HisPatient { get; set; }
        //Thong tin ho so dieu tri
        public HIS_TREATMENT HisTreatment { get; set; }
        //Thong tin dien doi tuong
        public HIS_PATIENT_TYPE_ALTER HisPatientTypeAlter { get; set; }
        public long DepartmentId { get; set; }
        //Thong tin the
        public string CardCode { get; set; }
        public string CardServiceCode { get; set; }
        public string BankCardCode { get; set; }

        public long TreatmentTime { get; set; }
        public string ProvinceCode { get; set; }
        public string DistrictCode { get; set; }
        public long RequestRoomId { get; set; }
        public bool IsChronic { get; set; }
        public byte[] ImgBhytData { get; set; }
        public byte[] ImgAvatarData { get; set; }
        public byte[] ImgCmndBeforeData { get; set; }
        public byte[] ImgCmndAfterData { get; set; }
        public byte[] ImgTransferInData { get; set; }
    }
}

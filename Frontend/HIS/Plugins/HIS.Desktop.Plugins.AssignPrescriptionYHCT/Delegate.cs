using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignPrescriptionYHCT
{
    public delegate void DelegateSelectMultiDate(List<DateTime?> datas, DateTime time);
    public delegate bool ValidAddRow(object data);
    public delegate MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE GetPatientTypeBySeTy(long patientTypeId, long serviceId, long serviceTypeId);
    public delegate long? CalulateUseTimeTo();
    public delegate bool ExistsAssianInDay(long serviceId);
    public delegate void ChonThuocTrongKhoCungHoatChat(HIS.Desktop.Plugins.AssignPrescriptionYHCT.OptionChonThuocThayThe chonThuocThayThe);
    public delegate void ChonVTTrongKho(HIS.Desktop.Plugins.AssignPrescriptionYHCT.EnumOptionChonVatTuThayThe chonVTThayThe);
}

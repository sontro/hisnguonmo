using System;
using System.Collections.Generic;

namespace HIS.Desktop.Plugins.AssignPrescriptionKidney
{
    public delegate void DelegateSelectMultiDate(List<DateTime?> datas, DateTime time);
    public delegate bool ValidAddRow(object data);
    public delegate MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE GetPatientTypeBySeTy(long patientTypeId, long serviceId, long serviceTypeId);
    public delegate long? CalulateUseTimeTo();
    public delegate bool ExistsAssianInDay(long serviceId);
    public delegate void ChonThuocTrongKhoCungHoatChat(HIS.Desktop.Plugins.AssignPrescriptionKidney.OptionChonThuocThayThe chonThuocThayThe);
    public delegate void ChonVTTrongKho(HIS.Desktop.Plugins.AssignPrescriptionKidney.EnumOptionChonVatTuThayThe chonVTThayThe);
}

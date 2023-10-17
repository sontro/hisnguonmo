using HIS.Desktop.Plugins.AssignPrescriptionCLS.ADO;
using System;
using System.Collections.Generic;

namespace HIS.Desktop.Plugins.AssignPrescriptionCLS
{
    public delegate void DelegateSelectMultiDate(List<DateTime?> datas, DateTime time);
    public delegate bool ValidAddRow(object data);
    //public delegate MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE GetPatientTypeBySeTy(long patientTypeId, MediMatyTypeADO medimaty);
    public delegate long? CalulateUseTimeTo();
    public delegate bool ExistsAssianInDay(long serviceId);
    public delegate void ChonThuocTrongKhoCungHoatChat(HIS.Desktop.Plugins.AssignPrescriptionCLS.OptionChonThuocThayThe chonThuocThayThe);
    public delegate void ChonVTTrongKho(HIS.Desktop.Plugins.AssignPrescriptionCLS.EnumOptionChonVatTuThayThe chonVTThayThe);
}

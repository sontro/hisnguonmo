using HIS.Desktop.Plugins.AssignPrescriptionPK.ADO;
using System;
using System.Collections.Generic;

namespace HIS.Desktop.Plugins.AssignPrescriptionPK
{
    public delegate void DelegateSelectMultiDate(List<DateTime?> datas, DateTime time);
    public delegate bool ValidAddRow(object data);
    public delegate long? CalulateUseTimeTo();
    public delegate bool ExistsAssianInDay(MediMatyTypeADO mediMatyType);
    public delegate void ChonThuocTrongKhoCungHoatChat(HIS.Desktop.Plugins.AssignPrescriptionPK.OptionChonThuocThayThe chonThuocThayThe);
    public delegate void ChonVTTrongKho(HIS.Desktop.Plugins.AssignPrescriptionPK.EnumOptionChonVatTuThayThe chonVTThayThe);
    public delegate void LyDoKeThuocTuongTac(string InteractionReason);
    public delegate void TuongTacKhongBoSung();
    public delegate void LyDoKeThuocQuaSoLuong(string ReasonMaxPrescription);

}

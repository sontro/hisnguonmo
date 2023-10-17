using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.EventLogUtil;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.UpdateDetail
{
    class HisImpMestUpdateDetailLog
    {
        private List<string> messageLog = new List<string>();

        internal void GenerateLogMessage(HIS_MEDICINE medicine, HIS_MEDICINE_TYPE oldMedicineType, HIS_MEDICINE_TYPE newMedicineType,
            HisImpMestMedicineSDO sdo)
        {
            try
            {
                string log = new StringBuilder().Append("(")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LoaiThuoc))
                    .Append(oldMedicineType.MEDICINE_TYPE_NAME)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoLuong))
                    .Append(medicine.AMOUNT)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaNhap))
                    .Append(medicine.IMP_PRICE)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VatNhap))
                    .Append(medicine.IMP_VAT_RATIO * 100)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoLo))
                    .Append(":")
                    .Append(medicine.PACKAGE_NUMBER)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HanDung))
                    .Append(":")
                    .Append(Inventec.Common.DateTime.Convert.TimeNumberToDateString(medicine.EXPIRED_DATE ?? 0))
                    .Append(" ==> ")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LoaiThuoc))
                    .Append(newMedicineType.MEDICINE_TYPE_NAME)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoLuong))
                    .Append(sdo.Amount)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaNhap))
                    .Append(sdo.ImpPrice)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VatNhap))
                    .Append(sdo.ImpVatRatio * 100)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoLo))
                    .Append(":")
                    .Append(sdo.PackageNumber)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HanDung))
                    .Append(":")
                    .Append(Inventec.Common.DateTime.Convert.TimeNumberToDateString(sdo.ExpireDate ?? 0))
                    .Append(")").ToString();

                this.messageLog.Add(log);

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal void GenerateLogMessage(HIS_MATERIAL material, HIS_MATERIAL_TYPE oldMaterialType, HIS_MATERIAL_TYPE newMaterialType,
            HisImpMestMaterialSDO sdo)
        {
            try
            {
                string log = new StringBuilder().Append("(")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LoaiVatTu))
                    .Append(oldMaterialType.MATERIAL_TYPE_NAME)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoLuong))
                    .Append(material.AMOUNT)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaNhap))
                    .Append(material.IMP_PRICE)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VatNhap))
                    .Append(material.IMP_VAT_RATIO * 100)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoLo))
                    .Append(":")
                    .Append(material.PACKAGE_NUMBER)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HanDung))
                    .Append(":")
                    .Append(Inventec.Common.DateTime.Convert.TimeNumberToDateString(material.EXPIRED_DATE ?? 0))
                    .Append(" ==> ")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LoaiVatTu))
                    .Append(newMaterialType.MATERIAL_TYPE_NAME)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoLuong))
                    .Append(sdo.Amount)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaNhap))
                    .Append(sdo.ImpPrice)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VatNhap))
                    .Append(sdo.ImpVatRatio * 100)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoLo))
                    .Append(":")
                    .Append(sdo.PackageNumber)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HanDung))
                    .Append(":")
                    .Append(Inventec.Common.DateTime.Convert.TimeNumberToDateString(sdo.ExpireDate ?? 0))
                    .Append(")").ToString();

                this.messageLog.Add(log);

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal void GenerateLogMessage(HIS_BLOOD blood, HIS_BLOOD_TYPE oldBloodType, HIS_BLOOD_TYPE newBloodType,
            HisImpMestBloodSDO sdo)
        {
            try
            {
                string log = new StringBuilder().Append("(")
                    .Append(LogCommonUtil.GetEventLogContent(EventLog.Enum.TuiMau))
                    .Append(blood.BLOOD_CODE)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LoaiMau))
                    .Append(oldBloodType.BLOOD_TYPE_NAME)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaNhap))
                    .Append(blood.IMP_PRICE)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VatNhap))
                    .Append(blood.IMP_VAT_RATIO * 100)
                    .Append(" ==> ")
                    .Append(LogCommonUtil.GetEventLogContent(EventLog.Enum.TuiMau))
                    .Append(blood.BLOOD_CODE)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LoaiMau))
                    .Append(newBloodType.BLOOD_TYPE_NAME)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaNhap))
                    .Append(sdo.ImpPrice)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VatNhap))
                    .Append(sdo.ImpVatRatio * 100)
                    .Append(")").ToString();

                this.messageLog.Add(log);

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal void GenerateLogMessage(HIS_IMP_MEST oldImp, HIS_IMP_MEST newImp)
        {
            try
            {
                string log = new StringBuilder().Append("(")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThongTinPhieu))
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoChungTu))
                    .Append(":")
                    .Append(oldImp.DOCUMENT_NUMBER)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NgayChungTu))
                    .Append(":")
                    .Append(Inventec.Common.DateTime.Convert.TimeNumberToDateString(oldImp.DOCUMENT_DATE ?? 0))
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoTienChungTu))
                    .Append(":")
                    .Append(oldImp.DOCUMENT_PRICE)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TienChietKhau))
                    .Append(":")
                    .Append(oldImp.DISCOUNT)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TyLeChietKhau))
                    .Append(":")
                    .Append(oldImp.DISCOUNT_RATIO)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NguoiGiao))
                    .Append(":")
                    .Append(oldImp.DELIVERER)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GhiChu))
                    .Append(":")
                    .Append(oldImp.DESCRIPTION)
                    .Append(" ==> ")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoChungTu))
                    .Append(":")
                    .Append(newImp.DOCUMENT_NUMBER)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NgayChungTu))
                    .Append(":")
                    .Append(Inventec.Common.DateTime.Convert.TimeNumberToDateString(newImp.DOCUMENT_DATE ?? 0))
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoTienChungTu))
                    .Append(":")
                    .Append(newImp.DOCUMENT_PRICE)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TienChietKhau))
                    .Append(":")
                    .Append(newImp.DISCOUNT)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TyLeChietKhau))
                    .Append(":")
                    .Append(newImp.DISCOUNT_RATIO)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NguoiGiao))
                    .Append(":")
                    .Append(newImp.DELIVERER)
                    .Append(".")
                    .Append(LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GhiChu))
                    .Append(":")
                    .Append(newImp.DESCRIPTION)
                    .Append(")").ToString();

                this.messageLog.Add(log);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal void Run(HIS_IMP_MEST impMest)
        {
            try
            {
                string logData = "";
                if (this.messageLog.Count > 0)
                {
                    logData = String.Join(";", this.messageLog);
                }

                new EventLogGenerator(EventLog.Enum.HisImpMest_SuaChiTietPhieuNhap, logData)
                    .ImpMestCode(impMest.IMP_MEST_CODE)
                    .Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}

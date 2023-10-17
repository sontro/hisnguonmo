using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.InPatient
{
    class HisServiceReqInPatientPresCheck : BusinessBase
    {
        internal HisServiceReqInPatientPresCheck()
            : base()
        {
        }

        internal HisServiceReqInPatientPresCheck(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        internal bool IsValidData(InPatientPresSDO data, ref List<long> instructionTimes)
        {
            bool valid = true;
            try
            {
                if (!this.IsValidInstructionTimes(data, ref instructionTimes))
                {
                    return false;
                }
                if (data.RemedyCount.HasValue && data.PrescriptionTypeId == PrescriptionType.NEW)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DonTanDuocKhongDuocPhepNhapSoThang);
                    return false;
                }
                if (!data.RemedyCount.HasValue && data.PrescriptionTypeId == PrescriptionType.TRADITIONAL)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DonYhctBatBuocNhapSoThang);
                    return false;
                }
                if (IsNotNullOrEmpty(data.SerialNumbers) && data.InstructionTimes.Count > 1)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongChoPhepKeDonNhieuNgayTrongTruongHopKeVatTuTheoSoSeri);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsNotCabinet(List<long> mediStockIds)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(mediStockIds))
                {
                    List<string> cabinets = HisMediStockCFG.DATA
                        .Where(o => mediStockIds != null && mediStockIds.Contains(o.ID) && o.IS_CABINET == MOS.UTILITY.Constant.IS_TRUE)
                        .Select(o => o.MEDI_STOCK_NAME).ToList();
                    if (IsNotNullOrEmpty(cabinets))
                    {
                        string cabinetStr = string.Join(",", cabinets);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_CacKhoLaTuTrucKhongChoPhepKe, cabinetStr);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsNotCabinet(long mediStockId)
        {
            return IsNotCabinet(new List<long>() { mediStockId });
        }

        private bool IsValidInstructionTimes(InPatientPresSDO data, ref List<long> instructionTimes)
        {
            bool valid = true;
            try
            {
                if (data != null)
                {
                    var temp = new List<long>();
                    if (HisServiceReqCFG.MANY_DAYS_PRESCRIPTION_OPTION == HisServiceReqCFG.ManyDaysPrescriptionOption.BY_PRES)
                    {
                        if (!IsNotNullOrEmpty(data.InstructionTimes))
                        {
                            LogSystem.Warn("data.InstructionTimes null");
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                            return false;
                        }
                        temp.AddRange(data.InstructionTimes);
                    }
                    else
                    {
                        bool isInValid = (IsNotNullOrEmpty(data.Medicines) && data.Medicines.Exists(t => !IsNotNullOrEmpty(t.InstructionTimes)))
                            || (IsNotNullOrEmpty(data.Materials) && data.Materials.Exists(t => !IsNotNullOrEmpty(t.InstructionTimes)))
                            || (IsNotNullOrEmpty(data.SerialNumbers) && data.SerialNumbers.Exists(t => !IsNotNullOrEmpty(t.InstructionTimes)))
                            || (IsNotNullOrEmpty(data.ServiceReqMaties) && data.ServiceReqMaties.Exists(t => !IsNotNullOrEmpty(t.InstructionTimes)))
                            || (IsNotNullOrEmpty(data.ServiceReqMeties) && data.ServiceReqMeties.Exists(t => !IsNotNullOrEmpty(t.InstructionTimes)));
                        if (isInValid)
                        {
                            LogSystem.Warn("Ton tai instructionTimes trong cac thuoc/vat tu null");
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                            return false;
                        }

                        if (IsNotNullOrEmpty(data.Medicines))
                        {
                            foreach (PresMedicineSDO s in data.Medicines)
                            {
                                temp.AddRange(s.InstructionTimes);
                            }
                        }
                        if (IsNotNullOrEmpty(data.Materials))
                        {
                            foreach (PresMaterialSDO s in data.Materials)
                            {
                                temp.AddRange(s.InstructionTimes);
                            }
                        }
                        if (IsNotNullOrEmpty(data.SerialNumbers))
                        {
                            foreach (PresMaterialBySerialNumberSDO s in data.SerialNumbers)
                            {
                                temp.AddRange(s.InstructionTimes);
                            }
                        }
                        if (IsNotNullOrEmpty(data.ServiceReqMaties))
                        {
                            foreach (PresOutStockMatySDO s in data.ServiceReqMaties)
                            {
                                temp.AddRange(s.InstructionTimes);
                            }
                        }
                        if (IsNotNullOrEmpty(data.ServiceReqMeties))
                        {
                            foreach (PresOutStockMetySDO s in data.ServiceReqMeties)
                            {
                                temp.AddRange(s.InstructionTimes);
                            }
                        }
                    }
                    instructionTimes = temp.Distinct().ToList();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}

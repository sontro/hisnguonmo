using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.Subclinical
{
    class HisServiceReqSubclinicalPresCheck : BusinessBase
    {
        internal HisServiceReqSubclinicalPresCheck()
            : base()
        {
        }

        internal HisServiceReqSubclinicalPresCheck(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        internal bool IsValidData(SubclinicalPresSDO data)
        {
            bool valid = true;
            try
            {
                if (!IsNotNullOrEmpty(data.Materials) && !IsNotNullOrEmpty(data.Medicines))
                {
                    LogSystem.Warn("data.Materials, data.Medicines null");
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    return false;
                }

                if (data.InstructionTime <= 0)
                {
                    LogSystem.Warn("data.InstructionTime");
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    return false;
                };
                if (string.IsNullOrWhiteSpace(data.ClientSessionKey))
                {
                    LogSystem.Warn("data.ClientSessionKey");
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    return false;
                };
                if (IsNotNullOrEmpty(data.Medicines))
                {
                    long count = data.Medicines.Select(t => new
                    {
                        t.IsExpend,
                        t.IsOutParentFee,
                        t.MedicineTypeId,
                        t.MediStockId,
                        t.PatientTypeId,
                        t.SereServParentId,
                        t.MedicineId                        
                    }).Distinct().Count();

                    if (count != data.Medicines.Count)
                    {
                        LogSystem.Warn("data.Medicines chua 2 dong co thong tin IsExpend, IsOutParentFee, MedicineTypeId, MediStockId, PatientTypeId, SereServParentId, MedicineId giong nhau");
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        return false;
                    }
                }

                if (IsNotNullOrEmpty(data.Materials))
                {
                    //ko check duplicate voi stent (vi stent neu ke so luong lon hon 1 thi se tach 
                    //thanh nhieu dong co so luong ko vuot qua 1 va server tu dong set stent_order)
                    var tmp = data.Materials.Where(o => !HisMaterialTypeCFG.IsStent(o.MaterialTypeId)).ToList();
                    if (IsNotNullOrEmpty(tmp))
                    {
                        long count = tmp.Select(t => new
                        {
                            t.IsExpend,
                            t.IsOutParentFee,
                            t.MaterialTypeId,
                            t.MediStockId,
                            t.PatientTypeId,
                            t.SereServParentId
                        }).Distinct().Count();

                        if (count != tmp.Count)
                        {
                            LogSystem.Warn("data.Materials chua 2 dong co thong tin IsExpend, IsOutParentFee, MaterialTypeId, MediStockId, PatientTypeId, SereServParentId giong nhau");
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            return false;
                        }
                    }

                    if (data.Materials.Exists(t => t.MaterialBeanIds == null || t.MaterialBeanIds.Count == 0))
                    {
                        LogSystem.Warn("Ton tai MaterialBeanIds null");
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
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

        internal bool IsValidStentAmount(PrescriptionSDO data)
        {
            try
            {
                List<long> invalidStents = IsNotNullOrEmpty(data.Materials) ? data.Materials.Where(o => HisMaterialTypeCFG.IsStent(o.MaterialTypeId) && o.Amount > 1).Select(o => o.MaterialTypeId).ToList() : null;
                if (IsNotNullOrEmpty(invalidStents))
                {
                    List<string> materialTypeNames = HisMaterialTypeCFG.DATA.Where(o => invalidStents.Contains(o.ID)).Select(o => o.MATERIAL_TYPE_NAME).ToList();
                    string materialTypeNameStr = string.Join(",", materialTypeNames);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_StentVoiSoLuongLonHon1CanTachThanhNhieuDong, materialTypeNameStr);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
        }

        internal bool IsValidParentServiceReq(SubclinicalPresSDO data, ref HIS_SERVICE_REQ parentServiceReq, ref long sereServParentId)
        {
            bool valid = true;
            try
            {
                List<long> sereServParentIds = new List<long>();
                List<long> ids1 = IsNotNullOrEmpty(data.Medicines) ? data.Medicines
                    .Where(o => o.SereServParentId.HasValue)
                    .Select(o => o.SereServParentId.Value).ToList() : null;

                List<long> ids2 = IsNotNullOrEmpty(data.Materials) ? data.Materials
                    .Where(o => o.SereServParentId.HasValue)
                    .Select(o => o.SereServParentId.Value).ToList() : null;

                if (IsNotNullOrEmpty(ids1))
                {
                    sereServParentIds.AddRange(ids1);
                }
                if (IsNotNullOrEmpty(ids2))
                {
                    sereServParentIds.AddRange(ids2);
                }

                if (!IsNotNullOrEmpty(sereServParentIds))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Truyen thieu thong tin sereServParentId");
                    return false;
                }

                sereServParentIds = sereServParentIds.Distinct().ToList();

                if (sereServParentIds.Count > 1)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("ko ton tai 2 sere_serv_parent_id khac nhau");
                    return false;
                }

                sereServParentId = sereServParentIds[0];

                HIS_SERE_SERV ss = new HisSereServGet().GetById(sereServParentId);

                if (ss == null || !ss.SERVICE_REQ_ID.HasValue)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("ko ton tai HIS_SERE_SERV");
                    return false;
                }

                parentServiceReq = new HisServiceReqGet().GetById(ss.SERVICE_REQ_ID.Value);

                if (parentServiceReq == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("ko ton tai parentServiceReq");
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
    }
}

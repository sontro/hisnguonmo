using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseHandler
{
    class HisExpMestBaseHandlerCheck : BusinessBase
    {
        internal HisExpMestBaseHandlerCheck()
            : base()
        {

        }

        internal HisExpMestBaseHandlerCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool IsStockAllowHandler(HIS_EXP_MEST raw, WorkPlaceSDO workplace)
        {
            bool valid = true;
            try
            {
                if (!workplace.MediStockId.HasValue
                    || (raw.CHMS_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST.CHMS_TYPE__ID__ADDITION && workplace.MediStockId.Value != raw.MEDI_STOCK_ID)
                    || (raw.CHMS_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST.CHMS_TYPE__ID__REDUCTION && workplace.MediStockId.Value != raw.IMP_MEDI_STOCK_ID))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhongKhongCoQuyenThucHienChucNangNay, workplace.RoomName, raw.EXP_MEST_CODE);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool IsNotExistsImpMest(HIS_EXP_MEST raw)
        {
            bool valid = true;
            try
            {
                List<HIS_IMP_MEST> exists = new HisImpMestGet().GetByChmsExpMestId(raw.ID);
                if (IsNotNullOrEmpty(exists))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Da ton tai phieu nhap tuong ung voi phieu xuat " + LogUtil.TraceData("ExpMest", raw));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool IsExistsImpMest(HIS_EXP_MEST raw, ref HIS_IMP_MEST impMest, ref List<HIS_EXP_MEST_MEDICINE> expMedicines, ref List<HIS_EXP_MEST_MATERIAL> expMaterials, ref List<HIS_IMP_MEST_MEDICINE> impMedicines, ref List<HIS_IMP_MEST_MATERIAL> impMaterials)
        {
            bool valid = true;
            try
            {
                List<HIS_IMP_MEST> exists = new HisImpMestGet().GetByChmsExpMestId(raw.ID);
                if (!IsNotNullOrEmpty(exists))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Khong ton tai phieu nhap tuong ung voi phieu xuat " + LogUtil.TraceData("ExpMest", raw));
                }
                impMest = exists.FirstOrDefault();
                impMedicines = new HisImpMestMedicineGet().GetByImpMestId(impMest.ID);
                impMaterials = new HisImpMestMaterialGet().GetByImpMestId(impMest.ID);
                expMedicines = new HisExpMestMedicineGet().GetByExpMestId(raw.ID);
                expMaterials = new HisExpMestMaterialGet().GetByExpMestId(raw.ID);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool IsAllowUnapprove(HIS_EXP_MEST raw, ref List<HIS_EXP_MEST_MEDICINE> expMedicines, ref List<HIS_EXP_MEST_MATERIAL> expMaterials)
        {
            bool valid = true;
            try
            {
                if (raw.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE)
                {
                    HIS_EXP_MEST_STT stt = HisExpMestSttCFG.DATA.Where(o => o.ID == raw.EXP_MEST_STT_ID).FirstOrDefault();
                    HIS_EXP_MEST_STT sttExec = HisExpMestSttCFG.DATA.Where(o => o.ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_PhieuThayDoiCoSoDangOTrangThaiKhongChoPhepTuChoiDuyet, stt.EXP_MEST_STT_NAME, sttExec.EXP_MEST_STT_NAME);
                    return false;
                }
                expMedicines = new HisExpMestMedicineGet().GetByExpMestId(raw.ID);
                expMaterials = new HisExpMestMaterialGet().GetByExpMestId(raw.ID);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool IsAllowExport(HIS_EXP_MEST raw, ref List<HIS_EXP_MEST_MEDICINE> expMedicines, ref List<HIS_EXP_MEST_MATERIAL> expMaterials)
        {
            bool valid = true;
            try
            {
                if (raw.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_PhieuChuaDuocDuyetHoacDaThucXuat);
                    return false;
                }
                expMedicines = new HisExpMestMedicineGet().GetByExpMestId(raw.ID);
                expMaterials = new HisExpMestMaterialGet().GetByExpMestId(raw.ID);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool CheckExistsExpMestBase(HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                HisExpMestFilterQuery filter = new HisExpMestFilterQuery();
                filter.HAS_CHMS_TYPE_ID = true;
                filter.EXP_MEST_STT_IDs = new List<long>(){
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST,
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE
                };
                if (expMest.CHMS_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST.CHMS_TYPE__ID__ADDITION)
                {
                    filter.MEDI_STOCK_ID__OR__IMP_MEDI_STOCK_ID = expMest.IMP_MEDI_STOCK_ID.Value;
                }
                else
                {
                    filter.MEDI_STOCK_ID__OR__IMP_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                }
                List<HIS_EXP_MEST> expMests = new HisExpMestGet().Get(filter);
                if (IsNotNullOrEmpty(expMests))
                {
                    V_HIS_MEDI_STOCK stock = HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == filter.MEDI_STOCK_ID__OR__IMP_MEDI_STOCK_ID);
                    string name = stock != null ? stock.MEDI_STOCK_NAME : "";
                    string codes = String.Join(",", expMests.Select(s => s.EXP_MEST_CODE).ToList());
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMediStock_TonTaiPhieuHoanBoSungChuaXuat, name, codes);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }
    }
}

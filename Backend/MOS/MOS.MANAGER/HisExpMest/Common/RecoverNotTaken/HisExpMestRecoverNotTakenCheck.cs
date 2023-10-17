using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.RecoverNotTaken
{
    class HisExpMestRecoverNotTakenCheck : BusinessBase
    {
        internal HisExpMestRecoverNotTakenCheck()
            : base()
        {

        }

        internal HisExpMestRecoverNotTakenCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool IsWorkingInStock(long workingRoomId, HIS_EXP_MEST raw)
        {
            bool valid = true;
            try
            {
                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(workingRoomId);
                if (workPlace == null || workPlace.MediStockId != raw.MEDI_STOCK_ID)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanDangKhongTruyCapVaoKhoXuatKhongChoPhepThucHien);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsExamPrescription(HIS_EXP_MEST raw)
        {
            bool valid = true;
            try
            {
                if (raw.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_CacPhieuKhongPhaiDonPhongKham, raw.EXP_MEST_CODE);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool HasDetail(HIS_EXP_MEST raw, ref List<HIS_EXP_MEST_MEDICINE> medicines, ref List<HIS_EXP_MEST_MATERIAL> materials, ref List<HIS_SERE_SERV> sereServs, ref HIS_TREATMENT treatment)
        {
            bool valid = true;
            try
            {
                if (!raw.TDL_TREATMENT_ID.HasValue || !raw.SERVICE_REQ_ID.HasValue)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("TDL_TREATMENT_ID AND SERVICE_REQ_ID Is Null");
                }
                List<HIS_EXP_MEST_MEDICINE> expMedicines = new HisExpMestMedicineGet().GetByExpMestId(raw.ID);
                List<HIS_EXP_MEST_MATERIAL> expMaterials = new HisExpMestMaterialGet().GetByExpMestId(raw.ID);

                if (!IsNotNullOrEmpty(expMaterials) && !IsNotNullOrEmpty(expMedicines))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_KhongCoThongTinThuocVatTu, raw.EXP_MEST_CODE);
                    return false;
                }

                List<HIS_SERE_SERV> ss = new HisSereServGet(param).GetByServiceReqId(raw.SERVICE_REQ_ID.Value);

                if (!IsNotNullOrEmpty(ss))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("HIS_SERE_SERV Is Empty");
                }

                treatment = new HisTreatmentGet().GetById(raw.TDL_TREATMENT_ID.Value);

                medicines = expMedicines;
                materials = expMaterials;
                sereServs = ss;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool VerifyUpdateSereServ(HIS_TREATMENT treatment, List<HIS_SERE_SERV> sereServs)
        {
            bool valid = true;
            try
            {
                List<HIS_SERE_SERV> isNoExecutes = sereServs.Where(o => o.IS_NO_EXECUTE == Constant.IS_TRUE).ToList();
                List<HIS_SERE_SERV> isExecutes = sereServs.Where(o => o.IS_NO_EXECUTE != Constant.IS_TRUE).ToList();
                if (IsNotNullOrEmpty(isNoExecutes) && IsNotNullOrEmpty(isExecutes))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Ton tai ca SereServ IsNoExecute va IsExecute");
                }
                if (IsNotNullOrEmpty(isNoExecutes))
                {
                    HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
                    valid = valid && treatChecker.IsUnLock(treatment);
                    valid = valid && treatChecker.IsUnTemporaryLock(treatment);
                    valid = valid && treatChecker.IsUnLockHein(treatment);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsPrescription(HIS_EXP_MEST raw)
        {
            bool valid = true;
            try
            {
                if (raw.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK && raw.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_CacPhieuKhongPhaiDonPhongKhamHoacDonDieuTri, raw.EXP_MEST_CODE);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}

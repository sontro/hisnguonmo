using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Base.BaseHandler;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Approve;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.MANAGER.HisMediStock;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.BaseDelete
{
    class HisExpMestBaseDelete : BusinessBase
    {
        internal HisExpMestBaseDelete()
            : base()
        {

        }

        internal HisExpMestBaseDelete(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisExpMestSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_EXP_MEST expMest = null;
                List<HIS_EXP_MEST_METY_REQ> metyReqs = null;
                List<HIS_EXP_MEST_MATY_REQ> matyReqs = null;
                WorkPlaceSDO workPlace = null;

                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                HisExpMestBaseHandlerCheck checker = new HisExpMestBaseHandlerCheck(param);
                HisExpMestApproveCheck approveChecker = new HisExpMestApproveCheck(param);
                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && commonChecker.VerifyId(data.ExpMestId, ref expMest);
                valid = valid && commonChecker.IsUnlock(expMest);
                valid = valid && commonChecker.IsChmsAdditionOrReduction(expMest);
                valid = valid && this.HasWorkPlaceInfo(data.ReqRoomId, ref workPlace);
                valid = valid && checker.IsNotExistsImpMest(expMest);
                valid = valid && commonChecker.IsInRequest(expMest);
                valid = valid && commonChecker.IsUnNotTaken(expMest);
                valid = valid && commonChecker.HasNotInExpMestAggr(expMest);
                valid = valid && commonChecker.HasNoNationalCode(expMest);
                valid = valid && commonChecker.HasNotBill(expMest);
                valid = valid && this.ValidData(expMest, workPlace, ref metyReqs, ref matyReqs);

                if (valid)
                {
                    List<string> sqls = new List<string>();
                    sqls.Add(String.Format("DELETE HIS_EXP_MEST_METY_REQ WHERE EXP_MEST_ID = {0}", expMest.ID));
                    sqls.Add(String.Format("DELETE HIS_EXP_MEST_MATY_REQ WHERE EXP_MEST_ID = {0}", expMest.ID));
                    sqls.Add(String.Format("DELETE HIS_EXP_MEST WHERE ID = {0}", expMest.ID));

                    if (!DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Sqls: " + sqls.ToString());
                    }
                    result = true;
                    new EventLogGenerator(EventLog.Enum.HisExpMest_XoaPhieuThayDoiCoSo).ExpMestCode(expMest.EXP_MEST_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private bool ValidData(HIS_EXP_MEST expMest, WorkPlaceSDO workPlace, ref List<HIS_EXP_MEST_METY_REQ> metyReqs, ref List<HIS_EXP_MEST_MATY_REQ> matyReqs)
        {
            bool valid = true;
            try
            {
                if (!workPlace.MediStockId.HasValue)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_BanDangKhongLamViecTaiKho);
                    return false;
                }

                metyReqs = new HisExpMestMetyReqGet().GetByExpMestId(expMest.ID);
                matyReqs = new HisExpMestMatyReqGet().GetByExpMestId(expMest.ID);

                List<HIS_EXP_MEST_MEDICINE> medicines = new HisExpMestMedicineGet().GetByExpMestId(expMest.ID);
                List<HIS_EXP_MEST_MATERIAL> materials = new HisExpMestMaterialGet().GetByExpMestId(expMest.ID);

                if (IsNotNullOrEmpty(medicines) || IsNotNullOrEmpty(materials))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Ton tai HIS_EXP_MEST_MEDICINE or HIS_EXP_MEST_MATERIAL. kiem tra lai du lieu");
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

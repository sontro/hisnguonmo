using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Unapprove;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseHandler.Unapprove
{
    class HisExpMestBaseUnapprove : BusinessBase
    {
        private ExpMestProcessor expMestProcessor;
        private MedicineProcessor medicineProcessor;
        private MaterialProcessor materialProcessor;

        internal HisExpMestBaseUnapprove()
            : base()
        {
            this.Init();
        }

        internal HisExpMestBaseUnapprove(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.expMestProcessor = new ExpMestProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
        }

        internal bool Run(HisExpMestSDO data, ref HIS_EXP_MEST resultData)
        {
            bool result = false;
            try
            {
                List<HIS_EXP_MEST_MEDICINE> medicines = null;
                List<HIS_EXP_MEST_MATERIAL> materials = null;

                bool valid = true;
                HIS_EXP_MEST expMest = null;
                WorkPlaceSDO workplace = null;
                HisExpMestBaseHandlerCheck checker = new HisExpMestBaseHandlerCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);

                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && commonChecker.VerifyId(data.ExpMestId, ref expMest);
                valid = valid && commonChecker.IsUnlock(expMest);
                valid = valid && commonChecker.IsChmsAdditionOrReduction(expMest);
                valid = valid && this.HasWorkPlaceInfo(data.ReqRoomId, ref workplace);
                valid = valid && checker.IsStockAllowHandler(expMest, workplace);
                valid = valid && commonChecker.HasNotInExpMestAggr(expMest);
                valid = valid && commonChecker.IsUnNotTaken(expMest);
                valid = valid && commonChecker.IsNotBeingApproved(expMest);
                valid = valid && checker.IsAllowUnapprove(expMest, ref medicines, ref materials);
                if (valid)
                {
                    List<string> sqls = new List<string>();

                    if (!this.expMestProcessor.Run(expMest))
                    {
                        throw new Exception("expMestProcessor. Ket thuoc nghiep vu");
                    }

                    if (!this.medicineProcessor.Run(expMest, medicines, ref sqls))
                    {
                        throw new Exception("medicineProcessor. Ket thuoc nghiep vu");
                    }
                    if (!this.materialProcessor.Run(expMest, materials, ref sqls))
                    {
                        throw new Exception("materialProcessor. Ket thuoc nghiep vu");
                    }

                    ///execute sql se thuc hien cuoi cung de de dang trong viec rollback
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    resultData = expMest;
                    result = true;
                    new EventLogGenerator(EventLog.Enum.HisExpMest_HuyDuyetPhieuThayDoiCoSo).ExpMestCode(expMest.EXP_MEST_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                resultData = null;
                this.RollBack();
                result = false;
            }
            return result;
        }

        private void RollBack()
        {
            this.materialProcessor.Rollback();
            this.medicineProcessor.Rollback();
            this.expMestProcessor.Rollback();
        }
    }
}

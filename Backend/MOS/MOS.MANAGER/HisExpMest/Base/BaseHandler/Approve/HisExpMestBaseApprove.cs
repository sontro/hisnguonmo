using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Approve;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseHandler.Approve
{
    class HisExpMestBaseApprove : BusinessBase
    {
        private ExpMestProcessor expMestProcessor;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;

        internal HisExpMestBaseApprove()
            : base()
        {
            this.Init();
        }

        internal HisExpMestBaseApprove(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.expMestProcessor = new ExpMestProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
        }

        internal bool Run(HisExpMestApproveSDO data, ref HisExpMestResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_EXP_MEST expMest = null;
                List<HIS_EXP_MEST_METY_REQ> metyReqs = null;
                List<HIS_EXP_MEST_MATY_REQ> matyReqs = null;
                List<HIS_EXP_MEST_BLTY_REQ> bltyReqs = null;
                WorkPlaceSDO workPlace = null;

                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                HisExpMestBaseHandlerCheck checker = new HisExpMestBaseHandlerCheck(param);
                HisExpMestApproveCheck approveChecker = new HisExpMestApproveCheck(param);
                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && commonChecker.VerifyId(data.ExpMestId, ref expMest);
                valid = valid && commonChecker.IsUnlock(expMest);
                valid = valid && commonChecker.IsChmsAdditionOrReduction(expMest);
                valid = valid && this.HasWorkPlaceInfo(data.ReqRoomId, ref workPlace);
                valid = valid && checker.IsStockAllowHandler(expMest, workPlace);
                valid = valid && checker.IsNotExistsImpMest(expMest);
                valid = valid && commonChecker.IsInRequest(expMest);
                valid = valid && commonChecker.IsUnNotTaken(expMest);
                valid = valid && commonChecker.IsNotBeingApproved(expMest);
                valid = valid && approveChecker.ValidateData(data, expMest, ref metyReqs, ref matyReqs, ref bltyReqs);
                valid = valid && approveChecker.IsNotApprovalAmountExceed(data, metyReqs, matyReqs, bltyReqs);

                if (valid)
                {
                    List<string> sqls = new List<string>();
                    long time = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    string username = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    List<HIS_EXP_MEST_MEDICINE> expMedicines = null;
                    List<HIS_EXP_MEST_MATERIAL> expMaterials = null;

                    if (!this.expMestProcessor.Run(expMest, data.Description, loginname, username, time))
                    {
                        throw new Exception("expMestProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.materialProcessor.Run(expMest, matyReqs, data.Materials, loginname, username, time, ref expMaterials, ref sqls))
                    {
                        throw new Exception("expMaterialProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.medicineProcessor.Run(expMest, metyReqs, data.Medicines, loginname, username, time, ref expMedicines, ref sqls))
                    {
                        throw new Exception("expMedicineProcessor. Ket thuc nghiep vu");
                    }

                    sqls.Add(String.Format("UPDATE HIS_EXP_MEST SET IS_BEING_APPROVED = NULL WHERE ID = {0}", expMest.ID));

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls: " + sqls.ToString());
                    }

                    expMest.IS_BEING_APPROVED = null;
                    result = true;
                    this.PassResult(ref resultData, expMest, expMedicines, expMaterials);

                    new EventLogGenerator(EventLog.Enum.HisExpMest_DuyetPhieuThayDoiCoSo).ExpMestCode(expMest.EXP_MEST_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                result = false;
            }
            return result;
        }

        private void PassResult(ref HisExpMestResultSDO resultData, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> expMedicines, List<HIS_EXP_MEST_MATERIAL> expMaterials)
        {
            resultData = new HisExpMestResultSDO();
            resultData.ExpMest = expMest;
            resultData.ExpMaterials = expMaterials;
            resultData.ExpMedicines = expMedicines;
        }

        private void Rollback()
        {
            this.materialProcessor.Rollback();
            this.medicineProcessor.Rollback();
            this.expMestProcessor.Rollback();
        }
    }
}

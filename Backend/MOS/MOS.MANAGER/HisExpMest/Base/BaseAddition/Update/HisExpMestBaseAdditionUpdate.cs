using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.BaseAddition;
using MOS.MANAGER.HisExpMest.Common;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseAddition.Update
{
    class HisExpMestBaseAdditionUpdate : BusinessBase
    {
        private ExpMestProcessor expMestProcessor;
        private MedicineProcessor medicineProcessor;
        private MaterialProcessor materialProcessor;

        internal HisExpMestBaseAdditionUpdate()
            : base()
        {
            this.Init();
        }

        internal HisExpMestBaseAdditionUpdate(CommonParam param)
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

        internal bool Run(CabinetBaseAdditionSDO data, ref HisExpMestResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_EXP_MEST raw = null;
                List<HIS_EXP_MEST_METY_REQ> metyReqs = null;
                List<HIS_EXP_MEST_MATY_REQ> matyReqs = null;
                List<HIS_MEDI_STOCK_METY> stockMetys = null;
                List<HIS_MEDI_STOCK_MATY> stockMatys = null;
                WorkPlaceSDO workplace = null;

                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                HisExpMestBaseAdditionCheck addChecker = new HisExpMestBaseAdditionCheck(param);
                HisExpMestBaseCheck baseChecker = new HisExpMestBaseCheck(param);

                valid = valid && IsNotNull(data);
                valid = valid && data.Id.HasValue && IsGreaterThanZero(data.Id.Value);
                valid = valid && IsGreaterThanZero(data.WorkingRoomId);
                valid = valid && (IsNotNullOrEmpty(data.MedicineTypes) || IsNotNullOrEmpty(data.MaterialTypes));
                if (!valid)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                }
                valid = valid && commonChecker.VerifyId(data.Id.Value, ref raw);
                valid = valid && commonChecker.IsUnlock(raw);
                valid = valid && commonChecker.IsInRequest(raw);
                valid = valid && commonChecker.HasNoNationalCode(raw);
                valid = valid && commonChecker.HasNotInExpMestAggr(raw);
                valid = valid && commonChecker.HasNotBill(raw);
                valid = valid && commonChecker.IsUnNotTaken(raw);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workplace);
                valid = valid && baseChecker.IsAllowEdit(raw, workplace);
                valid = valid && baseChecker.IsNotExistsApproveDetail(raw, ref stockMetys, ref stockMatys, ref metyReqs, ref matyReqs);
                valid = valid && addChecker.ValidData(data);
                valid = valid && baseChecker.IsNotExistsImpMest(raw);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    List<HIS_EXP_MEST_METY_REQ> newMetyReqs = null;
                    List<HIS_EXP_MEST_MATY_REQ> newMatyReqs = null;

                    if (!this.expMestProcessor.Run(data, raw))
                    {
                        throw new Exception("expMestProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.materialProcessor.Run(data.MaterialTypes, raw, matyReqs, ref newMatyReqs, ref sqls))
                    {
                        throw new Exception("materialProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.medicineProcessor.Run(data.MedicineTypes, raw, metyReqs, ref newMetyReqs, ref sqls))
                    {
                        throw new Exception("medicineProcessor. Ket thuc nghiep vu");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls: " + sqls.ToString());
                    }

                    this.PassResult(ref resultData, raw, newMetyReqs, newMatyReqs);
                    result = true;
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


        private void PassResult(ref HisExpMestResultSDO resultData, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_METY_REQ> expMetyReqs, List<HIS_EXP_MEST_MATY_REQ> expMatyReqs)
        {
            resultData = new HisExpMestResultSDO();
            resultData.ExpMest = expMest;
            resultData.ExpMatyReqs = expMatyReqs;
            resultData.ExpMetyReqs = expMetyReqs;
        }

        private void Rollback()
        {
            try
            {
                this.medicineProcessor.Rollback();
                this.materialProcessor.Rollback();
                this.expMestProcessor.Rollback();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}

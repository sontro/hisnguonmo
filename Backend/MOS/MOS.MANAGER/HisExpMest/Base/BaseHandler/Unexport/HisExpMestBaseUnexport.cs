using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisImpMest;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseHandler.Unexport
{
    class HisExpMestBaseUnexport : BusinessBase
    {
        private ExpMestProcessor expMestProcessor;
        private ExpMaterialProcessor expMaterialProcessor;
        private ExpMedicineProcessor expMedicineProcessor;
        private ImpMestProcessor impMestProcessor;
        private ImpMedicineProcessor impMedicineProcessor;
        private ImpMaterialProcessor impMaterialProcessor;
        private MediStockMatyProcessor mediStockMatyProcessor;
        private MediStockMetyProcessor mediStockMetyProcessor;

        internal HisExpMestBaseUnexport()
            : base()
        {
            this.Init();
        }

        internal HisExpMestBaseUnexport(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.expMestProcessor = new ExpMestProcessor(param);
            this.expMaterialProcessor = new ExpMaterialProcessor(param);
            this.expMedicineProcessor = new ExpMedicineProcessor(param);
            this.impMestProcessor = new ImpMestProcessor(param);
            this.impMedicineProcessor = new ImpMedicineProcessor(param);
            this.impMaterialProcessor = new ImpMaterialProcessor(param);
            this.mediStockMatyProcessor = new MediStockMatyProcessor(param);
            this.mediStockMetyProcessor = new MediStockMetyProcessor(param);
        }

        internal bool Run(HisExpMestSDO data, ref HIS_EXP_MEST resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workplace = null;
                HIS_EXP_MEST expMest = null;
                HIS_IMP_MEST impMest = null;
                List<HIS_EXP_MEST_MEDICINE> expMedicines = null;
                List<HIS_EXP_MEST_MATERIAL> expMaterials = null;
                List<HIS_IMP_MEST_MEDICINE> impMedicines = null;
                List<HIS_IMP_MEST_MATERIAL> impMaterials = null;

                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                HisImpMestCheck impChecker = new HisImpMestCheck(param);
                HisExpMestBaseHandlerCheck checker = new HisExpMestBaseHandlerCheck(param);
                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && commonChecker.VerifyId(data.ExpMestId, ref expMest);
                valid = valid && this.HasWorkPlaceInfo(data.ReqRoomId, ref workplace);
                valid = valid && commonChecker.IsUnlock(expMest);
                valid = valid && commonChecker.IsFinished(expMest);
                valid = valid && commonChecker.HasNoNationalCode(expMest);
                valid = valid && commonChecker.IsChmsAdditionOrReduction(expMest);
                valid = valid && commonChecker.HasNotBill(expMest);
                valid = valid && commonChecker.HasNotInExpMestAggr(expMest);
                valid = valid && checker.IsStockAllowHandler(expMest, workplace);
                valid = valid && checker.IsExistsImpMest(expMest, ref impMest, ref expMedicines, ref expMaterials, ref impMedicines, ref impMaterials);
                valid = valid && commonChecker.HasNoMediStockPeriod(expMedicines, expMaterials);
                valid = valid && impChecker.IsUnLock(impMest);
                valid = valid && impChecker.HasNoNationalCode(impMest);
                valid = valid && impChecker.HasNotInAggrImpMest(impMest);
                valid = valid && impChecker.HasNotMediStockPeriod(impMest);
                valid = valid && checker.CheckExistsExpMestBase(expMest);
                if (valid)
                {
                    List<string> sqls = new List<string>();

                    if (!this.mediStockMatyProcessor.Run(expMest, expMaterials))
                    {
                        throw new Exception("mediStockMatyProcessor. Ket thuoc nghiep vu");
                    }
                    if (!this.mediStockMetyProcessor.Run(expMest, expMedicines))
                    {
                        throw new Exception("mediStockMetyProcessor. Ket thuoc nghiep vu");
                    }
                    if (!this.impMaterialProcessor.Run(impMest, impMaterials, ref sqls))
                    {
                        throw new Exception("impMaterialProcessor. Ket thuoc nghiep vu");
                    }
                    if (!this.impMedicineProcessor.Run(impMest, impMedicines, ref sqls))
                    {
                        throw new Exception("impMedicineProcessor. Ket thuoc nghiep vu");
                    }
                    if (!this.impMestProcessor.Run(impMest, ref sqls))
                    {
                        throw new Exception("impMestProcessor. Ket thuoc nghiep vu");
                    }
                    if (!this.expMestProcessor.Run(expMest))
                    {
                        throw new Exception("expMestProcessor. Ket thuoc nghiep vu");
                    }
                    if (!this.expMaterialProcessor.Run(expMest, expMaterials))
                    {
                        throw new Exception("expMaterialProcessor. Ket thuoc nghiep vu");
                    }
                    if (!this.expMedicineProcessor.Run(expMest, expMedicines))
                    {
                        throw new Exception("expMedicineProcessor. Ket thuoc nghiep vu");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Sqls: " + sqls.ToString());
                    }
                    result = true;
                    resultData = expMest;
                    new EventLogGenerator(EventLog.Enum.HisExpMest_HuyThucXuatPhieuThayDoiCoSo).ExpMestCode(expMest.EXP_MEST_CODE).ImpMestCode(impMest.IMP_MEST_CODE).Run();
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

        private void Rollback()
        {
            try
            {
                this.expMedicineProcessor.Rollback();
                this.expMaterialProcessor.Rollback();
                this.expMestProcessor.Rollback();
                this.impMedicineProcessor.Rollback();
                this.impMaterialProcessor.Rollback();
                this.mediStockMetyProcessor.Rollback();
                this.mediStockMatyProcessor.Rollback();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}

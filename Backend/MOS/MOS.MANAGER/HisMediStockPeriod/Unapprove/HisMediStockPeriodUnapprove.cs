using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockPeriod.Unapprove
{
    class HisMediStockPeriodUnapprove : BusinessBase
    {
        private MediStockPeriodProcessor mediStockPeriodProcessor;
        private ExpMedicineProcessor expMedicineProcessor;
        private ExpMaterialProcessor expMaterialProcessor;
        private ExpMestMetyReqProcessor expMestMetyReqProcessor;
        private ExpMestMatyReqProcessor expMestMatyReqProcessor;
        private ExpMestProcessor expMestProcessor;
        private ImpMedicineProcessor impMedicineProcessor;
        private ImpMaterialProcessor impMaterialProcessor;
        private ImpMestProcessor impMestProcessor;


        internal HisMediStockPeriodUnapprove()
            : base()
        {
            this.Init();
        }

        internal HisMediStockPeriodUnapprove(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.mediStockPeriodProcessor = new MediStockPeriodProcessor(param);
            this.expMaterialProcessor = new ExpMaterialProcessor(param);
            this.expMedicineProcessor = new ExpMedicineProcessor(param);
            this.expMestMatyReqProcessor = new ExpMestMatyReqProcessor(param);
            this.expMestMetyReqProcessor = new ExpMestMetyReqProcessor(param);
            this.expMestProcessor = new ExpMestProcessor(param);
            this.impMaterialProcessor = new ImpMaterialProcessor(param);
            this.impMedicineProcessor = new ImpMedicineProcessor(param);
            this.impMestProcessor = new ImpMestProcessor(param);
        }

        internal bool Run(HisMestPeriodApproveSDO data, ref HIS_MEDI_STOCK_PERIOD resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MEDI_STOCK_PERIOD raw = null;
                HIS_EXP_MEST expMest = null;
                HIS_IMP_MEST impMest = null;
                WorkPlaceSDO workplace = null;

                HisMediStockPeriodUnapproveCheck checker = new HisMediStockPeriodUnapproveCheck(param);
                HisMediStockPeriodCheck commonChecker = new HisMediStockPeriodCheck(param);

                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && commonChecker.VerifyId(data.MediStockPeriodId, ref raw);
                valid = valid && commonChecker.IsUnLock(raw);
                valid = valid && commonChecker.IsApprove(raw);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workplace);
                valid = valid && commonChecker.VerifyWokingInStock(raw, workplace);
                valid = valid && checker.ValidData(raw, ref expMest, ref impMest);
                valid = valid && checker.VerifyExpMest(expMest);
                valid = valid && checker.VerifyImpMest(impMest);

                if (valid)
                {
                    List<string> sqls = new List<string>();

                    if (!this.mediStockPeriodProcessor.Run(raw))
                    {
                        throw new Exception("mediStockPeriodProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.expMedicineProcessor.Run(expMest, ref sqls))
                    {
                        throw new Exception("expMedicineProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.expMaterialProcessor.Run(expMest, ref sqls))
                    {
                        throw new Exception("expMaterialProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.expMestMetyReqProcessor.Run(expMest, ref sqls))
                    {
                        throw new Exception("expMestMetyReqProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.expMestMatyReqProcessor.Run(expMest, ref sqls))
                    {
                        throw new Exception("expMestMetyReqProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.expMestProcessor.Run(expMest, ref sqls))
                    {
                        throw new Exception("expMestProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.impMedicineProcessor.Run(impMest, ref sqls))
                    {
                        throw new Exception("impMedicineProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.impMaterialProcessor.Run(impMest, ref sqls))
                    {
                        throw new Exception("impMaterialProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.impMestProcessor.Run(impMest, ref sqls))
                    {
                        throw new Exception("impMestProcessor. Ket thuc nghiep vu");
                    }

                    if (IsNotNullOrEmpty(sqls)&&!DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Sql: " + sqls.ToString());
                    }

                    result = true;
                    resultData = raw;
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
            this.mediStockPeriodProcessor.Rollback();
        }
    }
}

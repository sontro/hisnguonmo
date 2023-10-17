using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Aggr.Unimport
{
    class HisImpMestAggrUnimport : BusinessBase
    {
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;
        private ImpMestProcessor impMestProcessor;

        internal HisImpMestAggrUnimport()
            : base()
        {
            this.Init();
        }

        internal HisImpMestAggrUnimport(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.impMestProcessor = new ImpMestProcessor(param);
        }

        internal bool Run(HIS_IMP_MEST data, ref HIS_IMP_MEST resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_IMP_MEST raw = null;
                List<HIS_IMP_MEST> childs = null;
                List<HIS_IMP_MEST_MEDICINE> impMestMedicnes = null;
                List<HIS_IMP_MEST_MATERIAL> impMestMaterials = null;
                HisImpMestCheck checker = new HisImpMestCheck(param);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.HasNotInAggrImpMest(raw);
                valid = valid && checker.HasNotMediStockPeriod(raw);
                valid = valid && checker.IsUnLockMediStock(raw);
                valid = valid && checker.VerifyStatusForCancelImport(raw);
                valid = valid && checker.IsAggrImpMest(raw, ref childs, ref impMestMedicnes, ref impMestMaterials);
                valid = valid && checker.CheckMediStockPermission(raw, false);
                valid = valid && checker.CheckImpLoginnamePermission(raw);
                valid = valid && checker.IsUnLock(childs);
                valid = valid && checker.VerifyStatusForCancelImport(childs);
                valid = valid && checker.HasNotMediStockPeriod(childs);
                if (valid)
                {
                    List<string> sqls = new List<string>();

                    if (!this.materialProcessor.Run(raw,impMestMaterials,ref sqls))
                    {
                        throw new Exception("materialProcessor. Rollback du lieu");
                    }

                    if (!this.medicineProcessor.Run(raw, impMestMedicnes, ref sqls))
                    {
                        throw new Exception("medicineProcessor. Rollback du lieu");
                    }

                    if (!this.impMestProcessor.Run(raw,childs))
                    {
                        throw new Exception("impMestProcessor. Rollback du lieu");
                    }

                    if (!DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls. Rollback du lieu");
                    }

                    new EventLogGenerator(EventLog.Enum.HisImpMest_HuyThucNhapPhieuNhap).ImpMestCode(raw.IMP_MEST_CODE).Run();

                    resultData = raw;
                    result = true;
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
                this.impMestProcessor.RollbackData();
                this.medicineProcessor.RollbackData();
                this.materialProcessor.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}

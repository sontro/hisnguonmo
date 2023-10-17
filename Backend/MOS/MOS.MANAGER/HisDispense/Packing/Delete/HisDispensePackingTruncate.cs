﻿using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Packing.Delete
{
    class HisDispensePackingTruncate : BusinessBase
    { 
        private PackingProcessor packingProcessor;
        private ExpMestProcessor expMestProcessor;
        private ExpMestMaterialProcessor expMestMaterialProcessor;
        private ImpMestProcessor impMestProcessor;

        internal HisDispensePackingTruncate()
            : base()
        {
            this.Init();
        }

        internal HisDispensePackingTruncate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.packingProcessor = new PackingProcessor(param);
            this.expMestProcessor = new ExpMestProcessor(param);
            this.expMestMaterialProcessor = new ExpMestMaterialProcessor(param);
            this.impMestProcessor = new ImpMestProcessor(param);
        }

        internal bool Run(HisPackingSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_DISPENSE dispense = null;
                HIS_EXP_MEST expMest = null;
                HIS_IMP_MEST impMest = null;
                HisPackingTruncateCheck checker = new HisPackingTruncateCheck(param);
                HisDispenseCheck commonChecker = new HisDispenseCheck(param);
                valid = valid && checker.CheckValidData(data);
                valid = valid && checker.ValidDispense(data, ref dispense);
                valid = valid && commonChecker.IsUnLock(dispense);
                valid = valid && commonChecker.IsPacking(dispense);
                valid = valid && checker.CheckExpMest(dispense, ref expMest);
                valid = valid && checker.CheckImpMest(dispense, ref impMest);
                valid = valid && checker.CheckWorkPlace(data, dispense);
                valid = valid && checker.CheckPermission(dispense);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    if (!this.impMestProcessor.Run(impMest, ref sqls))
                    {
                        throw new Exception("impMestProcessor. Rollback du lieu");
                    }

                    if (!this.expMestMaterialProcessor.Run(expMest, ref sqls))
                    {
                        throw new Exception("expMestMaterialProcessor. Rollback du lieu");
                    }

                    if (!this.expMestProcessor.Run(expMest, ref sqls))
                    {
                        throw new Exception("expMestProcessor. Rollback du lieu");
                    }

                    if (!this.packingProcessor.Run(dispense, ref sqls))
                    {
                        throw new Exception("packingProcessor. Rollback du lieu");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls. Rollback du lieu");
                    }

                    new EventLogGenerator(EventLog.Enum.HisDispense_XoaPhieuDongGoi).DispenseCode(dispense.DISPENSE_CODE).ExpMestCode(expMest.EXP_MEST_CODE).ImpMestCode(impMest.IMP_MEST_CODE).Run();

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
    }
}

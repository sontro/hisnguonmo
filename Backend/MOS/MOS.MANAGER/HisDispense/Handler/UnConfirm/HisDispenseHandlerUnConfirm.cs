using Inventec.Core;
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

namespace MOS.MANAGER.HisDispense.Handler.UnConfirm
{
    class HisDispenseHandlerUnConfirm : BusinessBase
    {
        private DispenseProcessor dispenseProcessor;
        private ExpMestProcessor expMestProcessor;
        private ExpMestMaterialProcessor expMestMaterialProcessor;
        private ExpMestMedicineProcessor expMestMedicineProcessor;
        private ImpMestProcessor impMestProcessor;
        private MedicineMaterialProcessor medicineMaterialProcessor;
        private MedicineMedicineProcessor medicineMedicineProcessor;

        internal HisDispenseHandlerUnConfirm()
            : base()
        {
            this.Init();
        }

        internal HisDispenseHandlerUnConfirm(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.dispenseProcessor = new DispenseProcessor(param);
            this.expMestProcessor = new ExpMestProcessor(param);
            this.expMestMaterialProcessor = new ExpMestMaterialProcessor(param);
            this.expMestMedicineProcessor = new ExpMestMedicineProcessor(param);
            this.impMestProcessor = new ImpMestProcessor(param);
            this.medicineMaterialProcessor = new MedicineMaterialProcessor(param);
            this.medicineMedicineProcessor = new MedicineMedicineProcessor(param);
        }

        internal bool Run(HisDispenseConfirmSDO data, ref HisDispenseResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_DISPENSE dispense = null;
                HIS_EXP_MEST expMest = null;
                HIS_IMP_MEST impMest = null;
                List<HIS_EXP_MEST_MATERIAL> expMestMaterials = null;
                List<HIS_EXP_MEST_MEDICINE> expMestMedicines = null;
                HIS_IMP_MEST_MEDICINE impMestMedicine = null;

                HisDispenseHandlerUnConfirmCheck checker = new HisDispenseHandlerUnConfirmCheck(param);
                HisDispenseCheck commonChecker = new HisDispenseCheck(param);
                valid = valid && checker.CheckValidData(data);
                valid = valid && checker.ValidDispense(data, ref dispense);
                valid = valid && commonChecker.IsUnLock(dispense);
                valid = valid && commonChecker.IsDispense(dispense);
                valid = valid && checker.CheckExpMest(dispense, ref expMest, ref expMestMaterials, ref expMestMedicines);
                valid = valid && checker.CheckImpMest(dispense, ref impMest, ref impMestMedicine);
                valid = valid && checker.CheckWorkPlace(data, dispense);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    if (!this.dispenseProcessor.Run(dispense))
                    {
                        throw new Exception("dispenseProcessor. Rollback du lieu");
                    }

                    if (!this.expMestProcessor.Run(expMest))
                    {
                        throw new Exception("expMestProcessor. Rollback du lieu");
                    }

                    if (!this.expMestMaterialProcessor.Run(expMest, expMestMaterials))
                    {
                        throw new Exception("expMestMaterialProcessor. Rollback du lieu");
                    }

                    if (!this.expMestMedicineProcessor.Run(expMest, expMestMedicines))
                    {
                        throw new Exception("expMestMedicineProcessor. Rollback du lieu");
                    }

                    if (!this.impMestProcessor.Run(impMest, impMestMedicine, ref sqls))
                    {
                        throw new Exception("impMestProcessor. Rollback du lieu");
                    }

                    if (!this.medicineMaterialProcessor.Run(impMestMedicine, ref sqls))
                    {
                        throw new Exception("medicineMaterialProcessor. Rollback du lieu");
                    }

                    if (!this.medicineMedicineProcessor.Run(impMestMedicine, ref sqls))
                    {
                        throw new Exception("medicineMedicineProcessor. Rollback du lieu");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Sqls. Rollback du lieu");
                    }

                    this.ParseResult(ref resultData, dispense, expMest, impMest);

                    new EventLogGenerator(EventLog.Enum.HisDispense_HuyXacNhanPhieuBaoChe).DispenseCode(dispense.DISPENSE_CODE).ExpMestCode(expMest.EXP_MEST_CODE).ImpMestCode(impMest.IMP_MEST_CODE).Run();

                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                resultData = null;
                result = false;
            }
            return result;
        }

        private void ParseResult(ref HisDispenseResultSDO resultData, HIS_DISPENSE dispense, HIS_EXP_MEST expMest, HIS_IMP_MEST impMest)
        {
            try
            {
                resultData = new HisDispenseResultSDO();
                resultData.HisDispense = dispense;
                resultData.HisExpMest = expMest;
                resultData.HisImpMest = impMest;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void Rollback()
        {
            try
            {
                this.impMestProcessor.RollbackData();
                this.expMestMedicineProcessor.RollbackData();
                this.expMestMaterialProcessor.RollbackData();
                this.expMestProcessor.RollbackData();
                this.dispenseProcessor.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}

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

namespace MOS.MANAGER.HisDispense.Handler.Confirm
{
    class HisDispenseHandlerConfirm : BusinessBase
    {
        private DispenseProcessor dispenseProcessor;
        private ExpMestProcessor expMestProcessor;
        private ExpMestMaterialProcessor expMestMaterialProcesor;
        private ExpMestMedicineProcessor expMestMedicineProcessor;
        private ImpMestProcessor impMestProcessor;
        private MedicineMaterialProcessor medicineMaterialProcessor;
        private MedicineMedicineProcessor medicineMedicineProcessor;

        internal HisDispenseHandlerConfirm()
            : base()
        {
            this.Init();
        }

        internal HisDispenseHandlerConfirm(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.dispenseProcessor = new DispenseProcessor(param);
            this.expMestProcessor = new ExpMestProcessor(param);
            this.expMestMaterialProcesor = new ExpMestMaterialProcessor(param);
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
                HIS_MEDICINE medicine = null;
                HisDispenseHandlerConfirmCheck checker = new HisDispenseHandlerConfirmCheck(param);
                HisDispenseCheck commonChecker = new HisDispenseCheck(param);
                valid = valid && checker.CheckValidData(data);
                valid = valid && checker.ValidDispense(data, ref dispense);
                valid = valid && commonChecker.IsUnLock(dispense);
                valid = valid && commonChecker.IsDispense(dispense);
                valid = valid && checker.CheckExpMest(dispense, ref expMest);
                valid = valid && checker.CheckImpMest(dispense, ref impMest);
                valid = valid && checker.CheckWorkPlace(data, dispense);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    string username = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    if (!this.dispenseProcessor.Run(dispense))
                    {
                        throw new Exception("dispenseProcessor. Rollback du lieu");
                    }

                    if (!this.expMestProcessor.Run(dispense, expMest, loginname, username))
                    {
                        throw new Exception("expMestProcessor. Rollback du lieu");
                    }

                    if (!this.expMestMaterialProcesor.Run(expMest, ref expMestMaterials, ref sqls))
                    {
                        throw new Exception("expMestMaterialProcesor. Rollback du lieu");
                    }

                    if (!this.expMestMedicineProcessor.Run(expMest, ref expMestMedicines, ref sqls))
                    {
                        throw new Exception("expMestMedicineProcessor. Rollback du lieu");
                    }

                    if (!this.impMestProcessor.Run(dispense, impMest, loginname, username, ref medicine, ref sqls))
                    {
                        throw new Exception("impMestProcessor. Rollback du lieu");
                    }

                    if (!this.medicineMaterialProcessor.Run(medicine, expMestMaterials))
                    {
                        throw new Exception("medicineMaterialProcessor. Rollback du lieu");
                    }

                    if (!this.medicineMedicineProcessor.Run(medicine, expMestMedicines))
                    {
                        throw new Exception("medicineMedicineProcessor. Rollback du lieu");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    this.ParseResult(ref resultData, dispense, expMest, impMest);

                    new EventLogGenerator(EventLog.Enum.HisDispense_XacNhanPhieuBaoChe).DispenseCode(dispense.DISPENSE_CODE).ExpMestCode(expMest.EXP_MEST_CODE).ImpMestCode(impMest.IMP_MEST_CODE).Run();

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
                this.medicineMedicineProcessor.RollbackData();
                this.medicineMaterialProcessor.RollbackData();
                this.impMestProcessor.RollbackData();
                this.expMestMedicineProcessor.RollbackData();
                this.expMestMaterialProcesor.RollbackData();
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

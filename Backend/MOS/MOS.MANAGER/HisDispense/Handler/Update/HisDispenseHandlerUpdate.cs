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

namespace MOS.MANAGER.HisDispense.Handler.Update
{
    class HisDispenseHandlerUpdate : BusinessBase
    {
        private List<HIS_EXP_MEST_MEDICINE> expMestMedicines = null;
        private List<HIS_EXP_MEST_MATERIAL> expMestMaterials = null;
        private HIS_IMP_MEST_MEDICINE impMestMedicine = null;


        private DispenseProcessor dispenseProcessor;
        private ExpMestProcessor expMestProcessor;
        private ExpMestMaterialProcessor expMestMaterialProcessor;
        private ExpMestMedicineProcessor expMestMedicineProcessor;
        private ImpMestProcessor impMestProcessor;

        internal HisDispenseHandlerUpdate()
            : base()
        {
            this.Init();
        }

        internal HisDispenseHandlerUpdate(CommonParam param)
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
        }

        internal bool Run(HisDispenseUpdateSDO data, ref HisDispenseHandlerResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_DISPENSE dispense = null;
                HIS_EXP_MEST expMest = null;
                HIS_IMP_MEST impMest = null;

                HisDispenseHandlerUpdateCheck checker = new HisDispenseHandlerUpdateCheck(param);
                HisDispenseCheck commonChecker = new HisDispenseCheck(param);
                valid = valid && checker.CheckValidData(data);
                valid = valid && checker.ValidMaterialType(data.MaterialTypes);
                valid = valid && checker.ValidMedicineType(data.MedicineTypes);
                valid = valid && checker.ValidDispense(data, ref dispense);
                valid = valid && commonChecker.IsUnLock(dispense);
                valid = valid && commonChecker.IsDispense(dispense);
                valid = valid && checker.CheckExpMest(dispense, ref expMest);
                valid = valid && checker.CheckImpMest(dispense, ref impMest);
                valid = valid && checker.CheckWorkPlace(data, dispense);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    if (!this.dispenseProcessor.Run(data, dispense))
                    {
                        throw new Exception("dispenseProcessor. Rollback du lieu");
                    }

                    if (!this.expMestProcessor.Run(data, dispense, expMest))
                    {
                        throw new Exception("expMestProcessor. Rollback du lieu");
                    }

                    if (!this.expMestMaterialProcessor.Run(data, expMest, ref this.expMestMaterials, ref sqls))
                    {
                        throw new Exception("expMestMaterialProcessor. Rollback du lieu");
                    }

                    if (!this.expMestMedicineProcessor.Run(data, expMest, ref this.expMestMedicines, ref sqls))
                    {
                        throw new Exception("expMestMedicineProcessor. Rollback du lieu");
                    }

                    if (!this.impMestProcessor.Run(data, dispense, impMest, this.expMestMedicines, this.expMestMaterials, ref this.impMestMedicine, ref sqls))
                    {
                        throw new Exception("impMestProcessor. Rollback du lieu");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("SqlDAO.Execute. Rollback du lieu");
                    }

                    this.ParseResult(ref resultData, dispense, expMest, impMest, data.MedicinePaties);

                    new EventLogGenerator(EventLog.Enum.HisDispense_SuaPhieuBaoChe).DispenseCode(dispense.DISPENSE_CODE).ExpMestCode(expMest.EXP_MEST_CODE).ImpMestCode(impMest.IMP_MEST_CODE).Run();

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

        private void ParseResult(ref HisDispenseHandlerResultSDO resultData, HIS_DISPENSE dispense, HIS_EXP_MEST expMest, HIS_IMP_MEST impMest, List<HIS_MEDICINE_PATY> medicinePatys)
        {
            try
            {
                resultData = new HisDispenseHandlerResultSDO();
                resultData.HisDispense = dispense;
                resultData.HisExpMest = expMest;
                resultData.HisImpMest = impMest;
                resultData.ExpMestMaterials = this.expMestMaterials;
                resultData.ExpMestMedicines = this.expMestMedicines;
                resultData.ImpMestMedicine = this.impMestMedicine;
                resultData.MedicinePaties = medicinePatys;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void Rollback()
        {
            this.impMestProcessor.RollbackData();
            this.expMestMedicineProcessor.RollbackData();
            this.expMestMaterialProcessor.RollbackData();
            this.expMestProcessor.RollbackData();
            this.dispenseProcessor.RollbackData();
        }
    }
}

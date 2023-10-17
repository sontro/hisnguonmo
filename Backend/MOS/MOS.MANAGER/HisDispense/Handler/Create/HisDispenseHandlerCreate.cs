using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDispense.Handler.Create
{
    partial class HisDispenseHandlerCreate : BusinessBase
    {
        private List<HIS_EXP_MEST_MEDICINE> expMestMedicines = null;
        private List<HIS_EXP_MEST_MATERIAL> expMestMaterials = null;
        private HIS_IMP_MEST_MEDICINE impMestMedicine = null;

        private DispenseProcessor dispenseProcessor;
        private ExpMestProcessor expMestProcessor;
        private ImpMestProcessor impMestProcessor;

        internal HisDispenseHandlerCreate()
            : base()
        {
            this.Init();
        }

        internal HisDispenseHandlerCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.dispenseProcessor = new DispenseProcessor(param);
            this.expMestProcessor = new ExpMestProcessor(param);
            this.impMestProcessor = new ImpMestProcessor(param);
        }

        internal bool Run(HisDispenseSDO data, ref HisDispenseHandlerResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_DISPENSE hisDispense = null;
                HIS_EXP_MEST hisExpMest = null;
                HIS_IMP_MEST hisImpMest = null;

                HisDispenseHandlerCreateCheck checker = new HisDispenseHandlerCreateCheck(param);
                valid = valid && checker.CheckValidData(data);
                valid = valid && checker.ValidMaterialType(data.MaterialTypes);
                valid = valid && checker.ValidMedicineType(data.MedicineTypes);
                if (valid)
                {
                    List<string> sqls = new List<string>();

                    if (!this.dispenseProcessor.Run(data, ref hisDispense))
                    {
                        throw new Exception("dispenseProdessor. Rollback du lieu");
                    }

                    if (!this.expMestProcessor.Run(data, hisDispense, ref hisExpMest, ref this.expMestMaterials, ref this.expMestMedicines, ref sqls))
                    {
                        throw new Exception("expMestProcessor. Rollback du lieu");
                    }

                    if (!this.impMestProcessor.Run(data, hisDispense, this.expMestMedicines, this.expMestMaterials, ref hisImpMest, ref this.impMestMedicine))
                    {
                        throw new Exception("impMestProcessor. Rollback du lieu");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("SqlDAO.Execute. Rollback du lieu");
                    }

                    this.ParseResult(ref resultData, hisDispense, hisExpMest, hisImpMest, data.MedicinePaties);

                    new EventLogGenerator(EventLog.Enum.HisDispense_TaoPhieuBaoChe).DispenseCode(hisDispense.DISPENSE_CODE).ExpMestCode(hisExpMest.EXP_MEST_CODE).ImpMestCode(hisImpMest.IMP_MEST_CODE).Run();

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
            this.expMestProcessor.RollbackData();
            this.dispenseProcessor.RollbackData();
        }
    }
}

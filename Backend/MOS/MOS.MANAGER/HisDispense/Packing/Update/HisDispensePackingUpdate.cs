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

namespace MOS.MANAGER.HisDispense.Packing.Update
{
    class HisDispensePackingUpdate : BusinessBase
    {
        private List<HIS_EXP_MEST_MATERIAL> expMestMaterials = null;
        private HIS_IMP_MEST_MATERIAL impMestMaterial = null;


        private PackingProcessor packingProcessor;
        private ExpMestProcessor expMestProcessor;
        private ExpMestMaterialProcessor expMestMaterialProcessor;
        private ImpMestProcessor impMestProcessor;

        internal HisDispensePackingUpdate()
            : base()
        {
            this.Init();
        }

        internal HisDispensePackingUpdate(CommonParam param)
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

        internal bool Run(HisPackingUpdateSDO data, ref HisPackingResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_DISPENSE dispense = null;
                HIS_EXP_MEST expMest = null;
                HIS_IMP_MEST impMest = null;

                HisPackingUpdateCheck checker = new HisPackingUpdateCheck(param);
                HisDispenseCheck commonChecker = new HisDispenseCheck(param);
                valid = valid && checker.CheckValidData(data);
                valid = valid && checker.ValidMaterialType(data.MaterialTypes);
                valid = valid && checker.ValidDispense(data, ref dispense);
                valid = valid && commonChecker.IsUnLock(dispense);
                valid = valid && commonChecker.IsPacking(dispense);
                valid = valid && checker.CheckExpMest(dispense, ref expMest);
                valid = valid && checker.CheckImpMest(dispense, ref impMest);
                valid = valid && checker.CheckWorkPlace(data, dispense);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    if (!this.packingProcessor.Run(data, dispense))
                    {
                        throw new Exception("packingProcessor. Rollback du lieu");
                    }

                    if (!this.expMestProcessor.Run(data, dispense, expMest))
                    {
                        throw new Exception("expMestProcessor. Rollback du lieu");
                    }

                    if (!this.expMestMaterialProcessor.Run(data, expMest, ref this.expMestMaterials, ref sqls))
                    {
                        throw new Exception("expMestMaterialProcessor. Rollback du lieu");
                    }

                    if (!this.impMestProcessor.Run(data, dispense, impMest, this.expMestMaterials, ref this.impMestMaterial, ref sqls))
                    {
                        throw new Exception("impMestProcessor. Rollback du lieu");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("SqlDAO.Execute. Rollback du lieu");
                    }

                    this.ParseResult(ref resultData, dispense, expMest, impMest, data.MaterialPaties);

                    new EventLogGenerator(EventLog.Enum.HisDispense_SuaPhieuDongGoi).DispenseCode(dispense.DISPENSE_CODE).ExpMestCode(expMest.EXP_MEST_CODE).ImpMestCode(impMest.IMP_MEST_CODE).Run();

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

        private void ParseResult(ref HisPackingResultSDO resultData, HIS_DISPENSE dispense, HIS_EXP_MEST expMest, HIS_IMP_MEST impMest, List<HIS_MATERIAL_PATY> materialPatys)
        {
            try
            {
                resultData = new HisPackingResultSDO();
                resultData.HisDispense = dispense;
                resultData.HisExpMest = expMest;
                resultData.HisImpMest = impMest;
                resultData.ExpMestMaterials = this.expMestMaterials;
                resultData.ImpMestMaterial = this.impMestMaterial;
                resultData.MaterialPaties = materialPatys;
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
                this.expMestMaterialProcessor.RollbackData();
                this.expMestProcessor.RollbackData();
                this.packingProcessor.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}

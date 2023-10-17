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

namespace MOS.MANAGER.HisDispense.Packing.Create
{
    class HisDispensePackingCreate : BusinessBase
    {
        private List<HIS_EXP_MEST_MATERIAL> expMestMaterials = null;
        private HIS_IMP_MEST_MATERIAL impMestMaterial = null;


        private PackingProcessor packingProcessor;
        private ExpMestProcessor expMestProcessor;
        private ImpMestProcessor impMestProcessor;        

        internal HisDispensePackingCreate()
            : base()
        {
            this.Init();
        }

        internal HisDispensePackingCreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.packingProcessor = new PackingProcessor(param);
            this.expMestProcessor = new ExpMestProcessor(param);
            this.impMestProcessor = new ImpMestProcessor(param);
        }

        internal bool Run(HisPackingCreateSDO data, ref HisPackingResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_DISPENSE hisDispense = null;
                HIS_EXP_MEST hisExpMest = null;
                HIS_IMP_MEST hisImpMest = null;

                HisDispensePackingCreateCheck checker = new HisDispensePackingCreateCheck(param);
                HisPackingCheck packChecker = new HisPackingCheck(param);
                valid = valid && checker.CheckValidData(data);
                valid = valid && checker.ValidMaterialType(data.MaterialTypes);
                valid = valid && packChecker.CheckWorkPlace(data);
                if (valid)
                {
                    List<string> sqls = new List<string>();

                    if (!this.packingProcessor.Run(data, ref hisDispense))
                    {
                        throw new Exception("packingProcessor. Rollback du lieu");
                    }

                    if (!this.expMestProcessor.Run(data, hisDispense, ref hisExpMest, ref this.expMestMaterials, ref sqls))
                    {
                        throw new Exception("expMestProcessor. Rollback du lieu");
                    }

                    if (!this.impMestProcessor.Run(data, hisDispense, this.expMestMaterials, ref hisImpMest, ref this.impMestMaterial))
                    {
                        throw new Exception("impMestProcessor. Rollback du lieu");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("SqlDAO.Execute. Rollback du lieu");
                    }

                    this.ParseResult(ref resultData, hisDispense, hisExpMest, hisImpMest, data.MaterialPaties);

                    new EventLogGenerator(EventLog.Enum.HisDispense_TaoPhieuDongGoi).DispenseCode(hisDispense.DISPENSE_CODE).ExpMestCode(hisExpMest.EXP_MEST_CODE).ImpMestCode(hisImpMest.IMP_MEST_CODE).Run();

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

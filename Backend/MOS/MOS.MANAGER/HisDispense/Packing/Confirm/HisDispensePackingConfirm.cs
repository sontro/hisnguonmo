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

namespace MOS.MANAGER.HisDispense.Packing.Confirm
{
    class HisDispensePackingConfirm : BusinessBase
    {
        private PackingProcessor packingProcessor;
        private ExpMestProcessor expMestProcessor;
        private ExpMestMaterialProcessor expMestMaterialProcesor;
        private ImpMestProcessor impMestProcessor;
        private MaterialMaterialProcessor materialMaterialProcessor;

        internal HisDispensePackingConfirm()
            : base()
        {
            this.Init();
        }

        internal HisDispensePackingConfirm(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.packingProcessor = new PackingProcessor(param);
            this.expMestProcessor = new ExpMestProcessor(param);
            this.expMestMaterialProcesor = new ExpMestMaterialProcessor(param);
            this.impMestProcessor = new ImpMestProcessor(param);
            this.materialMaterialProcessor = new MaterialMaterialProcessor(param);
        }

        internal bool Run(HisPackingSDO data, ref HisPackingResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_DISPENSE dispense = null;
                HIS_EXP_MEST expMest = null;
                HIS_IMP_MEST impMest = null;
                List<HIS_EXP_MEST_MATERIAL> expMestMaterials = null;
                HIS_MATERIAL material = null;
                HisDispensePackingConfirmCheck checker = new HisDispensePackingConfirmCheck(param);
                HisDispenseCheck commonChecker = new HisDispenseCheck(param);
                valid = valid && checker.CheckValidData(data);
                valid = valid && checker.ValidDispense(data, ref dispense);
                valid = valid && commonChecker.IsPacking(dispense);
                valid = valid && commonChecker.IsUnLock(dispense);
                valid = valid && checker.CheckExpMest(dispense, ref expMest);
                valid = valid && checker.CheckImpMest(dispense, ref impMest);
                valid = valid && checker.CheckWorkPlace(data, dispense);

                if (valid)
                {
                    List<string> sqls = new List<string>();
                    string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    string username = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    if (!this.packingProcessor.Run(dispense))
                    {
                        throw new Exception("packingProcessor. Rollback du lieu");
                    }

                    if (!this.expMestProcessor.Run(dispense, expMest, loginname, username))
                    {
                        throw new Exception("expMestProcessor. Rollback du lieu");
                    }

                    if (!this.expMestMaterialProcesor.Run(expMest, ref expMestMaterials, ref sqls))
                    {
                        throw new Exception("expMestMaterialProcesor. Rollback du lieu");
                    }

                    if (!this.impMestProcessor.Run(dispense, impMest, loginname, username, ref material, ref sqls))
                    {
                        throw new Exception("impMestProcessor. Rollback du lieu");
                    }

                    if (!this.materialMaterialProcessor.Run(material, expMestMaterials))
                    {
                        throw new Exception("materialMaterialProcessor. Rollback du lieu");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    this.ParseResult(ref resultData, dispense, expMest, impMest);

                    new EventLogGenerator(EventLog.Enum.HisDispense_XacNhanPhieuDongGoi).DispenseCode(dispense.DISPENSE_CODE).ExpMestCode(expMest.EXP_MEST_CODE).ImpMestCode(impMest.IMP_MEST_CODE).Run();

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

        private void ParseResult(ref HisPackingResultSDO resultData, HIS_DISPENSE dispense, HIS_EXP_MEST expMest, HIS_IMP_MEST impMest)
        {
            try
            {
                resultData = new HisPackingResultSDO();
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
                this.materialMaterialProcessor.RollbackData();
                this.impMestProcessor.RollbackData();
                this.expMestMaterialProcesor.RollbackData();
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

using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Export;
using MOS.MANAGER.HisMediStock;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseHandler.Export
{
    class HisExpMestBaseExport : BusinessBase
    {
        private ExpMestProcessor expMestProcessor;
        private ExpMaterialProcessor expMaterialProcessor;
        private ExpMedicineProcessor expMedicineProcessor;
        private ImpMestProcessor impMestProcessor;
        private ImpMaterialProcessor impMaterialProcessor;
        private ImpMedicineProcessor impMedicineProcessor;
        private MediStockMatyProcessor mediStockMatyProcessor;
        private MediStockMetyProcessor mediStockMetyProcessor;

        internal HisExpMestBaseExport()
            : base()
        {
            this.Init();
        }

        internal HisExpMestBaseExport(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.expMestProcessor = new ExpMestProcessor(param);
            this.expMaterialProcessor = new ExpMaterialProcessor(param);
            this.expMedicineProcessor = new ExpMedicineProcessor(param);
            this.impMestProcessor = new ImpMestProcessor(param);
            this.impMaterialProcessor = new ImpMaterialProcessor(param);
            this.impMedicineProcessor = new ImpMedicineProcessor(param);
            this.mediStockMatyProcessor = new MediStockMatyProcessor(param);
            this.mediStockMetyProcessor = new MediStockMetyProcessor(param);
        }

        internal bool Run(HisExpMestExportSDO data, ref CabinetBaseResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_EXP_MEST expMest = null;
                HIS_IMP_MEST impMest = null;
                List<HIS_EXP_MEST_MEDICINE> expMedicines = null;
                List<HIS_EXP_MEST_MATERIAL> expMaterials = null;
                WorkPlaceSDO workPlace = null;

                HisMediStockCheck mediStockChecker = new HisMediStockCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                HisExpMestBaseHandlerCheck checker = new HisExpMestBaseHandlerCheck(param);
                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && commonChecker.VerifyId(data.ExpMestId, ref expMest);
                valid = valid && commonChecker.IsUnlock(expMest);
                valid = valid && commonChecker.IsChmsAdditionOrReduction(expMest);
                valid = valid && this.HasWorkPlaceInfo(data.ReqRoomId, ref workPlace);
                valid = valid && checker.IsStockAllowHandler(expMest, workPlace);
                valid = valid && checker.IsNotExistsImpMest(expMest);
                valid = valid && commonChecker.HasNotInExpMestAggr(expMest);
                valid = valid && commonChecker.IsUnNotTaken(expMest);
                valid = valid && checker.IsAllowExport(expMest, ref expMedicines, ref expMaterials);
                valid = valid && mediStockChecker.IsUnLockCache(expMest.MEDI_STOCK_ID);
                valid = valid && mediStockChecker.IsUnLockCache(expMest.IMP_MEDI_STOCK_ID.Value);

                if (valid)
                {
                    List<string> sqls = new List<string>();
                    long time = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    string username = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    List<HIS_IMP_MEST_MEDICINE> impMedicines = null;
                    List<HIS_IMP_MEST_MATERIAL> impMaterials = null;

                    if (!this.expMestProcessor.Run(expMest, loginname, username, time))
                    {
                        throw new Exception("expMestProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.expMaterialProcessor.Run(expMest, expMaterials, loginname, username, time, ref sqls))
                    {
                        throw new Exception("expMaterialProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.expMedicineProcessor.Run(expMest, expMedicines, loginname, username, time, ref sqls))
                    {
                        throw new Exception("expMedicineProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.impMestProcessor.Run(expMest, workPlace, time, loginname, username, ref impMest))
                    {
                        throw new Exception("impMestProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.impMaterialProcessor.Run(impMest, expMaterials, ref impMaterials))
                    {
                        throw new Exception("impMaterialProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.impMedicineProcessor.Run(impMest, expMedicines, ref impMedicines))
                    {
                        throw new Exception("impMedicineProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.mediStockMatyProcessor.Run(expMest, expMaterials, ref sqls))
                    {
                        throw new Exception("mediStockMatyProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.mediStockMetyProcessor.Run(expMest, expMedicines, ref sqls))
                    {
                        throw new Exception("mediStockMetyProcessor. Ket thuc nghiep vu");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls: " + sqls.ToString());
                    }

                    result = true;
                    this.PassResult(ref resultData, expMest, impMest, expMedicines, expMaterials, impMedicines, impMaterials);
                    new EventLogGenerator(EventLog.Enum.HisExpMest_ThucXuatPhieuThayDoiCoSo).ExpMestCode(expMest.EXP_MEST_CODE).ImpMestCode(impMest.IMP_MEST_CODE).Run();
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

        private void PassResult(ref CabinetBaseResultSDO resultData, HIS_EXP_MEST expMest, HIS_IMP_MEST impMest, List<HIS_EXP_MEST_MEDICINE> expMedicines, List<HIS_EXP_MEST_MATERIAL> expMaterials,
            List<HIS_IMP_MEST_MEDICINE> impMedicines, List<HIS_IMP_MEST_MATERIAL> impMaterials)
        {
            resultData = new CabinetBaseResultSDO();
            resultData.ExpMest = expMest;
            resultData.ExpMaterials = expMaterials;
            resultData.ExpMedicines = expMedicines;
            resultData.ImpMest = impMest;
            resultData.ImpMaterials = impMaterials;
            resultData.ImpMedicines = impMedicines;
        }

        private void Rollback()
        {
            this.mediStockMetyProcessor.Rollback();
            this.mediStockMatyProcessor.Rollback();
            this.impMedicineProcessor.Rollback();
            this.impMaterialProcessor.Rollback();
            this.impMestProcessor.Rollback();
            this.expMestProcessor.Rollback();
        }
    }
}

using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Base;
using MOS.MANAGER.HisMediStock;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.BaseAddition.Create
{
    class HisExpMestBaseAdditionCreate : BusinessBase
    {

        private ExpMestProcessor expMestProcessor;
        private MedicineProcessor medicineProcessor;
        private MaterialProcessor materialProcessor;

        internal HisExpMestBaseAdditionCreate()
            : base()
        {
            this.Init();
        }

        internal HisExpMestBaseAdditionCreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.expMestProcessor = new ExpMestProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
        }

        internal bool Run(CabinetBaseAdditionSDO data, ref HisExpMestResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MEDI_STOCK cabinetStock = null;
                HIS_MEDI_STOCK expStock = null;
                HIS_EXP_MEST expMest = null;
                WorkPlaceSDO workPlace = null;
                List<HIS_EXP_MEST_METY_REQ> expMetyReqs = null;
                List<HIS_EXP_MEST_MATY_REQ> expMatyReqs = null;

                HisExpMestBaseAdditionCheck checker = new HisExpMestBaseAdditionCheck(param);
                HisMediStockCheck mediStockChecker = new HisMediStockCheck(param);
                HisExpMestBaseCheck baseChecker = new HisExpMestBaseCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && mediStockChecker.VerifyId(data.CabinetMediStockId, ref cabinetStock);
                valid = valid && mediStockChecker.VerifyId(data.ExpMestMediStockId, ref expStock);
                valid = valid && mediStockChecker.IsUnLock(cabinetStock);
                valid = valid && mediStockChecker.IsUnLock(expStock);
                valid = valid && mediStockChecker.IsCabinetStock(cabinetStock);
                valid = valid && mediStockChecker.IsNotCabinetStock(expStock);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && checker.IsWorkingInImpOrExpStock(workPlace, data.CabinetMediStockId, data.ExpMestMediStockId);
                valid = valid && baseChecker.IsNotExistsExpMestBase(cabinetStock, data.MedicineTypes, data.MaterialTypes);
                valid = valid && checker.ValidData(data);

                if (valid)
                {
                    if (!this.expMestProcessor.Run(data, workPlace, ref expMest))
                    {
                        throw new Exception("expMestProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.materialProcessor.Run(data.MaterialTypes, expMest, ref expMatyReqs))
                    {
                        throw new Exception("materialProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.medicineProcessor.Run(data.MedicineTypes, expMest, ref expMetyReqs))
                    {
                        throw new Exception("medicineProcessor. Ket thuc nghiep vu");
                    }

                    this.PassResult(ref resultData, expMest, expMetyReqs, expMatyReqs);
                    result = true;
                    new EventLogGenerator(EventLog.Enum.HisExpMest_TaoPhieuThayDoiCoSo).ExpMestCode(expMest.EXP_MEST_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.Rollback();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void PassResult(ref HisExpMestResultSDO resultData, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_METY_REQ> expMetyReqs, List<HIS_EXP_MEST_MATY_REQ> expMatyReqs)
        {
            resultData = new HisExpMestResultSDO();
            resultData.ExpMest = expMest;
            resultData.ExpMatyReqs = expMatyReqs;
            resultData.ExpMetyReqs = expMetyReqs;
        }

        private void Rollback()
        {
            try
            {
                this.medicineProcessor.Rollback();
                this.materialProcessor.Rollback();
                this.expMestProcessor.Rollback();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}

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

namespace MOS.MANAGER.HisMediStock.Inventory
{
    class HisMediStockInventory : BusinessBase
    {
        private ExpMestProcessor expMestProcessor;
        private ImpMestProcessor impMestProcessor;
        private ExpMaterialProcessor expMaterialProcessor;
        private ExpMedicineProcessor expMedicineProcessor;
        private ImpMaterialProcessor impMaterialProcessor;
        private ImpMedicineProcessor impMedicineProcessor;

        internal HisMediStockInventory()
            : base()
        {
            this.Init();
        }

        internal HisMediStockInventory(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.expMestProcessor = new ExpMestProcessor(param);
            this.impMestProcessor = new ImpMestProcessor(param);
            this.expMaterialProcessor = new ExpMaterialProcessor(param);
            this.expMedicineProcessor = new ExpMedicineProcessor(param);
            this.impMaterialProcessor = new ImpMaterialProcessor(param);
            this.impMedicineProcessor = new ImpMedicineProcessor(param);
        }

        internal bool Run(HisMediStockInventorySDO data, ref HisMediStockInventoryResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MEDI_STOCK mediStock = null;
                HIS_EXP_MEST expMest = null;
                HIS_IMP_MEST impMest = null;
                WorkPlaceSDO wp = null;
                HisMediStockCheck commonChecker = new HisMediStockCheck(param);
                HisMediStockInventoryCheck checker = new HisMediStockInventoryCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && commonChecker.VerifyId(data.MediStockId, ref mediStock);
                valid = valid && commonChecker.IsUnLock(mediStock);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref wp);
                valid = valid && this.IsWorkingAtRoom(mediStock.ROOM_ID, data.WorkingRoomId);
                valid = valid && checker.ValidData(data);

                if (valid)
                {
                    List<string> sqls = new List<string>();
                    if (!this.expMestProcessor.Run(data, ref expMest))
                    {
                        throw new Exception("expMestProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.impMestProcessor.Run(data, ref impMest))
                    {
                        throw new Exception("impMestProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.expMaterialProcessor.Run(data.ExpMaterials, expMest, ref sqls))
                    {
                        throw new Exception("expMaterialProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.expMedicineProcessor.Run(data.ExpMedicines, expMest, ref sqls))
                    {
                        throw new Exception("expMedicineProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.impMaterialProcessor.Run(data.ImpMaterials, impMest))
                    {
                        throw new Exception("impMaterialProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.impMedicineProcessor.Run(data.ImpMedicines, impMest))
                    {
                        throw new Exception("impMedicineProcessor. Ket thuc nghiep vu");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls: " + sqls.ToString());
                    }

                    this.PassResult(ref resultData, expMest, impMest);
                    result = true;

                    if (impMest != null)
                    {
                        new EventLogGenerator(EventLog.Enum.HisImpMest_TaoPhieuNhap).ImpMestCode(impMest.IMP_MEST_CODE).Run();
                    }
                    if (expMest != null)
                    {
                        new EventLogGenerator(EventLog.Enum.HisExpMest_TaoPhieuXuat).ExpMestCode(expMest.EXP_MEST_CODE).Run();
                    }
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

        private void PassResult(ref HisMediStockInventoryResultSDO resultData, HIS_EXP_MEST expMest, HIS_IMP_MEST impMest)
        {
            try
            {
                resultData = new HisMediStockInventoryResultSDO();
                resultData.ExpMest = expMest;
                resultData.ImpMest = impMest;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void Rollback()
        {
            this.impMedicineProcessor.Rollback();
            this.impMaterialProcessor.Rollback();
            this.expMedicineProcessor.Rollback();
            this.expMaterialProcessor.Rollback();
            this.impMestProcessor.Rollback();
            this.expMestProcessor.Rollback();
        }
    }
}

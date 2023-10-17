using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Aggr.Approve
{
    class HisImpMestAggrApprove : BusinessBase
    {
        private RejectImpMestProcessor rejectImpMestProcessor;
        private ImpMestProcessor impMestProcessor;

        internal HisImpMestAggrApprove()
            : base()
        {
            this.Init();
        }

        internal HisImpMestAggrApprove(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.rejectImpMestProcessor = new RejectImpMestProcessor(param);
            this.impMestProcessor = new ImpMestProcessor(param);
        }

        internal bool Run(ImpMestAggrApprovalSDO data, ref ImpMestAggrApprovalResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_IMP_MEST raw = null;
                List<HIS_IMP_MEST> childs = null;
                List<HIS_IMP_MEST_MEDICINE> impMestMedicines = null;
                List<HIS_IMP_MEST_MATERIAL> impMestMaterials = null;
                List<MobaMedicineSDO> rejectedMedicineList = null;
                List<MobaMaterialSDO> rejectedMaterialList = null;

                WorkPlaceSDO workPlace = null;

                HisImpMestCheck impMestChecker = new HisImpMestCheck(param);
                HisImpMestAggrApproveCheck checker = new HisImpMestAggrApproveCheck(param);

                valid = valid && impMestChecker.VerifyId(data.ImpMestId, ref raw);
                valid = valid && impMestChecker.IsUnLock(raw);
                valid = valid && impMestChecker.HasNotInAggrImpMest(raw);
                valid = valid && impMestChecker.HasNotMediStockPeriod(raw);
                valid = valid && impMestChecker.IsUnLockMediStock(raw);
                valid = valid && impMestChecker.IsRequesting(raw);
                valid = valid && impMestChecker.IsAggrImpMest(raw, ref childs, ref impMestMedicines, ref impMestMaterials);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && impMestChecker.IsWorkingAtMediStock(raw, workPlace);
                valid = valid && impMestChecker.IsUnLock(childs);
                valid = valid && impMestChecker.HasNotMediStockPeriod(childs);
                valid = valid && checker.IsValidData(data, raw, impMestMedicines, impMestMaterials, ref rejectedMedicineList, ref rejectedMaterialList);

                if (valid)
                {
                    List<HIS_IMP_MEST> newImpMests = null;

                    if (!this.rejectImpMestProcessor.Run(raw, workPlace, data.RejectedMediStockId, childs, impMestMedicines, impMestMaterials, rejectedMedicineList, rejectedMaterialList, ref newImpMests))
                    {
                        throw new Exception("Xu ly tao phieu tra thuoc/vat tu bi tu choi duyet that bai");
                    }

                    if (!this.impMestProcessor.Run(raw, childs))
                    {
                        throw new Exception("impMestProcessor. Rollback du lieu");
                    }

                    this.PassResult(raw, ref resultData);
                    new EventLogGenerator(EventLog.Enum.HisImpMest_DuyetPhieuNhap).ImpMestCode(raw.IMP_MEST_CODE).Run();

                    if (IsNotNullOrEmpty(childs))
                    {
                        foreach (HIS_IMP_MEST imp in childs)
                        {
                            new EventLogGenerator(EventLog.Enum.HisImpMest_DuyetPhieuNhap).ImpMestCode(imp.IMP_MEST_CODE).Run();
                        }
                    }

                    if (IsNotNullOrEmpty(newImpMests))
                    {
                        foreach (HIS_IMP_MEST imp in newImpMests)
                        {
                            new EventLogGenerator(EventLog.Enum.HisImpMest_TaoPhieuNhap).ImpMestCode(imp.IMP_MEST_CODE).Run();
                        }
                    }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                result = false;
            }
            return result;
        }

        private void Rollback()
        {
            try
            {
                this.impMestProcessor.RollbackData();
                this.rejectImpMestProcessor.Rollback();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void PassResult(HIS_IMP_MEST aggrImpMest, ref ImpMestAggrApprovalResultSDO resultData)
        {
            if (aggrImpMest != null)
            {
                resultData = new ImpMestAggrApprovalResultSDO();
                resultData.ImpMest = new HisImpMestGet().GetViewById(aggrImpMest.ID);
                resultData.ImpMestMedicines = new HisImpMestMedicineGet().GetViewByAggrImpMestId(aggrImpMest.ID);
                resultData.ImpMestMaterials = new HisImpMestMaterialGet().GetViewByAggrImpMestId(aggrImpMest.ID);
            }
        }
    }
}

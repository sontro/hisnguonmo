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

namespace MOS.MANAGER.HisImpMest.Aggr.Unapprove
{
    class HisImpMestAggrUnapprove : BusinessBase
    {
        private ImpMestProcessor impMestProcessor;
        private RejectImpMestProcessor rejectImpMestProcessor;

        internal HisImpMestAggrUnapprove()
            : base()
        {
            this.Init();
        }

        internal HisImpMestAggrUnapprove(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.impMestProcessor = new ImpMestProcessor(param);
            this.rejectImpMestProcessor = new RejectImpMestProcessor(param);
        }

        internal bool Run(ImpMestAggrUnapprovalSDO data, ref HIS_IMP_MEST resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_IMP_MEST raw = null;
                List<HIS_IMP_MEST> rejectedItemImpMests = null;
                
                WorkPlaceSDO workPlace = null;

                HisImpMestCheck impMestChecker = new HisImpMestCheck(param);
                HisImpMestAggrUnapproveCheck checker = new HisImpMestAggrUnapproveCheck(param);

                valid = valid && impMestChecker.VerifyId(data.ImpMestId, ref raw);
                valid = valid && impMestChecker.IsUnLock(raw);
                valid = valid && impMestChecker.HasNotInAggrImpMest(raw);
                valid = valid && impMestChecker.HasNotMediStockPeriod(raw);
                valid = valid && impMestChecker.IsUnLockMediStock(raw);
                valid = valid && impMestChecker.IsApproved(raw);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && impMestChecker.IsWorkingAtMediStock(raw, workPlace);
                valid = valid && checker.IsValidData(data.ImpMestId, ref rejectedItemImpMests);

                if (valid)
                {
                    List<string> sqls = new List<string>();

                    //Can xu ly phieu hoan truoc khi xu ly phieu tong hop tra
                    if (!this.rejectImpMestProcessor.Run(rejectedItemImpMests, ref sqls))
                    {
                        throw new Exception("Tao sql huy phieu hoan thuoc/vat tu bi tu choi duyet that bai");
                    }

                    if (!this.impMestProcessor.Run(raw, ref sqls))
                    {
                        throw new Exception("Tao sql cap nhat tong hop tra that bai");
                    }

                    if (DAOWorker.SqlDAO.Execute(sqls))
                    {
                        if (IsNotNullOrEmpty(rejectedItemImpMests))
                        {
                            foreach (HIS_IMP_MEST imp in rejectedItemImpMests)
                            {
                                new EventLogGenerator(EventLog.Enum.HisImpMest_HuyPhieuNhap).ImpMestCode(imp.IMP_MEST_CODE).Run();
                            }
                        }

                        new EventLogGenerator(EventLog.Enum.HisImpMest_HuyDuyetPhieuNhap).ImpMestCode(raw.IMP_MEST_CODE).Run();

                        resultData = raw;
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}

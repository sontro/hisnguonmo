using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisMestPeriodMate;
using MOS.MANAGER.HisMestPeriodMedi;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockPeriod.Unapprove
{
    class HisMediStockPeriodUnapproveCheck : BusinessBase
    {
        internal HisMediStockPeriodUnapproveCheck()
            : base()
        {

        }

        internal HisMediStockPeriodUnapproveCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool ValidData(HIS_MEDI_STOCK_PERIOD raw, ref HIS_EXP_MEST expMest, ref HIS_IMP_MEST impMest)
        {
            bool valid = true;
            try
            {
                List<HIS_EXP_MEST> exps = new HisExpMestGet().GetBySourceMestPeriodId(raw.ID);
                expMest = IsNotNullOrEmpty(exps) ? exps.FirstOrDefault() : null;

                List<HIS_IMP_MEST> imps = new HisImpMestGet().GetBySourceMestPeriodId(raw.ID);
                impMest = IsNotNullOrEmpty(imps) ? imps.FirstOrDefault() : null;

                if (!IsNotNull(expMest) && !IsNotNull(impMest))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Khong co du lieu xuat nhap Kiem Ke gan voi Ky kho. " + LogUtil.TraceData("MediStockPeriod", raw));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = true;
            }
            return valid;
        }

        internal bool VerifyExpMest(HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                if (expMest != null)
                {
                    HisExpMestCheck checker = new HisExpMestCheck(param);
                    valid = valid && checker.IsUnlock(expMest);
                    valid = valid && checker.IsUnNotTaken(expMest);
                    valid = valid && checker.HasNotInExpMestAggr(expMest);//thuoc phieu nhap tong hop, ko cho xoa
                    valid = valid && checker.VerifyStatusForDelete(expMest);
                    valid = valid && checker.HasNoNationalCode(expMest);
                    valid = valid && checker.HasNotBill(expMest);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool VerifyImpMest(HIS_IMP_MEST impMest)
        {
            bool valid = true;
            try
            {
                if (impMest != null)
                {
                    HisImpMestCheck checker = new HisImpMestCheck(param);
                    valid = valid && checker.IsUnLock(impMest);
                    valid = valid && checker.HasNotInAggrImpMest(impMest);//thuoc phieu nhap tong hop, ko cho xoa
                    valid = valid && checker.VerifyStatusForDelete(impMest);
                    valid = valid && checker.HasNoNationalCode(impMest);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }
    }
}

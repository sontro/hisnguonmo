using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTransaction;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.Config;
using AutoMapper;

namespace MOS.MANAGER.HisCashout
{
    partial class HisCashoutCreate : BusinessBase
    {
        private List<HIS_CASHOUT> recentHisCashouts = new List<HIS_CASHOUT>();
        private HisTransactionUpdate hisTransactionUpdate;

        internal HisCashoutCreate()
            : base()
        {
            this.hisTransactionUpdate = new HisTransactionUpdate(param);
        }

        internal HisCashoutCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisTransactionUpdate = new HisTransactionUpdate(param);
        }

        internal bool Create(HisCashoutSDO data, ref HIS_CASHOUT resultData)
        {
            bool result = false;
            try
            {
                if (!IsNotNullOrEmpty(data.TransactionIds) || data.CashoutTime <= 0)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    return result;
                }

                List<HIS_TRANSACTION> hisTransactions = new HisTransactionGet().GetByIds(data.TransactionIds);
                List<string> collecteds = hisTransactions
                    .Where(o => o.CASHOUT_ID.HasValue)
                    .Select(o => o.TRANSACTION_CODE).ToList();
                if (IsNotNullOrEmpty(collecteds))
                {
                    string collectedStr = string.Join(",", collecteds);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisCashOut_CacGiaoDichSauDaNopQuy, collectedStr);
                    return false;
                }

                //Kiem tra tong so tien so voi client gui len co khop hay khong
                decimal calcAmount = 0;
                foreach (HIS_TRANSACTION t in hisTransactions)
                {
                    if (t.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT || t.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU)
                    {
                        calcAmount += t.AMOUNT;
                    }
                    else if (t.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__HU)
                    {
                        calcAmount -= t.AMOUNT;
                    }
                    if (t.EXEMPTION.HasValue)
                    {
                        calcAmount -= t.EXEMPTION.Value;
                    }
                    if (t.KC_AMOUNT.HasValue)
                    {
                        calcAmount -= t.KC_AMOUNT.Value;
                    }
                    if (t.TDL_BILL_FUND_AMOUNT.HasValue)
                    {
                        calcAmount -= t.TDL_BILL_FUND_AMOUNT.Value;
                    }
                }

                if (calcAmount != data.Amount)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("So tien client gui len khong khop voi so tien server tinh." + LogUtil.TraceData("calcAmount", calcAmount));
                }

                HIS_CASHOUT cashOut = new HIS_CASHOUT();
                cashOut.USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                cashOut.LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                cashOut.CASHOUT_TIME = data.CashoutTime;
                cashOut.AMOUNT = data.Amount;
                if (this.Create(cashOut))
                {
                    this.ProcessTransaction(hisTransactions, cashOut.ID);
                    resultData = cashOut;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessTransaction(List<HIS_TRANSACTION> toUpdates, long cashOutId)
        {
            if (IsNotNullOrEmpty(toUpdates))
            {
                List<HIS_TRANSACTION> beforeUpdates = Mapper.Map<List<HIS_TRANSACTION>>(toUpdates);
                toUpdates.ForEach(o => o.CASHOUT_ID = cashOutId);
                if (!this.hisTransactionUpdate.UpdateList(toUpdates, beforeUpdates))
                {
                    throw new Exception("Rollback du lieu");
                }
            }
        }

        internal bool Create(HIS_CASHOUT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCashoutCheck checker = new HisCashoutCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisCashoutDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCashout_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCashout that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisCashouts.Add(data);
                    result = true;
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

        internal bool CreateList(List<HIS_CASHOUT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCashoutCheck checker = new HisCashoutCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisCashoutDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCashout_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCashout that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisCashouts.AddRange(listData);
                    result = true;
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

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.recentHisCashouts))
            {
                if (!new HisCashoutTruncate(param).TruncateList(this.recentHisCashouts))
                {
                    LogSystem.Warn("Rollback du lieu HisCashout that bai, can kiem tra lai." + LogUtil.TraceData("recentHisCashouts", this.recentHisCashouts));
                }
            }
            this.hisTransactionUpdate.RollbackData();
        }
    }
}

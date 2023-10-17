using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisTransaction;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCashout
{
    partial class HisCashoutUpdate : BusinessBase
    {
		private List<HIS_CASHOUT> beforeUpdateHisCashouts = new List<HIS_CASHOUT>();
        private HisTransactionUpdate hisTransactionUpdate;

        internal HisCashoutUpdate()
            : base()
        {
            this.hisTransactionUpdate = new HisTransactionUpdate(param);
        }

        internal HisCashoutUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.hisTransactionUpdate = new HisTransactionUpdate(param);
        }

        internal bool Update(HIS_CASHOUT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCashoutCheck checker = new HisCashoutCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_CASHOUT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisCashouts.Add(raw);
					if (!DAOWorker.HisCashoutDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCashout_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCashout that bai." + LogUtil.TraceData("data", data));
                    }
                    
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

        internal bool Update(HisCashoutSDO data, ref HIS_CASHOUT resultData)
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
                    .Where(o => o.CASHOUT_ID.HasValue && o.CASHOUT_ID.Value != data.Id)
                    .Select(o => o.TRANSACTION_CODE)
                    .ToList();

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
                cashOut.ID = data.Id;
                cashOut.USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                cashOut.LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                cashOut.CASHOUT_TIME = data.CashoutTime;
                cashOut.AMOUNT = data.Amount;
                if (this.Update(cashOut))
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

        private void ProcessTransaction(List<HIS_TRANSACTION> hisTransactions, long cashOutId)
        {
            if (IsNotNullOrEmpty(hisTransactions))
            {
                //Lay ra cac giao dich da duoc nop quy va gio ko co trong d/s gui len
                List<HIS_TRANSACTION> existTransactions = new HisTransactionGet().GetByCashoutId(cashOutId);

                List<HIS_TRANSACTION> removeCashOuts = IsNotNullOrEmpty(existTransactions) ? existTransactions.Where(o => !hisTransactions.Exists(t => t.ID == o.ID)).ToList() : null;
                List<HIS_TRANSACTION> addCashOuts = IsNotNullOrEmpty(hisTransactions) ? hisTransactions.Where(o => !existTransactions.Exists(t => t.ID == o.ID)).ToList() : null;

                List<HIS_TRANSACTION> toUpdates = new List<HIS_TRANSACTION>();
                List<HIS_TRANSACTION> beforeUpdates = new List<HIS_TRANSACTION>();

                if (IsNotNullOrEmpty(removeCashOuts))
                {
                    Mapper.CreateMap<HIS_TRANSACTION, HIS_TRANSACTION>();
                    List<HIS_TRANSACTION> tmp = Mapper.Map<List<HIS_TRANSACTION>>(removeCashOuts);

                    removeCashOuts.ForEach(o => o.CASHOUT_ID = null);
                    toUpdates.AddRange(removeCashOuts);
                    beforeUpdates.AddRange(tmp);
                }
                if (IsNotNullOrEmpty(addCashOuts))
                {
                    Mapper.CreateMap<HIS_TRANSACTION, HIS_TRANSACTION>();
                    List<HIS_TRANSACTION> tmp = Mapper.Map<List<HIS_TRANSACTION>>(addCashOuts);
                    addCashOuts.ForEach(o => o.CASHOUT_ID = cashOutId);
                    toUpdates.AddRange(addCashOuts);
                    beforeUpdates.AddRange(tmp);
                }
                if (IsNotNullOrEmpty(toUpdates))
                {
                    if (!this.hisTransactionUpdate.UpdateList(toUpdates, beforeUpdates))
                    {
                        throw new Exception("Rollback du lieu");
                    }
                }
            }
        }

        internal bool UpdateList(List<HIS_CASHOUT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCashoutCheck checker = new HisCashoutCheck(param);
                List<HIS_CASHOUT> listRaw = new List<HIS_CASHOUT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisCashouts.AddRange(listRaw);
					if (!DAOWorker.HisCashoutDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCashout_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCashout that bai." + LogUtil.TraceData("listData", listData));
                    }
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisCashouts))
            {
                if (!new HisCashoutUpdate(param).UpdateList(this.beforeUpdateHisCashouts))
                {
                    LogSystem.Warn("Rollback du lieu HisCashout that bai, can kiem tra lai." + LogUtil.TraceData("HisCashouts", this.beforeUpdateHisCashouts));
                }
            }
            this.hisTransactionUpdate.RollbackData();
        }
    }
}

using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisSereServBill;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction
{
    partial class HisTransactionLock: BusinessBase
    {
        internal HisTransactionLock()
            : base()
        {

        }

        internal HisTransactionLock(CommonParam paramLock)
            : base(paramLock)
        {

        }

        internal bool BillLock(long id, ref HIS_TRANSACTION resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    HIS_TRANSACTION data = new HisTransactionGet().GetById(id);
                    if (data != null)
                    {
                        if (data.TRANSACTION_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT)
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuKhongHopLe);
                            return false;
                        }
                        if (data.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                            return false;
                        }
                        if (data.IS_CANCEL == Constant.IS_TRUE)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_GiaoDichDaBiHuy);
                            return false;
                        }
                        
                        data.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE;
                        data.UNLOCK_TIME = null;
                        if (data.BEFORE_UL_CASHIER_ROOM_ID != null)
                        {
                            data.CASHIER_ROOM_ID = data.BEFORE_UL_CASHIER_ROOM_ID ?? 0;
                        }
                        if (data.BEFORE_UL_CASHIER_LOGINNAME != null)
                        {
                            data.CASHIER_LOGINNAME = data.BEFORE_UL_CASHIER_LOGINNAME;
                        }
                        if (data.BEFORE_UL_CASHIER_USERNAME != null)
                        {
                            data.CASHIER_USERNAME = data.BEFORE_UL_CASHIER_USERNAME;
                        }
                        result = DAOWorker.HisTransactionDAO.Update(data);

                        string sql = String.Format("UPDATE HIS_SERE_SERV_BILL SET IS_ACTIVE = 0 WHERE BILL_ID = {0}", id);
                        if (!DAOWorker.SqlDAO.Execute(sql))
                        {
                            LogSystem.Error("update HIS_SERE_SERV_BILL that bai");
                            return false;
                        }

                        new EventLogGenerator(EventLog.Enum.HisTransaction_KhoaGiaoDichThanhToan)
                         .TreatmentCode(data.TDL_TREATMENT_CODE)
                         .TransactionCode(data.TRANSACTION_CODE)
                         .Run();

                        resultData = result ? data : null;
                    }
                    else
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
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

        internal bool BillUnlock(TransactionLockSDO sdo, ref HIS_TRANSACTION resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                HisTransactionCheck checker = new HisTransactionCheck(param);
                valid = valid && checker.VerifyRequireFieldLockSDO(sdo);
                valid = valid && this.HasWorkPlaceInfo(sdo.RequestRoomId, ref workPlace);
                if (valid)
                {
                    HIS_TRANSACTION data = new HisTransactionGet().GetById(sdo.TransactionId);
                    if (data != null)
                    {
                        if (data.TRANSACTION_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT)
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuKhongHopLe);
                            return false;
                        }
                        if (data.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDaMoKhoa);
                            return false;
                        }
                        if (data.IS_CANCEL == Constant.IS_TRUE)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_GiaoDichDaBiHuy);
                            return false;
                        }
                        if (data.BEFORE_UL_CASHIER_LOGINNAME == null)
                        {
                            data.BEFORE_UL_CASHIER_LOGINNAME = data.CASHIER_LOGINNAME;
                            data.BEFORE_UL_CASHIER_ROOM_ID = data.CASHIER_ROOM_ID;
                            data.BEFORE_UL_CASHIER_USERNAME = data.CASHIER_USERNAME;
                        }
                        data.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                        data.CASHIER_ROOM_ID = workPlace.CashierRoomId ?? 0;
                        data.CASHIER_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        data.CASHIER_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                        data.UNLOCK_TIME = Inventec.Common.DateTime.Get.Now();
                        result = DAOWorker.HisTransactionDAO.Update(data);

                        string sql = String.Format("UPDATE HIS_SERE_SERV_BILL SET IS_ACTIVE = 1 WHERE BILL_ID = {0}", sdo.TransactionId);
                        if (!DAOWorker.SqlDAO.Execute(sql))
                        {
                            LogSystem.Error("update HIS_SERE_SERV_BILL that bai");
                            return false;
                        }
                        new EventLogGenerator(EventLog.Enum.HisTransaction_MoKhoaGiaoDichThanhToan)
                         .TreatmentCode(data.TDL_TREATMENT_CODE)
                         .TransactionCode(data.TRANSACTION_CODE)
                         .Run();

                        resultData = result ? data : null;

                    }
                    else
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
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

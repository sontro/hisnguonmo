using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Unexport
{
    class HisTransactionProcessor : BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;
        private HisTransactionUpdate hisTransactionUpdate;

        internal HisTransactionProcessor()
            : base()
        {
            this.Init();
        }

        internal HisTransactionProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
            this.hisTransactionUpdate = new HisTransactionUpdate(param);
        }

        internal bool Run(HIS_EXP_MEST expMest)
        {
            try
            {
                if (expMest != null && expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BAN && expMest.BILL_ID.HasValue && HisExpMestCFG.IS_AUTO_CANCEL_TRANSACTION_IN_CASE_OF_UNEXPORT_SALE)
                {
                    HIS_TRANSACTION transaction = new HisTransactionGet().GetById(expMest.BILL_ID.Value);

                    if (transaction != null)
                    {
                        Mapper.CreateMap<HIS_TRANSACTION, HIS_TRANSACTION>();
                        HIS_TRANSACTION before = Mapper.Map<HIS_TRANSACTION>(transaction);

                        string content = MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisExpMest_HuyThucXuat, param.LanguageCode);
                        if (!string.IsNullOrWhiteSpace(content))
                        {
                            content = String.Format(content, expMest.EXP_MEST_CODE);
                        }

                        transaction.CANCEL_REASON = content;
                        transaction.IS_CANCEL = MOS.UTILITY.Constant.IS_TRUE;
                        transaction.CANCEL_CASHIER_ROOM_ID = transaction.CASHIER_ROOM_ID;
                        transaction.CANCEL_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        transaction.CANCEL_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                        transaction.CANCEL_TIME = Inventec.Common.DateTime.Get.Now().Value;

                        if (!this.hisTransactionUpdate.UpdateWithoutCheckLock(transaction, before))
                        {
                            throw new Exception("hisTransactionUpdate. Ket thuc nghiep vu");
                        }

                        string sql = "UPDATE HIS_EXP_MEST SET BILL_ID = NULL, CASHIER_LOGINNAME = NULL, CASHIER_USERNAME = NULL WHERE BILL_ID = :param1";
                        if (!DAOWorker.SqlDAO.Execute(sql, transaction.ID))
                        {
                            throw new Exception("Cap nhat huy giao dich thanh toan cho phieu xuat ban bi that bai. Ma phieu xuat ban:" + expMest.EXP_MEST_CODE + ". Ma giao dich:" + transaction.TRANSACTION_CODE);
                        }

                        new EventLogGenerator(EventLog.Enum.HisTransaction_HuyGiaoDich).TransactionCode(transaction.TRANSACTION_CODE).Run();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }
    }
}

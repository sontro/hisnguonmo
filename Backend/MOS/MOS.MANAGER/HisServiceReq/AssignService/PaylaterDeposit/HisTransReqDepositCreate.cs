using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBranch;
using MOS.MANAGER.HisCard;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisSeseTransReq;
using MOS.MANAGER.HisTransReq;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.AssignService.PaylaterDeposit
{
    class HisTransReqDepositCreate : BusinessBase
    {
        private HisTransReqCreate hisTransReqCreate;
        private HisTransReqUpdate hisTransReqUpdate;
        private HisSeseTransReqCreate hisSeseTransReqCreate;

        internal HisTransReqDepositCreate(CommonParam param)
            : base(param)
        {
            this.hisTransReqCreate = new HisTransReqCreate(param);
            this.hisSeseTransReqCreate = new HisSeseTransReqCreate(param);
            this.hisTransReqUpdate = new HisTransReqUpdate(param);
        }

        internal bool Run(HisTransReqDepositData data, ref HIS_TRANS_REQ resultData)
        {
            bool result = false;
            try
            {
                V_HIS_ACCOUNT_BOOK accountBook = null;
                bool valid = true;
                HisTransReqDepositCheck checker = new HisTransReqDepositCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && checker.Run(data, ref accountBook);

                if (valid)
                {
                    string transReqCode = RequestPaylaterProcessor.Run(data);

                    if (!string.IsNullOrWhiteSpace(transReqCode))
                    {
                        HIS_TRANS_REQ transReq = null;
                        data.TransReqCode = transReqCode;
                        this.ProcessTransReq(data, accountBook, ref transReq);
                        this.ProcessSeseTransReq(data, transReq);
                        result = true;
                        resultData = transReq;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                resultData = null;
                result = false;
            }
            return result;
        }

        private void ProcessTransReq(HisTransReqDepositData data, V_HIS_ACCOUNT_BOOK accountBook, ref HIS_TRANS_REQ transReq)
        {
            if (data != null)
            {
                transReq = new HIS_TRANS_REQ();

                transReq.CASHIER_ROOM_ID = data.CashierRoomId;
                transReq.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU;
                transReq.BILL_TYPE_ID = accountBook.BILL_TYPE_ID;
                transReq.SALE_TYPE_ID = null;
                transReq.TRANS_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__REQUEST;
                transReq.CASHIER_LOGINNAME = data.LoginName;
                transReq.CASHIER_USERNAME = data.UserName;
                HisTransReqUtil.SetTdl(transReq, data.Treatment);
                if (!this.hisTransReqCreate.Create(transReq))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }   
        }

        private void ProcessSeseTransReq(HisTransReqDepositData data, HIS_TRANS_REQ transReq)
        {
            List<HIS_SESE_TRANS_REQ> seseTransReqs = new List<HIS_SESE_TRANS_REQ>();
            if (transReq != null && data.SereServs != null && data.SereServs.Count > 0)
            {
                foreach (V_HIS_SERE_SERV s in data.SereServs)
                {
                    HIS_SESE_TRANS_REQ ss = new HIS_SESE_TRANS_REQ();
                    ss.PRICE = s.VIR_PATIENT_PRICE.Value;
                    ss.TRANS_REQ_ID = transReq.ID;
                    ss.SERE_SERV_ID = s.ID;
                    seseTransReqs.Add(ss);
                }
            }

            if (IsNotNullOrEmpty(seseTransReqs) && !this.hisSeseTransReqCreate.CreateList(seseTransReqs))
            {
                throw new Exception("Cap nhat thong tin TRANS_REQ_ID cho yeu cau dich vu (HIS_SESE_TRANS_REQ) that bai. Du lieu se bi rollback");
            }
        }

        private void Rollback()
        {
            try
            {
                this.hisSeseTransReqCreate.RollbackData();
                this.hisTransReqCreate.RollbackData();
                this.hisTransReqUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}

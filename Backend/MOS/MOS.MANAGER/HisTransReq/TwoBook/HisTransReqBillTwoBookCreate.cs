using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBranch;
using MOS.MANAGER.HisCard;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisSeseTransReq;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransReq.TwoBook
{
    class HisTransReqBillTwoBookCreate : BusinessBase
    {
        private HIS_TRANS_REQ recentRecieptTransReq;
        private HIS_TRANS_REQ recentInvoiceTransReq;

        private HisTransReqCreate hisTransReqCreate;
        private HisSeseTransReqCreate hisSeseTransReqCreate;
        private HisTransReqUpdate hisTransReqUpdate;

        internal HisTransReqBillTwoBookCreate(CommonParam param)
            : base(param)
        {
            this.hisTransReqCreate = new HisTransReqCreate(param);
            this.hisSeseTransReqCreate = new HisSeseTransReqCreate(param);
            this.hisTransReqUpdate = new HisTransReqUpdate(param);
        }

        internal bool Run(HisTransReqBillTwoBookSDO data, ref List<HIS_TRANS_REQ> resultData)
        {
            bool result = false;
            try
            {
                WorkPlaceSDO workPlace = null;
                HIS_TREATMENT treatment = null;
                V_HIS_ACCOUNT_BOOK recieptAccountBook = null;
                V_HIS_ACCOUNT_BOOK invoiceAccountBook = null;
                bool valid = true;
                HisTransReqBillTwoBookCheck checker = new HisTransReqBillTwoBookCheck(param);
                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && checker.Run(data, ref workPlace, ref recieptAccountBook, ref invoiceAccountBook);
                valid = valid && treatChecker.VerifyId(data.TreatmentId, ref treatment);

                if (valid)
                {
                    this.ProcessTransReq(data, workPlace, recieptAccountBook, invoiceAccountBook, treatment);
                    this.ProcessSeseTransReq(data);

                    if ((data.InvoiceTransReq != null && data.InvoiceTransReq.PAY_FORM_ID == IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__KEYPAY)
                        || (data.RecieptTransReq != null && data.RecieptTransReq.PAY_FORM_ID == IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__KEYPAY))
                    {
                        this.ProcessKeyPayPaylater(data, workPlace, treatment);
                    }

                    result = true;
                    this.PassResult(ref resultData);
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

        private void ProcessKeyPayPaylater(HisTransReqBillTwoBookSDO data, WorkPlaceSDO workPlace, HIS_TREATMENT treatment)
        {
            List<HIS_TRANS_REQ> update = new List<HIS_TRANS_REQ>();

            if (String.IsNullOrWhiteSpace(HisTransReqCFG.HASH_KEY))
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransReq_KhongCoThongTinHashKey);
                throw new Exception("Chua cau hinh key ma hoa du lieu. Rollback du lieu");
            }

            HIS_PATIENT patient = new HisPatientGet().GetById(treatment.PATIENT_ID);
            List<HIS_CARD> cards = new HisCardGet().GetByPatientId(treatment.PATIENT_ID);
            HIS_BRANCH branch = new HisBranchGet().GetById(workPlace.BranchId);

            if (!IsNotNullOrEmpty(cards) && (!IsNotNull(patient) || !IsNotNull(patient.REGISTER_CODE)))
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransReq_KhongCoThongTinTheHoacMaMS);
                throw new Exception("Benh nhan khong co thong tin he thong the. Rollback du lieu");
            }

            if (!IsNotNull(branch) || !IsNotNull(branch.THE_BRANCH_CODE))
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransReq_KhongCoThongTinMaCsHtThe);
                throw new Exception("Khong co thong tin Ma co so he thong the. Rollback du lieu");
            }

            decimal totalAmount = 0;

            if (IsNotNull(data.InvoiceTransReq))
            {
                update.Add(data.InvoiceTransReq);
                totalAmount += data.InvoiceTransReq.AMOUNT;
            }

            if (IsNotNull(data.RecieptTransReq))
            {
                update.Add(data.RecieptTransReq);
                totalAmount += data.RecieptTransReq.AMOUNT;
            }

            YTT.SDO.YttPaylaterReqSDO sdo = new YTT.SDO.YttPaylaterReqSDO();
            sdo.ServiceCode = IsNotNullOrEmpty(cards) ? cards.OrderBy(o => o.ID).FirstOrDefault().SERVICE_CODE : "";
            sdo.RegisterCode = patient != null && !String.IsNullOrWhiteSpace(patient.REGISTER_CODE) ? patient.REGISTER_CODE : "";
            sdo.PeopleCode = "";
            sdo.BranchCode = branch != null ? branch.THE_BRANCH_CODE : "";
            sdo.Amount = (long)totalAmount;
            sdo.ClientTrace = Guid.NewGuid().ToString();
            long amount = (long)sdo.Amount;
            string mess = string.Format("{0}|{1}|{2}|{3}|{4}", amount, sdo.ClientTrace, sdo.PeopleCode, sdo.RegisterCode, sdo.ServiceCode);
            sdo.CheckSum = GenerateCheckSum(mess, HisTransReqCFG.HASH_KEY);

            var apiResult = ApiConsumerManager.ApiConsumerStore.YttConsumer.Post<YTT.SDO.YttPaylaterReqSDO>(true, "api/YttPaylaterReq/Request", param, sdo);
            if (IsNotNull(apiResult) && apiResult.ResultCode == "00" && !String.IsNullOrWhiteSpace(apiResult.TransactionCode))
            {
                if (IsNotNull(data.InvoiceTransReq))
                {
                    data.InvoiceTransReq.TIG_TRANSACTION_CODE = apiResult.TransactionCode;
                }

                if (IsNotNull(data.RecieptTransReq))
                {
                    data.RecieptTransReq.TIG_TRANSACTION_CODE = apiResult.TransactionCode;
                }
            }
            else if (IsNotNull(apiResult) && IsNotNull(apiResult.ResultDesc))
            {
                param.Messages.Add(apiResult.ResultDesc);
                Inventec.Common.Logging.LogSystem.Info(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => apiResult), apiResult));
                throw new Exception("Tao du lieu YttPaylaterReq that bai. Rollback du lieu");
            }
            else
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransReq_ThemMoiThatBai);
                Inventec.Common.Logging.LogSystem.Info(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param));
                throw new Exception("Tao du lieu YttPaylaterReq that bai. Rollback du lieu");
            }

            if (!this.hisTransReqUpdate.UpdateList(update))
            {
                if (IsNotNull(data.InvoiceTransReq))
                {
                    Inventec.Common.Logging.LogSystem.Error(string.Format("______________Cap nhat Invoice loi ID: {0}, TIG_TRANSACTION_CODE: {1} ", data.InvoiceTransReq.ID, data.InvoiceTransReq.TIG_TRANSACTION_CODE));
                }
                if (IsNotNull(data.RecieptTransReq))
                {
                    Inventec.Common.Logging.LogSystem.Error(string.Format("______________Cap nhat Reciept loi ID: {0}, TIG_TRANSACTION_CODE: {1} ", data.RecieptTransReq.ID, data.RecieptTransReq.TIG_TRANSACTION_CODE));
                }
            }
        }

        private static string GenerateCheckSum(string message, string key)
        {
            LogSystem.Info("Data before hash: " + message);
            string result = String.Empty;
            System.Text.UTF8Encoding enconding = new UTF8Encoding();
            byte[] keyBytes = enconding.GetBytes(key);
            byte[] messageBytes = enconding.GetBytes(message);
            using (HMACSHA256 sha256 = new HMACSHA256(keyBytes))
            {
                byte[] hashBytes = sha256.ComputeHash(messageBytes);
                result = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
            LogSystem.Info("Data after hash: " + result); return result;
        }

        private void ProcessTransReq(HisTransReqBillTwoBookSDO data, WorkPlaceSDO workPlace, V_HIS_ACCOUNT_BOOK recieptAccountBook, V_HIS_ACCOUNT_BOOK invoiceAccountBook, HIS_TREATMENT treatment)
        {
            List<HIS_TRANS_REQ> creates = new List<HIS_TRANS_REQ>();
            string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            string username = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
            if (data.RecieptTransReq != null)
            {
                data.RecieptTransReq.CASHIER_ROOM_ID = workPlace.CashierRoomId.Value;
                data.RecieptTransReq.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT;
                data.RecieptTransReq.BILL_TYPE_ID = recieptAccountBook.BILL_TYPE_ID;
                data.RecieptTransReq.SALE_TYPE_ID = null;
                data.RecieptTransReq.TRANS_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__REQUEST;
                data.RecieptTransReq.CASHIER_LOGINNAME = loginname;
                data.RecieptTransReq.CASHIER_USERNAME = username;
                data.RecieptTransReq.TIG_TRANSACTION_CODE = data.RecieptTransReq.TRANS_REQ_CODE;
                HisTransReqUtil.SetTdl(data.RecieptTransReq, treatment);
                creates.Add(data.RecieptTransReq);
            }
            if (data.InvoiceTransReq != null)
            {
                data.InvoiceTransReq.CASHIER_ROOM_ID = workPlace.CashierRoomId.Value;
                data.InvoiceTransReq.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT;
                data.InvoiceTransReq.BILL_TYPE_ID = invoiceAccountBook.BILL_TYPE_ID;
                data.InvoiceTransReq.SALE_TYPE_ID = null;
                data.InvoiceTransReq.TRANS_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__REQUEST;
                data.InvoiceTransReq.CASHIER_LOGINNAME = loginname;
                data.InvoiceTransReq.CASHIER_USERNAME = username;
                data.InvoiceTransReq.TIG_TRANSACTION_CODE = data.InvoiceTransReq.TRANS_REQ_CODE;
                HisTransReqUtil.SetTdl(data.InvoiceTransReq, treatment);
                creates.Add(data.InvoiceTransReq);
            }

            if (!this.hisTransReqCreate.CreateList(creates))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentRecieptTransReq = data.RecieptTransReq;
            this.recentInvoiceTransReq = data.InvoiceTransReq;
        }

        private void ProcessSeseTransReq(HisTransReqBillTwoBookSDO data)
        {
            List<HIS_SESE_TRANS_REQ> seseTransReqs = new List<HIS_SESE_TRANS_REQ>();
            if (this.recentRecieptTransReq != null)
            {
                data.RecieptSeseTransReqs.ForEach(o =>
                {
                    o.TRANS_REQ_ID = this.recentRecieptTransReq.ID;
                });
                seseTransReqs.AddRange(data.RecieptSeseTransReqs);
            }
            if (this.recentInvoiceTransReq != null)
            {
                data.InvoiceSeseTransReqs.ForEach(o =>
                {
                    o.TRANS_REQ_ID = this.recentInvoiceTransReq.ID;
                });
                seseTransReqs.AddRange(data.InvoiceSeseTransReqs);
            }

            if (IsNotNullOrEmpty(seseTransReqs) && !this.hisSeseTransReqCreate.CreateList(seseTransReqs))
            {
                throw new Exception("Cap nhat thong tin TRANS_REQ_ID cho yeu cau dich vu (HIS_SESE_TRANS_REQ) that bai. Du lieu se bi rollback");
            }
        }

        private void PassResult(ref List<HIS_TRANS_REQ> resultData)
        {
            resultData = new List<HIS_TRANS_REQ>();
            if (this.recentRecieptTransReq != null)
                resultData.Add(this.recentRecieptTransReq);
            if (this.recentInvoiceTransReq != null)
                resultData.Add(this.recentInvoiceTransReq);
        }

        private void Rollback()
        {
            try
            {
                this.hisSeseTransReqCreate.RollbackData();
                this.hisTransReqCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}

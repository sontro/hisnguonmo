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
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransReq.Bill
{
    class HisTransReqBillCreate : BusinessBase
    {
        private HIS_TRANS_REQ transReq;
        private HisTransReqCreate hisTransReqCreate;
        private HisTransReqUpdate hisTransReqUpdate;
        private HisSeseTransReqCreate hisSeseTransReqCreate;

        internal HisTransReqBillCreate(CommonParam param)
            : base(param)
        {
            this.hisTransReqCreate = new HisTransReqCreate(param);
            this.hisSeseTransReqCreate = new HisSeseTransReqCreate(param);
            this.hisTransReqUpdate = new HisTransReqUpdate(param);
        }

        internal bool Run(HisTransReqBillSDO data, ref HIS_TRANS_REQ resultData)
        {
            bool result = false;
            try
            {
                V_HIS_CASHIER_ROOM cashierRoom = null;
                HIS_TREATMENT treatment = null;
                V_HIS_ACCOUNT_BOOK accountBook = null;
                bool valid = true;
                HisTransReqBillCheck checker = new HisTransReqBillCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && checker.Run(data, ref cashierRoom, ref accountBook);
                valid = valid && treatmentChecker.VerifyId(data.TransReq.TREATMENT_ID, ref treatment);

                if (valid)
                {
                    this.ProcessTransReq(data, cashierRoom, accountBook, treatment);
                    this.ProcessSeseTransReq(data);

                    if (data.TransReq.PAY_FORM_ID == IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__KEYPAY)
                    {
                        this.ProcessKeyPayPaylater(data, cashierRoom);
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

        private void ProcessKeyPayPaylater(HisTransReqBillSDO data, V_HIS_CASHIER_ROOM cashierRoom)
        {
            if (String.IsNullOrWhiteSpace(HisTransReqCFG.HASH_KEY))
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransReq_KhongCoThongTinHashKey);
                throw new Exception("Chua cau hinh key ma hoa du lieu. Rollback du lieu");
            }

            HIS_PATIENT patient = new HisPatientGet().GetById(data.TransReq.TDL_PATIENT_ID ?? 0);
            List<HIS_CARD> cards = new HisCardGet().GetByPatientId(data.TransReq.TDL_PATIENT_ID ?? 0);
            HIS_BRANCH branch = new HisBranchGet().GetById(cashierRoom.BRANCH_ID);

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

            YTT.SDO.YttPaylaterReqSDO sdo = new YTT.SDO.YttPaylaterReqSDO();
            sdo.ServiceCode = IsNotNullOrEmpty(cards) ? cards.Where(o => o.IS_ACTIVE == Constant.IS_TRUE).OrderByDescending(o => o.ID).FirstOrDefault().SERVICE_CODE : "";
            sdo.RegisterCode = patient != null && !String.IsNullOrWhiteSpace(patient.REGISTER_CODE) ? patient.REGISTER_CODE : "";
            sdo.PeopleCode = "";
            sdo.BranchCode = branch != null ? branch.THE_BRANCH_CODE : "";
            sdo.Amount = (long)data.TransReq.AMOUNT;
            sdo.ClientTrace = Guid.NewGuid().ToString();
            long amount = (long)sdo.Amount;
            string mess = string.Format("{0}|{1}|{2}|{3}|{4}", amount, sdo.ClientTrace, sdo.PeopleCode, sdo.RegisterCode, sdo.ServiceCode);
            sdo.CheckSum = GenerateCheckSum(mess, HisTransReqCFG.HASH_KEY);

            var apiResult = ApiConsumerManager.ApiConsumerStore.YttConsumer.Post<YTT.SDO.YttPaylaterReqSDO>(true, "api/YttPaylaterReq/Request", param, sdo);
            if (IsNotNull(apiResult) && apiResult.ResultCode == "00" && !String.IsNullOrWhiteSpace(apiResult.TransactionCode))
            {
                data.TransReq.TIG_TRANSACTION_CODE = apiResult.TransactionCode;
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

            if (!this.hisTransReqUpdate.Update(data.TransReq))
            {
                Inventec.Common.Logging.LogSystem.Error(string.Format("______________Cap nhat TransReq loi ID: {0}, TIG_TRANSACTION_CODE: {1} ", data.TransReq.ID, data.TransReq.TIG_TRANSACTION_CODE));
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

        private void ProcessTransReq(HisTransReqBillSDO data, V_HIS_CASHIER_ROOM cashierRoom, V_HIS_ACCOUNT_BOOK accountBook, HIS_TREATMENT treatment)
        {
            string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            string username = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
            if (data.TransReq != null)
            {
                data.TransReq.CASHIER_ROOM_ID = cashierRoom.ID;
                data.TransReq.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT;
                data.TransReq.BILL_TYPE_ID = accountBook.BILL_TYPE_ID;
                data.TransReq.SALE_TYPE_ID = null;
                data.TransReq.TRANS_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__REQUEST;
                data.TransReq.CASHIER_LOGINNAME = loginname;
                data.TransReq.CASHIER_USERNAME = username;
                data.TransReq.TIG_TRANSACTION_CODE = data.TransReq.TRANS_REQ_CODE;
                HisTransReqUtil.SetTdl(data.TransReq, treatment);
            }

            if (!this.hisTransReqCreate.Create(data.TransReq))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.transReq = data.TransReq;
        }

        private void ProcessSeseTransReq(HisTransReqBillSDO data)
        {
            List<HIS_SESE_TRANS_REQ> seseTransReqs = new List<HIS_SESE_TRANS_REQ>();
            if (this.transReq != null)
            {
                data.SeseTransReqs.ForEach(o =>
                {
                    o.TRANS_REQ_ID = this.transReq.ID;
                });
                seseTransReqs.AddRange(data.SeseTransReqs);
            }

            if (IsNotNullOrEmpty(seseTransReqs) && !this.hisSeseTransReqCreate.CreateList(seseTransReqs))
            {
                throw new Exception("Cap nhat thong tin TRANS_REQ_ID cho yeu cau dich vu (HIS_SESE_TRANS_REQ) that bai. Du lieu se bi rollback");
            }
        }

        private void PassResult(ref HIS_TRANS_REQ resultData)
        {
            resultData = new HIS_TRANS_REQ();
            if (this.transReq != null)
                resultData = this.transReq;
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

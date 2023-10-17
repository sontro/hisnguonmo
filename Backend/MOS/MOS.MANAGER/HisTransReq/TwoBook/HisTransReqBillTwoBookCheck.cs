using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisAccountBook;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransReq.TwoBook
{
    class HisTransReqBillTwoBookCheck : BusinessBase
    {
        internal HisTransReqBillTwoBookCheck()
            : base()
        {

        }

        internal HisTransReqBillTwoBookCheck(CommonParam param)
            : base(param)
        {

        }


        internal bool Run(HisTransReqBillTwoBookSDO data, ref WorkPlaceSDO workPlace, ref V_HIS_ACCOUNT_BOOK recieptAccountBook, ref V_HIS_ACCOUNT_BOOK invoiceAccountBook)
        {
            bool valid = true;
            try
            {
                HIS_TREATMENT treatment = null;
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && (IsNotNull(data.RecieptTransReq) || IsNotNull(data.InvoiceTransReq));
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && this.IsCashierRoom(workPlace);
                valid = valid && treatmentChecker.VerifyId(data.TreatmentId, ref treatment);
                if (valid && IsNotNull(data.RecieptTransReq))
                {
                    data.RecieptTransReq.TREATMENT_ID = data.TreatmentId;
                    valid = valid && this.IsUnlockAccountBook(data.RecieptTransReq.ACCOUNT_BOOK_ID, ref recieptAccountBook);
                    valid = valid && this.IsGeneralOrder(recieptAccountBook);
                    if (data.RecieptTransReq.PAY_FORM_ID != IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__KEYPAY)
                    {
                        valid = valid && IsNotNull(data.RecieptTransReq.TRANS_REQ_CODE);
                    }
                }
                if (valid && IsNotNull(data.InvoiceTransReq))
                {
                    data.InvoiceTransReq.TREATMENT_ID = data.TreatmentId;
                    valid = valid && this.IsUnlockAccountBook(data.InvoiceTransReq.ACCOUNT_BOOK_ID, ref invoiceAccountBook);
                    valid = valid && this.IsGeneralOrder(invoiceAccountBook);
                    if (data.InvoiceTransReq.PAY_FORM_ID != IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__KEYPAY)
                    {
                        valid = valid && IsNotNull(data.InvoiceTransReq.TRANS_REQ_CODE);
                    }
                }

                if (IsNotNull(data.RecieptTransReq) && IsNotNull(data.InvoiceTransReq))
                {
                    valid = valid && data.InvoiceTransReq.PAY_FORM_ID == data.InvoiceTransReq.PAY_FORM_ID;
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

        internal bool IsUnlockAccountBook(long accountBookId, ref V_HIS_ACCOUNT_BOOK accountBook)
        {
            bool valid = true;
            try
            {
                accountBook = new HisAccountBookGet().GetViewById(accountBookId);

                if (accountBook == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("So thu chi khong ton tai tren he thong. accountBookId: " + accountBookId);
                }

                if (accountBook.IS_ACTIVE == null || accountBook.IS_ACTIVE.Value != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_SoDangBiKhoa, accountBook.ACCOUNT_BOOK_NAME);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        public bool IsCashierRoom(WorkPlaceSDO workPlace)
        {
            try
            {
                if (workPlace == null || !workPlace.CashierRoomId.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.BanKhongLamViecTaiPhongThuNgan);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return false;
        }

        bool IsGeneralOrder(V_HIS_ACCOUNT_BOOK accountBook)
        {
            bool valid = true;
            try
            {
                if (accountBook.IS_NOT_GEN_TRANSACTION_ORDER.HasValue && accountBook.IS_NOT_GEN_TRANSACTION_ORDER.Value == Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_SoBienLaiKhongTuDongTangSo, accountBook.ACCOUNT_BOOK_NAME);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}

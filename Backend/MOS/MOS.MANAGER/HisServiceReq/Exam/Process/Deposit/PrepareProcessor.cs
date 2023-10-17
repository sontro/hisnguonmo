using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisAccountBook;
using MOS.MANAGER.HisCaroAccountBook;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisUserAccountBook;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Exam.Process.Deposit
{
    class PrepareProcessor : BusinessBase
    {
        internal PrepareProcessor(CommonParam param)
            : base(param)
        {
        }

        internal bool IsAllowDeposit(long requestRoomId, HIS_TREATMENT treatment, HIS_SERE_SERV sereServ, ref long? cashierRoomId, ref long? accountBookId, ref HIS_CARD hisCard, ref string theBranchCode)
        {
            try
            {
                //Neu khong bat cau hinh thi khong xu ly
                if (!EpaymentCFG.IS_USING_EXECUTE_ROOM_PAYMENT)
                {
                    return false;
                }

                //Chi xu ly tam ung voi doi tuong khong phai BHYT va so tien BN chi tra > 0
                if (sereServ.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT || sereServ.VIR_TOTAL_PATIENT_PRICE <= 0)
                {
                    return false;
                }
                
                //Kiem tra xem phong chi dinh da duoc cau hinh phong thu ngan tuong ung chua
                V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == requestRoomId).FirstOrDefault();
                if (!room.DEFAULT_CASHIER_ROOM_ID.HasValue)
                {
                    LogSystem.Warn("Phong " + room.ROOM_CODE + " chua cau hinh phong thu ngan, khong the thuc hien tam thu dich vu tu dong");
                    return false;
                }

                cashierRoomId = room.DEFAULT_CASHIER_ROOM_ID;
                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();

                //Kiem tra xem tai khoan nguoi dung da duoc gan so tam ung nao chua
                HisUserAccountBookViewFilterQuery filter = new HisUserAccountBookViewFilterQuery();
                filter.LOGINNAME__EXACT = loginName;
                filter.IS_ACTIVE = Constant.IS_TRUE;
                filter.ACCOUNT_BOOK_IS_ACTIVE = Constant.IS_TRUE;
                filter.FOR_DEPOSIT = true;
                filter.IS_OUT_OF_BILL = false;

                List<V_HIS_USER_ACCOUNT_BOOK> userAccountBooks = new HisUserAccountBookGet().GetView(filter);
                if (IsNotNullOrEmpty(userAccountBooks))
                {
                    accountBookId = userAccountBooks.OrderByDescending(o => o.ID).Select(o => o.ACCOUNT_BOOK_ID).FirstOrDefault();
                }
                else
                {
                    HisCaroAccountBookViewFilterQuery f = new HisCaroAccountBookViewFilterQuery();
                    f.CASHIER_ROOM_ID = room.DEFAULT_CASHIER_ROOM_ID.Value;
                    f.FOR_DEPOSIT = true;
                    f.IS_OUT_OF_BILL = false;
                    f.ACCOUNT_BOOK_IS_ACTIVE = true;
                    List<V_HIS_CARO_ACCOUNT_BOOK> caroAccountBook = new HisCaroAccountBookGet().GetView(f);

                    if (IsNotNullOrEmpty(caroAccountBook))
                    {
                        accountBookId = caroAccountBook.OrderByDescending(o => o.ID).Select(o => o.ACCOUNT_BOOK_ID).FirstOrDefault();
                    }
                }

                if (!accountBookId.HasValue)
                {
                    LogSystem.Warn("Tai khoan " + loginName + " va phong thu ngan tuong ung chua duoc gan so tam ung");
                    return false;
                }

                //Lay so du cua tai khoan the cua BN
                decimal? balance = new HisPatientBalance().GetCardBalance(treatment.PATIENT_ID, ref theBranchCode, ref hisCard);
                if (!balance.HasValue || balance <= 0 || hisCard == null || string.IsNullOrWhiteSpace(hisCard.SERVICE_CODE))
                {
                    return false;
                }

                //Neu so du ko du thi bo qua
                if (sereServ.VIR_TOTAL_PATIENT_PRICE > balance)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
        }
    }
}

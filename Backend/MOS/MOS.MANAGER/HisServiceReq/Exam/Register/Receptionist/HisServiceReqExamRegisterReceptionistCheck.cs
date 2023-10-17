using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.Token;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using MOS.UTILITY;
using System.Threading;
using MOS.MANAGER.OldSystemIntegrate;
using AutoMapper;
using MOS.MANAGER.HisAccountBook.Authority;
using MOS.MANAGER.HisTreatment;

namespace MOS.MANAGER.HisServiceReq.Exam.Register.Receptionist
{
    class HisServiceReqExamRegisterReceptionistCheck : BusinessBase
    {
        internal HisServiceReqExamRegisterReceptionistCheck()
            : base()
        {
        }

        internal HisServiceReqExamRegisterReceptionistCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool IsValidData(HisServiceReqExamRegisterSDO data, ref bool isAuthorizedTransaction, ref V_HIS_CASHIER_ROOM cashierRoom)
        {
            bool valid = true;
            try
            {
                if ((data.IsAutoCreateBillForNonBhyt || data.IsAutoCreateDepositForNonBhyt)
                    && IsNotNullOrEmpty(data.ServiceReqDetails)
                    && data.IsNotRequireFee != Constant.IS_TRUE
                    && data.ServiceReqDetails.Exists(o => o.PatientTypeId != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT))
                {
                    if (!data.CashierWorkingRoomId.HasValue
                        || !data.AccountBookId.HasValue
                        || string.IsNullOrWhiteSpace(data.CashierLoginName))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("Thieu thong tin so bien lai/hoa don");
                        return false;
                    }

                    if (data.IsAutoCreateBillForNonBhyt && data.IsAutoCreateDepositForNonBhyt)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("IsAutoCreateBillForNonBhyt && IsAutoCreateDepositForNonBhyt");
                        return false;
                    }

                    V_HIS_CASHIER_ROOM room = HisCashierRoomCFG.DATA.Where(o => o.ROOM_ID == data.CashierWorkingRoomId.Value).FirstOrDefault();

                    if (room == null)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("CashierWorkingRoomId khong hop le");
                        return false;
                    }

                    if (data.IsAutoCreateBillForNonBhyt)
                    {
                        isAuthorizedTransaction = new HisAccountBookAuthorityProcessor(param).IsAuthorized(data.CashierLoginName, data.CashierUserName, data.CashierWorkingRoomId.Value, data.AccountBookId.Value, data.RequestRoomId);
                        if (!isAuthorizedTransaction)
                        {
                            return false;
                        }
                    }

                    if (data.IsAutoCreateDepositForNonBhyt)
                    {
                        WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(room.ROOM_ID);

                        //Neu ko phai lam viec tai phong thu ngan thi kiem tra xem co dang duoc uy quyen su dung so hoa don bien lai boi 1 thu ngan khac ko
                        if (workPlace == null)
                        {
                            isAuthorizedTransaction = new HisAccountBookAuthorityProcessor(param).IsAuthorized(data.CashierLoginName, data.CashierUserName, data.CashierWorkingRoomId.Value, data.AccountBookId.Value, data.RequestRoomId);
                            
                            if (!isAuthorizedTransaction)
                            {
                                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.KhongCoThongTinPhongLamViecThuNgan);
                                return false;
                            }
                        }
                    }

                    cashierRoom = room;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}

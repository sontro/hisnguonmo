using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServDebt;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisServiceReq.AssignService;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.ServicePaty;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceChangeReq.CashierApprove
{
    class HisServiceChangeReqCashierApproveCheck : BusinessBase
    {
        internal HisServiceChangeReqCashierApproveCheck()
            : base()
        {

        }

        internal HisServiceChangeReqCashierApproveCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool IsValidData(HisServiceChangeReqCashierApproveSDO data, ref HIS_SERVICE_CHANGE_REQ serviceChangeReq, ref HIS_SERVICE_REQ serviceReq, ref HIS_SERE_SERV oldSereServ, ref HIS_SERE_SERV newSereServ, ref HIS_SERE_SERV_DEPOSIT sereServDeposit)
        {
            try
            {
                HIS_SERVICE_CHANGE_REQ scr = new HisServiceChangeReqGet().GetById(data.ServiceChangeReqId);

                if (scr == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("data.ServiceChangeReqId ko hop le");
                    return false;
                }

                if (scr.APPROVAL_CASHIER_LOGINNAME != null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceChangeReq_YeuCauDoiDichVuDaDuocDuyetSuaPhieuThu);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(scr.APPROVAL_LOGINNAME) || !scr.ALTER_SERE_SERV_ID.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceChangeReq_YeuCauDoiDichVuChuaDuocDuyetSuaChiDinh);
                    return false;
                }

                oldSereServ = new HisSereServGet().GetById(scr.SERE_SERV_ID);
                serviceReq = new HisServiceReqGet().GetById(oldSereServ.SERVICE_REQ_ID.Value);
                newSereServ = new HisSereServGet().GetById(scr.ALTER_SERE_SERV_ID.Value);

                
                if (data.SereServDepositId.HasValue)
                {
                    sereServDeposit = new HisSereServDepositGet().GetById(data.SereServDepositId.Value);

                    if (sereServDeposit != null && sereServDeposit.SERE_SERV_ID != oldSereServ.ID)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("sereServDeposit.SERE_SERV_ID khong khop voi oldSereServ.ID");
                        return false;
                    }

                    //Kiem tra du lieu xem da bi huy hoac khoa chua
                    if (sereServDeposit.IS_CANCEL == MOS.UTILITY.Constant.IS_TRUE
                        || sereServDeposit.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisRepay_GiaoDichCuaCacDichVuSauDaBiHuyHoacKhoa, sereServDeposit.TDL_SERVICE_NAME);
                        return false;
                    }

                    //Kiem tra du lieu xem co ban ghi nao da co thong tin repay hay chua
                    List<HIS_SESE_DEPO_REPAY> ssDepoRepays = new HisSeseDepoRepayGet().GetNoCancelBySereServDepositId(data.SereServDepositId.Value);
                    if (IsNotNullOrEmpty(ssDepoRepays))
                    {
                        string serviceNames = string.Join(",", ssDepoRepays.Select(s => s.TDL_SERVICE_NAME).ToList());
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisRepay_CacDichVuDaHoanUngKhongChoHoanUngLai, serviceNames);
                        return false;
                    }

                    if (sereServDeposit.AMOUNT != data.RepayAmount)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("So tien can hoan ung khac voi so tien giao dich. sereServDeposit.AMOUNT: " + sereServDeposit.AMOUNT);
                        return false;
                    }

                    if (!data.RepayAccountBookId.HasValue || !data.RepayPayFormId.HasValue || !data.RepayAmount.HasValue)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("Neu hoan ung thi phai truyen RepayPayFormId, data.RepayAmount va RepayAccountBookId");
                        return false;
                    }
                }

                List<HIS_SERE_SERV_DEBT> existsDebts = new HisSereServDebtGet().GetNoCancelBySereServId(data.NewSereServId);
                if (IsNotNullOrEmpty(existsDebts))
                {
                    List<string> names = existsDebts.Select(o => o.TDL_SERVICE_NAME).Distinct().ToList();
                    string nameStr = string.Join(",", names);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_DichVuDaChotNo, nameStr);
                    return false;
                }

                if (newSereServ.VIR_TOTAL_PATIENT_PRICE != data.DepositAmount)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("So tien can tam ung khac voi so tien giao dich. newSereServ.VIR_TOTAL_PATIENT_PRICE: " + newSereServ.VIR_TOTAL_PATIENT_PRICE);
                    return false;
                }

                serviceChangeReq = scr;
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal bool IsValidWorkingRoom(WorkPlaceSDO workPlaceSdo)
        {
            try
            {
                if (!workPlaceSdo.CashierRoomId.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.BanKhongLamViecTaiPhongThuNgan);
                    return false;
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

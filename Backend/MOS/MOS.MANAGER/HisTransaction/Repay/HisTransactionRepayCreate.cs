using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServ.Update.PayslipInfo;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatment.Lock;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMest.Common.ApproveNotTaken;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTransaction.Repay
{
    partial class HisTransactionRepayCreate : BusinessBase
    {
        private HisTreatmentLock hisTreatmentLock;
        private HisTransactionCreate hisTransactionCreate;
        private HisSeseDepoRepayCreate hisSeseDepoRepayCreate;
        private HisSereServUpdatePayslipInfo hisSereServUpdatePayslipInfo;

        private HIS_TRANSACTION recentHisTransaction;

        internal HisTransactionRepayCreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisTransactionCreate = new HisTransactionCreate(param);
            this.hisSeseDepoRepayCreate = new HisSeseDepoRepayCreate(param);
            this.hisSereServUpdatePayslipInfo = new HisSereServUpdatePayslipInfo(param);
            this.hisTreatmentLock = new HisTreatmentLock(param);
        }

        public bool CreateRepay(HisTransactionRepaySDO data, ref V_HIS_TRANSACTION resultData)
        {
            bool result = false;
            try
            {
                this.SetServerTime(data);

                List<HIS_SERE_SERV_DEPOSIT> sereServDeposits = null;
                List<HIS_SERE_SERV> allExists = null;
                List<HIS_SERE_SERV> changes = null;
                V_HIS_ACCOUNT_BOOK hisAccountBook = null;
                HIS_TREATMENT treatment = null;
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                WorkPlaceSDO workPlace = null;
                bool valid = true;
                HisTransactionCheck checker = new HisTransactionCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.Transaction);
                valid = valid && checker.IsValidAmountRepay(data.Transaction, data.SereServDepositIds != null);
                valid = valid && (!data.Transaction.TREATMENT_ID.HasValue || treatmentChecker.VerifyId(data.Transaction.TREATMENT_ID.Value, ref treatment));
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && this.IsValidDepoRepay(data, ref sereServDeposits);
                valid = valid && this.ValidateInCaseOfUpdateExecute(data.Transaction.TREATMENT_ID.Value, sereServDeposits, treatment, ref allExists, ref changes);

                valid = valid && checker.HasNotFinancePeriod(data.Transaction);
                valid = valid && checker.IsUnlockAccountBook(data.Transaction.ACCOUNT_BOOK_ID, ref hisAccountBook);
                valid = valid && checker.IsValidNumOrder(data.Transaction, hisAccountBook);

                if (valid)
                {
                    if (!workPlace.CashierRoomId.HasValue)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.BanKhongLamViecTaiPhongThuNgan);
                        return false;
                    }

                    data.Transaction.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__HU;
                    data.Transaction.CASHIER_ROOM_ID = workPlace.CashierRoomId.Value;

                    this.ProcessTransactionRepay(data, treatment);
                    this.ProcessSeseDepoRepay(sereServDeposits);
                    this.ProcessTreatment(data);
                    this.ProcessSereServ(allExists, changes, treatment);//xu ly cuoi cung de tranh rollback sere_serv
                    this.ProcessSSThuocOrVatTu(changes);
                    this.PassResult(ref resultData);
                    result = true;
                    new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisTransaction_TaoGiaoDichHoanUng, this.recentHisTransaction.AMOUNT).TreatmentCode(treatment.TREATMENT_CODE).TransactionCode(this.recentHisTransaction.TRANSACTION_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
            }
            return result;
        }

        private void ProcessSSThuocOrVatTu(List<HIS_SERE_SERV> changes)
        {
            if (IsNotNullOrEmpty(changes))
            {
                List<HIS_SERE_SERV> ssThuocOrVT = changes
                    .Where(o => 
                        o.SERVICE_REQ_ID.HasValue
                        && (o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC || o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT)
                        ).ToList();
                if (HisSereServCFG.AUTO_SET_NO_EXECUTE_FOR_REPAY == HisSereServCFG.SetExpendForAutoSetNoExecuteForRepayOption.NO_EXECUTE_FOR_MAU && IsNotNullOrEmpty(ssThuocOrVT))
                {
                    HisExpMestFilterQuery filter = new HisExpMestFilterQuery();
                    filter.SERVICE_REQ_IDs = ssThuocOrVT.Select(o => o.SERVICE_REQ_ID.Value).ToList();
                    filter.HAS_IS_NOT_TAKEN = false;
                    List<HIS_EXP_MEST> exps = new HisExpMestGet().Get(filter);

                    if (IsNotNullOrEmpty(exps))
                    {
                        foreach (var exp in exps)
                        {
                            CommonParam paramCommon = new CommonParam();
                            ExpMestProcessor expMestProcessor = new ExpMestProcessor(paramCommon);
                            if (!expMestProcessor.Run(exp))
                            {
                                string desc = "";
                                if (IsNotNullOrEmpty(paramCommon.BugCodes))
                                {
                                    desc += paramCommon.GetBugCode();
                                }
                                if (IsNotNullOrEmpty(paramCommon.Messages))
                                {
                                    desc += paramCommon.GetMessage();
                                }
                                string queryTakenFall = string.Format("UPDATE HIS_EXP_MEST SET IS_NOT_TAKEN_FAIL = 1, NOT_TAKEN_DESC = '{0}' WHERE ID = {1}", desc, exp.ID);
                                if (!DAOWorker.SqlDAO.Execute(queryTakenFall))
                                {
                                    LogSystem.Warn("Cap nhat IS_NOT_TAKEN_FAIL that bai");
                                }
                                LogSystem.Warn("Khong set Not Taken duoc cho ExpMestCode: " + exp.EXP_MEST_CODE + LogUtil.TraceData("ExpMest", exp));
                            }
                        }
                    }
                }
            }
        }

        private void ProcessTransactionRepay(HisTransactionRepaySDO data, HIS_TREATMENT treatment)
        {
            if (IsNotNullOrEmpty(data.SereServDepositIds))
            {
                data.Transaction.TDL_SESE_DEPO_REPAY_COUNT = (long)data.SereServDepositIds.Count;
            }
            HisTransactionUtil.SetTreatmentFeeInfo(data.Transaction);

            if (!this.hisTransactionCreate.Create(data.Transaction, treatment))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentHisTransaction = data.Transaction;
        }

        private void ProcessSereServ(List<HIS_SERE_SERV> allExists, List<HIS_SERE_SERV> toChanges, HIS_TREATMENT treatment)
        {
            if ((HisSereServCFG.AUTO_SET_NO_EXECUTE_FOR_REPAY == HisSereServCFG.SetExpendForAutoSetNoExecuteForRepayOption.NO_EXECUTE_FOR_TH_VT_AND_MAU || HisSereServCFG.AUTO_SET_NO_EXECUTE_FOR_REPAY == HisSereServCFG.SetExpendForAutoSetNoExecuteForRepayOption.NO_EXECUTE_FOR_MAU)
                && IsNotNullOrEmpty(toChanges)
               )
            {
                toChanges.ForEach(o => o.IS_NO_EXECUTE = Constant.IS_TRUE);

                HisSereServPayslipSDO sdo = new HisSereServPayslipSDO();
                sdo.SereServs = toChanges;
                sdo.TreatmentId = treatment.ID;
                sdo.Field = UpdateField.IS_NO_EXECUTE;
                List<HIS_SERE_SERV> resultData = null;

                List<HIS_SERVICE_REQ> affectedServiceReqs = toChanges != null ? new HisServiceReqGet().GetByIds(toChanges.Select(o => o.SERVICE_REQ_ID.Value).ToList()) : null;

                if (!this.hisSereServUpdatePayslipInfo.RunWithoutChecking(allExists, affectedServiceReqs, sdo, treatment, null, ref resultData))
                {
                    throw new Exception("Tu dong thiet lap 'ko thuc hien dich vu (is_no_execute) that bai. Rollback du lieu");
                }
            }
        }

        /// <summary>
        /// Neu co truyen len chi tiet cac dich vu can hoan ung
        /// </summary>
        /// <param name="data"></param>
        private void ProcessSeseDepoRepay(List<HIS_SERE_SERV_DEPOSIT> sereServDeposits)
        {
            if (IsNotNullOrEmpty(sereServDeposits))
            {
                List<HIS_SESE_DEPO_REPAY> toInsertes = new List<HIS_SESE_DEPO_REPAY>();
                foreach (HIS_SERE_SERV_DEPOSIT d in sereServDeposits)
                {
                    HIS_SESE_DEPO_REPAY detail = new HIS_SESE_DEPO_REPAY();
                    detail.REPAY_ID = this.recentHisTransaction.ID;
                    detail.AMOUNT = d.AMOUNT;
                    detail.SERE_SERV_DEPOSIT_ID = d.ID;
                    HisSeseDepoRepayUtil.SetTdl(detail, d);
                    toInsertes.Add(detail);
                }

                if (!this.hisSeseDepoRepayCreate.CreateList(toInsertes))
                {
                    throw new Exception("Xu ly that bai. Rollback du lieu");
                }
            }
        }

        private void ProcessTreatment(HisTransactionRepaySDO data)
        {
            if (HisTreatmentCFG.AUTO_LOCK_AFTER_REPAY && data.Transaction != null && data.Transaction.TREATMENT_ID.HasValue)
            {
                V_HIS_TREATMENT_FEE_1 treatmentFee = new HisTreatmentGet(param).GetFeeView1ById(data.Transaction.TREATMENT_ID.Value);

                //Neu benh nhan da ket thuc dieu tri va chua duyet khoa tai chinh
                if (treatmentFee != null && treatmentFee.IS_PAUSE == MOS.UTILITY.Constant.IS_TRUE
                    && treatmentFee.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                {
                    //So tien can thu them
                    decimal? unpaid = new HisTreatmentGet().GetUnpaid(treatmentFee);

                    //Neu so tien can thu them = 0 thi tu dong duyet khoa
                    if (unpaid == 0)
                    {
                        HIS_TREATMENT treatment = new HIS_TREATMENT();

                        HisTreatmentLockSDO sdo = new HisTreatmentLockSDO();
                        sdo.RequestRoomId = data.RequestRoomId;
                        sdo.TreatmentId = treatmentFee.ID;

                        //Trong truong hop co cau hinh lay gio duyet khoa vien phi theo gio ra vien trong truong hop tu dong
                        if (HisTreatmentCFG.IS_USING_OUT_TIME_FOR_FEE_LOCK_TIME_IN_CASE_OF_AUTO
                            && treatmentFee.OUT_TIME.HasValue)
                        {
                            sdo.FeeLockTime = treatmentFee.OUT_TIME.Value;
                        }
                        else
                        {
                            sdo.FeeLockTime = Inventec.Common.DateTime.Get.Now().Value;
                        }

                        if (!this.hisTreatmentLock.Run(sdo, ref treatment))
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisRepay_KhongTuDongDuyetHoSoVienPhi);
                            LogSystem.Warn("Tu dong duyet khoa ho so dieu tri that bai. Treatment_id: " + treatment.ID);
                        }
                    }
                }
            }
        }

        private bool IsValidDepoRepay(HisTransactionRepaySDO data, ref List<HIS_SERE_SERV_DEPOSIT> sereServDeposits)
        {
            if (IsNotNullOrEmpty(data.SereServDepositIds))
            {
                sereServDeposits = new HisSereServDepositGet().GetByIds(data.SereServDepositIds);

                //Kiem tra du lieu xem co ban ghi nao da bi huy hoac khoa chua
                List<string> unavailables = sereServDeposits
                    .Where(o => o.IS_CANCEL == MOS.UTILITY.Constant.IS_TRUE ||
                        o.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                    .Select(o => o.TDL_SERVICE_NAME).ToList();

                if (IsNotNullOrEmpty(unavailables))
                {
                    string serviceNames = string.Join(",", unavailables);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisRepay_GiaoDichCuaCacDichVuSauDaBiHuyHoacKhoa, serviceNames);
                    return false;
                }

                //Kiem tra du lieu xem co ban ghi nao da co thong tin repay hay chua
                List<HIS_SESE_DEPO_REPAY> ssDepoRepays = new HisSeseDepoRepayGet().GetNoCancelBySereServDepositIds(data.SereServDepositIds);
                if (IsNotNullOrEmpty(ssDepoRepays))
                {
                    string serviceNames = string.Join(",", ssDepoRepays.Select(s => s.TDL_SERVICE_NAME).ToList());
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisRepay_CacDichVuDaHoanUngKhongChoHoanUngLai, serviceNames);
                    return false;
                }
                decimal totalAmount = sereServDeposits.Sum(o => o.AMOUNT);
                if (totalAmount != data.Transaction.AMOUNT)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Tong so tien can hoan ung khac voi so tien giao dich. totalAmount: " + totalAmount);
                }
            }
            return true;
        }

        private bool ValidateInCaseOfUpdateExecute(long? treatmentId, List<HIS_SERE_SERV_DEPOSIT> sereServDeposits, HIS_TREATMENT treatment, ref List<HIS_SERE_SERV> allExists, ref List<HIS_SERE_SERV> changes)
        {
            if (IsNotNullOrEmpty(sereServDeposits) && treatment != null)
            {
                List<long> sereServIds = null;
                if (HisSereServCFG.AUTO_SET_NO_EXECUTE_FOR_REPAY == HisSereServCFG.SetExpendForAutoSetNoExecuteForRepayOption.NO_EXECUTE_FOR_TH_VT_AND_MAU)
                {
                    //chi lay ra cac dich vu ko phai thuoc/vat tu/mau
                    sereServIds = sereServDeposits
                        .Where(d => d.TDL_SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC
                            && d.TDL_SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT
                            && d.TDL_SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__MAU)
                        .Select(d => d.SERE_SERV_ID).ToList();

                }
                else if (HisSereServCFG.AUTO_SET_NO_EXECUTE_FOR_REPAY == HisSereServCFG.SetExpendForAutoSetNoExecuteForRepayOption.NO_EXECUTE_FOR_MAU)
                {
                    //chi lay ra cac dich vu ko phai mau
                    sereServIds = sereServDeposits
                        .Where(d => d.TDL_SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__MAU)
                        .Select(d => d.SERE_SERV_ID).ToList();
                }

                if (IsNotNullOrEmpty(sereServIds))
                {
                    HisSereServCheck checker = new HisSereServCheck();
                    HisTreatmentCheck treatmentCheck = new HisTreatmentCheck(param);
                    bool valid = true;
                    valid = valid && treatmentCheck.IsUnLock(treatment);
                    valid = valid && treatmentCheck.IsUnTemporaryLock(treatment);
                    valid = valid && treatmentCheck.IsUnLockHein(treatment);

                    if (!valid)
                    {
                        return false;
                    }

                    List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByTreatmentId(treatmentId.Value);
                    List<HIS_SERE_SERV> toChanges = sereServs.Where(o => sereServIds.Contains(o.ID)).ToList();

                    if (!checker.HasNoInvoice(toChanges)
                        || !checker.HasNoHeinApproval(toChanges)
                        || !checker.HasNoBill(toChanges))
                    {
                        return false;
                    }

                    List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().GetByIds(toChanges.Where(o=>o.SERVICE_REQ_ID.HasValue).Select(o =>o.SERVICE_REQ_ID.Value).ToList());
                    List<long> serviceReqIds_NotCheck = null;

                    if (HisSereServCFG.MUST_BE_ACCEPTED_BEFORE_SETING_NO_EXECUTE)
                    {
                        serviceReqIds_NotCheck = toChanges.Where(o => o.IS_ACCEPTING_NO_EXECUTE == Constant.IS_TRUE
                                                                || HisRoomCFG.DATA.Exists(t => t.ID == o.TDL_REQUEST_ROOM_ID && t.IS_EXAM == Constant.IS_TRUE) == false)
                                                        .Where(o => o.SERVICE_REQ_ID.HasValue).Select(o => o.SERVICE_REQ_ID.Value).ToList() ?? new List<long>();
                        List<HIS_SERE_SERV> ss = null;
                        if (HisSereServCFG.AUTO_SET_NO_EXECUTE_FOR_REPAY == HisSereServCFG.SetExpendForAutoSetNoExecuteForRepayOption.NO_EXECUTE_FOR_TH_VT_AND_MAU)
                        {
                            //Chi kiem tra neu dv khong phai la "thuoc/vat tu/mau" va duoc chi dinh boi phong kham
                            ss = toChanges.Where(o => o.IS_ACCEPTING_NO_EXECUTE != Constant.IS_TRUE
                                && o.TDL_SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT
                                && o.TDL_SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC
                                && o.TDL_SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__MAU
                                && HisRoomCFG.DATA.Exists(t => t.ID == o.TDL_REQUEST_ROOM_ID && t.IS_EXAM == Constant.IS_TRUE)).ToList();
                        }
                        else if (HisSereServCFG.AUTO_SET_NO_EXECUTE_FOR_REPAY == HisSereServCFG.SetExpendForAutoSetNoExecuteForRepayOption.NO_EXECUTE_FOR_MAU)
                        {
                            //Chi kiem tra neu dv khong phai la "mau" va duoc chi dinh boi phong kham
                            ss = toChanges.Where(o => o.IS_ACCEPTING_NO_EXECUTE != Constant.IS_TRUE
                                && o.TDL_SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__MAU
                                && HisRoomCFG.DATA.Exists(t => t.ID == o.TDL_REQUEST_ROOM_ID && t.IS_EXAM == Constant.IS_TRUE)).ToList();
                        }

                        if (IsNotNullOrEmpty(ss))
                        {
                            string str = "";
                            foreach (HIS_SERE_SERV s in ss)
                            {
                                V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == s.TDL_REQUEST_ROOM_ID).FirstOrDefault();
                                if (room != null)
                                {
                                    str += string.Format("{0} ({1}-{2}); ", s.TDL_SERVICE_NAME, s.TDL_SERVICE_REQ_CODE, room.ROOM_NAME);
                                }
                            }
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_BacSyChuaXacNhanDongYKhongThucHien, str);
                            return false;
                        }
                    }

                    List<HIS_SERVICE_REQ> startedServiceReqs = serviceReqs != null ? serviceReqs.Where(o => o.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL && !serviceReqIds_NotCheck.Contains(o.ID)).ToList() : null;
                    if (IsNotNullOrEmpty(startedServiceReqs))
                    {
                        List<HIS_SERVICE_REQ> serviceReqs_DXL = startedServiceReqs.Where(o => o.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL).ToList() ?? new List<HIS_SERVICE_REQ>();
                        var toChanges_ConfirmNoExcute = toChanges.Where(o => o.IS_CONFIRM_NO_EXCUTE == Constant.IS_TRUE && serviceReqs_DXL.Select(s => s.ID).Contains(o.SERVICE_REQ_ID.Value)).ToList();
                        List<string> serviceNames = toChanges.Where(o => !toChanges_ConfirmNoExcute.Contains(o) && startedServiceReqs.Select(s => s.ID).Contains(o.SERVICE_REQ_ID.Value)).Select(o => o.TDL_SERVICE_NAME).ToList();
                        if (IsNotNullOrEmpty(serviceNames))
                        {
                            string nameStr = string.Join(",", serviceNames);

                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_DichVuDaBatDauKhongChoPhepCapNhatKhongThucHien, nameStr);
                            return false;
                        }
                    }

                    allExists = sereServs;

                    if (IsNotNullOrEmpty(toChanges))
                    {
                        Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                        changes = Mapper.Map<List<HIS_SERE_SERV>>(toChanges); //clone de ko anh huong den allExists
                    }
                }
            }
            return true;
        }

        private void PassResult(ref V_HIS_TRANSACTION resultData)
        {
            resultData = new HisTransactionGet(param).GetViewById(this.recentHisTransaction.ID);
        }

        /// <summary>
        /// Xu ly de gan cac thong tin thoi gian theo gio server
        /// </summary>
        /// <param name="data"></param>
        private void SetServerTime(HisTransactionRepaySDO data)
        {
            //Neu cau hinh su dung gio server thi gan lai theo gio server cac du lieu thoi gian truyen len
            if (SystemCFG.IS_USING_SERVER_TIME && data != null)
            {
                long now = Inventec.Common.DateTime.Get.Now().Value;
                if (data.Transaction != null)
                {
                    data.Transaction.TRANSACTION_TIME = now;
                }
            }
        }

        private void RollbackData()
        {
            this.hisSeseDepoRepayCreate.RollbackData();
            this.hisTransactionCreate.RollbackData();
        }
    }
}

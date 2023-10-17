using DevExpress.Entity.Model.Metadata;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Repay
{
    class HisTransactionRepayCheck : BusinessBase
    {
        internal HisTransactionRepayCheck()
            : base()
        {

        }

        internal HisTransactionRepayCheck(CommonParam param)
            : base(param)
        {

        }

        /// <summary>
        /// Kiem tra thuc hien giao dich hoan ung (Phuc vu thuc hien giao dich bang the onelink)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Run(HisTransactionRepaySDO data)
        {
            bool result = true;
            try
            {
                List<HIS_SERE_SERV_DEPOSIT> sereServDeposits = null;
                WorkPlaceSDO workPlace = null;
                HIS_TREATMENT hisTreatment = null;
                V_HIS_ACCOUNT_BOOK hisAccountBook = null;
                HisTransactionCheck checker = new HisTransactionCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                result = result && IsNotNull(data);
                result = result && IsNotNull(data.Transaction); 
                result = result && checker.HasNotFinancePeriod(data.Transaction);
                result = result && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                if (result)
                {
                    if (!workPlace.CashierRoomId.HasValue)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.BanKhongLamViecTaiPhongThuNgan);
                        return false;
                    }
                    //chi cho tao khi chua duyet khoa tai chinh voi cac giao dich gan lien voi ho so dieu tri
                    if (data.Transaction.TREATMENT_ID.HasValue)
                    {
                        result = result && treatmentChecker.VerifyId(data.Transaction.TREATMENT_ID.Value, ref hisTreatment);
                    }
                    data.Transaction.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__HU;
                    result = result && checker.IsUnlockAccountBook(data.Transaction.ACCOUNT_BOOK_ID, ref hisAccountBook);
                    result = result && checker.IsValidNumOrder(data.Transaction, hisAccountBook);
                    result = result && checker.IsValidAmountRepay(data.Transaction, data.SereServDepositIds != null);
                    result = result && this.IsValidDepoRepay(data, ref sereServDeposits);
                    result = result && this.ValidateInCaseOfUpdateExecute(data.Transaction.TREATMENT_ID.Value, sereServDeposits);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
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

        private bool ValidateInCaseOfUpdateExecute(long? treatmentId, List<HIS_SERE_SERV_DEPOSIT> sereServDeposits)
        {
            if (HisSereServCFG.AUTO_SET_NO_EXECUTE_FOR_REPAY == HisSereServCFG.SetExpendForAutoSetNoExecuteForRepayOption.NO_EXECUTE_FOR_TH_VT_AND_MAU && IsNotNullOrEmpty(sereServDeposits) && treatmentId.HasValue)
            {
                //chi lay ra cac dich vu ko phai thuoc/vat tu/mau
                List<long> sereServIds = sereServDeposits
                    .Where(d => d.TDL_SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC
                        && d.TDL_SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT
                        && d.TDL_SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__MAU)
                    .Select(d => d.SERE_SERV_ID).ToList();

                if (IsNotNullOrEmpty(sereServIds))
                {
                    HIS_TREATMENT treatment = null;
                    HisSereServCheck checker = new HisSereServCheck();
                    HisTreatmentCheck treatmentCheck = new HisTreatmentCheck(param);
                    bool valid = true;
                    valid = valid && treatmentCheck.VerifyId(treatmentId.Value, ref treatment);
                    valid = valid && treatmentCheck.IsUnLock(treatment);
                    valid = valid && treatmentCheck.IsUnTemporaryLock(treatment);
                    valid = valid && treatmentCheck.IsUnLockHein(treatment);
                    valid = valid && checker.HasNoDeposit(sereServIds, true);

                    List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByTreatmentId(treatmentId.Value);
                    List<HIS_SERE_SERV> toChanges = sereServs.Where(o => sereServIds.Contains(o.ID)).ToList();

                    if (!checker.HasNoInvoice(toChanges)
                        || !checker.HasNoHeinApproval(toChanges)
                        || !checker.HasNoBill(toChanges))
                    {
                        return false;
                    }

                    List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().GetByIds(toChanges.Select(o => o.SERVICE_REQ_ID.Value).ToList());

                    List<long> startedServiceReqIds = serviceReqs != null ? serviceReqs.Where(o => o.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL).Select(o => o.ID).ToList() : null;
                    if (IsNotNullOrEmpty(startedServiceReqIds))
                    {
                        List<string> serviceNames = toChanges.Where(o => startedServiceReqIds.Contains(o.SERVICE_REQ_ID.Value)).Select(o => o.TDL_SERVICE_NAME).ToList();
                        string nameStr = string.Join(",", serviceNames);

                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_DichVuDaBatDauKhongChoPhepCapNhatKhongThucHien, nameStr);
                        return false;
                    }
                }
            }
            return true;
        }

    }
}

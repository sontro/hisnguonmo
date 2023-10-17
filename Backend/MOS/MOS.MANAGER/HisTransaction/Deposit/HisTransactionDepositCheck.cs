using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisCard;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServDebt;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Deposit
{
    class HisTransactionDepositCheck : BusinessBase
    {
        internal HisTransactionDepositCheck()
            : base()
        {

        }

        internal HisTransactionDepositCheck(CommonParam param)
            : base(param)
        {

        }

        /// <summary>
        /// Kiem tra thuc hien giao dich tam ung (Phuc thu giao dich bang the onelink)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Run(HisTransactionDepositSDO data)
        {
            bool result = true;
            try
            {
                WorkPlaceSDO workPlace = null;
                HIS_TREATMENT hisTreatment = null;
                V_HIS_ACCOUNT_BOOK hisAccountBook = null;
                HisTransactionCheck checker = new HisTransactionCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                result = result && IsNotNull(data);
                result = result && IsNotNull(data.Transaction);
                result = result && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                result = result && checker.HasNotFinancePeriod(data.Transaction);
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
                    data.Transaction.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU;
                    result = result && checker.IsUnlockAccountBook(data.Transaction.ACCOUNT_BOOK_ID, ref hisAccountBook);
                    result = result && checker.IsValidNumOrder(data.Transaction, hisAccountBook);

                    if (result && IsNotNullOrEmpty(data.SereServDeposits))
                    {
                        List<long> sereServIds = data.SereServDeposits.Select(o => o.SERE_SERV_ID).ToList();

                        //Lay danh sach thong tin "cong no" (va chua bi huy) tuong ung voi sere_serv
                        List<HIS_SERE_SERV_DEBT> existsDebts = new HisSereServDebtGet().GetNoCancelBySereServIds(sereServIds);
                        if (IsNotNullOrEmpty(existsDebts))
                        {
                            List<string> names = existsDebts.Select(o => o.TDL_SERVICE_NAME).Distinct().ToList();
                            string nameStr = string.Join(",", names);
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_DichVuDaChotNo, nameStr);
                            return false;
                        }

                        List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByIds(sereServIds);

                        //Lay ra cac danh sach sere_serv khong thuc hien
                        List<string> noExecuteServices = sereServs
                            .Where(o => o.IS_NO_EXECUTE.HasValue && o.IS_NO_EXECUTE.Value == MOS.UTILITY.Constant.IS_TRUE)
                            .Select(o => o.TDL_SERVICE_NAME).ToList();

                        if (IsNotNullOrEmpty(noExecuteServices))
                        {
                            string names = string.Join(",", noExecuteServices);
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDeposit_CacDichVuSauKhongThucHienKhongChoTamUng, names);
                            return false;
                        }

                        List<string> presInServices = sereServs.Where(o => (o.MEDICINE_ID.HasValue || o.MATERIAL_ID.HasValue) && o.TDL_SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONDT).Select(s => s.TDL_SERVICE_NAME).ToList();
                        if (IsNotNullOrEmpty(presInServices))
                        {
                            string names = string.Join(",", presInServices);
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisDeposit_CacThuocVatTuSauThuocDonNoiTru, names);
                            return false;
                        }

                        if (!new HisSereServCheck(param).HasNoDeposit(sereServIds, true))
                        {
                            return false;
                        }

                        decimal totalAmount = data.SereServDeposits.Sum(s => s.AMOUNT);

                        if (totalAmount != data.Transaction.AMOUNT)
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            LogSystem.Warn("Tong so tien cua HIS_DERE_DETAIL khong khop voi so tien tam ung trong transaction. totalDereAmount: " + totalAmount);
                            return false;
                        }
                    }
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
    }
}

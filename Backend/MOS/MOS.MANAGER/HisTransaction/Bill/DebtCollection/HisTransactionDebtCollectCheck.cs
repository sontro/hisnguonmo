using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServDebt;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Bill.DebtCollection
{
    class HisTransactionDebtCollectCheck: BusinessBase
    {
        internal HisTransactionDebtCollectCheck()
            : base()
        {

        }

        internal HisTransactionDebtCollectCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool IsValidData(HisTransactionDebtCollecSDO data, ref List<HIS_TRANSACTION> debts)
        {
            bool valid = true;
            try
            {
                List<HIS_TRANSACTION> transactions = new HisTransactionGet().GetByIds(data.DebtIds);

                List<string> notDebts = IsNotNullOrEmpty(transactions) ? transactions.Where(o => o.TRANSACTION_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__NO).Select(o => o.TRANSACTION_CODE).ToList() : null;
                if (IsNotNullOrEmpty(notDebts))
                {
                    string codeStr = string.Join(",", notDebts);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_KhongPhaiCongNo, codeStr);
                    return false;
                }

                List<string> paids = IsNotNullOrEmpty(transactions) ? transactions.Where(o => o.DEBT_BILL_ID.HasValue).Select(o => o.TRANSACTION_CODE).ToList() : null;
                if (IsNotNullOrEmpty(paids))
                {
                    string codeStr = string.Join(",", paids);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_PhieuCongNoDaDuocThanhToan, codeStr);
                    return false;
                }

                bool drugStoreDebtExists = transactions.Exists(t => t.DEBT_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION.DEBT_TYPE__DRUG_STORE);
                bool treatmentFeeDebtExists = transactions.Exists(t => t.DEBT_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION.DEBT_TYPE__SERVICE || t.DEBT_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION.DEBT_TYPE__TREAT);
                if (drugStoreDebtExists && treatmentFeeDebtExists)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_TonTaiCaPhieuChotNoVienPhiVaNhaThuoc);
                    return false;
                }
                if (treatmentFeeDebtExists && !data.TreatmentId.HasValue)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Thieu treatment_id");
                    return false;
                }

                debts = transactions;
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

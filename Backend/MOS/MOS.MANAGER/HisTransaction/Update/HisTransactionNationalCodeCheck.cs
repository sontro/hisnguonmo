using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Update
{
    class HisTransactionNationalCodeCheck : BusinessBase
    {
        internal HisTransactionNationalCodeCheck()
            : base()
        {

        }

        internal HisTransactionNationalCodeCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool CheckExistsExpMest(HIS_TRANSACTION data)
        {
            bool valid = true;
            try
            {
                valid = this.CheckExistsExpMest(new List<HIS_TRANSACTION>() { data });
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool CheckExistsExpMest(List<HIS_TRANSACTION> listData)
        {
            bool valid = true;
            try
            {
                List<HIS_EXP_MEST> expMests = new HisExpMestGet().GetByBillIds(listData.Select(s => s.ID).ToList());
                List<HIS_TRANSACTION> notExists = listData.Where(o => expMests == null || !expMests.Exists(e => e.BILL_ID == o.ID)).ToList();
                if (IsNotNullOrEmpty(notExists))
                {
                    List<string> notExistsCodes = notExists.Select(o => o.TRANSACTION_CODE).ToList();
                    string str = string.Join(",", notExistsCodes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_KhongPhaiThanhToanPhieuXuat, str);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsBill(HIS_TRANSACTION data)
        {
            bool valid = true;
            try
            {
                valid = this.IsBill(new List<HIS_TRANSACTION>() { data });
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsBill(List<HIS_TRANSACTION> listData)
        {
            bool valid = true;
            try
            {
                List<string> hasCodes = listData != null ? listData
                    .Where(o => o.TRANSACTION_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT)
                    .Select(o => o.TRANSACTION_CODE)
                    .ToList() : null;

                if (IsNotNullOrEmpty(hasCodes))
                {
                    string str = string.Join(",", hasCodes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_KhongPhaiGiaoDichThanhToan, str);
                    return false;
                }
            }
            catch (Exception ex)
            {
                valid = false;
                param.HasException = true;
                LogSystem.Error(ex);
            }
            return valid;
        }

    }
}

using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Update
{
    class HisTransactionUpdateNationalCode : BusinessBase
    {
        internal HisTransactionUpdateNationalCode()
            : base()
        {

        }

        internal HisTransactionUpdateNationalCode(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(List<HIS_TRANSACTION> listData, ref List<HIS_TRANSACTION> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_TRANSACTION> listRaw = new List<HIS_TRANSACTION>();
                HisTransactionCheck checker = new HisTransactionCheck(param);
                HisTransactionNationalCodeCheck nationalChecker=new HisTransactionNationalCodeCheck(param);
                valid = valid && IsNotNullOrEmpty(listData);
                valid = valid && checker.VerifyIds(listData.Select(s => s.ID).ToList(), listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                valid = valid && checker.IsUnCancel(listRaw);
                valid = valid && checker.HasNoNationalCode(listRaw);
                valid = valid && nationalChecker.CheckExistsExpMest(listRaw);
                valid = valid && nationalChecker.IsBill(listRaw);                
                if (valid)
                {
                    foreach (var raw in listRaw)
                    {
                        HIS_TRANSACTION data = listData.FirstOrDefault(o => o.ID == raw.ID);
                        raw.NATIONAL_TRANSACTION_CODE = data.NATIONAL_TRANSACTION_CODE;
                    }

                    if (!DAOWorker.HisTransactionDAO.UpdateList(listRaw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransaction_ThemMoiThatBai);
                        throw new Exception("update NATIONAL_TRANSACTION_CODE HisTransaction that bai." + LogUtil.TraceData("listRaw", listRaw));
                    }
                    result = true;
                    resultData = listRaw;
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

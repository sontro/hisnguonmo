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
    class HisTransactionCancelNationalCode : BusinessBase
    {
        internal HisTransactionCancelNationalCode()
            : base()
        {

        }

        internal HisTransactionCancelNationalCode(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(List<long> listData, ref List<HIS_TRANSACTION> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_TRANSACTION> listRaw = new List<HIS_TRANSACTION>();
                HisTransactionCheck checker = new HisTransactionCheck(param);
                valid = valid && IsNotNullOrEmpty(listData);
                valid = valid && checker.VerifyIds(listData, listRaw);
                valid = valid && checker.HasNationalCode(listRaw);            
                if (valid)
                {
                    listRaw.ForEach(o => o.NATIONAL_TRANSACTION_CODE = null);

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

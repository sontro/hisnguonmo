using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTreatment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTransaction
{
    partial class HisTransactionUpdate : BusinessBase
    {

        internal bool UpdateFile(HIS_TRANSACTION data, ref V_HIS_TRANSACTION resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TRANSACTION raw = null;
                HisTransactionCheck checker = new HisTransactionCheck(param);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsUnCancel(raw);
                valid = valid && checker.IsAlowUpdateFile(raw);
                if (valid)
                {
                    AutoMapper.Mapper.CreateMap<HIS_TRANSACTION, HIS_TRANSACTION>();
                    this.beforeUpdateHisTransactions.Add(AutoMapper.Mapper.Map<HIS_TRANSACTION>(raw));
                    raw.FILE_NAME = data.FILE_NAME;
                    raw.FILE_URL = data.FILE_URL;
                    if (!DAOWorker.HisTransactionDAO.Update(raw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransaction_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTransaction that bai." + LogUtil.TraceData("data", data));
                    }
                    this.PassResult(raw, ref resultData);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void PassResult(HIS_TRANSACTION raw, ref V_HIS_TRANSACTION resultData)
        {
            resultData = new HisTransactionGet().GetViewById(raw.ID);
        }
    }
}

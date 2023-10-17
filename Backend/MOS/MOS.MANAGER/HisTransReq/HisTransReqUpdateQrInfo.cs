using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOS.MANAGER.HisTransReq
{
    class HisTransReqUpdateQrInfo : BusinessBase
    {
        private List<HIS_TRANS_REQ> beforeUpdateHisTransReqs = new List<HIS_TRANS_REQ>();

        internal HisTransReqUpdateQrInfo()
            : base()
        {

        }

        internal HisTransReqUpdateQrInfo(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Run(HisTransReqBankInfoSDO data, ref HIS_TRANS_REQ resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTransReqCheck checker = new HisTransReqCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.BankQrCode);
                HIS_TRANS_REQ raw = null;
                valid = valid && checker.VerifyId(data.TransReqId, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    HIS_TRANS_REQ updateData = new HIS_TRANS_REQ();
                    Inventec.Common.Mapper.DataObjectMapper.Map<HIS_TRANS_REQ>(updateData, raw);

                    updateData.BANK_QR_CODE = data.BankQrCode;

                    if (!DAOWorker.HisTransReqDAO.Update(updateData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTransReq that bai." + LogUtil.TraceData("data", data));
                    }

                    this.beforeUpdateHisTransReqs.Add(raw);
                    resultData = updateData;
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

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdateHisTransReqs))
            {
                if (!DAOWorker.HisTransReqDAO.UpdateList(this.beforeUpdateHisTransReqs))
                {
                    LogSystem.Warn("Rollback du lieu HisTransReq that bai, can kiem tra lai." + LogUtil.TraceData("HisTransReqs", this.beforeUpdateHisTransReqs));
                }
                this.beforeUpdateHisTransReqs = null;
            }
        }
    }
}

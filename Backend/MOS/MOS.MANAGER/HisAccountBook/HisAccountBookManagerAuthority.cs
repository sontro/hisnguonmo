using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisAccountBook.Authority;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccountBook
{
    public partial class HisAccountBookManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<AuthorityAccountBookSDO> Request(AuthorityAccountBookSDO data)
        {
            ApiResultObject<AuthorityAccountBookSDO> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                AuthorityAccountBookSDO resultData = null;
                if (valid)
                {
                    bool rs = new HisAccountBookAuthorityProcessor(param).Request(data, ref resultData);
                    result = this.PackResult(resultData, rs);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<bool> Approve(ApprovalAccountBookSDO data)
        {
            ApiResultObject<bool> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                if (valid)
                {
                    bool rs = new HisAccountBookAuthorityProcessor(param).Approve(data);
                    result = this.PackSingleResult(rs);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<bool> Unapprove(UnapprovalAccountBookSDO data)
        {
            ApiResultObject<bool> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                if (valid)
                {
                    bool rs = new HisAccountBookAuthorityProcessor(param).Unapprove(data);
                    result = this.PackSingleResult(rs);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<bool> Reject(RejectAccountBookSDO data)
        {
            ApiResultObject<bool> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                if (valid)
                {
                    bool rs = new HisAccountBookAuthorityProcessor(param).Reject(data);
                    result = this.PackSingleResult(rs);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<bool> Cancel()
        {
            ApiResultObject<bool> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                if (valid)
                {
                    bool rs = new HisAccountBookAuthorityProcessor(param).Cancel();
                    result = this.PackSingleResult(rs);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<List<AuthorityAccountBookSDO>> RequestToMe(long workingRoomId)
        {
            ApiResultObject<List<AuthorityAccountBookSDO>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<AuthorityAccountBookSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisAccountBookAuthorityProcessor(param).RequestToMe(workingRoomId);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<AuthorityAccountBookSDO> MyRequest(long workingRoomId)
        {
            ApiResultObject<AuthorityAccountBookSDO> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                AuthorityAccountBookSDO resultData = null;
                if (valid)
                {
                    resultData = new HisAccountBookAuthorityProcessor(param).MyRequest(workingRoomId);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }
    }
}

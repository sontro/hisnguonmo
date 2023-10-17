using Inventec.Common.Logging;
using Inventec.Common.WebApiClient;
using Inventec.Core;
using Inventec.Token.ResourceSystem;
using MOS.MANAGER.Config;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisAccountBook.Authority
{
    class HisAccountBookAuthorityMasterService
    {
        public static bool Request(AuthorityAccountBookSDO data, ref AuthorityAccountBookSDO resultData, CommonParam param)
        {
            bool result = false;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master"
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    string tokenCode = ResourceTokenManager.GetTokenCode();
                    ApiConsumer mosConsumer = new ApiConsumer(SystemCFG.MASTER_ADDRESS, tokenCode, MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE);
                    ApiResultObject<AuthorityAccountBookSDO> rs = mosConsumer.Post<ApiResultObject<AuthorityAccountBookSDO>>("api/HisAccountBook/Request", param, data);
                    if (rs == null || !rs.Success)
                    {
                        if (rs != null && rs.Param != null)
                        {
                            CommonUtil.AddParamInfo(rs.Param, param);
                        }
                        LogSystem.Warn("MOS mater HisAccountBook/Request that bai");
                        result = false;
                    }
                    else
                    {
                        result = true;
                        resultData = rs.Data;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }

        public static bool Approve(ApprovalAccountBookSDO data, CommonParam param)
        {
            bool result = false;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master"
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    string tokenCode = ResourceTokenManager.GetTokenCode();
                    ApiConsumer mosConsumer = new ApiConsumer(SystemCFG.MASTER_ADDRESS, tokenCode, MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE);
                    ApiResultObject<bool> rs = mosConsumer.Post<ApiResultObject<bool>>("api/HisAccountBook/Approve", param, data);
                    if (rs == null || !rs.Success)
                    {
                        if (rs != null && rs.Param != null)
                        {
                            CommonUtil.AddParamInfo(rs.Param, param);
                        }
                        LogSystem.Warn("MOS mater HisAccountBook/Approve that bai");
                        result = false;
                    }
                    else
                    {
                        result = rs.Data;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }

        public static bool Unapprove(UnapprovalAccountBookSDO data, CommonParam param)
        {
            bool result = false;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master"
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    string tokenCode = ResourceTokenManager.GetTokenCode();
                    ApiConsumer mosConsumer = new ApiConsumer(SystemCFG.MASTER_ADDRESS, tokenCode, MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE);
                    ApiResultObject<bool> rs = mosConsumer.Post<ApiResultObject<bool>>("api/HisAccountBook/Unapprove", param, data);
                    if (rs == null || !rs.Success)
                    {
                        if (rs != null && rs.Param != null)
                        {
                            CommonUtil.AddParamInfo(rs.Param, param);
                        }
                        LogSystem.Warn("MOS mater HisAccountBook/Unapprove that bai");
                        result = false;
                    }
                    else
                    {
                        result = rs.Data;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }

        public static bool Reject(RejectAccountBookSDO data, CommonParam param)
        {
            bool result = false;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master"
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    string tokenCode = ResourceTokenManager.GetTokenCode();
                    ApiConsumer mosConsumer = new ApiConsumer(SystemCFG.MASTER_ADDRESS, tokenCode, MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE);
                    ApiResultObject<bool> rs = mosConsumer.Post<ApiResultObject<bool>>("api/HisAccountBook/Reject", param, data);
                    if (rs == null || !rs.Success)
                    {
                        if (rs != null && rs.Param != null)
                        {
                            CommonUtil.AddParamInfo(rs.Param, param);
                        }
                        LogSystem.Warn("MOS mater HisAccountBook/Reject that bai");
                        result = false;
                    }
                    else
                    {
                        result = rs.Data;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }

        public static bool Cancel(CommonParam param)
        {
            bool result = false;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master"
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    string tokenCode = ResourceTokenManager.GetTokenCode();
                    ApiConsumer mosConsumer = new ApiConsumer(SystemCFG.MASTER_ADDRESS, tokenCode, MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE);
                    ApiResultObject<bool> rs = mosConsumer.Post<ApiResultObject<bool>>("api/HisAccountBook/Cancel", param, null);
                    if (rs == null || !rs.Success)
                    {
                        if (rs != null && rs.Param != null)
                        {
                            CommonUtil.AddParamInfo(rs.Param, param);
                        }
                        LogSystem.Warn("MOS mater HisAccountBook/Cancel that bai");
                        result = false;
                    }
                    else
                    {
                        result = rs.Data;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }

        public static List<AuthorityAccountBookSDO> RequestToMe(long workingRoomId, CommonParam param)
        {
            List<AuthorityAccountBookSDO> result = null;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master"
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    string tokenCode = ResourceTokenManager.GetTokenCode();
                    ApiConsumer mosConsumer = new ApiConsumer(SystemCFG.MASTER_ADDRESS, tokenCode, MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE);
                    ApiResultObject<List<AuthorityAccountBookSDO>> rs = mosConsumer.Get<ApiResultObject<List<AuthorityAccountBookSDO>>>("api/HisAccountBook/RequestToMe", param, workingRoomId);
                    if (rs == null || !rs.Success)
                    {
                        if (rs != null && rs.Param != null)
                        {
                            CommonUtil.AddParamInfo(rs.Param, param);
                        }
                        LogSystem.Warn("MOS mater HisAccountBook/RequestToMe that bai");
                        result = null;
                    }
                    else
                    {
                        result = rs.Data;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }

        public static AuthorityAccountBookSDO MyRequest(long workingRoomId, CommonParam param)
        {
            AuthorityAccountBookSDO result = null;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master"
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    string tokenCode = ResourceTokenManager.GetTokenCode();
                    ApiConsumer mosConsumer = new ApiConsumer(SystemCFG.MASTER_ADDRESS, tokenCode, MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE);
                    ApiResultObject<AuthorityAccountBookSDO> rs = mosConsumer.Get<ApiResultObject<AuthorityAccountBookSDO>>("api/HisAccountBook/MyRequest", param, workingRoomId);
                    if (rs == null || !rs.Success)
                    {
                        if (rs != null && rs.Param != null)
                        {
                            CommonUtil.AddParamInfo(rs.Param, param);
                        }
                        LogSystem.Warn("MOS mater HisAccountBook/MyRequest that bai");
                        result = null;
                    }
                    else
                    {
                        result = rs.Data;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }
    }
}

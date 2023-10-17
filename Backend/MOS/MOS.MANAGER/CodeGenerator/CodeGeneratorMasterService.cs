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

namespace MOS.MANAGER.CodeGenerator
{
    class CodeGeneratorMasterService
    {
        public static string InCodeGetNext(string input, CommonParam param)
        {
            string result = null;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master" de sinh code
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    ApiConsumer mosConsumer = new ApiConsumer(SystemCFG.MASTER_ADDRESS, ResourceTokenManager.GetTokenCode(), MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE);
                    ApiResultObject<string> rs = mosConsumer.Post<ApiResultObject<string>>("api/CodeGeneration/InCodeGetNext", param, input);
                    if (rs == null || !rs.Success)
                    {
                        if (rs != null && rs.Param != null)
                        {
                            CommonUtil.AddParamInfo(rs.Param, param);
                        }
                        LogSystem.Warn("MOS mater api/CodeGeneration/InCodeGetNext that bai");
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

        public static bool InCodeFinishUpdateDB(string input, CommonParam param)
        {
            bool result = false;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master"
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    ApiConsumer mosConsumer = new ApiConsumer(SystemCFG.MASTER_ADDRESS, ResourceTokenManager.GetTokenCode(), MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE);
                    ApiResultObject<bool> rs = mosConsumer.Post<ApiResultObject<bool>>("api/CodeGeneration/InCodeFinishUpdateDB", param, input);
                    if (rs == null || !rs.Success)
                    {
                        if (rs != null && rs.Param != null)
                        {
                            CommonUtil.AddParamInfo(rs.Param, param);
                        }
                        LogSystem.Warn("MOS mater api/CodeGeneration/InCodeFinishUpdateDB that bai");
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

        
        public static string ExtraEndCodeGetNext(string input, CommonParam param)
        {
            string result = null;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master" de sinh code
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    ApiConsumer mosConsumer = new ApiConsumer(SystemCFG.MASTER_ADDRESS, ResourceTokenManager.GetTokenCode(), MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE);
                    ApiResultObject<string> rs = mosConsumer.Post<ApiResultObject<string>>("api/CodeGeneration/ExtraEndCodeGetNext", param, input);
                    if (rs == null || !rs.Success)
                    {
                        if (rs != null && rs.Param != null)
                        {
                            CommonUtil.AddParamInfo(rs.Param, param);
                        }
                        LogSystem.Warn("MOS mater api/CodeGeneration/ExtraEndCodeGetNext that bai");
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

        public static bool ExtraEndCodeFinishUpdateDB(string input, CommonParam param)
        {
            bool result = false;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master"
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    ApiConsumer mosConsumer = new ApiConsumer(SystemCFG.MASTER_ADDRESS, ResourceTokenManager.GetTokenCode(), MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE);
                    ApiResultObject<bool> rs = mosConsumer.Post<ApiResultObject<bool>>("api/CodeGeneration/ExtraEndCodeFinishUpdateDB", param, input);
                    if (rs == null || !rs.Success)
                    {
                        if (rs != null && rs.Param != null)
                        {
                            CommonUtil.AddParamInfo(rs.Param, param);
                        }
                        LogSystem.Warn("MOS mater api/CodeGeneration/ExtraEndCodeFinishUpdateDB that bai");
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


        public static string StoreCodeGetNextOption1(long seedTime, CommonParam param)
        {
            string result = null;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master" de sinh code
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    ApiConsumer mosConsumer = new ApiConsumer(SystemCFG.MASTER_ADDRESS, ResourceTokenManager.GetTokenCode(), MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE);
                    ApiResultObject<string> rs = mosConsumer.Post<ApiResultObject<string>>("api/CodeGeneration/StoreCodeGetNextOption1", param, seedTime);
                    if (rs == null || !rs.Success)
                    {
                        if (rs != null && rs.Param != null)
                        {
                            CommonUtil.AddParamInfo(rs.Param, param);
                        }
                        LogSystem.Warn("MOS mater api/CodeGeneration/StoreCodeGetNextOption1 that bai");
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

        public static string StoreCodeGetNextOption2(StoreCodeGenerateSDO data, CommonParam param)
        {
            string result = null;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master" de sinh code
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    ApiConsumer mosConsumer = new ApiConsumer(SystemCFG.MASTER_ADDRESS, ResourceTokenManager.GetTokenCode(), MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE);
                    ApiResultObject<string> rs = mosConsumer.Post<ApiResultObject<string>>("api/CodeGeneration/StoreCodeGetNextOption2", param, data);
                    if (rs == null || !rs.Success)
                    {
                        if (rs != null && rs.Param != null)
                        {
                            CommonUtil.AddParamInfo(rs.Param, param);
                        }
                        LogSystem.Warn("MOS mater api/CodeGeneration/StoreCodeGetNextOption2 that bai");
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

        public static string StoreCodeGetNextOption34(StoreCodeGenerateSDO data, CommonParam param)
        {
            string result = null;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master" de sinh code
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    ApiConsumer mosConsumer = new ApiConsumer(SystemCFG.MASTER_ADDRESS, ResourceTokenManager.GetTokenCode(), MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE);
                    ApiResultObject<string> rs = mosConsumer.Post<ApiResultObject<string>>("api/CodeGeneration/StoreCodeGetNextOption34", param, data);
                    if (rs == null || !rs.Success)
                    {
                        if (rs != null && rs.Param != null)
                        {
                            CommonUtil.AddParamInfo(rs.Param, param);
                        }
                        LogSystem.Warn("MOS mater api/CodeGeneration/StoreCodeGetNextOption34 that bai");
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

        public static bool StoreCodeFinishUpdateDB(List<string> inputs, CommonParam param)
        {
            bool result = false;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master"
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    ApiConsumer mosConsumer = new ApiConsumer(SystemCFG.MASTER_ADDRESS, ResourceTokenManager.GetTokenCode(), MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE);
                    ApiResultObject<bool> rs = mosConsumer.Post<ApiResultObject<bool>>("api/CodeGeneration/StoreCodeFinishUpdateDB", param, inputs);
                    if (rs == null || !rs.Success)
                    {
                        if (rs != null && rs.Param != null)
                        {
                            CommonUtil.AddParamInfo(rs.Param, param);
                        }
                        LogSystem.Warn("MOS mater api/CodeGeneration/StoreCodeFinishUpdateDB that bai");
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


        public static string MediRecordStoreCodeGetNextOption1(long seedTime, CommonParam param)
        {
            string result = null;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master" de sinh code
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    ApiConsumer mosConsumer = new ApiConsumer(SystemCFG.MASTER_ADDRESS, ResourceTokenManager.GetTokenCode(), MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE);
                    ApiResultObject<string> rs = mosConsumer.Post<ApiResultObject<string>>("api/CodeGeneration/MediRecordStoreCodeGetNextOption1", param, seedTime);
                    if (rs == null || !rs.Success)
                    {
                        if (rs != null && rs.Param != null)
                        {
                            CommonUtil.AddParamInfo(rs.Param, param);
                        }
                        LogSystem.Warn("MOS mater api/CodeGeneration/MediRecordStoreCodeGetNextOption1 that bai");
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

        public static string MediRecordStoreCodeGetNextOption2(MediRecordStoreCodeGenerateSDO data, CommonParam param)
        {
            string result = null;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master" de sinh code
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    ApiConsumer mosConsumer = new ApiConsumer(SystemCFG.MASTER_ADDRESS, ResourceTokenManager.GetTokenCode(), MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE);
                    ApiResultObject<string> rs = mosConsumer.Post<ApiResultObject<string>>("api/CodeGeneration/MediRecordStoreCodeGetNextOption2", param, data);
                    if (rs == null || !rs.Success)
                    {
                        if (rs != null && rs.Param != null)
                        {
                            CommonUtil.AddParamInfo(rs.Param, param);
                        }
                        LogSystem.Warn("MOS mater api/CodeGeneration/MediRecordStoreCodeGetNextOption2 that bai");
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

        public static string MediRecordStoreCodeGetNextOption3(MediRecordStoreCodeGenerateSDO data, CommonParam param)
        {
            string result = null;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master" de sinh code
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    ApiConsumer mosConsumer = new ApiConsumer(SystemCFG.MASTER_ADDRESS, ResourceTokenManager.GetTokenCode(), MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE);
                    ApiResultObject<string> rs = mosConsumer.Post<ApiResultObject<string>>("api/CodeGeneration/MediRecordStoreCodeGetNextOption3", param, data);
                    if (rs == null || !rs.Success)
                    {
                        if (rs != null && rs.Param != null)
                        {
                            CommonUtil.AddParamInfo(rs.Param, param);
                        }
                        LogSystem.Warn("MOS mater api/CodeGeneration/MediRecordStoreCodeGetNextOption3 that bai");
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

        public static string MediRecordStoreCodeGetNextOption4(MediRecordStoreCodeGenerateSDO data, CommonParam param)
        {
            string result = null;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master" de sinh code
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    ApiConsumer mosConsumer = new ApiConsumer(SystemCFG.MASTER_ADDRESS, ResourceTokenManager.GetTokenCode(), MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE);
                    ApiResultObject<string> rs = mosConsumer.Post<ApiResultObject<string>>("api/CodeGeneration/MediRecordStoreCodeGetNextOption4", param, data);
                    if (rs == null || !rs.Success)
                    {
                        if (rs != null && rs.Param != null)
                        {
                            CommonUtil.AddParamInfo(rs.Param, param);
                        }
                        LogSystem.Warn("MOS mater api/CodeGeneration/MediRecordStoreCodeGetNextOption4 that bai");
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

        public static string MediRecordStoreCodeGetNextOption5(MediRecordStoreCodeGenerateSDO data, CommonParam param)
        {
            string result = null;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master" de sinh code
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    ApiConsumer mosConsumer = new ApiConsumer(SystemCFG.MASTER_ADDRESS, ResourceTokenManager.GetTokenCode(), MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE);
                    ApiResultObject<string> rs = mosConsumer.Post<ApiResultObject<string>>("api/CodeGeneration/MediRecordStoreCodeGetNextOption5", param, data);
                    if (rs == null || !rs.Success)
                    {
                        if (rs != null && rs.Param != null)
                        {
                            CommonUtil.AddParamInfo(rs.Param, param);
                        }
                        LogSystem.Warn("MOS mater api/CodeGeneration/MediRecordStoreCodeGetNextOption5 that bai");
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

        public static bool MediRecordStoreCodeFinishUpdateDB(List<string> inputs, CommonParam param)
        {
            bool result = false;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master"
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    ApiConsumer mosConsumer = new ApiConsumer(SystemCFG.MASTER_ADDRESS, ResourceTokenManager.GetTokenCode(), MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE);
                    ApiResultObject<bool> rs = mosConsumer.Post<ApiResultObject<bool>>("api/CodeGeneration/MediRecordStoreCodeFinishUpdateDB", param, inputs);
                    if (rs == null || !rs.Success)
                    {
                        if (rs != null && rs.Param != null)
                        {
                            CommonUtil.AddParamInfo(rs.Param, param);
                        }
                        LogSystem.Warn("MOS mater api/CodeGeneration/MediRecordStoreCodeFinishUpdateDB that bai");
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


        public static string BarcodeGetNext(long intructionTime, CommonParam param)
        {
            string result = null;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master" de sinh code
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    ApiConsumer mosConsumer = new ApiConsumer(SystemCFG.MASTER_ADDRESS, ResourceTokenManager.GetTokenCode(), MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE);
                    ApiResultObject<string> rs = mosConsumer.Post<ApiResultObject<string>>("api/CodeGeneration/BarcodeGetNext", param, intructionTime);
                    if (rs == null || !rs.Success)
                    {
                        if (rs != null && rs.Param != null)
                        {
                            CommonUtil.AddParamInfo(rs.Param, param);
                        }
                        LogSystem.Warn("MOS mater api/CodeGeneration/BarcodeGetNext that bai");
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

        public static bool BarcodeFinishUpdateDB(List<string> input, CommonParam param)
        {
            bool result = false;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master"
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    ApiConsumer mosConsumer = new ApiConsumer(SystemCFG.MASTER_ADDRESS, ResourceTokenManager.GetTokenCode(), MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE);
                    ApiResultObject<bool> rs = mosConsumer.Post<ApiResultObject<bool>>("api/CodeGeneration/BarcodeFinishUpdateDB", param, input);
                    if (rs == null || !rs.Success)
                    {
                        if (rs != null && rs.Param != null)
                        {
                            CommonUtil.AddParamInfo(rs.Param, param);
                        }
                        LogSystem.Warn("MOS mater api/CodeGeneration/BarcodeFinishUpdateDB that bai");
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

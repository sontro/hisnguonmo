using Inventec.Common.Logging;
using Inventec.A2Manager;
using System;
using Inventec.Common.Auth;

namespace ACS.LDP
{
    public abstract class BaseDataPriv<RAW>
    {
        public long NEGATIVE_ID = -1;

        public BaseDataPriv(string dataType)
        {
            DataType = dataType;
        }
        public string DataType { get; set; }

        public bool VerifyCreate(string moduleCode)
        {
            bool result = false;
            string dataGrant = "";
            try
            {
                dataGrant = A2.DetermineDataGrant(moduleCode, DataType, DataGrantTypeCodeConstant.CREATE);
                UserData user = A2.GetUserData();
                Workplace workplace = A2.GetWorkplace();
                if (user != null)
                {
                    switch (dataGrant)
                    {
                        case DataGrantCodeConstant.CREATE_ALL:
                            result = true;
                            break;
                        case DataGrantCodeConstant.CREATE_CHILD:
                            result = true;
                            break;
                        case DataGrantCodeConstant.CREATE_GROUP:
                            result = true;
                            break;
                        default:
                            result = false;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error("Kiem tra phan quyen VerifyCreate co exception." + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => moduleCode), moduleCode) + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => dataGrant), dataGrant), ex);
                result = false;
            }
            //return result;
            return true;
        }

        public bool VerifyUpdate(string moduleCode, string groupCode, string creator)
        {
            bool result = false;
            string dataGrant = "";
            try
            {
                dataGrant = A2.DetermineDataGrant(moduleCode, DataType, DataGrantTypeCodeConstant.UPDATE);
                UserData user = A2.GetUserData();
                Workplace workplace = A2.GetWorkplace();
                if (user != null)
                {
                    switch (dataGrant)
                    {
                        case DataGrantCodeConstant.UPDATE_ALL:
                            result = true;
                            break;
                        case DataGrantCodeConstant.UPDATE_CHILD:
                            result = workplace.LIST_GROUP_CODE.Contains(groupCode);
                            break;
                        case DataGrantCodeConstant.UPDATE_GROUP:
                            result = workplace.GROUP_CODE == groupCode;
                            break;
                        case DataGrantCodeConstant.UPDATE_MINE:
                            result = user.USER_NAME == creator;
                            break;
                        default:
                            result = false;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error("Kiem tra phan quyen VerifyUpdate co exception." + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => moduleCode), moduleCode) + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => groupCode), groupCode) + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => creator), creator) + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => dataGrant), dataGrant), ex);
                result = false;
            }
            //return result;
            return true;
        }

        public bool VerifyLock(string moduleCode, string groupCode, string creator)
        {
            bool result = false;
            string dataGrant = "";
            try
            {
                dataGrant = A2.DetermineDataGrant(moduleCode, DataType, DataGrantTypeCodeConstant.LOCK);
                UserData user = A2.GetUserData();
                Workplace workplace = A2.GetWorkplace();
                if (user != null)
                {
                    switch (dataGrant)
                    {
                        case DataGrantCodeConstant.LOCK_ALL:
                            result = true;
                            break;
                        case DataGrantCodeConstant.LOCK_CHILD:
                            result = workplace.LIST_GROUP_CODE.Contains(groupCode);
                            break;
                        case DataGrantCodeConstant.LOCK_GROUP:
                            result = workplace.GROUP_CODE == groupCode;
                            break;
                        case DataGrantCodeConstant.LOCK_MINE:
                            result = user.USER_NAME == creator;
                            break;
                        default:
                            result = false;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error("Kiem tra phan quyen VerifyLock co exception." + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => moduleCode), moduleCode) + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => groupCode), groupCode) + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => creator), creator) + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => dataGrant), dataGrant), ex);
                result = false;
            }
            //return result;
            return true;
        }

        public bool VerifyDelete(string moduleCode, string groupCode, string creator)
        {
            bool result = false;
            string dataGrant = "";
            try
            {
                dataGrant = A2.DetermineDataGrant(moduleCode, DataType, DataGrantTypeCodeConstant.DELETE);
                UserData user = A2.GetUserData();
                Workplace workplace = A2.GetWorkplace();
                if (user != null)
                {
                    switch (dataGrant)
                    {
                        case DataGrantCodeConstant.DELETE_ALL:
                            result = true;
                            break;
                        case DataGrantCodeConstant.DELETE_CHILD:
                            result = workplace.LIST_GROUP_CODE.Contains(groupCode);
                            break;
                        case DataGrantCodeConstant.DELETE_GROUP:
                            result = workplace.GROUP_CODE == groupCode;
                            break;
                        case DataGrantCodeConstant.DELETE_MINE:
                            result = user.USER_NAME == creator;
                            break;
                        default:
                            result = false;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error("Kiem tra phan quyen VerifyDelete co exception." + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => moduleCode), moduleCode) + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => groupCode), groupCode) + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => creator), creator) + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => dataGrant), dataGrant), ex);
                result = false;
            }
            //return result;
            return true;
        }

        public bool VerifyTruncate(string moduleCode, string groupCode, string creator)
        {
            bool result = false;
            string dataGrant = "";
            try
            {
                dataGrant = A2.DetermineDataGrant(moduleCode, DataType, DataGrantTypeCodeConstant.UPDATE);
                UserData user = A2.GetUserData();
                Workplace workplace = A2.GetWorkplace();
                if (user != null)
                {
                    switch (dataGrant)
                    {
                        case DataGrantCodeConstant.TRUNCATE_ALL:
                            result = true;
                            break;
                        case DataGrantCodeConstant.TRUNCATE_CHILD:
                            result = workplace.LIST_GROUP_CODE.Contains(groupCode);
                            break;
                        case DataGrantCodeConstant.TRUNCATE_GROUP:
                            result = workplace.GROUP_CODE == groupCode;
                            break;
                        case DataGrantCodeConstant.TRUNCATE_MINE:
                            result = user.USER_NAME == creator;
                            break;
                        default:
                            result = false;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error("Kiem tra phan quyen VerifyTruncate co exception." + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => moduleCode), moduleCode) + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => groupCode), groupCode) + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => creator), creator) + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => dataGrant), dataGrant), ex);
                result = false;
            }
            //return result;
            return true;
        }
    }
}

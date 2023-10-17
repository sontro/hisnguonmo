using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.Base
{
    public abstract class BusinessBase : EntityBase
    {
        public BusinessBase()
            : base()
        {
            param = new CommonParam();
            try
            {
                UserName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                param.Now = Inventec.Common.DateTime.Get.Now() ?? 0;
            }
            catch (Exception)
            {
            }
        }

        public BusinessBase(CommonParam paramBusiness)
            : base()
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
            try
            {
                UserName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                param.Now = Inventec.Common.DateTime.Get.Now() ?? 0;
            }
            catch (Exception)
            {
            }
        }

        protected CommonParam param { get; set; }

        protected void TroubleCheck()
        {
            try
            {
                if (param.HasException || (param.BugCodes != null && param.BugCodes.Count > 0))
                {
                    MethodName = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name;
                    TroubleCache.Add(GetInfoProcess() + (param.HasException ? "param.HasException." : "") + param.GetBugCode());
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public ApiResultObject<T> PackCollectionResult<T>(T listData)
        {
            ApiResultObject<T> result = new ApiResultObject<T>();
            try
            {
                result.SetValue(listData, listData != null, param);
            }
            catch (Exception ex)
            {
                Logging("Co exception khi dong goi ApiResultObject.", LogType.Error);
                LogSystem.Error(ex);
            }
            return result;
        }

        public ApiResultObject<T> PackResult<T>(T data, bool isSuccess)
        {
            ApiResultObject<T> result = new ApiResultObject<T>();
            try
            {
                result.SetValue(data, isSuccess, param);
            }
            catch (Exception ex)
            {
                Logging("Co exception khi dong goi ApiResultObject.", LogType.Error);
                LogSystem.Error(ex);
            }
            return result;
        }

        public ApiResultObject<T> PackSuccess<T>(T data)
        {
            ApiResultObject<T> result = new ApiResultObject<T>();
            try
            {
                result.SetValue(data, true, param);
            }
            catch (Exception ex)
            {
                Logging("Co exception khi dong goi ApiResultObject.", LogType.Error);
                LogSystem.Error(ex);
            }
            return result;
        }

        public ApiResultObject<T> PackSingleResult<T>(T data)
        {
            ApiResultObject<T> result = new ApiResultObject<T>();
            try
            {
                if (typeof(T) == typeof(bool))
                {
                    bool t = bool.Parse(data.ToString());
                    result.SetValue(data, t, param);
                }
                else
                {
                    result.SetValue(data, data != null, param);
                }
            }
            catch (Exception ex)
            {
                Logging("Co exception khi dong goi ApiResultObject.", LogType.Error);
                LogSystem.Error(ex);
            }
            return result;
        }

        internal bool HasWorkPlaceInfo(long roomId, ref WorkPlaceSDO workPlace)
        {
            try
            {
                workPlace = TokenManager.GetWorkPlace(roomId);
                if (workPlace == null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.KhongCoThongTinPhongLamViec);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
        }

        internal bool IsWorkingAtRoom(long roomId, long workingRoomId)
        {
            if (roomId != workingRoomId)
            {
                V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == roomId).FirstOrDefault();
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.BanDangKhongLamViecTaiPhong, room.ROOM_NAME);
                return false;
            }
            return true;
        }

        internal bool IsWorkingAtDepartment(long departmentId, long workingDepartmentId)
        {
            if (workingDepartmentId != departmentId)
            {
                HIS_DEPARTMENT department = HisDepartmentCFG.DATA.Where(o => o.ID == departmentId).FirstOrDefault();
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.BanDangKhongLamViecTaiKhoa, department.DEPARTMENT_NAME);
                return false;
            }
            return true;
        }

        internal List<string> GetBugCodes()
        {
            return this.param != null ? this.param.BugCodes : null;
        }

        internal List<string> GetMessages()
        {
            return this.param != null ? this.param.Messages : null;
        }

        internal List<string> GetMessageCodes()
        {
            return this.param != null ? this.param.MessageCodes : null;
        }
    }
}

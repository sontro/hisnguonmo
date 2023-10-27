using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using ACS.SDO;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsUser.CheckActive
{
    class AcsUserCheckActiveBehaviorEv : BeanObjectBase, IAcsUserCheckActive
    {
        string entity;

        internal AcsUserCheckActiveBehaviorEv(CommonParam param, string data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsUserCheckActive.Run()
        {
            bool result = false;
            try
            {
                ACS_USER user = Valid() ? new AcsUserBO().Get<ACS_USER>(entity.ToLower()) : null;
                if (user == null)
                {
                    ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Common__ThieuThongTinBatBuoc);
                    Inventec.Common.Logging.LogSystem.Warn("Kiem tra du lieu user khong hop le. Kiem tra thong tin kich hoat tai khoan that bai. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                }
                else
                {
                    result = CheckUserIsActive(user);
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

        bool Valid()
        {
            return !String.IsNullOrEmpty(entity);
        }

        /// <summary>
        /// Ham kiem tra tai khoan co bi tam khoa hay khong
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckUserIsActive(ACS_USER data)
        {
            bool valid = false;
            try
            {

                valid = (data != null && data.ID > 0 && data.IS_ACTIVE == IMSys.DbConfig.SDA_RS.COMMON.IS_ACTIVE__TRUE);
                if (!valid && data != null)
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_TaiKhoanDangBiTamKhoa);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}

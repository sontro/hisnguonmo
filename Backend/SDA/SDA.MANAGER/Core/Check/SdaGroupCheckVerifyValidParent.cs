using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;
using SDA.MANAGER.Core.SdaGroup;
using Inventec.Common.Logging;

namespace SDA.MANAGER.Core.Check
{
    class SdaGroupCheckVerifyValidParent
    {
        internal static bool Verify(CommonParam param, SDA_GROUP data)
        {
            bool result = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
            }
            catch (ArgumentNullException ex)
            {
                SDA.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Warn(ex);
                result = false;


                if (data.PARENT_ID.HasValue)
                {
                    SDA_GROUP parent = new SdaGroupBO().Get<SDA_GROUP>(data.PARENT_ID.Value);
                    if (parent == null)
                    {
                        result = false;
                        SDA.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
                        Inventec.Common.Logging.LogSystem.Error("Group co cha nhung ko truy van duoc." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data));
                    }
                    else if (String.IsNullOrEmpty(parent.CODE_PATH) || String.IsNullOrEmpty(parent.ID_PATH))
                    {
                        result = false;
                        SDA.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.SdaGroup_DonViKhongCoPath);
                        Inventec.Common.Logging.LogSystem.Error("Du lieu don vi cha khong co path." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => parent), parent));
                    }
                    else if (parent.ID_PATH.Contains(data.ID_PATH))
                    {
                        result = false;
                        SDA.MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.SdaGroup_ChaMoiCuaDonViKhongDuocLaConHienTai);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }
    }
}

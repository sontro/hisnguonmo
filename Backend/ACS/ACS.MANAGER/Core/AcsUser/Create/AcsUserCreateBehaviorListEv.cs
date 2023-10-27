using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsUser.Create
{
    class AcsUserCreateBehaviorListEv : BeanObjectBase, IAcsUserCreate
    {
        List<ACS_USER> entities;

        internal AcsUserCreateBehaviorListEv(CommonParam param, List<ACS_USER> datas)
            : base(param)
        {
            entities = datas;
        }

        bool IAcsUserCreate.Run()
        {
            bool result = false;
            try
            {
                if (Check())
                {
                    if (!DAOWorker.AcsUserDAO.CreateList(entities))
                    {
                        ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__ThemMoiThatBai);
                        Inventec.Common.Logging.LogSystem.Warn("Tao moi danh sach nguoi dung that bai. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entities), entities));
                    }
                    else
                    {
                        result = true;
                        entities.ForEach(o => o.LOGINNAME = "");
                    }
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

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && AcsUserCheckVerifyValidData.Verify(param, entities);

                if (result)
                {
                    foreach (var user in entities)
                    {
                        string pass = ACS.UTILITY.Password.GeneratePassword();
                        if (LibraryConfig.WebConfig.IS_APPLICATION_GENERATE_PASSWORD)
                        {
                            pass = user.LOGINNAME;
                        }
                        user.PASSWORD = new MOS.EncryptPassword.Cryptor().EncryptPassword(pass, user.LOGINNAME);
                    }
                }
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}

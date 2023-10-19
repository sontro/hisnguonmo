using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using ACS.SDO;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsUser.Update
{
    class AcsUserUpdateBehaviorForSubLogin : BeanObjectBase, IAcsUserUpdate
    {
        AcsUserUpdateLoginNameTDO entity;
        ACS_USER raw;

        internal AcsUserUpdateBehaviorForSubLogin(CommonParam param, AcsUserUpdateLoginNameTDO data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsUserUpdate.Run()
        {
            bool result = false;
            try
            {
                if (Check())
                {
                    //raw.SUB_LOGINNAME = entity.SubLoginName.ToLower();
                    result = DAOWorker.AcsUserDAO.Update(raw);
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
                raw = new ACS_USER();
                result = result && AcsUserCheckVerifyValidData.Verify(param, entity);
                result = result && AcsUserCheckVerifyValidDataForLogin.Verify(param, ref raw, entity.LoginName, entity.Password);
                result = result && AcsUserCheckVerifyIsUnlock.Verify(param, raw);
                result = result && AcsUserCheckVerifyExistsCode.Verify(param, entity.SubLoginName, raw.ID);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}

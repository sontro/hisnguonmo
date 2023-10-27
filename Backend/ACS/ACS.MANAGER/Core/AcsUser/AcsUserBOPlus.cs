using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsUser.CheckResetPasswordB;
using ACS.MANAGER.Core.AcsUser.CopyRole;
using ACS.MANAGER.Core.AcsUser.ResetPasswordB;
using ACS.SDO;
using Inventec.Core;
using Inventec.Token.AuthSystem;
using Inventec.Token.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsUser
{
    partial class AcsUserBO : BusinessObjectBase
    {
        internal bool ChangePassword(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new AcsUserChangePassword(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool ChangePasswordWithOtp(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new AcsUserChangePasswordWithOtp(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool ResetPassword(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new AcsUserResetPassword(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal object ResetPasswordB(object data)
        {
            object result = null;
            try
            {
                IAcsUserResetPasswordB delegacy = new AcsUserResetPasswordB(param, data);
                result = delegacy.Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        internal object CheckResetPasswordB(object data)
        {
            object result = null;
            try
            {
                IAcsUserCheckResetPasswordB delegacy = new AcsUserCheckResetPasswordB(param, data);
                result = delegacy.Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        internal object CopyRole(object data)
        {
            object result = null;
            try
            {
                IAcsUserCopyRole delegacy = new AcsUserCopyRole(param, data);
                result = delegacy.Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        internal List<object> GetDynamic(object filter)
        {
            List<object> result = new List<object>();
            try
            {
                IDelegacyT delegacy = new AcsUserGetDynamic(param, filter);
                result = delegacy.Execute<List<object>>();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = new List<object>();
            }
            return result;
        }

        internal bool CheckActive(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new AcsUserCheckActive(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool ActivationRequired(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new AcsUserActivationRequired(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool ActivationRequiredWithMessage(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new AcsUserActivationRequiredWithMessage(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool Activate(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new AcsUserActivate(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool InActive(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new AcsUserInActive(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool UpdateListInfo(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new AcsUserUpdateListInfo(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}

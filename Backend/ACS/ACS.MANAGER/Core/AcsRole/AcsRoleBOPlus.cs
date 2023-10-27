using System;

namespace ACS.MANAGER.Core.AcsRole
{
    partial class AcsRoleBO : BusinessObjectBase
    {
        internal T GetRoleBase<T>(object data)
        {
            T result = default(T);
            try
            {
                IDelegacyT delegacy = new AcsRoleRoleBaseGet(param, data);
                result = delegacy.Execute<T>();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = default(T);
            }
            return result;
        }

        internal bool CreateWithRoleBase(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new AcsRoleRoleBaseMake(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool UpdateWithRoleBase(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new AcsRoleRoleBaseUpdate(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool DeleteWithRoleBase(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new AcsRoleRoleBaseDelete(param, data);
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

using System;

namespace ACS.MANAGER.Core.AcsRoleUser
{
    partial class AcsRoleUserBO : BusinessObjectBase
    {
        internal AcsRoleUserBO()
            : base()
        {

        }

        internal T Get<T>(object data)
        {
            T result = default(T);
            try
            {
                IDelegacyT delegacy = new AcsRoleUserGet(param, data);
                result = delegacy.Execute<T>();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = default(T);
            }
            return result;
        }

        internal bool Create(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new AcsRoleUserCreate(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool Update(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new AcsRoleUserUpdate(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool ChangeLock(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new AcsRoleUserChangeLock(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool Delete(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new AcsRoleUserDelete(param, data);
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

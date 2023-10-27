using System;

namespace ACS.MANAGER.Core.AcsUser
{
    partial class AcsUserBO : BusinessObjectBase
    {
        internal AcsUserBO()
            : base()
        {

        }

        internal T Get<T>(object data)
        {
            T result = default(T);
            try
            {
                IDelegacyT delegacy = new AcsUserGet(param, data);
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
                IDelegacy delegacy = new AcsUserCreate(param, data);
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
                IDelegacy delegacy = new AcsUserUpdate(param, data);
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
                IDelegacy delegacy = new AcsUserChangeLock(param, data);
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
                IDelegacy delegacy = new AcsUserDelete(param, data);
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

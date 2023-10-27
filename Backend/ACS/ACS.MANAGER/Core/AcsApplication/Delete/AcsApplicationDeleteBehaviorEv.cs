using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsAppOtpType.Get;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsApplication.Delete
{
    class AcsApplicationDeleteBehaviorEv : BeanObjectBase, IAcsApplicationDelete
    {
        ACS_APPLICATION entity;

        internal AcsApplicationDeleteBehaviorEv(CommonParam param, ACS_APPLICATION data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsApplicationDelete.Run()
        {
            bool result = false;
            try
            {
                if (Check())
                {
                    DeleteDetailAppData();
                    result = DAOWorker.AcsApplicationDAO.Truncate(entity);
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

        bool DeleteDetailAppData()
        {
            bool success = false;
            try
            {
                AcsAppOtpTypeFilterQuery appOtpTypeFilterQuery = new AcsAppOtpTypeFilterQuery();
                appOtpTypeFilterQuery.APPLICSTION_ID = entity.ID;
                var appOtpTypes = DAOWorker.AcsAppOtpTypeDAO.Get(appOtpTypeFilterQuery.Query(), new CommonParam());
                if (appOtpTypes != null && appOtpTypes.Count > 0)
                {
                    if (!DAOWorker.AcsAppOtpTypeDAO.TruncateList(appOtpTypes))
                    {
                        param.Messages.Add("Xoa danh sách dữ liệu AcsAppOtpType theo ứng dụng de khoi tao lai thất bại");
                    }
                    else
                        success = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return success;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && AcsApplicationCheckVerifyIsUnlock.Verify(param, entity.ID);
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

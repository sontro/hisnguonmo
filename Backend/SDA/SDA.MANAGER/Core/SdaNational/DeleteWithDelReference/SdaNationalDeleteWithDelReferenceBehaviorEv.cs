using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;
using SDA.MANAGER.Core.SdaProvince;

namespace SDA.MANAGER.Core.SdaNational.DeleteWithDelReference
{
    class SdaNationalDeleteWithDelReferenceBehaviorEv : BeanObjectBase, ISdaNationalDeleteWithDelReference
    {
        SDA_NATIONAL entity;

        internal SdaNationalDeleteWithDelReferenceBehaviorEv(CommonParam param, SDA_NATIONAL data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaNationalDeleteWithDelReference.Run()
        {
            bool result = false;
            try
            {
                if (Check())
                {
                    result = DAOWorker.SdaNationalDAO.Truncate(entity);
                    if (result)
                    {
                        List<long> listId = new List<long>();
                        listId.Add(entity.ID);
                        DeleteProvince(listId);
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

        private void DeleteProvince(List<long> nationalIds)
        {
            try
            {
                if (!new SdaProvinceBO().DeleteWithDelReference(nationalIds))
                {
                    Logging("Khong xoa duoc cac tinh thanh thuoc quoc gia da xoa." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => nationalIds), nationalIds), LogType.Error);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error("Khong xoa duoc cac tinh thanh thuoc quoc gia da xoa." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => nationalIds), nationalIds), ex);
            }
        }

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && SdaNationalCheckVerifyIsUnlock.Verify(param, entity.ID);
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

using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaProvince.DeleteWithDelReference
{
    class SdaProvinceDeleteWithDelReferenceBehaviorEv : BeanObjectBase, ISdaProvinceDeleteWithDelReference
    {
        SDA_PROVINCE entity;
        List<SDA_PROVINCE> listRaw;
        List<long> listId;

        internal SdaProvinceDeleteWithDelReferenceBehaviorEv(CommonParam param, SDA_PROVINCE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaProvinceDeleteWithDelReference.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaProvinceDAO.Truncate(entity);
                if (result)
                {
                    listId = new List<long>();
                    listId.Add(entity.ID);
                    DeleteDistrict(listId);
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

        private void DeleteDistrict(List<long> provinceIds)
        {
            try
            {
                if (!new SdaDistrict.SdaDistrictBO().Delete(provinceIds))
                {
                    Logging("Khong xoa duoc cac quan huyen thuoc tinh thanh da xoa." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => provinceIds), provinceIds), LogType.Error);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error("Khong xoa duoc cac quan huyen thuoc tinh thanh da xoa." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => provinceIds), provinceIds), ex);
            }
        }

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && SdaProvinceCheckVerifyIsUnlock.Verify(param, entity.ID);
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

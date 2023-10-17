using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using SDA.MANAGER.Core.SdaCommune;

namespace SDA.MANAGER.Core.SdaDistrict.Delete
{
    class SdaDistrictDeleteBehaviorByProvince : BeanObjectBase, ISdaDistrictDelete
    {
        List<long> entity;

        internal SdaDistrictDeleteBehaviorByProvince(CommonParam param, List<long> data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaDistrictDelete.Run()
        {
            bool result = false;
            try
            {
                if (Check())
                {
                    List<SDA_DISTRICT> listRaw = new SdaDistrictBO().Get<List<SDA_DISTRICT>>(entity);
                    if (listRaw != null && listRaw.Count > 0)
                    {
                        List<long> ids = listRaw.Select(o => o.ID).ToList();
                        result = DAOWorker.SdaDistrictDAO.TruncateList(listRaw);
                        if (result) DeleteCommune(ids);
                    }
                    else
                    {
                        result = true;
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

        private void DeleteCommune(List<long> districtIds)
        {
            try
            {
                if (!new SdaCommuneBO().Delete(districtIds))
                {
                    Logging("Khong xoa duoc cac xa phuong thuoc quan huyen da xoa." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => districtIds), districtIds), LogType.Error);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error("Khong xoa duoc cac xa phuong thuoc quan huyen da xoa." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => districtIds), districtIds), ex);
            }
        }

        bool Check()
        {
            bool result = true;
            try
            {
                if (entity == null || entity.Count == 0)
                {
                    throw new ArgumentNullException("entity is null");
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
    }
}

using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBloodVolume
{
    partial class HisBloodVolumeGet : EntityBase
    {
        public HIS_BLOOD_VOLUME GetByCode(string code, HisBloodVolumeSO search)
        {
            HIS_BLOOD_VOLUME result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_BLOOD_VOLUME.AsQueryable().Where(p => p.BLOOD_VOLUME_CODE == code);
                        if (search.listHisBloodVolumeExpression != null && search.listHisBloodVolumeExpression.Count > 0)
                        {
                            foreach (var item in search.listHisBloodVolumeExpression)
                            {
                                query = query.Where(item);
                            }
                        }
                        result = query.SingleOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => code), code) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search), LogType.Error);
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}

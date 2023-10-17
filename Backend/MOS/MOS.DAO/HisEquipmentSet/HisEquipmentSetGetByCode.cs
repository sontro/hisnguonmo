using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEquipmentSet
{
    partial class HisEquipmentSetGet : EntityBase
    {
        public HIS_EQUIPMENT_SET GetByCode(string code, HisEquipmentSetSO search)
        {
            HIS_EQUIPMENT_SET result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_EQUIPMENT_SET.AsQueryable().Where(p => p.EQUIPMENT_SET_CODE == code);
                        if (search.listHisEquipmentSetExpression != null && search.listHisEquipmentSetExpression.Count > 0)
                        {
                            foreach (var item in search.listHisEquipmentSetExpression)
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

using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEquipmentSetMaty
{
    partial class HisEquipmentSetMatyGet : EntityBase
    {
        public HIS_EQUIPMENT_SET_MATY GetByCode(string code, HisEquipmentSetMatySO search)
        {
            HIS_EQUIPMENT_SET_MATY result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_EQUIPMENT_SET_MATY.AsQueryable().Where(p => p.EQUIPMENT_SET_MATY_CODE == code);
                        if (search.listHisEquipmentSetMatyExpression != null && search.listHisEquipmentSetMatyExpression.Count > 0)
                        {
                            foreach (var item in search.listHisEquipmentSetMatyExpression)
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

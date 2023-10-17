using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineBean
{
    partial class HisMedicineBeanGet : GetBase
    {
        internal List<L_HIS_MEDICINE_BEAN> GetLView(HisMedicineBeanLViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicineBeanDAO.GetLView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<L_HIS_MEDICINE_BEAN> GetLViewByIds(List<long> ids)
        {
            try
            {
                if (ids != null)
                {
                    HisMedicineBeanLViewFilterQuery filter = new HisMedicineBeanLViewFilterQuery();
                    filter.IDs = ids;
                    return this.GetLView(filter);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
   
        internal L_HIS_MEDICINE_BEAN GetLViewById(long id)
        {
            try
            {
                return GetLViewById(id, new HisMedicineBeanLViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal L_HIS_MEDICINE_BEAN GetLViewById(long id, HisMedicineBeanLViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicineBeanDAO.GetLViewById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}

using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialBean
{
    partial class HisMaterialBeanGet : GetBase
    {
        internal List<L_HIS_MATERIAL_BEAN> GetLView(HisMaterialBeanLViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMaterialBeanDAO.GetLView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<L_HIS_MATERIAL_BEAN> GetLViewByIds(List<long> ids)
        {
            try
            {
                if (ids != null)
                {
                    HisMaterialBeanLViewFilterQuery filter = new HisMaterialBeanLViewFilterQuery();
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

        internal L_HIS_MATERIAL_BEAN GetLViewById(long id)
        {
            try
            {
                return GetLViewById(id, new HisMaterialBeanLViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal L_HIS_MATERIAL_BEAN GetLViewById(long id, HisMaterialBeanLViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMaterialBeanDAO.GetLViewById(id, filter.Query());
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

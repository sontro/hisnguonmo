using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediStockMaty
{
    class HisMediStockMatyGet : GetBase
    {
        internal HisMediStockMatyGet()
            : base()
        {

        }

        internal HisMediStockMatyGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MEDI_STOCK_MATY> Get(HisMediStockMatyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediStockMatyDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_MEDI_STOCK_MATY> GetView(HisMediStockMatyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediStockMatyDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_MEDI_STOCK_MATY_1> GetView1(HisMediStockMatyView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediStockMatyDAO.GetView1(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDI_STOCK_MATY GetById(long id)
        {
            try
            {
                return GetById(id, new HisMediStockMatyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDI_STOCK_MATY GetById(long id, HisMediStockMatyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediStockMatyDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MEDI_STOCK_MATY GetViewById(long id)
        {
            try
            {
                return GetViewById(id, new HisMediStockMatyViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MEDI_STOCK_MATY GetViewById(long id, HisMediStockMatyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediStockMatyDAO.GetViewById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MEDI_STOCK_MATY_1 GetView1ById(long id)
        {
            try
            {
                return GetView1ById(id, new HisMediStockMatyView1FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MEDI_STOCK_MATY_1 GetView1ById(long id, HisMediStockMatyView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediStockMatyDAO.GetView1ById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_MEDI_STOCK_MATY> GetByMaterialTypeId(long id)
        {
            try
            {
                HisMediStockMatyFilterQuery filter = new HisMediStockMatyFilterQuery();
                filter.MATERIAL_TYPE_ID = id;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_MEDI_STOCK_MATY> GetByMediStockId(long mediStockId)
        {
            try
            {
                HisMediStockMatyFilterQuery filter = new HisMediStockMatyFilterQuery();
                filter.MEDI_STOCK_ID = mediStockId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_MEDI_STOCK_MATY> GetByMediStockIds(List<long> mediStockIds)
        {
            try
            {
                if (mediStockIds != null)
                {
                    HisMediStockMatyFilterQuery filter = new HisMediStockMatyFilterQuery();
                    filter.MEDI_STOCK_IDs = mediStockIds;
                    return this.Get(filter);
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

        internal List<V_HIS_MEDI_STOCK_MATY_1> GetView1ByMediStockId(long mediStockId)
        {
            try
            {
                HisMediStockMatyView1FilterQuery filter = new HisMediStockMatyView1FilterQuery();
                filter.MEDI_STOCK_ID = mediStockId;
                return this.GetView1(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_MEDI_STOCK_MATY_1> GetView1ByMediStockIds(List<long> materialTypeIds)
        {
            try
            {
                if (materialTypeIds != null)
                {
                    HisMediStockMatyView1FilterQuery filter = new HisMediStockMatyView1FilterQuery();
                    filter.MATERIAL_TYPE_IDs = materialTypeIds;
                    return this.GetView1(filter);
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
    }
}

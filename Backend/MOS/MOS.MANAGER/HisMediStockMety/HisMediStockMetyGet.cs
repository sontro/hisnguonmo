using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediStockMety
{
    class HisMediStockMetyGet : GetBase
    {
        internal HisMediStockMetyGet()
            : base()
        {

        }

        internal HisMediStockMetyGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MEDI_STOCK_METY> Get(HisMediStockMetyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediStockMetyDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_MEDI_STOCK_METY> GetView(HisMediStockMetyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediStockMetyDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_MEDI_STOCK_METY_1> GetView1(HisMediStockMetyView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediStockMetyDAO.GetView1(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDI_STOCK_METY GetById(long id)
        {
            try
            {
                return GetById(id, new HisMediStockMetyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDI_STOCK_METY GetById(long id, HisMediStockMetyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediStockMetyDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MEDI_STOCK_METY GetViewById(long id)
        {
            try
            {
                return GetViewById(id, new HisMediStockMetyViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MEDI_STOCK_METY GetViewById(long id, HisMediStockMetyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediStockMetyDAO.GetViewById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MEDI_STOCK_METY_1 GetView1ById(long id)
        {
            try
            {
                return GetView1ById(id, new HisMediStockMetyView1FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MEDI_STOCK_METY_1 GetView1ById(long id, HisMediStockMetyView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediStockMetyDAO.GetView1ById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_MEDI_STOCK_METY> GetByMediStockId(long mediStockId)
        {
            try
            {
                HisMediStockMetyFilterQuery filter = new HisMediStockMetyFilterQuery();
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

        internal List<HIS_MEDI_STOCK_METY> GetByMedicineTypeId(long id)
        {
            try
            {
                HisMediStockMetyFilterQuery filter = new HisMediStockMetyFilterQuery();
                filter.MEDICINE_TYPE_ID = id;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_MEDI_STOCK_METY> GetByMediStockIds(List<long> mediStockIds)
        {
            try
            {
                if (mediStockIds != null)
                {
                    HisMediStockMetyFilterQuery filter = new HisMediStockMetyFilterQuery();
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

        internal List<V_HIS_MEDI_STOCK_METY_1> GetView1ByMediStockId(long mediStockId)
        {
            try
            {
                HisMediStockMetyView1FilterQuery filter = new HisMediStockMetyView1FilterQuery();
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

        internal List<V_HIS_MEDI_STOCK_METY_1> GetView1ByMedicineTypeIds(List<long> medicineTypeIds)
        {
            try
            {
                if (medicineTypeIds != null)
                {
                    HisMediStockMetyView1FilterQuery filter = new HisMediStockMetyView1FilterQuery();
                    filter.MEDICINE_TYPE_IDs = medicineTypeIds;
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

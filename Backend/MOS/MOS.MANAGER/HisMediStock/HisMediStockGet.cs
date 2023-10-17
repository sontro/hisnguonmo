using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMediStock
{
    partial class HisMediStockGet : GetBase
    {
        internal HisMediStockGet()
            : base()
        {

        }

        internal HisMediStockGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MEDI_STOCK> Get(HisMediStockFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediStockDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_MEDI_STOCK> GetView(HisMediStockViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediStockDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDI_STOCK GetById(long id)
        {
            try
            {
                return GetById(id, new HisMediStockFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDI_STOCK GetById(long id, HisMediStockFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediStockDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MEDI_STOCK GetViewById(long id)
        {
            try
            {
                return GetViewById(id, new HisMediStockViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDI_STOCK GetByRoomId(long roomId)
        {
            try
            {
                HisMediStockFilterQuery filter = new HisMediStockFilterQuery();
                filter.ROOM_ID = roomId;
                List<HIS_MEDI_STOCK> hisMediStockDTOs = Get(filter);
                return hisMediStockDTOs != null ? hisMediStockDTOs[0] : null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_MEDI_STOCK> GetByRoomIds(List<long> roomIds)
        {
            if (IsNotNullOrEmpty(roomIds))
            {
                HisMediStockFilterQuery filter = new HisMediStockFilterQuery();
                filter.ROOM_IDs = roomIds;
                return Get(filter);
            }
            return null;
        }

        internal V_HIS_MEDI_STOCK GetViewById(long id, HisMediStockViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediStockDAO.GetViewById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDI_STOCK GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisMediStockFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDI_STOCK GetByCode(string code, HisMediStockFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediStockDAO.GetByCode(code, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MEDI_STOCK GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisMediStockViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MEDI_STOCK GetViewByCode(string code, HisMediStockViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediStockDAO.GetViewByCode(code, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_MEDI_STOCK> GetByParentId(long id)
        {
            HisMediStockFilterQuery filter = new HisMediStockFilterQuery();
            filter.PARENT_ID = id;
            return this.Get(filter);
        }

        internal List<HIS_MEDI_STOCK> GetActive()
        {
            HisMediStockFilterQuery filter = new HisMediStockFilterQuery();
            filter.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
            return this.Get(filter);
        }

        internal List<V_HIS_MEDI_STOCK> GetViewActive()
        {
            HisMediStockViewFilterQuery filter = new HisMediStockViewFilterQuery();
            filter.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
            return this.GetView(filter);
        }

        internal HisMediStockReplaceSDO GetReplaceSDO(HisMediStockReplaceSDOFilter filter)
        {
            HisMediStockReplaceSDO result = null;
            try
            {
                result = new HisMediStockReplaceSDO();
                result.MaterialReplaces = new List<L_HIS_EXP_MEST_MATERIAL>();
                result.MedicineReplaces = new List<L_HIS_EXP_MEST_MEDICINE>();
                if (IsNotNullOrEmpty(filter.MedicineTypeIds))
                {
                    HisExpMestMedicineLViewFilterQuery medicineFilter = new HisExpMestMedicineLViewFilterQuery();
                    medicineFilter.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS;
                    medicineFilter.MEDICINE_TYPE_IDs = filter.MedicineTypeIds;
                    medicineFilter.TDL_MEDI_STOCK_ID = filter.MediStockId;
                    medicineFilter.IS_REPLACE = true;

                    List<L_HIS_EXP_MEST_MEDICINE> medicines = new HisExpMestMedicineGet().GetLView(medicineFilter);
                    if (IsNotNullOrEmpty(medicines))
                    {
                        result.MedicineReplaces = medicines.OrderByDescending(o => o.APPROVAL_TIME).GroupBy(g => g.MEDICINE_TYPE_ID).Select(s => s.FirstOrDefault()).ToList();
                    }
                }

                if (IsNotNullOrEmpty(filter.MaterialTypeIds))
                {
                    HisExpMestMaterialLViewFilterQuery materialFilter = new HisExpMestMaterialLViewFilterQuery();
                    materialFilter.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS;
                    materialFilter.MATERIAL_TYPE_IDs = filter.MaterialTypeIds;
                    materialFilter.TDL_MEDI_STOCK_ID = filter.MediStockId;
                    materialFilter.IS_REPLACE = true;

                    List<L_HIS_EXP_MEST_MATERIAL> materials = new HisExpMestMaterialGet().GetLView(materialFilter);
                    if (IsNotNullOrEmpty(materials))
                    {
                        result.MaterialReplaces = materials.OrderByDescending(o => o.APPROVAL_TIME).GroupBy(g => g.MATERIAL_TYPE_ID).Select(s => s.FirstOrDefault()).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}

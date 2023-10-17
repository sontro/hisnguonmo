using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.UTILITY;

namespace MOS.MANAGER.HisRoom
{
    class HisRoomGet : GetBase
    {
        internal HisRoomGet()
            : base()
        {

        }

        internal HisRoomGet(Inventec.Core.CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_ROOM> Get(HisRoomFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRoomDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_ROOM> GetActive()
        {
            HisRoomFilterQuery filter = new HisRoomFilterQuery();
            filter.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
            return this.Get(filter);
        }

        internal List<V_HIS_ROOM> GetViewActive()
        {
            HisRoomViewFilterQuery filter = new HisRoomViewFilterQuery();
            filter.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
            return this.GetView(filter);
        }

        internal List<V_HIS_ROOM> GetView(HisRoomViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRoomDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_ROOM_COUNTER> GetCounterView(HisRoomCounterViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRoomDAO.GetCounterView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<L_HIS_ROOM_COUNTER> GetCounterLView(HisRoomCounterLViewFilterQuery filter)
        {
            try
            {
                //Mang y nghia la chi lay ra cac phong co the xu ly duoc toan bo 
                //cac dich vu nay (theo cau hinh dich vu - phong)
                if (IsNotNullOrEmpty(filter.SERVICE_IDs))
                {
                    filter.SERVICE_IDs = filter.SERVICE_IDs.Distinct().ToList();

                    var groups = HisServiceRoomCFG.DATA_VIEW != null ? HisServiceRoomCFG.DATA_VIEW.Where(o => o.IS_ACTIVE == Constant.IS_TRUE).GroupBy(o => o.ROOM_ID) : null;
                    List<long> roomIds = new List<long>();
                    foreach (var g in groups)
                    {
                        List<long> serviceIds = g.Select(o => o.SERVICE_ID).ToList();

                        //Neu phong do co the xu ly duoc het toan bo cac service_id thi add vao d/s
                        if (CommonUtil.Contains(serviceIds, filter.SERVICE_IDs))
                        {
                            roomIds.Add(g.Key);
                        }
                    }

                    if (!IsNotNullOrEmpty(roomIds))
                    {
                        return null;
                    }

                    filter.ROOM_IDs = roomIds;
                }
                return DAOWorker.HisRoomDAO.GetCounterLView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_ROOM_COUNTER_1> GetCounter1View(HisRoomCounter1ViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRoomDAO.GetCounter1View(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_ROOM_COUNTER_1> GetCounter1ViewByIds(List<long> ids)
        {
            if (IsNotNullOrEmpty(ids))
            {
                HisRoomCounter1ViewFilterQuery filter = new HisRoomCounter1ViewFilterQuery();
                filter.IDs = ids;
                return this.GetCounter1View(filter);
            }
            return null;
        }

        internal HIS_ROOM GetById(long id)
        {
            try
            {
                return GetById(id, new HisRoomFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_ROOM> GetByIds(List<long> ids)
        {
            try
            {
                HisRoomFilterQuery filter = new HisRoomFilterQuery();
                filter.IDs = ids;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        internal HIS_ROOM GetById(long id, HisRoomFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRoomDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ROOM GetViewById(long id)
        {
            try
            {
                return GetViewById(id, new HisRoomViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_ROOM> GetViewByIds(List<long> ids)
        {
            if (IsNotNullOrEmpty(ids))
            {
                HisRoomViewFilterQuery filter = new HisRoomViewFilterQuery();
                filter.IDs = ids;
                this.GetView(filter);
            }
            return null;
        }

        internal V_HIS_ROOM GetViewById(long id, HisRoomViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRoomDAO.GetViewById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ROOM GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisRoomViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ROOM GetViewByCode(string code, HisRoomViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRoomDAO.GetViewByCode(code, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_ROOM> GetByDepartmentId(long departmentId)
        {
            try
            {
                HisRoomFilterQuery filter = new HisRoomFilterQuery();
                filter.DEPARTMENT_ID = departmentId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_ROOM> GetByRoomTypeId(long id)
        {
            try
            {
                HisRoomFilterQuery filter = new HisRoomFilterQuery();
                filter.ROOM_TYPE_ID = id;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_ROOM> GetByRoomGroupId(long id)
        {
            HisRoomFilterQuery filter = new HisRoomFilterQuery();
            filter.ROOM_GROUP_ID = id;
            return this.Get(filter);
        }

        internal List<HIS_ROOM> GetBySpecialityId(long specialityId)
        {
            try
            {
                HisRoomFilterQuery filter = new HisRoomFilterQuery();
                filter.SPECIALITY_ID = specialityId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<L_HIS_ROOM_COUNTER_1> GetCounterLView1(HisRoomCounterLView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRoomDAO.GetCounterLView1(filter.Query(), param);
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

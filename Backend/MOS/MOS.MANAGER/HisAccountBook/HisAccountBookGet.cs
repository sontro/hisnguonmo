using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAccountBook
{
    class HisAccountBookGet : GetBase
    {
        internal HisAccountBookGet()
            : base()
        {

        }

        internal HisAccountBookGet(Inventec.Core.CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_ACCOUNT_BOOK> Get(HisAccountBookFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAccountBookDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_ACCOUNT_BOOK> GetView(HisAccountBookViewFilterQuery filter)
        {
            try
            {
                List<long> ids = null;

                if (!String.IsNullOrWhiteSpace(filter.LOGINNAME))
                {
                    if (ids == null)
                    {
                        ids = new List<long>();
                    }
                    List<long> byLoginnameIds = DAOWorker.SqlDAO.GetSql<long>("SELECT ACCOUNT_BOOK_ID FROM HIS_USER_ACCOUNT_BOOK WHERE LOGINNAME = :param1", filter.LOGINNAME);
                    if (byLoginnameIds != null && byLoginnameIds.Count > 0)
                    {
                        ids.AddRange(byLoginnameIds);
                    }

                    List<long> byCreatorIds = DAOWorker.SqlDAO.GetSql<long>("SELECT ID FROM HIS_ACCOUNT_BOOK WHERE CREATOR = :param1", filter.LOGINNAME);
                    if (byCreatorIds != null && byCreatorIds.Count > 0)
                    {
                        ids.AddRange(byCreatorIds);
                    }
                }

                if (filter.CASHIER_ROOM_ID.HasValue)
                {
                    if (ids == null)
                    {
                        ids = new List<long>();
                    }
                    List<long> byCashierRoomIds = DAOWorker.SqlDAO.GetSql<long>("SELECT ACCOUNT_BOOK_ID FROM HIS_CARO_ACCOUNT_BOOK WHERE CASHIER_ROOM_ID = :param1", filter.CASHIER_ROOM_ID.Value);
                    if (byCashierRoomIds != null && byCashierRoomIds.Count > 0)
                    {
                        ids.AddRange(byCashierRoomIds);
                    }
                }

                if (ids != null)
                {
                    ids = ids.Distinct().ToList();
                    filter.IDs = ids;
                }

                return DAOWorker.HisAccountBookDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ACCOUNT_BOOK GetById(long id)
        {
            try
            {
                return GetById(id, new HisAccountBookFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ACCOUNT_BOOK GetById(long id, HisAccountBookFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAccountBookDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ACCOUNT_BOOK GetViewById(long id)
        {
            try
            {
                return GetViewById(id, new HisAccountBookViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ACCOUNT_BOOK GetViewById(long id, HisAccountBookViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAccountBookDAO.GetViewById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ACCOUNT_BOOK GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisAccountBookFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ACCOUNT_BOOK GetByCode(string code, HisAccountBookFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAccountBookDAO.GetByCode(code, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ACCOUNT_BOOK GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisAccountBookViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ACCOUNT_BOOK GetViewByCode(string code, HisAccountBookViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAccountBookDAO.GetViewByCode(code, filter.Query());
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

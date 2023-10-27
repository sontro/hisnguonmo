using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsModule
{
    partial class AcsModuleDelete : EntityBase
    {
        public AcsModuleDelete()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_MODULE>();
        }

        private BridgeDAO<ACS_MODULE> bridgeDAO;

        public bool Delete(ACS_MODULE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Delete(data.ID);
        }

        public bool DeleteReference(long moduleId)
        {            
            bool result = false;
            try
            {
                if (IsGreaterThanZero(moduleId))
                {
                    using (var ctx = new AppContext())
                    {
                        string sql = String.Format("DELETE FROM " + ctx.GetTableName<ACS_MODULE_ROLE>() + " WHERE MODULE_ID = {0}", moduleId);
                        result = bridgeDAO.ExecuteSql(sql);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }

            return result;
        }

        public bool DeleteList(List<ACS_MODULE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(listData);
                if (valid)
                {
                    List<ACS_MODULE> listIdNotFound = new List<ACS_MODULE>();
                    List<long> listId = new List<long>();
                    foreach (var data in listData)
                    {
                        if (data.ID > 0)
                        {
                            listId.Add(data.ID);
                        }
                        else
                        {
                            listIdNotFound.Add(data);
                        }
                    }
                    if (listIdNotFound.Count > 0)
                    {
                        Logging("Danh sach du lieu can delete co ton tai id <= 0." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listIdNotFound), listIdNotFound), LogType.Warn);
                    }
                    else
                    {
                        using (var ctx = new AppContext())
                        {
                            List<ACS_MODULE> listRaw = ctx.ACS_MODULE.Where(o => listId.Contains(o.ID)).ToList();
                            if (listRaw != null && listRaw.Count == listData.Count)
                            {
                                result = bridgeDAO.DeleteListRaw(listRaw);
                            }
                            else
                            {
                                Logging("Danh sach du lieu truy van duoc de delete null hoac so luong khong bang danh sach dau vao (listRaw.Count <> listData.Count)." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listRaw), listRaw), LogType.Warn);
                            }
                        }
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                Logging(LogUtil.TraceDbException(ex), LogType.Error);
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData), LogType.Error);
                LogSystem.Error(ex);
                result = false;
            }
            catch (Exception ex)
            {
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData), LogType.Error);
                LogSystem.Error(ex);
                result = false;
            }

            return result;
        }
    }
}

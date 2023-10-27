using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsRole
{
    partial class AcsRoleUpdate : EntityBase
    {
        public AcsRoleUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<ACS_ROLE>();
        }

        private BridgeDAO<ACS_ROLE> bridgeDAO;

        public bool Update(ACS_ROLE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<ACS_ROLE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }

        //public bool UpdateList(List<ACS_ROLE> listData)
        //{
        //    bool result = false;
        //    try
        //    {
        //        bool valid = true;
        //        valid = valid && IsNotNullOrEmpty(listData);
        //        if (valid)
        //        {
        //            List<ACS_ROLE> listIdNotFound = new List<ACS_ROLE>();
        //            Dictionary<long, ACS_ROLE> dictionaryData = new Dictionary<long, ACS_ROLE>();
        //            foreach (var data in listData)
        //            {
        //                if (data.ID > 0)
        //                {
        //                    dictionaryData[data.ID] = data;
        //                }
        //                else
        //                {
        //                    listIdNotFound.Add(data);
        //                }
        //            }
        //            if (listIdNotFound.Count > 0)
        //            {
        //                Logging("Danh sach du lieu can update co ton tai id <= 0." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData), LogType.Warn);
        //            }
        //            else
        //            {
        //                var listId = dictionaryData.Keys.ToList();
        //                using (var ctx = new AppContext())
        //                {
        //                    List<ACS_ROLE> listRaw = ctx.ACS_ROLE.Where(o => listId.Contains(o.ID) && (!o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1)).ToList();
        //                    if (listRaw != null && listRaw.Count == listData.Count)
        //                    {
        //                        foreach (var raw in listRaw)
        //                        {
        //                            ACS.EFMODEL.Decorator.IsActiveDecorator.Set<ACS_ROLE>(dictionaryData[raw.ID]);
        //                            ACS.EFMODEL.Decorator.IsDeleteDecorator.Set<ACS_ROLE>(dictionaryData[raw.ID]);
        //                            ACS.EFMODEL.Decorator.DenyUpdateDecorator.Set<ACS_ROLE>(raw, dictionaryData[raw.ID]);
        //                            System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<ACS_ROLE>();
        //                            foreach (var item in pi)
        //                            {
        //                                item.SetValue(raw, (item.GetValue(dictionaryData[raw.ID])));
        //                            }
        //                            ACS.EFMODEL.Decorator.AppModifierDecorator.Set<ACS_ROLE>(raw);
        //                            ACS.EFMODEL.Decorator.ModifierDecorator.Set<ACS_ROLE>(raw, Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName());
        //                        }
        //                        ctx.SaveChanges();
        //                        result = true;
        //                    }
        //                    else
        //                    {
        //                        Logging("Danh sach du lieu truy van duoc de update null hoac so luong khong bang danh sach dau vao (listRaw.Count <> listData.Count)." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listRaw), listRaw), LogType.Warn);
        //                    }
        //                }
        //            }
        //            return result;
        //        }
        //    }
        //    catch (System.Data.Entity.Validation.DbEntityValidationException ex)
        //    {
        //        Logging(LogUtil.TraceDbException(ex), LogType.Error);
        //        Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData), LogType.Error);
        //        LogSystem.Error(ex);
        //        result = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData), LogType.Error);
        //        LogSystem.Error(ex);
        //        result = false;
        //    }
            
        //    return result;
        //}
    }
}

using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExeServiceModule
{
    public class HisExeServiceModuleFilterQuery : HisExeServiceModuleFilter
    {
        public HisExeServiceModuleFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_EXE_SERVICE_MODULE, bool>>> listHisExeServiceModuleExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EXE_SERVICE_MODULE, bool>>>();



        internal HisExeServiceModuleSO Query()
        {
            HisExeServiceModuleSO search = new HisExeServiceModuleSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisExeServiceModuleExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisExeServiceModuleExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisExeServiceModuleExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisExeServiceModuleExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisExeServiceModuleExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisExeServiceModuleExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisExeServiceModuleExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisExeServiceModuleExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisExeServiceModuleExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisExeServiceModuleExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisExeServiceModuleExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (!String.IsNullOrWhiteSpace(this.MODULE_LINK__EXACT))
                {
                    this.listHisExeServiceModuleExpression.Add(o => o.MODULE_LINK == this.MODULE_LINK__EXACT);
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisExeServiceModuleExpression.Add(o => o.EXE_SERVICE_MODULE_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.GROUP_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.MODULE_LINK.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisExeServiceModuleExpression.AddRange(listHisExeServiceModuleExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisExeServiceModuleExpression.Clear();
                search.listHisExeServiceModuleExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

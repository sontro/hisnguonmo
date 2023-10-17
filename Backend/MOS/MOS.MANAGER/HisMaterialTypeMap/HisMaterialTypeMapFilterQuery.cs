using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialTypeMap
{
    public class HisMaterialTypeMapFilterQuery : HisMaterialTypeMapFilter
    {
        public HisMaterialTypeMapFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_MATERIAL_TYPE_MAP, bool>>> listHisMaterialTypeMapExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MATERIAL_TYPE_MAP, bool>>>();



        internal HisMaterialTypeMapSO Query()
        {
            HisMaterialTypeMapSO search = new HisMaterialTypeMapSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisMaterialTypeMapExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisMaterialTypeMapExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisMaterialTypeMapExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisMaterialTypeMapExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisMaterialTypeMapExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisMaterialTypeMapExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisMaterialTypeMapExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisMaterialTypeMapExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisMaterialTypeMapExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisMaterialTypeMapExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisMaterialTypeMapExpression.Add(o => o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.MATERIAL_TYPE_MAP_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.MATERIAL_TYPE_MAP_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisMaterialTypeMapExpression.AddRange(listHisMaterialTypeMapExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisMaterialTypeMapExpression.Clear();
                search.listHisMaterialTypeMapExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

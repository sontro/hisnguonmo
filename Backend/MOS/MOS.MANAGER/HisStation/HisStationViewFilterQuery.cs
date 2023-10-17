using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisStation
{
    public class HisStationViewFilterQuery : HisStationViewFilter
    {
        public HisStationViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_STATION, bool>>> listVHisStationExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_STATION, bool>>>();

        

        internal HisStationSO Query()
        {
            HisStationSO search = new HisStationSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisStationExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisStationExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisStationExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisStationExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisStationExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisStationExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisStationExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisStationExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisStationExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisStationExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisStationExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.ROOM_ID.HasValue)
                {
                    listVHisStationExpression.Add(o => o.ROOM_ID == this.ROOM_ID.Value);
                }
                if (this.ROOM_IDs != null)
                {
                    listVHisStationExpression.Add(o => this.ROOM_IDs.Contains(o.ROOM_ID));
                }
                if (this.DEPARTMENT_ID.HasValue)
                {
                    listVHisStationExpression.Add(o => o.DEPARTMENT_ID == this.DEPARTMENT_ID.Value);
                }
                if (this.DEPARTMENT_IDs != null)
                {
                    listVHisStationExpression.Add(o => this.DEPARTMENT_IDs.Contains(o.DEPARTMENT_ID));
                }
                if (this.ROOM_TYPE_ID.HasValue)
                {
                    listVHisStationExpression.Add(o => o.ROOM_TYPE_ID == this.ROOM_TYPE_ID.Value);
                }
                if (this.ROOM_TYPE_IDs != null)
                {
                    listVHisStationExpression.Add(o => this.ROOM_TYPE_IDs.Contains(o.ROOM_TYPE_ID));
                }
                if (!String.IsNullOrWhiteSpace(this.STATION_CODE__EXACT))
                {
                    listVHisStationExpression.Add(o => o.STATION_CODE == this.STATION_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisStationExpression.Add(o => o.STATION_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.STATION_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.ADDRESS.ToLower().Contains(this.KEY_WORD)
                        || o.DEPARTMENT_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.DEPARTMENT_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisStationExpression.AddRange(listVHisStationExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisStationExpression.Clear();
                search.listVHisStationExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

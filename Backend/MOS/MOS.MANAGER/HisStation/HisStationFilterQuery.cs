using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisStation
{
    public class HisStationFilterQuery : HisStationFilter
    {
        public HisStationFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_STATION, bool>>> listHisStationExpression = new List<System.Linq.Expressions.Expression<Func<HIS_STATION, bool>>>();

        

        internal HisStationSO Query()
        {
            HisStationSO search = new HisStationSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisStationExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisStationExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisStationExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisStationExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisStationExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisStationExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisStationExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisStationExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisStationExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisStationExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisStationExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.ROOM_ID.HasValue)
                {
                    listHisStationExpression.Add(o => o.ROOM_ID == this.ROOM_ID.Value);
                }
                if (this.ROOM_IDs != null)
                {
                    listHisStationExpression.Add(o => this.ROOM_IDs.Contains(o.ROOM_ID));
                }
                if (!String.IsNullOrWhiteSpace(this.STATION_CODE__EXACT))
                {
                    listHisStationExpression.Add(o => o.STATION_CODE == this.STATION_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisStationExpression.Add(o => o.APP_CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.APP_MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.GROUP_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.STATION_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.STATION_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisStationExpression.AddRange(listHisStationExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisStationExpression.Clear();
                search.listHisStationExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

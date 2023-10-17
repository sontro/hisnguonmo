using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServPtttTemp
{
    public class HisSereServPtttTempFilterQuery : HisSereServPtttTempFilter
    {
        public HisSereServPtttTempFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_SERE_SERV_PTTT_TEMP, bool>>> listHisSereServPtttTempExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SERE_SERV_PTTT_TEMP, bool>>>();



        internal HisSereServPtttTempSO Query()
        {
            HisSereServPtttTempSO search = new HisSereServPtttTempSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisSereServPtttTempExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisSereServPtttTempExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisSereServPtttTempExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisSereServPtttTempExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisSereServPtttTempExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisSereServPtttTempExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisSereServPtttTempExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisSereServPtttTempExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisSereServPtttTempExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisSereServPtttTempExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisSereServPtttTempExpression.Add(o => 
                        o.SERE_SERV_PTTT_TEMP_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.SERE_SERV_PTTT_TEMP_NAME.ToLower().Contains(this.KEY_WORD)
                      );
                }

                search.listHisSereServPtttTempExpression.AddRange(listHisSereServPtttTempExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisSereServPtttTempExpression.Clear();
                search.listHisSereServPtttTempExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

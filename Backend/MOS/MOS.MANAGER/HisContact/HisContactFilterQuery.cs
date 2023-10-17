using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisContact
{
    public class HisContactFilterQuery : HisContactFilter
    {
        public HisContactFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_CONTACT, bool>>> listHisContactExpression = new List<System.Linq.Expressions.Expression<Func<HIS_CONTACT, bool>>>();

        

        internal HisContactSO Query()
        {
            HisContactSO search = new HisContactSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisContactExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisContactExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisContactExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisContactExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisContactExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisContactExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisContactExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisContactExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisContactExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                #endregion

                if (this.CONTACT_POINT1_ID.HasValue)
                {
                    listHisContactExpression.Add(o => o.CONTACT_POINT1_ID == this.CONTACT_POINT1_ID.Value);
                }
                if (this.CONTACT_POINT2_ID.HasValue)
                {
                    listHisContactExpression.Add(o => o.CONTACT_POINT2_ID == this.CONTACT_POINT2_ID.Value);
                }
                if (this.CONTACT_TIME_FROM.HasValue)
                {
                    listHisContactExpression.Add(o => o.CONTACT_TIME >= this.CONTACT_TIME_FROM.Value);
                }
                if (this.CONTACT_TIME_TO.HasValue)
                {
                    listHisContactExpression.Add(o => o.CONTACT_TIME <= this.CONTACT_TIME_TO.Value);
                }
                if (this.CONTACT_POINT1_ID__OR__CONTACT_POINT2_ID.HasValue)
                {
                    listHisContactExpression.Add(o => o.CONTACT_POINT2_ID == this.CONTACT_POINT1_ID__OR__CONTACT_POINT2_ID.Value || o.CONTACT_POINT1_ID == this.CONTACT_POINT1_ID__OR__CONTACT_POINT2_ID.Value);
                }
                if (this.CONTACT_POINT1_ID__OR__CONTACT_POINT2_IDs != null && this.CONTACT_POINT1_ID__OR__CONTACT_POINT2_IDs.Count > 0)
                {
                    foreach (long id in this.CONTACT_POINT1_ID__OR__CONTACT_POINT2_IDs)
                    {
                        listHisContactExpression.Add(o => o.CONTACT_POINT2_ID == id || o.CONTACT_POINT1_ID == id);
                    }
                }

                search.listHisContactExpression.AddRange(listHisContactExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisContactExpression.Clear();
                search.listHisContactExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

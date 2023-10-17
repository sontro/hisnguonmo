using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSurgRemuneration
{
    public class HisSurgRemunerationFilterQuery : HisSurgRemunerationFilter
    {
        public HisSurgRemunerationFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_SURG_REMUNERATION, bool>>> listHisSurgRemunerationExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SURG_REMUNERATION, bool>>>();

        

        internal HisSurgRemunerationSO Query()
        {
            HisSurgRemunerationSO search = new HisSurgRemunerationSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisSurgRemunerationExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisSurgRemunerationExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisSurgRemunerationExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisSurgRemunerationExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisSurgRemunerationExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisSurgRemunerationExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisSurgRemunerationExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisSurgRemunerationExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisSurgRemunerationExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisSurgRemunerationExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.ID_NOT_EQUAL.HasValue)
                {
                    listHisSurgRemunerationExpression.Add(o => o.ID != this.ID_NOT_EQUAL.Value);
                }

                if (this.PTTT_GROUP_ID.HasValue)
                {
                    listHisSurgRemunerationExpression.Add(o => o.PTTT_GROUP_ID == this.PTTT_GROUP_ID.Value);
                }
                if (this.PTTT_GROUP_IDs != null)
                {
                    listHisSurgRemunerationExpression.Add(o => this.PTTT_GROUP_IDs.Contains(o.PTTT_GROUP_ID));
                }

                if (this.EMOTIONLESS_METHOD_ID.HasValue)
                {
                    listHisSurgRemunerationExpression.Add(o => o.EMOTIONLESS_METHOD_ID.HasValue && o.EMOTIONLESS_METHOD_ID == this.EMOTIONLESS_METHOD_ID.Value);
                }
                if (this.EMOTIONLESS_METHOD_IDs != null)
                {
                    listHisSurgRemunerationExpression.Add(o => o.EMOTIONLESS_METHOD_ID.HasValue && this.EMOTIONLESS_METHOD_IDs.Contains(o.EMOTIONLESS_METHOD_ID.Value));
                }
                if (this.SERVICE_TYPE_ID.HasValue)
                {
                    listHisSurgRemunerationExpression.Add(o => o.SERVICE_TYPE_ID == this.SERVICE_TYPE_ID.Value);
                }
                if (this.SERVICE_TYPE_IDs != null)
                {
                    listHisSurgRemunerationExpression.Add(o => this.SERVICE_TYPE_IDs.Contains(o.SERVICE_TYPE_ID));
                }
                
                search.listHisSurgRemunerationExpression.AddRange(listHisSurgRemunerationExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisSurgRemunerationExpression.Clear();
                search.listHisSurgRemunerationExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

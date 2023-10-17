using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSurgRemuneration
{
    public class HisSurgRemunerationViewFilterQuery : HisSurgRemunerationViewFilter
    {
        public HisSurgRemunerationViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_SURG_REMUNERATION, bool>>> listVHisSurgRemunerationExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SURG_REMUNERATION, bool>>>();

        

        internal HisSurgRemunerationSO Query()
        {
            HisSurgRemunerationSO search = new HisSurgRemunerationSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisSurgRemunerationExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisSurgRemunerationExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisSurgRemunerationExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisSurgRemunerationExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisSurgRemunerationExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisSurgRemunerationExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisSurgRemunerationExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisSurgRemunerationExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisSurgRemunerationExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisSurgRemunerationExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisSurgRemunerationExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.ID_NOT_EQUAL.HasValue)
                {
                    listVHisSurgRemunerationExpression.Add(o => o.ID != this.ID_NOT_EQUAL.Value);
                }
                if (this.PTTT_GROUP_ID.HasValue)
                {
                    listVHisSurgRemunerationExpression.Add(o => o.PTTT_GROUP_ID == this.PTTT_GROUP_ID.Value);
                }
                if (this.PTTT_GROUP_IDs != null)
                {
                    listVHisSurgRemunerationExpression.Add(o => this.PTTT_GROUP_IDs.Contains(o.PTTT_GROUP_ID));
                }

                if (this.EMOTIONLESS_METHOD_ID.HasValue)
                {
                    listVHisSurgRemunerationExpression.Add(o => o.EMOTIONLESS_METHOD_ID.HasValue && o.EMOTIONLESS_METHOD_ID == this.EMOTIONLESS_METHOD_ID.Value);
                }
                if (this.EMOTIONLESS_METHOD_IDs != null)
                {
                    listVHisSurgRemunerationExpression.Add(o => o.EMOTIONLESS_METHOD_ID.HasValue && this.EMOTIONLESS_METHOD_IDs.Contains(o.EMOTIONLESS_METHOD_ID.Value));
                }
                if (this.SERVICE_TYPE_ID.HasValue)
                {
                    listVHisSurgRemunerationExpression.Add(o => o.SERVICE_TYPE_ID == this.SERVICE_TYPE_ID.Value);
                }
                if (this.SERVICE_TYPE_IDs != null)
                {
                    listVHisSurgRemunerationExpression.Add(o => this.SERVICE_TYPE_IDs.Contains(o.SERVICE_TYPE_ID));
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisSurgRemunerationExpression.Add(o =>
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.EMOTIONLESS_METHOD_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.EMOTIONLESS_METHOD_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.PTTT_GROUP_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.PTTT_GROUP_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_TYPE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.SURG_REMUNERATION_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.SURG_REMUNERATION_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisSurgRemunerationExpression.AddRange(listVHisSurgRemunerationExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisSurgRemunerationExpression.Clear();
                search.listVHisSurgRemunerationExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

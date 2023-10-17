using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTestSampleType
{
    public class HisTestSampleTypeFilterQuery : HisTestSampleTypeFilter
    {
        public HisTestSampleTypeFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_TEST_SAMPLE_TYPE, bool>>> listHisTestSampleTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TEST_SAMPLE_TYPE, bool>>>();



        internal HisTestSampleTypeSO Query()
        {
            HisTestSampleTypeSO search = new HisTestSampleTypeSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisTestSampleTypeExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisTestSampleTypeExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisTestSampleTypeExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisTestSampleTypeExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisTestSampleTypeExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisTestSampleTypeExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisTestSampleTypeExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisTestSampleTypeExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisTestSampleTypeExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisTestSampleTypeExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisTestSampleTypeExpression.Add(o => o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.TEST_SAMPLE_TYPE_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.TEST_SAMPLE_TYPE_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisTestSampleTypeExpression.AddRange(listHisTestSampleTypeExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisTestSampleTypeExpression.Clear();
                search.listHisTestSampleTypeExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientClassify
{
    public class HisPatientClassifyFilterQuery : HisPatientClassifyFilter
    {
        public HisPatientClassifyFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_PATIENT_CLASSIFY, bool>>> listHisPatientClassifyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PATIENT_CLASSIFY, bool>>>();

        

        internal HisPatientClassifySO Query()
        {
            HisPatientClassifySO search = new HisPatientClassifySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisPatientClassifyExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisPatientClassifyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisPatientClassifyExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisPatientClassifyExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisPatientClassifyExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisPatientClassifyExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisPatientClassifyExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisPatientClassifyExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisPatientClassifyExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisPatientClassifyExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower();
                    listHisPatientClassifyExpression.Add(o =>o.PATIENT_CLASSIFY_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.PATIENT_CLASSIFY_NAME.ToLower().Contains(this.KEY_WORD));
                }

                search.listHisPatientClassifyExpression.AddRange(listHisPatientClassifyExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisPatientClassifyExpression.Clear();
                search.listHisPatientClassifyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

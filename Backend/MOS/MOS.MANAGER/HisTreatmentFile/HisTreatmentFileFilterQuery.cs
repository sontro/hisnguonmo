using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentFile
{
    public class HisTreatmentFileFilterQuery : HisTreatmentFileFilter
    {
        public HisTreatmentFileFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_TREATMENT_FILE, bool>>> listHisTreatmentFileExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TREATMENT_FILE, bool>>>();

        internal HisTreatmentFileSO Query()
        {
            HisTreatmentFileSO search = new HisTreatmentFileSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisTreatmentFileExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisTreatmentFileExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisTreatmentFileExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisTreatmentFileExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisTreatmentFileExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisTreatmentFileExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisTreatmentFileExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisTreatmentFileExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisTreatmentFileExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisTreatmentFileExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.TREATMENT_ID.HasValue)
                {
                    listHisTreatmentFileExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.TREATMENT_IDs != null)
                {
                    listHisTreatmentFileExpression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                if (this.FILE_TYPE_ID.HasValue)
                {
                    listHisTreatmentFileExpression.Add(o => o.FILE_TYPE_ID == this.FILE_TYPE_ID.Value);
                }
                if (this.FILE_TYPE_IDs != null)
                {
                    listHisTreatmentFileExpression.Add(o => this.FILE_TYPE_IDs.Contains(o.FILE_TYPE_ID));
                }
                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisTreatmentFileExpression.Add(o =>
                        (o.DESCRIPTION != null && o.DESCRIPTION.ToLower().Contains(this.KEY_WORD))
                        || (o.FILE_NAME != null && o.FILE_NAME.ToLower().Contains(this.KEY_WORD))
                        || (o.FILE_URLS != null && o.FILE_URLS.ToLower().Contains(this.KEY_WORD))
                        );
                }
                search.listHisTreatmentFileExpression.AddRange(listHisTreatmentFileExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisTreatmentFileExpression.Clear();
                search.listHisTreatmentFileExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

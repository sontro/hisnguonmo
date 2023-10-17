using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPrepare
{
    public class HisPrepareFilterQuery : HisPrepareFilter
    {
        public HisPrepareFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_PREPARE, bool>>> listHisPrepareExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PREPARE, bool>>>();



        internal HisPrepareSO Query()
        {
            HisPrepareSO search = new HisPrepareSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisPrepareExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisPrepareExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisPrepareExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisPrepareExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisPrepareExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisPrepareExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisPrepareExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisPrepareExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisPrepareExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisPrepareExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisPrepareExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.TREATMENT_ID.HasValue)
                {
                    listHisPrepareExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.TREATMENT_IDs != null)
                {
                    listHisPrepareExpression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }

                if (this.APPROVAL_TIME_FROM.HasValue)
                {
                    listHisPrepareExpression.Add(o => o.APPROVAL_TIME.HasValue && o.APPROVAL_TIME.Value >= this.APPROVAL_TIME_FROM.Value);
                }
                if (this.APPROVAL_TIME_TO.HasValue)
                {
                    listHisPrepareExpression.Add(o => o.APPROVAL_TIME.HasValue && o.APPROVAL_TIME.Value <= this.APPROVAL_TIME_TO.Value);
                }
                if (this.FROM_TIME_FROM.HasValue)
                {
                    listHisPrepareExpression.Add(o => o.FROM_TIME.HasValue && o.FROM_TIME.Value >= this.FROM_TIME_FROM.Value);
                }
                if (this.FROM_TIME_TO.HasValue)
                {
                    listHisPrepareExpression.Add(o => o.FROM_TIME.HasValue && o.FROM_TIME.Value <= this.FROM_TIME_TO.Value);
                }
                if (this.TO_TIME_FROM.HasValue)
                {
                    listHisPrepareExpression.Add(o => o.TO_TIME.HasValue && o.TO_TIME.Value >= this.TO_TIME_FROM.Value);
                }
                if (this.TO_TIME_TO.HasValue)
                {
                    listHisPrepareExpression.Add(o => o.TO_TIME.HasValue && o.TO_TIME.Value <= this.TO_TIME_TO.Value);
                }

                if (!String.IsNullOrWhiteSpace(this.APPROVAL_LOGINNAME__EXACT))
                {
                    listHisPrepareExpression.Add(o => o.APPROVAL_LOGINNAME == this.APPROVAL_LOGINNAME__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.PREPARE_CODE__EXACT))
                {
                    listHisPrepareExpression.Add(o => o.PREPARE_CODE == this.PREPARE_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.REQ_LOGINNAME__EXACT))
                {
                    listHisPrepareExpression.Add(o => o.REQ_LOGINNAME == this.REQ_LOGINNAME__EXACT);
                }

                if (this.IS_APPROVE.HasValue)
                {
                    if (this.IS_APPROVE.Value)
                    {
                        listHisPrepareExpression.Add(o => o.APPROVAL_TIME.HasValue);
                    }
                    else
                    {
                        listHisPrepareExpression.Add(o => !o.APPROVAL_TIME.HasValue);
                    }
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisPrepareExpression.Add(o => o.APPROVAL_LOGINNAME.ToLower().Contains(this.KEY_WORD)
                        || o.APPROVAL_USERNAME.ToLower().Contains(this.KEY_WORD)
                        || o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.DESCRIPTION.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.PREPARE_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.REQ_LOGINNAME.ToLower().Contains(this.KEY_WORD)
                        || o.REQ_USERNAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisPrepareExpression.AddRange(listHisPrepareExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisPrepareExpression.Clear();
                search.listHisPrepareExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

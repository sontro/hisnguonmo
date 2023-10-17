using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisLicenseClass
{
    public class HisLicenseClassFilterQuery : HisLicenseClassFilter
    {
        public HisLicenseClassFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_LICENSE_CLASS, bool>>> listHisLicenseClassExpression = new List<System.Linq.Expressions.Expression<Func<HIS_LICENSE_CLASS, bool>>>();

        

        internal HisLicenseClassSO Query()
        {
            HisLicenseClassSO search = new HisLicenseClassSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisLicenseClassExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisLicenseClassExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisLicenseClassExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisLicenseClassExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisLicenseClassExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisLicenseClassExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisLicenseClassExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisLicenseClassExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisLicenseClassExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisLicenseClassExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (!string.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisLicenseClassExpression.Add(o =>
                        o.LICENSE_CLASS_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.LICENSE_CLASS_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }
                if (!string.IsNullOrEmpty(this.LICENSE_CLASS_CODE__EXACT))
                {
                    listHisLicenseClassExpression.Add(o => o.LICENSE_CLASS_CODE == this.LICENSE_CLASS_CODE__EXACT);
                }

                search.listHisLicenseClassExpression.AddRange(listHisLicenseClassExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisLicenseClassExpression.Clear();
                search.listHisLicenseClassExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

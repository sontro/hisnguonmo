using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineMaterial
{
    public class HisMedicineMaterialFilterQuery : HisMedicineMaterialFilter
    {
        public HisMedicineMaterialFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_MEDICINE_MATERIAL, bool>>> listHisMedicineMaterialExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEDICINE_MATERIAL, bool>>>();

        

        internal HisMedicineMaterialSO Query()
        {
            HisMedicineMaterialSO search = new HisMedicineMaterialSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisMedicineMaterialExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisMedicineMaterialExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisMedicineMaterialExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisMedicineMaterialExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisMedicineMaterialExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisMedicineMaterialExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisMedicineMaterialExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisMedicineMaterialExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisMedicineMaterialExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisMedicineMaterialExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisMedicineMaterialExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.MEDICINE_ID.HasValue)
                {
                    listHisMedicineMaterialExpression.Add(o => o.MEDICINE_ID == this.MEDICINE_ID.Value);
                }
                if (this.MEDICINE_IDs != null)
                {
                    listHisMedicineMaterialExpression.Add(o => this.MEDICINE_IDs.Contains(o.MEDICINE_ID));
                }
                if (this.MATERIAL_ID.HasValue)
                {
                    listHisMedicineMaterialExpression.Add(o => o.MATERIAL_ID == this.MATERIAL_ID.Value);
                }
                if (this.MATERIAL_IDs != null)
                {
                    listHisMedicineMaterialExpression.Add(o => this.MATERIAL_IDs.Contains(o.MATERIAL_ID));
                }

                search.listHisMedicineMaterialExpression.AddRange(listHisMedicineMaterialExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisMedicineMaterialExpression.Clear();
                search.listHisMedicineMaterialExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

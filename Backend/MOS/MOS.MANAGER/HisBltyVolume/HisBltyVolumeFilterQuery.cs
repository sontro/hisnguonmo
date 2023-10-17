using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBltyVolume
{
    public class HisBltyVolumeFilterQuery : HisBltyVolumeFilter
    {
        public HisBltyVolumeFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_BLTY_VOLUME, bool>>> listHisBltyVolumeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_BLTY_VOLUME, bool>>>();

        

        internal HisBltyVolumeSO Query()
        {
            HisBltyVolumeSO search = new HisBltyVolumeSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisBltyVolumeExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisBltyVolumeExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisBltyVolumeExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisBltyVolumeExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisBltyVolumeExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisBltyVolumeExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisBltyVolumeExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisBltyVolumeExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisBltyVolumeExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisBltyVolumeExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.BLOOD_TYPE_ID.HasValue)
                {
                    listHisBltyVolumeExpression.Add(o => o.BLOOD_TYPE_ID == this.BLOOD_TYPE_ID.Value);
                }
                if (this.BLOOD_TYPE_IDs != null)
                {
                    listHisBltyVolumeExpression.Add(o => this.BLOOD_TYPE_IDs.Contains(o.BLOOD_TYPE_ID));
                }

                if (this.BLOOD_VOLUME_ID.HasValue)
                {
                    listHisBltyVolumeExpression.Add(o => o.BLOOD_VOLUME_ID == this.BLOOD_VOLUME_ID.Value);
                }
                if (this.BLOOD_VOLUME_IDs != null)
                {
                    listHisBltyVolumeExpression.Add(o => this.BLOOD_VOLUME_IDs.Contains(o.BLOOD_VOLUME_ID));
                }
                
                search.listHisBltyVolumeExpression.AddRange(listHisBltyVolumeExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisBltyVolumeExpression.Clear();
                search.listHisBltyVolumeExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

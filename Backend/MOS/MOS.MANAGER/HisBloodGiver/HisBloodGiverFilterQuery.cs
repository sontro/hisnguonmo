using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBloodGiver
{
    public class HisBloodGiverFilterQuery : HisBloodGiverFilter
    {
        public HisBloodGiverFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_BLOOD_GIVER, bool>>> listHisBloodGiverExpression = new List<System.Linq.Expressions.Expression<Func<HIS_BLOOD_GIVER, bool>>>();



        internal HisBloodGiverSO Query()
        {
            HisBloodGiverSO search = new HisBloodGiverSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisBloodGiverExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisBloodGiverExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisBloodGiverExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisBloodGiverExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisBloodGiverExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisBloodGiverExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisBloodGiverExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisBloodGiverExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisBloodGiverExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisBloodGiverExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    search.listHisBloodGiverExpression.Add(o => o.CCCD_NUMBER.ToLower().Contains(this.KEY_WORD)
                        || o.CAREER_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.CMND_NUMBER.ToLower().Contains(this.KEY_WORD)
                        || o.COMMUNE_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.DISTRICT_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.DISTRICT_NAME_BLOOD.ToLower().Contains(this.KEY_WORD)
                        || o.EXAM_LOGINNAME.ToLower().Contains(this.KEY_WORD)
                        || o.EXAM_USERNAME.ToLower().Contains(this.KEY_WORD)
                        || o.EXECUTE_LOGINNAME.ToLower().Contains(this.KEY_WORD)
                        || o.EXECUTE_USERNAME.ToLower().Contains(this.KEY_WORD)
                        || o.GIVE_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.NATIONAL_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.MOTHER_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.PASSPORT_NUMBER.ToLower().Contains(this.KEY_WORD)
                        || o.PROVINCE_NAME_BLOOD.ToLower().Contains(this.KEY_WORD)
                        || o.VIR_ADDRESS.ToLower().Contains(this.KEY_WORD)
                        || o.PHONE.ToLower().Contains(this.KEY_WORD)
                        || o.PROVINCE_NAME.ToLower().Contains(this.KEY_WORD));
                }

                if (this.IMP_MEST_ID.HasValue)
                {
                    listHisBloodGiverExpression.Add(o => o.IMP_MEST_ID == this.IMP_MEST_ID.Value);
                }
                if (this.IMP_MEST_IDs != null)
                {
                    listHisBloodGiverExpression.Add(o => this.IMP_MEST_IDs.Contains(o.IMP_MEST_ID));
                }

                search.listHisBloodGiverExpression.AddRange(listHisBloodGiverExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisBloodGiverExpression.Clear();
                search.listHisBloodGiverExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

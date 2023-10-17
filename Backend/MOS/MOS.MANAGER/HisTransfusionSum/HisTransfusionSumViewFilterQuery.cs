using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransfusionSum
{
    public class HisTransfusionSumViewFilterQuery : HisTransfusionSumViewFilter
    {
        public HisTransfusionSumViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_TRANSFUSION_SUM, bool>>> listVHisTransfusionSumExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TRANSFUSION_SUM, bool>>>();



        internal HisTransfusionSumSO Query()
        {
            HisTransfusionSumSO search = new HisTransfusionSumSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisTransfusionSumExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisTransfusionSumExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisTransfusionSumExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisTransfusionSumExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisTransfusionSumExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisTransfusionSumExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisTransfusionSumExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisTransfusionSumExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisTransfusionSumExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisTransfusionSumExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisTransfusionSumExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.TREATMENT_ID.HasValue)
                {
                    listVHisTransfusionSumExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.TREATMENT_IDs != null)
                {
                    listVHisTransfusionSumExpression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                if (this.EXP_MEST_BLOOD_ID.HasValue)
                {
                    listVHisTransfusionSumExpression.Add(o => o.EXP_MEST_BLOOD_ID == this.EXP_MEST_BLOOD_ID.Value);
                }
                if (this.EXP_MEST_BLOOD_IDs != null)
                {
                    listVHisTransfusionSumExpression.Add(o => this.EXP_MEST_BLOOD_IDs.Contains(o.EXP_MEST_BLOOD_ID));
                }
                if (this.DEPARTMENT_ID.HasValue)
                {
                    listVHisTransfusionSumExpression.Add(o => o.DEPARTMENT_ID.HasValue && o.DEPARTMENT_ID.Value == this.DEPARTMENT_ID.Value);
                }
                if (this.DEPARTMENT_IDs != null)
                {
                    listVHisTransfusionSumExpression.Add(o => o.DEPARTMENT_ID.HasValue && this.DEPARTMENT_IDs.Contains(o.DEPARTMENT_ID.Value));
                }
                if (this.ROOM_ID.HasValue)
                {
                    listVHisTransfusionSumExpression.Add(o => o.ROOM_ID.HasValue && o.ROOM_ID.Value == this.ROOM_ID.Value);
                }
                if (this.ROOM_IDs != null)
                {
                    listVHisTransfusionSumExpression.Add(o => o.ROOM_ID.HasValue && this.ROOM_IDs.Contains(o.ROOM_ID.Value));
                }

                if (this.BLOOD_ABO_ID.HasValue)
                {
                    listVHisTransfusionSumExpression.Add(o => o.BLOOD_ABO_ID == this.BLOOD_ABO_ID.Value);
                }
                if (this.BLOOD_ABO_IDs != null)
                {
                    listVHisTransfusionSumExpression.Add(o => this.BLOOD_ABO_IDs.Contains(o.BLOOD_ABO_ID));
                }
                if (this.BLOOD_RH_ID.HasValue)
                {
                    listVHisTransfusionSumExpression.Add(o => o.BLOOD_RH_ID.HasValue && o.BLOOD_RH_ID.Value == this.BLOOD_RH_ID.Value);
                }
                if (this.BLOOD_RH_IDs != null)
                {
                    listVHisTransfusionSumExpression.Add(o => o.BLOOD_RH_ID.HasValue && this.BLOOD_RH_IDs.Contains(o.BLOOD_RH_ID.Value));
                }
                if (this.BLOOD_TYPE_ID.HasValue)
                {
                    listVHisTransfusionSumExpression.Add(o => o.BLOOD_TYPE_ID == this.BLOOD_TYPE_ID.Value);
                }
                if (this.BLOOD_TYPE_IDs != null)
                {
                    listVHisTransfusionSumExpression.Add(o => this.BLOOD_TYPE_IDs.Contains(o.BLOOD_TYPE_ID));
                }
                if (this.BLOOD_VOLUME_ID.HasValue)
                {
                    listVHisTransfusionSumExpression.Add(o => o.BLOOD_VOLUME_ID == this.BLOOD_VOLUME_ID.Value);
                }
                if (this.BLOOD_VOLUME_IDs != null)
                {
                    listVHisTransfusionSumExpression.Add(o => this.BLOOD_VOLUME_IDs.Contains(o.BLOOD_VOLUME_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.BLOOD_CODE__EXACT))
                {
                    listVHisTransfusionSumExpression.Add(o => o.BLOOD_CODE == this.BLOOD_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.BLOOD_TYPE_CODE__EXACT))
                {
                    listVHisTransfusionSumExpression.Add(o => o.BLOOD_TYPE_CODE == this.BLOOD_TYPE_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.TDL_PATIENT_CODE__EXACT))
                {
                    listVHisTransfusionSumExpression.Add(o => o.TDL_PATIENT_CODE == this.TDL_PATIENT_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.TREATMENT_CODE__EXACT))
                {
                    listVHisTransfusionSumExpression.Add(o => o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisTransfusionSumExpression.Add(o => o.APP_CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.APP_MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.BLOOD_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.BLOOD_TYPE_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.BLOOD_TYPE_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.COOMBS.ToLower().Contains(this.KEY_WORD)
                        || o.EXECUTE_LOGINNAME.ToLower().Contains(this.KEY_WORD)
                        || o.EXECUTE_USERNAME.ToLower().Contains(this.KEY_WORD)
                        || o.GIVE_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.GIVE_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.GROUP_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.ICD_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.ICD_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.ICD_SUB_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.ICD_TEXT.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.NOTE.ToLower().Contains(this.KEY_WORD)
                        || o.PATIENT_BLOOD_ABO_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.PATIENT_BLOOD_RH_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.PUC.ToLower().Contains(this.KEY_WORD)
                        || o.SCANGEL_GELCARD.ToLower().Contains(this.KEY_WORD)
                        || o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.TDL_PATIENT_GENDER_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.TEST_TUBE.ToLower().Contains(this.KEY_WORD)
                        || o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisTransfusionSumExpression.AddRange(listVHisTransfusionSumExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisTransfusionSumExpression.Clear();
                search.listVHisTransfusionSumExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

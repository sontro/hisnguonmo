using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVitaminA
{
    public class HisVitaminAViewFilterQuery : HisVitaminAViewFilter
    {
        public HisVitaminAViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_VITAMIN_A, bool>>> listVHisVitaminAExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_VITAMIN_A, bool>>>();



        internal HisVitaminASO Query()
        {
            HisVitaminASO search = new HisVitaminASO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisVitaminAExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisVitaminAExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisVitaminAExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisVitaminAExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisVitaminAExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisVitaminAExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisVitaminAExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisVitaminAExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisVitaminAExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisVitaminAExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisVitaminAExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.BRANCH_ID.HasValue)
                {
                    listVHisVitaminAExpression.Add(o => o.BRANCH_ID == this.BRANCH_ID.Value);
                }
                if (this.BRANCH_IDs != null)
                {
                    listVHisVitaminAExpression.Add(o => this.BRANCH_IDs.Contains(o.BRANCH_ID));
                }
                if (this.EXECUTE_DEPARTMENT_ID.HasValue)
                {
                    listVHisVitaminAExpression.Add(o => o.EXECUTE_DEPARTMENT_ID == this.EXECUTE_DEPARTMENT_ID.Value);
                }
                if (this.EXECUTE_DEPARTMENT_IDs != null)
                {
                    listVHisVitaminAExpression.Add(o => this.EXECUTE_DEPARTMENT_IDs.Contains(o.EXECUTE_DEPARTMENT_ID));
                }
                if (this.EXECUTE_ROOM_ID.HasValue)
                {
                    listVHisVitaminAExpression.Add(o => o.EXECUTE_ROOM_ID == this.EXECUTE_ROOM_ID.Value);
                }
                if (this.EXECUTE_ROOM_IDs != null)
                {
                    listVHisVitaminAExpression.Add(o => this.EXECUTE_ROOM_IDs.Contains(o.EXECUTE_ROOM_ID));
                }
                if (this.EXP_MEST_ID.HasValue)
                {
                    listVHisVitaminAExpression.Add(o => o.EXP_MEST_ID.HasValue && o.EXP_MEST_ID.Value == this.EXP_MEST_ID.Value);
                }
                if (this.EXP_MEST_IDs != null)
                {
                    listVHisVitaminAExpression.Add(o => o.EXP_MEST_ID.HasValue && this.EXP_MEST_IDs.Contains(o.EXP_MEST_ID.Value));
                }
                if (this.EXP_MEST_STT_ID.HasValue)
                {
                    listVHisVitaminAExpression.Add(o => o.EXP_MEST_STT_ID.HasValue && o.EXP_MEST_STT_ID.Value == this.EXP_MEST_STT_ID.Value);
                }
                if (this.EXP_MEST_STT_IDs != null)
                {
                    listVHisVitaminAExpression.Add(o => o.EXP_MEST_STT_ID.HasValue && this.EXP_MEST_STT_IDs.Contains(o.EXP_MEST_STT_ID.Value));
                }
                if (this.EXP_MEST_TYPE_ID.HasValue)
                {
                    listVHisVitaminAExpression.Add(o => o.EXP_MEST_TYPE_ID.HasValue && o.EXP_MEST_TYPE_ID.Value == this.EXP_MEST_TYPE_ID.Value);
                }
                if (this.EXP_MEST_TYPE_IDs != null)
                {
                    listVHisVitaminAExpression.Add(o => o.EXP_MEST_TYPE_ID.HasValue && this.EXP_MEST_TYPE_IDs.Contains(o.EXP_MEST_TYPE_ID.Value));
                }
                if (this.MEDI_STOCK_ID.HasValue)
                {
                    listVHisVitaminAExpression.Add(o => o.MEDI_STOCK_ID.HasValue && o.MEDI_STOCK_ID.Value == this.MEDI_STOCK_ID.Value);
                }
                if (this.MEDI_STOCK_IDs != null)
                {
                    listVHisVitaminAExpression.Add(o => o.MEDI_STOCK_ID.HasValue && this.MEDI_STOCK_IDs.Contains(o.MEDI_STOCK_ID.Value));
                }
                if (this.MEDICINE_TYPE_ID.HasValue)
                {
                    listVHisVitaminAExpression.Add(o => o.MEDICINE_TYPE_ID.HasValue && o.MEDICINE_TYPE_ID.Value == this.MEDICINE_TYPE_ID.Value);
                }
                if (this.MEDICINE_TYPE_IDs != null)
                {
                    listVHisVitaminAExpression.Add(o => o.MEDICINE_TYPE_ID.HasValue && this.MEDICINE_TYPE_IDs.Contains(o.MEDICINE_TYPE_ID.Value));
                }
                if (this.PATIENT_ID.HasValue)
                {
                    listVHisVitaminAExpression.Add(o => o.PATIENT_ID == this.PATIENT_ID.Value);
                }
                if (this.PATIENT_IDs != null)
                {
                    listVHisVitaminAExpression.Add(o => this.PATIENT_IDs.Contains(o.PATIENT_ID));
                }
                if (this.REQUEST_DEPARTMENT_ID.HasValue)
                {
                    listVHisVitaminAExpression.Add(o => o.REQUEST_DEPARTMENT_ID == this.REQUEST_DEPARTMENT_ID.Value);
                }
                if (this.REQUEST_DEPARTMENT_IDs != null)
                {
                    listVHisVitaminAExpression.Add(o => this.REQUEST_DEPARTMENT_IDs.Contains(o.REQUEST_DEPARTMENT_ID));
                }
                if (this.REQUEST_ROOM_ID.HasValue)
                {
                    listVHisVitaminAExpression.Add(o => o.REQUEST_ROOM_ID == this.REQUEST_ROOM_ID.Value);
                }
                if (this.REQUEST_ROOM_IDs != null)
                {
                    listVHisVitaminAExpression.Add(o => this.REQUEST_ROOM_IDs.Contains(o.REQUEST_ROOM_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.EXP_MEST_CODE__EXACT))
                {
                    listVHisVitaminAExpression.Add(o => o.EXP_MEST_CODE == this.EXP_MEST_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.TDL_PATIENT_CODE__EXACT))
                {
                    listVHisVitaminAExpression.Add(o => o.TDL_PATIENT_CODE == this.TDL_PATIENT_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.VITAMIN_A_CODE__EXACT))
                {
                    listVHisVitaminAExpression.Add(o => o.VITAMIN_A_CODE == this.VITAMIN_A_CODE__EXACT);
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisVitaminAExpression.Add(o => o.APP_CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.APP_MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.EXECUTE_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.EXECUTE_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.EXP_MEST_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MEDICINE_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MEDICINE_TYPE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.REQUEST_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.REQUEST_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_ADDRESS.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CAREER_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_FIRST_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_GENDER_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_LAST_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_WORK_PLACE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_WORK_PLACE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.VITAMIN_A_CODE.ToLower().Contains(this.KEY_WORD)
                        );
                }

                if (this.VITAMIN_STT != null)
                {
                    switch (this.VITAMIN_STT)
                    {
                        case VITAMIN_STT_ENUM.DRINK:
                            listVHisVitaminAExpression.Add(o => o.EXECUTE_TIME.HasValue);
                            break;
                        case VITAMIN_STT_ENUM.NOT_DRINK:
                            listVHisVitaminAExpression.Add(o => !o.EXECUTE_TIME.HasValue);
                            break;
                        case VITAMIN_STT_ENUM.OUT_OF_STOCK:
                            listVHisVitaminAExpression.Add(o => o.EXECUTE_TIME.HasValue && o.EXP_MEST_ID.HasValue);
                            break;
                        default:
                            break;
                    }
                }

                if (this.REQUEST_TIME_FROM.HasValue)
                {
                    listVHisVitaminAExpression.Add(o => o.REQUEST_TIME >= this.REQUEST_TIME_FROM.Value);
                }
                if (this.REQUEST_TIME_TO.HasValue)
                {
                    listVHisVitaminAExpression.Add(o => o.REQUEST_TIME <= this.REQUEST_TIME_TO.Value);
                }

                search.listVHisVitaminAExpression.AddRange(listVHisVitaminAExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisVitaminAExpression.Clear();
                search.listVHisVitaminAExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

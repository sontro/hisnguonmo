using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestMedicine
{
    public class HisExpMestMedicineView6FilterQuery : HisExpMestMedicineView6Filter
    {
        public HisExpMestMedicineView6FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_EXP_MEST_MEDICINE_6, bool>>> listVHisExpMestMedicine6Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EXP_MEST_MEDICINE_6, bool>>>();

        internal HisExpMestMedicineSO Query()
        {
            HisExpMestMedicineSO search = new HisExpMestMedicineSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.HAS_MEDI_STOCK_PERIOD.HasValue)
                {
                    if (this.HAS_MEDI_STOCK_PERIOD.Value)
                    {
                        listVHisExpMestMedicine6Expression.Add(o => o.MEDI_STOCK_PERIOD_ID != null);
                    }
                    else
                    {
                        listVHisExpMestMedicine6Expression.Add(o => o.MEDI_STOCK_PERIOD_ID == null);
                    }
                }


                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower();
                    listVHisExpMestMedicine6Expression.Add(o => o.APP_CREATOR.Contains(this.KEY_WORD) ||
                        o.APP_MODIFIER.Contains(this.KEY_WORD) ||
                        o.CREATOR.Contains(this.KEY_WORD) ||
                        o.DESCRIPTION.Contains(this.KEY_WORD) ||
                        o.TUTORIAL.Contains(this.KEY_WORD) ||
                        o.ACTIVE_INGR_BHYT_CODE.Contains(this.KEY_WORD) ||
                        o.ACTIVE_INGR_BHYT_NAME.Contains(this.KEY_WORD) ||
                        o.EXP_MEST_CODE.Contains(this.KEY_WORD) ||
                        o.MEDICINE_REGISTER_NUMBER.Contains(this.KEY_WORD) ||
                        o.MEDICINE_TYPE_CODE.Contains(this.KEY_WORD) ||
                        o.MEDICINE_TYPE_NAME.Contains(this.KEY_WORD) ||
                        o.NATIONAL_NAME.Contains(this.KEY_WORD) ||
                        o.PACKAGE_NUMBER.Contains(this.KEY_WORD) ||
                        o.REGISTER_NUMBER.Contains(this.KEY_WORD) ||
                        o.SERVICE_UNIT_CODE.Contains(this.KEY_WORD) ||
                        o.SERVICE_UNIT_NAME.Contains(this.KEY_WORD)
                        );
                }

                if (this.TDL_MEDICINE_TYPE_ID != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => this.TDL_MEDICINE_TYPE_ID == o.TDL_MEDICINE_TYPE_ID);
                }
                if (this.EXP_MEST_TYPE_ID != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => this.EXP_MEST_TYPE_ID == o.EXP_MEST_TYPE_ID);
                }
                if (this.MEDI_STOCK_ID != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => this.MEDI_STOCK_ID == o.MEDI_STOCK_ID);
                }
                if (this.MEDI_STOCK_PERIOD_ID != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.MEDI_STOCK_PERIOD_ID.HasValue && this.MEDI_STOCK_PERIOD_ID.Value == o.MEDI_STOCK_PERIOD_ID.Value);
                }
                if (this.EXP_MEST_STT_ID != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => this.EXP_MEST_STT_ID == o.EXP_MEST_STT_ID);
                }
                if (this.REQ_ROOM_ID != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => this.REQ_ROOM_ID.Value == o.REQ_ROOM_ID);
                }
                if (this.REQ_DEPARTMENT_ID != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => this.REQ_DEPARTMENT_ID.Value == o.REQ_DEPARTMENT_ID);
                }
                if (this.BID_ID != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.BID_ID.HasValue && this.BID_ID.Value == o.BID_ID.Value);
                }
                if (this.MEDICINE_TYPE_ID != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => this.MEDICINE_TYPE_ID == o.MEDICINE_TYPE_ID);
                }
                if (this.SUPPLIER_ID != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.SUPPLIER_ID.HasValue && this.SUPPLIER_ID.Value == o.SUPPLIER_ID.Value);
                }
                if (this.SERVICE_ID != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => this.SERVICE_ID == o.SERVICE_ID);
                }
                if (this.MANUFACTURER_ID != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.MANUFACTURER_ID.HasValue && this.MANUFACTURER_ID.Value == o.MANUFACTURER_ID.Value);
                }
                if (this.MEMA_GROUP_ID != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.MEMA_GROUP_ID.HasValue && this.MEMA_GROUP_ID.Value == o.MEMA_GROUP_ID.Value);
                }
                if (this.SERVICE_UNIT_ID != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => this.SERVICE_UNIT_ID == o.SERVICE_UNIT_ID);
                }
                if (this.EXP_MEST_ID != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => this.EXP_MEST_ID == o.EXP_MEST_ID);
                }
                if (this.MEDICINE_ID != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => this.MEDICINE_ID == o.MEDICINE_ID);
                }
                if (this.TDL_MEDI_STOCK_ID != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => this.TDL_MEDI_STOCK_ID == o.TDL_MEDI_STOCK_ID);
                }
                if (this.TDL_MEDICINE_TYPE_IDs != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.TDL_MEDICINE_TYPE_ID.HasValue && this.TDL_MEDICINE_TYPE_IDs.Contains(o.TDL_MEDICINE_TYPE_ID.Value));
                }
                if (this.EXP_MEST_TYPE_IDs != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => this.EXP_MEST_TYPE_IDs.Contains(o.EXP_MEST_TYPE_ID));
                }
                if (this.MEDI_STOCK_IDs != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => this.MEDI_STOCK_IDs.Contains(o.MEDI_STOCK_ID));
                }
                if (this.MEDI_STOCK_PERIOD_IDs != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.MEDI_STOCK_PERIOD_ID.HasValue && this.MEDI_STOCK_PERIOD_IDs.Contains(o.MEDI_STOCK_PERIOD_ID.Value));
                }
                if (this.EXP_MEST_STT_IDs != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => this.EXP_MEST_STT_IDs.Contains(o.EXP_MEST_STT_ID));
                }
                if (this.REQ_ROOM_IDs != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => this.REQ_ROOM_IDs.Contains(o.REQ_ROOM_ID));
                }
                if (this.REQ_DEPARTMENT_IDs != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => this.REQ_DEPARTMENT_IDs.Contains(o.REQ_DEPARTMENT_ID));
                }
                if (this.BID_IDs != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.BID_ID.HasValue && this.BID_IDs.Contains(o.BID_ID.Value));
                }
                if (this.MEDICINE_TYPE_IDs != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => this.MEDICINE_TYPE_IDs.Contains(o.MEDICINE_TYPE_ID));
                }
                if (this.SUPPLIER_IDs != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.SUPPLIER_ID.HasValue && this.SUPPLIER_IDs.Contains(o.SUPPLIER_ID.Value));
                }
                if (this.SERVICE_IDs != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => this.SERVICE_IDs.Contains(o.SERVICE_ID));
                }
                if (this.MANUFACTURER_IDs != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.MANUFACTURER_ID.HasValue && this.MANUFACTURER_IDs.Contains(o.MANUFACTURER_ID.Value));
                }
                if (this.MEMA_GROUP_IDs != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.MEMA_GROUP_ID.HasValue && this.MEMA_GROUP_IDs.Contains(o.MEMA_GROUP_ID.Value));
                }
                if (this.SERVICE_UNIT_IDs != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => this.SERVICE_UNIT_IDs.Contains(o.SERVICE_UNIT_ID));
                }
                if (this.EXP_MEST_IDs != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.EXP_MEST_ID.HasValue && this.EXP_MEST_IDs.Contains(o.EXP_MEST_ID.Value));
                }
                if (this.MEDICINE_IDs != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.MEDICINE_ID.HasValue && this.MEDICINE_IDs.Contains(o.MEDICINE_ID.Value));
                }
                if (this.TDL_MEDI_STOCK_IDs != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.TDL_MEDI_STOCK_ID.HasValue && this.TDL_MEDI_STOCK_IDs.Contains(o.TDL_MEDI_STOCK_ID.Value));
                }

                if (this.EXP_TIME_FROM.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.EXP_TIME.HasValue && o.EXP_TIME.Value >= this.EXP_TIME_FROM.Value);
                }
                if (this.EXP_DATE_FROM.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.EXP_DATE.HasValue && o.EXP_DATE.Value >= this.EXP_DATE_FROM.Value);
                }
                if (this.EXPIRED_DATE_FROM.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.EXPIRED_DATE.HasValue && o.EXPIRED_DATE.Value >= this.EXPIRED_DATE_FROM.Value);
                }
                if (this.IMP_TIME_FROM.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.IMP_TIME.HasValue && o.IMP_TIME.Value >= this.IMP_TIME_FROM.Value);
                }
                if (this.APPROVAL_TIME_FROM.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.APPROVAL_TIME >= this.APPROVAL_TIME_FROM.Value);
                }
                if (this.APPROVAL_DATE_FROM.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.APPROVAL_DATE >= this.APPROVAL_DATE_FROM.Value);
                }
                if (this.EXP_TIME_TO.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.EXP_TIME.HasValue && o.EXP_TIME.Value <= this.EXP_TIME_TO.Value);
                }
                if (this.EXP_DATE_TO.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.EXP_DATE.HasValue && o.EXP_DATE.Value <= this.EXP_DATE_TO.Value);
                }
                if (this.EXPIRED_DATE_TO.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.EXPIRED_DATE.HasValue && o.EXPIRED_DATE.Value <= this.EXPIRED_DATE_TO.Value);
                }
                if (this.IMP_TIME_TO.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.IMP_TIME.HasValue && o.IMP_TIME.Value <= this.IMP_TIME_TO.Value);
                }
                if (this.APPROVAL_TIME_TO.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.APPROVAL_TIME <= this.APPROVAL_TIME_TO.Value);
                }
                if (this.APPROVAL_DATE_TO.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.APPROVAL_DATE <= this.APPROVAL_DATE_TO.Value);
                }
                if (this.IS_EXPEND.HasValue && this.IS_EXPEND.Value)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.IS_EXPEND.HasValue && o.IS_EXPEND.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_EXPORT.HasValue && this.IS_EXPORT.Value)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.IS_EXPORT.HasValue && o.IS_EXPORT.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_FUNCTIONAL_FOOD.HasValue && this.IS_FUNCTIONAL_FOOD.Value)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.IS_FUNCTIONAL_FOOD.HasValue && o.IS_FUNCTIONAL_FOOD.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_EXPEND.HasValue && !this.IS_EXPEND.Value)
                {
                    listVHisExpMestMedicine6Expression.Add(o => !o.IS_EXPEND.HasValue || o.IS_EXPEND.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_EXPORT.HasValue && !this.IS_EXPORT.Value)
                {
                    listVHisExpMestMedicine6Expression.Add(o => !o.IS_EXPORT.HasValue || o.IS_EXPORT.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_FUNCTIONAL_FOOD.HasValue && !this.IS_FUNCTIONAL_FOOD.Value)
                {
                    listVHisExpMestMedicine6Expression.Add(o => !o.IS_FUNCTIONAL_FOOD.HasValue || o.IS_FUNCTIONAL_FOOD.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.TDL_TREATMENT_ID.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.TDL_TREATMENT_ID.HasValue && this.TDL_TREATMENT_ID.Value == o.TDL_TREATMENT_ID);
                }
                if (this.TDL_TREATMENT_IDs != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.TDL_TREATMENT_ID.HasValue && this.TDL_TREATMENT_IDs.Contains(o.TDL_TREATMENT_ID.Value));
                }

                if (this.VACCINATION_RESULT_ID.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.VACCINATION_RESULT_ID.HasValue && this.VACCINATION_RESULT_ID.Value == o.VACCINATION_RESULT_ID);
                }
                if (this.VACCINATION_RESULT_IDs != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.VACCINATION_RESULT_ID.HasValue && this.VACCINATION_RESULT_IDs.Contains(o.VACCINATION_RESULT_ID.Value));
                }

                if (this.TDL_VACCINATION_ID.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.TDL_VACCINATION_ID.HasValue && this.TDL_VACCINATION_ID.Value == o.TDL_VACCINATION_ID);
                }
                if (this.TDL_VACCINATION_IDs != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.TDL_VACCINATION_ID.HasValue && this.TDL_VACCINATION_IDs.Contains(o.TDL_VACCINATION_ID.Value));
                }
                if (this.TDL_SERVICE_REQ_ID.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.TDL_SERVICE_REQ_ID.HasValue && this.TDL_SERVICE_REQ_ID.Value == o.TDL_SERVICE_REQ_ID);
                }
                if (this.TDL_SERVICE_REQ_IDs != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.TDL_SERVICE_REQ_ID.HasValue && this.TDL_SERVICE_REQ_IDs.Contains(o.TDL_SERVICE_REQ_ID.Value));
                }

                if (this.TDL_INTRUCTION_DATE_FROM.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.TDL_INTRUCTION_DATE.HasValue && o.TDL_INTRUCTION_DATE.Value >= this.TDL_INTRUCTION_DATE_FROM.Value);
                }
                if (this.TDL_INTRUCTION_DATE_TO.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.TDL_INTRUCTION_DATE.HasValue && o.TDL_INTRUCTION_DATE.Value <= this.TDL_INTRUCTION_DATE_TO.Value);
                }
                if (this.PRESCRIPTION_ID.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.PRESCRIPTION_ID.HasValue && this.PRESCRIPTION_ID.Value == o.PRESCRIPTION_ID);
                }
                if (this.PRESCRIPTION_IDs != null)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.PRESCRIPTION_ID.HasValue && this.PRESCRIPTION_IDs.Contains(o.PRESCRIPTION_ID.Value));
                }

                if (this.TDL_INTRUCTION_DATE__EQUAL.HasValue)
                {
                    listVHisExpMestMedicine6Expression.Add(o => o.TDL_INTRUCTION_DATE.HasValue && o.TDL_INTRUCTION_DATE.Value == this.TDL_INTRUCTION_DATE__EQUAL.Value);
                }
                if (this.IS_NOT_TAKEN.HasValue)
                {
                    if (this.IS_NOT_TAKEN.Value)
                    {
                        listVHisExpMestMedicine6Expression.Add(o => o.IS_NOT_TAKEN.HasValue && o.IS_NOT_TAKEN.Value == Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisExpMestMedicine6Expression.Add(o => !o.IS_NOT_TAKEN.HasValue || o.IS_NOT_TAKEN.Value != Constant.IS_TRUE);
                    }
                }
                search.listVHisExpMestMedicine6Expression.AddRange(listVHisExpMestMedicine6Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisExpMestMedicine6Expression.Clear();
                search.listVHisExpMestMedicine6Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

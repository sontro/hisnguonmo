using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.UTILITY;

namespace MOS.MANAGER.HisExpMest.Common.Get
{
    public class HisExpMestView3FilterQuery : HisExpMestView3Filter
    {
        public HisExpMestView3FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_EXP_MEST_3, bool>>> listVHisExpMest3Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EXP_MEST_3, bool>>>();



        internal HisExpMestSO Query()
        {
            HisExpMestSO search = new HisExpMestSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisExpMest3Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisExpMest3Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisExpMest3Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisExpMest3Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisExpMest3Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisExpMest3Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisExpMest3Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisExpMest3Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisExpMest3Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisExpMest3Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.FINISH_DATE_FROM.HasValue)
                {
                    listVHisExpMest3Expression.Add(o => o.FINISH_DATE.Value >= this.FINISH_DATE_FROM.Value);
                }
                if (this.FINISH_DATE_TO.HasValue)
                {
                    listVHisExpMest3Expression.Add(o => o.FINISH_DATE.Value <= this.FINISH_DATE_TO.Value);
                }
                if (this.FINISH_TIME_FROM.HasValue)
                {
                    listVHisExpMest3Expression.Add(o => o.FINISH_TIME.Value >= this.FINISH_TIME_FROM.Value);
                }
                if (this.FINISH_TIME_TO.HasValue)
                {
                    listVHisExpMest3Expression.Add(o => o.FINISH_TIME.Value <= this.FINISH_TIME_TO.Value);
                }

                if (this.EXP_MEST_TYPE_ID != null)
                {
                    listVHisExpMest3Expression.Add(o => this.EXP_MEST_TYPE_ID.Value == o.EXP_MEST_TYPE_ID);
                }
                if (this.EXP_MEST_STT_ID != null)
                {
                    listVHisExpMest3Expression.Add(o => this.EXP_MEST_STT_ID.Value == o.EXP_MEST_STT_ID);
                }
                if (this.MEDI_STOCK_ID != null)
                {
                    listVHisExpMest3Expression.Add(o => this.MEDI_STOCK_ID.Value == o.MEDI_STOCK_ID);
                }
                if (this.REQ_ROOM_ID != null)
                {
                    listVHisExpMest3Expression.Add(o => this.REQ_ROOM_ID.Value == o.REQ_ROOM_ID);
                }
                if (this.REQ_DEPARTMENT_ID != null)
                {
                    listVHisExpMest3Expression.Add(o => this.REQ_DEPARTMENT_ID.Value == o.REQ_DEPARTMENT_ID);
                }
                if (this.EXP_MEST_REASON_ID != null)
                {
                    listVHisExpMest3Expression.Add(o => o.EXP_MEST_REASON_ID.HasValue && this.EXP_MEST_REASON_ID.Value == o.EXP_MEST_REASON_ID.Value);
                }
                if (this.SERVICE_REQ_ID != null)
                {
                    listVHisExpMest3Expression.Add(o => o.SERVICE_REQ_ID.HasValue && this.SERVICE_REQ_ID.Value == o.SERVICE_REQ_ID.Value);
                }
                if (this.TDL_TREATMENT_ID != null)
                {
                    listVHisExpMest3Expression.Add(o => o.TDL_TREATMENT_ID.HasValue && this.TDL_TREATMENT_ID.Value == o.TDL_TREATMENT_ID.Value);
                }
                if (this.IMP_MEDI_STOCK_ID != null)
                {
                    listVHisExpMest3Expression.Add(o => o.IMP_MEDI_STOCK_ID.HasValue && this.IMP_MEDI_STOCK_ID.Value == o.IMP_MEDI_STOCK_ID.Value);
                }
                if (this.AGGR_EXP_MEST_ID != null)
                {
                    listVHisExpMest3Expression.Add(o => o.AGGR_EXP_MEST_ID.HasValue && this.AGGR_EXP_MEST_ID.Value == o.AGGR_EXP_MEST_ID.Value);
                }
                if (this.XBTT_EXP_MEST_ID != null)
                {
                    listVHisExpMest3Expression.Add(o => o.XBTT_EXP_MEST_ID.HasValue && this.XBTT_EXP_MEST_ID.Value == o.XBTT_EXP_MEST_ID.Value);
                }
                if (this.SUPPLIER_ID != null)
                {
                    listVHisExpMest3Expression.Add(o => o.SUPPLIER_ID.HasValue && this.SUPPLIER_ID.Value == o.SUPPLIER_ID.Value);
                }
                if (this.MANU_IMP_MEST_ID != null)
                {
                    listVHisExpMest3Expression.Add(o => o.MANU_IMP_MEST_ID.HasValue && this.MANU_IMP_MEST_ID.Value == o.MANU_IMP_MEST_ID.Value);
                }
                if (this.PRESCRIPTION_ID != null)
                {
                    listVHisExpMest3Expression.Add(o => o.PRESCRIPTION_ID.HasValue && this.PRESCRIPTION_ID.Value == o.PRESCRIPTION_ID.Value);
                }
                if (this.BILL_ID != null)
                {
                    listVHisExpMest3Expression.Add(o => o.BILL_ID.HasValue && this.BILL_ID.Value == o.BILL_ID.Value);
                }
                if (this.TDL_PATIENT_ID != null)
                {
                    listVHisExpMest3Expression.Add(o => o.TDL_PATIENT_ID.HasValue && this.TDL_PATIENT_ID.Value == o.TDL_PATIENT_ID.Value);
                }
                if (this.TDL_PATIENT_GENDER_ID != null)
                {
                    listVHisExpMest3Expression.Add(o => o.TDL_PATIENT_GENDER_ID.HasValue && this.TDL_PATIENT_GENDER_ID.Value == o.TDL_PATIENT_GENDER_ID.Value);
                }
                if (this.SALE_PATIENT_TYPE_ID != null)
                {
                    listVHisExpMest3Expression.Add(o => o.SALE_PATIENT_TYPE_ID.HasValue && this.SALE_PATIENT_TYPE_ID.Value == o.SALE_PATIENT_TYPE_ID.Value);
                }
                if (this.MEDI_STOCK_ID__OR__IMP_MEDI_STOCK_ID != null)
                {
                    listVHisExpMest3Expression.Add(o => o.IMP_MEDI_STOCK_ID == this.MEDI_STOCK_ID__OR__IMP_MEDI_STOCK_ID || o.MEDI_STOCK_ID == this.MEDI_STOCK_ID__OR__IMP_MEDI_STOCK_ID);
                }
                if (this.EXP_MEST_TYPE_IDs != null)
                {
                    listVHisExpMest3Expression.Add(o => this.EXP_MEST_TYPE_IDs.Contains(o.EXP_MEST_TYPE_ID));
                }
                if (this.EXP_MEST_STT_IDs != null)
                {
                    listVHisExpMest3Expression.Add(o => this.EXP_MEST_STT_IDs.Contains(o.EXP_MEST_STT_ID));
                }
                if (this.MEDI_STOCK_IDs != null)
                {
                    listVHisExpMest3Expression.Add(o => this.MEDI_STOCK_IDs.Contains(o.MEDI_STOCK_ID));
                }
                if (this.REQ_ROOM_IDs != null)
                {
                    listVHisExpMest3Expression.Add(o => this.REQ_ROOM_IDs.Contains(o.REQ_ROOM_ID));
                }
                if (this.REQ_DEPARTMENT_IDs != null)
                {
                    listVHisExpMest3Expression.Add(o => this.REQ_DEPARTMENT_IDs.Contains(o.REQ_DEPARTMENT_ID));
                }
                if (this.EXP_MEST_REASON_IDs != null)
                {
                    listVHisExpMest3Expression.Add(o => o.EXP_MEST_REASON_ID.HasValue && this.EXP_MEST_REASON_IDs.Contains(o.EXP_MEST_REASON_ID.Value));
                }
                if (this.SERVICE_REQ_IDs != null)
                {
                    listVHisExpMest3Expression.Add(o => o.SERVICE_REQ_ID.HasValue && this.SERVICE_REQ_IDs.Contains(o.SERVICE_REQ_ID.Value));
                }
                if (this.TDL_TREATMENT_IDs != null)
                {
                    listVHisExpMest3Expression.Add(o => o.TDL_TREATMENT_ID.HasValue && this.TDL_TREATMENT_IDs.Contains(o.TDL_TREATMENT_ID.Value));
                }
                if (this.IMP_MEDI_STOCK_IDs != null)
                {
                    listVHisExpMest3Expression.Add(o => o.IMP_MEDI_STOCK_ID.HasValue && this.IMP_MEDI_STOCK_IDs.Contains(o.IMP_MEDI_STOCK_ID.Value));
                }
                if (this.AGGR_EXP_MEST_IDs != null)
                {
                    listVHisExpMest3Expression.Add(o => o.AGGR_EXP_MEST_ID.HasValue && this.AGGR_EXP_MEST_IDs.Contains(o.AGGR_EXP_MEST_ID.Value));
                }
                if (this.SUPPLIER_IDs != null)
                {
                    listVHisExpMest3Expression.Add(o => o.SUPPLIER_ID.HasValue && this.SUPPLIER_IDs.Contains(o.SUPPLIER_ID.Value));
                }
                if (this.MANU_IMP_MEST_IDs != null)
                {
                    listVHisExpMest3Expression.Add(o => o.MANU_IMP_MEST_ID.HasValue && this.MANU_IMP_MEST_IDs.Contains(o.MANU_IMP_MEST_ID.Value));
                }
                if (this.PRESCRIPTION_IDs != null)
                {
                    listVHisExpMest3Expression.Add(o => o.PRESCRIPTION_ID.HasValue && this.PRESCRIPTION_IDs.Contains(o.PRESCRIPTION_ID.Value));
                }
                if (this.BILL_IDs != null)
                {
                    listVHisExpMest3Expression.Add(o => o.BILL_ID.HasValue && this.BILL_IDs.Contains(o.BILL_ID.Value));
                }
                if (this.TDL_PATIENT_IDs != null)
                {
                    listVHisExpMest3Expression.Add(o => o.TDL_PATIENT_ID.HasValue && this.TDL_PATIENT_IDs.Contains(o.TDL_PATIENT_ID.Value));
                }
                if (this.TDL_PATIENT_GENDER_IDs != null)
                {
                    listVHisExpMest3Expression.Add(o => o.TDL_PATIENT_GENDER_ID.HasValue && this.TDL_PATIENT_GENDER_IDs.Contains(o.TDL_PATIENT_GENDER_ID.Value));
                }
                if (this.SALE_PATIENT_TYPE_IDs != null)
                {
                    listVHisExpMest3Expression.Add(o => o.SALE_PATIENT_TYPE_ID.HasValue && this.SALE_PATIENT_TYPE_IDs.Contains(o.SALE_PATIENT_TYPE_ID.Value));
                }
                if (this.CREATE_DATE_FROM.HasValue)
                {
                    listVHisExpMest3Expression.Add(o => o.CREATE_DATE >= this.CREATE_DATE_FROM.Value);
                }
                if (this.CREATE_DATE_TO.HasValue)
                {
                    listVHisExpMest3Expression.Add(o => o.CREATE_DATE <= this.CREATE_DATE_TO.Value);
                }

                //Dung de phan quyen du lieu (kho = Kho dang chon hoac REQ_ROOM = phong dang chon hoac REQ_DEPARTMENT = khoa dang chon)
                if (this.DATA_DOMAIN_FILTER)
                {
                    string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    if (!this.WORKING_ROOM_ID.HasValue)
                    {
                        listVHisExpMest3Expression.Add(o => loginName.Equals(o.CREATOR));
                    }
                    else
                    {
                        List<WorkPlaceSDO> workPlaces = TokenManager.GetWorkPlaceList();
                        WorkPlaceSDO workPlace = workPlaces != null ? workPlaces.Where(o => o.RoomId == this.WORKING_ROOM_ID).FirstOrDefault() : null;
                        if (workPlace != null)
                        {
                            listVHisExpMest3Expression.Add(o => o.MEDI_STOCK_ID == workPlace.MediStockId || o.REQ_ROOM_ID == workPlace.RoomId || loginName.Equals(o.CREATOR));
                        }
                        else
                        {
                            listVHisExpMest3Expression.Add(o => loginName.Equals(o.CREATOR));
                        }
                    }
                }
                if (!String.IsNullOrEmpty(this.EXP_MEST_CODE__EXACT))
                {
                    listVHisExpMest3Expression.Add(o => o.EXP_MEST_CODE == this.EXP_MEST_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TDL_TREATMENT_CODE__EXACT))
                {
                    listVHisExpMest3Expression.Add(o => o.TDL_TREATMENT_CODE == this.TDL_TREATMENT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TDL_PATIENT_CODE__EXACT))
                {
                    listVHisExpMest3Expression.Add(o => o.TDL_PATIENT_CODE == this.TDL_PATIENT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.NATIONAL_EXP_MEST_CODE__EXACT))
                {
                    listVHisExpMest3Expression.Add(o => o.NATIONAL_EXP_MEST_CODE == this.NATIONAL_EXP_MEST_CODE__EXACT);
                }
                if (this.TDL_INTRUCTION_DATE_FROM.HasValue)
                {
                    listVHisExpMest3Expression.Add(o => o.TDL_INTRUCTION_DATE.Value >= this.TDL_INTRUCTION_DATE_FROM.Value);
                }
                if (this.TDL_INTRUCTION_DATE_TO.HasValue)
                {
                    listVHisExpMest3Expression.Add(o => o.TDL_INTRUCTION_DATE.Value <= this.TDL_INTRUCTION_DATE_TO.Value);
                }
                if (this.TDL_INTRUCTION_TIME_FROM.HasValue)
                {
                    listVHisExpMest3Expression.Add(o => o.TDL_INTRUCTION_TIME.Value >= this.TDL_INTRUCTION_TIME_FROM.Value);
                }
                if (this.TDL_INTRUCTION_TIME_TO.HasValue)
                {
                    listVHisExpMest3Expression.Add(o => o.TDL_INTRUCTION_TIME.Value <= this.TDL_INTRUCTION_TIME_TO.Value);
                }
                if (this.HAS_XBTT_EXP_MEST_ID.HasValue)
                {
                    if (this.HAS_XBTT_EXP_MEST_ID.Value)
                    {
                        listVHisExpMest3Expression.Add(o => o.XBTT_EXP_MEST_ID.HasValue);
                    }
                    else
                    {
                        listVHisExpMest3Expression.Add(o => !o.XBTT_EXP_MEST_ID.HasValue);
                    }
                }
                if (this.IS_EXPORT_EQUAL_APPROVE.HasValue)
                {
                    if (this.IS_EXPORT_EQUAL_APPROVE.Value)
                    {
                        listVHisExpMest3Expression.Add(o => o.IS_EXPORT_EQUAL_APPROVE.HasValue && o.IS_EXPORT_EQUAL_APPROVE.Value == MOS.UTILITY.Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisExpMest3Expression.Add(o => !o.IS_EXPORT_EQUAL_APPROVE.HasValue || o.IS_EXPORT_EQUAL_APPROVE != MOS.UTILITY.Constant.IS_TRUE);
                    }
                }
                if (this.IS_EXPORT_EQUAL_REQUEST.HasValue)
                {
                    if (this.IS_EXPORT_EQUAL_REQUEST.Value)
                    {
                        listVHisExpMest3Expression.Add(o => o.IS_EXPORT_EQUAL_REQUEST.HasValue && o.IS_EXPORT_EQUAL_REQUEST.Value == MOS.UTILITY.Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisExpMest3Expression.Add(o => !o.IS_EXPORT_EQUAL_REQUEST.HasValue || o.IS_EXPORT_EQUAL_REQUEST != MOS.UTILITY.Constant.IS_TRUE);
                    }
                }
                if (this.HAS_AGGR_EXP_MEST_ID.HasValue)
                {
                    if (this.HAS_AGGR_EXP_MEST_ID.Value)
                    {
                        listVHisExpMest3Expression.Add(o => o.AGGR_EXP_MEST_ID.HasValue);
                    }
                    else
                    {
                        listVHisExpMest3Expression.Add(o => !o.AGGR_EXP_MEST_ID.HasValue);
                    }
                }
                if (this.DISPENSE_IDs != null)
                {
                    listVHisExpMest3Expression.Add(o => o.DISPENSE_ID.HasValue && this.DISPENSE_IDs.Contains(o.DISPENSE_ID.Value));
                }
                if (this.DISPENSE_ID.HasValue)
                {
                    listVHisExpMest3Expression.Add(o => o.DISPENSE_ID == this.DISPENSE_ID);
                }
                if (this.VACCINATION_IDs != null)
                {
                    listVHisExpMest3Expression.Add(o => o.VACCINATION_ID.HasValue && this.VACCINATION_IDs.Contains(o.VACCINATION_ID.Value));
                }
                if (this.VACCINATION_ID != null)
                {
                    listVHisExpMest3Expression.Add(o => o.VACCINATION_ID.HasValue && this.VACCINATION_ID.Value == o.VACCINATION_ID.Value);
                }
                if (this.HAS_NATIONAL_EXP_MEST_CODE.HasValue)
                {
                    if (this.HAS_NATIONAL_EXP_MEST_CODE.Value)
                    {
                        listVHisExpMest3Expression.Add(o => o.NATIONAL_EXP_MEST_CODE != null);
                    }
                    else
                    {
                        listVHisExpMest3Expression.Add(o => o.NATIONAL_EXP_MEST_CODE == null);
                    }
                }
                if (this.IS_NOT_TAKEN.HasValue)
                {
                    if (this.IS_NOT_TAKEN.Value)
                    {
                        listVHisExpMest3Expression.Add(o => o.IS_NOT_TAKEN.HasValue && o.IS_NOT_TAKEN.Value == Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisExpMest3Expression.Add(o => !o.IS_NOT_TAKEN.HasValue || o.IS_NOT_TAKEN.Value != Constant.IS_TRUE);
                    }
                }
                if (this.IS_KIDNEY.HasValue)
                {
                    if (this.IS_KIDNEY.Value)
                    {
                        listVHisExpMest3Expression.Add(o => o.IS_KIDNEY.HasValue && o.IS_KIDNEY.Value == Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisExpMest3Expression.Add(o => !o.IS_KIDNEY.HasValue || o.IS_KIDNEY.Value != Constant.IS_TRUE);
                    }
                }

                search.listVHisExpMest3Expression.AddRange(listVHisExpMest3Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisExpMest3Expression.Clear();
                search.listVHisExpMest3Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

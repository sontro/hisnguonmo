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

namespace MOS.MANAGER.HisExpMest.Common.Get
{
    public class HisExpMestView2FilterQuery : HisExpMestView2Filter
    {
        public HisExpMestView2FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_EXP_MEST_2, bool>>> listVHisExpMest2Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EXP_MEST_2, bool>>>();

        internal HisExpMestSO Query()
        {
            HisExpMestSO search = new HisExpMestSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    search.listVHisExpMest2Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisExpMest2Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    search.listVHisExpMest2Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    search.listVHisExpMest2Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    search.listVHisExpMest2Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    search.listVHisExpMest2Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    search.listVHisExpMest2Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    search.listVHisExpMest2Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    search.listVHisExpMest2Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    search.listVHisExpMest2Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.BILL_ID != null)
                {
                    listVHisExpMest2Expression.Add(o => o.BILL_ID.HasValue && this.BILL_ID.Value == o.BILL_ID.Value);
                }
                if (this.PRESCRIPTION_ID != null)
                {
                    listVHisExpMest2Expression.Add(o => o.PRESCRIPTION_ID.HasValue && this.PRESCRIPTION_ID.Value == o.PRESCRIPTION_ID.Value);
                }
                if (this.MANU_IMP_MEST_ID != null)
                {
                    listVHisExpMest2Expression.Add(o => o.MANU_IMP_MEST_ID.HasValue && this.MANU_IMP_MEST_ID.Value == o.MANU_IMP_MEST_ID.Value);
                }
                if (this.SUPPLIER_ID != null)
                {
                    listVHisExpMest2Expression.Add(o => o.SUPPLIER_ID.HasValue && this.SUPPLIER_ID.Value == o.SUPPLIER_ID.Value);
                }
                if (this.AGGR_USE_TIME != null)
                {
                    listVHisExpMest2Expression.Add(o => o.AGGR_USE_TIME.HasValue && this.AGGR_USE_TIME.Value == o.AGGR_USE_TIME.Value);
                }
                if (this.AGGR_EXP_MEST_ID != null)
                {
                    listVHisExpMest2Expression.Add(o => o.AGGR_EXP_MEST_ID.HasValue && this.AGGR_EXP_MEST_ID.Value == o.AGGR_EXP_MEST_ID.Value);
                }
                if (this.IMP_MEDI_STOCK_ID != null)
                {
                    listVHisExpMest2Expression.Add(o => o.IMP_MEDI_STOCK_ID.HasValue && this.IMP_MEDI_STOCK_ID.Value == o.IMP_MEDI_STOCK_ID.Value);
                }
                if (this.IMP_OR_EXP_MEDI_STOCK_ID != null)
                {
                    listVHisExpMest2Expression.Add(o => o.MEDI_STOCK_ID == this.IMP_OR_EXP_MEDI_STOCK_ID.Value || (o.IMP_MEDI_STOCK_ID.HasValue && this.IMP_OR_EXP_MEDI_STOCK_ID.Value == o.IMP_MEDI_STOCK_ID.Value));
                }
                if (this.TDL_TREATMENT_ID != null)
                {
                    listVHisExpMest2Expression.Add(o => o.TDL_TREATMENT_ID.HasValue && this.TDL_TREATMENT_ID.Value == o.TDL_TREATMENT_ID.Value);
                }
                if (this.SERVICE_REQ_ID != null)
                {
                    listVHisExpMest2Expression.Add(o => o.SERVICE_REQ_ID.HasValue && this.SERVICE_REQ_ID.Value == o.SERVICE_REQ_ID.Value);
                }
                if (this.EXP_MEST_REASON_ID != null)
                {
                    listVHisExpMest2Expression.Add(o => o.EXP_MEST_REASON_ID.HasValue && this.EXP_MEST_REASON_ID.Value == o.EXP_MEST_REASON_ID.Value);
                }
                if (this.REQ_DEPARTMENT_ID != null)
                {
                    listVHisExpMest2Expression.Add(o => this.REQ_DEPARTMENT_ID.Value == o.REQ_DEPARTMENT_ID);
                }
                if (this.MEDI_STOCK_ID != null)
                {
                    listVHisExpMest2Expression.Add(o => this.MEDI_STOCK_ID.Value == o.MEDI_STOCK_ID);
                }
                if (this.EXP_MEST_STT_ID != null)
                {
                    listVHisExpMest2Expression.Add(o => this.EXP_MEST_STT_ID.Value == o.EXP_MEST_STT_ID);
                }
                if (this.EXP_MEST_TYPE_ID != null)
                {
                    listVHisExpMest2Expression.Add(o => this.EXP_MEST_TYPE_ID.Value == o.EXP_MEST_TYPE_ID);
                }
                if (this.SALE_PATIENT_TYPE_ID != null)
                {
                    listVHisExpMest2Expression.Add(o => o.SALE_PATIENT_TYPE_ID.HasValue && this.SALE_PATIENT_TYPE_ID.Value == o.SALE_PATIENT_TYPE_ID.Value);
                }
                if (this.TDL_PATIENT_ID != null)
                {
                    listVHisExpMest2Expression.Add(o => o.TDL_PATIENT_ID.HasValue && this.TDL_PATIENT_ID.Value == o.TDL_PATIENT_ID.Value);
                }
                if (this.BILL_IDs != null)
                {
                    listVHisExpMest2Expression.Add(o => o.BILL_ID.HasValue && this.BILL_IDs.Contains(o.BILL_ID.Value));
                }
                if (this.PRESCRIPTION_IDs != null)
                {
                    listVHisExpMest2Expression.Add(o => o.PRESCRIPTION_ID.HasValue && this.PRESCRIPTION_IDs.Contains(o.PRESCRIPTION_ID.Value));
                }
                if (this.MANU_IMP_MEST_IDs != null)
                {
                    listVHisExpMest2Expression.Add(o => o.MANU_IMP_MEST_ID.HasValue && this.MANU_IMP_MEST_IDs.Contains(o.MANU_IMP_MEST_ID.Value));
                }
                if (this.SUPPLIER_IDs != null)
                {
                    listVHisExpMest2Expression.Add(o => o.SUPPLIER_ID.HasValue && this.SUPPLIER_IDs.Contains(o.SUPPLIER_ID.Value));
                }
                if (this.AGGR_USE_TIMEs != null)
                {
                    listVHisExpMest2Expression.Add(o => o.AGGR_USE_TIME.HasValue && this.AGGR_USE_TIMEs.Contains(o.AGGR_USE_TIME.Value));
                }
                if (this.AGGR_EXP_MEST_IDs != null)
                {
                    listVHisExpMest2Expression.Add(o => o.AGGR_EXP_MEST_ID.HasValue && this.AGGR_EXP_MEST_IDs.Contains(o.AGGR_EXP_MEST_ID.Value));
                }
                if (this.IMP_MEDI_STOCK_IDs != null)
                {
                    listVHisExpMest2Expression.Add(o => o.IMP_MEDI_STOCK_ID.HasValue && this.IMP_MEDI_STOCK_IDs.Contains(o.IMP_MEDI_STOCK_ID.Value));
                }
                if (this.TDL_TREATMENT_IDs != null)
                {
                    listVHisExpMest2Expression.Add(o => o.TDL_TREATMENT_ID.HasValue && this.TDL_TREATMENT_IDs.Contains(o.TDL_TREATMENT_ID.Value));
                }
                if (this.SERVICE_REQ_IDs != null)
                {
                    listVHisExpMest2Expression.Add(o => o.SERVICE_REQ_ID.HasValue && this.SERVICE_REQ_IDs.Contains(o.SERVICE_REQ_ID.Value));
                }
                if (this.EXP_MEST_REASON_IDs != null)
                {
                    listVHisExpMest2Expression.Add(o => o.EXP_MEST_REASON_ID.HasValue && this.EXP_MEST_REASON_IDs.Contains(o.EXP_MEST_REASON_ID.Value));
                }
                if (this.REQ_DEPARTMENT_IDs != null)
                {
                    listVHisExpMest2Expression.Add(o => this.REQ_DEPARTMENT_IDs.Contains(o.REQ_DEPARTMENT_ID));
                }
                if (this.MEDI_STOCK_IDs != null)
                {
                    listVHisExpMest2Expression.Add(o => this.MEDI_STOCK_IDs.Contains(o.MEDI_STOCK_ID));
                }
                if (this.EXP_MEST_STT_IDs != null)
                {
                    listVHisExpMest2Expression.Add(o => this.EXP_MEST_STT_IDs.Contains(o.EXP_MEST_STT_ID));
                }
                if (this.EXP_MEST_TYPE_IDs != null)
                {
                    listVHisExpMest2Expression.Add(o => this.EXP_MEST_TYPE_IDs.Contains(o.EXP_MEST_TYPE_ID));
                }
                if (this.SALE_PATIENT_TYPE_IDs != null)
                {
                    listVHisExpMest2Expression.Add(o => o.SALE_PATIENT_TYPE_ID.HasValue && this.SALE_PATIENT_TYPE_IDs.Contains(o.SALE_PATIENT_TYPE_ID.Value));
                }
                if (this.TDL_PATIENT_IDs != null)
                {
                    listVHisExpMest2Expression.Add(o => o.TDL_PATIENT_ID.HasValue && this.TDL_PATIENT_IDs.Contains(o.TDL_PATIENT_ID.Value));
                }

                if (this.CREATE_DATE_FROM.HasValue)
                {
                    listVHisExpMest2Expression.Add(o => o.CREATE_DATE >= this.CREATE_DATE_FROM.Value);
                }
                if (this.CREATE_DATE_TO.HasValue)
                {
                    listVHisExpMest2Expression.Add(o => o.CREATE_DATE <= this.CREATE_DATE_TO.Value);
                }
                if (this.FINISH_DATE_FROM.HasValue)
                {
                    listVHisExpMest2Expression.Add(o => o.FINISH_DATE.Value >= this.FINISH_DATE_FROM.Value);
                }
                if (this.FINISH_DATE_TO.HasValue)
                {
                    listVHisExpMest2Expression.Add(o => o.FINISH_DATE.Value <= this.FINISH_DATE_TO.Value);
                }
                if (this.FINISH_TIME_FROM.HasValue)
                {
                    listVHisExpMest2Expression.Add(o => o.FINISH_TIME.Value >= this.FINISH_TIME_FROM.Value);
                }
                if (this.FINISH_TIME_TO.HasValue)
                {
                    listVHisExpMest2Expression.Add(o => o.FINISH_TIME.Value <= this.FINISH_TIME_TO.Value);
                }
                if (this.TDL_INTRUCTION_DATE_FROM.HasValue)
                {
                    listVHisExpMest2Expression.Add(o => o.TDL_INTRUCTION_DATE.HasValue && o.TDL_INTRUCTION_DATE.Value >= this.TDL_INTRUCTION_DATE_FROM.Value);
                }
                if (this.TDL_INTRUCTION_DATE_TO.HasValue)
                {
                    listVHisExpMest2Expression.Add(o => o.TDL_INTRUCTION_DATE.HasValue && o.TDL_INTRUCTION_DATE.Value <= this.TDL_INTRUCTION_DATE_TO.Value);
                }
                if (this.REQ_ROOM_ID.HasValue)
                {
                    listVHisExpMest2Expression.Add(o => o.REQ_ROOM_ID == this.REQ_ROOM_ID.Value);
                }
                if (this.REQ_ROOM_IDs != null)
                {
                    listVHisExpMest2Expression.Add(o => this.REQ_ROOM_IDs.Contains(o.REQ_ROOM_ID));
                }
                if (!String.IsNullOrEmpty(this.TDL_SERVICE_REQ_CODE__EXACT))
                {
                    listVHisExpMest2Expression.Add(o => o.TDL_SERVICE_REQ_CODE == this.TDL_SERVICE_REQ_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.VIR_HEIN_CARD_PREFIX__EXACT))
                {
                    listVHisExpMest2Expression.Add(o => o.VIR_HEIN_CARD_PREFIX != null && o.VIR_HEIN_CARD_PREFIX.ToUpper() == this.VIR_HEIN_CARD_PREFIX__EXACT.ToUpper());
                }
                if (!String.IsNullOrEmpty(this.TDL_HEIN_CARD_NUMBER__EXACT))
                {
                    listVHisExpMest2Expression.Add(o => o.TDL_HEIN_CARD_NUMBER != null && o.TDL_HEIN_CARD_NUMBER.ToUpper() == this.TDL_HEIN_CARD_NUMBER__EXACT.ToUpper());
                }
                if (!String.IsNullOrEmpty(this.TDL_HEIN_CARD_NUMBER))
                {
                    listVHisExpMest2Expression.Add(o => o.TDL_HEIN_CARD_NUMBER != null && o.TDL_HEIN_CARD_NUMBER.ToUpper().Contains(this.TDL_HEIN_CARD_NUMBER.ToUpper()));
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisExpMest2Expression.Add(o => o.EXP_MEST_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.REQ_LOGINNAME.ToLower().Contains(this.KEY_WORD)
                        || o.REQ_USERNAME.ToLower().Contains(this.KEY_WORD)
                        || o.REQ_DEPARTMENT_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.REQ_DEPARTMENT_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.EXP_MEST_TYPE_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                if (this.DATA_DOMAIN_FILTER)
                {
                    string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    if (!this.WORKING_ROOM_ID.HasValue)
                    {
                        listVHisExpMest2Expression.Add(o => loginName.Equals(o.CREATOR));
                    }
                    else
                    {
                        List<WorkPlaceSDO> workPlaces = TokenManager.GetWorkPlaceList();
                        WorkPlaceSDO workPlace = workPlaces != null ? workPlaces.Where(o => o.RoomId == this.WORKING_ROOM_ID).FirstOrDefault() : null;
                        if (workPlace != null)
                        {
                            listVHisExpMest2Expression.Add(o => o.MEDI_STOCK_ID == workPlace.MediStockId || o.IMP_MEDI_STOCK_ID == workPlace.MediStockId || o.REQ_ROOM_ID == workPlace.RoomId || loginName.Equals(o.CREATOR));
                        }
                        else
                        {
                            listVHisExpMest2Expression.Add(o => loginName.Equals(o.CREATOR));
                        }
                    }
                }

                if (!String.IsNullOrEmpty(this.EXP_MEST_CODE__EXACT))
                {
                    listVHisExpMest2Expression.Add(o => o.EXP_MEST_CODE == this.EXP_MEST_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.REQ_LOGINNAME__EXACT))
                {
                    listVHisExpMest2Expression.Add(o => o.REQ_LOGINNAME == this.REQ_LOGINNAME__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TDL_TREATMENT_CODE__EXACT))
                {
                    listVHisExpMest2Expression.Add(o => o.TDL_TREATMENT_CODE == this.TDL_TREATMENT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TDL_PATIENT_CODE__EXACT))
                {
                    listVHisExpMest2Expression.Add(o => o.TDL_PATIENT_CODE == this.TDL_PATIENT_CODE__EXACT);
                }
                if (this.HAS_AGGR.HasValue && this.HAS_AGGR.Value)
                {
                    search.listVHisExpMest2Expression.Add(o => o.AGGR_EXP_MEST_ID.HasValue);
                }
                if (this.HAS_AGGR.HasValue && !this.HAS_AGGR.Value)
                {
                    search.listVHisExpMest2Expression.Add(o => !o.AGGR_EXP_MEST_ID.HasValue);
                }
                if (this.IS_NOT_TAKEN.HasValue && this.IS_NOT_TAKEN.Value)
                {
                    search.listVHisExpMest2Expression.Add(o => o.IS_NOT_TAKEN.HasValue && o.IS_NOT_TAKEN.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_NOT_TAKEN.HasValue && !this.IS_NOT_TAKEN.Value)
                {
                    search.listVHisExpMest2Expression.Add(o => !o.IS_NOT_TAKEN.HasValue || o.IS_NOT_TAKEN.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_CABINET.HasValue && this.IS_CABINET.Value)
                {
                    search.listVHisExpMest2Expression.Add(o => o.IS_CABINET.HasValue && o.IS_CABINET.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_CABINET.HasValue && !this.IS_CABINET.Value)
                {
                    search.listVHisExpMest2Expression.Add(o => !o.IS_CABINET.HasValue || o.IS_CABINET.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_SENT_ERX.HasValue && this.IS_SENT_ERX.Value)
                {
                    search.listVHisExpMest2Expression.Add(o => o.IS_SENT_ERX.HasValue && o.IS_SENT_ERX.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_SENT_ERX.HasValue && !this.IS_SENT_ERX.Value)
                {
                    search.listVHisExpMest2Expression.Add(o => !o.IS_SENT_ERX.HasValue || o.IS_SENT_ERX.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.HAS_XBTT_EXP_MEST_ID.HasValue)
                {
                    if (this.HAS_XBTT_EXP_MEST_ID.Value)
                    {
                        listVHisExpMest2Expression.Add(o => o.XBTT_EXP_MEST_ID.HasValue);
                    }
                    else
                    {
                        listVHisExpMest2Expression.Add(o => !o.XBTT_EXP_MEST_ID.HasValue);
                    }
                }
                if (this.IS_EXPORT_EQUAL_APPROVE.HasValue)
                {
                    if (this.IS_EXPORT_EQUAL_APPROVE.Value)
                    {
                        listVHisExpMest2Expression.Add(o => o.IS_EXPORT_EQUAL_APPROVE.HasValue && o.IS_EXPORT_EQUAL_APPROVE.Value == MOS.UTILITY.Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisExpMest2Expression.Add(o => !o.IS_EXPORT_EQUAL_APPROVE.HasValue || o.IS_EXPORT_EQUAL_APPROVE != MOS.UTILITY.Constant.IS_TRUE);
                    }
                }
                if (this.IS_EXPORT_EQUAL_REQUEST.HasValue)
                {
                    if (this.IS_EXPORT_EQUAL_REQUEST.Value)
                    {
                        listVHisExpMest2Expression.Add(o => o.IS_EXPORT_EQUAL_REQUEST.HasValue && o.IS_EXPORT_EQUAL_REQUEST.Value == MOS.UTILITY.Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisExpMest2Expression.Add(o => !o.IS_EXPORT_EQUAL_REQUEST.HasValue || o.IS_EXPORT_EQUAL_REQUEST != MOS.UTILITY.Constant.IS_TRUE);
                    }
                }
                if (this.TREATMENT_IS_ACTIVE.HasValue && this.TREATMENT_IS_ACTIVE.Value)
                {
                    search.listVHisExpMest2Expression.Add(o => o.TREATMENT_IS_ACTIVE.HasValue && o.TREATMENT_IS_ACTIVE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.TREATMENT_IS_ACTIVE.HasValue && !this.TREATMENT_IS_ACTIVE.Value)
                {
                    search.listVHisExpMest2Expression.Add(o => !o.TREATMENT_IS_ACTIVE.HasValue || o.TREATMENT_IS_ACTIVE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }

                if (this.HAS_BILL_ID.HasValue)
                {
                    if (this.HAS_BILL_ID.Value)
                    {
                        listVHisExpMest2Expression.Add(o => o.BILL_ID.HasValue);
                    }
                    else
                    {
                        listVHisExpMest2Expression.Add(o => !o.BILL_ID.HasValue);
                    }
                }
                if (this.TDL_PATIENT_TYPE_ID.HasValue)
                {
                    listVHisExpMest2Expression.Add(o => o.TDL_PATIENT_TYPE_ID == this.TDL_PATIENT_TYPE_ID.Value);
                }
                if (this.TDL_PATIENT_TYPE_IDs != null)
                {
                    listVHisExpMest2Expression.Add(o => o.TDL_PATIENT_TYPE_ID.HasValue && this.TDL_PATIENT_TYPE_IDs.Contains(o.TDL_PATIENT_TYPE_ID.Value));
                }
                if (this.HAS_CHMS_TYPE_ID.HasValue)
                {
                    if (this.HAS_CHMS_TYPE_ID.Value)
                    {
                        listVHisExpMest2Expression.Add(o => o.CHMS_TYPE_ID.HasValue);
                    }
                    else
                    {
                        listVHisExpMest2Expression.Add(o => !o.CHMS_TYPE_ID.HasValue);
                    }
                }
                if (this.IS_CONFIRM.HasValue)
                {
                    if (this.IS_CONFIRM.Value)
                    {
                        listVHisExpMest2Expression.Add(o => o.IS_CONFIRM.HasValue && o.IS_CONFIRM.Value == MOS.UTILITY.Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisExpMest2Expression.Add(o => !o.IS_CONFIRM.HasValue || o.IS_CONFIRM != MOS.UTILITY.Constant.IS_TRUE);
                    }
                }
                if (this.CREATE_MONTH__EQUAL.HasValue)
                {
                    listVHisExpMest2Expression.Add(o => o.VIR_CREATE_MONTH.HasValue && o.VIR_CREATE_MONTH.Value == this.CREATE_MONTH__EQUAL);
                }
                if (this.CREATE_DATE__EQUAL.HasValue)
                {
                    listVHisExpMest2Expression.Add(o => o.CREATE_DATE == this.CREATE_DATE__EQUAL);
                }
                if (this.PRES_NUMBER.HasValue)
                {
                    listVHisExpMest2Expression.Add(o => o.PRES_NUMBER == this.PRES_NUMBER.Value);
                }
                if (!string.IsNullOrWhiteSpace(this.TDL_BLOOD_CODE__EXACT))
                {
                    var closureVariable = "," + this.TDL_BLOOD_CODE__EXACT.ToString() + ",";
                    listVHisExpMest2Expression.Add(o => (o.TDL_BLOOD_CODE != null && ("," + o.TDL_BLOOD_CODE + ",").Contains(closureVariable)));
                }

                search.listVHisExpMest2Expression.AddRange(listVHisExpMest2Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisExpMest2Expression.Clear();
                search.listVHisExpMest2Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

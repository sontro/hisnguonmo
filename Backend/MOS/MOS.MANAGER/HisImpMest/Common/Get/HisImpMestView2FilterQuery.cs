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

namespace MOS.MANAGER.HisImpMest
{
    public class HisImpMestView2FilterQuery : HisImpMestView2Filter
    {
        public HisImpMestView2FilterQuery()
            : base()
        {

        }
        
        internal List<System.Linq.Expressions.Expression<Func<V_HIS_IMP_MEST_2, bool>>> listVHisImpMest2Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_IMP_MEST_2, bool>>>();
        
        internal HisImpMestSO Query()
        {
            HisImpMestSO search = new HisImpMestSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisImpMest2Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisImpMest2Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisImpMest2Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisImpMest2Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisImpMest2Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisImpMest2Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisImpMest2Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisImpMest2Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisImpMest2Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisImpMest2Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.IMP_MEST_TYPE_ID != null)
                {
                    listVHisImpMest2Expression.Add(o => this.IMP_MEST_TYPE_ID.Value == o.IMP_MEST_TYPE_ID);
                }
                if (this.IMP_MEST_STT_ID != null)
                {
                    listVHisImpMest2Expression.Add(o => this.IMP_MEST_STT_ID.Value == o.IMP_MEST_STT_ID);
                }
                if (this.MEDI_STOCK_ID != null)
                {
                    listVHisImpMest2Expression.Add(o => this.MEDI_STOCK_ID.Value == o.MEDI_STOCK_ID);
                }
                if (this.MEDI_STOCK_PERIOD_ID != null)
                {
                    listVHisImpMest2Expression.Add(o => o.MEDI_STOCK_PERIOD_ID.HasValue && this.MEDI_STOCK_PERIOD_ID.Value == o.MEDI_STOCK_PERIOD_ID.Value);
                }
                if (this.REQ_ROOM_ID != null)
                {
                    listVHisImpMest2Expression.Add(o => o.REQ_ROOM_ID.HasValue && this.REQ_ROOM_ID.Value == o.REQ_ROOM_ID.Value);
                }
                if (this.REQ_DEPARTMENT_ID != null)
                {
                    listVHisImpMest2Expression.Add(o => o.REQ_DEPARTMENT_ID.HasValue && this.REQ_DEPARTMENT_ID.Value == o.REQ_DEPARTMENT_ID.Value);
                }
                if (this.CHMS_EXP_MEST_ID != null)
                {
                    listVHisImpMest2Expression.Add(o => o.CHMS_EXP_MEST_ID.HasValue && this.CHMS_EXP_MEST_ID.Value == o.CHMS_EXP_MEST_ID.Value);
                }
                if (this.AGGR_IMP_MEST_ID != null)
                {
                    listVHisImpMest2Expression.Add(o => o.AGGR_IMP_MEST_ID.HasValue && this.AGGR_IMP_MEST_ID.Value == o.AGGR_IMP_MEST_ID.Value);
                }
                if (this.MOBA_EXP_MEST_ID != null)
                {
                    listVHisImpMest2Expression.Add(o => o.MOBA_EXP_MEST_ID.HasValue && this.MOBA_EXP_MEST_ID.Value == o.MOBA_EXP_MEST_ID.Value);
                }
                if (this.TDL_TREATMENT_ID != null)
                {
                    listVHisImpMest2Expression.Add(o => o.TDL_TREATMENT_ID.HasValue && this.TDL_TREATMENT_ID.Value == o.TDL_TREATMENT_ID.Value);
                }
                if (this.SUPPLIER_ID != null)
                {
                    listVHisImpMest2Expression.Add(o => o.SUPPLIER_ID.HasValue && this.SUPPLIER_ID.Value == o.SUPPLIER_ID.Value);
                }

                if (this.IMP_MEST_TYPE_IDs != null)
                {
                    listVHisImpMest2Expression.Add(o => this.IMP_MEST_TYPE_IDs.Contains(o.IMP_MEST_TYPE_ID));
                }
                if (this.IMP_MEST_STT_IDs != null)
                {
                    listVHisImpMest2Expression.Add(o => this.IMP_MEST_STT_IDs.Contains(o.IMP_MEST_STT_ID));
                }
                if (this.MEDI_STOCK_IDs != null)
                {
                    listVHisImpMest2Expression.Add(o => this.MEDI_STOCK_IDs.Contains(o.MEDI_STOCK_ID));
                }
                if (this.MEDI_STOCK_PERIOD_IDs != null)
                {
                    listVHisImpMest2Expression.Add(o => o.MEDI_STOCK_PERIOD_ID.HasValue && this.MEDI_STOCK_PERIOD_IDs.Contains(o.MEDI_STOCK_PERIOD_ID.Value));
                }
                if (this.REQ_ROOM_IDs != null)
                {
                    listVHisImpMest2Expression.Add(o => o.REQ_ROOM_ID.HasValue && this.REQ_ROOM_IDs.Contains(o.REQ_ROOM_ID.Value));
                }
                if (this.REQ_DEPARTMENT_IDs != null)
                {
                    listVHisImpMest2Expression.Add(o => o.REQ_DEPARTMENT_ID.HasValue && this.REQ_DEPARTMENT_IDs.Contains(o.REQ_DEPARTMENT_ID.Value));
                }
                if (this.CHMS_EXP_MEST_IDs != null)
                {
                    listVHisImpMest2Expression.Add(o => o.CHMS_EXP_MEST_ID.HasValue && this.CHMS_EXP_MEST_IDs.Contains(o.CHMS_EXP_MEST_ID.Value));
                }
                if (this.AGGR_IMP_MEST_IDs != null)
                {
                    listVHisImpMest2Expression.Add(o => o.AGGR_IMP_MEST_ID.HasValue && this.AGGR_IMP_MEST_IDs.Contains(o.AGGR_IMP_MEST_ID.Value));
                }
                if (this.MOBA_EXP_MEST_IDs != null)
                {
                    listVHisImpMest2Expression.Add(o => o.MOBA_EXP_MEST_ID.HasValue && this.MOBA_EXP_MEST_IDs.Contains(o.MOBA_EXP_MEST_ID.Value));
                }
                if (this.TDL_TREATMENT_IDs != null)
                {
                    listVHisImpMest2Expression.Add(o => o.TDL_TREATMENT_ID.HasValue && this.TDL_TREATMENT_IDs.Contains(o.TDL_TREATMENT_ID.Value));
                }
                if (this.SUPPLIER_IDs != null)
                {
                    listVHisImpMest2Expression.Add(o => o.SUPPLIER_ID.HasValue && this.SUPPLIER_IDs.Contains(o.SUPPLIER_ID.Value));
                }

                if (this.IMP_TIME_FROM.HasValue)
                {
                    listVHisImpMest2Expression.Add(o => o.IMP_TIME.Value >= this.IMP_TIME_FROM.Value);
                }
                if (this.IMP_DATE_FROM.HasValue)
                {
                    listVHisImpMest2Expression.Add(o => o.IMP_DATE.Value >= this.IMP_DATE_FROM.Value);
                }
                if (this.APPROVAL_TIME_FROM.HasValue)
                {
                    listVHisImpMest2Expression.Add(o => o.APPROVAL_TIME.Value >= this.APPROVAL_TIME_FROM.Value);
                }
                if (this.CREATE_DATE_FROM.HasValue)
                {
                    listVHisImpMest2Expression.Add(o => o.CREATE_DATE >= this.CREATE_DATE_FROM.Value);
                }
                if (this.DOCUMENT_DATE_FROM.HasValue)
                {
                    listVHisImpMest2Expression.Add(o => o.DOCUMENT_DATE.Value >= this.DOCUMENT_DATE_FROM.Value);
                }

                if (this.IMP_TIME_TO.HasValue)
                {
                    listVHisImpMest2Expression.Add(o => o.IMP_TIME.Value <= this.IMP_TIME_TO.Value);
                }
                if (this.IMP_DATE_TO.HasValue)
                {
                    listVHisImpMest2Expression.Add(o => o.IMP_DATE.Value <= this.IMP_DATE_TO.Value);
                }
                if (this.APPROVAL_TIME_TO.HasValue)
                {
                    listVHisImpMest2Expression.Add(o => o.APPROVAL_TIME.Value <= this.APPROVAL_TIME_TO.Value);
                }
                if (this.CREATE_DATE_TO.HasValue)
                {
                    listVHisImpMest2Expression.Add(o => o.CREATE_DATE <= this.CREATE_DATE_TO.Value);
                }
                if (this.DOCUMENT_DATE_TO.HasValue)
                {
                    listVHisImpMest2Expression.Add(o => o.DOCUMENT_DATE.Value <= this.DOCUMENT_DATE_TO.Value);
                }

                if (this.HAS_MEDI_STOCK_PERIOD.HasValue && this.HAS_MEDI_STOCK_PERIOD.Value)
                {
                    listVHisImpMest2Expression.Add(o => o.MEDI_STOCK_PERIOD_ID.HasValue);
                }
                if (this.HAS_MEDI_STOCK_PERIOD.HasValue && !this.HAS_MEDI_STOCK_PERIOD.Value)
                {
                    listVHisImpMest2Expression.Add(o => !o.MEDI_STOCK_PERIOD_ID.HasValue);
                }
                if (this.HAS_DOCUMENT_NUMBER.HasValue && this.HAS_DOCUMENT_NUMBER.Value)
                {
                    listVHisImpMest2Expression.Add(o => o.DOCUMENT_NUMBER != null);
                }
                if (this.HAS_DOCUMENT_NUMBER.HasValue && !this.HAS_DOCUMENT_NUMBER.Value)
                {
                    listVHisImpMest2Expression.Add(o => o.DOCUMENT_NUMBER == null);
                }
                if (this.HAS_AGGR.HasValue && this.HAS_AGGR.Value)
                {
                    listVHisImpMest2Expression.Add(o => o.AGGR_IMP_MEST_ID.HasValue);
                }
                if (this.HAS_AGGR.HasValue && !this.HAS_AGGR.Value)
                {
                    listVHisImpMest2Expression.Add(o => !o.AGGR_IMP_MEST_ID.HasValue);
                }
                if (!String.IsNullOrEmpty(this.IMP_MEST_CODE__EXACT))
                {
                    listVHisImpMest2Expression.Add(o => o.IMP_MEST_CODE == this.IMP_MEST_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.DOCUMENT_NUMBER__EXACT))
                {
                    listVHisImpMest2Expression.Add(o => o.DOCUMENT_NUMBER != null && o.DOCUMENT_NUMBER == this.DOCUMENT_NUMBER__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TDL_AGG_IMP_MEST_CODE__EXACT))
                {
                    listVHisImpMest2Expression.Add(o => o.TDL_AGGR_IMP_MEST_CODE == this.TDL_AGG_IMP_MEST_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TDL_CHMS_EXP_MEST_CODE__EXACT))
                {
                    listVHisImpMest2Expression.Add(o => o.TDL_CHMS_EXP_MEST_CODE == this.TDL_CHMS_EXP_MEST_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TDL_MOBA_EXP_MEST_CODE__EXACT))
                {
                    listVHisImpMest2Expression.Add(o => o.TDL_MOBA_EXP_MEST_CODE == this.TDL_MOBA_EXP_MEST_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TDL_TREATMENT_CODE__EXACT))
                {
                    listVHisImpMest2Expression.Add(o => o.TDL_TREATMENT_CODE == this.TDL_TREATMENT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.IMP_MEST_SUB_CODE__EXACT))
                {
                    listVHisImpMest2Expression.Add(o => o.IMP_MEST_SUB_CODE == this.IMP_MEST_SUB_CODE__EXACT);
                }
                if (this.DATA_DOMAIN_FILTER)
                {
                    string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    if (!this.WORKING_ROOM_ID.HasValue)
                    {
                        listVHisImpMest2Expression.Add(o => loginName.Equals(o.CREATOR));
                    }
                    else
                    {
                        List<WorkPlaceSDO> workPlaces = TokenManager.GetWorkPlaceList();
                        WorkPlaceSDO workPlace = workPlaces != null ? workPlaces.Where(o => o.RoomId == this.WORKING_ROOM_ID).FirstOrDefault() : null;
                        if (workPlace != null)
                        {
                            listVHisImpMest2Expression.Add(o => o.MEDI_STOCK_ID == workPlace.MediStockId || o.REQ_ROOM_ID == workPlace.RoomId || loginName.Equals(o.CREATOR) || o.REQ_DEPARTMENT_ID == workPlace.DepartmentId);
                        }
                        else
                        {
                            listVHisImpMest2Expression.Add(o => loginName.Equals(o.CREATOR));
                        }
                    }
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisImpMest2Expression.Add(o => o.DOCUMENT_NUMBER.ToLower().Contains(this.KEY_WORD)
                        || o.IMP_MEST_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.MEDI_STOCK_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.NATIONAL_IMP_MEST_CODE.ToLower().Contains(this.KEY_WORD));
                }

                if (this.MEDICAL_CONTRACT_ID.HasValue)
                {
                    listVHisImpMest2Expression.Add(o => o.MEDICAL_CONTRACT_ID.HasValue && o.MEDICAL_CONTRACT_ID == this.MEDICAL_CONTRACT_ID.Value);
                }
                if (this.MEDICAL_CONTRACT_IDs != null)
                {
                    listVHisImpMest2Expression.Add(o => o.MEDICAL_CONTRACT_ID.HasValue && this.MEDICAL_CONTRACT_IDs.Contains(o.MEDICAL_CONTRACT_ID.Value));
                }

                search.listVHisImpMest2Expression.AddRange(listVHisImpMest2Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisImpMest2Expression.Clear();
                search.listVHisImpMest2Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

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
    public class HisImpMestFilterQuery : HisImpMestFilter
    {
        public HisImpMestFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_IMP_MEST, bool>>> listHisImpMestExpression = new List<System.Linq.Expressions.Expression<Func<HIS_IMP_MEST, bool>>>();



        internal HisImpMestSO Query()
        {
            HisImpMestSO search = new HisImpMestSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisImpMestExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisImpMestExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisImpMestExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisImpMestExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisImpMestExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisImpMestExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisImpMestExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisImpMestExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisImpMestExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisImpMestExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.IMP_MEST_TYPE_ID != null)
                {
                    listHisImpMestExpression.Add(o => this.IMP_MEST_TYPE_ID.Value == o.IMP_MEST_TYPE_ID);
                }
                if (this.APPROVAL_IMP_MEST_ID != null)
                {
                    listHisImpMestExpression.Add(o => o.APPROVAL_IMP_MEST_ID.HasValue && this.APPROVAL_IMP_MEST_ID.Value == o.APPROVAL_IMP_MEST_ID);
                }
                if (this.IMP_MEST_STT_ID != null)
                {
                    listHisImpMestExpression.Add(o => this.IMP_MEST_STT_ID.Value == o.IMP_MEST_STT_ID);
                }
                if (this.MEDI_STOCK_ID != null)
                {
                    listHisImpMestExpression.Add(o => this.MEDI_STOCK_ID.Value == o.MEDI_STOCK_ID);
                }
                if (this.MEDI_STOCK_PERIOD_ID != null)
                {
                    listHisImpMestExpression.Add(o => o.MEDI_STOCK_PERIOD_ID.HasValue && this.MEDI_STOCK_PERIOD_ID.Value == o.MEDI_STOCK_PERIOD_ID.Value);
                }
                if (this.REQ_ROOM_ID != null)
                {
                    listHisImpMestExpression.Add(o => o.REQ_ROOM_ID.HasValue && this.REQ_ROOM_ID.Value == o.REQ_ROOM_ID.Value);
                }
                if (this.REQ_DEPARTMENT_ID != null)
                {
                    listHisImpMestExpression.Add(o => o.REQ_DEPARTMENT_ID.HasValue && this.REQ_DEPARTMENT_ID.Value == o.REQ_DEPARTMENT_ID.Value);
                }
                if (this.CHMS_EXP_MEST_ID != null)
                {
                    listHisImpMestExpression.Add(o => o.CHMS_EXP_MEST_ID.HasValue && this.CHMS_EXP_MEST_ID.Value == o.CHMS_EXP_MEST_ID.Value);
                }
                if (this.AGGR_IMP_MEST_ID != null)
                {
                    listHisImpMestExpression.Add(o => o.AGGR_IMP_MEST_ID.HasValue && this.AGGR_IMP_MEST_ID.Value == o.AGGR_IMP_MEST_ID.Value);
                }
                if (this.MOBA_EXP_MEST_ID != null)
                {
                    listHisImpMestExpression.Add(o => o.MOBA_EXP_MEST_ID.HasValue && this.MOBA_EXP_MEST_ID.Value == o.MOBA_EXP_MEST_ID.Value);
                }
                if (this.TDL_TREATMENT_ID != null)
                {
                    listHisImpMestExpression.Add(o => o.TDL_TREATMENT_ID.HasValue && this.TDL_TREATMENT_ID.Value == o.TDL_TREATMENT_ID.Value);
                }
                if (this.SUPPLIER_ID != null)
                {
                    listHisImpMestExpression.Add(o => o.SUPPLIER_ID.HasValue && this.SUPPLIER_ID.Value == o.SUPPLIER_ID.Value);
                }
                if (this.IMP_MEST_PROPOSE_ID != null)
                {
                    listHisImpMestExpression.Add(o => o.IMP_MEST_PROPOSE_ID.HasValue && this.IMP_MEST_PROPOSE_ID.Value == o.IMP_MEST_PROPOSE_ID.Value);
                }

                if (this.IMP_MEST_TYPE_IDs != null)
                {
                    listHisImpMestExpression.Add(o => this.IMP_MEST_TYPE_IDs.Contains(o.IMP_MEST_TYPE_ID));
                }
                if (this.IMP_MEST_STT_IDs != null)
                {
                    listHisImpMestExpression.Add(o => this.IMP_MEST_STT_IDs.Contains(o.IMP_MEST_STT_ID));
                }
                if (this.MEDI_STOCK_IDs != null)
                {
                    listHisImpMestExpression.Add(o => this.MEDI_STOCK_IDs.Contains(o.MEDI_STOCK_ID));
                }
                if (this.MEDI_STOCK_PERIOD_IDs != null)
                {
                    listHisImpMestExpression.Add(o => o.MEDI_STOCK_PERIOD_ID.HasValue && this.MEDI_STOCK_PERIOD_IDs.Contains(o.MEDI_STOCK_PERIOD_ID.Value));
                }
                if (this.REQ_ROOM_IDs != null)
                {
                    listHisImpMestExpression.Add(o => o.REQ_ROOM_ID.HasValue && this.REQ_ROOM_IDs.Contains(o.REQ_ROOM_ID.Value));
                }
                if (this.REQ_DEPARTMENT_IDs != null)
                {
                    listHisImpMestExpression.Add(o => o.REQ_DEPARTMENT_ID.HasValue && this.REQ_DEPARTMENT_IDs.Contains(o.REQ_DEPARTMENT_ID.Value));
                }
                if (this.CHMS_EXP_MEST_IDs != null)
                {
                    listHisImpMestExpression.Add(o => o.CHMS_EXP_MEST_ID.HasValue && this.CHMS_EXP_MEST_IDs.Contains(o.CHMS_EXP_MEST_ID.Value));
                }
                if (this.AGGR_IMP_MEST_IDs != null)
                {
                    listHisImpMestExpression.Add(o => o.AGGR_IMP_MEST_ID.HasValue && this.AGGR_IMP_MEST_IDs.Contains(o.AGGR_IMP_MEST_ID.Value));
                }
                if (this.MOBA_EXP_MEST_IDs != null)
                {
                    listHisImpMestExpression.Add(o => o.MOBA_EXP_MEST_ID.HasValue && this.MOBA_EXP_MEST_IDs.Contains(o.MOBA_EXP_MEST_ID.Value));
                }
                if (this.TDL_TREATMENT_IDs != null)
                {
                    listHisImpMestExpression.Add(o => o.TDL_TREATMENT_ID.HasValue && this.TDL_TREATMENT_IDs.Contains(o.TDL_TREATMENT_ID.Value));
                }
                if (this.SUPPLIER_IDs != null)
                {
                    listHisImpMestExpression.Add(o => o.SUPPLIER_ID.HasValue && this.SUPPLIER_IDs.Contains(o.SUPPLIER_ID.Value));
                }
                if (this.IMP_MEST_PROPOSE_IDs != null)
                {
                    listHisImpMestExpression.Add(o => o.IMP_MEST_PROPOSE_ID.HasValue && this.IMP_MEST_PROPOSE_IDs.Contains(o.IMP_MEST_PROPOSE_ID.Value));
                }

                if (this.IMP_TIME_FROM.HasValue)
                {
                    listHisImpMestExpression.Add(o => o.IMP_TIME.Value >= this.IMP_TIME_FROM.Value);
                }
                if (this.IMP_DATE_FROM.HasValue)
                {
                    listHisImpMestExpression.Add(o => o.IMP_DATE.Value >= this.IMP_DATE_FROM.Value);
                }
                if (this.APPROVAL_TIME_FROM.HasValue)
                {
                    listHisImpMestExpression.Add(o => o.APPROVAL_TIME.Value >= this.APPROVAL_TIME_FROM.Value);
                }
                if (this.CREATE_DATE_FROM.HasValue)
                {
                    listHisImpMestExpression.Add(o => o.CREATE_DATE >= this.CREATE_DATE_FROM.Value);
                }
                if (this.DOCUMENT_DATE_FROM.HasValue)
                {
                    listHisImpMestExpression.Add(o => o.DOCUMENT_DATE.Value >= this.DOCUMENT_DATE_FROM.Value);
                }

                if (this.IMP_TIME_TO.HasValue)
                {
                    listHisImpMestExpression.Add(o => o.IMP_TIME.Value <= this.IMP_TIME_TO.Value);
                }
                if (this.IMP_DATE_TO.HasValue)
                {
                    listHisImpMestExpression.Add(o => o.IMP_DATE.Value <= this.IMP_DATE_TO.Value);
                }
                if (this.APPROVAL_TIME_TO.HasValue)
                {
                    listHisImpMestExpression.Add(o => o.APPROVAL_TIME.Value <= this.APPROVAL_TIME_TO.Value);
                }
                if (this.CREATE_DATE_TO.HasValue)
                {
                    listHisImpMestExpression.Add(o => o.CREATE_DATE <= this.CREATE_DATE_TO.Value);
                }
                if (this.DOCUMENT_DATE_TO.HasValue)
                {
                    listHisImpMestExpression.Add(o => o.DOCUMENT_DATE.Value <= this.DOCUMENT_DATE_TO.Value);
                }

                if (this.HAS_MEDI_STOCK_PERIOD.HasValue && this.HAS_MEDI_STOCK_PERIOD.Value)
                {
                    listHisImpMestExpression.Add(o => o.MEDI_STOCK_PERIOD_ID.HasValue);
                }
                if (this.HAS_MEDI_STOCK_PERIOD.HasValue && !this.HAS_MEDI_STOCK_PERIOD.Value)
                {
                    listHisImpMestExpression.Add(o => !o.MEDI_STOCK_PERIOD_ID.HasValue);
                }
                if (this.HAS_DOCUMENT_NUMBER.HasValue && this.HAS_DOCUMENT_NUMBER.Value)
                {
                    listHisImpMestExpression.Add(o => o.DOCUMENT_NUMBER != null);
                }
                if (this.HAS_DOCUMENT_NUMBER.HasValue && !this.HAS_DOCUMENT_NUMBER.Value)
                {
                    listHisImpMestExpression.Add(o => o.DOCUMENT_NUMBER == null);
                }
                if (this.HAS_AGGR.HasValue && this.HAS_AGGR.Value)
                {
                    listHisImpMestExpression.Add(o => o.AGGR_IMP_MEST_ID.HasValue);
                }
                if (this.HAS_AGGR.HasValue && !this.HAS_AGGR.Value)
                {
                    listHisImpMestExpression.Add(o => !o.AGGR_IMP_MEST_ID.HasValue);
                }
                if (!String.IsNullOrEmpty(this.IMP_MEST_CODE__EXACT))
                {
                    listHisImpMestExpression.Add(o => o.IMP_MEST_CODE == this.IMP_MEST_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.DOCUMENT_NUMBER__EXACT))
                {
                    listHisImpMestExpression.Add(o => o.DOCUMENT_NUMBER != null && o.DOCUMENT_NUMBER == this.DOCUMENT_NUMBER__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TDL_AGG_IMP_MEST_CODE__EXACT))
                {
                    listHisImpMestExpression.Add(o => o.TDL_AGGR_IMP_MEST_CODE == this.TDL_AGG_IMP_MEST_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TDL_CHMS_EXP_MEST_CODE__EXACT))
                {
                    listHisImpMestExpression.Add(o => o.TDL_CHMS_EXP_MEST_CODE == this.TDL_CHMS_EXP_MEST_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TDL_MOBA_EXP_MEST_CODE__EXACT))
                {
                    listHisImpMestExpression.Add(o => o.TDL_MOBA_EXP_MEST_CODE == this.TDL_MOBA_EXP_MEST_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.NATIONAL_IMP_MEST_CODE__EXACT))
                {
                    listHisImpMestExpression.Add(o => o.NATIONAL_IMP_MEST_CODE == this.NATIONAL_IMP_MEST_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.IMP_MEST_SUB_CODE__EXACT))
                {
                    listHisImpMestExpression.Add(o => o.IMP_MEST_SUB_CODE == this.IMP_MEST_SUB_CODE__EXACT);
                }
                if (this.DISPENSE_ID.HasValue)
                {
                    listHisImpMestExpression.Add(o => o.DISPENSE_ID.HasValue && o.DISPENSE_ID.Value == this.DISPENSE_ID.Value);
                }
                if (this.DISPENSE_IDs != null)
                {
                    listHisImpMestExpression.Add(o => o.DISPENSE_ID.HasValue && this.DISPENSE_IDs.Contains(o.DISPENSE_ID.Value));
                }
                if (this.SOURCE_MEST_PERIOD_ID.HasValue)
                {
                    listHisImpMestExpression.Add(o => o.SOURCE_MEST_PERIOD_ID.HasValue && o.SOURCE_MEST_PERIOD_ID.Value == this.SOURCE_MEST_PERIOD_ID.Value);
                }

                if (this.DATA_DOMAIN_FILTER)
                {
                    string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    if (!this.WORKING_ROOM_ID.HasValue)
                    {
                        listHisImpMestExpression.Add(o => loginName.Equals(o.CREATOR));
                    }
                    else
                    {
                        List<WorkPlaceSDO> workPlaces = TokenManager.GetWorkPlaceList();
                        WorkPlaceSDO workPlace = workPlaces != null ? workPlaces.Where(o => o.RoomId == this.WORKING_ROOM_ID).FirstOrDefault() : null;
                        if (workPlace != null)
                        {
                            listHisImpMestExpression.Add(o => o.MEDI_STOCK_ID == workPlace.MediStockId || o.REQ_ROOM_ID == workPlace.RoomId || loginName.Equals(o.CREATOR) || o.REQ_DEPARTMENT_ID == workPlace.DepartmentId);
                        }
                        else
                        {
                            listHisImpMestExpression.Add(o => loginName.Equals(o.CREATOR));
                        }
                    }
                }
                if (this.DISPENSE_IDs != null)
                {
                    listHisImpMestExpression.Add(o => o.DISPENSE_ID.HasValue && this.DISPENSE_IDs.Contains(o.DISPENSE_ID.Value));
                }
                if (this.DISPENSE_ID.HasValue)
                {
                    listHisImpMestExpression.Add(o => o.DISPENSE_ID == this.DISPENSE_ID);
                }
                if (this.HAS_NATIONAL_IMP_MEST_CODE.HasValue)
                {
                    if (this.HAS_NATIONAL_IMP_MEST_CODE.Value)
                    {
                        listHisImpMestExpression.Add(o => o.NATIONAL_IMP_MEST_CODE != null);
                    }
                    else
                    {
                        listHisImpMestExpression.Add(o => o.NATIONAL_IMP_MEST_CODE == null);
                    }
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisImpMestExpression.Add(o => o.APPROVAL_LOGINNAME.ToLower().Contains(this.KEY_WORD)
                        || o.APPROVAL_USERNAME.ToLower().Contains(this.KEY_WORD)
                        || o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.DELIVERER.ToLower().Contains(this.KEY_WORD)
                        || o.DESCRIPTION.ToLower().Contains(this.KEY_WORD)
                        || o.DOCUMENT_NUMBER.ToLower().Contains(this.KEY_WORD)
                        || o.IMP_LOGINNAME.ToLower().Contains(this.KEY_WORD)
                        || o.IMP_MEST_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.IMP_USERNAME.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.REQ_LOGINNAME.ToLower().Contains(this.KEY_WORD)
                        || o.REQ_USERNAME.ToLower().Contains(this.KEY_WORD)
                        || o.NATIONAL_IMP_MEST_CODE.ToLower().Contains(this.KEY_WORD));
                }

                search.listHisImpMestExpression.AddRange(listHisImpMestExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisImpMestExpression.Clear();
                listHisImpMestExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

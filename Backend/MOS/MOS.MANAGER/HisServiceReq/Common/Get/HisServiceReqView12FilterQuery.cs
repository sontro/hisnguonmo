using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq
{
    public class HisServiceReqView12FilterQuery : HisServiceReqView12Filter
    {
        public HisServiceReqView12FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_REQ_12, bool>>> listVHisServiceReq12Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_REQ_12, bool>>>();

        internal HisServiceReqSO Query()
        {
            HisServiceReqSO search = new HisServiceReqSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisServiceReq12Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisServiceReq12Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisServiceReq12Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisServiceReq12Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisServiceReq12Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisServiceReq12Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisServiceReq12Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisServiceReq12Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisServiceReq12Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisServiceReq12Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.SERVICE_REQ_CODE))
                {
                    listVHisServiceReq12Expression.Add(o => o.SERVICE_REQ_CODE == this.SERVICE_REQ_CODE);
                }
                if (this.EXECUTE_ROOM_ID.HasValue)
                {
                    listVHisServiceReq12Expression.Add(o => o.EXECUTE_ROOM_ID == this.EXECUTE_ROOM_ID.Value);
                }
                if (this.REQUEST_ROOM_ID.HasValue)
                {
                    listVHisServiceReq12Expression.Add(o => o.REQUEST_ROOM_ID == this.REQUEST_ROOM_ID.Value);
                }
                if (this.SERVICE_REQ_STT_IDs != null)
                {
                    listVHisServiceReq12Expression.Add(o => this.SERVICE_REQ_STT_IDs.Contains(o.SERVICE_REQ_STT_ID));
                }
                if (this.INTRUCTION_TIME_FROM.HasValue)
                {
                    listVHisServiceReq12Expression.Add(o => o.INTRUCTION_TIME >= this.INTRUCTION_TIME_FROM.Value);
                }
                if (this.INTRUCTION_TIME_TO.HasValue)
                {
                    listVHisServiceReq12Expression.Add(o => o.INTRUCTION_TIME <= this.INTRUCTION_TIME_TO.Value);
                }
                if (this.TREATMENT_ID.HasValue)
                {
                    listVHisServiceReq12Expression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.EXECUTE_DEPARTMENT_ID.HasValue)
                {
                    listVHisServiceReq12Expression.Add(o => o.EXECUTE_DEPARTMENT_ID == this.EXECUTE_DEPARTMENT_ID.Value);
                }
                if (this.FINISH_TIME_FROM.HasValue)
                {
                    listVHisServiceReq12Expression.Add(o => o.FINISH_TIME >= this.FINISH_TIME_FROM);
                }
                if (this.FINISH_TIME_TO.HasValue)
                {
                    listVHisServiceReq12Expression.Add(o => o.FINISH_TIME <= this.FINISH_TIME_TO);
                }
                if (this.EXECUTE_DEPARTMENT_IDs != null)
                {
                    listVHisServiceReq12Expression.Add(s => EXECUTE_DEPARTMENT_IDs.Contains(s.EXECUTE_DEPARTMENT_ID));
                }
                if (this.START_TIME_FROM != null)
                {
                    listVHisServiceReq12Expression.Add(s => s.START_TIME.HasValue && s.START_TIME.Value >= this.START_TIME_FROM.Value);
                }
                if (this.START_TIME_TO != null)
                {
                    listVHisServiceReq12Expression.Add(s => s.START_TIME.HasValue && s.START_TIME.Value <= this.START_TIME_TO.Value);
                }
                if (this.REQUEST_DEPARTMENT_IDs != null)
                {
                    listVHisServiceReq12Expression.Add(s => this.REQUEST_DEPARTMENT_IDs.Contains(s.REQUEST_DEPARTMENT_ID));
                }
                if (this.REQUEST_DEPARTMENT_ID != null)
                {
                    listVHisServiceReq12Expression.Add(s => s.REQUEST_DEPARTMENT_ID == this.REQUEST_DEPARTMENT_ID);
                }
                if (!String.IsNullOrWhiteSpace(this.SESSION_CODE__EXACT))
                {
                    listVHisServiceReq12Expression.Add(o => o.SESSION_CODE == this.SESSION_CODE__EXACT);
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisServiceReq12Expression.Add(o =>
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_REQ_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD)
                        );
                }
                if (!String.IsNullOrEmpty(this.KEYWORD__SERVICE_REQ_CODE__PATIENT_NAME))
                {
                    this.KEYWORD__SERVICE_REQ_CODE__PATIENT_NAME = this.KEYWORD__SERVICE_REQ_CODE__PATIENT_NAME.ToLower().Trim();
                    listVHisServiceReq12Expression.Add(o =>
                        o.SERVICE_REQ_CODE.ToLower().Contains(this.KEYWORD__SERVICE_REQ_CODE__PATIENT_NAME) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEYWORD__SERVICE_REQ_CODE__PATIENT_NAME)
                        );
                }
                if (!String.IsNullOrEmpty(this.KEYWORD__SERVICE_REQ_CODE__TREATMENT_CODE__PATIENT_NAME))
                {
                    this.KEYWORD__SERVICE_REQ_CODE__TREATMENT_CODE__PATIENT_NAME = this.KEYWORD__SERVICE_REQ_CODE__TREATMENT_CODE__PATIENT_NAME.ToLower().Trim();
                    listVHisServiceReq12Expression.Add(o =>
                        o.SERVICE_REQ_CODE.ToLower().Contains(this.KEYWORD__SERVICE_REQ_CODE__TREATMENT_CODE__PATIENT_NAME) ||
                        o.TDL_TREATMENT_CODE.ToLower().Contains(this.KEYWORD__SERVICE_REQ_CODE__TREATMENT_CODE__PATIENT_NAME) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEYWORD__SERVICE_REQ_CODE__TREATMENT_CODE__PATIENT_NAME)
                        );
                }
                if (this.TREATMENT_IDs != null)
                {
                    listVHisServiceReq12Expression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                if (this.SERVICE_REQ_TYPE_ID.HasValue)
                {
                    listVHisServiceReq12Expression.Add(o => o.SERVICE_REQ_TYPE_ID == this.SERVICE_REQ_TYPE_ID.Value);
                }
                if (this.SERVICE_REQ_TYPE_IDs != null)
                {
                    listVHisServiceReq12Expression.Add(o => this.SERVICE_REQ_TYPE_IDs.Contains(o.SERVICE_REQ_TYPE_ID));
                }
                if (this.TRACKING_ID.HasValue)
                {
                    listVHisServiceReq12Expression.Add(o => o.TRACKING_ID.HasValue && o.TRACKING_ID.Value == this.TRACKING_ID.Value);
                }
                if (this.TRACKING_IDs != null)
                {
                    listVHisServiceReq12Expression.Add(o => o.TRACKING_ID.HasValue && this.TRACKING_IDs.Contains(o.TRACKING_ID.Value));
                }
                if (this.HAS_EXECUTE.HasValue && this.HAS_EXECUTE.Value)
                {
                    listVHisServiceReq12Expression.Add(o => !o.IS_NO_EXECUTE.HasValue || o.IS_NO_EXECUTE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.HAS_EXECUTE.HasValue && !this.HAS_EXECUTE.Value)
                {
                    listVHisServiceReq12Expression.Add(o => o.IS_NO_EXECUTE.HasValue && o.IS_NO_EXECUTE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.INTRUCTION_DATE_FROM.HasValue)
                {
                    listVHisServiceReq12Expression.Add(o => o.INTRUCTION_DATE >= this.INTRUCTION_DATE_FROM.Value);
                }
                if (this.INTRUCTION_DATE_TO.HasValue)
                {
                    listVHisServiceReq12Expression.Add(o => o.INTRUCTION_DATE <= this.INTRUCTION_DATE_TO.Value);
                }
                if (this.RATION_TIME_ID.HasValue)
                {
                    listVHisServiceReq12Expression.Add(o => o.RATION_TIME_ID.HasValue && o.RATION_TIME_ID.Value == this.RATION_TIME_ID.Value);
                }
                if (this.RATION_TIME_IDs != null)
                {
                    listVHisServiceReq12Expression.Add(o => o.RATION_TIME_ID.HasValue && this.RATION_TIME_IDs.Contains(o.RATION_TIME_ID.Value));
                }
                if (this.RATION_SUM_ID.HasValue)
                {
                    listVHisServiceReq12Expression.Add(o => o.RATION_SUM_ID.HasValue && o.RATION_SUM_ID.Value == this.RATION_SUM_ID.Value);
                }
                if (this.RATION_SUM_IDs != null)
                {
                    listVHisServiceReq12Expression.Add(o => o.RATION_SUM_ID.HasValue && this.RATION_SUM_IDs.Contains(o.RATION_SUM_ID.Value));
                }

                if (this.HAS_RATION_SUM_ID.HasValue)
                {
                    if (this.HAS_RATION_SUM_ID.Value)
                    {
                        listVHisServiceReq12Expression.Add(o => o.RATION_SUM_ID.HasValue);
                    }
                    else
                    {
                        listVHisServiceReq12Expression.Add(o => !o.RATION_SUM_ID.HasValue);
                    }
                }
                if (this.IS_REQUEST_EQUAL_EXECUTE.HasValue)
                {
                    if (this.IS_REQUEST_EQUAL_EXECUTE.Value)
                    {
                        listVHisServiceReq12Expression.Add(o => o.REQUEST_ROOM_ID == o.EXECUTE_ROOM_ID);
                    }
                    else
                    {
                        listVHisServiceReq12Expression.Add(o => o.REQUEST_ROOM_ID != o.EXECUTE_ROOM_ID);
                    }
                }

                if (this.EXECUTE_ROOM_IDs != null)
                {
                    listVHisServiceReq12Expression.Add(o => this.EXECUTE_ROOM_IDs.Contains(o.EXECUTE_ROOM_ID));
                }
                if (!String.IsNullOrEmpty(this.TDL_TREATMENT_CODE__EXACT))
                {
                    listVHisServiceReq12Expression.Add(o => o.TDL_TREATMENT_CODE == this.TDL_TREATMENT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TDL_PATIENT_CODE__EXACT))
                {
                    listVHisServiceReq12Expression.Add(o => o.TDL_PATIENT_CODE == this.TDL_PATIENT_CODE__EXACT);
                }
                if (this.NOT_IN_SERVICE_REQ_TYPE_IDs != null)
                {
                    listVHisServiceReq12Expression.Add(o => !this.NOT_IN_SERVICE_REQ_TYPE_IDs.Contains(o.SERVICE_REQ_TYPE_ID));
                }

                if (this.HAS_TRACKING_ID.HasValue)
                {
                    if (this.HAS_TRACKING_ID.Value)
                    {
                        listVHisServiceReq12Expression.Add(o => o.TRACKING_ID.HasValue);
                    }
                    else
                    {
                        listVHisServiceReq12Expression.Add(o => !o.TRACKING_ID.HasValue);
                    }
                }
                if (this.TREATMENT_TYPE_ID.HasValue)
                {
                    listVHisServiceReq12Expression.Add(o => o.TREATMENT_TYPE_ID.HasValue && o.TREATMENT_TYPE_ID.Value == this.TREATMENT_TYPE_ID.Value);
                }
                if (this.TREATMENT_TYPE_IDs != null)
                {
                    listVHisServiceReq12Expression.Add(o => o.TREATMENT_TYPE_ID.HasValue && this.TREATMENT_TYPE_IDs.Contains(o.TREATMENT_TYPE_ID.Value));
                }

                search.listVHisServiceReq12Expression.AddRange(listVHisServiceReq12Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
                search.ExtraOrderField1 = ORDER_FIELD1;
                search.ExtraOrderDirection1 = ORDER_DIRECTION1;
                search.ExtraOrderField2 = ORDER_FIELD2;
                search.ExtraOrderDirection2 = ORDER_DIRECTION2;
                search.ExtraOrderField3 = ORDER_FIELD3;
                search.ExtraOrderDirection3 = ORDER_DIRECTION3;
                search.ExtraOrderField4 = ORDER_FIELD4;
                search.ExtraOrderDirection4 = ORDER_DIRECTION4;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisServiceReq12Expression.Clear();
                search.listVHisServiceReq12Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

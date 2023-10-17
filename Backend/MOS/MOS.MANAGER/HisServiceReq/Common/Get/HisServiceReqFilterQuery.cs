using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisServiceReq.Test;
using MOS.MANAGER.HisServiceReq.Test.LisSenderV1;
using MOS.MANAGER.Token;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReq
{
    public class HisServiceReqFilterQuery : HisServiceReqFilter
    {
        public HisServiceReqFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_REQ, bool>>> listHisServiceReqExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_REQ, bool>>>();

        internal HisServiceReqSO Query()
        {
            HisServiceReqSO search = new HisServiceReqSO(this.IS_INCLUDE_DELETED);
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listHisServiceReqExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    search.listHisServiceReqExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    search.listHisServiceReqExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    search.listHisServiceReqExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.PARENT_ID.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.PARENT_ID == this.PARENT_ID);
                }
                if (this.PAAN_POSITION_ID.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.PAAN_POSITION_ID.HasValue && o.PAAN_POSITION_ID.Value == this.PAAN_POSITION_ID.Value);
                }
                if (this.PAAN_POSITION_IDs != null)
                {
                    search.listHisServiceReqExpression.Add(o => o.PAAN_POSITION_ID.HasValue && this.PAAN_POSITION_IDs.Contains(o.PAAN_POSITION_ID.Value));
                }
                if (this.REHA_SUM_ID.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.REHA_SUM_ID.HasValue && o.REHA_SUM_ID.Value == this.REHA_SUM_ID.Value);
                }
                if (this.REHA_SUM_IDs != null)
                {
                    search.listHisServiceReqExpression.Add(o => o.REHA_SUM_ID.HasValue && this.REHA_SUM_IDs.Contains(o.REHA_SUM_ID.Value));
                }
                if (this.TDL_PATIENT_ID.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.TDL_PATIENT_ID == this.TDL_PATIENT_ID);
                }
                if (this.TREATMENT_ID.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID);
                }
                if (this.SERVICE_REQ_TYPE_ID.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.SERVICE_REQ_TYPE_ID == this.SERVICE_REQ_TYPE_ID);
                }
                if (this.REQUEST_DEPARTMENT_ID__OR__EXECUTE_DEPARTMENT_ID.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.REQUEST_DEPARTMENT_ID == this.REQUEST_DEPARTMENT_ID__OR__EXECUTE_DEPARTMENT_ID.Value || o.EXECUTE_DEPARTMENT_ID == this.REQUEST_DEPARTMENT_ID__OR__EXECUTE_DEPARTMENT_ID.Value);
                }
                if (this.REQUEST_ROOM_ID__OR__EXECUTE_ROOM_ID.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.REQUEST_ROOM_ID == this.REQUEST_ROOM_ID__OR__EXECUTE_ROOM_ID.Value || o.EXECUTE_ROOM_ID == this.REQUEST_ROOM_ID__OR__EXECUTE_ROOM_ID.Value);
                }
                if (this.SERVICE_REQ_STT_ID.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.SERVICE_REQ_STT_ID == this.SERVICE_REQ_STT_ID.Value);
                }
                if (this.SERVICE_REQ_STT_IDs != null)
                {
                    search.listHisServiceReqExpression.Add(o => this.SERVICE_REQ_STT_IDs.Contains(o.SERVICE_REQ_STT_ID));
                }
                if (this.PARENT_IDs != null)
                {
                    search.listHisServiceReqExpression.Add(o => o.PARENT_ID.HasValue && this.PARENT_IDs.Contains(o.PARENT_ID.Value));
                }
                if (!String.IsNullOrEmpty(this.SERVICE_REQ_CODE__ENDS_WITH))
                {
                    search.listHisServiceReqExpression.Add(o => o.SERVICE_REQ_CODE.EndsWith(this.SERVICE_REQ_CODE__ENDS_WITH));
                }
                if (this.EXECUTE_GROUP_ID.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.EXECUTE_GROUP_ID.HasValue && o.EXECUTE_GROUP_ID.Value == this.EXECUTE_GROUP_ID.Value);
                }
                if (this.INTRUCTION_TIME_FROM.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.INTRUCTION_TIME >= this.INTRUCTION_TIME_FROM.Value);
                }
                if (this.INTRUCTION_TIME_TO.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.INTRUCTION_TIME <= this.INTRUCTION_TIME_TO.Value);
                }
                if (this.FINISH_TIME_FROM.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.FINISH_TIME.HasValue && o.FINISH_TIME >= this.FINISH_TIME_FROM.Value);
                }
                if (this.FINISH_TIME_TO.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.FINISH_TIME.HasValue && o.FINISH_TIME <= this.FINISH_TIME_TO.Value);
                }
                if (this.EXECUTE_DEPARTMENT_ID.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.EXECUTE_DEPARTMENT_ID == this.EXECUTE_DEPARTMENT_ID.Value);
                }
                if (this.REQUEST_DEPARTMENT_ID.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.REQUEST_DEPARTMENT_ID == this.REQUEST_DEPARTMENT_ID.Value);
                }
                if (this.REQUEST_DEPARTMENT_IDs != null)
                {
                    search.listHisServiceReqExpression.Add(o => this.REQUEST_DEPARTMENT_IDs.Contains(o.REQUEST_DEPARTMENT_ID));
                }
                if (this.TREATMENT_IDs != null)
                {
                    search.listHisServiceReqExpression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                if (this.TRACKING_ID.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.TRACKING_ID.HasValue && o.TRACKING_ID.Value == this.TRACKING_ID.Value);
                }
                if (this.TRACKING_IDs != null)
                {
                    search.listHisServiceReqExpression.Add(o => o.TRACKING_ID.HasValue && this.TRACKING_IDs.Contains(o.TRACKING_ID.Value));
                }
                if (this.HAS_EXECUTE.HasValue && this.HAS_EXECUTE.Value)
                {
                    listHisServiceReqExpression.Add(o => !o.IS_NO_EXECUTE.HasValue || o.IS_NO_EXECUTE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.HAS_EXECUTE.HasValue && !this.HAS_EXECUTE.Value)
                {
                    listHisServiceReqExpression.Add(o => o.IS_NO_EXECUTE.HasValue && o.IS_NO_EXECUTE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (!String.IsNullOrEmpty(this.BARCODE__EXACT))
                {
                    search.listHisServiceReqExpression.Add(o => o.BARCODE == this.BARCODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.SERVICE_REQ_CODE__EXACT))
                {
                    search.listHisServiceReqExpression.Add(o => o.SERVICE_REQ_CODE == this.SERVICE_REQ_CODE__EXACT);
                }
                if (this.PAAN_LIQUID_ID.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.PAAN_LIQUID_ID.HasValue && o.PAAN_LIQUID_ID.Value == this.PAAN_LIQUID_ID.Value);
                }
                if (this.PAAN_LIQUID_IDs != null)
                {
                    search.listHisServiceReqExpression.Add(o => o.PAAN_LIQUID_ID.HasValue && this.PAAN_LIQUID_IDs.Contains(o.PAAN_LIQUID_ID.Value));
                }
                if (this.DHST_ID.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.DHST_ID.HasValue && o.DHST_ID.Value == this.DHST_ID.Value);
                }
                if (this.DHST_IDs != null)
                {
                    search.listHisServiceReqExpression.Add(o => o.DHST_ID.HasValue && this.DHST_IDs.Contains(o.DHST_ID.Value));
                }
                if (!String.IsNullOrEmpty(this.TREATMENT_CODE__EXACT))
                {
                    listHisServiceReqExpression.Add(o => o.TDL_TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.REQUEST_LOGINNAME__EXACT))
                {
                    listHisServiceReqExpression.Add(o => o.REQUEST_LOGINNAME == this.REQUEST_LOGINNAME__EXACT);
                }
                if (!String.IsNullOrEmpty(this.SERVICE_REQ_CODE__EXACT))
                {
                    listHisServiceReqExpression.Add(o => o.SERVICE_REQ_CODE == this.SERVICE_REQ_CODE__EXACT);
                }
                if (this.SERVICE_REQ_TYPE_IDs != null)
                {
                    listHisServiceReqExpression.Add(o => this.SERVICE_REQ_TYPE_IDs.Contains(o.SERVICE_REQ_TYPE_ID));
                }
                if (this.REQUEST_ROOM_IDs != null)
                {
                    listHisServiceReqExpression.Add(o => this.REQUEST_ROOM_IDs.Contains(o.REQUEST_ROOM_ID));
                }
                if (this.REQUEST_ROOM_ID.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.REQUEST_ROOM_ID == this.REQUEST_ROOM_ID.Value);
                }
                if (this.EXECUTE_ROOM_ID.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.EXECUTE_ROOM_ID == this.EXECUTE_ROOM_ID.Value);
                }
                if (this.SAMPLE_ROOM_ID.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.SAMPLE_ROOM_ID.HasValue && o.SAMPLE_ROOM_ID.Value == this.SAMPLE_ROOM_ID.Value);
                }
                if (this.INTRUCTION_DATE_FROM.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.INTRUCTION_DATE >= this.INTRUCTION_DATE_FROM.Value);
                }
                if (this.INTRUCTION_DATE_TO.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.INTRUCTION_DATE <= this.INTRUCTION_DATE_TO.Value);
                }
                if (this.INTRUCTION_DATE__EQUAL.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.INTRUCTION_DATE == this.INTRUCTION_DATE__EQUAL.Value);
                }
                if (this.USE_TIME_OR_INTRUCTION_TIME_FROM.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => (o.USE_TIME.HasValue && o.USE_TIME >= this.USE_TIME_OR_INTRUCTION_TIME_FROM.Value) || (!o.USE_TIME.HasValue && o.INTRUCTION_TIME >= this.USE_TIME_OR_INTRUCTION_TIME_FROM.Value));
                }
                if (this.USE_TIME_OR_INTRUCTION_TIME_TO.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => (o.USE_TIME.HasValue && o.USE_TIME <= this.USE_TIME_OR_INTRUCTION_TIME_TO.Value) || (!o.USE_TIME.HasValue && o.INTRUCTION_TIME <= this.USE_TIME_OR_INTRUCTION_TIME_TO.Value));
                }

                if (this.ATTACHED_IDs != null)
                {
                    search.listHisServiceReqExpression.Add(o => o.ATTACHED_ID.HasValue && this.ATTACHED_IDs.Contains(o.ATTACHED_ID.Value));
                }
                if (!String.IsNullOrEmpty(this.KEYWORD__SERVICE_REQ_CODE__TREATMENT_CODE__PATIENT_NAME__PATIENT_CODE))
                {
                    string keyword = this.KEYWORD__SERVICE_REQ_CODE__TREATMENT_CODE__PATIENT_NAME__PATIENT_CODE.ToLower().Trim();
                    listHisServiceReqExpression.Add(o =>
                        o.TDL_PATIENT_NAME.ToLower().Contains(keyword)
                        || o.SERVICE_REQ_CODE.ToLower().Contains(keyword)
                        || o.TDL_TREATMENT_CODE.ToLower().Contains(keyword)
                        || o.TDL_PATIENT_CODE.ToLower().Contains(keyword)
                        );

                }

                if (!String.IsNullOrEmpty(this.KEYWORD__SERVICE_REQ_CODE__TREATMENT_CODE__PATIENT_NAME))
                {
                    string keyword = this.KEYWORD__SERVICE_REQ_CODE__TREATMENT_CODE__PATIENT_NAME.ToLower().Trim();
                    listHisServiceReqExpression.Add(o =>
                        o.TDL_PATIENT_NAME.ToLower().Contains(keyword)
                        || o.SERVICE_REQ_CODE.ToLower().Contains(keyword)
                        || o.TDL_TREATMENT_CODE.ToLower().Contains(keyword)
                        );
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    string keyword = this.KEY_WORD.ToLower().Trim();
                    listHisServiceReqExpression.Add(o =>
                        o.TDL_PATIENT_NAME.ToLower().Contains(keyword)
                        || o.SERVICE_REQ_CODE.ToLower().Contains(keyword)
                        || o.TDL_TREATMENT_CODE.ToLower().Contains(keyword)
                        || o.TDL_PATIENT_CODE.ToLower().Contains(keyword)
                        );
                }

                if (this.EXECUTE_ROOM_IDs != null)
                {
                    listHisServiceReqExpression.Add(o => this.EXECUTE_ROOM_IDs.Contains(o.EXECUTE_ROOM_ID));
                }
                if (this.NOT_IN_SERVICE_REQ_TYPE_IDs != null)
                {
                    search.listHisServiceReqExpression.Add(o => !this.NOT_IN_SERVICE_REQ_TYPE_IDs.Contains(o.SERVICE_REQ_TYPE_ID));
                }
                if (this.IS_SEND_LIS.HasValue && this.IS_SEND_LIS.Value)
                {
                    listHisServiceReqExpression.Add(o => o.LIS_STT_ID.HasValue && o.LIS_STT_ID.Value != LisUtil.LIS_STT_ID__UPDATE);
                }
                if (this.IS_SEND_LIS.HasValue && !this.IS_SEND_LIS.Value)
                {
                    listHisServiceReqExpression.Add(o => !o.LIS_STT_ID.HasValue || o.LIS_STT_ID.Value == LisUtil.LIS_STT_ID__UPDATE);
                }
                if (this.IS_SEND_PACS.HasValue && this.IS_SEND_PACS.Value)
                {
                    listHisServiceReqExpression.Add(o => o.PACS_STT_ID.HasValue);
                }
                if (this.IS_SEND_PACS.HasValue && !this.IS_SEND_PACS.Value)
                {
                    listHisServiceReqExpression.Add(o => !o.PACS_STT_ID.HasValue);
                }
                if (this.NOT_IN_IDs != null && this.NOT_IN_IDs.Count > 0)
                {
                    search.listHisServiceReqExpression.Add(o => !this.NOT_IN_IDs.Contains(o.ID));
                }
                if (this.PREVIOUS_SERVICE_REQ_ID.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.PREVIOUS_SERVICE_REQ_ID.HasValue && o.PREVIOUS_SERVICE_REQ_ID.Value == this.PREVIOUS_SERVICE_REQ_ID.Value);
                }
                if (this.PREVIOUS_SERVICE_REQ_IDs != null)
                {
                    search.listHisServiceReqExpression.Add(o => o.PREVIOUS_SERVICE_REQ_ID.HasValue && this.PREVIOUS_SERVICE_REQ_IDs.Contains(o.PREVIOUS_SERVICE_REQ_ID.Value));
                }
                if (this.IS_MAIN_EXAM.HasValue && this.IS_MAIN_EXAM.Value)
                {
                    listHisServiceReqExpression.Add(o => o.IS_MAIN_EXAM.HasValue && o.IS_MAIN_EXAM.Value == Constant.IS_TRUE);
                }
                if (this.IS_MAIN_EXAM.HasValue && !this.IS_MAIN_EXAM.Value)
                {
                    listHisServiceReqExpression.Add(o => !o.IS_MAIN_EXAM.HasValue || o.IS_MAIN_EXAM.Value != Constant.IS_TRUE);
                }
                if (this.IS_NOT_SENT__OR__UPDATED.HasValue && this.IS_NOT_SENT__OR__UPDATED.Value)
                {
                    listHisServiceReqExpression.Add(o => !o.IS_SENT_EXT.HasValue || o.IS_SENT_EXT != Constant.IS_TRUE || o.IS_UPDATED_EXT == Constant.IS_TRUE);
                }
                if (this.IS_NOT_SENT__OR__UPDATED.HasValue && !this.IS_NOT_SENT__OR__UPDATED.Value)
                {
                    listHisServiceReqExpression.Add(o => o.IS_SENT_EXT == Constant.IS_TRUE && (!o.IS_UPDATED_EXT.HasValue || o.IS_UPDATED_EXT != Constant.IS_TRUE));
                }

                if (!String.IsNullOrWhiteSpace(this.SESSION_CODE__EXACT))
                {
                    listHisServiceReqExpression.Add(o => o.SESSION_CODE == this.SESSION_CODE__EXACT);
                }
                if (this.IS_NOT_SURGERY__OR__IS__APPROVED__OR__IS_EMERGENCY.HasValue && this.IS_NOT_SURGERY__OR__IS__APPROVED__OR__IS_EMERGENCY.Value)
                {
                    listHisServiceReqExpression.Add(o =>
                        o.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT ||
                        o.PTTT_APPROVAL_STT_ID == IMSys.DbConfig.HIS_RS.HIS_PTTT_APPROVAL_STT.ID__APPROVED ||
                        o.IS_EMERGENCY == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_NOT_SURGERY__OR__IS__APPROVED__OR__IS_EMERGENCY.HasValue && !this.IS_NOT_SURGERY__OR__IS__APPROVED__OR__IS_EMERGENCY.Value)
                {
                    listHisServiceReqExpression.Add(o =>
                        o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT &&
                        o.PTTT_APPROVAL_STT_ID != IMSys.DbConfig.HIS_RS.HIS_PTTT_APPROVAL_STT.ID__APPROVED &&
                        o.IS_EMERGENCY != MOS.UTILITY.Constant.IS_TRUE);
                }

                if (this.HAS_RATION_SUM_ID.HasValue)
                {
                    if (this.HAS_RATION_SUM_ID.Value)
                    {
                        listHisServiceReqExpression.Add(o => o.RATION_SUM_ID.HasValue);
                    }
                    else
                    {
                        listHisServiceReqExpression.Add(o => !o.RATION_SUM_ID.HasValue);
                    }
                }
                if (this.HAS_PTTT_CALENDAR_ID.HasValue)
                {
                    if (this.HAS_PTTT_CALENDAR_ID.Value)
                    {
                        listHisServiceReqExpression.Add(o => o.PTTT_CALENDAR_ID.HasValue);
                    }
                    else
                    {
                        listHisServiceReqExpression.Add(o => !o.PTTT_CALENDAR_ID.HasValue);
                    }
                }
                if (this.PTTT_CALENDAR_ID.HasValue)
                {
                    listHisServiceReqExpression.Add(o => o.PTTT_CALENDAR_ID.HasValue && o.PTTT_CALENDAR_ID.Value == this.PTTT_CALENDAR_ID.Value);
                }
                if (this.PTTT_CALENDAR_IDs != null)
                {
                    listHisServiceReqExpression.Add(o => o.PTTT_CALENDAR_ID.HasValue && this.PTTT_CALENDAR_IDs.Contains(o.PTTT_CALENDAR_ID.Value));
                }
                if (this.RATION_TIME_ID.HasValue)
                {
                    listHisServiceReqExpression.Add(o => o.RATION_TIME_ID.HasValue && o.RATION_TIME_ID.Value == this.RATION_TIME_ID.Value);
                }
                if (this.RATION_TIME_IDs != null)
                {
                    listHisServiceReqExpression.Add(o => o.RATION_TIME_ID.HasValue && this.RATION_TIME_IDs.Contains(o.RATION_TIME_ID.Value));
                }
                if (this.RATION_SUM_ID.HasValue)
                {
                    listHisServiceReqExpression.Add(o => o.RATION_SUM_ID.HasValue && o.RATION_SUM_ID.Value == this.RATION_SUM_ID.Value);
                }
                if (this.RATION_SUM_IDs != null)
                {
                    listHisServiceReqExpression.Add(o => o.RATION_SUM_ID.HasValue && this.RATION_SUM_IDs.Contains(o.RATION_SUM_ID.Value));
                }
                if (this.PTTT_APPROVAL_STT_ID.HasValue)
                {
                    listHisServiceReqExpression.Add(o => o.PTTT_APPROVAL_STT_ID.HasValue && o.PTTT_APPROVAL_STT_ID.Value == this.PTTT_APPROVAL_STT_ID.Value);
                }
                if (this.PTTT_APPROVAL_STT_IDs != null)
                {
                    listHisServiceReqExpression.Add(o => o.PTTT_APPROVAL_STT_ID.HasValue && this.PTTT_APPROVAL_STT_IDs.Contains(o.PTTT_APPROVAL_STT_ID.Value));
                }
                if (this.TDL_TREATMENT_TYPE_ID.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.TDL_TREATMENT_TYPE_ID.HasValue && o.TDL_TREATMENT_TYPE_ID.Value == this.TDL_TREATMENT_TYPE_ID.Value);
                }
                if (this.TDL_TREATMENT_TYPE_IDs != null)
                {
                    search.listHisServiceReqExpression.Add(o => o.TDL_TREATMENT_TYPE_ID.HasValue && this.TDL_TREATMENT_TYPE_IDs.Contains(o.TDL_TREATMENT_TYPE_ID.Value));
                }
                if (this.KIDNEY_SHIFT.HasValue)
                {
                    listHisServiceReqExpression.Add(o => o.KIDNEY_SHIFT == this.KIDNEY_SHIFT);
                }
                if (this.MACHINE_ID.HasValue)
                {
                    listHisServiceReqExpression.Add(o => o.MACHINE_ID == this.MACHINE_ID);
                }
                if (this.MACHINE_IDs != null)
                {
                    listHisServiceReqExpression.Add(o => o.MACHINE_ID.HasValue && this.MACHINE_IDs.Contains(o.MACHINE_ID.Value));
                }
                if (this.KIDNEY_SHIFTs != null)
                {
                    listHisServiceReqExpression.Add(o => o.KIDNEY_SHIFT.HasValue && this.KIDNEY_SHIFTs.Contains(o.KIDNEY_SHIFT.Value));
                }
                if (this.IS_KIDNEY.HasValue && this.IS_KIDNEY.Value)
                {
                    listHisServiceReqExpression.Add(o => o.IS_KIDNEY.HasValue && o.IS_KIDNEY == Constant.IS_TRUE);
                }
                if (this.IS_KIDNEY.HasValue && !this.IS_KIDNEY.Value)
                {
                    listHisServiceReqExpression.Add(o => !o.IS_KIDNEY.HasValue || o.IS_KIDNEY != Constant.IS_TRUE);
                }
                if (this.IS_NO_EXECUTE.HasValue)
                {
                    if (this.IS_NO_EXECUTE.Value)
                    {
                        listHisServiceReqExpression.Add(o => o.IS_NO_EXECUTE.HasValue && o.IS_NO_EXECUTE == Constant.IS_TRUE);
                    }
                    else
                    {
                        listHisServiceReqExpression.Add(o => !o.IS_NO_EXECUTE.HasValue || o.IS_NO_EXECUTE != Constant.IS_TRUE);
                    }
                }
                if (this.ID__NOT_EQUAL.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.ID != this.ID__NOT_EQUAL.Value);
                }
                if (this.IS_NOT_KSK_REQURIED_APROVAL__OR__IS_KSK_APPROVE.HasValue)
                {
                    if (this.IS_NOT_KSK_REQURIED_APROVAL__OR__IS_KSK_APPROVE.Value)
                    {
                        listHisServiceReqExpression.Add(o => !o.TDL_KSK_IS_REQUIRED_APPROVAL.HasValue || o.TDL_KSK_IS_REQUIRED_APPROVAL.Value != Constant.IS_TRUE || (o.TDL_IS_KSK_APPROVE.HasValue && o.TDL_IS_KSK_APPROVE.Value == Constant.IS_TRUE));
                    }
                    else
                    {
                        listHisServiceReqExpression.Add(o => o.TDL_KSK_IS_REQUIRED_APPROVAL.HasValue && o.TDL_KSK_IS_REQUIRED_APPROVAL.Value == Constant.IS_TRUE && (!o.TDL_IS_KSK_APPROVE.HasValue || o.TDL_IS_KSK_APPROVE.Value != Constant.IS_TRUE));
                    }
                }
                if (this.IS_HOME_PRES.HasValue)
                {
                    if (this.IS_HOME_PRES.Value)
                    {
                        listHisServiceReqExpression.Add(o => o.IS_HOME_PRES.HasValue && o.IS_HOME_PRES.Value == Constant.IS_TRUE);
                    }
                    else
                    {
                        listHisServiceReqExpression.Add(o => !o.IS_HOME_PRES.HasValue || o.IS_HOME_PRES.Value != Constant.IS_TRUE);
                    }
                }
                if (this.PATIENT_CLASSIFY_ID.HasValue)
                {
                    listHisServiceReqExpression.Add(o => o.TDL_PATIENT_CLASSIFY_ID.HasValue && o.TDL_PATIENT_CLASSIFY_ID == this.PATIENT_CLASSIFY_ID.Value);
                }
                if (this.PATIENT_CLASSIFY_IDs != null)
                {
                    listHisServiceReqExpression.Add(o => o.TDL_PATIENT_CLASSIFY_ID.HasValue && this.PATIENT_CLASSIFY_IDs.Contains(o.TDL_PATIENT_CLASSIFY_ID.Value));
                }
                if (this.IS_INFORM_RESULT_BY_SMS.HasValue && this.IS_INFORM_RESULT_BY_SMS.Value)
                {
                    listHisServiceReqExpression.Add(o => o.IS_INFORM_RESULT_BY_SMS == Constant.IS_TRUE);
                }
                if (this.IS_INFORM_RESULT_BY_SMS.HasValue && !this.IS_INFORM_RESULT_BY_SMS.Value)
                {
                    listHisServiceReqExpression.Add(o => !o.IS_INFORM_RESULT_BY_SMS.HasValue || o.IS_INFORM_RESULT_BY_SMS != Constant.IS_TRUE);
                }
                if (this.IS_NOT_IN_DEBT.HasValue && this.IS_NOT_IN_DEBT.Value)
                {
                    listHisServiceReqExpression.Add(o => o.IS_NOT_IN_DEBT == Constant.IS_TRUE);
                }
                if (this.IS_NOT_IN_DEBT.HasValue && !this.IS_NOT_IN_DEBT.Value)
                {
                    listHisServiceReqExpression.Add(o => !o.IS_NOT_IN_DEBT.HasValue || o.IS_NOT_IN_DEBT != Constant.IS_TRUE);
                }
                if (!String.IsNullOrWhiteSpace(this.TDL_PATIENT_CODE__EXACT))
                {
                    listHisServiceReqExpression.Add(o => o.TDL_PATIENT_CODE == this.TDL_PATIENT_CODE__EXACT);
                }
                if (this.HAS_RESULTING_ORDER.HasValue && this.HAS_RESULTING_ORDER.Value)
                {
                    listHisServiceReqExpression.Add(o => o.RESULTING_ORDER.HasValue);
                }
                if (this.HAS_RESULTING_ORDER.HasValue && !this.HAS_RESULTING_ORDER.Value)
                {
                    listHisServiceReqExpression.Add(o => !o.RESULTING_ORDER.HasValue);
                }
                if (this.HAS_SAMPLED.HasValue)
                {
                    if (this.HAS_SAMPLED.Value)
                    {
                        listHisServiceReqExpression.Add(o => o.IS_SAMPLED.HasValue && o.IS_SAMPLED == Constant.IS_TRUE);
                    }
                    else
                    {
                        listHisServiceReqExpression.Add(o => !o.IS_SAMPLED.HasValue || o.IS_SAMPLED != Constant.IS_TRUE);
                    }
                }
                if (!String.IsNullOrEmpty(this.BLOCK__EXACT))
                {
                    listHisServiceReqExpression.Add(o => o.BLOCK == this.BLOCK__EXACT);
                }
                if (!String.IsNullOrEmpty(this.ASSIGN_TURN_CODE__EXACT))
                {
                    listHisServiceReqExpression.Add(o => o.ASSIGN_TURN_CODE == this.ASSIGN_TURN_CODE__EXACT);
                }
                if (this.HAS_TRACKING_ID.HasValue)
                {
                    if (this.HAS_TRACKING_ID.Value)
                    {
                        listHisServiceReqExpression.Add(o => o.TRACKING_ID.HasValue);
                    }
                    else
                    {
                        listHisServiceReqExpression.Add(o => !o.TRACKING_ID.HasValue);
                    }
                }
                if (this.HAS_TDL_KSK_CONTRACT_ID.HasValue)
                {
                    if (this.HAS_TDL_KSK_CONTRACT_ID.Value)
                    {
                        listHisServiceReqExpression.Add(o => o.TDL_KSK_CONTRACT_ID.HasValue);
                    }
                    else
                    {
                        listHisServiceReqExpression.Add(o => !o.TDL_KSK_CONTRACT_ID.HasValue);
                    }
                }
                if (this.SERVICE_IDs != null)
                {
                    var searchPredicate = PredicateBuilder.False<HIS_SERVICE_REQ>();

                    foreach (long id in this.SERVICE_IDs)
                    {
                        var closureVariable = "," + id.ToString() + ",";//can khai bao bien rieng de cho vao menh de ben duoi
                        searchPredicate = searchPredicate.Or(o => (o.TDL_SERVICE_IDS != null && ("," + o.TDL_SERVICE_IDS + ",").Contains(closureVariable)));
                    }
                    listHisServiceReqExpression.Add(searchPredicate);
                }

                if (this.HAS_ATTACH_ASSIGN_PRINT_TYPE_CODE.HasValue)
                {
                    if (this.HAS_ATTACH_ASSIGN_PRINT_TYPE_CODE.Value)
                    {
                        listHisServiceReqExpression.Add(o => o.ATTACH_ASSIGN_PRINT_TYPE_CODE != null);
                    }
                    else
                    {
                        listHisServiceReqExpression.Add(o => o.ATTACH_ASSIGN_PRINT_TYPE_CODE == null);
                    }
                }
                if (this.IS_ENOUGH_SUBCLINICAL_PRES.HasValue && this.IS_ENOUGH_SUBCLINICAL_PRES.Value)
                {
                    listHisServiceReqExpression.Add(o => o.IS_ENOUGH_SUBCLINICAL_PRES == Constant.IS_TRUE);
                }
                if (this.IS_ENOUGH_SUBCLINICAL_PRES.HasValue && !this.IS_ENOUGH_SUBCLINICAL_PRES.Value)
                {
                    listHisServiceReqExpression.Add(o => o.IS_ENOUGH_SUBCLINICAL_PRES == null || o.IS_ENOUGH_SUBCLINICAL_PRES != Constant.IS_TRUE);
                }
                if (this.TDL_KSK_CONTRACT_ID.HasValue)
                {
                    listHisServiceReqExpression.Add(o => o.TDL_KSK_CONTRACT_ID.HasValue && o.TDL_KSK_CONTRACT_ID == this.TDL_KSK_CONTRACT_ID.Value);
                }
                if (this.CARER_CARD_BORROW_ID.HasValue)
                {
                    search.listHisServiceReqExpression.Add(o => o.CARER_CARD_BORROW_ID == this.CARER_CARD_BORROW_ID.Value);
                }
                if (this.CARER_CARD_BORROW_IDs != null)
                {
                    search.listHisServiceReqExpression.Add(o => o.CARER_CARD_BORROW_ID.HasValue && this.CARER_CARD_BORROW_IDs.Contains(o.CARER_CARD_BORROW_ID.Value));
                }
                if (this.IS_FOR_AUTO_CREATE_RATION.HasValue)
                {
                    if (this.IS_FOR_AUTO_CREATE_RATION.Value)
                    {
                        listHisServiceReqExpression.Add(o => o.IS_FOR_AUTO_CREATE_RATION.HasValue && o.IS_FOR_AUTO_CREATE_RATION == Constant.IS_TRUE);
                    }
                    else
                    {
                        listHisServiceReqExpression.Add(o => !o.IS_FOR_AUTO_CREATE_RATION.HasValue || o.IS_FOR_AUTO_CREATE_RATION != Constant.IS_TRUE);
                    }
                }
                if (this.USE_TIME.HasValue)
                {
                    listHisServiceReqExpression.Add(o => o.USE_TIME == this.USE_TIME);
                }
                if (this.USE_TIME_FROM.HasValue)
                {
                    listHisServiceReqExpression.Add(o => o.USE_TIME >= this.USE_TIME_FROM.Value);
                }
                if (this.USE_TIME_TO.HasValue)
                {
                    listHisServiceReqExpression.Add(o => o.USE_TIME <= this.USE_TIME_TO.Value);
                }
                if (this.TRACKING_ID__OR__USED_FOR_TRACKING_ID.HasValue)
                {
                    listHisServiceReqExpression.Add(o => o.TRACKING_ID == this.TRACKING_ID__OR__USED_FOR_TRACKING_ID.Value || o.USED_FOR_TRACKING_ID == this.TRACKING_ID__OR__USED_FOR_TRACKING_ID.Value);
                }
                if (this.USED_FOR_TRACKING_ID.HasValue)
                {
                    listHisServiceReqExpression.Add(o => o.USED_FOR_TRACKING_ID == this.USED_FOR_TRACKING_ID.Value);
                }
                if (this.BED_LOG_ID.HasValue)
                {
                    listHisServiceReqExpression.Add(o => o.BED_LOG_ID == this.BED_LOG_ID.Value);
                }
                if (this.IS_RESTRICTED_KSK.HasValue && this.IS_RESTRICTED_KSK.Value)
                {
                    List<long> kskContractIds = TokenManager.GetAccessibleKskContract() ?? new List<long>();
                    listHisServiceReqExpression.Add
                        (o => !o.TDL_KSK_CONTRACT_ID.HasValue
                              ||
                              (o.TDL_KSK_CONTRACT_ID.HasValue &&
                                (
                                    (!o.TDL_KSK_CONTRACT_IS_RESTRICTED.HasValue || o.TDL_KSK_CONTRACT_IS_RESTRICTED.Value != Constant.IS_TRUE)
                                    ||
                                    (
                                        o.TDL_KSK_CONTRACT_IS_RESTRICTED.HasValue && o.TDL_KSK_CONTRACT_IS_RESTRICTED.Value == Constant.IS_TRUE &&
                                            kskContractIds.Contains(o.TDL_KSK_CONTRACT_ID.Value)
                                    )
                                )
                              )
                        );
                }
                if (this.ALLOW_SEND_PACS.HasValue)
                {
                    if (this.ALLOW_SEND_PACS.Value)
                    {
                        listHisServiceReqExpression.Add(o => o.ALLOW_SEND_PACS.HasValue && o.ALLOW_SEND_PACS == Constant.IS_TRUE);
                    }
                    else
                    {
                        listHisServiceReqExpression.Add(o => !o.ALLOW_SEND_PACS.HasValue || o.ALLOW_SEND_PACS != Constant.IS_TRUE);
                    }
                }

                if (this.SERVICE_REQ_CODES != null && this.SERVICE_REQ_CODES.Count > 0)
                {
                    listHisServiceReqExpression.Add(o => !string.IsNullOrWhiteSpace(o.SERVICE_REQ_CODE) && this.SERVICE_REQ_CODES.Contains(o.SERVICE_REQ_CODE));
                }
                search.listHisServiceReqExpression.AddRange(listHisServiceReqExpression);
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
                search.DynamicColumns = this.ColumnParams;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisServiceReqExpression.Clear();
                search.listHisServiceReqExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

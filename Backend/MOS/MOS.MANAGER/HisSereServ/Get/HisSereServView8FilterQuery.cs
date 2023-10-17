using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServ
{
    public class HisSereServView8FilterQuery : HisSereServView8Filter
    {
        public HisSereServView8FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_8, bool>>> listVHisSereServ8Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_8, bool>>>();



        internal HisSereServSO Query()
        {
            HisSereServSO search = new HisSereServSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisSereServ8Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisSereServ8Expression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion


                if (this.TDL_HEIN_SERVICE_TYPE_ID.HasValue)
                {
                    listVHisSereServ8Expression.Add(o => o.TDL_HEIN_SERVICE_TYPE_ID.HasValue && o.TDL_HEIN_SERVICE_TYPE_ID == this.TDL_HEIN_SERVICE_TYPE_ID.Value);
                }
                if (this.IS_EXPEND.HasValue && this.IS_EXPEND.Value)
                {
                    listVHisSereServ8Expression.Add(o => o.IS_EXPEND.HasValue && o.IS_EXPEND.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_EXPEND.HasValue && !this.IS_EXPEND.Value)
                {
                    listVHisSereServ8Expression.Add(o => !o.IS_EXPEND.HasValue || o.IS_EXPEND.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.HAS_EXECUTE.HasValue && !this.HAS_EXECUTE.Value)
                {
                    listVHisSereServ8Expression.Add(o => o.IS_NO_EXECUTE.HasValue && o.IS_NO_EXECUTE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.HAS_EXECUTE.HasValue && this.HAS_EXECUTE.Value)
                {
                    listVHisSereServ8Expression.Add(o => !o.IS_NO_EXECUTE.HasValue || o.IS_NO_EXECUTE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.END_TIME_TO.HasValue)
                {
                    listVHisSereServ8Expression.Add(o => o.END_TIME.HasValue && o.END_TIME <= this.END_TIME_TO.Value);
                }
                if (this.END_TIME_FROM.HasValue)
                {
                    listVHisSereServ8Expression.Add(o => o.END_TIME.HasValue && o.END_TIME >= this.END_TIME_FROM.Value);
                }
                if (this.BEGIN_TIME_TO.HasValue)
                {
                    listVHisSereServ8Expression.Add(o => o.BEGIN_TIME.HasValue && o.BEGIN_TIME <= this.BEGIN_TIME_TO.Value);
                }
                if (this.BEGIN_TIME_FROM.HasValue)
                {
                    listVHisSereServ8Expression.Add(o => o.BEGIN_TIME.HasValue && o.BEGIN_TIME >= this.BEGIN_TIME_FROM.Value);
                }
                if (this.SERVICE_TYPE_ID.HasValue)
                {
                    listVHisSereServ8Expression.Add(o => o.TDL_SERVICE_TYPE_ID == this.SERVICE_TYPE_ID.Value);
                }
                if (this.SERVICE_TYPE_IDs != null)
                {
                    listVHisSereServ8Expression.Add(o => this.SERVICE_TYPE_IDs.Contains(o.TDL_SERVICE_TYPE_ID));
                }

                if (this.PTTT_PRIORITY_ID.HasValue)
                {
                    listVHisSereServ8Expression.Add(o => o.PTTT_PRIORITY_ID == this.PTTT_PRIORITY_ID.Value);
                }
                if (this.PTTT_PRIORITY_IDs != null)
                {
                    listVHisSereServ8Expression.Add(o => o.PTTT_PRIORITY_ID.HasValue && this.PTTT_PRIORITY_IDs.Contains(o.PTTT_PRIORITY_ID.Value));
                }
                if (this.REQ_SURG_TREATMENT_TYPE_ID.HasValue)
                {
                    listVHisSereServ8Expression.Add(o => o.REQ_SURG_TREATMENT_TYPE_ID.HasValue && o.REQ_SURG_TREATMENT_TYPE_ID.Value == this.REQ_SURG_TREATMENT_TYPE_ID.Value);
                }
                if (this.REQ_SURG_TREATMENT_TYPE_IDs != null)
                {
                    listVHisSereServ8Expression.Add(o => o.REQ_SURG_TREATMENT_TYPE_ID.HasValue && this.REQ_SURG_TREATMENT_TYPE_IDs.Contains(o.REQ_SURG_TREATMENT_TYPE_ID.Value));
                }
                if (this.TDL_EXECUTE_ROOM_ID.HasValue)
                {
                    listVHisSereServ8Expression.Add(o => o.TDL_EXECUTE_ROOM_ID == this.TDL_EXECUTE_ROOM_ID.Value);
                }
                if (this.TDL_EXECUTE_ROOM_IDs != null)
                {
                    listVHisSereServ8Expression.Add(o => this.TDL_EXECUTE_ROOM_IDs.Contains(o.TDL_EXECUTE_ROOM_ID));
                }
                if (this.HAS_SERVICE_PTTT_GROUP_ID.HasValue)
                {
                    if (this.HAS_SERVICE_PTTT_GROUP_ID.Value)
                    {
                        listVHisSereServ8Expression.Add(o => o.SERVICE_PTTT_GROUP_ID.HasValue);
                    }
                    else
                    {
                        listVHisSereServ8Expression.Add(o => !o.SERVICE_PTTT_GROUP_ID.HasValue);
                    }
                }

                if (this.IS_GATHER_DATA.HasValue && this.IS_GATHER_DATA.Value)
                {
                    listVHisSereServ8Expression.Add(o => o.IS_GATHER_DATA.HasValue && o.IS_GATHER_DATA.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_GATHER_DATA.HasValue && !this.IS_GATHER_DATA.Value)
                {
                    listVHisSereServ8Expression.Add(o => !o.IS_GATHER_DATA.HasValue || o.IS_GATHER_DATA.Value != MOS.UTILITY.Constant.IS_TRUE);
                }

                if (this.IS_FEE.HasValue && this.IS_FEE.Value)
                {
                    listVHisSereServ8Expression.Add(o => o.IS_FEE.HasValue && o.IS_FEE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_FEE.HasValue && !this.IS_FEE.Value)
                {
                    listVHisSereServ8Expression.Add(o => !o.IS_FEE.HasValue || o.IS_FEE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (!String.IsNullOrEmpty(this.SERVICE_REQ_CODE__EXACT))
                {
                    listVHisSereServ8Expression.Add(o => o.SERVICE_REQ_CODE != null && o.SERVICE_REQ_CODE.Equals(this.SERVICE_REQ_CODE__EXACT));
                }

                if (this.HAS_EKIP.HasValue)
                {
                    if (this.HAS_EKIP.Value)
                    {
                        listVHisSereServ8Expression.Add(o => o.EKIP_ID.HasValue);
                    }
                    else
                    {
                        listVHisSereServ8Expression.Add(o => !o.EKIP_ID.HasValue);
                    }
                }
                if (this.SERVICE_REQ_STT_IDs != null)
                {
                    listVHisSereServ8Expression.Add(o => this.SERVICE_REQ_STT_IDs.Contains(o.SERVICE_REQ_STT_ID));
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisSereServ8Expression.Add(o =>
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_SERVICE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_SERVICE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.PTTT_GROUP_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.PTTT_GROUP_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.EMOTIONLESS_METHOD_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.EMOTIONLESS_METHOD_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.PTTT_PRIORITY_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.PTTT_PRIORITY_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_TYPE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.EXECUTE_ROOM_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.EXECUTE_ROOM_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_TYPE_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisSereServ8Expression.AddRange(listVHisSereServ8Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisSereServ8Expression.Clear();
                search.listVHisSereServ8Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}

using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisSereServSO : StagingObjectBase
    {
        public HisSereServSO()
        {
            this.Init();
        }

        public HisSereServSO(bool isIncludeDeleted)
        {
            this.IsIncludeDeleted = isIncludeDeleted;
            this.Init();
        }

        public void Init()
        {
            if (!this.IsIncludeDeleted)
            {
                //sere_serv neu co service_req_id null ==> service_req da bi xoa ==> ko lay ra sere_serv nay
                listHisSereServExpression.Add(o => (!o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1) && o.SERVICE_REQ_ID.HasValue && o.TDL_PATIENT_ID.HasValue && o.TDL_TREATMENT_ID.HasValue);
                listVHisSereServExpression.Add(o => (!o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1) && o.SERVICE_REQ_ID.HasValue && o.TDL_PATIENT_ID.HasValue && o.TDL_TREATMENT_ID.HasValue);
                listVHisSereServ1Expression.Add(o => (!o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1) && o.SERVICE_REQ_ID.HasValue && o.TDL_PATIENT_ID.HasValue && o.TDL_TREATMENT_ID.HasValue);
                listVHisSereServ2Expression.Add(o => (!o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1) && o.SERVICE_REQ_ID.HasValue && o.TDL_PATIENT_ID.HasValue && o.TDL_TREATMENT_ID.HasValue);
                listVHisSereServ3Expression.Add(o => (!o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1) && o.SERVICE_REQ_ID.HasValue && o.TDL_PATIENT_ID.HasValue && o.TDL_TREATMENT_ID.HasValue);
                listVHisSereServ4Expression.Add(o => (!o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1) && o.SERVICE_REQ_ID.HasValue && o.TDL_PATIENT_ID.HasValue && o.TDL_TREATMENT_ID.HasValue);
                listVHisSereServ5Expression.Add(o => (!o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1) && o.SERVICE_REQ_ID.HasValue && o.TDL_PATIENT_ID.HasValue && o.TDL_TREATMENT_ID.HasValue);
                listVHisSereServ6Expression.Add(o => (!o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1) && o.SERVICE_REQ_ID.HasValue && o.TDL_PATIENT_ID.HasValue && o.TDL_TREATMENT_ID.HasValue);
                listVHisSereServ7Expression.Add(o => (!o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1) && o.SERVICE_REQ_ID.HasValue && o.TDL_PATIENT_ID.HasValue && o.TDL_TREATMENT_ID.HasValue);
                listVHisSereServ9Expression.Add(o => (!o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1) && o.SERVICE_REQ_ID.HasValue && o.TDL_PATIENT_ID.HasValue && o.TDL_TREATMENT_ID.HasValue);
                listVHisSereServ11Expression.Add(o => (!o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1) && o.SERVICE_REQ_ID.HasValue && o.TDL_PATIENT_ID.HasValue && o.TDL_TREATMENT_ID.HasValue);
                listVHisSereServ12Expression.Add(o => (!o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1) && o.SERVICE_REQ_ID.HasValue && o.TDL_PATIENT_ID.HasValue && o.TDL_TREATMENT_ID.HasValue);
                listVHisSereServ13Expression.Add(o => (!o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1) && o.SERVICE_REQ_ID.HasValue && o.TDL_PATIENT_ID.HasValue && o.TDL_TREATMENT_ID.HasValue);
                listVHisSereServ15Expression.Add(o => (!o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1) && o.SERVICE_REQ_ID.HasValue && o.TDL_PATIENT_ID.HasValue && o.TDL_TREATMENT_ID.HasValue);
                listVHisSereServ16Expression.Add(o => (!o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1) && o.SERVICE_REQ_ID.HasValue && o.TDL_PATIENT_ID.HasValue && o.TDL_TREATMENT_ID.HasValue);
                listVHisSereServ17Expression.Add(o => (!o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1) && o.SERVICE_REQ_ID.HasValue && o.TDL_PATIENT_ID.HasValue && o.TDL_TREATMENT_ID.HasValue);
            }
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_SERE_SERV, bool>>> listHisSereServExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SERE_SERV, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV, bool>>> listVHisSereServExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_1, bool>>> listVHisSereServ1Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_1, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_2, bool>>> listVHisSereServ2Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_2, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_3, bool>>> listVHisSereServ3Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_3, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_4, bool>>> listVHisSereServ4Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_4, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_5, bool>>> listVHisSereServ5Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_5, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_6, bool>>> listVHisSereServ6Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_6, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_7, bool>>> listVHisSereServ7Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_7, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_9, bool>>> listVHisSereServ9Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_9, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_11, bool>>> listVHisSereServ11Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_11, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_12, bool>>> listVHisSereServ12Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_12, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_13, bool>>> listVHisSereServ13Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_13, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_8, bool>>> listVHisSereServ8Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_8, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_14, bool>>> listVHisSereServ14Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_14, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_15, bool>>> listVHisSereServ15Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_15, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_16, bool>>> listVHisSereServ16Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_16, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_17, bool>>> listVHisSereServ17Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_17, bool>>>();
    }
}

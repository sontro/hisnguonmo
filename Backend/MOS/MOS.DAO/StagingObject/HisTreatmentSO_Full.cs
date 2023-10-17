using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisTreatmentSO : StagingObjectBase
    {
        public HisTreatmentSO()
        {
            //Tam thoi chua bo sung cac check "is_delete" o day, vi chua xu lý được triệt để vấn đề sinh mã bắt đầu
            //từ 1 số cố định nào đó (ko phải bắt đầu từ 1). Hiện tại đang phải xử lý vấn đề này bằng cách tạo ra các
            //hồ sơ fake (các hồ sơ được cấp mã và is_delete = 1).

            //listHisTreatmentExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
            //listVHisTreatmentExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_TREATMENT, bool>>> listHisTreatmentExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TREATMENT, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT, bool>>> listVHisTreatmentExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_FEE, bool>>> listVHisTreatmentFeeExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_FEE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_1, bool>>> listVHisTreatment1Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_1, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_2, bool>>> listVHisTreatment2Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_2, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_3, bool>>> listVHisTreatment3Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_3, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_4, bool>>> listVHisTreatment4Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_4, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_6, bool>>> listVHisTreatment6Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_6, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_7, bool>>> listVHisTreatment7Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_7, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_8, bool>>> listVHisTreatment8Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_8, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_9, bool>>> listVHisTreatment9Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_9, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_FEE_2, bool>>> listVHisTreatmentFee2Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_FEE_2, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_FEE_1, bool>>> listVHisTreatmentFee1Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_FEE_1, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<L_HIS_TREATMENT, bool>>> listLHisTreatmentExpression = new List<System.Linq.Expressions.Expression<Func<L_HIS_TREATMENT, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<L_HIS_TREATMENT_1, bool>>> listLHisTreatment1Expression = new List<System.Linq.Expressions.Expression<Func<L_HIS_TREATMENT_1, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<L_HIS_TREATMENT_2, bool>>> listLHisTreatment2Expression = new List<System.Linq.Expressions.Expression<Func<L_HIS_TREATMENT_2, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<L_HIS_TREATMENT_3, bool>>> listLHisTreatment3Expression = new List<System.Linq.Expressions.Expression<Func<L_HIS_TREATMENT_3, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_5, bool>>> listVHisTreatment5Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_5, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_10, bool>>> listVHisTreatment10Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_10, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_FEE_3, bool>>> listVHisTreatmentFee3Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_FEE_3, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_FEE_4, bool>>> listVHisTreatmentFee4Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_FEE_4, bool>>>();
		public List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_11, bool>>> listVHisTreatment11Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_11, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_12, bool>>> listVHisTreatment12Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_12, bool>>>();
    }
}

using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.CodeGenerator.HisTreatment;
using MOS.MANAGER.Config;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Update
{
    class HisTreatmentInCode
    {
        /// <summary>
        /// Xu ly de set ma vao vien (ko thuc hien update vao DB)
        /// </summary>
        /// <param name="treatment"></param>
        /// <param name="departmentId">ID cua khoa ma ho so se duoc nhap vao</param>
        internal static void  SetInCode(HIS_TREATMENT treatment, long clinicalInTime, long? departmentId, long? treatmentTypeId)
        {
            if (clinicalInTime <= 0)
            {
                throw new Exception("Thieu thong tin clinicalInTime");
            }

            //Neu cau hinh nhap so vao vien thu cong thi bo qua
            if (HisTreatmentCFG.IS_MANUAL_IN_CODE)
            {
                return;
            }

            //Lay 2 chu so cuoi cua nam
            string year = clinicalInTime.ToString().Substring(2, 2);

            if (HisTreatmentCFG.IN_CODE_FORMAT_OPTION == (int)HisTreatmentCFG.InCodeFormatOption.OPTION1)
            {
                // Lay so nha vien theo thu tu
                if (HisTreatmentCFG.IS_ALLOW_IN_CODE_GENERATE_TYPE_OPTION)
                {
                    treatment.IN_CODE = InCodeGenerator.GetInCode(year);
                }
                else
                {
                    treatment.IN_CODE = InCodeGenerator.GetNext(year);
                }
                treatment.IN_CODE_SEED_CODE = year;
            }
            else if (HisTreatmentCFG.IN_CODE_FORMAT_OPTION == (int)HisTreatmentCFG.InCodeFormatOption.OPTION2)
            {
                if (!departmentId.HasValue)
                {
                    throw new Exception("Thieu thong tin departmentId");
                }

                HIS_DEPARTMENT department = HisDepartmentCFG.DATA.Where(o => o.ID == departmentId.Value).FirstOrDefault();
                if (department == null)
                {
                    throw new Exception("Ko co thong tin department");
                }

                if (!treatmentTypeId.HasValue)
                {
                    throw new Exception("Thieu thong tin treatmentTypeId");
                }

                HIS_TREATMENT_TYPE treatmentType = HisTreatmentTypeCFG.DATA.Where(o => o.ID == treatmentTypeId.Value).FirstOrDefault();
                if (department == null)
                {
                    throw new Exception("Ko co thong tin treatmentType");
                }

                string seedCode = string.Format("{0}/{1}/{2}", department.DEPARTMENT_CODE, treatmentType.TREATMENT_TYPE_CODE, year);

                // Lay so nha vien theo thu tu
                if (HisTreatmentCFG.IS_ALLOW_IN_CODE_GENERATE_TYPE_OPTION)
                {
                    treatment.IN_CODE = InCodeGenerator.GetInCode(seedCode);
                }
                else
                {
                    treatment.IN_CODE = InCodeGenerator.GetNext(seedCode);
                }
				treatment.IN_CODE_SEED_CODE = seedCode;
            }
        }

        internal static void FinishDB(HIS_TREATMENT treatment)
        {
            if (treatment != null && !string.IsNullOrWhiteSpace(treatment.IN_CODE))
            {
                InCodeGenerator.FinishUpdateDB(treatment.IN_CODE);
            }
        }
    }
}

using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.CodeGenerator.HisTreatment;
using MOS.SDO;
using System;
using System.Linq;

namespace MOS.MANAGER.HisTreatment
{
    class HisTreatmentSetEndcode : BusinessBase
    {
        private HisTreatmentUpdate hisTreatmentUpdate;
        
        internal HisTreatmentSetEndcode()
            : base()
        {
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal HisTreatmentSetEndcode(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal bool Run(HisTreatmentSetEndCodeSDO data, ref HIS_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                HIS_TREATMENT treatment = null;
                WorkPlaceSDO workplace = null;
                bool valid = true;
                valid = valid && checker.VerifyId(data.TreatmentId, ref treatment);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workplace);
                valid = valid && this.IsAllow(workplace, treatment);

                if (valid)
                {
                    Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                    HIS_TREATMENT before = Mapper.Map<HIS_TREATMENT>(treatment);

                    if (EndCodeGenerator.SetEndCode(treatment, param)
                        && this.hisTreatmentUpdate.Update(treatment, before))
                    {
                        result = true;
                        resultData = treatment;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private bool IsAllow(WorkPlaceSDO workplace, HIS_TREATMENT treatment)
        {
            try
            {
                if (!treatment.OUT_TIME.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_ChuaCoThoiGianRaVien, treatment.TREATMENT_CODE);
                    return false;
                }

                HIS_DEPARTMENT_TRAN lastDt = new HisDepartmentTranGet().GetLastByTreatmentId(treatment.ID);

                HIS_DEPARTMENT department = HisDepartmentCFG.DATA.Where(o => o.ID == lastDt.DEPARTMENT_ID).FirstOrDefault();

                if (!lastDt.DEPARTMENT_IN_TIME.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_BenhNhanDangChoTiepNhanVaoKhoa, department.DEPARTMENT_NAME);
                    return false;
                }

                if (workplace == null || lastDt.DEPARTMENT_ID != workplace.DepartmentId)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_BenhNhanDangThuocKhoa, department.DEPARTMENT_NAME);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }
        
        internal void RollBack()
        {
            this.hisTreatmentUpdate.RollbackData();
        }
    }
}

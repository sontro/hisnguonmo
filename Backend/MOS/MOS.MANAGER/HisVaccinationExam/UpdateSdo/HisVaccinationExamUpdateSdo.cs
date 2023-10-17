using AutoMapper;
using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccinationExam
{
    class HisVaccinationExamUpdateSdo : BusinessBase
    {
        private HIS_VACCINATION_EXAM beforeUpdate;

        internal HisVaccinationExamUpdateSdo()
            : base()
        {
            this.Init();
        }

        internal HisVaccinationExamUpdateSdo(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
        }

        internal bool Run(HisVaccinationExamSDO data, ref HIS_VACCINATION_EXAM resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccinationExamCheck checker = new HisVaccinationExamCheck(param);
                HIS_VACCINATION_EXAM raw = null;
                WorkPlaceSDO workPlace = null;
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.VaccinationExam.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && checker.IsValidUserAccount(raw, workPlace);
                valid = valid && checker.IsValidConclude(raw.CONCLUDE);
                valid = valid && (data.VaccinationExam.EXECUTE_ROOM_ID == raw.EXECUTE_ROOM_ID || checker.CheckConstraint(raw.ID));
                if (valid)
                {
                    Mapper.CreateMap<HIS_VACCINATION_EXAM, HIS_VACCINATION_EXAM>();
                    this.beforeUpdate = Mapper.Map<HIS_VACCINATION_EXAM>(raw);

                    if (!DAOWorker.HisVaccinationExamDAO.Update(data.VaccinationExam))
                    {
                        throw new Exception("cAp nhat kham tiem chung that bai. Ket thuc nghiep vu");
                    }

                    resultData = data.VaccinationExam;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void Rollback()
        {
            if (this.beforeUpdate != null)
            {
                if (!DAOWorker.HisVaccinationExamDAO.Update(this.beforeUpdate))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccinationExam that bai, can kiem tra lai.");
                }
            }
        }
    }
}

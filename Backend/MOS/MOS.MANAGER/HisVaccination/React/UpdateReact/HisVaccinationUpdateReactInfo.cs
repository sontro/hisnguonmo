using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccination.React
{
    class HisVaccinationUpdateReactInfo : BusinessBase
    {
        private VaccinationProcessor vaccinationProcessor;
        private VaccinationVrplProcessor vaccinationVrplProcessor;
        private VaccinationVrtyProcessor vaccinationVrtyProcessor;

        internal HisVaccinationUpdateReactInfo()
            : base()
        {
            this.Init();
        }

        internal HisVaccinationUpdateReactInfo(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.vaccinationProcessor = new VaccinationProcessor(param);
            this.vaccinationVrplProcessor = new VaccinationVrplProcessor(param);
            this.vaccinationVrtyProcessor = new VaccinationVrtyProcessor(param);
        }

        internal bool Run(HisVaccReactInfoSDO data, ref HIS_VACCINATION resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_VACCINATION raw = null;
                HisVaccinationCheck commonChecker = new HisVaccinationCheck(param);
                HisVaccinationReactCheck checker = new HisVaccinationReactCheck(param);
                valid = valid && checker.ValidData(data);
                valid = valid && commonChecker.VerifyId(data.VaccinationId, ref raw);
                valid = valid && commonChecker.IsUnLock(raw);
                if (valid)
                {
                    List<string> sqls = new List<string>();

                    if (!this.vaccinationProcessor.Run(data, raw))
                    {
                        throw new Exception("vaccinationProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.vaccinationVrplProcessor.Run(data.HisVaccinationVrpls, raw, ref sqls))
                    {
                        throw new Exception("vaccinationVrplProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.vaccinationVrtyProcessor.Run(data.HisVaccinationVrtys, raw, ref sqls))
                    {
                        throw new Exception("vaccinationVrtyProcessor. Ket thuc nghiep vu");
                    }

                    if (IsNotNullOrEmpty(sqls))
                    {
                        if (!DAOWorker.SqlDAO.Execute(sqls))
                        {
                            throw new Exception("Sql. Ket thuc nghiep vu");
                        }
                    }

                    result = true;
                    resultData = raw;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.Rollback();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void Rollback()
        {
            try
            {
                this.vaccinationVrtyProcessor.RollbackData();
                this.vaccinationVrplProcessor.RollbackData();
                this.vaccinationProcessor.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}

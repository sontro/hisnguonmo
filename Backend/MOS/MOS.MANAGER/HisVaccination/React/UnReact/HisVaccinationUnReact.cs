using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisVaccinationVrpl;
using MOS.MANAGER.HisVaccinationVrty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccination.React.UnReact
{
    class HisVaccinationUnReact : BusinessBase
    {
        private HisVaccinationUpdate hisVaccinationUpdate;

        internal HisVaccinationUnReact()
            : base()
        {
            this.hisVaccinationUpdate = new HisVaccinationUpdate(param);
        }

        internal HisVaccinationUnReact(CommonParam param)
            : base(param)
        {
            this.hisVaccinationUpdate = new HisVaccinationUpdate(param);
        }

        internal bool Run(HIS_VACCINATION data, ref HIS_VACCINATION resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_VACCINATION raw = null;
                HisVaccinationCheck vaccChecker = new HisVaccinationCheck(param);
                HisVaccinationReactCheck checker = new HisVaccinationReactCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && vaccChecker.VerifyId(data.ID, ref raw);
                valid = valid && vaccChecker.IsUnLock(raw);
                valid = valid && checker.HasReactInfo(raw);
                valid = valid && checker.IsFollowerOrAdmin(raw);
                valid = valid && new HisVaccinationVrplTruncate(param).TruncateByVaccinationId(raw.ID);
                valid = valid && new HisVaccinationVrtyTruncate(param).TruncateByVaccinationId(raw.ID);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    this.ProcessHisVaccinationVrpl(raw, ref sqls);
                    this.ProcessHisVaccinationVrty(raw, ref sqls);
                    Mapper.CreateMap<HIS_VACCINATION, HIS_VACCINATION>();
                    HIS_VACCINATION before = Mapper.Map<HIS_VACCINATION>(raw);
                    raw.VACCINATION_REACT_ID = null;
                    raw.VACC_HEALTH_STT_ID = null;
                    raw.REACT_TIME = null;
                    raw.REACT_RESPONSER = null;
                    raw.REACT_REPORTER = null;
                    raw.PATHOLOGICAL_HISTORY = null;
                    raw.IS_REACT_RESPONSE = null;
                    raw.FOLLOW_USERNAME = null;
                    raw.FOLLOW_LOGINNAME = null;
                    raw.DEATH_TIME = null;
                    raw.VACCINATION_STT_ID = IMSys.DbConfig.HIS_RS.HIS_VACCINATION_STT.ID__PROCESSING;
                    if (!this.hisVaccinationUpdate.Update(raw, before))
                    {
                        throw new Exception("hisVaccinationUpdate");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Sql: " + sqls.ToString());
                    }

                    result = true;
                    resultData = raw;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                result = false;
            }
            return result;
        }

        private void ProcessHisVaccinationVrpl(HIS_VACCINATION data, ref List<string> sqls)
        {
            List<HIS_VACCINATION_VRPL> listData = new HisVaccinationVrplGet().GetByVaccinationId(data.ID);
            if (IsNotNullOrEmpty(listData))
            {
                HisVaccinationVrplCheck vccVrplchecker = new HisVaccinationVrplCheck(param);
                if (!vccVrplchecker.IsUnLock(listData))
                {
                    throw new Exception("HIS_VACCINATION_VRPL is lock");
                }
                sqls.Add(String.Format("DELETE HIS_VACCINATION_VRPL WHERE VACCINATION_ID = {0}", data.ID));
            }
        }

        private void ProcessHisVaccinationVrty(HIS_VACCINATION data, ref List<string> sqls)
        {
            List<HIS_VACCINATION_VRTY> listData = new HisVaccinationVrtyGet().GetByVaccinationId(data.ID);
            if (IsNotNullOrEmpty(listData))
            {
                HisVaccinationVrtyCheck vccVrtychecker = new HisVaccinationVrtyCheck(param);
                if (!vccVrtychecker.IsUnLock(listData))
                {
                    throw new Exception("HIS_VACCINATION_VRTY is lock");
                }
                sqls.Add(String.Format("DELETE HIS_VACCINATION_VRTY WHERE VACCINATION_ID = {0}", data.ID));
            }
        }

        private void Rollback()
        {
            try
            {
                this.hisVaccinationUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}

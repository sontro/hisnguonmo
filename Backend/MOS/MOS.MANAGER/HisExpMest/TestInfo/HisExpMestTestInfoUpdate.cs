using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpBltyService;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMestBlood;
using MOS.MANAGER.HisPatient;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.TestInfo
{
    class HisExpMestTestInfoUpdate : BusinessBase
    {
        private HisExpMestBloodUpdate hisExpMestBloodUpdate;
        private HisPatientUpdate hisPatientUpdate;

        internal HisExpMestTestInfoUpdate()
            : base()
        {
            this.Init();
        }

        internal HisExpMestTestInfoUpdate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestBloodUpdate = new HisExpMestBloodUpdate(param);
            this.hisPatientUpdate = new HisPatientUpdate(param);
        }

        internal bool Run(HisExpMestTestInfoSDO data, ref HisExpMestResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_EXP_MEST expMest = null;
                List<HIS_EXP_MEST_BLOOD> listRaw = new List<HIS_EXP_MEST_BLOOD>();
                HisExpMestTestInfoCheck checker = new HisExpMestTestInfoCheck(param);
                HisExpMestCheck expMestChecker = new HisExpMestCheck(param);
                HisExpMestBloodCheck bloodChecker = new HisExpMestBloodCheck(param);
                valid = valid && checker.ValidData(data);
                valid = valid && expMestChecker.VerifyId(data.ExpMestId, ref expMest);
                valid = valid && checker.CheckWorkPlace(data.RequestRoomId, expMest);
                valid = valid && bloodChecker.VerifyIds(data.ExpMestBloods.Select(s => s.ExpMestBloodId).ToList(), listRaw);
                valid = valid && bloodChecker.IsUnLock(listRaw);
                valid = valid && checker.CheckExpMest(expMest, listRaw);
                if (valid)
                {
                    this.ProcessExpMestBlood(data, listRaw);
                    this.ProcessPatient(expMest, listRaw);
                    this.PassResult(expMest, ref resultData);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.RollBack();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessExpMestBlood(HisExpMestTestInfoSDO data, List<HIS_EXP_MEST_BLOOD> listRaw)
        {
            Mapper.CreateMap<List<HIS_EXP_MEST_BLOOD>, List<HIS_EXP_MEST_BLOOD>>();
            List<HIS_EXP_MEST_BLOOD> listBefore = Mapper.Map<List<HIS_EXP_MEST_BLOOD>>(listRaw);
            foreach (var sdo in data.ExpMestBloods)
            {
                HIS_EXP_MEST_BLOOD raw = listRaw.FirstOrDefault(o => o.ID == sdo.ExpMestBloodId);
                raw.PATIENT_BLOOD_ABO_CODE = sdo.PatientBloodAboCode;
                raw.PATIENT_BLOOD_RH_CODE = sdo.PatientBloodRhCode;
                raw.PUC = sdo.Puc;
                raw.SCANGEL_GELCARD = sdo.ScangelGelcard;
                raw.COOMBS = sdo.Coombs;

                raw.TEST_TUBE = sdo.TestTube;
                raw.SALT_ENVI = sdo.SaltEnvironment;
                raw.ANTI_GLOBULIN_ENVI = sdo.AntiGlobulinEnvironment;
                raw.TEST_TUBE_TWO = sdo.TestTubeTwo;
                raw.SALT_ENVI_TWO = sdo.SaltEnvironmentTwo;
                raw.ANTI_GLOBULIN_ENVI_TWO = sdo.AntiGlobulinEnvironmentTwo;
                raw.AC_SELF_ENVIDENCE = sdo.AcSelfEnvidence;
                raw.AC_SELF_ENVIDENCE_SECOND = sdo.AcSelfEnvidenceSecond;
            }

            if (!this.hisExpMestBloodUpdate.UpdateList(listRaw, listBefore))
            {
                throw new Exception("hisExpMestBloodUpdate. ket thuc nghiep vu");
            }
        }

        private void ProcessPatient(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_BLOOD> listRaw)
        {
            HIS_EXP_MEST_BLOOD blood = listRaw.FirstOrDefault();
            if (expMest.TDL_PATIENT_ID.HasValue && (!String.IsNullOrWhiteSpace(blood.PATIENT_BLOOD_ABO_CODE) || !String.IsNullOrWhiteSpace(blood.PATIENT_BLOOD_RH_CODE)))
            {
                HIS_PATIENT patient = new HisPatientGet().GetById(expMest.TDL_PATIENT_ID.Value);
                if ((!String.IsNullOrWhiteSpace(blood.PATIENT_BLOOD_ABO_CODE) && patient.BLOOD_ABO_CODE != blood.PATIENT_BLOOD_ABO_CODE)
                    || (!String.IsNullOrWhiteSpace(blood.PATIENT_BLOOD_RH_CODE) && patient.BLOOD_RH_CODE != blood.PATIENT_BLOOD_RH_CODE))
                {
                    Mapper.CreateMap<HIS_PATIENT, HIS_PATIENT>();
                    HIS_PATIENT before = Mapper.Map<HIS_PATIENT>(patient);
                    if (!String.IsNullOrWhiteSpace(blood.PATIENT_BLOOD_ABO_CODE))
                    {
                        patient.BLOOD_ABO_CODE = blood.PATIENT_BLOOD_ABO_CODE;
                    }
                    if (!String.IsNullOrWhiteSpace(blood.PATIENT_BLOOD_RH_CODE))
                    {
                        patient.BLOOD_RH_CODE = blood.PATIENT_BLOOD_RH_CODE;
                    }
                    if (!this.hisPatientUpdate.Update(patient, before))
                    {
                        throw new Exception("hisPatientUpdate. Ket thuc nghiep vu");
                    }
                }
            }
        }

        private void PassResult(HIS_EXP_MEST expMest, ref HisExpMestResultSDO resultData)
        {
            resultData = new HisExpMestResultSDO();
            resultData.ExpMest = expMest;
            resultData.ExpBloods = new HisExpMestBloodGet().GetByExpMestId(expMest.ID);
        }

        private void RollBack()
        {
            this.hisPatientUpdate.RollbackData();
            this.hisExpMestBloodUpdate.RollbackData();
        }
    }
}

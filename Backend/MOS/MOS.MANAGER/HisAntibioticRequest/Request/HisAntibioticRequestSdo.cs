using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using MOS.MANAGER.HisDhst;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisAntibioticMicrobi;
using MOS.MANAGER.HisAntibioticOldReg;
using MOS.MANAGER.HisAntibioticNewReg;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisAntibioticRequest
{
    class HisAntibioticRequestSdo : BusinessBase
    {
        private HisDhstCreate dhstCreate;
        private HisDhstUpdate dhstUpdate;

        private HisAntibioticRequestCreate antibioticRequestCreate;
        private HisAntibioticMicrobiCreate antibioticMicrobiCreate;
        private HisAntibioticOldRegCreate antibioticOldRegCreate;
        private HisAntibioticNewRegCreate antibioticNewRegCreate;

        private HisAntibioticRequestUpdate antibioticRequestUpdate;

        private HisAntibioticMicrobiTruncate antibioticMicrobiTruncate;
        private HisAntibioticOldRegTruncate antibioticOldRegTruncate;
        private HisAntibioticNewRegTruncate antibioticNewRegTruncate;

        private List<HIS_ANTIBIOTIC_MICROBI> oldMicrobis = null;
        private List<HIS_ANTIBIOTIC_OLD_REG> oldOldRegs = null;
        private List<HIS_ANTIBIOTIC_NEW_REG> oldNewRegs = null;

        internal HisAntibioticRequestSdo()
            : base()
        {
            this.Init();
        }

        internal HisAntibioticRequestSdo(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.dhstCreate = new HisDhstCreate(param);
            this.dhstUpdate = new HisDhstUpdate(param);

            this.antibioticRequestCreate = new HisAntibioticRequestCreate(param);
            this.antibioticMicrobiCreate = new HisAntibioticMicrobiCreate(param);
            this.antibioticOldRegCreate = new HisAntibioticOldRegCreate(param);
            this.antibioticNewRegCreate = new HisAntibioticNewRegCreate(param);

            this.antibioticRequestUpdate = new HisAntibioticRequestUpdate(param);

            this.antibioticMicrobiTruncate = new HisAntibioticMicrobiTruncate(param);
            this.antibioticOldRegTruncate = new HisAntibioticOldRegTruncate(param);
            this.antibioticNewRegTruncate = new HisAntibioticNewRegTruncate(param);

            this.oldMicrobis = new List<HIS_ANTIBIOTIC_MICROBI>();
            this.oldOldRegs = new List<HIS_ANTIBIOTIC_OLD_REG>();
            this.oldNewRegs = new List<HIS_ANTIBIOTIC_NEW_REG>();
        }

        internal bool Run(HisAntibioticRequestSDO data, ref HisAntibioticRequestResultSDO resultData)
        {
            bool result = false;
            try
            {
                WorkPlaceSDO workPlace = null;
                HIS_EXP_MEST expMest = null;
                HIS_DHST dhst = null;
                HIS_ANTIBIOTIC_REQUEST antibioticRequest = null;
                string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                string username = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                HisAntibioticRequestCheck checker = new HisAntibioticRequestCheck(param);

                bool valid = true;
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && checker.IsValidRequest(data.AntibioticMicrobis, data.AntibioticRequest);
                valid = valid && checker.IsValidExpMestId(data.ExpMestId, workPlace, loginname, data.AntibioticRequest.ID, ref expMest);
                valid = valid && checker.IsValidUpdate(data.AntibioticRequest, workPlace, loginname, ref antibioticRequest);
                if (valid)
                {
                    if (data.AntibioticRequest.ID == 0)
                    {
                        this.ProcessCreateDHST(data.Temperature, data.Weight, data.Height, expMest.TDL_TREATMENT_ID.Value, data.WorkingRoomId, ref dhst);
                        this.ProcessCreateAntibioticRequest(data, workPlace, loginname, username, dhst.ID, expMest.TDL_TREATMENT_ID.Value, ref antibioticRequest);
                        if (IsNotNullOrEmpty(data.AntibioticMicrobis))
                        {
                            this.ProcessCreateAntibioticMicrobis(data.AntibioticMicrobis, antibioticRequest.ID);
                        }
                        if (IsNotNullOrEmpty(data.AntibioticOldRegs))
                        {
                            this.ProcessCreateAntibioticOldRegs(data.AntibioticOldRegs, antibioticRequest.ID);
                        }
                        if (IsNotNullOrEmpty(data.AntibioticNewRegs))
                        {
                            this.ProcessCreateAntibioticNewRegs(data.AntibioticNewRegs, antibioticRequest.ID);
                        }
                        this.ProcessExpMest(expMest, antibioticRequest.ID);



                    }
                    if (data.AntibioticRequest.ID > 0)
                    {
                        if (IsNotNull(antibioticRequest))
                        {
                            this.ProcessUpdateDHST(antibioticRequest.DHST_ID, antibioticRequest.TREATMENT_ID, data.Temperature, data.Weight, data.Height, data.WorkingRoomId, ref dhst);
                            this.ProcessUpdateAntibioticRequest(data.AntibioticRequest, antibioticRequest);
                            this.ProcessUpdateAntibioticMicrobis(data.AntibioticMicrobis, antibioticRequest.ID);
                            this.ProcessUpdateAntibioticOldRegs(data.AntibioticOldRegs, antibioticRequest.ID);
                            this.ProcessUpdateAntibioticNewRegs(data.AntibioticNewRegs, antibioticRequest.ID);
                        }
                    }

                    result = true;
                    resultData = new HisAntibioticRequestResultSDO();
                    HisAntibioticRequestViewFilterQuery filter = new HisAntibioticRequestViewFilterQuery();
                    filter.ID = antibioticRequest.ID;
                    resultData.AntibioticRequest = new HisAntibioticRequestGet().GetView(filter).FirstOrDefault();
                    resultData.AntibioticMicrobis = data.AntibioticMicrobis;
                    resultData.AntibioticOldRegs = data.AntibioticOldRegs;
                    HisAntibioticNewRegViewFilterQuery newRegFilter = new HisAntibioticNewRegViewFilterQuery();
                    newRegFilter.IDs = data.AntibioticNewRegs.Select(s => s.ID).ToList();
                    resultData.AntibioticNewRegs = new HisAntibioticNewRegGet().GetView(newRegFilter);
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

        private void ProcessUpdateAntibioticRequest(HIS_ANTIBIOTIC_REQUEST newData, HIS_ANTIBIOTIC_REQUEST oldData)
        {
            if (IsNotNull(newData) && IsNotNull(oldData))
            {
                newData.DHST_ID = oldData.DHST_ID;
                newData.REQUEST_DEPARTMENT_ID = oldData.REQUEST_DEPARTMENT_ID;
                newData.REQUEST_TIME = oldData.REQUEST_TIME;
                newData.REQUEST_LOGINNAME = oldData.REQUEST_LOGINNAME;
                newData.REQUEST_USERNAME = oldData.REQUEST_USERNAME;
                newData.ANTIBIOTIC_REQUEST_STT = Constant.IS_TRUE;
                newData.TREATMENT_ID = oldData.TREATMENT_ID;
                if (!this.antibioticRequestUpdate.Update(newData))
                {
                    throw new Exception("Cap nhat HIS_ANTIBIOTIC_REQUEST that bai .Rollback du lieu");
                }
            }
        }

        private void ProcessUpdateAntibioticNewRegs(List<HIS_ANTIBIOTIC_NEW_REG> antibioticNewRegs, long antibioticRequestId)
        {
            HisAntibioticNewRegFilterQuery filter = new HisAntibioticNewRegFilterQuery();
            filter.ANTIBIOTIC_REQUEST_ID = antibioticRequestId;
            var oldAntibioticNewReg = new HisAntibioticNewRegGet().Get(filter);
            this.oldNewRegs.AddRange(oldAntibioticNewReg);
            if (IsNotNullOrEmpty(oldAntibioticNewReg))
            {
                if (!this.antibioticNewRegTruncate.TruncateList(oldAntibioticNewReg))
                {
                    throw new Exception("Xoa du lieu cu HIS_ANTIBIOTIC_NEW_REG that bai .Rollback du lieu");
                }
            }
            if (IsNotNullOrEmpty(antibioticNewRegs))
            {
                antibioticNewRegs.ForEach(o => o.ANTIBIOTIC_REQUEST_ID = antibioticRequestId);
                if (!this.antibioticNewRegCreate.CreateList(antibioticNewRegs))
                {
                    throw new Exception("Cap nhat du lieu HIS_ANTIBIOTIC_NEW_REG that bai .Rollback du lieu");
                }
            }
        }

        private void ProcessUpdateAntibioticOldRegs(List<HIS_ANTIBIOTIC_OLD_REG> antibioticOldRegs, long antibioticRequestId)
        {
            HisAntibioticOldRegFilterQuery filter = new HisAntibioticOldRegFilterQuery();
            filter.ANTIBIOTIC_REQUEST_ID = antibioticRequestId;
            var oldAntibioticOldReg = new HisAntibioticOldRegGet().Get(filter);
            this.oldOldRegs.AddRange(oldAntibioticOldReg);
            if (IsNotNullOrEmpty(oldAntibioticOldReg))
            {
                if (!this.antibioticOldRegTruncate.TruncateList(oldAntibioticOldReg))
                {
                    throw new Exception("Xoa du lieu cu HIS_ANTIBIOTIC_OLD_REG that bai .Rollback du lieu");
                }
            }
            if (IsNotNullOrEmpty(antibioticOldRegs))
            {
                antibioticOldRegs.ForEach(o => o.ANTIBIOTIC_REQUEST_ID = antibioticRequestId);
                if (!this.antibioticOldRegCreate.CreateList(antibioticOldRegs))
                {
                    throw new Exception("Cap nhat du lieu HIS_ANTIBIOTIC_OLD_REG that bai .Rollback du lieu");
                }
            }
        }

        private void ProcessUpdateAntibioticMicrobis(List<HIS_ANTIBIOTIC_MICROBI> antibioticMicrobis, long antibioticRequestId)
        {
            HisAntibioticMicrobiFilterQuery filter = new HisAntibioticMicrobiFilterQuery();
            filter.ANTIBIOTIC_REQUEST_ID = antibioticRequestId;
            var oldAntibioticMicrobis = new HisAntibioticMicrobiGet().Get(filter);
            this.oldMicrobis.AddRange(oldAntibioticMicrobis);
            if (IsNotNullOrEmpty(oldAntibioticMicrobis))
            {
                if (!this.antibioticMicrobiTruncate.TruncateList(oldAntibioticMicrobis))
                {
                    throw new Exception("Xoa du lieu cu HIS_ANTIBIOTIC_MICROBI that bai .Rollback du lieu");
                }
            }
            if (IsNotNullOrEmpty(antibioticMicrobis))
            {
                antibioticMicrobis.ForEach(o => o.ANTIBIOTIC_REQUEST_ID = antibioticRequestId);
                if (!this.antibioticMicrobiCreate.CreateList(antibioticMicrobis))
                {
                    throw new Exception("Cap nhat du lieu HIS_ANTIBIOTIC_MICROBI that bai .Rollback du lieu");
                }
            }
        }

        private void ProcessUpdateDHST(long dhstId, long treatmentId, decimal? temperature, decimal? weight, decimal? height, long workingRoomId, ref HIS_DHST dhst)
        {
            dhst = new HisDhstGet().GetById(dhstId);
            if (IsNotNull(dhst))
            {
                dhst.TEMPERATURE = temperature;
                dhst.WEIGHT = weight;
                dhst.HEIGHT = height;
                if (!this.dhstUpdate.Update(dhst))
                {
                    throw new Exception("Cap nhat His Dhst that bai .Rollback du lieu");
                }
            }
            else
            {
                this.ProcessCreateDHST(temperature, weight, height, treatmentId, workingRoomId, ref dhst);
            }
        }

        private void ProcessCreateDHST(decimal? temperature, decimal? weight, decimal? height, long treatmentId, long workingRoomId, ref HIS_DHST dhst)
        {
            dhst = new HIS_DHST();
            dhst.TEMPERATURE = temperature;
            dhst.WEIGHT = weight;
            dhst.HEIGHT = height;
            dhst.TREATMENT_ID = treatmentId;
            dhst.EXECUTE_ROOM_ID = workingRoomId;
            if (!this.dhstCreate.Create(dhst))
            {
                throw new Exception("Tao moi His Dhst that bai .Rollback du lieu");
            }
        }

        private void ProcessCreateAntibioticRequest(HisAntibioticRequestSDO data, WorkPlaceSDO workPlace, string loginname, string username, long dhstId, long treatmentId, ref HIS_ANTIBIOTIC_REQUEST antibioticRequest)
        {
            Mapper.CreateMap<HIS_ANTIBIOTIC_REQUEST, HIS_ANTIBIOTIC_REQUEST>();
            antibioticRequest = Mapper.Map<HIS_ANTIBIOTIC_REQUEST>(data.AntibioticRequest);
            antibioticRequest.DHST_ID = dhstId;
            antibioticRequest.REQUEST_DEPARTMENT_ID = workPlace.DepartmentId;
            antibioticRequest.REQUEST_TIME = Inventec.Common.DateTime.Get.Now().Value;
            antibioticRequest.REQUEST_LOGINNAME = loginname;
            antibioticRequest.REQUEST_USERNAME = username;
            antibioticRequest.ANTIBIOTIC_REQUEST_STT = Constant.IS_TRUE;
            antibioticRequest.TREATMENT_ID = treatmentId;
            if (!this.antibioticRequestCreate.Create(antibioticRequest))
            {
                throw new Exception("Tao moi yeu cau su dung khang sinh that bai .Rollback du lieu");
            }
        }

        private void ProcessExpMest(HIS_EXP_MEST expMest, long antibioticRequestId)
        {
            expMest.ANTIBIOTIC_REQUEST_ID = antibioticRequestId;
            if (!DAOWorker.HisExpMestDAO.Update(expMest))
            {
                throw new Exception("Cap nhat HIS_EXP_MEST that bai .Rollback du lieu");
            }
        }

        private void ProcessCreateAntibioticNewRegs(List<HIS_ANTIBIOTIC_NEW_REG> antibioticNewRegs, long antibioticRequestId)
        {
            antibioticNewRegs.ForEach(o => o.ANTIBIOTIC_REQUEST_ID = antibioticRequestId);
            if (!this.antibioticNewRegCreate.CreateList(antibioticNewRegs))
            {
                throw new Exception("Tao moi HIS_ANTIBIOTIC_NEW_REG that bai .Rollback du lieu");
            }
        }

        private void ProcessCreateAntibioticOldRegs(List<HIS_ANTIBIOTIC_OLD_REG> antibioticOldRegs, long antibioticRequestId)
        {
            antibioticOldRegs.ForEach(o => o.ANTIBIOTIC_REQUEST_ID = antibioticRequestId);
            if (!this.antibioticOldRegCreate.CreateList(antibioticOldRegs))
            {
                throw new Exception("Tao moi HIS_ANTIBIOTIC_OLD_REG that bai .Rollback du lieu");
            }
        }

        private void ProcessCreateAntibioticMicrobis(List<HIS_ANTIBIOTIC_MICROBI> antibioticMicrobis, long antibioticRequestId)
        {
            antibioticMicrobis.ForEach(o => o.ANTIBIOTIC_REQUEST_ID = antibioticRequestId);
            if (!this.antibioticMicrobiCreate.CreateList(antibioticMicrobis))
            {
                throw new Exception("Tao moi HIS_ANTIBIOTIC_MICROBI that bai .Rollback du lieu");
            }
        }

        internal void Rollback()
        {
            this.dhstCreate.RollbackData();
            this.antibioticRequestCreate.RollbackData();
            this.antibioticRequestUpdate.RollbackData();
            if (IsNotNullOrEmpty(this.oldMicrobis))
            {
                if (!DAOWorker.HisAntibioticMicrobiDAO.CreateList(this.oldMicrobis))
                {
                    LogSystem.Warn("Rollback du lieu HisAntibioticMicrobi that bai, can kiem tra lai.");
                }
                this.oldMicrobis = null;
            }
            if (IsNotNullOrEmpty(this.oldNewRegs))
            {
                if (!DAOWorker.HisAntibioticNewRegDAO.CreateList(this.oldNewRegs))
                {
                    LogSystem.Warn("Rollback du lieu HisAntibioticNewReg that bai, can kiem tra lai.");
                }
                this.oldNewRegs = null;
            }
            if (IsNotNullOrEmpty(this.oldOldRegs))
            {
                if (!DAOWorker.HisAntibioticOldRegDAO.CreateList(this.oldOldRegs))
                {
                    LogSystem.Warn("Rollback du lieu HisAntibioticOldRegDAO that bai, can kiem tra lai.");
                }
                this.oldOldRegs = null;
            }
            this.antibioticNewRegCreate.RollbackData();
            this.antibioticOldRegCreate.RollbackData();
            this.antibioticMicrobiCreate.RollbackData();
        }
    }
}

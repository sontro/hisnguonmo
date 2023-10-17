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

namespace MOS.MANAGER.HisServiceReq.UpdateSampleInfo
{
    class HisServiceReqUpdateSampleInfo : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;

        internal HisServiceReqUpdateSampleInfo()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqUpdateSampleInfo(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal bool Run(ServiceReqSampleInfoSDO data, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                WorkPlaceSDO workPlace = null;
                HIS_SERVICE_REQ serviceReq = null;

                HisServiceReqCheck commomChecker = new HisServiceReqCheck(param);
                HisServiceReqUpdateSampleInfoCheck checker = new HisServiceReqUpdateSampleInfoCheck(param);


                bool valid = true;
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && this.HasWorkPlaceInfo(data.ReqRoomId, ref workPlace);
                valid = valid && commomChecker.VerifyId(data.ServiceReqId, ref serviceReq);
                valid = valid && commomChecker.IsTypeXN(serviceReq);
                valid = valid && this.IsWorkingAtDepartment(serviceReq.REQUEST_DEPARTMENT_ID, workPlace.DepartmentId);
                valid = valid && checker.IsValidForCancel(serviceReq, data.IsCancel);
                if (valid)
                {
                    // Huy lay mau
                    if (data.IsCancel)
                    {
                        serviceReq.SAMPLE_TIME = null;
                        serviceReq.START_TIME = null;
                        serviceReq.SAMPLER_LOGINNAME = null;
                        serviceReq.SAMPLER_USERNAME = null;
                        serviceReq.TEST_SAMPLE_TYPE_ID = null;
                        if (!this.hisServiceReqUpdate.Update(serviceReq, false))
                        {
                            throw new Exception("Cap nhat thong tin y lenh huy lay mau that bai. Rollback du lieu");
                        }
                    }
                    else
                    {
                        serviceReq.SAMPLE_TIME = data.SampleTime;
                        //serviceReq.START_TIME = data.SampleTime;
                        serviceReq.SAMPLER_LOGINNAME = data.SamplerLoginname;
                        serviceReq.SAMPLER_USERNAME = data.SamplerUsername;
                        serviceReq.TEST_SAMPLE_TYPE_ID = data.TestSampleTypeId;
                        if (!this.hisServiceReqUpdate.Update(serviceReq, false))
                        {
                            throw new Exception("Cap nhat thong tin y lenh that bai. Rollback du lieu");
                        }
                    }
                    result = true;
                    resultData = serviceReq;
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
            this.hisServiceReqUpdate.RollbackData();
        }
    }
}

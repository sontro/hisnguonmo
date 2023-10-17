using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisServiceReq.Exam;
using MOS.MANAGER.HisTreatment;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Common.Update.Paan
{
    class HisServiceReqUpdatePaanSample : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;

        internal HisServiceReqUpdatePaanSample()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqUpdatePaanSample(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal bool Take(long id, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                HIS_SERVICE_REQ serviceReq = null;
                HIS_SERVICE_REQ oldServiceReq = null;
                bool valid = true;
                valid = valid && checker.VerifyId(id, ref serviceReq);
                valid = valid && CheckDataTakeSample(serviceReq);
                if (valid)
                {
                    result = true;

                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    oldServiceReq = Mapper.Map<HIS_SERVICE_REQ>(serviceReq);

                    serviceReq.IS_SAMPLED = Constant.IS_TRUE;
                    serviceReq.SAMPLE_TIME = Inventec.Common.DateTime.Get.Now();
                    serviceReq.SAMPLER_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    serviceReq.SAMPLER_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();

                    if (!this.hisServiceReqUpdate.Update(serviceReq, oldServiceReq, true))
                    {
                        throw new Exception("Cap nhat HIS_SERVICE_REQ that bai.");
                    }

                    resultData = serviceReq;

                    new EventLogGenerator(EventLog.Enum.HisServiceReq_LayMauGiaiPhauBenhLy, oldServiceReq.SAMPLE_TIME, oldServiceReq.SAMPLER_LOGINNAME, serviceReq.SAMPLE_TIME, serviceReq.SAMPLER_LOGINNAME)
                        .TreatmentCode(serviceReq.TDL_TREATMENT_CODE)
                        .ServiceReqCode(serviceReq.SERVICE_REQ_CODE)
                        .Run();
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

        internal bool Cancel(long id, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                HIS_SERVICE_REQ serviceReq = null;
                HIS_SERVICE_REQ oldServiceReq = null;
                bool valid = true;
                valid = valid && checker.VerifyId(id, ref serviceReq);
                valid = valid && CheckDataCancelSample(serviceReq);
                if (valid)
                {
                    result = true;

                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    oldServiceReq = Mapper.Map<HIS_SERVICE_REQ>(serviceReq);

                    serviceReq.IS_SAMPLED = null;
                    serviceReq.SAMPLE_TIME = null;
                    serviceReq.SAMPLER_LOGINNAME = null;
                    serviceReq.SAMPLER_USERNAME = null;

                    if (!this.hisServiceReqUpdate.Update(serviceReq, oldServiceReq, true))
                    {
                        throw new Exception("Cap nhat HIS_SERVICE_REQ that bai.");
                    }

                    resultData = serviceReq;

                    new EventLogGenerator(EventLog.Enum.HisServiceReq_LayMauGiaiPhauBenhLy, oldServiceReq.SAMPLE_TIME, oldServiceReq.SAMPLER_LOGINNAME, serviceReq.SAMPLE_TIME, serviceReq.SAMPLER_LOGINNAME)
                        .TreatmentCode(serviceReq.TDL_TREATMENT_CODE)
                        .ServiceReqCode(serviceReq.SERVICE_REQ_CODE)
                        .Run();
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

        private bool CheckDataCancelSample(HIS_SERVICE_REQ serviceReq)
        {
            bool result = true;
            try
            {
                if (serviceReq.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__GPBL)
                {
                    MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_YLenhKhongPhaiLaGiaiPhauBenhLy);
                    throw new Exception("y lenh khong phai loai giai phau benh");
                }

                if (serviceReq.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                {
                    MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_PhieuChiDinhDaThucHien, serviceReq.SERVICE_REQ_CODE);
                    throw new Exception("y lenh da xu ly");
                }

                if (serviceReq.IS_SAMPLED != Constant.IS_TRUE)
                {
                    MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_YLenhChuaLayMau);
                    throw new Exception("y lenh chua lay mau");
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private bool CheckDataTakeSample(HIS_SERVICE_REQ serviceReq)
        {
            bool result = true;
            try
            {
                if (serviceReq.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__GPBL)
                {
                    MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_YLenhKhongPhaiLaGiaiPhauBenhLy);
                    throw new Exception("y lenh khong phai loai giai phau benh");
                }

                if (serviceReq.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                {
                    MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_PhieuChiDinhDaThucHien, serviceReq.SERVICE_REQ_CODE);
                    throw new Exception("y lenh da xu ly");
                }

                if (serviceReq.IS_SAMPLED == Constant.IS_TRUE)
                {
                    MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_YLenhDaLayMau);
                    throw new Exception("y lenh da lay mau");
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private void Rollback()
        {
            try
            {
                this.hisServiceReqUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}

using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Common.Update.Paan
{
    class HisServiceReqUpdatePaanBlock : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;

        internal HisServiceReqUpdatePaanBlock()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqUpdatePaanBlock(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal bool Run(PaanBlockSDO data, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                HIS_SERVICE_REQ serviceReq = null;
                HIS_SERVICE_REQ oldServiceReq = null;
                bool valid = true;
                valid = valid && checker.VerifyId(data.ServiceReqId, ref serviceReq);
                valid = valid && CheckData(serviceReq);
                valid = valid && checker.IsAllowedForStart(serviceReq);
                if (valid)
                {
                    result = true;

                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    oldServiceReq = Mapper.Map<HIS_SERVICE_REQ>(serviceReq);

                    serviceReq.BLOCK = data.Block;
                    if (serviceReq.IS_SAMPLED != Constant.IS_TRUE)
                    {
                        serviceReq.IS_SAMPLED = Constant.IS_TRUE;
                        serviceReq.SAMPLE_TIME = Inventec.Common.DateTime.Get.Now();
                        serviceReq.SAMPLER_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        serviceReq.SAMPLER_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    }

                    if (!this.hisServiceReqUpdate.Update(serviceReq, oldServiceReq, true))
                    {
                        throw new Exception("Cap nhat HIS_SERVICE_REQ that bai.");
                    }

                    resultData = serviceReq;

                    new EventLogGenerator(EventLog.Enum.HisServiceReq_CapNhatBlock, oldServiceReq.BLOCK, oldServiceReq.IS_SAMPLED, serviceReq.BLOCK, serviceReq.IS_SAMPLED)
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

        private bool CheckData(HIS_SERVICE_REQ serviceReq)
        {
            bool result = true;
            try
            {
                if (serviceReq.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__GPBL)
                {
                    MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_YLenhKhongPhaiLaGiaiPhauBenhLy, serviceReq.SERVICE_REQ_CODE);
                    throw new Exception("y lenh khong phai loai giai phau benh");
                }

                if (serviceReq.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                {
                    MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_PhieuChiDinhDaThucHien, serviceReq.SERVICE_REQ_CODE);
                    throw new Exception("y lenh da xu ly");
                }

                if (serviceReq.IS_SAMPLED == Constant.IS_TRUE)
                {
                    MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_YLenhDaLayMau, serviceReq.SERVICE_REQ_CODE);
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

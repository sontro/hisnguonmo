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
    class HisServiceReqUpdateListPaanBlock : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;

        internal HisServiceReqUpdateListPaanBlock()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqUpdateListPaanBlock(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal bool Run(List<PaanBlockSDO> datas, ref List<HIS_SERVICE_REQ> resultData)
        {
            bool result = false;
            try
            {
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                List<HIS_SERVICE_REQ> serviceReqs = new List<HIS_SERVICE_REQ>();
                List<HIS_SERVICE_REQ> oldServiceReqs = null;
                List<long> serviceReqIds = datas.Select(s => s.ServiceReqId).ToList();
                bool valid = true;
                valid = valid && checker.VerifyIds(serviceReqIds, serviceReqs);
                valid = valid && CheckData(serviceReqs);

                foreach (var req in serviceReqs)
                {
                    valid = valid && checker.VerifyRequireField(req);
                    valid = valid && checker.IsUnLock(req);
                    if (req.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                    {
                        valid = valid && checker.IsAllowedForStart(req);
                    }
                }

                if (valid)
                {
                    result = true;

                    Mapper.CreateMap<List<HIS_SERVICE_REQ>, List<HIS_SERVICE_REQ>>();
                    oldServiceReqs = Mapper.Map<List<HIS_SERVICE_REQ>>(serviceReqs);

                    foreach (var serviceReq in serviceReqs)
                    {
                        PaanBlockSDO data = datas.First(o => o.ServiceReqId == serviceReq.ID);
                        if (IsNotNull(data))
                        {
                            serviceReq.BLOCK = data.Block;
                            if (serviceReq.IS_SAMPLED != Constant.IS_TRUE)
                            {
                                serviceReq.IS_SAMPLED = Constant.IS_TRUE;
                                serviceReq.SAMPLE_TIME = Inventec.Common.DateTime.Get.Now();
                                serviceReq.SAMPLER_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                                serviceReq.SAMPLER_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                            }
                        }
                    }

                    if (!this.hisServiceReqUpdate.UpdateList(serviceReqs, oldServiceReqs))
                    {
                        throw new Exception("Cap nhat HIS_SERVICE_REQ that bai.");
                    }

                    resultData = serviceReqs;

                    foreach (var serviceReq in serviceReqs)
                    {
                        HIS_SERVICE_REQ oldServiceReq = oldServiceReqs.FirstOrDefault(o => o.ID == serviceReq.ID);
                        if (IsNotNull(oldServiceReq))
                        {
                            new EventLogGenerator(EventLog.Enum.HisServiceReq_CapNhatBlock, oldServiceReq.BLOCK, oldServiceReq.IS_SAMPLED, serviceReq.BLOCK, serviceReq.IS_SAMPLED)
                                .TreatmentCode(serviceReq.TDL_TREATMENT_CODE)
                                .ServiceReqCode(serviceReq.SERVICE_REQ_CODE)
                                .Run();
                        }
                    }
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

        private bool CheckData(List<HIS_SERVICE_REQ> serviceReqs)
        {
            bool result = true;
            try
            {
                Dictionary<MOS.LibraryMessage.Message.Enum, List<string>> dicError = new Dictionary<MOS.LibraryMessage.Message.Enum, List<string>>();

                foreach (var serviceReq in serviceReqs)
                {
                    if (serviceReq.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__GPBL)
                    {
                        if (!dicError.ContainsKey(MOS.LibraryMessage.Message.Enum.HisServiceReq_YLenhKhongPhaiLaGiaiPhauBenhLy))
                        {
                            dicError[MOS.LibraryMessage.Message.Enum.HisServiceReq_YLenhKhongPhaiLaGiaiPhauBenhLy] = new List<string>();
                        }

                        dicError[MOS.LibraryMessage.Message.Enum.HisServiceReq_YLenhKhongPhaiLaGiaiPhauBenhLy].Add(serviceReq.SERVICE_REQ_CODE);
                    }
                    if (serviceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                    {
                        if (!dicError.ContainsKey(MOS.LibraryMessage.Message.Enum.HisServiceReq_PhieuChiDinhDaThucHien))
                        {
                            dicError[MOS.LibraryMessage.Message.Enum.HisServiceReq_PhieuChiDinhDaThucHien] = new List<string>();
                        }

                        dicError[MOS.LibraryMessage.Message.Enum.HisServiceReq_PhieuChiDinhDaThucHien].Add(serviceReq.SERVICE_REQ_CODE);
                    }
                }

                if (dicError.Count > 0)
                {
                    foreach (var item in dicError)
                    {
                        string mess = string.Join(",", item.Value);
                        MessageUtil.SetMessage(param, item.Key, mess);
                    }

                    throw new Exception("Loi du lieu dau vao");
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

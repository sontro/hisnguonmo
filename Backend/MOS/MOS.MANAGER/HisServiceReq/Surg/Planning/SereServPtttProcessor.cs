using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServPttt;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Surg.Planning
{
    class SereServPtttProcessor : BusinessBase
    {
        private HisSereServPtttCreate hisSereServPtttCreate;
        private HisSereServPtttUpdate hisSereServPtttUpdate;

        internal SereServPtttProcessor()
            : base()
        {
            this.Init();
        }

        internal SereServPtttProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServPtttCreate = new HisSereServPtttCreate(param);
            this.hisSereServPtttUpdate = new HisSereServPtttUpdate(param);
        }

        internal bool Run(HIS_SERVICE_REQ serviceReq, HisServiceReqPlanSDO data)
        {
            bool result = false;
            try
            {

                List<HIS_SERE_SERV> lstSereServ = new HisSereServGet().GetByServiceReqId(serviceReq.ID);
                List<HIS_SERE_SERV_PTTT> lstSereServPttt = null;

                if (!IsNotNullOrEmpty(lstSereServ))
                {
                    return true;
                }

                lstSereServPttt = new HisSereServPtttGet().GetBySereServIds(lstSereServ.Select(s => s.ID).ToList());

                lstSereServPttt = IsNotNullOrEmpty(lstSereServPttt) ? lstSereServPttt.OrderByDescending(o => o.ID).ToList() : null;
                if (data.EmotionlessMethodId.HasValue || data.PtttMethodId.HasValue || !string.IsNullOrEmpty(data.Manner) || IsNotNullOrEmpty(lstSereServPttt))
                {
                    List<HIS_SERE_SERV_PTTT> creates = new List<HIS_SERE_SERV_PTTT>();
                    List<HIS_SERE_SERV_PTTT> updates = new List<HIS_SERE_SERV_PTTT>();
                    List<HIS_SERE_SERV_PTTT> befores = new List<HIS_SERE_SERV_PTTT>();
                    Mapper.CreateMap<HIS_SERE_SERV_PTTT, HIS_SERE_SERV_PTTT>();

                    foreach (HIS_SERE_SERV item in lstSereServ)
                    {
                        HIS_SERE_SERV_PTTT pttt = lstSereServPttt != null ? lstSereServPttt.FirstOrDefault(o => o.SERE_SERV_ID == item.ID) : null;
                        if (pttt != null)
                        {
                            HIS_SERE_SERV_PTTT before = Mapper.Map<HIS_SERE_SERV_PTTT>(pttt);
                            pttt.PTTT_METHOD_ID = data.PtttMethodId;
                            pttt.EMOTIONLESS_METHOD_ID = data.EmotionlessMethodId;
                            pttt.MANNER = data.Manner;
                            updates.Add(pttt);
                            befores.Add(before);
                        }
                        else
                        {
                            pttt = new HIS_SERE_SERV_PTTT();
                            pttt.EMOTIONLESS_METHOD_ID = data.EmotionlessMethodId;
                            pttt.MANNER = data.Manner;
                            pttt.PTTT_METHOD_ID = data.PtttMethodId;
                            pttt.SERE_SERV_ID = item.ID;
                            pttt.TDL_TREATMENT_ID = serviceReq.TREATMENT_ID;
                            creates.Add(pttt);
                        }
                    }

                    if (IsNotNullOrEmpty(creates) && !this.hisSereServPtttCreate.CreateList(creates))
                    {
                        throw new Exception("hisSereServPtttCreate");
                    }

                    if (IsNotNullOrEmpty(updates) && !this.hisSereServPtttUpdate.UpdateList(updates, befores))
                    {
                        throw new Exception("hisSereServPtttUpdate");
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        internal void Rollback()
        {
            this.hisSereServPtttUpdate.RollbackData();
            this.hisSereServPtttCreate.RollbackData();
        }
    }
}

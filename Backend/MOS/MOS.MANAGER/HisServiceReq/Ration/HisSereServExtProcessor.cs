using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Ration
{
    public class HisSereServExtProcessor : BusinessBase
    {
        private HisSereServExtCreate hisSereServExtCreate;

        internal HisSereServExtProcessor()
            : base()
        {
            this.hisSereServExtCreate = new HisSereServExtCreate(param);
        }

        internal HisSereServExtProcessor(CommonParam param)
            : base(param)
        {
            this.hisSereServExtCreate = new HisSereServExtCreate(param);
        }

        internal bool Run(HIS_TREATMENT treatment, List<HIS_SERVICE_REQ> serviceReqs, List<HIS_SERE_SERV> sereServs, List<RationRequest> rationRequests, ref List<HIS_SERE_SERV_EXT> sereServExts)
        {
            try
            {
                List<HIS_SERE_SERV_EXT> toInserts = this.MakeData(treatment, serviceReqs, sereServs, rationRequests);
                if (!IsNotNullOrEmpty(toInserts))
                {
                    return true;
                }
                if (this.hisSereServExtCreate.CreateList(toInserts))
                {
                    sereServExts = toInserts;
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        /// <summary>
        /// Tao du lieu sere_serv
        /// Luu y co nghiep vu xu ly cac truong TDL, va bo sung thong tin gia
        /// </summary>
        /// <param name="treatment"></param>
        /// <param name="serviceReqs"></param>
        /// <param name="rationRequests"></param>
        /// <returns></returns>
        private List<HIS_SERE_SERV_EXT> MakeData(HIS_TREATMENT treatment, List<HIS_SERVICE_REQ> serviceReqs, List<HIS_SERE_SERV> sereServs, List<RationRequest> rationRequests)
        {
            List<HIS_SERE_SERV_EXT> result = new List<HIS_SERE_SERV_EXT>();
            foreach (RationRequest rr in rationRequests)
            {
                //Chi xu ly HIS_SERE_SERV_EXT khi co InstructionNote
                if (!string.IsNullOrWhiteSpace(rr.InstructionNote))
                {
                    HIS_SERVICE_REQ serviceReq = serviceReqs.Where(o => o.RATION_TIME_ID == rr.RationTimeId && o.INTRUCTION_TIME == rr.IntructionTime && o.EXECUTE_ROOM_ID == rr.RoomId).FirstOrDefault();
                    if (serviceReq == null)
                    {
                        throw new Exception("serviceReq null. Loi he thong");
                    }

                    HIS_SERE_SERV sereServ = sereServs.Where(o => o.SERVICE_REQ_ID == serviceReq.ID && o.SERVICE_ID == rr.ServiceId && o.PATIENT_TYPE_ID == rr.PatientTypeId).FirstOrDefault();
                    if (sereServ == null)
                    {
                        throw new Exception("sereServ null. Loi he thong");
                    }

                    HIS_SERE_SERV_EXT toInsert = new HIS_SERE_SERV_EXT();
                    toInsert.INSTRUCTION_NOTE = rr.InstructionNote;
                    toInsert.SERE_SERV_ID = sereServ.ID;
                    HisSereServExtUtil.SetTdl(toInsert, sereServ);

                    result.Add(toInsert);
                }
            }
            return result;
        }

        internal void Rollback()
        {
            this.hisSereServExtCreate.RollbackData();
        }
    }
}

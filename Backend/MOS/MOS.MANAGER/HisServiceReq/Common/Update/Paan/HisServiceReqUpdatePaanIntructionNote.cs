using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisSereServ;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Common.Update.Paan
{
    class HisServiceReqUpdatePaanIntructionNote : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;

        internal HisServiceReqUpdatePaanIntructionNote()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqUpdatePaanIntructionNote(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal bool Run(PaanIntructionNoteSDO data, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                HIS_SERVICE_REQ serviceReq = null;
                HIS_SERVICE_REQ oldServiceReq = null;
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && checker.VerifyId(data.ServiceReqId, ref serviceReq);
                valid = valid && CheckData(serviceReq);
                if (valid)
                {
                    result = true;

                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    oldServiceReq = Mapper.Map<HIS_SERVICE_REQ>(serviceReq);

                    serviceReq.TDL_INSTRUCTION_NOTE = data.IntructionNote;

                    if (!this.hisServiceReqUpdate.Update(serviceReq, oldServiceReq, true))
                    {
                        throw new Exception("Cap nhat HIS_SERVICE_REQ that bai.");
                    }

                    ProcessSereServExt(data);

                    resultData = serviceReq;

                    new EventLogGenerator(EventLog.Enum.HisServiceReq_CapNhatIntructionNote, oldServiceReq.TDL_INSTRUCTION_NOTE, serviceReq.TDL_INSTRUCTION_NOTE)
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

        private void ProcessSereServExt(PaanIntructionNoteSDO data)
        {
            List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByServiceReqId(data.ServiceReqId);
            if (IsNotNullOrEmpty(sereServs))
            {
                string sql = string.Format("UPDATE HIS_SERE_SERV_EXT SET INSTRUCTION_NOTE = '{0}' WHERE %IN_CLAUSE% ", data.IntructionNote);

                string sqls = DAOWorker.SqlDAO.AddInClause(sereServs.Select(s => s.ID).ToList(), sql, "SERE_SERV_ID");
                if (!DAOWorker.SqlDAO.Execute(sqls))
                {
                    throw new Exception("Update HIS_SERE_SERV_EXT that bai. " + string.Join(",", sereServs.Select(s => s.ID).ToList()));
                }
            }
            else
            {
                throw new Exception("y lenh khong co thong tin chi tiet");
            }
        }

        private bool CheckData(HIS_SERVICE_REQ serviceReq)
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

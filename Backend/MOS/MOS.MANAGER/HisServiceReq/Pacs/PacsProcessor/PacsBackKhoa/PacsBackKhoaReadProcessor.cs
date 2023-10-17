using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisServiceReq.Update.Finish;
using MOS.MANAGER.HisServiceReq.Update.Status;
using MOS.PACS.Fhir;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Pacs.PacsProcessor.PacsBackKhoa
{
    class PacsBackKhoaReadProcessor : BusinessBase, IPacsReadProcessor
    {
        private HisSereServExtUpdate hisSereServExtUpdate;
        private HisSereServExtCreate hisSereServExtCreate;
        private HisServiceReqUpdateFinish hisServiceReqUpdateFinish;
        private HisServiceReqUpdateStart hisServiceReqUpdateStart;
        private FhirProcessor fhirProcessor;

        internal PacsBackKhoaReadProcessor()
            : base()
        {
        }

        internal PacsBackKhoaReadProcessor(CommonParam param)
            : base(param)
        {

        }

        bool IPacsReadProcessor.ReadResult()
        {
            bool result = false;
            try
            {
                if (!String.IsNullOrWhiteSpace(PacsCFG.FHIR_CONNECT_INFO))
                {
                    List<string> cfgs = PacsCFG.FHIR_CONNECT_INFO.Split('|').ToList();
                    if (cfgs == null || cfgs.Count < 3)
                    {
                        throw new ArgumentNullException("Cau hinh FHIR OPTION thieu thong tin");
                    }

                    string uri = cfgs[0];
                    string loginname = cfgs[1];
                    string password = cfgs[2];

                    if (String.IsNullOrWhiteSpace(uri) || String.IsNullOrWhiteSpace(loginname) || String.IsNullOrWhiteSpace(password))
                        throw new ArgumentNullException("Cau hinh FHIR OPTION thieu thong tin");

                    fhirProcessor = new FhirProcessor(uri, loginname, password, Config.HisEmployeeCFG.DATA, Config.HisServiceCFG.DATA_VIEW);

                    List<HIS_SERE_SERV> sereServs = null;
                    List<HIS_SERVICE_REQ> serviceReqs = null;
                    GetData(ref sereServs, ref serviceReqs);

                    if (IsNotNullOrEmpty(sereServs))
                    {
                        foreach (var sereserv in sereServs)
                        {
                            if (sereserv.IS_DELETE == Constant.IS_TRUE)
                                continue;

                            HIS_SERVICE_REQ serviceReq = serviceReqs.FirstOrDefault(o => o.ID == sereserv.SERVICE_REQ_ID);
                            if (!IsNotNull(serviceReq) || serviceReq.PACS_STT_ID == PacsCFG.PACS_STT_ID_RESULT)
                                continue;

                            MOS.PACS.Fhir.PacsReceivedData dataResult = fhirProcessor.GetResult(sereserv.ID);
                            if (dataResult != null)
                            {
                                UpdateReult(dataResult, sereserv, serviceReq);
                            }
                            else
                            {
                                Inventec.Common.Logging.LogSystem.Warn("Khong co thong tin ket qua cua dv " + sereserv.ID);
                                Inventec.Common.Logging.LogAction.Warn("Khong co thong tin ket qua cua dv " + sereserv.ID);
                            }
                        }
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                LogAction.Error(ex);
                result = false;
            }
            return result;
        }

        private void UpdateReult(MOS.PACS.Fhir.PacsReceivedData messageReceived, HIS_SERE_SERV sereserv, HIS_SERVICE_REQ serviceReq)
        {
            try
            {
                if (messageReceived != null && messageReceived.SereServId > 0)
                {
                    HIS_SERE_SERV sereServ = null;
                    bool valid = true;
                    valid = valid && new HisSereServCheck().VerifyId(messageReceived.SereServId, ref sereServ);
                    if (valid)
                    {
                        //chưa bắt đầu thì bắt đầu
                        ProcessStartServiceReq(messageReceived, serviceReq);

                        HIS_SERE_SERV_EXT sereServExt = new HisSereServExt.HisSereServExtGet().GetBySereServId(sereServ.ID);
                        if (sereServExt != null)
                        {
                            sereServExt.BEGIN_TIME = messageReceived.BeginTime;
                            sereServExt.CONCLUDE = Inventec.Common.String.CountVi.SubStringVi(messageReceived.Conclude, 3000);
                            sereServExt.DESCRIPTION = Inventec.Common.String.CountVi.SubStringVi(messageReceived.Description, 3000);
                            sereServExt.NOTE = Inventec.Common.String.CountVi.SubStringVi(messageReceived.Note, 3000);
                            sereServExt.END_TIME = messageReceived.EndTime;
                            sereServExt.TDL_SERVICE_REQ_ID = sereServ.SERVICE_REQ_ID;
                            sereServExt.TDL_TREATMENT_ID = sereServ.TDL_TREATMENT_ID;
                            sereServExt.SUBCLINICAL_RESULT_USERNAME = messageReceived.SubclinicalResultUsername;
                            sereServExt.SUBCLINICAL_RESULT_LOGINNAME = messageReceived.SubclinicalResultLoginname;
                            sereServExt.NUMBER_OF_FILM = messageReceived.NumberOfFilm;
                            if (!String.IsNullOrWhiteSpace(messageReceived.studyID))
                            {
                                sereServExt.JSON_PRINT_ID = string.Format("studyID:{0}", messageReceived.studyID);
                            }

                            if (!String.IsNullOrWhiteSpace(messageReceived.Machine))
                            {
                                HIS_MACHINE machine = HisMachineCFG.DATA.FirstOrDefault(o => o.MACHINE_NAME.ToLower().Contains(messageReceived.Machine.ToLower()) || o.MACHINE_CODE.ToLower().Contains(messageReceived.Machine.ToLower()));
                                if (machine != null)
                                {
                                    sereServExt.MACHINE_ID = machine.ID;
                                    sereServExt.MACHINE_CODE = machine.MACHINE_CODE;
                                }
                            }

                            hisSereServExtUpdate = new HisSereServExtUpdate();
                            hisSereServExtUpdate.Update(sereServExt, false);
                        }
                        else
                        {
                            sereServExt = new HIS_SERE_SERV_EXT();
                            sereServExt.SERE_SERV_ID = sereServ.ID;
                            sereServExt.BEGIN_TIME = messageReceived.BeginTime;
                            sereServExt.CONCLUDE = Inventec.Common.String.CountVi.SubStringVi(messageReceived.Conclude, 4000);
                            sereServExt.DESCRIPTION = Inventec.Common.String.CountVi.SubStringVi(messageReceived.Description, 4000);
                            sereServExt.NOTE = Inventec.Common.String.CountVi.SubStringVi(messageReceived.Note, 4000);
                            sereServExt.END_TIME = messageReceived.EndTime;
                            sereServExt.TDL_SERVICE_REQ_ID = sereServ.SERVICE_REQ_ID;
                            sereServExt.TDL_TREATMENT_ID = sereServ.TDL_TREATMENT_ID;
                            sereServExt.SUBCLINICAL_RESULT_USERNAME = messageReceived.SubclinicalResultUsername;
                            sereServExt.SUBCLINICAL_RESULT_LOGINNAME = messageReceived.SubclinicalResultLoginname;
                            sereServExt.NUMBER_OF_FILM = messageReceived.NumberOfFilm;
                            if (!String.IsNullOrWhiteSpace(messageReceived.studyID))
                            {
                                sereServExt.JSON_PRINT_ID = string.Format("studyID:{0}", messageReceived.studyID);
                            }

                            if (!String.IsNullOrWhiteSpace(messageReceived.Machine))
                            {
                                HIS_MACHINE machine = HisMachineCFG.DATA.FirstOrDefault(o => o.MACHINE_NAME.ToLower().Contains(messageReceived.Machine.ToLower()) || o.MACHINE_CODE.ToLower().Contains(messageReceived.Machine.ToLower()));
                                if (machine != null)
                                {
                                    sereServExt.MACHINE_ID = machine.ID;
                                    sereServExt.MACHINE_CODE = machine.MACHINE_CODE;
                                }
                            }

                            HisSereServExtUtil.SetTdl(sereServExt, sereServ);

                            hisSereServExtCreate = new HisSereServExtCreate();
                            hisSereServExtCreate.Create(sereServExt);
                        }

                        ProcessFinishServiceReq(messageReceived, serviceReq);
                    }
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                Inventec.Common.Logging.LogSystem.Error(ex);
                LogAction.Error(ex);
            }
        }

        private string CreateAuthenString(string loginname, string password)
        {
            if (string.IsNullOrWhiteSpace(loginname) || string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("loginname, password khong duoc de trong");
            }

            string authenString = string.Format("{0}:{1}", loginname, password);

            return Convert.ToBase64String(Encoding.Default.GetBytes(authenString));
        }

        private void GetData(ref List<HIS_SERE_SERV> sereServs, ref List<HIS_SERVICE_REQ> serviceReqs)
        {
            List<long> executeRoomIds = null;

            if (PacsCFG.CONNECTION_TYPE == PacsConnectionType.FILE)
            {
                executeRoomIds = HisRoomCFG.DATA.Where(o => PacsCFG.PACS_FILE_ADDRESSES.Any(a => a.RoomCode == o.ROOM_CODE && !String.IsNullOrWhiteSpace(a.SaveFolder))).Select(s => s.ID).ToList();
            }
            else
            {
                executeRoomIds = HisRoomCFG.DATA.Where(o => PacsCFG.PACS_ADDRESS.Any(a => a.RoomCode == o.ROOM_CODE && !String.IsNullOrWhiteSpace(a.Address))).Select(s => s.ID).ToList();
            }

            if (executeRoomIds != null && executeRoomIds.Count > 0)
            {
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.IS_NOT_SENT__OR__UPDATED = false;  //lay cac y lenh da gui sang PACS
                filter.HAS_EXECUTE = true;
                filter.EXECUTE_ROOM_IDs = executeRoomIds;
                filter.SERVICE_REQ_STT_IDs = new List<long> { IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL };
                filter.ALLOW_SEND_PACS = true;
                filter.NOT_IN_SERVICE_REQ_TYPE_IDs = new List<long>() { IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN };
                filter.CREATE_TIME_FROM = Convert.ToInt64(DateTime.Now.AddDays(-HisServiceReqCFG.INTEGRATE_SYSTEM_DAY_NUM_SYNC).ToString("yyyyMMdd") + "000000");
                serviceReqs = new HisServiceReqGet().Get(filter);

                if (IsNotNullOrEmpty(serviceReqs))
                {
                    serviceReqs = serviceReqs.Where(o => o.PACS_STT_ID != PacsCFG.PACS_STT_ID_RESULT).ToList();
                }

                List<long> serviceReqIds = serviceReqs != null ? serviceReqs.Select(o => o.ID).ToList() : null;

                if (serviceReqIds != null && serviceReqIds.Count > 0)
                {
                    HisSereServFilterQuery ssFilter = new HisSereServFilterQuery();
                    ssFilter.HAS_EXECUTE = true;
                    ssFilter.SERVICE_REQ_IDs = serviceReqIds;
                    //voi sancy thi can lay ca cac du lieu da xoa (is_delete = 1)
                    ssFilter.IS_INCLUDE_DELETED = true;
                    sereServs = new HisSereServGet().Get(ssFilter);
                }
            }
        }

        private void ProcessStartServiceReq(MOS.PACS.Fhir.PacsReceivedData data, HIS_SERVICE_REQ serviceReq)
        {
            bool valid = true;
            valid = valid && IsNotNull(data);
            valid = valid && IsNotNull(serviceReq);
            if (valid)
            {
                if ((!serviceReq.START_TIME.HasValue) || (serviceReq.START_TIME.HasValue && data.BeginTime.HasValue && serviceReq.START_TIME.Value > data.BeginTime.Value))
                {
                    serviceReq.START_TIME = serviceReq.START_TIME.HasValue ? serviceReq.START_TIME.Value : Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(DateTime.Now);
                    serviceReq.EXECUTE_LOGINNAME = data.SubclinicalResultLoginname;
                    serviceReq.EXECUTE_USERNAME = data.SubclinicalResultUsername;
                    HIS_SERVICE_REQ resultData = null;
                    if (!new HisServiceReqUpdateStart(param).StartWithTime(serviceReq, ref resultData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceReq that bai." + LogUtil.TraceData("data", serviceReq));
                    }

                }
            }
        }

        private void ProcessFinishServiceReq(MOS.PACS.Fhir.PacsReceivedData data, HIS_SERVICE_REQ serviceReq)
        {
            bool valid = true;
            valid = valid && IsNotNull(data);
            valid = valid && IsNotNull(serviceReq);
            if (valid)
            {
                bool finish = true;
                //1 y lenh co 2 dich vu chi nhan kq cua 1 dich vu
                //do khi gui chi dinh sang PACS da tao du lieu HIS_SERE_SERV_EXT luon nen cau kiem tra bi sai.
                string sql = "SELECT COUNT(*) FROM HIS_SERE_SERV SS WHERE NOT EXISTS(SELECT 1 FROM HIS_SERE_SERV_EXT WHERE SERE_SERV_ID = SS.ID AND (CONCLUDE IS NOT NULL OR DESCRIPTION IS NOT NULL OR END_TIME IS NOT NULL)) AND SERVICE_REQ_ID = :param";
                long ssCheck = DAOWorker.SqlDAO.GetSqlSingle<long>(sql, serviceReq.ID);
                if (ssCheck > 0)
                {
                    finish = false;
                }

                if (finish)
                {
                    HIS_SERVICE_REQ serviceReqResult = new HIS_SERVICE_REQ();
                    hisServiceReqUpdateFinish = new HisServiceReqUpdateFinish();
                    if (!data.FinishTime.HasValue || data.FinishTime < serviceReq.START_TIME)
                        data.FinishTime = serviceReq.START_TIME;

                    if ((!serviceReq.FINISH_TIME.HasValue) || (serviceReq.FINISH_TIME.HasValue && data.FinishTime.HasValue && serviceReq.FINISH_TIME.Value < data.FinishTime.Value))
                    {
                        serviceReq.FINISH_TIME = data.FinishTime;
                    }

                    if (!hisServiceReqUpdateFinish.FinishWithTime(serviceReq, ref serviceReqResult))
                    {
                        Inventec.Common.Logging.LogSystem.Error("ket thu dich vu that bai");
                        Inventec.Common.Logging.LogAction.Error("ket thu dich vu that bai");
                    }

                    string updateExecuteName = "UPDATE HIS_SERVICE_REQ SET PACS_STT_ID = :param_1 WHERE ID = :param_2";
                    if (!DAOWorker.SqlDAO.Execute(updateExecuteName, PacsCFG.PACS_STT_ID_RESULT, serviceReq.ID))
                    {
                        Inventec.Common.Logging.LogSystem.Error("Cap nhat thong tin nguoi xu ly that bai. " + serviceReq.SERVICE_REQ_CODE + "__" + data.SubclinicalResultUsername);
                        Inventec.Common.Logging.LogAction.Error("Cap nhat thong tin nguoi xu ly that bai. " + serviceReq.SERVICE_REQ_CODE + "__" + data.SubclinicalResultUsername);
                    }
                }
            }
        }

        private void RollbackData()
        {
            try
            {
                if (hisSereServExtUpdate != null)
                    hisSereServExtUpdate.RollbackData();
                if (hisSereServExtCreate != null)
                    hisSereServExtCreate.RollbackData();
                if (hisServiceReqUpdateFinish != null)
                    hisServiceReqUpdateFinish.Rollback();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                Inventec.Common.Logging.LogAction.Error(ex);
            }
        }
    }
}

using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq.Pacs.PacsThread;
using MOS.PACS.Fhir;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Pacs.PacsProcessor.PacsBackKhoa
{
    class PacsBackKhoaProcessor : BusinessBase, IPacsProcessor
    {
        private FhirProcessor fhirProcessor;

        internal PacsBackKhoaProcessor()
            : base()
        {
        }

        internal PacsBackKhoaProcessor(CommonParam param)
            : base(param)
        {

        }

        bool IPacsProcessor.SendOrder(PacsOrderData data, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                bool? valid = null;
                V_HIS_ROOM exeRoom = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == data.ServiceReq.EXECUTE_ROOM_ID);

                List<string> cfgs = PacsCFG.FHIR_CONNECT_INFO.Split('|').ToList();
                if (cfgs == null || cfgs.Count < 3)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_CauHinhFHIRThieuThongTin);
                    throw new ArgumentNullException("Cau hinh FHIR OPTION thieu thong tin");
                }

                string uri = cfgs[0];
                string loginname = cfgs[1];
                string password = cfgs[2];

                if (String.IsNullOrWhiteSpace(uri) || String.IsNullOrWhiteSpace(loginname) || String.IsNullOrWhiteSpace(password))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_CauHinhFHIRThieuThongTin);
                    throw new ArgumentNullException("Cau hinh FHIR OPTION thieu thong tin");
                }

                fhirProcessor = new FhirProcessor(uri, loginname, password, Config.HisEmployeeCFG.DATA, Config.HisServiceCFG.DATA_VIEW);

                List<long> successIds = new List<long>();
                if (IsNotNullOrEmpty(data.Inserts) ||
                    (data.ServiceReq.IS_SENT_EXT == Constant.IS_TRUE && data.ServiceReq.IS_UPDATED_EXT == Constant.IS_TRUE && IsNotNullOrEmpty(data.Availables)))
                {
                    List<HIS_SERE_SERV> listSereServ = null;
                    if (IsNotNullOrEmpty(data.Inserts))
                    {
                        listSereServ = data.Inserts;
                    }
                    else
                    {
                        listSereServ = data.Availables;
                    }

                    List<HIS_SERE_SERV_EXT> sereServExt = ProcessGetSsextBySsId(listSereServ);
                    foreach (HIS_SERE_SERV sereServ in listSereServ)
                    {
                        HIS_SERE_SERV_EXT ext = IsNotNullOrEmpty(sereServExt) ? sereServExt.FirstOrDefault(o => o.SERE_SERV_ID == sereServ.ID) : null;
                        string studyID = "";
                        if (fhirProcessor.SendNewOrder(data.Treatment, data.ServiceReq, sereServ, exeRoom, ext, ref studyID))
                        {
                            if (!valid.HasValue) valid = true;
                            successIds.Add(sereServ.ID);

                            if (ext != null && !String.IsNullOrWhiteSpace(studyID))
                            {
                                string sql = "UPDATE HIS_SERE_SERV_EXT SET JSON_PRINT_ID = 'studyID:{0}' WHERE ID = {1}";
                                sqls.Add(string.Format(sql, studyID, ext.ID));
                            }
                            else if (!IsNotNull(ext))
                            {
                                string sql = "INSERT INTO HIS_SERE_SERV_EXT(SERE_SERV_ID, JSON_PRINT_ID, TDL_SERVICE_REQ_ID, TDL_TREATMENT_ID) VALUES({0},'studyID:{1}',{2},{3})";
                                sqls.Add(string.Format(sql, sereServ.ID, studyID, sereServ.SERVICE_REQ_ID, sereServ.TDL_TREATMENT_ID));
                            }
                        }
                        else if (data.ServiceReq.IS_SENT_EXT == Constant.IS_TRUE && data.ServiceReq.IS_UPDATED_EXT == Constant.IS_TRUE)
                        {
                            valid = true;
                        }
                        else
                        {
                            valid = false;
                        }
                    }
                }

                if (IsNotNullOrEmpty(data.Deletes))
                {
                    List<HIS_SERE_SERV_EXT> sereServExt = ProcessGetSsextBySsId(data.Deletes);
                    foreach (HIS_SERE_SERV sereServ in data.Deletes)
                    {
                        HIS_SERE_SERV_EXT ext = IsNotNullOrEmpty(sereServExt) ? sereServExt.FirstOrDefault(o => o.SERE_SERV_ID == sereServ.ID) : null;
                        if (ext != null && !String.IsNullOrWhiteSpace(ext.JSON_PRINT_ID))
                        {
                            string bundleId = ext.JSON_PRINT_ID.Replace("studyID:", "").Trim();
                            if (fhirProcessor.SendDeleteFhir(bundleId))
                            {
                                if (!valid.HasValue) valid = true;
                                successIds.Add(sereServ.ID);
                            }
                            else
                            {
                                valid = false;
                            }
                        }
                    }
                }

                if (IsNotNullOrEmpty(successIds))
                {
                    string sql = "UPDATE HIS_SERE_SERV SET IS_SENT_EXT = 1 WHERE IS_SENT_EXT IS NULL AND %IN_CLAUSE%";
                    sql = DAOWorker.SqlDAO.AddInClause(successIds, sql, "ID");
                    sqls.Add(sql);
                }

                Inventec.Common.Logging.LogAction.Debug("IsSuccess: " + valid);
                data.IsSuccess = valid;
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                Inventec.Common.Logging.LogAction.Error(ex);
                result = false;
            }
            return result;
        }

        private List<HIS_SERE_SERV_EXT> ProcessGetSsextBySsId(List<HIS_SERE_SERV> listSereServ)
        {
            List<HIS_SERE_SERV_EXT> result = null;
            try
            {
                if (IsNotNullOrEmpty(listSereServ))
                {
                    List<long> sereServIds = listSereServ.Select(s => s.ID).Distinct().ToList();
                    string sql = "SELECT * FROM HIS_SERE_SERV_EXT WHERE %IN_CLAUSE%";
                    sql = DAOWorker.SqlDAO.AddInClause(sereServIds, sql, "SERE_SERV_ID");
                    result = DAOWorker.SqlDAO.GetSql<HIS_SERE_SERV_EXT>(sql);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                Inventec.Common.Logging.LogAction.Error(ex);
            }
            return result;
        }

        void IPacsProcessor.UpdateStatus(List<PacsOrderData> listData, List<string> sqls)
        {
            try
            {
                List<long> newIds = new List<long>();
                List<long> updateIds = new List<long>();

                foreach (PacsOrderData item in listData)
                {
                    if (!item.IsSuccess.HasValue) continue;
                    if (item.IsSuccess.Value)
                    {
                        if (!item.ServiceReq.IS_SENT_EXT.HasValue)
                        {
                            newIds.Add(item.ServiceReq.ID);
                        }
                        else if (item.ServiceReq.IS_SENT_EXT == Constant.IS_TRUE
                            && item.ServiceReq.IS_UPDATED_EXT == Constant.IS_TRUE)
                        {
                            updateIds.Add(item.ServiceReq.ID);
                        }
                    }
                }

                if (IsNotNullOrEmpty(newIds))
                {
                    string sql = "UPDATE HIS_SERVICE_REQ SET IS_SENT_EXT = 1, IS_UPDATED_EXT = NULL WHERE %IN_CLAUSE% ";
                    sql = DAOWorker.SqlDAO.AddInClause(newIds, sql, "ID");
                    sqls.Add(sql);
                }
                if (IsNotNullOrEmpty(updateIds))
                {
                    string sql = "UPDATE HIS_SERVICE_REQ SET IS_UPDATED_EXT = NULL WHERE IS_UPDATED_EXT IS NOT NULL AND %IN_CLAUSE% ";
                    sql = DAOWorker.SqlDAO.AddInClause(updateIds, sql, "ID");
                    sqls.Add(sql);
                }

                if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                {
                    LogSystem.Warn("Update Status Pacs that bai sql: " + string.Join("\r\n", sqls));
                    LogAction.Warn("Update Status Pacs that bai sql: " + string.Join("\r\n", sqls));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                Inventec.Common.Logging.LogAction.Error(ex);
            }
        }

        bool IPacsProcessor.UpdatePatientInfo(HIS_PATIENT patient, ref List<string> messages)
        {
            bool result = true;
            try
            {
                if (IsNotNull(patient))
                {
                    List<string> cfgs = PacsCFG.FHIR_CONNECT_INFO.Split('|').ToList();
                    if (cfgs == null || cfgs.Count < 3)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_CauHinhFHIRThieuThongTin);
                        throw new ArgumentNullException("Cau hinh FHIR OPTION thieu thong tin");
                    }

                    string uri = cfgs[0];
                    string loginname = cfgs[1];
                    string password = cfgs[2];

                    if (String.IsNullOrWhiteSpace(uri) || String.IsNullOrWhiteSpace(loginname) || String.IsNullOrWhiteSpace(password))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_CauHinhFHIRThieuThongTin);
                        throw new ArgumentNullException("Cau hinh FHIR OPTION thieu thong tin");
                    }

                    var fhirProcessor = new FhirProcessor(uri, loginname, password, Config.HisEmployeeCFG.DATA, Config.HisServiceCFG.DATA_VIEW);

                    HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                    filter.IS_NOT_SENT__OR__UPDATED = false; //lay cac y lenh chua gui sang PACS hoac co cap nhat
                    filter.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
                    filter.ALLOW_SEND_PACS = true;
                    filter.NOT_IN_SERVICE_REQ_TYPE_IDs = new List<long>() { IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN };
                    filter.TDL_PATIENT_ID = patient.ID;
                    List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().Get(filter);
                    if (IsNotNullOrEmpty(serviceReqs))
                    {
                        List<HIS_SERE_SERV> sereServs = new List<HIS_SERE_SERV>();
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

                        List<PacsOrderData> orderData = GetDataProcessor.Prepare(sereServs, serviceReqs);

                        foreach (var data in orderData)
                        {
                            if (IsNotNullOrEmpty(data.Availables))
                            {
                                List<HIS_SERE_SERV_EXT> sereServExt = ProcessGetSsextBySsId(data.Availables);
                                foreach (HIS_SERE_SERV sereServ in data.Availables)
                                {
                                    if (sereServ.IS_DELETE == Constant.IS_TRUE)
                                    {
                                        HIS_SERE_SERV_EXT ext = IsNotNullOrEmpty(sereServExt) ? sereServExt.FirstOrDefault(o => o.SERE_SERV_ID == sereServ.ID) : null;
                                        if (ext != null && !String.IsNullOrWhiteSpace(ext.JSON_PRINT_ID))
                                        {
                                            string bundleId = ext.JSON_PRINT_ID.Replace("studyID:", "").Trim();
                                            if (!fhirProcessor.SendDeleteFhir(bundleId))
                                            {
                                                Inventec.Common.Logging.LogSystem.Error("Gui chi dinh sang he thong PACS that bai");
                                                Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => sereServ), sereServ));
                                                Inventec.Common.Logging.LogAction.Error("Gui chi dinh sang he thong PACS that bai");
                                                Inventec.Common.Logging.LogAction.Error(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => sereServ), sereServ));
                                                return false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        V_HIS_ROOM exeRoom = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == data.ServiceReq.EXECUTE_ROOM_ID);
                                        HIS_SERE_SERV_EXT ext = IsNotNullOrEmpty(sereServExt) ? sereServExt.FirstOrDefault(o => o.SERE_SERV_ID == sereServ.ID) : null;
                                        string studyID = "";
                                        if (!fhirProcessor.SendNewOrder(data.Treatment, data.ServiceReq, sereServ, exeRoom, ext, ref studyID))
                                        {
                                            Inventec.Common.Logging.LogSystem.Error("Gui chi dinh sang he thong PACS that bai");
                                            Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => sereServ), sereServ));
                                            Inventec.Common.Logging.LogAction.Error("Gui chi dinh sang he thong PACS that bai");
                                            Inventec.Common.Logging.LogAction.Error(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => sereServ), sereServ));
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                Inventec.Common.Logging.LogAction.Error(ex);
                result = false;
            }
            return result;
        }
    }
}

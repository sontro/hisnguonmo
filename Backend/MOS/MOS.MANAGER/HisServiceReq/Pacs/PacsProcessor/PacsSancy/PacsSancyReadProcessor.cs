using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisServiceReq.Update.Finish;
using MOS.MANAGER.HisServiceReq.Update.Status;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Pacs.PacsProcessor.PacsSancy
{
    class PacsSancyReadProcessor : BusinessBase, IPacsReadProcessor
    {
        private HisSereServExtUpdate hisSereServExtUpdate;
        private HisSereServExtCreate hisSereServExtCreate;
        private HisServiceReqUpdateFinish hisServiceReqUpdateFinish;

        internal PacsSancyReadProcessor()
            : base()
        {
        }

        internal PacsSancyReadProcessor(CommonParam param)
            : base(param)
        {

        }

        bool IPacsReadProcessor.ReadResult()
        {
            try
            {
                foreach (var item in PacsCFG.PACS_FILE_ADDRESSES)
                {
                    List<FileInfo> files = FileHandler.Read(item.Ip, item.User, item.Password, item.ReadFolder);
                    if (files != null && files.Count > 0)
                    {
                        foreach (var file in files)
                        {
                            if (file != null && !String.IsNullOrEmpty(file.FileName) && file.FileName.Contains("ORU"))
                            {
                                if (!String.IsNullOrEmpty(file.Data) && UpdateResult(file.Data))
                                {
                                    FileHandler.Move(item.Ip, item.User, item.Password, file, "success");
                                }
                                else
                                {
                                    FileHandler.Move(item.Ip, item.User, item.Password, file, "fail");
                                }
                            }
                        }
                    }
                    else
                    {
                        LogSystem.Debug("Khong doc duoc file ket qua nao tu folder: " + item.ReadFolder);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return true;
        }

        internal bool UpdateResult(string data)
        {
            bool result = false;
            try
            {
                PacsReceivedData messageReceived = PacsUtil.ProcessReceived(data);

                if (messageReceived != null && messageReceived.SereServId > 0)
                {
                    HIS_SERE_SERV sereServ = null;
                    HIS_SERVICE_REQ raw = null;
                    bool valid = true;
                    valid = valid && new HisSereServCheck(param).VerifyId(messageReceived.SereServId, ref sereServ);
                    valid = valid && new HisServiceReqCheck(param).VerifyId(sereServ.SERVICE_REQ_ID ?? 0, ref raw);
                    if (valid)
                    {
                        //chưa bắt đầu thì bắt đầu
                        ProcessStartServiceReq(messageReceived, raw);

                        HIS_SERE_SERV_EXT sereServExt = new HisSereServExt.HisSereServExtGet().GetBySereServId(sereServ.ID);
                        if (sereServExt != null)
                        {
                            sereServExt.BEGIN_TIME = messageReceived.BeginTime;
                            sereServExt.CONCLUDE = Inventec.Common.String.CountVi.SubStringVi(messageReceived.Conclude, 4000);
                            sereServExt.DESCRIPTION = Inventec.Common.String.CountVi.SubStringVi(messageReceived.Description, 4000);
                            sereServExt.NOTE = Inventec.Common.String.CountVi.SubStringVi(messageReceived.Note, 4000);
                            sereServExt.END_TIME = messageReceived.EndTime;
                            sereServExt.TDL_SERVICE_REQ_ID = sereServ.SERVICE_REQ_ID;
                            sereServExt.TDL_TREATMENT_ID = sereServ.TDL_TREATMENT_ID;
                            sereServExt.JSON_PRINT_ID = string.Format("studyID:{0}", messageReceived.studyID);
                            sereServExt.SUBCLINICAL_RESULT_LOGINNAME = messageReceived.SubclinicalResultLoginname;
                            sereServExt.SUBCLINICAL_RESULT_USERNAME = messageReceived.SubclinicalResultUsername;

                            if (!messageReceived.NumberOfFilm.HasValue)
                            {
                                V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == sereServ.SERVICE_ID);
                                if (service != null)
                                {
                                    sereServExt.NUMBER_OF_FILM = service.NUMBER_OF_FILM;
                                }
                            }
                            else
                            {
                                sereServExt.NUMBER_OF_FILM = messageReceived.NumberOfFilm;
                            }

                            if (!String.IsNullOrWhiteSpace(messageReceived.Machine))
                            {
                                HIS_MACHINE machine = HisMachineCFG.DATA.FirstOrDefault(o => (!String.IsNullOrWhiteSpace(o.MACHINE_NAME) && o.MACHINE_NAME.ToLower().Contains(messageReceived.Machine.ToLower())) || o.MACHINE_CODE.ToLower().Contains(messageReceived.Machine.ToLower()));
                                if (machine != null)
                                {
                                    sereServExt.MACHINE_ID = machine.ID;
                                    sereServExt.MACHINE_CODE = machine.MACHINE_CODE;
                                }
                            }

                            hisSereServExtUpdate = new HisSereServExtUpdate();
                            result = hisSereServExtUpdate.Update(sereServExt, false);
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
                            sereServExt.JSON_PRINT_ID = string.Format("studyID:{0}", messageReceived.studyID);
                            sereServExt.SUBCLINICAL_RESULT_LOGINNAME = messageReceived.SubclinicalResultLoginname;
                            sereServExt.SUBCLINICAL_RESULT_USERNAME = messageReceived.SubclinicalResultUsername;

                            if (!messageReceived.NumberOfFilm.HasValue)
                            {
                                V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == sereServ.SERVICE_ID);
                                if (service != null)
                                {
                                    sereServExt.NUMBER_OF_FILM = service.NUMBER_OF_FILM;
                                }
                            }
                            else
                            {
                                sereServExt.NUMBER_OF_FILM = messageReceived.NumberOfFilm;
                            }

                            if (!String.IsNullOrWhiteSpace(messageReceived.Machine))
                            {
                                HIS_MACHINE machine = HisMachineCFG.DATA.FirstOrDefault(o => (!String.IsNullOrWhiteSpace(o.MACHINE_NAME) && o.MACHINE_NAME.ToLower().Contains(messageReceived.Machine.ToLower())) || o.MACHINE_CODE.ToLower().Contains(messageReceived.Machine.ToLower()));
                                if (machine != null)
                                {
                                    sereServExt.MACHINE_ID = machine.ID;
                                    sereServExt.MACHINE_CODE = machine.MACHINE_CODE;
                                }
                            }

                            HisSereServExtUtil.SetTdl(sereServExt, sereServ);

                            hisSereServExtCreate = new HisSereServExtCreate();
                            result = hisSereServExtCreate.Create(sereServExt);
                        }

                        ProcessFinishServiceReq(messageReceived, raw);
                    }
                    else
                    {
                        Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param));
                    }
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Error("Lỗi đọc bản tin HL7: \r\n" + data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                this.RollbackData();
                result = false;
            }
            return result;
        }

        private void ProcessStartServiceReq(PacsReceivedData data, HIS_SERVICE_REQ serviceReq)
        {
            bool valid = true;
            valid = valid && IsNotNull(data);
            valid = valid && IsNotNull(serviceReq);
            if (valid)
            {
                if ((!serviceReq.START_TIME.HasValue) || (data.BeginTime.HasValue && serviceReq.START_TIME.Value > data.BeginTime.Value))
                {
                    serviceReq.START_TIME = Inventec.Common.DateTime.Get.Now();
                    if (data.BeginTime.HasValue)
                    {
                        serviceReq.START_TIME = data.BeginTime.Value;
                    }

                    //thông tin SubclinicalResultLoginname có thể trả về nhiều người phân cách nhau bởi dấu chấm phẩy.
                    if (!String.IsNullOrWhiteSpace(data.SubclinicalResultLoginname))
                    {
                        string[] loginname = data.SubclinicalResultLoginname.Split(';');
                        serviceReq.EXECUTE_LOGINNAME = loginname.Last();
                    }

                    if (!String.IsNullOrWhiteSpace(data.SubclinicalResultUsername))
                    {
                        string[] username = data.SubclinicalResultUsername.Split(';');
                        serviceReq.EXECUTE_USERNAME = username.Last();
                    }

                    HIS_SERVICE_REQ resultData = null;
                    if (!new HisServiceReqUpdateStart(param).StartWithTime(serviceReq, ref resultData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceReq that bai." + LogUtil.TraceData("data", serviceReq));
                    }

                }
            }
        }

        private bool ProcessFinishServiceReq(PacsReceivedData data, HIS_SERVICE_REQ serviceReq)
        {
            bool finish = false;
            bool valid = true;
            valid = valid && IsNotNull(data);
            valid = valid && IsNotNull(serviceReq);
            if (valid)
            {
                finish = true;
                string sql = "SELECT COUNT(*) FROM HIS_SERE_SERV SS WHERE IS_DELETE = 0 AND NOT EXISTS(SELECT 1 FROM HIS_SERE_SERV_EXT WHERE SERE_SERV_ID = SS.ID AND JSON_PRINT_ID IS NOT NULL) AND SERVICE_REQ_ID = :param";
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

                    serviceReq.FINISH_TIME = data.FinishTime;

                    if (data.EndTime.HasValue && (!serviceReq.FINISH_TIME.HasValue || serviceReq.FINISH_TIME.Value < data.EndTime.Value))
                    {
                        serviceReq.FINISH_TIME = data.EndTime;
                    }

                    if (!hisServiceReqUpdateFinish.FinishWithTime(serviceReq, ref serviceReqResult))
                    {
                        finish = false;
                        Inventec.Common.Logging.LogSystem.Error("ket thu dich vu that bai");
                    }
                    return finish;
                }
            }
            return valid;
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
            }
        }
    }
}

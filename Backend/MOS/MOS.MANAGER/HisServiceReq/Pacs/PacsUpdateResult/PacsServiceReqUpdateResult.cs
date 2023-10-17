using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisServiceReq.Update.Finish;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisServiceReq.Update.Status;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Pacs.PacsUpdateResult
{
    class PacsServiceReqUpdateResult : BusinessBase
    {
        private HisServiceReqUpdateStart startServiceReq;
        private HisServiceReqUpdateUnfinish finishServiceReq;
        private HisSereServExtUpdate updateSSExt;
        private HisSereServExtCreate createSSExt;

        internal PacsServiceReqUpdateResult()
            : base()
        {
            this.Init();
        }

        internal PacsServiceReqUpdateResult(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.startServiceReq = new HisServiceReqUpdateStart(param);
            this.finishServiceReq = new HisServiceReqUpdateUnfinish(param);
            this.updateSSExt = new HisSereServExtUpdate(param);
            this.createSSExt = new HisSereServExtCreate(param);
        }

        internal bool Run(HisPacsResultTDO data)
        {
            bool result = false;

            try
            {
                HIS_TREATMENT treatment = null;
                HIS_SERVICE_REQ serviceReq = null;
                HIS_SERE_SERV sereServ = null;
                HIS_SERE_SERV_EXT ssExt = null;

                PacsServiceReqUpdateResultCheck checker = new PacsServiceReqUpdateResultCheck(param);
                HisSereServCheck commonChecker = new HisSereServCheck(param);

                bool valid = true;
                valid = valid && checker.IsValidAccessionNumber(data.AccessionNumber, ref sereServ, ref ssExt, ref serviceReq, ref treatment);
                valid = valid && commonChecker.HasExecute(sereServ);
                valid = valid && checker.IsValidCanCel(data.IsCancel, data.EndTime, treatment);
                if (valid)
                {
                    if (data.IsCancel)
                    {
                        if (serviceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                        {
                            HIS_SERVICE_REQ resultData = null;
                            if (!finishServiceReq.Run(serviceReq.ID, ref resultData))
                            {
                                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReq_CapNhatThatBai);
                                throw new Exception("Ket thuc y lenh that bai." + LogUtil.TraceData("serviceReq", serviceReq));
                            }
                        }

                        if (ssExt != null)
                        {
                            ssExt.SUBCLINICAL_PRES_LOGINNAME = null;
                            ssExt.SUBCLINICAL_PRES_USERNAME = null;
                            ssExt.BEGIN_TIME = null;
                            ssExt.END_TIME = null;
                            ssExt.CONCLUDE = null;
                            ssExt.DESCRIPTION = null;
                            ssExt.NOTE = null;
                            ssExt.SUBCLINICAL_RESULT_LOGINNAME = null;
                            ssExt.SUBCLINICAL_RESULT_USERNAME = null;
                            ssExt.MACHINE_CODE = null;
                            ssExt.MACHINE_ID = null;
                            ssExt.NUMBER_OF_FILM = null;
                            if (!this.updateSSExt.Update(ssExt, false))
                            {
                                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServExt_CapNhatThatBai);
                                throw new Exception("Cap nhat thong tin dich vu SSEXT ve null that bai." + LogUtil.TraceData("ssExt", ssExt));
                            }
                        }
                    }
                    else
                    {
                        if (serviceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                        {
                            serviceReq.START_TIME = data.BeginTime;
                            serviceReq.EXECUTE_LOGINNAME = data.ExecuteLoginname;
                            serviceReq.EXECUTE_USERNAME = data.ExecuteUsername;
                            HIS_SERVICE_REQ resultData = null;
                            if (!this.startServiceReq.StartWithTime(serviceReq, ref resultData))
                            {
                                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReq_CapNhatThatBai);
                                throw new Exception("Bat dau y lenh that bai." + LogUtil.TraceData("serviceReq", serviceReq));
                            }
                        }

                        if (ssExt != null)
                        {
                            ssExt.SUBCLINICAL_PRES_LOGINNAME = data.TechnicianLoginname;
                            ssExt.SUBCLINICAL_PRES_USERNAME = data.TechnicianUsername;
                            ssExt.BEGIN_TIME = data.BeginTime;
                            ssExt.END_TIME = data.EndTime;
                            ssExt.CONCLUDE = data.Conclude;
                            ssExt.DESCRIPTION = data.Description;
                            ssExt.NOTE = data.Note;
                            ssExt.SUBCLINICAL_RESULT_LOGINNAME = data.ExecuteLoginname;
                            ssExt.SUBCLINICAL_RESULT_USERNAME = data.ExecuteUsername;
                            var machine = HisMachineCFG.DATA.FirstOrDefault(o => o.MACHINE_CODE == data.MachineCode);
                            if (machine != null)
                            {
                                ssExt.MACHINE_CODE = data.MachineCode;
                                ssExt.MACHINE_ID = machine.ID;
                            }
                            ssExt.NUMBER_OF_FILM = data.NumberOfFilm;

                            HisSereServExtUtil.SetTdl(ssExt, sereServ);

                            if (!this.updateSSExt.Update(ssExt, false))
                            {
                                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServExt_CapNhatThatBai);
                                throw new Exception("Cap nhat thong tin dich vu SSEXT ve null that bai." + LogUtil.TraceData("ssExt", ssExt));
                            }
                        }
                        else
                        {
                            HIS_SERE_SERV_EXT newExt = new HIS_SERE_SERV_EXT();
                            newExt.SERE_SERV_ID = sereServ.ID;
                            newExt.SUBCLINICAL_PRES_LOGINNAME = data.TechnicianLoginname;
                            newExt.SUBCLINICAL_PRES_USERNAME = data.TechnicianUsername;
                            newExt.BEGIN_TIME = data.BeginTime;
                            newExt.END_TIME = data.EndTime;
                            newExt.CONCLUDE = data.Conclude;
                            newExt.DESCRIPTION = data.Description;
                            newExt.NOTE = data.Note;
                            newExt.SUBCLINICAL_RESULT_LOGINNAME = data.ExecuteLoginname;
                            newExt.SUBCLINICAL_RESULT_USERNAME = data.ExecuteUsername;
                            var machine = HisMachineCFG.DATA.FirstOrDefault(o => o.MACHINE_CODE == data.MachineCode);
                            if (machine != null)
                            {
                                newExt.MACHINE_CODE = data.MachineCode;
                                newExt.MACHINE_ID = machine.ID;
                            }
                            newExt.NUMBER_OF_FILM = data.NumberOfFilm;

                            HisSereServExtUtil.SetTdl(newExt, sereServ);

                            if (!this.createSSExt.Create(newExt))
                            {
                                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServExt_ThemMoiThatBai);
                                throw new Exception("Cap nhat thong tin dich vu SSEXT ve null that bai." + LogUtil.TraceData("newExt", newExt));
                            }
                        }

                        List<HIS_SERE_SERV> allSS = new HisSereServGet().GetByServiceReqId(serviceReq.ID);
                        List<HIS_SERE_SERV_EXT> allSSExt = IsNotNullOrEmpty(allSS) ? new HisSereServExtGet().GetBySereServIds(allSS.Select(o => o.ID).ToList()) : null;
                        if (IsNotNullOrEmpty(allSSExt) && allSSExt.All(o => o.END_TIME.HasValue))
                        {
                            string sql = "UPDATE HIS_SERVICE_REQ SET SERVICE_REQ_STT_ID = :param1 WHERE ID = :param2";
                            if (!DAOWorker.SqlDAO.Execute(sql, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT, serviceReq.ID))
                            {
                                throw new Exception("Cap nhat trang thai y lenh ve hoan thanh that bai");
                            }
                        }
                    }

                    result = true;
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
            this.createSSExt.RollbackData();
            this.updateSSExt.RollbackData();
            this.startServiceReq.RollbackData();
            this.finishServiceReq.RollbackData();
        }
    }
}

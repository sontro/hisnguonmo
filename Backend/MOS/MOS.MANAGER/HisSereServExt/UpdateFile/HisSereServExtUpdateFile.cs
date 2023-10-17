using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Fss.Client;
using Inventec.Fss.Utility;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisEkip;
using MOS.MANAGER.HisEkipUser;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServFile;
using MOS.MANAGER.HisSereServPttt;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace MOS.MANAGER.HisSereServExt.Update
{
    partial class HisSereServExtUpdateFile : BusinessBase
    {
        private HisSereServFileCreate hisSereServFileCreate;
        private HisSereServPtttCreate hisSereServPtttCreate;
        private HisSereServPtttUpdate hisSereServPtttUpdate;
        private HisServiceReqUpdate hisServiceReqUpdate;
        private HisEkipCreate hisEkipCreate;
        private HisEkipUserCreate hisEkipUserCreate;
        private HisEkipUserTruncate hisEkipUserTruncate;
        private HisEkipUserUpdate hisEkipUserUpdate;
        private HisSereServUpdate hisSereServUpdate;
        private HisSereServExtUpdate hisSereServExtUpdate;

        internal HisSereServExtUpdateFile()
            : base()
        {
        }

        internal HisSereServExtUpdateFile(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServFileCreate = new HisSereServFileCreate(param);
            this.hisSereServPtttCreate = new HisSereServPtttCreate(param);
            this.hisSereServPtttUpdate = new HisSereServPtttUpdate(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.hisEkipCreate = new HisEkipCreate(param);
            this.hisEkipUserTruncate = new HisEkipUserTruncate(param);
            this.hisEkipUserCreate = new HisEkipUserCreate(param);
            this.hisEkipUserUpdate = new HisEkipUserUpdate(param);
            this.hisSereServUpdate = new HisSereServUpdate(param);
            this.hisSereServExtUpdate = new HisSereServExtUpdate(param);
        }

        internal bool Run(HisSereServExtSDO data, ref HisSereServExtWithFileSDO resultData)
        {
            bool result = true;
            try
            {
                HIS_SERE_SERV_EXT sereServExt = null;
                HIS_SERVICE_REQ serviceReq = null;
                HIS_SERE_SERV sereServ = null;
                HisSereServExtUpdateFileCheck checker = new HisSereServExtUpdateFileCheck(param);
                HisSereServExtCheck checkerExt = new HisSereServExtCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                bool valid = checker.IsValidData(data, ref serviceReq, ref sereServ, ref sereServExt);
                valid = valid && checker.IsAllowUpdateResultSubclinical(data, serviceReq, sereServExt, sereServ);
                valid = valid && checker.IsNotDuplicateEkipUser(data.HisEkipUsers);
                valid = valid && checkerExt.IsValidMinAndMaxProcessTime(data.HisSereServExt);
                valid = valid && treatmentChecker.CheckRecordInspection(serviceReq);
                valid = valid && checkerExt.IsValidLoginName(data, sereServ);
                if (valid)
                {
                    
                    HIS_SERE_SERV_PTTT sereServPttt = null;
                    List<HIS_SERE_SERV_FILE> sereServFiles = null;
                    HIS_EKIP ekip = null;
                    List<HIS_EKIP_USER> ekipUsers = null;

                    bool hasSelectedMachine = false;
                    this.ProcessSereServExt(data, ref sereServExt, ref hasSelectedMachine);
                    this.ProcessSereServPttt(data, sereServ, ref sereServPttt);
                    this.ProcessSereServFile(sereServ.ID, data.Files, ref sereServFiles);
                    this.ProcessEkip(data, sereServ, ref ekip, ref ekipUsers);
                    this.ProcessSereServ(data, sereServ, ekip);
                    this.ProcessServiceReq(serviceReq, sereServ, hasSelectedMachine);

                    resultData = new HisSereServExtWithFileSDO();
                    resultData.EkipUsers = ekipUsers;
                    resultData.SereServExt = sereServExt;
                    resultData.SereServFiles = sereServFiles;
                    resultData.SereServPttt = sereServPttt;

                    HisSereServExtUtil.Log(serviceReq, sereServ, data.HisSereServExt);
                    if (hasSelectedMachine)
                    {
                        new EventLogGenerator(EventLog.Enum.ChonMayXuLyChoDichVu, serviceReq.MACHINE_NAMES)
                        .TreatmentCode(serviceReq.TDL_TREATMENT_CODE)
                        .ServiceReqCode(serviceReq.SERVICE_REQ_CODE)
                        .Run();
                    }
                    this.InitThreadSendResultToPacs(sereServExt);
                }
            }
            catch (Exception ex)
            {
                resultData = null;
                LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                result = false;
            }
            return result;
        }

        private void InitThreadSendResultToPacs(HIS_SERE_SERV_EXT sereServExt)
        {
            try
            {
                Thread thread = new Thread(new ParameterizedThreadStart(this.SendResultToPacs));
                thread.Priority = ThreadPriority.Lowest;
                thread.Start(sereServExt);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void SendResultToPacs(object data)
        {
            try
            {
                HIS_SERE_SERV_EXT sereServExt = (HIS_SERE_SERV_EXT)data;
                new HisSereServExtSendResultToPacs().Run(sereServExt);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ProcessServiceReq(HIS_SERVICE_REQ serviceReq, HIS_SERE_SERV sereServ, bool hasSelectedMachine)
        {
            if (sereServ != null && sereServ.SERVICE_REQ_ID.HasValue)
            {
                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                string userName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                if (serviceReq != null)
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    HIS_SERVICE_REQ before = Mapper.Map<HIS_SERVICE_REQ>(serviceReq);
                    serviceReq.EXECUTE_LOGINNAME = loginName;
                    serviceReq.EXECUTE_USERNAME = userName;
                    serviceReq.EXE_WORKING_SHIFT_ID = TokenManager.GetWorkingShift();
                    if (hasSelectedMachine)
                        ChangeMachineNames(serviceReq);
                    if (!this.hisServiceReqUpdate.Update(serviceReq, before, false))
                    {
                        throw new Exception("Cap nhat thong tin nguoi xu ly that bai");
                    }
                }
            }
        }

        private void ChangeMachineNames(HIS_SERVICE_REQ serviceReq)
        {
            var SereServExts = new HisSereServExtGet().GetByServiceReqId(serviceReq.ID);
            if (!IsNotNullOrEmpty(SereServExts)) return;
            List<long> machineIds = SereServExts.Select(o => o.MACHINE_ID ?? 0).Distinct().ToList();
            List<HIS_MACHINE> machineAlls = new HisMachine.HisMachineGet().GetByIds(machineIds);
            List<string> machineNames = new List<string>();
            bool isUpdate = false;
            foreach (var item in SereServExts)
            {
                HIS_MACHINE machine = machineAlls.FirstOrDefault(o => o.ID == item.MACHINE_ID);
                if (machine != null)
                {
                    machineNames.Add(machine.MACHINE_NAME);
                }
            }
            if (IsNotNullOrEmpty(machineNames))
            {
                serviceReq.MACHINE_NAMES = string.Join(",", machineNames);
            }
        }

        private void ProcessSereServPttt(HisSereServExtSDO data, HIS_SERE_SERV sereServ, ref HIS_SERE_SERV_PTTT sereServPttt)
        {
            try
            {
                if (data.HisSereServPttt != null)
                {
                    data.HisSereServPttt.TDL_TREATMENT_ID = sereServ.TDL_TREATMENT_ID;
                    data.HisSereServPttt.SERE_SERV_ID = sereServ.ID;

                    HIS_SERE_SERV_PTTT exist = new HisSereServPtttGet().GetBySereServId(sereServ.ID);

                    if (exist == null)
                    {
                        if (!hisSereServPtttCreate.Create(data.HisSereServPttt))
                        {
                            throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                        }
                    }
                    else
                    {
                        data.HisSereServPttt.ID = exist.ID;

                        if (!hisSereServPtttUpdate.Update(data.HisSereServPttt))
                        {
                            throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                        }
                    }
                    sereServPttt = data.HisSereServPttt;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void ProcessSereServExt(HisSereServExtSDO data, ref HIS_SERE_SERV_EXT sereServExt, ref bool hasSelectedMachine)
        {
            data.HisSereServExt.SUBCLINICAL_RESULT_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            data.HisSereServExt.SUBCLINICAL_RESULT_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
            data.HisSereServExt.SUBCLINICAL_NURSE_LOGINNAME = TokenManager.GetNurseLoginName();
            data.HisSereServExt.SUBCLINICAL_NURSE_USERNAME = TokenManager.GetNurseUserName();
            if (IsNotNull(sereServExt) && sereServExt.MACHINE_ID != data.HisSereServExt.MACHINE_ID)
            {
                hasSelectedMachine = true;
            }
            if (!this.hisSereServExtUpdate.Update(data.HisSereServExt, true))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu.");
            }
            sereServExt = data.HisSereServExt;
        }

        private void ProcessEkip(HisSereServExtSDO data, HIS_SERE_SERV sereServ, ref HIS_EKIP ekip, ref List<HIS_EKIP_USER> ekipUsers)
        {
            if (sereServ != null)
            {
                //Neu sere_serv chua co ekip_id va client co gui len thong tin ekip thi thi tao moi ekip
                if (!sereServ.EKIP_ID.HasValue && data.HisEkipUsers != null && data.HisEkipUsers.Count > 0)
                {
                    //Neu sere_serv ko co ekip_id ma thong tin ekip_user lai co ekip_id ==> du lieu ko hop le
                    if (data.HisEkipUsers.Where(o => o.EKIP_ID > 0).Any())
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("ekip_users co thong tin ekip_id trong khi sere_serv chua co ekip_id." + LogUtil.TraceData("data", data));
                    }

                    HIS_EKIP ek = new HIS_EKIP();
                    ek.HIS_EKIP_USER = data.HisEkipUsers;
                    if (!this.hisEkipCreate.Create(ek))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                    ekip = ek;
                    ekipUsers = new List<HIS_EKIP_USER>();
                    ekipUsers.AddRange(ek.HIS_EKIP_USER);
                }
                else if (sereServ.EKIP_ID.HasValue)
                {
                    //Neu ton tai ekip_user co ekip_id khac voi ekip_id trong sere_serv ==> du lieu ko hop le
                    if (data.HisEkipUsers != null && data.HisEkipUsers.Where(o => o.EKIP_ID != sereServ.EKIP_ID.Value).Any())
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("ton tai ekip_user co ekip_id khac voi ekip_id trong sere_serv_ext" + LogUtil.TraceData("data", data.HisSereServExt) + LogUtil.TraceData("SereServ", sereServ));
                    }

                    List<HIS_EKIP_USER> exists = new HisEkipUserGet().GetByEkipId(sereServ.EKIP_ID.Value);

                    //Danh sach can xoa la danh sach co trong exists nhung ko co trong danh sach client gui len
                    List<HIS_EKIP_USER> toTruncates = exists == null ? null : exists.Where(o => data.HisEkipUsers == null || !data.HisEkipUsers.Exists(t => t.ID == o.ID)).ToList();
                    //Danh sach can them moi la danh sach ko co ID
                    List<HIS_EKIP_USER> toInserts = data.HisEkipUsers == null ? null : data.HisEkipUsers.Where(o => o.ID <= 0).ToList();
                    //Danh sach can cap nhat la danh sach co ID
                    List<HIS_EKIP_USER> toUpdates = data.HisEkipUsers == null ? null : data.HisEkipUsers.Where(o => o.ID > 0).ToList();

                    if (IsNotNullOrEmpty(toTruncates))
                    {
                        if (!this.hisEkipUserTruncate.TruncateList(toTruncates))
                        {
                            throw new Exception("Ket thuc nghiep vu");
                        }
                    }

                    ekipUsers = new List<HIS_EKIP_USER>();
                    if (IsNotNullOrEmpty(toInserts))
                    {
                        if (!this.hisEkipUserCreate.CreateList(toInserts))
                        {
                            throw new Exception("Ket thuc nghiep vu");
                        }
                        ekipUsers.AddRange(toInserts);
                    }

                    if (IsNotNullOrEmpty(toUpdates))
                    {
                        if (!this.hisEkipUserUpdate.UpdateList(toUpdates))
                        {
                            throw new Exception("Ket thuc nghiep vu");
                        }
                        ekipUsers.AddRange(toUpdates);
                    }
                }
            }
        }

        private void ProcessSereServ(HisSereServExtSDO data, HIS_SERE_SERV sereServ, HIS_EKIP ekip)
        {
            if (sereServ != null)
            {
                //Neu co tao moi thong tin kip thi cap nhat ekip_id cho sere_serv
                if (ekip != null)
                {
                    sereServ.EKIP_ID = ekip.ID;
                }

                //Thuc hien cap nhat cac thong tin cho sere_Serv
                if (!this.hisSereServUpdate.UpdateResult(sereServ, ref sereServ))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessSereServFile(long sereServId, List<FileSDO> files, ref List<HIS_SERE_SERV_FILE> hisSereServFiles)
        {
            try
            {
                if (IsNotNullOrEmpty(files) && files.Exists(t => t.Content != null))
                {
                    List<FileHolder> fileHolders = new List<FileHolder>();
                    foreach (FileSDO file in files)
                    {
                        FileHolder f = new FileHolder();
                        f.Content = new MemoryStream(file.Content);
                        f.FileName = file.FileName;
                        fileHolders.Add(f);
                    }

                    //upload file sang he thong thong FSS
                    List<FileUploadInfo> fileUploadInfos = FileUpload.UploadFile(Constant.APPLICATION_CODE, FileStoreLocation.SERE_SERV, fileHolders);
                    List<HIS_SERE_SERV_FILE> ssFiles = new List<HIS_SERE_SERV_FILE>();
                    if (fileUploadInfos != null && fileUploadInfos.Count > 0)
                    {
                        foreach (FileUploadInfo fui in fileUploadInfos)
                        {
                            HIS_SERE_SERV_FILE f = new HIS_SERE_SERV_FILE();
                            f.SERE_SERV_FILE_NAME = fui.OriginalName;
                            f.URL = fui.Url;
                            f.SERE_SERV_ID = sereServId;

                            FileSDO file = files.FirstOrDefault(o => o.FileName == fui.OriginalName);
                            if (file != null)
                            {
                                f.BODY_PART_ID = file.BodyPartId;
                                f.CAPTION = file.Caption;
                            }
                            ssFiles.Add(f);
                        }
                        if (!this.hisSereServFileCreate.CreateList(ssFiles))
                        {
                            throw new Exception("Rollback du lieu. Ket thuc nghiep vu.");
                        }
                        hisSereServFiles = ssFiles;
                    }
                }
            }
            catch (FileUploadException ex)
            {
                LogSystem.Error("Upload file co loi xay ra. StatusCode: " + ex.StatusCode.ToString() + ". " + ex.Message);
                param.HasException = true;
                throw ex;
            }
        }

        private void Rollback()
        {
            this.hisSereServFileCreate.RollbackData();
            this.hisSereServPtttCreate.RollbackData();
            this.hisSereServPtttUpdate.RollbackData();
            this.hisSereServExtUpdate.RollbackData();
        }
    }
}

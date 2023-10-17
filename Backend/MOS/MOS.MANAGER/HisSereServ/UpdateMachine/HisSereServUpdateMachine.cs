using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisMachine;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisSereServTein;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ.UpdateMachine
{
    internal class HisSereServUpdateMachine : BusinessBase
    {
        private HisSereServExtUpdate hisSSExtUpdate { get; set; }
        private HisSereServTeinUpdate hisSSTeinUpdate { get; set; }
        private HisServiceReqUpdate hisServiceReqUpdate { get; set; }
        private HisSereServExtCreate hisSereServExtCreate { get; set; }
        internal HisSereServUpdateMachine()
            : base()
        {
            this.Init();
        }

        internal HisSereServUpdateMachine(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSSExtUpdate = new HisSereServExtUpdate(param);
            this.hisSSTeinUpdate = new HisSereServTeinUpdate(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.hisSereServExtCreate = new HisSereServExtCreate(param);
        }

        internal bool Run(List<HisSereServUpdateMachineSDO> data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                if (!IsNotNullOrEmpty(data))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("HisSereServUpdateMachineSDO ko hop le");
                    return false;
                }
                List<long> serviceReqIds = data.Select(o => o.ServiceReqID).Distinct().ToList();
                List<HIS_SERVICE_REQ> serviceReqAlls = new HisServiceReqGet().GetByIds(serviceReqIds);

                HisSereServUpdateMachineCheck checker = new HisSereServUpdateMachineCheck(param);
                valid = valid && checker.IsValidUpdateMachine(serviceReqAlls);

                if (valid)
                {
                    List<long> machineIds = data.Select(o => o.MachineId).Distinct().ToList();
                    List<long> sereServIds = data.Select(o => o.SereServID ?? 0).Distinct().ToList();
                    sereServIds.Remove(0);

                    List<HIS_SERE_SERV_EXT> ssExtAlls = new HisSereServExtGet().GetByServiceReqIds(serviceReqIds);
                    if(IsNotNullOrEmpty(ssExtAlls))
                    {
                        List<long> machineIdExts = ssExtAlls.Select(o => o.MACHINE_ID ?? 0).ToList();
                        machineIdExts.Remove(0);
                        if (IsNotNullOrEmpty(machineIdExts))
                            machineIds.AddRange(machineIdExts);
                    }
                        
                    List<HIS_MACHINE> machineAlls = new HisMachineGet().GetByIds(machineIds);
                    List<HIS_SERE_SERV_TEIN> ssTeinAlls = new HisSereServTeinGet().GetViewByServiceReqIds(serviceReqIds);
                    List<HIS_SERE_SERV> sereServAlls = null;
                    List<HIS_SERE_SERV_EXT> ssExtCreates = new List<HIS_SERE_SERV_EXT>();
                    List<HIS_SERE_SERV> sereServCre = new List<HIS_SERE_SERV>();

                    if (IsNotNullOrEmpty(sereServIds))
                    {
                        sereServAlls = new HisSereServGet().GetByIds(sereServIds);
                    }
                    if (!IsNotNullOrEmpty(ssExtAlls))
                    {
                        sereServCre = new HisSereServGet().GetByServiceReqIds(serviceReqAlls.Select(o => o.ID).ToList());
                    }
                    else
                    {
                        
                        if (!IsNotNullOrEmpty(sereServAlls))
                        {
                            List<long> serviceReqIdssExts = ssExtAlls.Select(o => o.TDL_SERVICE_REQ_ID ?? 0).Distinct().ToList();
                            serviceReqIdssExts.Remove(0);

                            List<HIS_SERVICE_REQ> serviceReqErrors = serviceReqAlls.Where(o => !serviceReqIdssExts.Contains(o.ID)).ToList();
                            if (IsNotNullOrEmpty(serviceReqErrors))
                            {
                                sereServCre = new HisSereServGet().GetByServiceReqIds(serviceReqErrors.Select(o => o.ID).ToList());
                            }
                        }
                        else
                        {
                            List<long> sereServIdssExts = ssExtAlls.Select(o => o.SERE_SERV_ID).Distinct().ToList();
                            sereServIdssExts.Remove(0);

                            List<HIS_SERE_SERV> sereServError = sereServAlls.Where(o => !sereServIdssExts.Contains(o.ID)).ToList();
                            if (IsNotNullOrEmpty(sereServError))
                            {
                                sereServCre = sereServError;
                            }
                        }
                    }
                    if (IsNotNullOrEmpty(sereServCre))
                    {
                        foreach (var item in sereServCre)
                        {
                            HIS_SERE_SERV_EXT extCreate = new HIS_SERE_SERV_EXT();
                            extCreate.SERE_SERV_ID = item.ID;
                            extCreate.TDL_SERVICE_REQ_ID = item.SERVICE_REQ_ID;
                            extCreate.TDL_TREATMENT_ID = item.TDL_TREATMENT_ID;

                            ssExtCreates.Add(extCreate);
                        }
                    }
                    if (IsNotNullOrEmpty(ssExtCreates))
                    {
                        if (!this.hisSereServExtCreate.CreateList(ssExtCreates))
                        {
                            throw new Exception("Tao moi HIS_SERE_SERV_EXT that bai. Ket thuc nghiep vu");
                        }
                        ssExtAlls.AddRange(ssExtCreates);
                    }
                    if (!IsNotNullOrEmpty(machineAlls))
                    {
                        LogSystem.Warn("machineAlls null");
                        return false;
                    }
                    
                    List<HIS_SERE_SERV_EXT> ExtUpdates = new List<HIS_SERE_SERV_EXT>();
                    List<HIS_SERE_SERV_TEIN> TeinUpdates = new List<HIS_SERE_SERV_TEIN>();
                    List<HIS_SERVICE_REQ> hisServiceReqUpdates = new List<HIS_SERVICE_REQ>();

                    bool ssId = false;
                    List<string> logs = new List<string>();
                    foreach (var item in data)
                    {
                        HIS_MACHINE hisMachine = machineAlls.FirstOrDefault(o => o.ID == item.MachineId);
                        
                        if (item.SereServID == null)
                        {
                            List<HIS_SERE_SERV_EXT> hisSSExtServiceReqs = ssExtAlls.Where(o => o.TDL_SERVICE_REQ_ID == item.ServiceReqID).ToList();

                            HIS_SERVICE_REQ serviceReq = serviceReqAlls.FirstOrDefault(o => o.ID == item.ServiceReqID);
                            List<string> machineNames = new List<string>();
                            if (hisMachine != null)
                            {
                                foreach (var ext in hisSSExtServiceReqs)
                                {
                                    ext.MACHINE_ID = hisMachine.ID;
                                    ext.MACHINE_CODE = hisMachine.MACHINE_CODE;
                                    machineNames.Add(hisMachine.MACHINE_NAME);

                                    ExtUpdates.Add(ext);
                                }
                                if (IsNotNullOrEmpty(ssTeinAlls))
                                {
                                    List<HIS_SERE_SERV_TEIN> hisSSTeinServiceReqs = ssTeinAlls.Where(o => o.TDL_SERVICE_REQ_ID == item.ServiceReqID).ToList();
                                    foreach (var tein in hisSSTeinServiceReqs)
                                    {
                                        tein.MACHINE_ID = hisMachine.ID;
                                        TeinUpdates.Add(tein);
                                    }
                                }
                                if (IsNotNullOrEmpty(serviceReqAlls))
                                {
                                    serviceReq.MACHINE_NAMES = string.Join(",", machineNames);
                                    hisServiceReqUpdates.Add(serviceReq);

                                    string log1 = String.Format("TREATMENT_CODE: {0}. SERVICE_REQ_CODE: {1}. MACHINE_NAME: {2}", serviceReq.TDL_TREATMENT_CODE, serviceReq.SERVICE_REQ_CODE, serviceReq.MACHINE_NAMES);
                                    logs.Add(log1);
                                }
                            }
                        }

                        if (item.SereServID != null)
                        {
                            List<HIS_SERE_SERV_EXT> hisSSExtSereServs = ssExtAlls.Where(o => o.SERE_SERV_ID == item.SereServID).ToList();
                            if (hisMachine != null)
                            {
                                foreach (var ext in hisSSExtSereServs)
                                {
                                    ext.MACHINE_ID = hisMachine.ID;
                                    ext.MACHINE_CODE = hisMachine.MACHINE_CODE;
                                    ExtUpdates.Add(ext);
                                }
                                if (IsNotNullOrEmpty(ssTeinAlls))
                                {
                                    List<HIS_SERE_SERV_TEIN> hisSSTeinSereServs = ssTeinAlls.Where(o => o.SERE_SERV_ID == item.SereServID).ToList();//
                                    foreach (var tein in hisSSTeinSereServs)
                                    {
                                        tein.MACHINE_ID = hisMachine.ID;
                                        TeinUpdates.Add(tein);
                                    }
                                }
                                ssId = true;
                            }
                        }
                    }

                    if (ssId)
                    {
                        foreach (var sr in serviceReqAlls)
                        {
                            List<HIS_SERE_SERV_EXT> hisSSExtServiceReqs = ssExtAlls.Where(o => o.TDL_SERVICE_REQ_ID == sr.ID).ToList();
                            List<string> machineNames = new List<string>();
                            List<string> sereServIdLogs = new List<string>();
                            HIS_SERE_SERV hisSereServ = null;
                            foreach (var ssExt in hisSSExtServiceReqs)
                            {
                                HIS_MACHINE machine = machineAlls.FirstOrDefault(o => o.ID == ssExt.MACHINE_ID);
                                if (machine != null)
                                {
                                    machineNames.Add(machine.MACHINE_NAME);
                                    hisSereServ = sereServAlls.FirstOrDefault(o => o.ID == ssExt.SERE_SERV_ID);
                                    if (hisSereServ != null)
                                    {
                                        string ssIdLog = String.Format("SERE_SERV_ID: {0}({1})", hisSereServ.ID, machine.MACHINE_NAME);
                                        sereServIdLogs.Add(ssIdLog);
                                    }
                                }
                            }

                            sr.MACHINE_NAMES = string.Join(",", machineNames);
                            hisServiceReqUpdates.Add(sr);

                            if (hisSereServ != null)
                            {
                                string log2 = String.Format("TREATMENT_CODE: {0}. SERVICE_REQ_CODE: {1}. {2}", hisSereServ.TDL_TREATMENT_CODE, hisSereServ.TDL_SERVICE_REQ_CODE, string.Join(".", sereServIdLogs));
                                logs.Add(log2);
                            }
                        }
                            
                    }

                    if (IsNotNullOrEmpty(ssExtAlls) && IsNotNullOrEmpty(ExtUpdates))
                    {
                        if (!this.hisSSExtUpdate.UpdateList(ExtUpdates, ssExtAlls))
                        {
                            throw new Exception("Cap nhat HIS_SERE_SERV_EXT that bai. Ket thuc nghiep vu");
                        }
                    }

                    if (IsNotNullOrEmpty(ssTeinAlls) && IsNotNullOrEmpty(TeinUpdates))
                    {
                        if (!this.hisSSTeinUpdate.UpdateList(TeinUpdates, ssTeinAlls))
                        {
                            throw new Exception("Cap nhat HIS_SERE_SERV_TEIN that bai. Ket thuc nghiep vu");
                        }
                    }
                    if (IsNotNullOrEmpty(serviceReqAlls))
                    {
                        if (!this.hisServiceReqUpdate.UpdateList(hisServiceReqUpdates, serviceReqAlls))
                        {
                            throw new Exception("Cap nhat HIS_SERVICE_REQ that bai. Ket thuc nghiep vu");
                        }
                    }
                    new EventLogGenerator(EventLog.Enum.ThayDoiMayXuLyChoDichVu, logs).Run();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private void RollbackData()
        {
            this.hisSSExtUpdate.RollbackData();
            this.hisServiceReqUpdate.RollbackData();
            this.hisSSTeinUpdate.RollbackData();
        }
    }
}

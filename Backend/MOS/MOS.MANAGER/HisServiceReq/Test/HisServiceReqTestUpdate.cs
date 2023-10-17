using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisMachine;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisSereServTein;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisServiceReq.Update.Finish;
using MOS.MANAGER.HisServiceReq.Update.Status;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.TDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceReq.Test
{
    partial class HisServiceReqTestUpdate : BusinessBase
    {
        internal HisServiceReqTestUpdate()
            : base()
        {

        }

        internal HisServiceReqTestUpdate(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        /// <summary>
        /// Cap nhat thong tin ket qua cua yeu cau xet nghiem
        /// </summary>
        /// <param name="data">Du lieu ket qua xet nghiem</param>
        /// <param name="isComparativeQuery">Tim kiem theo ma yeu cau: tuong doi hay tuyet doi</param>
        /// <returns></returns>
        internal bool UpdateResult(HisTestResultTDO data, bool useBarcode)
        {
            HisSereServTeinUpdate ssTeinUpdate = new HisSereServTeinUpdate(param);
            HisSereServTeinCreate ssTeinCreate = new HisSereServTeinCreate(param);
            bool result = false;
            try
            {
                bool valid = true;
                HIS_SERVICE_REQ serviceReq = null;
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && this.HasServiceReq(useBarcode, data, ref serviceReq);
                valid = valid && (serviceReq.IS_ANTIBIOTIC_RESISTANCE == Constant.IS_TRUE || IsNotNullOrEmpty(data.TestIndexDatas));
                valid = valid && checker.HasExecute(serviceReq);
                valid = valid && this.IsAllowCancelTreatment(data, serviceReq);
                if (valid)
                {
                    if (String.IsNullOrWhiteSpace(data.ExecuteLoginname))
                    {
                        data.ExecuteLoginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        data.ExecuteUsername = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    }
                    //Lay du lieu HisSereServ tuong ung voi ServiceReq o tren
                    List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByServiceReqId(serviceReq.ID);
                    if (!IsNotNullOrEmpty(sereServs))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        throw new Exception("Ko tim thay sereServs tuong ung voi SERVICE_REQ_ID: " + serviceReq.ID);
                    }

                    string machineName = "";
                    //Lay du lieu HisSereServTein can update
                    //Neu ton tai thong tin "vi khuan" thi la tra ket qua "vi sinh"
                    if (IsNotNullOrEmpty(data.TestIndexDatas) && (data.TestIndexDatas.Exists(t => !string.IsNullOrWhiteSpace(t.BacteriumCode)) || serviceReq.IS_ANTIBIOTIC_RESISTANCE == Constant.IS_TRUE))
                    {
                        serviceReq.IS_ANTIBIOTIC_RESISTANCE = Constant.IS_TRUE;
                        this.ProcessAntibioticResult(data, ssTeinUpdate, ssTeinCreate, serviceReq, sereServs, ref machineName);
                    }
                    //Neu ko ton tai thong tin "vi khuan" thi la xu ly nhu ket qua thuong
                    else if (IsNotNullOrEmpty(data.TestIndexDatas))
                    {
                        this.ProcessTestIndex(data, ssTeinUpdate, ssTeinCreate, serviceReq, sereServs, ref machineName);
                    }

                    result = true;

                    if (serviceReq.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT && (!data.IsCancel.HasValue || !data.IsCancel.Value))
                    {
                        HIS_SERVICE_REQ serviceReqRaw = null;
                        serviceReq.ATTACHMENT_FILE_URL = data.AttachmentFileUrl;
                        serviceReq.IS_RESULTED = Constant.IS_TRUE;
                        if (data.SampleTime.HasValue)
                        {
                            serviceReq.SAMPLE_TIME = data.SampleTime;
                        }

                        if (data.FinishTime.HasValue)
                        {
                            serviceReq.FINISH_TIME = data.FinishTime;
                        }
                        else
                        {
                            serviceReq.FINISH_TIME = Inventec.Common.DateTime.Get.Now();
                        }
                        V_HIS_EXECUTE_ROOM room = HisExecuteRoomCFG.DATA.Where(o => o.ROOM_ID == serviceReq.EXECUTE_ROOM_ID).FirstOrDefault();
                        bool? doNotFinish = false;
                        if (room != null)
                        {
                            doNotFinish = HisServiceReqCFG.DoNotFinishTestServiceReqWhenReceivingResult(room.BRANCH_ID);
                        }

                        //Neu co cau hinh ko cap nhat trang thai "Hoan thanh" cua xet nghiem thi chi cap nhat cac thong tin "da co ket qua"
                        if (doNotFinish.HasValue && doNotFinish.Value)
                        {
                            //tra ket qua xet nghiem ko can verify treatment
                            if (!new HisServiceReqUpdate().Update(serviceReq, false))
                            {
                                LogSystem.Warn("Tu dong cap nhat trang thai His_service_req sang 'da co ket qua' that bai." + LogUtil.TraceData("hisServiceReq", serviceReq));
                            }
                        }
                        else
                        {
                            //tra ket qua xet nghiem ko can verify treatment
                            if (!new HisServiceReqUpdateFinish().Finish(serviceReq, false, ref serviceReqRaw, data.ExecuteLoginname, data.ExecuteUsername))
                            {
                                LogSystem.Warn("Tu dong cap nhat trang thai His_service_req sang 'hoan thanh' that bai." + LogUtil.TraceData("hisServiceReq", serviceReq));
                            }
                        }
                    }
                    else if (serviceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT && (data.IsCancel.HasValue && data.IsCancel.Value))
                    {
                        if (!new HisServiceReqUpdateUnfinish().Run(serviceReq.ID))
                        {
                            LogSystem.Warn("Tu dong cap nhat trang thai His_service_req tu 'hoan thanh' sang 'dang thuc hien' that bai." + LogUtil.TraceData("serviceReq", serviceReq));
                        }
                    }
                    else if (serviceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                    {
                        HIS_SERVICE_REQ serviceReqRaw = null;
                        if (!new HisServiceReqUpdateStart().Start(serviceReq, false, ref serviceReqRaw, data.ExecuteLoginname, data.ExecuteUsername, data.SampleTime))
                        {
                            LogSystem.Warn("Tu dong cap nhat trang thai His_service_req tu 'y/c' sang 'dang thuc hien' that bai." + LogUtil.TraceData("serviceReq", serviceReq));
                        }
                    }

                    if (data.IsUpdateApprover || data.SampleLoginname != null || data.SampleUsername != null || data.ReceiveSampleTime.HasValue)
                    {
                        if (data.IsUpdateApprover)
                        {
                            serviceReq.RESULT_APPROVER_LOGINNAME = data.ApproverLoginname;
                            serviceReq.RESULT_APPROVER_USERNAME = data.ApproverUsername;
                        }

                        if (data.SampleLoginname != null || data.SampleUsername != null)
                        {
                            serviceReq.SAMPLER_LOGINNAME = data.SampleLoginname;
                            serviceReq.SAMPLER_USERNAME = data.SampleUsername;
                        }

                        if (data.ReceiveSampleTime.HasValue)
                        {
                            serviceReq.RECEIVE_SAMPLE_LOGINNAME = data.ReceiveSampleLoginname;
                            serviceReq.RECEIVE_SAMPLE_USERNAME = data.ReceiveSampleUsername;
                            serviceReq.RECEIVE_SAMPLE_TIME = data.ReceiveSampleTime;
                        }

                        string sql = "UPDATE HIS_SERVICE_REQ SET RESULT_APPROVER_LOGINNAME = :param1, RESULT_APPROVER_USERNAME = :param2, SAMPLER_LOGINNAME = :param3, SAMPLER_USERNAME = :param4, RECEIVE_SAMPLE_LOGINNAME = :param5, RECEIVE_SAMPLE_USERNAME = :param6, RECEIVE_SAMPLE_TIME = :param7  WHERE ID = :param8 ";
                        ///execute sql se thuc hien cuoi cung
                        if (!DAOWorker.SqlDAO.Execute(sql, serviceReq.RESULT_APPROVER_LOGINNAME, serviceReq.RESULT_APPROVER_USERNAME, serviceReq.SAMPLER_LOGINNAME, serviceReq.SAMPLER_USERNAME, serviceReq.RECEIVE_SAMPLE_LOGINNAME, serviceReq.RECEIVE_SAMPLE_USERNAME, serviceReq.RECEIVE_SAMPLE_TIME, serviceReq.ID))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisServiceReq_CapNhatThatBai);
                            Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData("sqls Failed", sql));
                            throw new Exception("Cap nhat approver info cho HisServiceReq that bai");
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(machineName))
                    {
                        string sql = "UPDATE HIS_SERVICE_REQ SET MACHINE_NAMES = :param1 WHERE ID = :param2";
                        ///execute sql se thuc hien cuoi cung
                        if (!DAOWorker.SqlDAO.Execute(sql, machineName, serviceReq.ID))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisServiceReq_CapNhatThatBai);
                            Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData("sqls Failed", sql));
                            throw new Exception("Cap nhat cac ten may xu ly dich vu tuong ung voi y lenh that bai");
                        }
                        new EventLogGenerator(EventLog.Enum.ChonMayXuLyChoDichVu, machineName)
                        .TreatmentCode(serviceReq.TDL_TREATMENT_CODE)
                        .ServiceReqCode(serviceReq.SERVICE_REQ_CODE)
                        .Run();
                    }
                    if (!string.IsNullOrEmpty(data.Note) || data.Note == null)
                    {
                        string sql = "UPDATE HIS_SERVICE_REQ SET NOTE = :param1 WHERE ID = :param2";
                        ///execute sql se thuc hien cuoi cung
                        if (!DAOWorker.SqlDAO.Execute(sql, data.Note, serviceReq.ID))
                        {
                            LogSystem.Warn("cap nhat NOTE trong His_service_req that bai." + LogUtil.TraceData("hisServiceReq", serviceReq));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessTestIndex(HisTestResultTDO data, HisSereServTeinUpdate ssTeinUpdate, HisSereServTeinCreate ssTeinCreate, HIS_SERVICE_REQ serviceReq, List<HIS_SERE_SERV> sereServs, ref string machineName)
        {
            List<long> serviceIds = sereServs.Select(s => s.SERVICE_ID).ToList();
            List<long> sereServIds = sereServs.Select(o => o.ID).ToList();
            List<string> testIndexCodes = data.TestIndexDatas.Select(o => o.TestIndexCode).ToList();

            List<V_HIS_TEST_INDEX> hisTestIndexs = Config.HisTestIndexCFG.DATA_VIEW != null ? Config.HisTestIndexCFG.DATA_VIEW.Where(o => o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE && serviceIds.Contains(o.TEST_SERVICE_TYPE_ID)).ToList() : null;

            List<V_HIS_SERE_SERV_TEIN> sereServTeins = this.GetHisSereServTein(sereServIds, testIndexCodes);

            List<HIS_SERE_SERV_TEIN> listToUpdate = new List<HIS_SERE_SERV_TEIN>();
            List<HIS_SERE_SERV_TEIN> listToCreate = new List<HIS_SERE_SERV_TEIN>();
            Mapper.CreateMap<V_HIS_SERE_SERV_TEIN, HIS_SERE_SERV_TEIN>();

            HisSereServExtCreate ssExtCreate = new HisSereServExtCreate(param);
            HisSereServExtUpdate ssExtUpdate = new HisSereServExtUpdate(param);

            List<HIS_SERE_SERV_EXT> lstSereServExt = new HisSereServExtGet().GetBySereServIds(sereServIds);
            List<HIS_SERE_SERV_EXT> listExtUpdate = new List<HIS_SERE_SERV_EXT>();
            List<HIS_SERE_SERV_EXT> listExtCreate = new List<HIS_SERE_SERV_EXT>();
            List<HIS_SERE_SERV_EXT> listExtBefore = new List<HIS_SERE_SERV_EXT>();

            Dictionary<long, List<string>> dicExecuteName = new Dictionary<long, List<string>>();
            string firstExecuteLoginname = "";
            string firstExecuteUsername = "";

            foreach (HisTestIndexResultTDO tdo in data.TestIndexDatas)
            {
                V_HIS_SERE_SERV_TEIN viewSSTein = sereServTeins != null ? sereServTeins.FirstOrDefault(o => o.TEST_INDEX_CODE == tdo.TestIndexCode) : null;
                if (viewSSTein != null)
                {
                    HIS_SERE_SERV_TEIN ssTein = Mapper.Map<HIS_SERE_SERV_TEIN>(viewSSTein);
                    ssTein.VALUE = tdo.Value;
                    ssTein.RESULT_CODE = tdo.ResultCode;
                    ssTein.DESCRIPTION = tdo.Description;
                    ssTein.NOTE = tdo.Note;
                    ssTein.LEAVEN = tdo.Leaven;
                    ssTein.OLD_VALUE = tdo.OldValue;
                    ssTein.RESULT_DESCRIPTION = tdo.ResultDescription;

                    if (tdo.MachineId.HasValue)
                    {
                        ssTein.MACHINE_ID = tdo.MachineId;
                    }
                    else if (!String.IsNullOrEmpty(tdo.MayXetNghiemID))
                    {
                        HIS_MACHINE machine = HisMachineCFG.DATA != null ? HisMachineCFG.DATA.FirstOrDefault(o => o.MACHINE_CODE == tdo.MayXetNghiemID) : null;
                        if (machine != null)
                        {
                            ssTein.MACHINE_ID = machine.ID;
                        }
                        else
                        {
                            LogSystem.Warn("Ma may xet nghiem khong chinh xac: " + tdo.MayXetNghiemID);
                        }
                    }

                    if (!String.IsNullOrWhiteSpace(tdo.ExecuteLoginname))
                    {
                        if (!dicExecuteName.ContainsKey(viewSSTein.SERE_SERV_ID))
                        {
                            dicExecuteName[viewSSTein.SERE_SERV_ID] = new List<string>();
                        }

                        dicExecuteName[viewSSTein.SERE_SERV_ID].Add(string.Format("{0}|{1}", tdo.ExecuteLoginname, tdo.ExecuteUsername));

                        if (String.IsNullOrWhiteSpace(firstExecuteLoginname))
                        {
                            firstExecuteLoginname = tdo.ExecuteLoginname;
                            firstExecuteUsername = tdo.ExecuteUsername;
                        }
                    }

                    listToUpdate.Add(ssTein);
                }
                else
                {
                    V_HIS_TEST_INDEX tIndex = hisTestIndexs != null ? hisTestIndexs.FirstOrDefault(o => o.TEST_INDEX_CODE == tdo.TestIndexCode) : null;
                    if (tIndex != null)
                    {
                        HIS_SERE_SERV_TEIN ssTein = new HIS_SERE_SERV_TEIN();
                        ssTein.TEST_INDEX_ID = tIndex.ID;
                        ssTein.TDL_TREATMENT_ID = serviceReq.TREATMENT_ID;
                        ssTein.TDL_SERVICE_REQ_ID = serviceReq.ID;
                        ssTein.SERE_SERV_ID = sereServs.FirstOrDefault(o => o.SERVICE_ID == tIndex.TEST_SERVICE_TYPE_ID).ID;
                        ssTein.VALUE = tdo.Value;
                        ssTein.RESULT_CODE = tdo.ResultCode;
                        ssTein.DESCRIPTION = tdo.Description;
                        ssTein.NOTE = tdo.Note;
                        ssTein.LEAVEN = tdo.Leaven;
                        ssTein.OLD_VALUE = tdo.OldValue;
                        ssTein.RESULT_DESCRIPTION = tdo.ResultDescription;

                        if (tdo.MachineId.HasValue)
                        {
                            ssTein.MACHINE_ID = tdo.MachineId;
                        }
                        else if (!String.IsNullOrEmpty(tdo.MayXetNghiemID))
                        {
                            HIS_MACHINE machine = HisMachineCFG.DATA != null ? HisMachineCFG.DATA.FirstOrDefault(o => o.MACHINE_CODE == tdo.MayXetNghiemID) : null;
                            if (machine != null)
                            {
                                ssTein.MACHINE_ID = machine.ID;
                            }
                            else
                            {
                                LogSystem.Warn("Ma may xet nghiem khong chinh xac: " + tdo.MayXetNghiemID);
                            }
                        }

                        if (!String.IsNullOrWhiteSpace(tdo.ExecuteLoginname))
                        {
                            if (!dicExecuteName.ContainsKey(ssTein.SERE_SERV_ID))
                            {
                                dicExecuteName[ssTein.SERE_SERV_ID] = new List<string>();
                            }

                            dicExecuteName[ssTein.SERE_SERV_ID].Add(string.Format("{0}|{1}", tdo.ExecuteLoginname, tdo.ExecuteUsername));

                            if (String.IsNullOrWhiteSpace(firstExecuteLoginname))
                            {
                                firstExecuteLoginname = tdo.ExecuteLoginname;
                                firstExecuteUsername = tdo.ExecuteUsername;
                            }
                        }

                        listToCreate.Add(ssTein);
                    }
                }
            }

            if (dicExecuteName.Count > 0)
            {
                foreach (var item in dicExecuteName)
                {
                    string executeLoginname = "";
                    string executeUsername = "";

                    if (ProcessExecuteName(item.Value, ref executeLoginname, ref executeUsername))
                    {
                        HIS_SERE_SERV_EXT ext = lstSereServExt != null ? lstSereServExt.FirstOrDefault(o => o.SERE_SERV_ID == item.Key) : null;
                        if (ext != null)
                        {
                            Mapper.CreateMap<HIS_SERE_SERV_EXT, HIS_SERE_SERV_EXT>();
                            HIS_SERE_SERV_EXT before = Mapper.Map<HIS_SERE_SERV_EXT>(ext);
                            ext.CONCLUDE = data.Conclude;
                            ext.DESCRIPTION = data.Description;

                            if (ext.SUBCLINICAL_RESULT_LOGINNAME != executeLoginname)
                            {
                                ext.SUBCLINICAL_RESULT_LOGINNAME = executeLoginname;
                                ext.SUBCLINICAL_RESULT_USERNAME = executeUsername;
                            }

                            listExtUpdate.Add(ext);
                            listExtBefore.Add(before);
                        }
                        else if (ext == null)
                        {
                            HIS_SERE_SERV ss = sereServs.FirstOrDefault(o => o.ID == item.Key) ?? null;
                            if (IsNotNull(ss))
                            {
                                ext = new HIS_SERE_SERV_EXT();
                                ext.SUBCLINICAL_RESULT_LOGINNAME = executeLoginname;
                                ext.SUBCLINICAL_RESULT_USERNAME = executeUsername;
                                ext.SERE_SERV_ID = ss.ID;
                                ext.TDL_SERVICE_REQ_ID = ss.SERVICE_REQ_ID;
                                ext.TDL_TREATMENT_ID = ss.TDL_TREATMENT_ID;
                                ext.CONCLUDE = data.Conclude;
                                ext.DESCRIPTION = data.Description;
                                listExtCreate.Add(ext);
                            }
                        }
                    }
                }

                serviceReq.EXECUTE_LOGINNAME = firstExecuteLoginname;
                serviceReq.EXECUTE_USERNAME = firstExecuteUsername;
            }
            else if (IsNotNullOrEmpty(sereServs))
            {
                foreach (var item in sereServs)
                {
                    HIS_SERE_SERV_EXT ext = lstSereServExt != null ? lstSereServExt.FirstOrDefault(o => o.SERE_SERV_ID == item.ID) : null;
                    if (ext != null)
                    {
                        Mapper.CreateMap<HIS_SERE_SERV_EXT, HIS_SERE_SERV_EXT>();
                        HIS_SERE_SERV_EXT before = Mapper.Map<HIS_SERE_SERV_EXT>(ext);
                        ext.CONCLUDE = data.Conclude;
                        ext.DESCRIPTION = data.Description;
                        listExtUpdate.Add(ext);
                        listExtBefore.Add(before);
                    }
                    else if (ext == null)
                    {
                        if (IsNotNull(item))
                        {
                            ext = new HIS_SERE_SERV_EXT();
                            ext.SERE_SERV_ID = item.ID;
                            ext.TDL_SERVICE_REQ_ID = item.SERVICE_REQ_ID;
                            ext.TDL_TREATMENT_ID = item.TDL_TREATMENT_ID;
                            ext.CONCLUDE = data.Conclude;
                            ext.DESCRIPTION = data.Description;
                            listExtCreate.Add(ext);
                        }
                    }
                }
            }
            
            if (IsNotNullOrEmpty(listExtUpdate) && !ssExtUpdate.UpdateList(listExtUpdate, listExtBefore))
            {
                throw new Exception("Cap nhat SereServExt that bai: ");
            }

            if (IsNotNullOrEmpty(listExtCreate) && !ssExtCreate.CreateList(listExtCreate))
            {
                throw new Exception("Them moi SereServExt that bai: ");
            }

            if (IsNotNullOrEmpty(listToUpdate) && !ssTeinUpdate.UpdateList(listToUpdate, serviceReq.TDL_PATIENT_DOB, serviceReq.TDL_PATIENT_GENDER_ID.Value))
            {
                throw new Exception("Cap nhat SereServTein that bai: ");
            }

            if (IsNotNullOrEmpty(listToCreate) && !ssTeinCreate.CreateList(listToCreate, serviceReq.TDL_PATIENT_DOB, serviceReq.TDL_PATIENT_GENDER_ID.Value))
            {
                throw new Exception("Them moi SereServTein that bai: ");
            }

            List<HIS_SERE_SERV_TEIN> sereSereTeinAfters = new List<HIS_SERE_SERV_TEIN>();

            if (IsNotNullOrEmpty(listToUpdate))
            {
                sereSereTeinAfters.AddRange(listToUpdate);
            }
            if (IsNotNullOrEmpty(listToCreate))
            {
                sereSereTeinAfters.AddRange(listToCreate);
            }
            if (IsNotNullOrEmpty(sereServTeins) && IsNotNullOrEmpty(sereSereTeinAfters))
            {
                List<V_HIS_SERE_SERV_TEIN> ssTeins = sereServTeins.Where(o => !sereSereTeinAfters.Select(p => p.ID).Contains(o.ID)).ToList();
                foreach (var item in ssTeins)
                {
                    HIS_SERE_SERV_TEIN Tein = new HIS_SERE_SERV_TEIN();
                    Tein.ID = item.ID;
                    Tein.MACHINE_ID = item.MACHINE_ID;
                    sereSereTeinAfters.Add(Tein);
                }
            }
            if (IsNotNullOrEmpty(sereSereTeinAfters))
            {
                sereSereTeinAfters = sereSereTeinAfters.OrderBy(o => o.SERE_SERV_ID).ToList();
                List<string> machineNameAfters = new List<string>();
                List<long> machineIdAfters = sereSereTeinAfters.Select(o => o.MACHINE_ID ?? 0).Distinct().ToList();
                if (IsNotNullOrEmpty(machineIdAfters))
                {
                    List<HIS_MACHINE> machineAlls = new HisMachineGet().GetByIds(machineIdAfters);
                    string machineNameUpdate = "";
                    if (IsNotNullOrEmpty(machineAlls))
                    {
                        long sereSereIdBefore = 0;
                        foreach (var item in sereSereTeinAfters)
                        {
                            if (sereSereIdBefore != item.SERE_SERV_ID)
                            {
                                HIS_MACHINE machine = machineAlls.FirstOrDefault(o => o.ID == item.MACHINE_ID);
                                if (machine != null)
                                {
                                    machineNameAfters.Add(machine.MACHINE_NAME);
                                    sereSereIdBefore = item.SERE_SERV_ID;
                                }
                            }
                        }
                        if (IsNotNullOrEmpty(machineNameAfters))
                        {
                            machineNameUpdate = string.Join(",", machineNameAfters);
                            if (machineNameUpdate != serviceReq.MACHINE_NAMES)
                            {
                                machineName = machineNameUpdate;
                            }
                        }
                    }
                }
            }
        }

        private void ProcessAntibioticResult(HisTestResultTDO data, HisSereServTeinUpdate ssTeinUpdate, HisSereServTeinCreate ssTeinCreate, HIS_SERVICE_REQ serviceReq, List<HIS_SERE_SERV> sereServs, ref string machineName)
        {
            HisSereServExtCreate ssExtCreate = new HisSereServExtCreate(param);
            HisSereServExtUpdate ssExtUpdate = new HisSereServExtUpdate(param);

            List<long> sereServIds = sereServs.Select(o => o.ID).ToList();
            List<V_HIS_SERE_SERV_TEIN> sereServTeins = this.GetHisSereServTein(sereServIds, null);

            List<HIS_SERE_SERV_TEIN> listToUpdate = new List<HIS_SERE_SERV_TEIN>();
            List<HIS_SERE_SERV_TEIN> listToCreate = new List<HIS_SERE_SERV_TEIN>();

            List<HIS_SERE_SERV_EXT> lstSereServExt = new HisSereServExtGet().GetBySereServIds(sereServIds);
            List<HIS_SERE_SERV_EXT> listExtUpdate = new List<HIS_SERE_SERV_EXT>();
            List<HIS_SERE_SERV_EXT> listExtCreate = new List<HIS_SERE_SERV_EXT>();
            List<HIS_SERE_SERV_EXT> listExtBefore = new List<HIS_SERE_SERV_EXT>();

            //Xu ly de tu ma chi so, suy ra ma dich vu. Vi co 2 truong hop tra ve du lieu:
            //- Tich hop voi Roche --> tra ve ma chi so
            //- Tra ket qua vi sinh tren HisPro --> truyen vao ma dich vu

            //Tao d/s luu cac chi so duoc su dung de tranh truy van den danh muc nhieu lan (HisTestIndexCFG.DATA_VIEW)
            List<V_HIS_TEST_INDEX> indexes = new List<V_HIS_TEST_INDEX>();

            foreach (HisTestIndexResultTDO tdo in data.TestIndexDatas)
            {
                if (!IsNotNullOrEmpty(tdo.ServiceCode) && IsNotNullOrEmpty(tdo.TestIndexCode))
                {
                    V_HIS_TEST_INDEX testIndex = indexes.Where(o => o.TEST_INDEX_CODE == tdo.TestIndexCode).FirstOrDefault();
                    if (testIndex == null)
                    {
                        testIndex = HisTestIndexCFG.DATA_VIEW.Where(o => o.TEST_INDEX_CODE == tdo.TestIndexCode).FirstOrDefault();
                        if (testIndex != null)
                        {
                            indexes.Add(testIndex);
                        }
                    }

                    if (testIndex != null)
                    {
                        tdo.ServiceCode = testIndex.SERVICE_CODE;
                    }
                }
            }

            var Groups = data.TestIndexDatas.GroupBy(g => g.ServiceCode).ToList();
            foreach (var group in Groups)
            {
                if (String.IsNullOrWhiteSpace(group.Key))
                {
                    LogSystem.Warn("Tra ket qua Khang Sinh Do. Khong co thong tin ServiceCode: \n" + LogUtil.TraceData("Data", group.ToList()));
                    continue;
                }

                HIS_SERE_SERV ss = sereServs.FirstOrDefault(o => o.TDL_SERVICE_CODE == group.Key);
                if (ss == null)
                {
                    LogSystem.Warn("Tra ket qua Khang Sinh Do. Khong lay duoc SereServ theo ServiceCode: \n" + LogUtil.TraceData("Data", group.ToList()));
                    continue;
                }

                HIS_SERE_SERV_EXT ext = lstSereServExt != null ? lstSereServExt.FirstOrDefault(o => o.SERE_SERV_ID == ss.ID) : null;
                if (ext != null && ext.CONCLUDE != group.FirstOrDefault().MicrobiologicalResult)
                {
                    Mapper.CreateMap<HIS_SERE_SERV_EXT, HIS_SERE_SERV_EXT>();
                    HIS_SERE_SERV_EXT before = Mapper.Map<HIS_SERE_SERV_EXT>(ext);
                    ext.CONCLUDE = group.FirstOrDefault().MicrobiologicalResult;
                    listExtUpdate.Add(ext);
                    listExtBefore.Add(before);
                }
                else if (ext == null)
                {
                    ext = new HIS_SERE_SERV_EXT();
                    ext.CONCLUDE = group.FirstOrDefault().MicrobiologicalResult;
                    ext.SERE_SERV_ID = ss.ID;
                    ext.TDL_SERVICE_REQ_ID = ss.SERVICE_REQ_ID;
                    ext.TDL_TREATMENT_ID = ss.TDL_TREATMENT_ID;
                    listExtCreate.Add(ext);
                }

                foreach (HisTestIndexResultTDO tdo in group.ToList())
                {
                    if (String.IsNullOrWhiteSpace(tdo.BacteriumCode) || String.IsNullOrWhiteSpace(tdo.AntibioticCode))
                    {
                        LogSystem.Warn("Tra ket qua Khang Sinh Do. Chi so khong co ma vi khuan va ma khang sinh: \n" + LogUtil.TraceData("TDO", tdo));
                        continue;
                    }

                    V_HIS_SERE_SERV_TEIN viewSSTein = sereServTeins != null ? sereServTeins.FirstOrDefault(o => o.SERE_SERV_ID == ss.ID && o.BACTERIUM_CODE == tdo.BacteriumCode && o.ANTIBIOTIC_RESISTANCE_CODE == tdo.AntibioticCode) : null;

                    if (viewSSTein != null)
                    {
                        Mapper.CreateMap<V_HIS_SERE_SERV_TEIN, HIS_SERE_SERV_TEIN>();
                        HIS_SERE_SERV_TEIN ssTein = Mapper.Map<HIS_SERE_SERV_TEIN>(viewSSTein);
                        ssTein.BACTERIUM_AMOUNT = tdo.BacteriumAmount;
                        ssTein.BACTERIUM_DENSITY = tdo.BacteriumDensity;
                        ssTein.BACTERIUM_NAME = tdo.BacteriumName;
                        ssTein.BACTERIUM_CODE = tdo.BacteriumCode;
                        ssTein.ANTIBIOTIC_RESISTANCE_NAME = tdo.AntibioticName;
                        ssTein.BACTERIUM_NOTE = tdo.BacteriumNote;
                        ssTein.NOTE = tdo.Note;
                        ssTein.VALUE = tdo.Mic;
                        ssTein.SRI_CODE = tdo.SriCode;

                        if (tdo.MachineId.HasValue)
                        {
                            ssTein.MACHINE_ID = tdo.MachineId;
                        }
                        else if (!String.IsNullOrEmpty(tdo.MayXetNghiemID))
                        {
                            HIS_MACHINE machine = HisMachineCFG.DATA != null ? HisMachineCFG.DATA.FirstOrDefault(o => o.MACHINE_CODE == tdo.MayXetNghiemID) : null;
                            if (machine != null)
                            {
                                ssTein.MACHINE_ID = machine.ID;
                            }
                            else
                            {
                                LogSystem.Warn("Ma may xet nghiem khong chinh xac: " + tdo.MayXetNghiemID);
                            }
                        }
                        listToUpdate.Add(ssTein);
                    }
                    else
                    {
                        HIS_SERE_SERV_TEIN ssTein = new HIS_SERE_SERV_TEIN();
                        ssTein.TDL_TREATMENT_ID = serviceReq.TREATMENT_ID;
                        ssTein.TDL_SERVICE_REQ_ID = serviceReq.ID;
                        ssTein.SERE_SERV_ID = ss.ID;
                        ssTein.BACTERIUM_CODE = tdo.BacteriumCode;
                        ssTein.BACTERIUM_NAME = tdo.BacteriumName;
                        ssTein.BACTERIUM_AMOUNT = tdo.BacteriumAmount;
                        ssTein.BACTERIUM_DENSITY = tdo.BacteriumDensity;
                        ssTein.ANTIBIOTIC_RESISTANCE_CODE = tdo.AntibioticCode;
                        ssTein.ANTIBIOTIC_RESISTANCE_NAME = tdo.AntibioticName;
                        ssTein.BACTERIUM_NOTE = tdo.BacteriumNote;
                        ssTein.NOTE = tdo.Note;
                        ssTein.VALUE = tdo.Mic;
                        ssTein.SRI_CODE = tdo.SriCode;

                        if (tdo.MachineId.HasValue)
                        {
                            ssTein.MACHINE_ID = tdo.MachineId;
                        }
                        else if (!String.IsNullOrEmpty(tdo.MayXetNghiemID))
                        {
                            HIS_MACHINE machine = HisMachineCFG.DATA != null ? HisMachineCFG.DATA.FirstOrDefault(o => o.MACHINE_CODE == tdo.MayXetNghiemID) : null;
                            if (machine != null)
                            {
                                ssTein.MACHINE_ID = machine.ID;
                            }
                            else
                            {
                                LogSystem.Warn("Ma may xet nghiem khong chinh xac: " + tdo.MayXetNghiemID);
                            }
                        }
                        listToCreate.Add(ssTein);
                    }
                }
            }

            if (IsNotNullOrEmpty(listExtUpdate) && !ssExtUpdate.UpdateList(listExtUpdate, listExtBefore))
            {
                throw new Exception("Cap nhat SereServExt that bai: ");
            }

            if (IsNotNullOrEmpty(listExtCreate) && !ssExtCreate.CreateList(listExtCreate))
            {
                throw new Exception("Them moi SereServExt that bai: ");
            }

            if (IsNotNullOrEmpty(listToUpdate) && !ssTeinUpdate.UpdateList(listToUpdate, serviceReq.TDL_PATIENT_DOB, serviceReq.TDL_PATIENT_GENDER_ID.Value))
            {
                throw new Exception("Cap nhat SereServTein that bai: ");
            }

            if (IsNotNullOrEmpty(listToCreate) && !ssTeinCreate.CreateList(listToCreate, serviceReq.TDL_PATIENT_DOB, serviceReq.TDL_PATIENT_GENDER_ID.Value))
            {
                throw new Exception("Them moi SereServTein that bai: ");
            }

            List<HIS_SERE_SERV_TEIN> sereSereTeinAfters = new List<HIS_SERE_SERV_TEIN>();

            if (IsNotNullOrEmpty(listToUpdate))
            {
                sereSereTeinAfters.AddRange(listToUpdate);
            }
            if (IsNotNullOrEmpty(listToCreate))
            {
                sereSereTeinAfters.AddRange(listToCreate);
            }
            if (IsNotNullOrEmpty(sereServTeins) && IsNotNullOrEmpty(sereSereTeinAfters))
            {
                List<V_HIS_SERE_SERV_TEIN> ssTeins = sereServTeins.Where(o => !sereSereTeinAfters.Select(p => p.ID).Contains(o.ID)).ToList();
                foreach (var item in ssTeins)
                {
                    HIS_SERE_SERV_TEIN Tein = new HIS_SERE_SERV_TEIN();
                    Tein.ID = item.ID;
                    Tein.MACHINE_ID = item.MACHINE_ID;
                    sereSereTeinAfters.Add(Tein);
                }
            }
            if (IsNotNullOrEmpty(sereSereTeinAfters))
            {
                sereSereTeinAfters = sereSereTeinAfters.OrderBy(o => o.SERE_SERV_ID).ToList();
                List<string> machineNameAfters = new List<string>();
                List<long> machineIdAfters = sereSereTeinAfters.Select(o => o.MACHINE_ID ?? 0).Distinct().ToList();
                if (IsNotNullOrEmpty(machineIdAfters))
                {
                    List<HIS_MACHINE> machineAlls = new HisMachineGet().GetByIds(machineIdAfters);
                    string machineNameUpdate = "";
                    if (IsNotNullOrEmpty(machineAlls))
                    {
                        long sereSereIdBefore = 0;
                        foreach (var item in sereSereTeinAfters)
                        {
                            if (sereSereIdBefore != item.SERE_SERV_ID)
                            {
                                HIS_MACHINE machine = machineAlls.FirstOrDefault(o => o.ID == item.MACHINE_ID);
                                if (machine != null)
                                {
                                    machineNameAfters.Add(machine.MACHINE_NAME);
                                    sereSereIdBefore = item.SERE_SERV_ID;
                                }
                            }
                        }
                        if (IsNotNullOrEmpty(machineNameAfters))
                        {
                            machineNameUpdate = string.Join(",", machineNameAfters);
                            if (machineNameUpdate != serviceReq.MACHINE_NAMES)
                            {
                                machineName = machineNameUpdate;
                            }
                        }
                    }
                }
            }
        }

        private bool HasServiceReq(bool useBarcode, HisTestResultTDO data, ref HIS_SERVICE_REQ serviceReq)
        {
            bool result = false;
            try
            {
                if (data.ServiceReqId.HasValue)
                {
                    serviceReq = new HisServiceReqGet().GetById(data.ServiceReqId.Value);
                    if (serviceReq == null)
                    {
                        LogSystem.Error("Ko ton tai service_req tuong ung voi id:" + data.ServiceReqId);
                    }
                }
                else
                {
                    //Neu truy van theo barcode (fix voi roche)
                    if (useBarcode)
                    {
                        serviceReq = new HisServiceReqGet().GetByBarcode(data.ServiceReqCode);
                    }
                    else
                    {
                        serviceReq = new HisServiceReqGet().GetByServiceReqCode(data.ServiceReqCode);
                    }
                    if (serviceReq == null)
                    {
                        LogSystem.Error("Ko ton tai service_req tuong ung voi ma (barcode):" + data.ServiceReqCode);
                    }
                }

                result = serviceReq != null;
                if (!result)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private bool IsAllowCancelTreatment(HisTestResultTDO data, HIS_SERVICE_REQ serviceReq)
        {
            bool result = true;
            try
            {
                HIS_TREATMENT treatment = new HisTreatmentGet().GetById(serviceReq.TREATMENT_ID);
                if (!HisServiceReqCFG.IS_ALLOWING_PROCESSING_SUBCLINICAL_AFTER_LOCKING_TREATMENT 
                    && treatment != null && treatment.IS_LOCK_HEIN == Constant.IS_TRUE && treatment.TDL_PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_HoSoBenhAnDaDuocDuyetKhongChoPhepSuaThongTinXuTri);
                    return false;
                }
                if (data.IsCancel.HasValue && data.IsCancel.Value)
                {
                    HisTreatmentCheck checker = new HisTreatmentCheck(param);
                    return checker.IsUnpause(treatment)
                        && checker.IsUnLock(treatment)
                        && checker.IsUnTemporaryLock(treatment)
                        && checker.IsUnLockHein(treatment);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Lay thong tin HisSereServTein dua vao danh sach sereServId va testIndexCode
        /// </summary>
        /// <param name="sereServIds"></param>
        /// <param name="testIndexCodes"></param>
        /// <returns></returns>
        private List<V_HIS_SERE_SERV_TEIN> GetHisSereServTein(List<long> sereServIds, List<string> testIndexCodes)
        {
            HisSereServTeinViewFilterQuery sereServTeinViewFilter = new HisSereServTeinViewFilterQuery();
            sereServTeinViewFilter.SERE_SERV_IDs = sereServIds;
            sereServTeinViewFilter.TEST_INDEX_CODEs = testIndexCodes;
            return new HisSereServTeinGet().GetView(sereServTeinViewFilter);
        }

        /// <summary>
        /// Cap nhat y lenh sang trang thai bat dau
        /// </summary>
        internal bool UpdateStart(string barcode, string loginName, string userName)
        {
            bool result = false;
            try
            {
                if (!string.IsNullOrWhiteSpace(barcode))
                {
                    HIS_SERVICE_REQ serviceReq = new HisServiceReqGet().GetByBarcode(barcode);

                    if (serviceReq == null)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        throw new Exception("Ko tim thay HIS_SERVICE_REQ tuong ung voi barcode: " + barcode);
                    }

                    HIS_SERVICE_REQ resultData = null;

                    //Chi thuc hien cap nhat khi dang o trang thai "chua xu ly"
                    if (serviceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL
                        && !new HisServiceReqUpdateStart().Start(serviceReq, false, ref resultData, loginName, userName))
                    {
                        LogSystem.Warn("Tu dong cap nhat trang thai His_service_req tu 'y/c' sang 'dang thuc hien' that bai." + LogUtil.TraceData("serviceReq", serviceReq));
                    }
                    else
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private bool ProcessExecuteName(List<string> listName, ref string executeLoginname, ref string executeUsername)
        {
            bool result = true;
            try
            {
                List<string> loginname = new List<string>();
                List<string> username = new List<string>();

                listName = listName.Distinct().OrderBy(o => o).ToList();
                foreach (var item in listName)
                {
                    string[] splitName = item.Split('|');
                    loginname.Add(splitName[0]);
                    username.Add(splitName[1]);
                }

                executeLoginname = string.Join(";", loginname);
                executeUsername = string.Join(";", username);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}

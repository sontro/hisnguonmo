using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBlood;
using MOS.MANAGER.HisExpMestBlood;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisSereServTein;
using MOS.MANAGER.HisServiceReq.Update.Finish;
using MOS.MANAGER.HisServiceReq.Update.Status;
using MOS.SDO;
using MOS.TDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Approve
{
    class BloodProcessor : BusinessBase
    {
        private HisExpMestBloodMaker hisExpMestBloodMaker;
        private HisExpMestBltyReqIncreaseDdAmount hisExpMestBltyReqIncreaseDdAmount;
        private HisExpMestBloodUpdate hisExpMestBloodUpdate;
        private HisSereServTeinUpdate ssTeinUpdate;
        private HisSereServTeinCreate ssTeinCreate;
        private HisSereServExtCreate ssExtCreate;
        private HisSereServExtUpdate ssExtUpdate;
        private HisBloodUpdate blUpdate;

        internal BloodProcessor()
            : base()
        {
            this.Init();
        }

        internal BloodProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestBloodMaker = new HisExpMestBloodMaker(param);
            this.hisExpMestBltyReqIncreaseDdAmount = new HisExpMestBltyReqIncreaseDdAmount(param);
            this.hisExpMestBloodUpdate = new HisExpMestBloodUpdate(param);
            this.ssTeinUpdate = new HisSereServTeinUpdate(param);
            this.ssTeinCreate = new HisSereServTeinCreate(param);
            this.ssExtCreate = new HisSereServExtCreate(param);
            this.ssExtUpdate = new HisSereServExtUpdate(param);
            this.blUpdate = new HisBloodUpdate(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_BLTY_REQ> bltyReqs, List<ExpBloodSDO> bloods, string loginname, string username, long approvalTime, List<HisTestResultTDO> testResults, ref string exBloodCodes, ref List<HIS_EXP_MEST_BLOOD> expMestBloods, ref List<string> sqls)
        {
            try
            {
                if (IsNotNullOrEmpty(bltyReqs) && IsNotNullOrEmpty(bloods) && HisExpMestConstant.HAS_REQ_EXP_MEST_TYPE_IDs.Contains(expMest.EXP_MEST_TYPE_ID))
                {
                    List<V_HIS_BLOOD> bls = null;
                    //Tao exp_mest_blood
                    if (!this.hisExpMestBloodMaker.Run(bloods, expMest, loginname, username, approvalTime, ref bls, ref exBloodCodes, ref expMestBloods, ref sqls))
                    {
                        throw new Exception("exp_mest_material: Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //neu duyet thanh cong thi cap nhat so luong da duyet vao blty_req
                    if (IsNotNullOrEmpty(expMestBloods))
                    {
                        Dictionary<long, decimal> increaseDic = new Dictionary<long, decimal>();

                        //cap nhat so luong da duyet
                        foreach (HIS_EXP_MEST_BLTY_REQ req in bltyReqs)
                        {
                            decimal approvalAmount = expMestBloods.Where(o => o.EXP_MEST_BLTY_REQ_ID == req.ID).Count();
                            if (approvalAmount > 0)
                            {
                                increaseDic.Add(req.ID, approvalAmount);
                            }
                        }

                        if (IsNotNullOrEmpty(increaseDic))
                        {
                            if (!this.hisExpMestBltyReqIncreaseDdAmount.Run(increaseDic))
                            {
                                throw new Exception("Cap nhat dd_amount that bai. Rollback");
                            }
                        }

                        List<HIS_BLOOD> blToUpdates = new List<HIS_BLOOD>();
                        Mapper.CreateMap<V_HIS_BLOOD, HIS_BLOOD>();

                        foreach (HIS_EXP_MEST_BLOOD expb in expMestBloods)
                        {
                            V_HIS_BLOOD vBl = IsNotNullOrEmpty(bls) ? bls.FirstOrDefault(o => o.ID == expb.BLOOD_ID) : null;
                            HIS_BLOOD bl = Mapper.Map<HIS_BLOOD>(vBl);

                            if (IsNotNull(bl))
                            {
                                bl.LAST_EXP_MEST_BLOOD_ID = expb.ID;
                                blToUpdates.Add(bl);
                            }
                        }

                        if (IsNotNullOrEmpty(blToUpdates) && !this.blUpdate.UpdateList(blToUpdates, false))
                        {
                            throw new Exception("Cap nhat LAST_EXP_MEST_BLOOD_ID HIS_BLOOD that bai. Rollback");
                        }
                    }
                }
                //Voi cac loai xuat ma ko tao y/c xuat thi thuc hien cap nhat thong tin duyet
                else if (!HisExpMestConstant.HAS_REQ_EXP_MEST_TYPE_IDs.Contains(expMest.EXP_MEST_TYPE_ID))
                {
                    this.UpdateExpMestBlood(expMest, loginname, username, approvalTime, ref expMestBloods);
                }

                this.ProcessTestResult(testResults, expMest.SERVICE_REQ_ID);

                return true;
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                return false;
            }
        }

        private void ProcessTestResult(List<HisTestResultTDO> testResults, long? serviceReqId)
        {

            if (IsNotNullOrEmpty(testResults) && serviceReqId.HasValue)
            {
                List<HIS_SERVICE_REQ> dvkts = new HisServiceReqGet().GetByParentId(serviceReqId.Value);

                if (IsNotNullOrEmpty(dvkts))
                {
                    List<HIS_SERE_SERV> allSereServs = new HisSereServGet().GetByServiceReqIds(dvkts.Select(o => o.ID).ToList());
                    foreach (var testResult in testResults)
                    {
                        if (String.IsNullOrWhiteSpace(testResult.ExecuteLoginname))
                        {
                            testResult.ExecuteLoginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                            testResult.ExecuteUsername = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                        }
                        HIS_SERVICE_REQ req = dvkts.FirstOrDefault(o => o.ID == testResult.ServiceReqId);
                        if (req != null)
                        {
                            List<HIS_SERE_SERV> sereServs = allSereServs.Where(o => o.SERVICE_REQ_ID == req.ID).ToList();
                            if (IsNotNullOrEmpty(testResult.TestIndexDatas) && (testResult.TestIndexDatas.Exists(t => !string.IsNullOrWhiteSpace(t.BacteriumCode))
                                || req.IS_ANTIBIOTIC_RESISTANCE == Constant.IS_TRUE))
                            {
                                req.IS_ANTIBIOTIC_RESISTANCE = Constant.IS_TRUE;
                                this.ProcessAntibioticResult(testResult, ssTeinUpdate, ssTeinCreate, req, sereServs);
                            }
                            //Neu ko ton tai thong tin "vi khuan" thi la xu ly nhu ket qua thuong
                            else if (IsNotNullOrEmpty(testResult.TestIndexDatas))
                            {
                                this.ProcessTestIndex(testResult, ssTeinUpdate, ssTeinCreate, req, sereServs);
                            }
                        }
                        if (req.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT && (!testResult.IsCancel.HasValue || !testResult.IsCancel.Value))
                        {
                            HIS_SERVICE_REQ serviceReqRaw = null;
                            req.ATTACHMENT_FILE_URL = testResult.AttachmentFileUrl;
                            req.IS_RESULTED = Constant.IS_TRUE;
                            req.SAMPLE_TIME = testResult.SampleTime;
                            req.FINISH_TIME = testResult.FinishTime;

                            V_HIS_EXECUTE_ROOM room = HisExecuteRoomCFG.DATA.Where(o => o.ROOM_ID == req.EXECUTE_ROOM_ID).FirstOrDefault();
                            bool? doNotFinish = false;
                            if (room != null)
                            {
                                doNotFinish = HisServiceReqCFG.DoNotFinishTestServiceReqWhenReceivingResult(room.BRANCH_ID);
                            }

                            //Neu co cau hinh ko cap nhat trang thai "Hoan thanh" cua xet nghiem thi chi cap nhat cac thong tin "da co ket qua"
                            if (doNotFinish.HasValue && doNotFinish.Value)
                            {
                                //tra ket qua xet nghiem ko can verify treatment
                                if (!new HisServiceReqUpdate().Update(req, false))
                                {
                                    LogSystem.Warn("Tu dong cap nhat trang thai His_service_req sang 'da co ket qua' that bai." + LogUtil.TraceData("hisServiceReq", req));
                                }
                            }
                            else
                            {
                                //tra ket qua xet nghiem ko can verify treatment
                                if (!new HisServiceReqUpdateFinish().Finish(req, false, ref serviceReqRaw, testResult.ExecuteLoginname, testResult.ExecuteUsername))
                                {
                                    LogSystem.Warn("Tu dong cap nhat trang thai His_service_req sang 'hoan thanh' that bai." + LogUtil.TraceData("hisServiceReq", req));
                                }
                            }
                        }
                        else if (req.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT && (testResult.IsCancel.HasValue && testResult.IsCancel.Value))
                        {
                            if (!new HisServiceReqUpdateUnfinish().Run(req.ID))
                            {
                                LogSystem.Warn("Tu dong cap nhat trang thai His_service_req tu 'hoan thanh' sang 'dang thuc hien' that bai." + LogUtil.TraceData("serviceReq", req));
                            }
                        }
                        else if (req.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                        {
                            HIS_SERVICE_REQ serviceReqRaw = null;
                            if (!new HisServiceReqUpdateStart().Start(req, false, ref serviceReqRaw, testResult.ExecuteLoginname, testResult.ExecuteUsername))
                            {
                                LogSystem.Warn("Tu dong cap nhat trang thai His_service_req tu 'y/c' sang 'dang thuc hien' that bai." + LogUtil.TraceData("serviceReq", req));
                            }
                        }

                        if (testResult.IsUpdateApprover && !DAOWorker.SqlDAO.Execute("UPDATE HIS_SERVICE_REQ SET RESULT_APPROVER_LOGINNAME = :param1, RESULT_APPROVER_USERNAME = :param2 WHERE ID = :param3", testResult.ApproverLoginname, testResult.ApproverUsername, req.ID))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisServiceReq_CapNhatThatBai);
                            throw new Exception("Cap nhat approver info cho HisServiceReq that bai");
                        }
                    }
                }
            }
        }

        private void ProcessTestIndex(HisTestResultTDO data, HisSereServTeinUpdate ssTeinUpdate, HisSereServTeinCreate ssTeinCreate, HIS_SERVICE_REQ serviceReq, List<HIS_SERE_SERV> sereServs)
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
                        if (ext != null && ext.SUBCLINICAL_RESULT_LOGINNAME != executeLoginname)
                        {
                            Mapper.CreateMap<HIS_SERE_SERV_EXT, HIS_SERE_SERV_EXT>();
                            HIS_SERE_SERV_EXT before = Mapper.Map<HIS_SERE_SERV_EXT>(ext);
                            ext.SUBCLINICAL_RESULT_LOGINNAME = executeLoginname;
                            ext.SUBCLINICAL_RESULT_USERNAME = executeUsername;
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
                                listExtCreate.Add(ext);
                            }
                        }
                    }
                }

                serviceReq.EXECUTE_LOGINNAME = firstExecuteLoginname;
                serviceReq.EXECUTE_USERNAME = firstExecuteUsername;
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
        }

        private List<V_HIS_SERE_SERV_TEIN> GetHisSereServTein(List<long> sereServIds, List<string> testIndexCodes)
        {
            HisSereServTeinViewFilterQuery sereServTeinViewFilter = new HisSereServTeinViewFilterQuery();
            sereServTeinViewFilter.SERE_SERV_IDs = sereServIds;
            sereServTeinViewFilter.TEST_INDEX_CODEs = testIndexCodes;
            return new HisSereServTeinGet().GetView(sereServTeinViewFilter);
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

        private void ProcessAntibioticResult(HisTestResultTDO data, HisSereServTeinUpdate ssTeinUpdate, HisSereServTeinCreate ssTeinCreate, HIS_SERVICE_REQ serviceReq, List<HIS_SERE_SERV> sereServs)
        {
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
        }

        private void UpdateExpMestBlood(HIS_EXP_MEST expMest, string loginname, string username, long approvalTime, ref List<HIS_EXP_MEST_BLOOD> expMestBloods)
        {
            List<HIS_EXP_MEST_BLOOD> bloods = new HisExpMestBloodGet().GetByExpMestId(expMest.ID);

            if (IsNotNullOrEmpty(bloods))
            {
                Mapper.CreateMap<HIS_EXP_MEST_BLOOD, HIS_EXP_MEST_BLOOD>();
                List<HIS_EXP_MEST_BLOOD> befores = Mapper.Map<List<HIS_EXP_MEST_BLOOD>>(bloods);
                bloods.ForEach(o =>
                {
                    o.APPROVAL_TIME = approvalTime;
                    o.APPROVAL_LOGINNAME = loginname;
                    o.APPROVAL_USERNAME = username;
                });
                if (!this.hisExpMestBloodUpdate.UpdateList(bloods, befores))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
                expMestBloods = bloods;
            }
        }

        internal void Rollback()
        {
            this.ssTeinUpdate.RollbackData();
            this.ssTeinCreate.RollbackData();
            this.ssExtCreate.RollbackData();
            this.ssExtUpdate.RollbackData();
            this.hisExpMestBloodUpdate.RollbackData();
            this.hisExpMestBloodMaker.Rollback();
            this.hisExpMestBltyReqIncreaseDdAmount.Rollback();
            this.blUpdate.RollbackData();
        }
    }
}

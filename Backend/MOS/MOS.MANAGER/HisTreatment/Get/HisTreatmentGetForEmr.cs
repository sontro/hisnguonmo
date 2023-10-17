using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Get
{
    class HisTreatmentGetForEmr : GetBase
    {
        internal HisTreatmentGetForEmr()
            : base()
        {

        }
        internal HisTreatmentGetForEmr(CommonParam param)
            : base(param)
        {

        }

        internal HisTreatmentForEmrSDO Run(HisTreatmentForEmrSDO data)
        {
            HisTreatmentForEmrSDO result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.TreatmentId);
                valid = valid && IsGreaterThanZero(data.PatientId);
                valid = valid && IsGreaterThanZero(data.IntructionTime);
                if (valid)
                {
                    result = new HisTreatmentForEmrSDO();

                    V_HIS_TREATMENT Treatment = null;
                    V_HIS_PATIENT Patient = null;
                    V_HIS_PATIENT_TYPE_ALTER PatientTypeAlter = null;
                    V_HIS_TREATMENT_BED_ROOM TreatmentBedRoom = null;
                    HIS_DHST Dhst = null;
                    V_HIS_BABY Baby = null;
                    List<V_HIS_DEPARTMENT_TRAN> DepartmentTrans = null;
                    List<V_HIS_SERVICE_REQ> ServiceReqs = null;
                    List<V_HIS_SERE_SERV_PTTT> SereServPttts = null;
                    List<HIS_SERE_SERV> SereServs = null;
                    HIS_DHST RecentDhst = null;
                    List<HIS_EXP_MEST_MEDICINE> ExpMestMedicines = null;
                    long TreatmentIcdCount = 0;

                    List<Task> taslAll = new List<Task>();
                    Task tsDataTreatment = Task.Factory.StartNew((object obj) =>
                    {
                        HisTreatmentForEmrSDO dt = obj as HisTreatmentForEmrSDO;
                        Treatment = DAOWorker.SqlDAO.GetSqlSingle<V_HIS_TREATMENT>("SELECT * FROM V_HIS_TREATMENT WHERE ID = :param1", dt.TreatmentId);
                        if (Treatment == null)
                        {
                            throw new Exception("TreatmentId invalid: " + dt.TreatmentId);
                        }

                        if (!String.IsNullOrWhiteSpace(Treatment.ICD_CODE))
                        {
                            TreatmentIcdCount = DAOWorker.SqlDAO.GetSqlSingle<long>("SELECT NVL(COUNT(*),0) FROM HIS_TREATMENT WHERE PATIENT_ID = :param1 AND IN_TIME <= :param2 AND ICD_CODE = :param3", Treatment.PATIENT_ID, Treatment.IN_TIME, Treatment.ICD_CODE);
                        }
                    }, data);
                    taslAll.Add(tsDataTreatment);

                    Task tsDataPatient = Task.Factory.StartNew((object obj) =>
                    {
                        HisTreatmentForEmrSDO dt = obj as HisTreatmentForEmrSDO;
                        Patient = DAOWorker.SqlDAO.GetSqlSingle<V_HIS_PATIENT>("SELECT * FROM V_HIS_PATIENT WHERE ID = :param1", dt.PatientId);
                        if (Patient == null)
                        {
                            throw new Exception("PatientId invalid: " + dt.PatientId);
                        }
                    }, data);
                    taslAll.Add(tsDataPatient);

                    Task tsDataPatientTypeAlter = Task.Factory.StartNew((object obj) =>
                    {
                        HisTreatmentForEmrSDO dt = obj as HisTreatmentForEmrSDO;
                        PatientTypeAlter = new HisPatientTypeAlterGet().GetViewApplied(dt.TreatmentId, dt.IntructionTime);
                    }, data);
                    taslAll.Add(tsDataPatientTypeAlter);

                    Task tsDataBaby = Task.Factory.StartNew((object obj) =>
                    {
                        HisTreatmentForEmrSDO dt = obj as HisTreatmentForEmrSDO;
                        Baby = DAOWorker.SqlDAO.GetSqlSingle<V_HIS_BABY>("SELECT * FROM V_HIS_BABY WHERE TREATMENT_ID = :param1 FETCH FIRST ROWS ONLY", dt.TreatmentId);
                    }, data);
                    taslAll.Add(tsDataBaby);

                    Task tsDataDepartmentTrans = Task.Factory.StartNew((object obj) =>
                    {
                        HisTreatmentForEmrSDO dt = obj as HisTreatmentForEmrSDO;
                        DepartmentTrans = DAOWorker.SqlDAO.GetSql<V_HIS_DEPARTMENT_TRAN>("SELECT * FROM V_HIS_DEPARTMENT_TRAN WHERE TREATMENT_ID = :param1 AND DEPARTMENT_IN_TIME IS NOT NULL", dt.TreatmentId);
                    }, data);
                    taslAll.Add(tsDataDepartmentTrans);

                    Task tsDataServiceReqs = Task.Factory.StartNew((object obj) =>
                    {
                        HisTreatmentForEmrSDO dt = obj as HisTreatmentForEmrSDO;
                        ServiceReqs = DAOWorker.SqlDAO.GetSql<V_HIS_SERVICE_REQ>("SELECT * FROM V_HIS_SERVICE_REQ WHERE TREATMENT_ID = :param1 AND (IS_DELETE IS NULL OR IS_DELETE <> 1)", dt.TreatmentId);

                        List<V_HIS_SERVICE_REQ> examReqs = ServiceReqs != null ? ServiceReqs.Where(o => o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH).ToList() : null;
                        if (IsNotNullOrEmpty(examReqs))
                        {
                            long dhstId = 0;
                            if (examReqs.Any(a => a.IS_MAIN_EXAM == Constant.IS_TRUE && a.DHST_ID.HasValue))
                            {
                                dhstId = examReqs.Where(o => o.IS_MAIN_EXAM == Constant.IS_TRUE && o.DHST_ID.HasValue).OrderBy(o => o.INTRUCTION_TIME).FirstOrDefault().DHST_ID.Value;
                            }
                            else if (examReqs.Any(a => a.DHST_ID.HasValue))
                            {
                                dhstId = examReqs.Where(o => o.DHST_ID.HasValue).OrderBy(o => o.INTRUCTION_TIME).FirstOrDefault().DHST_ID.Value;
                            }

                            if (dhstId > 0)
                                Dhst = DAOWorker.SqlDAO.GetSqlSingle<HIS_DHST>("SELECT * FROM HIS_DHST WHERE ID = :param1", dhstId);
                        }
                    }, data);
                    taslAll.Add(tsDataServiceReqs);

                    Task tsDataRecentDhst = Task.Factory.StartNew((object obj) =>
                    {
                        HisTreatmentForEmrSDO dt = obj as HisTreatmentForEmrSDO;
                        RecentDhst = DAOWorker.SqlDAO.GetSqlSingle<HIS_DHST>("SELECT * FROM HIS_DHST WHERE CARE_ID IS NOT NULL AND EXECUTE_TIME IS NOT NULL AND TREATMENT_ID = :param1 ORDER BY EXECUTE_TIME FETCH FIRST ROWS ONLY", dt.TreatmentId);
                    }, data);
                    taslAll.Add(tsDataRecentDhst);

                    Task tsDataSereServs = Task.Factory.StartNew((object obj) =>
                    {
                        HisTreatmentForEmrSDO dt = obj as HisTreatmentForEmrSDO;
                        SereServs = DAOWorker.SqlDAO.GetSql<HIS_SERE_SERV>("SELECT * FROM HIS_SERE_SERV WHERE TDL_TREATMENT_ID = :param1 AND (IS_DELETE IS NULL OR IS_DELETE <> 1) AND SERVICE_REQ_ID IS NOT NULL AND TDL_PATIENT_ID IS NOT NULL", dt.TreatmentId);
                    }, data);
                    taslAll.Add(tsDataSereServs);

                    Task tsDataSereServPttts = Task.Factory.StartNew((object obj) =>
                    {
                        HisTreatmentForEmrSDO dt = obj as HisTreatmentForEmrSDO;
                        SereServPttts = DAOWorker.SqlDAO.GetSql<V_HIS_SERE_SERV_PTTT>("SELECT * FROM V_HIS_SERE_SERV_PTTT WHERE TDL_TREATMENT_ID = :param1", dt.TreatmentId);
                    }, data);
                    taslAll.Add(tsDataSereServPttts);

                    Task tsDataExpMestMedicines = Task.Factory.StartNew((object obj) =>
                    {
                        HisTreatmentForEmrSDO dt = obj as HisTreatmentForEmrSDO;
                        ExpMestMedicines = DAOWorker.SqlDAO.GetSql<HIS_EXP_MEST_MEDICINE>("SELECT EMME.* FROM HIS_EXP_MEST_MEDICINE EMME JOIN HIS_EXP_MEST EXP ON EMME.EXP_MEST_ID = EXP.ID WHERE EXP.TDL_TREATMENT_ID = :param1 AND EXP.EXP_MEST_TYPE_ID IN (1,9,11) AND (EXP.IS_NOT_TAKEN IS NULL OR EXP.IS_NOT_TAKEN <> 1) AND (EMME.IS_DELETE IS NULL OR EMME.IS_DELETE <> 1)", dt.TreatmentId);
                    }, data);
                    taslAll.Add(tsDataExpMestMedicines);

                    Task tsDataTreatmentBedRoom = Task.Factory.StartNew((object obj) =>
                    {
                        HisTreatmentForEmrSDO dt = obj as HisTreatmentForEmrSDO;
                        List<V_HIS_TREATMENT_BED_ROOM> lstTreatmentBedRoom = DAOWorker.SqlDAO.GetSql<V_HIS_TREATMENT_BED_ROOM>("SELECT * FROM V_HIS_TREATMENT_BED_ROOM WHERE TREATMENT_ID = :param1 ORDER BY ADD_TIME DESC", dt.TreatmentId);

                        if (IsNotNullOrEmpty(lstTreatmentBedRoom))
                        {
                            if (dt.WorkingDepartmentId.HasValue)
                            {
                                lstTreatmentBedRoom = lstTreatmentBedRoom.Where(o => HisBedRoomCFG.DATA.Any(a => a.ID == o.BED_ROOM_ID && a.DEPARTMENT_ID == dt.WorkingDepartmentId.Value)).ToList();
                            }
                            TreatmentBedRoom = lstTreatmentBedRoom.FirstOrDefault();
                        }
                    }, data);
                    taslAll.Add(tsDataTreatmentBedRoom);

                    Task.WaitAll(taslAll.ToArray());

                    result.Treatment = Treatment;
                    result.Patient = Patient;
                    result.PatientTypeAlter = PatientTypeAlter;
                    result.TreatmentBedRoom = TreatmentBedRoom;
                    result.Baby = Baby;
                    result.DepartmentTrans = DepartmentTrans;
                    result.ServiceReqs = ServiceReqs;
                    result.Dhst = Dhst;
                    result.RecentDhst = RecentDhst;
                    result.TreatmentIcdCount = TreatmentIcdCount;
                    result.SereServs = SereServs;
                    result.SereServPttts = SereServPttts;
                    result.ExpMestMedicines = ExpMestMedicines;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                param.HasException = true;
            }
            return result;
        }
    }
}

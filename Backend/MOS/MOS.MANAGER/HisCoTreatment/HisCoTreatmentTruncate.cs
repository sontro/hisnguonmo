using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBedLog;
using MOS.MANAGER.HisRestRetrType;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatment.Util;
using MOS.MANAGER.HisTreatmentBedRoom;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCoTreatment
{
    partial class HisCoTreatmentTruncate : BusinessBase
    {

        private HisTreatmentUpdate hisTreatmentUpdate;

        internal HisCoTreatmentTruncate()
            : base()
        {
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal HisCoTreatmentTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCoTreatmentCheck checker = new HisCoTreatmentCheck(param);
                valid = valid && IsGreaterThanZero(id);
                HIS_CO_TREATMENT raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(raw.ID);
                if (valid)
                {
                    //Neu khoa da co chi dinh thi khong cho phep xoa
                    if (raw.START_TIME.HasValue)
                    {
                        HisServiceReqFilterQuery serviceReqFilter = new HisServiceReqFilterQuery();
                        serviceReqFilter.REQUEST_DEPARTMENT_ID = raw.DEPARTMENT_ID;
                        serviceReqFilter.TREATMENT_ID = raw.TDL_TREATMENT_ID;
                        serviceReqFilter.HAS_EXECUTE = true;
                        serviceReqFilter.INTRUCTION_TIME_FROM = raw.START_TIME.Value;
                        serviceReqFilter.INTRUCTION_TIME_TO = raw.FINISH_TIME;
                        List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().Get(serviceReqFilter);
                        if (IsNotNullOrEmpty(serviceReqs))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisDepartmentTran_KhongChoPhepXoaHoSoDaCoDichVuDoKhoaChiDinh);
                            return false;
                        }
                    }

                    List<string> sqls = new List<string>();

                    List<HIS_TREATMENT_BED_ROOM> treatmentBedRooms = new HisTreatmentBedRoomGet().GetByCoTreatmentId(raw.ID);
                    if (IsNotNullOrEmpty(treatmentBedRooms))
                    {
                        HisTreatmentBedRoomCheck treatBedChecker = new HisTreatmentBedRoomCheck(param);

                        valid = valid && IsNotNullOrEmpty(treatmentBedRooms);
                        foreach (HIS_TREATMENT_BED_ROOM data in treatmentBedRooms)
                        {
                            valid = valid && treatBedChecker.IsUnLock(data);
                        }
                        List<HIS_BED_LOG> bedLogs = new HisBedLogGet().GetByTreatmentBedRoomIds(treatmentBedRooms.Select(s => s.ID).ToList());

                        if (!valid)
                        {
                            throw new Exception("HisTreatmentBedRoom dang bi tam khoa");
                        }

                        if (IsNotNullOrEmpty(bedLogs))
                        {
                            HisBedLogCheck bedLogChecker = new HisBedLogCheck(param);
                            foreach (var data in bedLogs)
                            {
                                valid = valid && bedLogChecker.IsUnLock(data);
                            }
                            if (!valid)
                            {
                                throw new Exception("HisBedLog dang bi tam khoa");
                            }
                            string deleteBedLog = DAOWorker.SqlDAO.AddInClause(bedLogs.Select(s => s.ID).ToList(), "DELETE HIS_BED_LOG WHERE %IN_CLAUSE%", "ID");
                            sqls.Add(deleteBedLog);
                        }

                        string deleteBedRoom = DAOWorker.SqlDAO.AddInClause(treatmentBedRooms.Select(s => s.ID).ToList(), "DELETE HIS_TREATMENT_BED_ROOM WHERE %IN_CLAUSE%", "ID");
                        sqls.Add(deleteBedRoom);
                    }
                    sqls.Add(String.Format("DELETE HIS_CO_TREATMENT WHERE ID = {0}", raw.ID));
                    result = DAOWorker.SqlDAO.Execute(sqls);

                    if (result) this.ProcessTreatment(raw);
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

        internal bool TruncateList(List<HIS_CO_TREATMENT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCoTreatmentCheck checker = new HisCoTreatmentCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisCoTreatmentDAO.TruncateList(listData);
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


        private void ProcessTreatment(HIS_CO_TREATMENT raw)
        {
            try
            {
                HIS_TREATMENT treatment = new HisTreatmentGet().GetById(raw.TDL_TREATMENT_ID);
                if (treatment != null)
                {
                    List<HIS_CO_TREATMENT> cts = new HisCoTreatmentGet().GetByTreatmentId(raw.TDL_TREATMENT_ID);
                    Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                    HIS_TREATMENT beforeUpdate = Mapper.Map<HIS_TREATMENT>(treatment);

                    HisTreatmentUtil.SetCoDepartmentInfo(treatment, cts);

                    if (ValueChecker.IsPrimitiveDiff<HIS_TREATMENT>(beforeUpdate, treatment)
                        && !this.hisTreatmentUpdate.Update(treatment, beforeUpdate))
                    {
                        LogSystem.Warn("Cap nhat thong tin CO_DEPARTMENT_IDS cho bang Treatment that bai. Rollback du lieu. TreatmentId: " + treatment.ID);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}

using AutoMapper;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBedLog;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatment.Util;
using MOS.MANAGER.HisTreatmentBedRoom;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisCoTreatment.Receive
{
    class HisCoTreatmentReceive : BusinessBase
    {
        private HIS_CO_TREATMENT recentCoTreatment = null;
        private HIS_TREATMENT_BED_ROOM recentTreatmentBedRoom = null;

        private HisCoTreatmentUpdate hisCoTreatmentUpdate;
        private HisTreatmentBedRoomCreate hisTreatmentBedRoomCreate;
        private HisBedLogCreate hisBedLogCreate;
        private HisTreatmentUpdate hisTreatmentUpdate;

        internal HisCoTreatmentReceive()
            : base()
        {
            this.Init();
        }

        internal HisCoTreatmentReceive(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisCoTreatmentUpdate = new HisCoTreatmentUpdate(param);
            this.hisTreatmentBedRoomCreate = new HisTreatmentBedRoomCreate(param);
            this.hisBedLogCreate = new HisBedLogCreate(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal bool Receive(HisCoTreatmentReceiveSDO data, ref HIS_CO_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                HIS_CO_TREATMENT coTreat = null;
                WorkPlaceSDO workPlace = null;
                HisCoTreatmentCheck checker = new HisCoTreatmentCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisCoTreatmentReceiveCheck receiveChecker = new HisCoTreatmentReceiveCheck(param);
                HIS_TREATMENT treatment = null;

                bool valid = true;
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && checker.VerifyId(data.Id, ref coTreat);
                valid = valid && treatmentChecker.VerifyId(coTreat.TDL_TREATMENT_ID, ref treatment);
                valid = valid && receiveChecker.IsAllow(coTreat, data, workPlace);

                if (valid)
                {
                    this.ProcessCoTreatment(coTreat, data);
                    this.ProcessTreatmentBedRoom(data);
                    this.ProcessBedLog(data, treatment);
                    this.ProcessTreatment(treatment);
                    resultData = this.recentCoTreatment;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
            }
            return result;
        }

        private void ProcessCoTreatment(HIS_CO_TREATMENT coTreat, HisCoTreatmentReceiveSDO data)
        {
            Mapper.CreateMap<HIS_CO_TREATMENT, HIS_CO_TREATMENT>();
            HIS_CO_TREATMENT update = Mapper.Map<HIS_CO_TREATMENT>(coTreat);
            update.START_TIME = data.StartTime;
            if (!this.hisCoTreatmentUpdate.Update(update, coTreat))
            {
                throw new Exception("Cap nhat HisCoTreatment that bai. ket thuc nghiep vu");
            }
            this.recentCoTreatment = update;
        }

        private void ProcessTreatmentBedRoom(HisCoTreatmentReceiveSDO data)
        {
            HIS_TREATMENT_BED_ROOM treatmentBedRoom = new HIS_TREATMENT_BED_ROOM();
            treatmentBedRoom.BED_ROOM_ID = data.BedRoomId;
            treatmentBedRoom.TREATMENT_ID = this.recentCoTreatment.TDL_TREATMENT_ID;
            treatmentBedRoom.CO_TREATMENT_ID = this.recentCoTreatment.ID;
            treatmentBedRoom.ADD_TIME = data.StartTime;
            treatmentBedRoom.BED_ID = data.BedId;
            if (!hisTreatmentBedRoomCreate.Create(treatmentBedRoom))
            {
                throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
            }
            this.recentTreatmentBedRoom = treatmentBedRoom;
        }

        private void ProcessBedLog(HisCoTreatmentReceiveSDO data, HIS_TREATMENT treatment)
        {
            if (data.BedId.HasValue && this.recentTreatmentBedRoom != null)
            {
                HIS_BED_LOG bedLog = new HIS_BED_LOG();
                bedLog.BED_ID = data.BedId.Value;
                bedLog.BED_SERVICE_TYPE_ID = data.BedServiceId;
                bedLog.TREATMENT_BED_ROOM_ID = this.recentTreatmentBedRoom.ID;
                bedLog.START_TIME = data.StartTime;
                if (!this.hisBedLogCreate.Create(bedLog, treatment, data.RequestRoomId))
                {
                    throw new Exception("Tao thong tin giuong that bai. Ket thuc nghiep vu Rollback du lieu");
                }
            }
        }

        private void ProcessTreatment(HIS_TREATMENT treatment)
        {
            if (treatment != null)
            {
                Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                HIS_TREATMENT beforeUpdate = Mapper.Map<HIS_TREATMENT>(treatment);

                List<HIS_CO_TREATMENT> allCoTreats = new HisCoTreatmentGet().GetByTreatmentId(treatment.ID);
                HisTreatmentUtil.SetCoDepartmentInfo(treatment, allCoTreats);

                if (ValueChecker.IsPrimitiveDiff<HIS_TREATMENT>(beforeUpdate, treatment)
                    && !this.hisTreatmentUpdate.Update(treatment, beforeUpdate))
                {
                    throw new Exception("Cap nhat thong tin CO_DEPARTMENT_IDS cho bang Treatment that bai. Rollback du lieu");
                }
            }
        }

        internal void RollbackData()
        {
            try
            {
                this.hisTreatmentUpdate.RollbackData();
                this.hisBedLogCreate.RollbackData();
                this.hisTreatmentBedRoomCreate.RollbackData();
                this.hisCoTreatmentUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}

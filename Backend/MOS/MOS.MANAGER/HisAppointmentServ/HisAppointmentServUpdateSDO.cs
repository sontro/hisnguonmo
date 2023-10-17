using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisAppointmentServ
{
    class HisAppointmentServUpdateSDO : BusinessBase
    {

        private HisAppointmentServCreate hisAppointmentServCreate;
        private HisAppointmentServUpdate hisAppointmentServUpdate;
        private HisAppointmentServTruncate hisAppointmentServTruncate;

        internal HisAppointmentServUpdateSDO()
            : base()
        {
            this.Init();
        }

        internal HisAppointmentServUpdateSDO(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisAppointmentServCreate = new HisAppointmentServCreate(param);
            this.hisAppointmentServTruncate = new HisAppointmentServTruncate(param);
            this.hisAppointmentServUpdate = new HisAppointmentServUpdate(param);
        }

        internal bool Run(HisAppointmentServSDO data, ref List<HIS_APPOINTMENT_SERV> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                
                valid = valid && IsNotNull(data);
                //valid = valid && IsNotNullOrEmpty(data.AppointmentServs);
                HisAppointmentServCheck checker = new HisAppointmentServCheck(param);
                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
                HIS_TREATMENT treatment = null;
                valid = valid && treatChecker.VerifyId(data.TreatmentId, ref treatment);
                //Ko can ho so phai ket thuc (thuc te, voi BN noi tru, vien co nhu cau chi dinh hen kham truoc khi ra vien)
                //valid = valid && treatChecker.IsPause(treatment);
                valid = valid && treatChecker.IsUnLock(treatment);
                //valid = valid && treatChecker.IsTreatmentEndTypeAppointment(treatment);
                if (valid)
                {
                    List<HIS_APPOINTMENT_SERV> inserts = new List<HIS_APPOINTMENT_SERV>();
                    List<HIS_APPOINTMENT_SERV> updates = new List<HIS_APPOINTMENT_SERV>();
                    List<HIS_APPOINTMENT_SERV> deletes = new List<HIS_APPOINTMENT_SERV>();

                    List<HIS_APPOINTMENT_SERV> beforeUpdates = new List<HIS_APPOINTMENT_SERV>();

                    List<HIS_APPOINTMENT_SERV> olds = new HisAppointmentServGet().GetByTreatmentId(data.TreatmentId);
                    if (IsNotNullOrEmpty(data.AppointmentServs))
                    {
                        data.AppointmentServs.ForEach(o =>
                        {
                            o.TREATMENT_ID = treatment.ID;
                            o.TDL_PATIENT_ID = treatment.PATIENT_ID;
                            o.TDL_APPOINTMENT_TIME = treatment.APPOINTMENT_TIME;
                        }
                        );

                        Mapper.CreateMap<HIS_APPOINTMENT_SERV, HIS_APPOINTMENT_SERV>();
                        foreach (var item in data.AppointmentServs)
                        {
                            valid = valid && checker.VerifyRequireField(item);
                            if (valid)
                            {
                                HIS_APPOINTMENT_SERV apps = olds != null ? olds.FirstOrDefault(o => o.SERVICE_ID == item.SERVICE_ID) : null;
                                if (apps == null)
                                {
                                    inserts.Add(item);
                                }
                                else
                                {
                                    beforeUpdates.Add(Mapper.Map<HIS_APPOINTMENT_SERV>(apps));
                                    apps.AMOUNT = item.AMOUNT;
                                    apps.PATIENT_TYPE_ID = item.PATIENT_TYPE_ID;
                                    updates.Add(apps);
                                }
                            }
                        }
                    }

                    deletes = olds != null ? olds.Where(o => !updates.Exists(e => e.ID == o.ID)).ToList() : null;
                    if (valid)
                    {
                        if (IsNotNullOrEmpty(inserts))
                        {
                            if (!this.hisAppointmentServCreate.CreateList(inserts))
                            {
                                throw new Exception("hisAppointmentServCreate. Ket thuc nghiep vu");
                            }
                        }

                        if (IsNotNullOrEmpty(updates))
                        {
                            if (!this.hisAppointmentServUpdate.UpdateList(updates, beforeUpdates))
                            {
                                throw new Exception("hisAppointmentServUpdate. Ket thuc nghiep vu. Rollback du lieu");
                            }
                        }

                        if (IsNotNullOrEmpty(deletes))
                        {
                            if (!this.hisAppointmentServTruncate.TruncateList(deletes))
                            {
                                throw new Exception("hisAppointmentServTruncate. Ket thuc nghiep vu. Rollback du lieu");
                            }
                        }
                        resultData = new List<HIS_APPOINTMENT_SERV>();
                        if (IsNotNullOrEmpty(inserts))
                        {
                            resultData.AddRange(inserts);
                        }
                        if (IsNotNullOrEmpty(updates))
                        {
                            resultData.AddRange(updates);
                        }
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                result = false;
            }
            return result;
        }

        private void Rollback()
        {
            try
            {
                this.hisAppointmentServUpdate.RollbackData();
                this.hisAppointmentServCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}

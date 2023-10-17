using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisDhst;
using MOS.MANAGER.HisKskGeneral;
using MOS.MANAGER.HisKskOccupational;
using MOS.MANAGER.HisServiceReq.Update.Finish;
using MOS.MANAGER.HisServiceReq.Update.Status;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Ksk.KskExecute
{
    public class HisServiceReqKskExecute : BusinessBase
    {
        private HisServiceReqUpdateFinish hisServiceReqUpdateFinish;
        private HisServiceReqUpdate hisServiceReqUpdate;
        private HisDhstCreate hisDhstCreate;
        private HisDhstUpdate hisDhstUpdate;
        private HisKskGeneralUpdate hisKskGeneralUpdate;
        private HisKskGeneralCreate hisKskGeneralCreate;
        private HisKskOccupationalUpdate hisKskOccupationalUpdate;
        private HisKskOccupationalCreate hisKskOccupationalCreate;
        private HisServiceReqUpdateStart hisServiceReqUpdateStart;

        internal HisServiceReqKskExecute()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqKskExecute(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqUpdateFinish = new HisServiceReqUpdateFinish(param);
            this.hisServiceReqUpdateStart = new HisServiceReqUpdateStart(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.hisDhstCreate = new HisDhstCreate(param);
            this.hisDhstUpdate = new HisDhstUpdate(param);
            this.hisKskGeneralUpdate = new HisKskGeneralUpdate(param);
            this.hisKskGeneralCreate = new HisKskGeneralCreate(param);
            this.hisKskOccupationalUpdate = new HisKskOccupationalUpdate(param);
            this.hisKskOccupationalCreate = new HisKskOccupationalCreate(param);
        }

        public bool Run(HisServiceReqKskExecuteSDO data, ref KskExecuteResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_SERVICE_REQ serviceReq = null;
                WorkPlaceSDO workPlace = null;

                HisServiceReqKskExecuteCheck checker = new HisServiceReqKskExecuteCheck(param);
                HisServiceReqCheck reqCheck = new HisServiceReqCheck(param);

                valid = valid && checker.VerifyRequireField(data);
                valid = valid && reqCheck.VerifyId(data.ServiceReqId, ref serviceReq);
                valid = valid && checker.VerifyServiceReq(serviceReq);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                if (valid)
                {
                    HIS_KSK_GENERAL kskGeneral = null;
                    HIS_KSK_OCCUPATIONAL kskOccupational = null;

                    if (serviceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                    {
                        HIS_SERVICE_REQ serviceReqRaw = null;
                        hisServiceReqUpdateStart.Start(serviceReq, false, ref serviceReqRaw, null, null);
                    }

                    ProcessServiceReq(data, serviceReq);

                    ProcessKskGeneral(data, serviceReq, workPlace, ref kskGeneral);

                    ProcessKskOccupational(data, serviceReq, workPlace, ref kskOccupational);

                    if (data.isFinish && serviceReq.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                    {
                        HIS_SERVICE_REQ serviceReqRaw = null;
                        hisServiceReqUpdateFinish.Finish(serviceReq, false, ref serviceReqRaw, null, null);
                    }

                    resultData = new KskExecuteResultSDO();
                    resultData.HisKskGeneral = kskGeneral;
                    resultData.HisKskOccupational = kskOccupational;
                    resultData.HisServiceReq = new HisServiceReqGet().GetViewById(serviceReq.ID);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void ProcessKskOccupational(HisServiceReqKskExecuteSDO data, HIS_SERVICE_REQ serviceReq, WorkPlaceSDO workPlace, ref HIS_KSK_OCCUPATIONAL kskOccupational)
        {
            if (IsNotNull(data.KskOccupational))
            {
                this.ProcessDhst(data.KskOccupational.HisDhst, serviceReq.TREATMENT_ID, workPlace);

                List<HIS_KSK_OCCUPATIONAL> KskOccupationals = new HisKskOccupationalGet().GetByServiceReqId(serviceReq.ID);
                if (IsNotNullOrEmpty(KskOccupationals))
                {
                    HIS_KSK_OCCUPATIONAL update = KskOccupationals.FirstOrDefault();
                    ProcessKskOccupationalData(data.KskOccupational, ref update);
                    if (data.KskOccupational.HisDhst != null)
                    {
                        update.DHST_ID = data.KskOccupational.HisDhst.ID;
                    }

                    if (!this.hisKskOccupationalUpdate.Update(update))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }

                    kskOccupational = update;
                }
                else
                {
                    HIS_KSK_OCCUPATIONAL create = new HIS_KSK_OCCUPATIONAL();
                    create.SERVICE_REQ_ID = serviceReq.ID;
                    ProcessKskOccupationalData(data.KskOccupational, ref create);
                    if (data.KskOccupational.HisDhst != null)
                    {
                        create.DHST_ID = data.KskOccupational.HisDhst.ID;
                    }

                    if (!this.hisKskOccupationalCreate.Create(create))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }

                    kskOccupational = create;
                }
            }
        }

        private void ProcessKskOccupationalData(HisKskOccupationalSDO data, ref HIS_KSK_OCCUPATIONAL update)
        {
            if (data != null)
            {
                update.CONCLUSION = data.Conclusion;
                update.DISEASES = data.Diseases;
                update.EXAM_CAPILLARY = data.ExamCapillary;
                update.EXAM_CAPILLARY_RANK = data.ExamCapillaryRank;
                update.EXAM_CARDIOVASCULAR = data.ExamCardiovascular;
                update.EXAM_CARDIOVASCULAR_RANK = data.ExamCardiovascularRank;
                update.EXAM_CIRCULATION = data.ExamCirculation;
                update.EXAM_CIRCULATION_RANK = data.ExamCirculationRank;
                update.EXAM_DERMATOLOGY = data.ExamDermatology;
                update.EXAM_DERMATOLOGY_RANK = data.ExamDermatologyRank;
                update.EXAM_DIGESTION = data.ExamDigestion;
                update.EXAM_DIGESTION_RANK = data.ExamDigestionRank;
                update.EXAM_ENT = data.ExamEnt;
                update.EXAM_ENT_RANK = data.ExamEntRank;
                update.EXAM_EYE = data.ExamEye;
                update.EXAM_EYE_RANK = data.ExamEyeRank;
                update.EXAM_HEMATOPOIETIC = data.ExamHematopoietic;
                update.EXAM_HEMATOPOIETIC_RANK = data.ExamHematopoieticRank;
                update.EXAM_KIDNEY_UROLOGY = data.ExamKidneyUrology;
                update.EXAM_KIDNEY_UROLOGY_RANK = data.ExamKidneyUrologyRank;
                update.EXAM_LYMPH_NODES = data.ExamLymphNodes;
                update.EXAM_LYMPH_NODES_RANK = data.ExamLymphNodesRank;
                update.EXAM_MENTAL = data.ExamMental;
                update.EXAM_MENTAL_RANK = data.ExamMentalRank;
                update.EXAM_MOTION = data.ExamMotion;
                update.EXAM_MOTION_RANK = data.ExamMotionRank;
                update.EXAM_MUCOSA = data.ExamMucosa;
                update.EXAM_MUCOSA_RANK = data.ExamMucosaRank;
                update.EXAM_MUSCLE_BONE = data.ExamMuscleBone;
                update.EXAM_MUSCLE_BONE_RANK = data.ExamMuscleBoneRank;
                update.EXAM_NAIL = data.ExamNail;
                update.EXAM_NAIL_RANK = data.ExamNailRank;
                update.EXAM_NEUROLOGICAL = data.ExamNeurological;
                update.EXAM_NEUROLOGICAL_RANK = data.ExamNeurologicalRank;
                update.EXAM_OEND = data.ExamOend;
                update.EXAM_OEND_RANK = data.ExamOendRank;
                update.EXAM_RESPIRATORY = data.ExamRespiratory;
                update.EXAM_RESPIRATORY_RANK = data.ExamRespiratoryRank;
                update.EXAM_STOMATOLOGY = data.ExamStomatology;
                update.EXAM_STOMATOLOGY_RANK = data.ExamStomatologyRank;
                update.EXAM_SURGERY = data.ExamSurgery;
                update.EXAM_SURGERY_RANK = data.ExamSurgeryRank;
                update.HEALTH_EXAM_RANK_ID = data.HealthExamRankId;
                update.TREATMENT_INSTRUCTION = data.TreatmentInstruction;
            }
        }

        private void ProcessKskGeneral(HisServiceReqKskExecuteSDO data, HIS_SERVICE_REQ serviceReq, WorkPlaceSDO workPlace, ref HIS_KSK_GENERAL kskGeneral)
        {
            if (IsNotNull(data.KskGeneral))
            {
                this.ProcessDhst(data.KskGeneral.HisDhst, serviceReq.TREATMENT_ID, workPlace);

                List<HIS_KSK_GENERAL> kskGenerals = new HisKskGeneralGet().GetByServiceReqId(serviceReq.ID);
                if (IsNotNullOrEmpty(kskGenerals))
                {
                    HIS_KSK_GENERAL update = kskGenerals.FirstOrDefault();
                    ProcessKskGeneralData(data.KskGeneral, ref update);
                    if (data.KskGeneral.HisDhst != null)
                    {
                        update.DHST_ID = data.KskGeneral.HisDhst.ID;
                    }

                    if (!this.hisKskGeneralUpdate.Update(update))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }

                    kskGeneral = update;
                }
                else
                {
                    HIS_KSK_GENERAL create = new HIS_KSK_GENERAL();
                    create.SERVICE_REQ_ID = serviceReq.ID;
                    ProcessKskGeneralData(data.KskGeneral, ref create);
                    if (data.KskGeneral.HisDhst != null)
                    {
                        create.DHST_ID = data.KskGeneral.HisDhst.ID;
                    }

                    if (!this.hisKskGeneralCreate.Create(create))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }

                    kskGeneral = create;
                }
            }
        }

        private void ProcessKskGeneralData(HisKskGeneralSDO data, ref HIS_KSK_GENERAL update)
        {
            if (data != null)
            {
                update.DISEASES = data.Diseases;
                update.EXAM_CIRCULATION = data.ExamCirculation;
                update.EXAM_CIRCULATION_RANK = data.ExamCirculationRank;
                update.EXAM_DERMATOLOGY = data.ExamDermatology;
                update.EXAM_DERMATOLOGY_RANK = data.ExamDermatologyRank;
                update.EXAM_DIGESTION = data.ExamDigestion;
                update.EXAM_DIGESTION_RANK = data.ExamDigestionRank;
                update.EXAM_ENT = data.ExamEnt;
                update.EXAM_ENT_RANK = data.ExamEntRank;
                update.EXAM_EYE = data.ExamEye;
                update.EXAM_EYE_RANK = data.ExamEyeRank;
                update.EXAM_KIDNEY_UROLOGY = data.ExamKidneyUrology;
                update.EXAM_KIDNEY_UROLOGY_RANK = data.ExamKidneyUrologyRank;
                update.EXAM_MENTAL = data.ExamMental;
                update.EXAM_MENTAL_RANK = data.ExamMentalRank;
                update.EXAM_MUSCLE_BONE = data.ExamMuscleBone;
                update.EXAM_MUSCLE_BONE_RANK = data.ExamMuscleBoneRank;
                update.EXAM_NEUROLOGICAL = data.ExamNeurological;
                update.EXAM_NEUROLOGICAL_RANK = data.ExamNeurologicalRank;
                update.EXAM_OEND = data.ExamOend;
                update.EXAM_OEND_RANK = data.ExamOendRank;
                update.EXAM_RESPIRATORY = data.ExamRespiratory;
                update.EXAM_RESPIRATORY_RANK = data.ExamRespiratoryRank;
                update.EXAM_STOMATOLOGY = data.ExamStomatology;
                update.EXAM_STOMATOLOGY_RANK = data.ExamStomatologyRank;
                update.EXAM_SURGERY = data.ExamSurgery;
                update.EXAM_SURGERY_RANK = data.ExamSurgeryRank;

                update.EXAM_OBSTETRIC = data.ExamObstetric;
                update.EXAM_OBSTETRIC_RANK = data.ExamObstetricRank;
                update.EXAM_OCCUPATIONAL_THERAPY = data.ExamOccupationalTherapy;
                update.EXAM_OCCUPATIONAL_THERAPY_RANK = data.ExamOccupationalTherapyRank;
                update.EXAM_TRADITIONAL = data.ExamTraditional;
                update.EXAM_TRADITIONAL_RANK = data.ExamTraditionalRank;
                update.EXAM_NUTRION = data.ExamNutrion;
                update.EXAM_NUTRION_RANK = data.ExamNutrionRank;

                update.HEALTH_EXAM_RANK_ID = data.HealthExamRankId;
                update.NOTE_BIOCHEMICAL = data.NoteBiochemical;
                update.NOTE_BLOOD = data.NoteBlood;
                update.NOTE_PROSTASE = data.NoteProstase;
                update.NOTE_SUPERSONIC = data.NoteSupersonic;
                update.NOTE_XRAY = data.NoteXray;
                update.TREATMENT_INSTRUCTION = data.TreatmentInstruction;
                update.NOTE_DIIM = data.NoteDiim;
                update.NOTE_TEST_URINE = data.NoteTestUrine;
                update.NOTE_TEST_OTHER = data.NoteTestOther;

                update.CONCLUSION_TIME = data.ConclusionTime;
                update.CONCLUDER_LOGINNAME = data.ConcluderLoginName;
                update.CONCLUDER_USERNAME = data.ConcluderUserName;
                update.HEIN_MEDI_ORG_CODE = data.HeinMediOrgCode;
            }
        }

        private void ProcessServiceReq(HisServiceReqKskExecuteSDO data, HIS_SERVICE_REQ serviceReq)
        {
            serviceReq.CONCLUSION_CLINICAL = data.ConclusionClinical;
            serviceReq.CONCLUSION_SUBCLINICAL = data.ConclusionSubclinical;
            serviceReq.OCCUPATIONAL_DISEASE = data.OccupationalDisease;
            serviceReq.CONCLUSION_CONSULTATION = data.ConclusionConsultation;
            serviceReq.PROVISIONAL_DIAGNOSIS = data.ProvisionalDiagnosis;
            serviceReq.EXAM_CONCLUSION = data.ExamConclusion;
            serviceReq.CONCLUSION = data.Conclusion;

            if (!this.hisServiceReqUpdate.Update(serviceReq, false))
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReq_CapNhatThatBai);
                throw new Exception("Cap nhat thong tin HisServiceReq that bai." + LogUtil.TraceData("data", data));
            }
        }

        private void ProcessDhst(HIS_DHST data, long treatmentId, WorkPlaceSDO workPlace)
        {
            if (data != null)
            {
                data.TREATMENT_ID = treatmentId;
                data.EXECUTE_ROOM_ID = workPlace.RoomId;
                data.EXECUTE_DEPARTMENT_ID = workPlace.DepartmentId;
                data.EXECUTE_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                data.EXECUTE_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();

                if (data.ID > 0)
                {
                    if (!this.hisDhstUpdate.Update(data))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }
                }
                else
                {
                    if (!this.hisDhstCreate.Create(data))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }
                }
            }
        }

        private void Rollback()
        {
            try
            {
                hisServiceReqUpdateFinish.Rollback();
                hisServiceReqUpdate.RollbackData();
                hisDhstCreate.RollbackData();
                hisDhstUpdate.RollbackData();
                hisKskGeneralUpdate.RollbackData();
                hisKskGeneralCreate.RollbackData();
                hisKskOccupationalUpdate.RollbackData();
                hisKskOccupationalCreate.RollbackData();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}

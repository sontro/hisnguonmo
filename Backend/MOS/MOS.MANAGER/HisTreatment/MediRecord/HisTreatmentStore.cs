using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMediRecord;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.CodeGenerator.HisTreatment;
using MOS.MANAGER.HisTreatment.Util;
using MOS.SDO;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.MediRecord
{
    class HisTreatmentStore : BusinessBase
    {
        private HisMediRecordCreate hisMediRecordCreate;

        private List<HIS_TREATMENT> recentTreatments = null;

        internal HisTreatmentStore()
            : base()
        {
            this.Init();
        }

        internal HisTreatmentStore(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisMediRecordCreate = new HisMediRecordCreate(param);
        }

        internal bool Run(HisTreatmentStoreSDO data, ref List<HIS_TREATMENT> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_TREATMENT> listRaw = new List<HIS_TREATMENT>();
                Dictionary<HIS_TREATMENT, HIS_MEDI_RECORD> dicMediRecord = new Dictionary<HIS_TREATMENT, HIS_MEDI_RECORD>();
                HisTreatmentStoreCheck checker = new HisTreatmentStoreCheck(param);
                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
                valid = valid && checker.VerifyData(data);
                valid = valid && treatChecker.VerifyIds(data.TreatmentIds, listRaw);
                valid = valid && treatChecker.HasNoDataStoreId(listRaw);
                valid = valid && checker.VerifyIsOutPatient(data, listRaw);
                valid = valid && checker.IsNotExistStoreCode(data, listRaw);
                valid = valid && checker.IsTreatmentDidNotPassTheChecklist(listRaw);
                if (valid)
                {
                    this.ProcessMediRecord(data, listRaw, ref dicMediRecord);
                    this.ProcessTreatment(data, listRaw, dicMediRecord);
                    this.ProcessSyncEmr(listRaw);
                    result = true;
                    resultData = listRaw;

                    // Ghi nhat ky tac dong
                    this.ProcessEventLog(listRaw);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
                this.Rollback();
            }
            return result;
        }

        private void ProcessEventLog(List<HIS_TREATMENT> treatments)
        {
            if (IsNotNullOrEmpty(treatments))
            {
                foreach (var treatment in treatments)
                {
                    new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisTreatment_TaoVaLuuBenhAn).TreatmentCode(treatment.TREATMENT_CODE).PatientCode(treatment.TDL_PATIENT_CODE).Run();
                }
            }
        }

        private void ProcessSyncEmr(List<HIS_TREATMENT> treatments)
        {
            HisTreatmentUploadEmr syncEmr = new HisTreatmentUploadEmr();
            syncEmr.Run(treatments, true);
        }

        private void ProcessMediRecord(HisTreatmentStoreSDO data, List<HIS_TREATMENT> treatments, ref Dictionary<HIS_TREATMENT, HIS_MEDI_RECORD> dicMediRecord)
        {

            List<HIS_MEDI_RECORD> createds = new List<HIS_MEDI_RECORD>();
            List<string> storeCodes = new List<string>();

            //Neu bat cau hinh "giữ 'mã lưu trữ'" thì lấy lại các thông tin mã lưu trữ đã lưu trong treament để tránh bị cấp trùng
            if (HisTreatmentCFG.IS_KEEPING_STORE_CODE)
            {
                HisTreatmentFilterQuery tFilter = new HisTreatmentFilterQuery();
                tFilter.HAS_STORE_CODE = true;
                tFilter.HAS_MEDI_RECORD = false;
                List<HIS_TREATMENT> keepedStoreCodeTreats = new HisTreatmentGet().Get(tFilter);
                if (IsNotNullOrEmpty(keepedStoreCodeTreats))
                {
                    storeCodes.AddRange(keepedStoreCodeTreats.Select(o => o.STORE_CODE).ToList());
                }
            }

            foreach (HIS_TREATMENT treat in treatments)
            {

                if (data.IsOutPatient.HasValue && data.IsOutPatient.Value)
                {
                    HisMediRecordFilterQuery filter = new HisMediRecordFilterQuery();
                    filter.PATIENT_ID = treat.PATIENT_ID;
                    filter.PROGRAM_ID = data.ProgramId;
                    filter.VIR_STORE_YEAR__EQUAL = decimal.Parse((data.StoreTime).ToString().Substring(0, 4));
                    List<HIS_MEDI_RECORD> mediRecords = new HisMediRecordGet().Get(filter);
                    if (IsNotNullOrEmpty(mediRecords))
                    {
                        dicMediRecord[treat] = mediRecords[0];
                        continue;
                    }
                }

                long? seedTime = Inventec.Common.DateTime.Get.Now();
                HIS_MEDI_RECORD mediRecord = new HIS_MEDI_RECORD();
                mediRecord.MEDI_RECORD_TYPE_ID = data.MediRecordTypeId;
                mediRecord.PATIENT_ID = treat.PATIENT_ID;
                mediRecord.PROGRAM_ID = data.ProgramId;
                mediRecord.DATA_STORE_ID = data.DataStoreId;
                mediRecord.STORE_TIME = data.StoreTime;
                mediRecord.LOCATION_STORE_ID = data.LocationStoreId;

                if (data.IsUseEndCode == true)
                {
                    mediRecord.STORE_CODE = treat.END_CODE;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(data.StoreCode))
                    {
                        mediRecord.STORE_CODE = new StoreCodeGeneratorFactory(param, storeCodes).GenerateStoreCode(data.StoreTime, data.DataStoreId, treat, ref seedTime, ref storeCodes);
                    }
                    else // Th nguoi dung nhap so luu tru, gan lai seedtime theo key
                    {
                        mediRecord.STORE_CODE = data.StoreCode;
                        if (HisTreatmentCFG.STORE_CODE_SEED_TIME_OPTION == (int)HisTreatmentCFG.StoreCodeSeedTimeOption.BY_OUT_TIME)
                        {
                            if (!treat.OUT_TIME.HasValue)
                            {
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_HoSoChuaKetThucDieuTri);
                                throw new Exception("StoreCode Generate by out_time but treatment is not finish");
                            }
                            seedTime = treat.OUT_TIME.Value;
                        }
                        else if (HisTreatmentCFG.STORE_CODE_SEED_TIME_OPTION == (int)HisTreatmentCFG.StoreCodeSeedTimeOption.BY_IN_TIME)
                        {
                            if (!IsGreaterThanZero(treat.IN_TIME))
                            {
                                throw new Exception("StoreCode Generate by in_time <= 0");
                            }
                            seedTime = treat.IN_TIME;
                        }
                    }
                }
                
                mediRecord.SEED_CODE_TIME = seedTime.Value;
                
                createds.Add(mediRecord);
                dicMediRecord[treat] = mediRecord;
            }
            if (IsNotNullOrEmpty(createds) && !this.hisMediRecordCreate.CreateList(createds))
            {
                throw new Exception("hisMediRecordCreate. Ket thuc nghiep vu");
            }

            new StoreCodeGeneratorFactory(param).FinishUpdateDB(storeCodes);

            if (IsNotNullOrEmpty(createds))
            {
                List<HIS_MEDI_RECORD> newRecords = new HisMediRecordGet().GetByIds(createds.Select(s => s.ID).ToList());
                createds.ForEach(f =>
                {
                    HIS_MEDI_RECORD nRecord = newRecords.FirstOrDefault(o => o.ID == f.ID);
                    f.STORE_CODE = nRecord.STORE_CODE;
                    f.VIR_STORE_YEAR = nRecord.VIR_STORE_YEAR;
                    f.VIR_SEED_CODE_YEAR = nRecord.VIR_SEED_CODE_YEAR;
                });
            }
        }

        private void ProcessTreatment(HisTreatmentStoreSDO data, List<HIS_TREATMENT> treatments, Dictionary<HIS_TREATMENT, HIS_MEDI_RECORD> dicMediRecord)
        {
            Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
            List<HIS_TREATMENT> befores = Mapper.Map<List<HIS_TREATMENT>>(treatments);

            foreach (HIS_TREATMENT treat in treatments)
            {
                HIS_MEDI_RECORD record = dicMediRecord[treat];
                treat.MEDI_RECORD_ID = record.ID;
                treat.MEDI_RECORD_TYPE_ID = data.MediRecordTypeId;
                if (data.IsOutPatient.HasValue && data.IsOutPatient.Value)
                {
                    treat.PROGRAM_ID = data.ProgramId;
                }
                treat.STORE_TIME = data.StoreTime;
                treat.STORE_CODE = record.STORE_CODE;
                treat.DATA_STORE_ID = data.DataStoreId;
            }
            if (!DAOWorker.HisTreatmentDAO.UpdateList(treatments))
            {
                throw new Exception("hisTreatmentUpdate. Ket thuc nghiep vu");
            }
        }

        private void Rollback()
        {
            try
            {
                if (IsNotNullOrEmpty(this.recentTreatments) && !DAOWorker.HisTreatmentDAO.UpdateList(this.recentTreatments))
                {
                    LogSystem.Warn("Rollback HIS_TREATMENT that bai. Kiem tra lai du lieu");
                }
                this.hisMediRecordCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}

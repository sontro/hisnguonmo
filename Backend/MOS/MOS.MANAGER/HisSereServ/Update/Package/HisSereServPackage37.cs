using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ.Update.Package
{
    partial class HisSereServPackage37 : BusinessBase
    {
        private const long _3DAY_IN_MINUTE = 4320;
        private const long _7DAY_IN_MINUTE = 10080;

        private List<HIS_SERE_SERV> existedSereServs;
        private long treatmentId;
        private long requestRoomId;
        private long requestDepartmentId;

        internal HisSereServPackage37(CommonParam param)
            : base(param)
        {
            
        }

        internal HisSereServPackage37(CommonParam param, long treatmentId, long requestRoomId, long requestDepartmentId, List<HIS_SERE_SERV> exists)
            : base(param)
        {
            this.treatmentId = treatmentId;
            this.requestRoomId = requestRoomId;
            this.requestDepartmentId = requestDepartmentId;
            this.existedSereServs = exists;
        }

        internal void Apply3Day7Day(HIS_SERE_SERV sereServs, long instructionTime)
        {
            this.Apply3Day7Day(new List<HIS_SERE_SERV>(){sereServs}, null, null, instructionTime);
        }

        internal void Apply3Day7Day(List<HIS_SERE_SERV> sereServs, long instructionTime)
        {
            this.Apply3Day7Day(sereServs, null, null, instructionTime);
        }

        /// <summary>
        /// Xu ly chinh sach gia "3 ngay, 7 ngay". Chinh sach nay duoc phat bieu nhu sau:
        /// Sau khi benh nhan hoan thanh 1 dich vu (thuong la dich vu phau thuat) ma dich vu nay duoc xac dinh thuoc chinh sach gia "goi 3 ngay, 7 ngay",
        /// thi trong 3 ngay nam trong khoa "hoi suc cap cuu" hoac 7 ngay nam trong khoa "dieu tri", cac dich vu chi dinh se duoc tu dong gan "chi phi trong goi",
        /// con lai se duoc tinh la chi phi ngoai goi
        /// </summary>
        /// <param name="hisSereServs"></param>
        /// <param name="instructionTime"></param>
        internal void Apply3Day7Day(List<HIS_SERE_SERV> sereServs, List<HIS_EXP_MEST_MATERIAL> materials, List<HIS_EXP_MEST_MEDICINE> medicines, long instructionTime)
        {
            try
            {
                if (IsNotNullOrEmpty(materials) || IsNotNullOrEmpty(medicines) || IsNotNullOrEmpty(sereServs))
                {
                    List<long?> parentIds = new List<long?>();
                    List<long?> sereServParentIds = IsNotNullOrEmpty(sereServs) ? sereServs.Where(o => o.PARENT_ID.HasValue).Select(o => o.PARENT_ID).ToList() : null;
                    List<long?> materialParentIds = IsNotNullOrEmpty(materials) ? materials.Where(o => o.SERE_SERV_PARENT_ID.HasValue).Select(o => o.SERE_SERV_PARENT_ID).ToList() : null;
                    List<long?> medicineParentIds = IsNotNullOrEmpty(materials) ? materials.Where(o => o.SERE_SERV_PARENT_ID.HasValue).Select(o => o.SERE_SERV_PARENT_ID).ToList() : null;

                    if (IsNotNullOrEmpty(sereServParentIds))
                    {
                        parentIds.AddRange(sereServParentIds);
                    }
                    if (IsNotNullOrEmpty(materialParentIds))
                    {
                        parentIds.AddRange(materialParentIds);
                    }
                    if (IsNotNullOrEmpty(medicineParentIds))
                    {
                        parentIds.AddRange(medicineParentIds);
                    }

                    //can review lai code
                    //Tam thoi chi check voi truong hop chi co 1 parent_id 
                    //(hien tai client chi cho phep trong 1 lan chi dinh chi co 1 parent_id)
                    long? parentId = IsNotNullOrEmpty(parentIds) ? parentIds[0] : null;

                    bool isAuto = false; //co tu dong gan parent_id hay khong
                    HIS_SERE_SERV parent = null;

                    if (this.Is3Day7DayPolicy(parentId, ref parent, ref isAuto))
                    {
                        //Neu dich vu da ket thuc thi kiem tra xem khoa hien tai ma benh nhan dang nam la khoa nao
                        if (parent != null)
                        {
                            //kiem tra xem dich vu co phai la PT hay ko
                            V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.Where(o => o.ID == parent.SERVICE_ID).FirstOrDefault();
                            //Neu ko phai la phau thuat thi bo qua (do goi 3-7 chi ap dung cho dich vu phau thuat)
                            if (service == null || !parent.SERVICE_REQ_ID.HasValue || service.SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__PT)
                            {
                                return;
                            }

                            //neu dv phau thuat chua bat dau thi bo qua
                            HIS_SERVICE_REQ parentServiceReq = new HisServiceReqGet().GetById(parent.SERVICE_REQ_ID.Value);
                            if (parentServiceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                            {
                                return;
                            }

                            HIS_DEPARTMENT_TRAN lastDt = new HisDepartmentTranGet().GetLastByTreatmentId(parent.TDL_TREATMENT_ID.Value);
                            if (lastDt != null)
                            {
                                long currDepId = lastDt.DEPARTMENT_ID;
                                //Thoi diem bat dau tinh thoi gian hoi suc cap cuu sau khi lam dich vu
                                long beginCountResTime = 0;
                                //Thoi diem bat dau tinh thoi gian dieu tri sau khi lam dich vu
                                long beginCountTreatTime = 0;

                                this.GetBeginCountTime(parentServiceReq, ref beginCountResTime, ref beginCountTreatTime);

                                //voi khoa "hoi suc cap cuu" thi check 3 ngay
                                if (beginCountResTime > 0)
                                {
                                    this.SetPolicy(beginCountResTime, sereServs, medicines, materials, HisDepartmentCFG.RESUSCITATION_DEPARTMENT_IDS, currDepId, parent.ID, instructionTime, _3DAY_IN_MINUTE, isAuto);
                                }

                                //voi khoa "dieu tri" thi check 7 ngay
                                if (beginCountTreatTime > 0)
                                {
                                    this.SetPolicy(beginCountTreatTime, sereServs, medicines, materials, HisDepartmentCFG.TREATMENT_DEPARTMENT_IDS, currDepId, parent.ID, instructionTime, _7DAY_IN_MINUTE, isAuto);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        /// <summary>
        /// Quet lai danh sach sere_serv va cap nhat cac dich vu cho vao goi cua dich vu chinh (parent_sere_serv), neu sere_serv nay thuoc goi "3 ngay 7 ngay"
        /// </summary>
        /// <param name="existedSereServs">Danh sach sere_serv hien tai</param>
        /// <param name="parentSereServ">Dich vu chinh</param>
        internal void Update3Day7Day(HIS_SERVICE_REQ parentServiceReq, HIS_SERE_SERV parentSereServ,
            ref List<HIS_SERE_SERV> changeRecords, ref List<HIS_SERE_SERV> oldOfChangeRecords,
            ref List<HIS_EXP_MEST_MEDICINE> changeRecordMedicines, ref List<HIS_EXP_MEST_MEDICINE> oldOfChangeRecordMedicines,
            ref List<HIS_EXP_MEST_MATERIAL> changeRecordMaterials, ref List<HIS_EXP_MEST_MATERIAL> oldOfChangeRecordMaterials)
        {
            try
            {
                //Chi xu ly nghiep vu nay khi vien co cau hinh goi 3/7
                if (HisPackageCFG.PACKAGE_ID__3DAY7DAY > 0)
                {
                    List<HIS_SERE_SERV> existedSereServs = new HisSereServGet().GetByTreatmentId(parentSereServ.TDL_TREATMENT_ID.Value);

                    List<HIS_EXP_MEST_MEDICINE> existedExpMestMedicines = new HisExpMestMedicineGet().GetByTreatmentId(parentSereServ.TDL_TREATMENT_ID.Value);

                    List<HIS_EXP_MEST_MATERIAL> existedExpMestMaterials = new HisExpMestMaterialGet().GetByTreatmentId(parentSereServ.TDL_TREATMENT_ID.Value);

                    List<long> serviceReqIds = new List<long>();
                    if (IsNotNullOrEmpty(existedExpMestMedicines))
                    {
                        serviceReqIds.AddRange(existedExpMestMedicines.Where(o => o.TDL_SERVICE_REQ_ID.HasValue).Select(o => o.TDL_SERVICE_REQ_ID.Value).ToList());
                    }
                    if (IsNotNullOrEmpty(existedExpMestMaterials))
                    {
                        serviceReqIds.AddRange(existedExpMestMaterials.Where(o => o.TDL_SERVICE_REQ_ID.HasValue).Select(o => o.TDL_SERVICE_REQ_ID.Value).ToList());
                    }

                    List<HIS_SERVICE_REQ> presServiceReqs = null;
                    if (IsNotNullOrEmpty(serviceReqIds))
                    {
                        presServiceReqs = new HisServiceReqGet().GetByIds(serviceReqIds);
                    }
                    
                    //Luu du lieu truoc khi xu ly
                    Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                    Mapper.CreateMap<HIS_EXP_MEST_MEDICINE, HIS_EXP_MEST_MEDICINE>();
                    Mapper.CreateMap<HIS_EXP_MEST_MATERIAL, HIS_EXP_MEST_MATERIAL>();

                    List<HIS_SERE_SERV> beforeChanges = Mapper.Map<List<HIS_SERE_SERV>>(existedSereServs);
                    List<HIS_EXP_MEST_MEDICINE> beforeChangeMedicines = Mapper.Map<List<HIS_EXP_MEST_MEDICINE>>(existedExpMestMedicines);
                    List<HIS_EXP_MEST_MATERIAL> beforeChangeMaterials = Mapper.Map<List<HIS_EXP_MEST_MATERIAL>>(existedExpMestMaterials);

                    //Thoi diem bat dau tinh thoi gian hoi suc cap cuu sau khi lam dich vu
                    long beginCountResTime = 0;
                    //Thoi diem bat dau tinh thoi gian dieu tri sau khi lam dich vu
                    long beginCountTreatTime = 0;

                    this.GetBeginCountTime(parentServiceReq, ref beginCountResTime, ref beginCountTreatTime);

                    if (beginCountResTime == 0 && beginCountTreatTime == 0)
                    {
                        return;
                    }

                    if (IsNotNullOrEmpty(existedSereServs))
                    {
                        //Duyet cac sere_serv co san de tu dong gan vao goi
                        foreach (HIS_SERE_SERV sereServ in existedSereServs)
                        {
                            //Neu dich vu cha la goi 3/7 thi xu ly de gan cac sere_serv phu hop 
                            //dinh kem vao dich vu cha
                            if (parentSereServ.PACKAGE_ID == HisPackageCFG.PACKAGE_ID__3DAY7DAY)
                            {
                                if (sereServ.ID != parentSereServ.ID)
                                {
                                    //voi khoa "hoi suc cap cuu" thi check 3 ngay
                                    if (beginCountResTime > 0)
                                    {
                                        this.SetPolicy(beginCountResTime, sereServ, HisDepartmentCFG.RESUSCITATION_DEPARTMENT_IDS, parentSereServ.ID, _3DAY_IN_MINUTE);
                                    }

                                    //voi khoa "dieu tri" thi check 7 ngay
                                    if (beginCountTreatTime > 0)
                                    {
                                        this.SetPolicy(beginCountTreatTime, sereServ, HisDepartmentCFG.TREATMENT_DEPARTMENT_IDS, parentSereServ.ID, _7DAY_IN_MINUTE);
                                    }
                                }
                            }
                            
                            //Neu dich vu cha ko la goi 3/7, ma dich vu con khong duoc chi dinh boi phong
                            //xu ly dich vu cha, hien dang duoc dinh kem vao dich vu cha thi bo dinh kem
                            else if (sereServ.PARENT_ID == parentSereServ.ID && sereServ.TDL_REQUEST_ROOM_ID != parentSereServ.TDL_EXECUTE_ROOM_ID)
                            {
                                sereServ.PARENT_ID = null;
                            }
                        }
                    }

                    if (IsNotNullOrEmpty(existedExpMestMedicines))
                    {
                        //Duyet cac medicines co san de tu dong gan vao goi
                        foreach (HIS_EXP_MEST_MEDICINE medicine in existedExpMestMedicines)
                        {
                            //Neu dich vu cha la goi 3/7 thi xu ly de gan cac sere_serv phu hop 
                            //dinh kem vao dich vu cha
                            if (parentSereServ.PACKAGE_ID == HisPackageCFG.PACKAGE_ID__3DAY7DAY)
                            {
                                if (medicine.SERE_SERV_PARENT_ID != parentSereServ.ID)
                                {
                                    //voi khoa "hoi suc cap cuu" thi check 3 ngay
                                    if (beginCountResTime > 0)
                                    {
                                        this.SetPolicy(beginCountResTime, medicine, presServiceReqs, HisDepartmentCFG.RESUSCITATION_DEPARTMENT_IDS, parentSereServ.ID, _3DAY_IN_MINUTE);
                                    }

                                    //voi khoa "dieu tri" thi check 7 ngay
                                    if (beginCountTreatTime > 0)
                                    {
                                        this.SetPolicy(beginCountTreatTime, medicine, presServiceReqs, HisDepartmentCFG.TREATMENT_DEPARTMENT_IDS, parentSereServ.ID, _7DAY_IN_MINUTE);
                                    }
                                }
                            }
                            //Neu dich vu cha ko la goi 3/7, ma dich vu con khong duoc chi dinh boi phong
                            //xu ly dich vu cha, hien dang duoc dinh kem vao dich vu cha thi bo dinh kem
                            else if (medicine.SERE_SERV_PARENT_ID == parentSereServ.ID)
                            {
                                HIS_SERVICE_REQ req = presServiceReqs != null && medicine.TDL_SERVICE_REQ_ID.HasValue ? presServiceReqs.Where(o => o.ID == medicine.TDL_SERVICE_REQ_ID.Value).FirstOrDefault() : null;
                                if (req == null || req.REQUEST_ROOM_ID != parentSereServ.TDL_EXECUTE_ROOM_ID)
                                {
                                    medicine.SERE_SERV_PARENT_ID = null;
                                }
                            }
                        }
                    }

                    if (IsNotNullOrEmpty(existedExpMestMaterials))
                    {
                        //Duyet cac materials co san de tu dong gan vao goi
                        foreach (HIS_EXP_MEST_MATERIAL material in existedExpMestMaterials)
                        {
                            //Neu dich vu cha la goi 3/7 thi xu ly de gan cac sere_serv phu hop 
                            //dinh kem vao dich vu cha
                            if (parentSereServ.PACKAGE_ID == HisPackageCFG.PACKAGE_ID__3DAY7DAY)
                            {
                                if (material.SERE_SERV_PARENT_ID != parentSereServ.ID)
                                {
                                    //voi khoa "hoi suc cap cuu" thi check 3 ngay
                                    if (beginCountResTime > 0)
                                    {
                                        this.SetPolicy(beginCountResTime, material, presServiceReqs, HisDepartmentCFG.RESUSCITATION_DEPARTMENT_IDS, parentSereServ.ID, _3DAY_IN_MINUTE);
                                    }

                                    //voi khoa "dieu tri" thi check 7 ngay
                                    if (beginCountTreatTime > 0)
                                    {
                                        this.SetPolicy(beginCountTreatTime, material, presServiceReqs, HisDepartmentCFG.TREATMENT_DEPARTMENT_IDS, parentSereServ.ID, _7DAY_IN_MINUTE);
                                    }
                                }
                            }
                            //Neu dich vu cha ko la goi 3/7, ma dich vu con khong duoc chi dinh boi phong
                            //xu ly dich vu cha, hien dang duoc dinh kem vao dich vu cha thi bo dinh kem
                            else if (material.SERE_SERV_PARENT_ID == parentSereServ.ID)
                            {
                                HIS_SERVICE_REQ req = presServiceReqs != null && material.TDL_SERVICE_REQ_ID.HasValue ? presServiceReqs.Where(o => o.ID == material.TDL_SERVICE_REQ_ID.Value).FirstOrDefault() : null;
                                if (req == null || req.REQUEST_ROOM_ID != parentSereServ.TDL_EXECUTE_ROOM_ID)
                                {
                                    material.SERE_SERV_PARENT_ID = null;
                                }
                            }
                        }
                    }
                    
                    //Lay ra danh sach du lieu bi thay doi
                    List<HIS_SERE_SERV> afterChanges = Mapper.Map<List<HIS_SERE_SERV>>(existedSereServs);
                    List<HIS_EXP_MEST_MEDICINE> afterChangeMedicines = Mapper.Map<List<HIS_EXP_MEST_MEDICINE>>(existedExpMestMedicines);
                    List<HIS_EXP_MEST_MATERIAL> afterChangeMaterials = Mapper.Map<List<HIS_EXP_MEST_MATERIAL>>(existedExpMestMaterials);

                    this.GetChangeRecord(beforeChanges, afterChanges, ref changeRecords, ref oldOfChangeRecords,
                        beforeChangeMedicines, afterChangeMedicines, ref changeRecordMedicines, ref oldOfChangeRecordMedicines,
                        beforeChangeMaterials, afterChangeMaterials, ref changeRecordMaterials, ref oldOfChangeRecordMaterials);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void GetChangeRecord(List<HIS_SERE_SERV> beforeChanges, List<HIS_SERE_SERV> afterChanges, ref List<HIS_SERE_SERV> changeList, ref List<HIS_SERE_SERV> oldOfchangeList,
            List<HIS_EXP_MEST_MEDICINE> beforeChangeMedicines, List<HIS_EXP_MEST_MEDICINE> afterChangeMedicines, ref List<HIS_EXP_MEST_MEDICINE> changeListMedicine, ref List<HIS_EXP_MEST_MEDICINE> oldOfchangeListMedicine,
            List<HIS_EXP_MEST_MATERIAL> beforeChangeMaterials, List<HIS_EXP_MEST_MATERIAL> afterChangeMaterials, ref List<HIS_EXP_MEST_MATERIAL> changeListMaterial, ref List<HIS_EXP_MEST_MATERIAL> oldOfchangeListMaterial)
        {
            if (IsNotNullOrEmpty(beforeChanges) && IsNotNullOrEmpty(afterChanges))
            {
                changeList = new List<HIS_SERE_SERV>();
                oldOfchangeList = new List<HIS_SERE_SERV>();
                foreach (HIS_SERE_SERV before in beforeChanges)
                {
                    HIS_SERE_SERV after = afterChanges.Where(o => o.ID == before.ID).FirstOrDefault();
                    if (ValueChecker.IsPrimitiveDiff(before, after))
                    {
                        changeList.Add(after);
                        oldOfchangeList.Add(before);
                    }
                }
            }

            if (IsNotNullOrEmpty(beforeChangeMedicines) && IsNotNullOrEmpty(afterChangeMedicines))
            {
                changeListMedicine = new List<HIS_EXP_MEST_MEDICINE>();
                oldOfchangeListMedicine = new List<HIS_EXP_MEST_MEDICINE>();
                foreach (HIS_EXP_MEST_MEDICINE before in beforeChangeMedicines)
                {
                    HIS_EXP_MEST_MEDICINE after = afterChangeMedicines.Where(o => o.ID == before.ID).FirstOrDefault();
                    if (ValueChecker.IsPrimitiveDiff(before, after))
                    {
                        changeListMedicine.Add(after);
                        oldOfchangeListMedicine.Add(before);
                    }
                }
            }

            if (IsNotNullOrEmpty(beforeChangeMaterials) && IsNotNullOrEmpty(afterChangeMaterials))
            {
                changeListMaterial = new List<HIS_EXP_MEST_MATERIAL>();
                oldOfchangeListMaterial = new List<HIS_EXP_MEST_MATERIAL>();
                foreach (HIS_EXP_MEST_MATERIAL before in beforeChangeMaterials)
                {
                    HIS_EXP_MEST_MATERIAL after = afterChangeMaterials.Where(o => o.ID == before.ID).FirstOrDefault();
                    if (ValueChecker.IsPrimitiveDiff(before, after))
                    {
                        changeListMaterial.Add(after);
                        oldOfchangeListMaterial.Add(before);
                    }
                }
            }
        }

        private void GetBeginCountTime(HIS_SERVICE_REQ parentServiceReq, ref long beginCountResTime, ref long beginCountTreatTime)
        {
            beginCountResTime = 0;
            beginCountTreatTime = 0;

            //Lay toan bo thong tin ra vao khoa cua ho so dieu tri
            List<HIS_DEPARTMENT_TRAN> departmentTrans = new HisDepartmentTranGet().GetByTreatmentId(parentServiceReq.TREATMENT_ID);

            //Kiem tra xem tai thoi diem ngay truoc khi bat dau dv, BN dang thuoc khoa nao
            HIS_DEPARTMENT_TRAN beforeDepartment = departmentTrans
                .Where(o => o.DEPARTMENT_IN_TIME.HasValue
                    && parentServiceReq.START_TIME.HasValue
                    && o.DEPARTMENT_IN_TIME <= parentServiceReq.START_TIME.Value)
                .OrderByDescending(o => o.DEPARTMENT_IN_TIME)
                .FirstOrDefault();

            if (beforeDepartment != null)
            {
                //Neu truoc thoi diem hoan thanh, BN thuoc khoa "hoi suc cap cuu",
                //thi thoi gian bat dau tinh so ngay tai khoa "hoi suc cap cuu" la thoi diem hoan thanh dich vu
                if (HisDepartmentCFG.RESUSCITATION_DEPARTMENT_IDS.Contains(beforeDepartment.DEPARTMENT_ID))
                {
                    beginCountResTime = parentServiceReq.START_TIME.Value;
                }
                //Neu truoc thoi diem hoan thanh, BN thuoc khoa "dieu tri",
                //thi thoi gian bat dau tinh so ngay tai khoa "dieu tri" la thoi diem hoan thanh dich vu
                else if (HisDepartmentCFG.TREATMENT_DEPARTMENT_IDS.Contains(beforeDepartment.DEPARTMENT_ID))
                {
                    beginCountTreatTime = parentServiceReq.START_TIME.Value;
                }

                if (beginCountTreatTime <= 0)
                {
                    //Kiem tra xem tai thoi diem ngay sau khi bat dau dv, BN chuyen vao khoa dieu tri khi nao
                    HIS_DEPARTMENT_TRAN firstTreatDepartment = departmentTrans
                        .Where(o => o.DEPARTMENT_IN_TIME.HasValue 
                            && parentServiceReq.START_TIME.HasValue
                            && o.DEPARTMENT_IN_TIME >= parentServiceReq.START_TIME.Value
                            && HisDepartmentCFG.TREATMENT_DEPARTMENT_IDS != null
                            && HisDepartmentCFG.TREATMENT_DEPARTMENT_IDS.Contains(o.DEPARTMENT_ID))
                        .OrderBy(o => o.DEPARTMENT_IN_TIME)
                        .FirstOrDefault();

                    beginCountTreatTime = firstTreatDepartment != null ? firstTreatDepartment.DEPARTMENT_IN_TIME.Value : 0;
                }

                if (beginCountResTime <= 0)
                {
                    //Kiem tra xem tai thoi diem ngay sau khi ket thuc dv, BN chuyen vao khoa hoi suc cap cuu khi nao
                    HIS_DEPARTMENT_TRAN firstResDepartment = departmentTrans
                        .Where(o => o.DEPARTMENT_IN_TIME.HasValue && o.DEPARTMENT_IN_TIME.Value >= parentServiceReq.START_TIME.Value
                            && HisDepartmentCFG.RESUSCITATION_DEPARTMENT_IDS != null
                            && HisDepartmentCFG.RESUSCITATION_DEPARTMENT_IDS.Contains(o.DEPARTMENT_ID))
                        .OrderBy(o => o.DEPARTMENT_IN_TIME)
                        .FirstOrDefault();

                    beginCountResTime = firstResDepartment != null ? firstResDepartment.DEPARTMENT_IN_TIME.Value : 0;
                }
            }
        }

        private void SetPolicy(long beginCountTime, List<HIS_SERE_SERV> hisSereServs, List<HIS_EXP_MEST_MEDICINE> medicines, List<HIS_EXP_MEST_MATERIAL> materials, List<long> configDepartmentIds, long currentDepartmentId, long? parentId, long instructionTime, long configTime, bool isAuto)
        {
            //Neu dang nam tai khoa can check
            if (IsNotNullOrEmpty(configDepartmentIds) && configDepartmentIds.Contains(currentDepartmentId))
            {
                //Kiem tra xem tu thoi diem dau tien vao khoa can check den thoi diem ra y lenh da vuot thoi han chua
                bool isExceedConfigTime = this.IsExceedConfigTime(beginCountTime, configTime, instructionTime);

                if (IsNotNullOrEmpty(hisSereServs))
                {
                    foreach (HIS_SERE_SERV s in hisSereServs)
                    {
                        //Tu dong gan parent_id trong truong hop "is_auto" = true
                        if (isAuto && !isExceedConfigTime)
                        {
                            s.PARENT_ID = parentId;
                        }

                        if (s.PARENT_ID == parentId)
                        {
                            s.IS_OUT_PARENT_FEE = isExceedConfigTime ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                        }
                    }
                }

                if (IsNotNullOrEmpty(medicines))
                {
                    foreach (HIS_EXP_MEST_MEDICINE s in medicines)
                    {
                        //Tu dong gan parent_id trong truong hop "is_auto" = true
                        if (isAuto && !isExceedConfigTime)
                        {
                            s.SERE_SERV_PARENT_ID = parentId;
                        }

                        if (s.SERE_SERV_PARENT_ID == parentId)
                        {
                            s.IS_OUT_PARENT_FEE = isExceedConfigTime ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                        }
                    }
                }

                if (IsNotNullOrEmpty(materials))
                {
                    foreach (HIS_EXP_MEST_MATERIAL s in materials)
                    {
                        //Tu dong gan parent_id trong truong hop "is_auto" = true
                        if (isAuto && !isExceedConfigTime)
                        {
                            s.SERE_SERV_PARENT_ID = parentId;
                        }

                        if (s.SERE_SERV_PARENT_ID == parentId)
                        {
                            s.IS_OUT_PARENT_FEE = isExceedConfigTime ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                        }
                    }
                }
            }
        }

        private void SetPolicy(long beginCountTime, HIS_SERE_SERV hisSereServ, List<long> configDepartmentIds, long parentId, long configTime)
        {
            //Neu dich vu da thuoc goi dich vu khac thi bo qua
            if (hisSereServ.PARENT_ID.HasValue && hisSereServ.PARENT_ID.Value != parentId)
            {
                return;
            }

            //Neu dich vu chua thuoc goi nao hoac da thuoc goi dang xu ly thi kiem tra
            //Neu dang nam tai khoa can check
            if (IsNotNullOrEmpty(configDepartmentIds) && hisSereServ != null && configDepartmentIds.Contains(hisSereServ.TDL_REQUEST_DEPARTMENT_ID))
            {
                //Kiem tra xem tu thoi diem dau tien vao khoa can check den thoi diem ra y lenh da vuot thoi han chua
                bool isExceedConfigTime = this.IsExceedConfigTime(beginCountTime, configTime, hisSereServ.TDL_INTRUCTION_TIME);

                //Neu vuot qua thoi gian quy dinh va dinh kem dich vu nay, thi bo dinh kem (cho ra ngoai goi)
                if (isExceedConfigTime && hisSereServ.PARENT_ID == parentId)
                {
                    hisSereServ.IS_OUT_PARENT_FEE = null;
                    hisSereServ.PARENT_ID = null;
                }
                //Neu nam trong thoi gian quy dinh thi thiet lap trong goi.
                //Ko tu dong set "trong chi phi", vi co the lam mat thiet lap "ngoai chi phi" do nguoi dung thiet lap
                //Voi mau va phau thuat thi ko gan vao trong goi
                else if (!isExceedConfigTime
                    && hisSereServ.TDL_SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__MAU
                    && hisSereServ.TDL_SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__PT)
                {
                    hisSereServ.PARENT_ID = parentId;
                }
            }
        }

        private void SetPolicy(long beginCountTime, HIS_EXP_MEST_MEDICINE medicine, List<HIS_SERVICE_REQ> presServiceReqs, List<long> configDepartmentIds, long parentId, long configTime)
        {
            //Neu dich vu da thuoc goi dich vu khac thi bo qua
            if (medicine.SERE_SERV_PARENT_ID.HasValue && medicine.SERE_SERV_PARENT_ID.Value != parentId)
            {
                return;
            }

            HIS_SERVICE_REQ serviceReq = presServiceReqs != null ? presServiceReqs.Where(o => o.ID == medicine.TDL_SERVICE_REQ_ID.Value).FirstOrDefault() : null;

            //Neu dich vu chua thuoc goi nao hoac da thuoc goi dang xu ly thi kiem tra
            //Neu dang nam tai khoa can check
            if (IsNotNullOrEmpty(configDepartmentIds) && medicine != null && serviceReq != null && configDepartmentIds.Contains(serviceReq.REQUEST_DEPARTMENT_ID))
            {
                //Kiem tra xem tu thoi diem dau tien vao khoa can check den thoi diem ra y lenh da vuot thoi han chua
                bool isExceedConfigTime = this.IsExceedConfigTime(beginCountTime, configTime, serviceReq.INTRUCTION_TIME);

                //Neu vuot qua thoi gian quy dinh va dinh kem dich vu nay, thi bo dinh kem (cho ra ngoai goi)
                if (isExceedConfigTime && medicine.SERE_SERV_PARENT_ID == parentId)
                {
                    medicine.IS_OUT_PARENT_FEE = null;
                    medicine.SERE_SERV_PARENT_ID = null;
                }
                //Neu nam trong thoi gian quy dinh thi thiet lap trong goi.
                //Ko tu dong set "trong chi phi", vi co the lam mat thiet lap "ngoai chi phi" do nguoi dung thiet lap
                else if (!isExceedConfigTime)
                {
                    medicine.SERE_SERV_PARENT_ID = parentId;
                }
            }
        }

        private void SetPolicy(long beginCountTime, HIS_EXP_MEST_MATERIAL material, List<HIS_SERVICE_REQ> presServiceReqs, List<long> configDepartmentIds, long parentId, long configTime)
        {
            //Neu dich vu da thuoc goi dich vu khac thi bo qua
            if (material.SERE_SERV_PARENT_ID.HasValue && material.SERE_SERV_PARENT_ID.Value != parentId)
            {
                return;
            }

            HIS_SERVICE_REQ serviceReq = presServiceReqs != null ? presServiceReqs.Where(o => o.ID == material.TDL_SERVICE_REQ_ID.Value).FirstOrDefault() : null;

            //Neu dich vu chua thuoc goi nao hoac da thuoc goi dang xu ly thi kiem tra
            //Neu dang nam tai khoa can check
            if (IsNotNullOrEmpty(configDepartmentIds) && material != null && serviceReq != null && configDepartmentIds.Contains(serviceReq.REQUEST_DEPARTMENT_ID))
            {
                //Kiem tra xem tu thoi diem dau tien vao khoa can check den thoi diem ra y lenh da vuot thoi han chua
                bool isExceedConfigTime = this.IsExceedConfigTime(beginCountTime, configTime, serviceReq.INTRUCTION_TIME);

                //Neu vuot qua thoi gian quy dinh va dinh kem dich vu nay, thi bo dinh kem (cho ra ngoai goi)
                if (isExceedConfigTime && material.SERE_SERV_PARENT_ID == parentId)
                {
                    material.IS_OUT_PARENT_FEE = null;
                    material.SERE_SERV_PARENT_ID = null;
                }
                //Neu nam trong thoi gian quy dinh thi thiet lap trong goi.
                //Ko tu dong set "trong chi phi", vi co the lam mat thiet lap "ngoai chi phi" do nguoi dung thiet lap
                else if (!isExceedConfigTime)
                {
                    material.SERE_SERV_PARENT_ID = parentId;
                }
            }
        }

        private bool IsExceedConfigTime(long inDepartmentTime, long configTime, long instructionTime)
        {
            try
            {
                if (inDepartmentTime > 0)
                {
                    if (inDepartmentTime <= instructionTime)
                    {
                        //Luon lay dau ngay de check do thuc te nguoi dung ko nho duoc cu the gio, phut (theo issue 1982)
                        long timeBegin = Inventec.Common.DateTime.Get.StartDay(inDepartmentTime).Value;

                        //Kiem tra xem tu thoi diem dau tien vao khoa den thoi gian y lenh da vuot qua gioi han chua
                        long minDiff = Inventec.Common.DateTime.Calculation.DifferenceTime(timeBegin, instructionTime, Inventec.Common.DateTime.Calculation.UnitDifferenceTime.MINUTE);
                        return minDiff >= configTime;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        /// <summary>
        /// Kiem tra dich vu chính có duoc gan chinh sach gia "goi 3 ngay 7 ngay" hay khong
        /// </summary>
        /// <param name="sereServParentId"></param>
        /// <param name="sereServ"></param>
        /// <returns></returns>
        private bool Is3Day7DayPolicy(long? sereServParentId, ref HIS_SERE_SERV sereServ, ref bool isAuto)
        {
            if (this.IsApply3Day7DayConfig())
            {
                //Kiem tra dich vu cha co thuoc chinh sach gia goi 3 ngay 7 ngay
                if (sereServParentId.HasValue && IsNotNullOrEmpty(this.existedSereServs))
                {
                    isAuto = false;
                    sereServ = this.existedSereServs.Where(o => o.ID == sereServParentId.Value).FirstOrDefault();
                    return sereServ.PACKAGE_ID == HisPackageCFG.PACKAGE_ID__3DAY7DAY;
                }
                //Neu client ko truyen len parent_id thi tu dong lay sere_serv 
                //co chinh sach goi "3 ngay 7 ngay" gan nhat lam parent_id
                else
                {
                    //can review lai, xem co can kiem tra ca cac sere_serv chua insert ko
                    HIS_SERE_SERV _3day7daySereServ = this.existedSereServs
                        .Where(o => o.PACKAGE_ID == HisPackageCFG.PACKAGE_ID__3DAY7DAY
                        && o.IS_NO_EXECUTE == null && o.SERVICE_REQ_ID.HasValue)
                        .OrderByDescending(o => o.TDL_INTRUCTION_TIME).FirstOrDefault();
                    if (_3day7daySereServ != null)
                    {
                        isAuto = true;
                        sereServ = _3day7daySereServ;
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsApply3Day7DayConfig()
        {
            //Chi xet khi co cau hinh goi dich vu 3 ngay 7 ngay va voi cac chi dinh 
            //do "khoa hoi suc cap cuu" hoac "khoa dieu tri" chi dinh
            return HisPackageCFG.PACKAGE_ID__3DAY7DAY > 0 &&
                ((HisDepartmentCFG.RESUSCITATION_DEPARTMENT_IDS != null && HisDepartmentCFG.RESUSCITATION_DEPARTMENT_IDS.Contains(this.requestDepartmentId))
                || (HisDepartmentCFG.TREATMENT_DEPARTMENT_IDS != null && HisDepartmentCFG.TREATMENT_DEPARTMENT_IDS.Contains(this.requestDepartmentId)));
        }
    }
}

using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMedicineTypeAcin;
using MOS.MANAGER.HisService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicineType
{
    class HisMedicineTypeCreate : BusinessBase
    {
        private List<HIS_MEDICINE_TYPE> recentMedicineTypeDTOs = new List<HIS_MEDICINE_TYPE>();

        internal HisMedicineTypeCreate()
            : base()
        {

        }

        internal HisMedicineTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDICINE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineTypeCheck checker = new HisMedicineTypeCheck(param);
                HisServiceCheck serviceChecker = new HisServiceCheck(param);
                valid = valid && HisMedicineTypeUtil.GenerateMedicineTypeCode(data);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.MEDICINE_TYPE_CODE, null);
                valid = valid && checker.IsValidActiveIngredient(data);
                valid = valid && serviceChecker.ExistsCode(data.MEDICINE_TYPE_CODE, null);
                if (valid)
                {
                    HIS_MEDICINE_TYPE parent = null;
                    //luu du thua du lieu
                    data.HIS_SERVICE.SERVICE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC;
                    data.HIS_SERVICE.SERVICE_CODE = data.MEDICINE_TYPE_CODE;
                    data.HIS_SERVICE.SERVICE_NAME = data.MEDICINE_TYPE_NAME;
                    data.HIS_SERVICE.NUM_ORDER = data.NUM_ORDER;
                    if (data.PARENT_ID.HasValue)
                    {
                        parent = new HisMedicineTypeGet().GetById(data.PARENT_ID.Value);
                        data.HIS_SERVICE.PARENT_ID = parent.SERVICE_ID;
                    }
                    data.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE; //ban ghi moi tao ra luon la "la'"
                    data.HIS_SERVICE.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                    data.TDL_SERVICE_UNIT_ID = data.HIS_SERVICE.SERVICE_UNIT_ID;

                    if (DAOWorker.HisMedicineTypeDAO.Create(data))
                    {
                        this.recentMedicineTypeDTOs.Add(data); //phuc vu rollback
                        //set lai is_leaf = null cho dich vu parent
                        if (parent != null && parent.IS_LEAF == MOS.UTILITY.Constant.IS_TRUE)
                        {
                            parent.IS_LEAF = null;
                            if (!DAOWorker.HisMedicineTypeDAO.Update(parent))
                            {
                                throw new Exception("Cap nhat IS_LEAF du lieu parent that bai. Ket thuc xu ly. Rollback du lieu");
                            }
                        }

                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool CreateList(List<HIS_MEDICINE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(listData);
                HisMedicineTypeCheck checker = new HisMedicineTypeCheck(param);
                HisServiceCheck serviceChecker = new HisServiceCheck(param);
                List<HIS_MEDICINE_TYPE> listParent = new List<HIS_MEDICINE_TYPE>();
                foreach (HIS_MEDICINE_TYPE data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.MEDICINE_TYPE_CODE, null);
                    valid = valid && serviceChecker.ExistsCode(data.MEDICINE_TYPE_CODE, null);
                    if (valid)
                    {
                        HIS_MEDICINE_TYPE parent = null;
                        //luu du thua du lieu
                        data.HIS_SERVICE.SERVICE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC;
                        data.HIS_SERVICE.SERVICE_CODE = data.MEDICINE_TYPE_CODE;
                        data.HIS_SERVICE.SERVICE_NAME = data.MEDICINE_TYPE_NAME;
                        data.HIS_SERVICE.NUM_ORDER = data.NUM_ORDER;
                        if (data.PARENT_ID.HasValue)
                        {
                            parent = new HisMedicineTypeGet().GetById(data.PARENT_ID.Value);
                            data.HIS_SERVICE.PARENT_ID = parent.SERVICE_ID;
                        }
                        data.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE; //ban ghi moi tao ra luon la "la'"
                        data.HIS_SERVICE.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                        data.TDL_SERVICE_UNIT_ID = data.HIS_SERVICE.SERVICE_UNIT_ID;

                        if (!data.IMP_UNIT_ID.HasValue)
                        {
                            data.IMP_UNIT_CONVERT_RATIO = null;
                        }

                        if (parent != null && parent.IS_LEAF == MOS.UTILITY.Constant.IS_TRUE && !listParent.Exists(o => o.ID == parent.ID))
                        {
                            listParent.Add(parent);
                        }
                    }
                }

                if (valid)
                {
                    if (!DAOWorker.HisMedicineTypeDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisService_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMedicineType that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.recentMedicineTypeDTOs.AddRange(listData);//Phuc vu rollback

                    if (IsNotNullOrEmpty(listParent))
                    {
                        listParent.ForEach(o => o.IS_LEAF = null);
                        if (!DAOWorker.HisMedicineTypeDAO.UpdateList(listParent))
                        {
                            throw new Exception("Cap nhat IS_LEAF du lieu parent that bai. Ket thuc xu ly. Rollback du lieu");
                        }
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.Rollback();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool CreateParent(HIS_MEDICINE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineTypeCheck checker = new HisMedicineTypeCheck(param);
                HisServiceCheck serviceChecker = new HisServiceCheck(param);
                valid = valid && HisMedicineTypeUtil.GenerateMedicineTypeCode(data);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.MEDICINE_TYPE_CODE, null);
                valid = valid && serviceChecker.ExistsCode(data.MEDICINE_TYPE_CODE, null);
                if (valid)
                {
                    //luu du thua du lieu
                    data.HIS_SERVICE.SERVICE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC;
                    data.HIS_SERVICE.SERVICE_CODE = data.MEDICINE_TYPE_CODE;
                    data.HIS_SERVICE.SERVICE_NAME = data.MEDICINE_TYPE_NAME;
                    data.HIS_SERVICE.NUM_ORDER = data.NUM_ORDER;
                    data.IS_LEAF = null; //ban ghi moi tao ra luon cha "cha'"
                    data.HIS_SERVICE.IS_LEAF = null;
                    data.TDL_SERVICE_UNIT_ID = data.HIS_SERVICE.SERVICE_UNIT_ID;

                    if (DAOWorker.HisMedicineTypeDAO.Create(data))
                    {
                        this.recentMedicineTypeDTOs.Add(data); //phuc vu rollback
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void Rollback()
        {
            try
            {
                if (IsNotNullOrEmpty(this.recentMedicineTypeDTOs))
                {
                    DAOWorker.HisMedicineTypeDAO.TruncateList(this.recentMedicineTypeDTOs);
                    new HisServiceTruncate(param).TruncateListId(this.recentMedicineTypeDTOs.Select(s => s.SERVICE_ID).ToList());
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}

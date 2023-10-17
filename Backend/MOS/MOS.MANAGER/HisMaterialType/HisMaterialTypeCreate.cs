using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMaterialType
{
    class HisMaterialTypeCreate : BusinessBase
    {
        private List<HIS_MATERIAL_TYPE> recentMaterialTypeDTOs = new List<HIS_MATERIAL_TYPE>();

        internal HisMaterialTypeCreate()
            : base()
        {

        }

        internal HisMaterialTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MATERIAL_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMaterialTypeCheck checker = new HisMaterialTypeCheck(param);
                HisServiceCheck serviceChecker = new HisServiceCheck(param);
                valid = valid && HisMaterialTypeUtil.GenerateMaterialTypeCode(data);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.MATERIAL_TYPE_CODE, null);
                valid = valid && serviceChecker.ExistsCode(data.MATERIAL_TYPE_CODE, null);
                if (valid)
                {
                    HIS_MATERIAL_TYPE parent = null;
                    data.HIS_SERVICE.SERVICE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT;
                    data.HIS_SERVICE.SERVICE_CODE = data.MATERIAL_TYPE_CODE;
                    data.HIS_SERVICE.SERVICE_NAME = data.MATERIAL_TYPE_NAME;
                    if (data.PARENT_ID.HasValue)
                    {
                        parent = new HisMaterialTypeGet().GetById(data.PARENT_ID.Value);
                        data.HIS_SERVICE.PARENT_ID = parent.SERVICE_ID;
                    }
                    data.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                    data.HIS_SERVICE.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                    data.TDL_SERVICE_UNIT_ID = data.HIS_SERVICE.SERVICE_UNIT_ID; //luu du thua du lieu
                    if (!data.IMP_UNIT_ID.HasValue)
                    {
                        data.IMP_UNIT_CONVERT_RATIO = null;
                    }

                    if (DAOWorker.HisMaterialTypeDAO.Create(data))
                    {
                        this.recentMaterialTypeDTOs.Add(data); //phuc vu rollback
                        //set lai is_leaf = null cho dich vu parent
                        if (parent != null && parent.IS_LEAF == MOS.UTILITY.Constant.IS_TRUE)
                        {
                            parent.IS_LEAF = null;
                            if (!DAOWorker.HisMaterialTypeDAO.Update(parent))
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
                LogSystem.Error(ex);
                this.Rollback();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool CreateList(List<HIS_MATERIAL_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(listData);
                HisMaterialTypeCheck checker = new HisMaterialTypeCheck(param);
                HisServiceCheck serviceChecker = new HisServiceCheck(param);
                List<HIS_MATERIAL_TYPE> listParent = new List<HIS_MATERIAL_TYPE>();
                foreach (HIS_MATERIAL_TYPE data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.MATERIAL_TYPE_CODE, null);
                    valid = valid && serviceChecker.ExistsCode(data.MATERIAL_TYPE_CODE, null);

                    if (valid)
                    {
                        HIS_MATERIAL_TYPE parent = null;
                        data.HIS_SERVICE.SERVICE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT;
                        data.HIS_SERVICE.SERVICE_CODE = data.MATERIAL_TYPE_CODE;
                        data.HIS_SERVICE.SERVICE_NAME = data.MATERIAL_TYPE_NAME;
                        if (data.PARENT_ID.HasValue)
                        {
                            parent = new HisMaterialTypeGet().GetById(data.PARENT_ID.Value);
                            data.HIS_SERVICE.PARENT_ID = parent.SERVICE_ID;
                        }
                        data.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                        data.HIS_SERVICE.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                        data.TDL_SERVICE_UNIT_ID = data.HIS_SERVICE.SERVICE_UNIT_ID;
                        if (parent != null && parent.IS_LEAF == MOS.UTILITY.Constant.IS_TRUE && !listParent.Exists(o => o.ID == parent.ID))
                        {
                            listParent.Add(parent);
                        }
                    }
                }
                if (valid)
                {
                    if (!DAOWorker.HisMaterialTypeDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisService_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMaterialType that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.recentMaterialTypeDTOs.AddRange(listData);//Phuc vu rollback

                    if (IsNotNullOrEmpty(listParent))
                    {
                        listParent.ForEach(o => o.IS_LEAF = null);
                        if (!DAOWorker.HisMaterialTypeDAO.UpdateList(listParent))
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

        internal bool CreateParent(HIS_MATERIAL_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMaterialTypeCheck checker = new HisMaterialTypeCheck(param);
                HisServiceCheck serviceChecker = new HisServiceCheck(param);
                valid = valid && HisMaterialTypeUtil.GenerateMaterialTypeCode(data);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.MATERIAL_TYPE_CODE, null);
                valid = valid && serviceChecker.ExistsCode(data.MATERIAL_TYPE_CODE, null);
                if (valid)
                {
                    data.HIS_SERVICE.SERVICE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT;
                    data.HIS_SERVICE.SERVICE_CODE = data.MATERIAL_TYPE_CODE;
                    data.HIS_SERVICE.SERVICE_NAME = data.MATERIAL_TYPE_NAME;
                    data.IS_LEAF = null;//luon la cha
                    data.HIS_SERVICE.IS_LEAF = null;
                    data.TDL_SERVICE_UNIT_ID = data.HIS_SERVICE.SERVICE_UNIT_ID; //luu du thua du lieu

                    if (DAOWorker.HisMaterialTypeDAO.Create(data))
                    {
                        this.recentMaterialTypeDTOs.Add(data); //phuc vu rollback
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                this.Rollback();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void Rollback()
        {
            try
            {
                if (IsNotNullOrEmpty(this.recentMaterialTypeDTOs))
                {
                    DAOWorker.HisMaterialTypeDAO.TruncateList(this.recentMaterialTypeDTOs);
                    new HisServiceTruncate(param).TruncateListId(this.recentMaterialTypeDTOs.Select(s => s.SERVICE_ID).ToList());
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}

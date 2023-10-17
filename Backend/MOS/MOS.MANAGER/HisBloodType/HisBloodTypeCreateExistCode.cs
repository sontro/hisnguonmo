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

namespace MOS.MANAGER.HisBloodType
{
    partial class HisBloodTypeCreate : BusinessBase
    {
        private HIS_BLOOD_TYPE recentHisBloodType;
        private HIS_BLOOD_TYPE recentParent;
        private List<HIS_BLOOD_TYPE> recentListHisBloodType;
        private List<HIS_BLOOD_TYPE> recentListParent;

        internal HisBloodTypeCreate()
            : base()
        {

        }

        internal HisBloodTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BLOOD_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBloodTypeCheck checker = new HisBloodTypeCheck(param);
                HisServiceCheck serviceCheck = new HisServiceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.BLOOD_TYPE_CODE, null);
                valid = valid && serviceCheck.ExistsCode(data.BLOOD_TYPE_CODE, null);
                if (valid)
                {
                    HIS_BLOOD_TYPE parent = null;

                    data.HIS_SERVICE.SERVICE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__MAU;
                    //luu du thua du lieu
                    data.HIS_SERVICE.SERVICE_CODE = data.BLOOD_TYPE_CODE;
                    data.HIS_SERVICE.SERVICE_NAME = data.BLOOD_TYPE_NAME;
                    data.HIS_SERVICE.NUM_ORDER = data.NUM_ORDER;
                    data.HIS_SERVICE.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                    if (data.PARENT_ID.HasValue)
                    {
                        parent = new HisBloodTypeGet().GetById(data.PARENT_ID.Value);
                        data.HIS_SERVICE.PARENT_ID = parent.SERVICE_ID;
                    }
                    data.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE; //ban ghi moi tao ra luon la "la'"
                    data.TDL_SERVICE_UNIT_ID = data.HIS_SERVICE.SERVICE_UNIT_ID;

                    if (!DAOWorker.HisBloodTypeDAO.Create(data))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisBidBloodType_ThemMoiThatBai);
                        throw new Exception("Tao moi HIS_BLOOD_TYPE That bai" + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBloodType = data; //phuc vu rollback
                    //set lai is_leaf = null cho dich vu parent
                    if (parent != null && parent.IS_LEAF == MOS.UTILITY.Constant.IS_TRUE)
                    {
                        Mapper.CreateMap<HIS_BLOOD_TYPE, HIS_BLOOD_TYPE>();
                        HIS_BLOOD_TYPE cloneParent = Mapper.Map<HIS_BLOOD_TYPE>(parent);
                        parent.IS_LEAF = null;
                        if (!DAOWorker.HisBloodTypeDAO.Update(parent))
                        {
                            throw new Exception("Cap nhat IS_LEAF du lieu parent that bai. Ket thuc xu ly. Rollback du lieu");
                        }
                        this.recentParent = cloneParent;
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                result = false;
            }
            return result;
        }

        internal bool CreateList(List<HIS_BLOOD_TYPE> listData)
        {
            bool result = false;
            try
            {
                if (listData != null && listData.Count > 0)
                {
                    List<HIS_BLOOD_TYPE> listCreateData = new List<HIS_BLOOD_TYPE>();
                    List<HIS_BLOOD_TYPE> listParentData = new List<HIS_BLOOD_TYPE>();
                    bool error = true;

                    foreach (var data in listData)
                    {
                        bool valid = true;
                        HisBloodTypeCheck checker = new HisBloodTypeCheck(param);
                        HisServiceCheck serviceCheck = new HisServiceCheck(param);
                        valid = valid && checker.VerifyRequireField(data);
                        valid = valid && checker.ExistsCode(data.BLOOD_TYPE_CODE, null);
                        valid = valid && serviceCheck.ExistsCode(data.BLOOD_TYPE_CODE, null);
                        if (valid)
                        {
                            HIS_BLOOD_TYPE parent = null;

                            data.HIS_SERVICE.SERVICE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__MAU;
                            //luu du thua du lieu
                            data.HIS_SERVICE.SERVICE_CODE = data.BLOOD_TYPE_CODE;
                            data.HIS_SERVICE.SERVICE_NAME = data.BLOOD_TYPE_NAME;
                            data.HIS_SERVICE.NUM_ORDER = data.NUM_ORDER;
                            data.HIS_SERVICE.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                            if (data.PARENT_ID.HasValue)
                            {
                                parent = new HisBloodTypeGet().GetById(data.PARENT_ID.Value);
                                data.HIS_SERVICE.PARENT_ID = parent.SERVICE_ID;
                            }
                            data.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE; //ban ghi moi tao ra luon la "la'"
                            data.TDL_SERVICE_UNIT_ID = data.HIS_SERVICE.SERVICE_UNIT_ID;

                            listCreateData.Add(data);
                            if (recentListHisBloodType == null) recentListHisBloodType = new List<HIS_BLOOD_TYPE>();
                            recentListHisBloodType.Add(data); //phuc vu rollback
                            if (parent != null && parent.IS_LEAF == MOS.UTILITY.Constant.IS_TRUE && !listParentData.Exists(o => o.ID == parent.ID))
                            {
                                Mapper.CreateMap<HIS_BLOOD_TYPE, HIS_BLOOD_TYPE>();
                                HIS_BLOOD_TYPE cloneParent = Mapper.Map<HIS_BLOOD_TYPE>(parent);
                                parent.IS_LEAF = null;
                                listParentData.Add(parent);
                                if (recentListParent == null) recentListParent = new List<HIS_BLOOD_TYPE>();
                                recentListParent.Add(parent);
                            }
                            //set lai is_leaf = null cho dich vu parent
                            result = true;
                            error = false;
                        }
                        else
                        {
                            error = true;
                        }

                        if (error) break;
                    }

                    if (!error)
                    {
                        if (!IsNotNullOrEmpty(listCreateData) || !DAOWorker.HisBloodTypeDAO.CreateList(listCreateData))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisBidBloodType_ThemMoiThatBai);
                            throw new Exception("Tao moi HIS_BLOOD_TYPE That bai" + LogUtil.TraceData("listCreateData", listCreateData));
                        }

                        if (IsNotNullOrEmpty(listParentData))
                        {
                            if (!DAOWorker.HisBloodTypeDAO.UpdateList(listParentData))
                                throw new Exception("Cap nhat IS_LEAF du lieu parent that bai. Ket thuc xu ly. Rollback du lieu");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
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
                if (this.recentParent != null)
                {
                    DAOWorker.HisBloodTypeDAO.Update(this.recentParent);
                }
                if (this.recentHisBloodType != null)
                {
                    DAOWorker.HisBloodTypeDAO.Truncate(this.recentHisBloodType);
                    new HisServiceTruncate(param).Truncate(this.recentHisBloodType.SERVICE_ID);
                }
                if (IsNotNullOrEmpty(this.recentListHisBloodType))
                {
                    DAOWorker.HisBloodTypeDAO.TruncateList(this.recentListHisBloodType);
                    new HisServiceTruncate(param).TruncateListId(this.recentListHisBloodType.Select(s => s.SERVICE_ID).ToList());
                }
                if (IsNotNullOrEmpty(this.recentListParent))
                {
                    DAOWorker.HisBloodTypeDAO.UpdateList(this.recentListParent);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}

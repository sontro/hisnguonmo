using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestMaterial
{
    class HisImpMestMaterialCreate : BusinessBase
    {
        private List<HIS_IMP_MEST_MATERIAL> recentHisImpMestMaterials = new List<HIS_IMP_MEST_MATERIAL>();
        internal HisImpMestMaterialCreate()
            : base()
        {

        }

        internal HisImpMestMaterialCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_IMP_MEST_MATERIAL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestMaterialCheck checker = new HisImpMestMaterialCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    data.REQ_AMOUNT = data.AMOUNT; //Mac dinh luc tao thi so luong yeu cau = so luong duyet
                    if (!DAOWorker.HisImpMestMaterialDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestMaterial_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisImpMestMaterial that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisImpMestMaterials.Add(data);
                    result = true;
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

        internal bool CreateList(List<HIS_IMP_MEST_MATERIAL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpMestMaterialCheck checker = new HisImpMestMaterialCheck(param);
                foreach (var data in listData)
                {
                    data.REQ_AMOUNT = data.AMOUNT; //Mac dinh luc tao thi so luong yeu cau = so luong duyet
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisImpMestMaterialDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestMaterial_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisImpMestMaterial that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisImpMestMaterials.AddRange(listData);
                    result = true;
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

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.recentHisImpMestMaterials))
            {
                if (!DAOWorker.HisImpMestMaterialDAO.TruncateList(this.recentHisImpMestMaterials))
                {
                    LogSystem.Warn("Rollback du lieu HisImpMestMaterial that bai, can kiem tra lai." + LogUtil.TraceData("HisImpMestMaterials", this.recentHisImpMestMaterials));
                }
            }
        }
    }
}

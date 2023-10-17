using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisQcNormation.Create
{
    partial class HisQcNormationCreateSdo : BusinessBase
    {
		
        internal HisQcNormationCreateSdo()
            : base()
        {

        }

        internal HisQcNormationCreateSdo(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Run(HisQcNormationSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisQcNormationCreateSdoCheck checker = new HisQcNormationCreateSdoCheck(param);
                valid = valid && checker.IsValidData(data);
                if (valid)
                {
                    //Xoa du lieu cu
                    string sql = "DELETE HIS_QC_NORMATION WHERE MACHINE_ID = :param1 AND QC_TYPE_ID = :param2";

                    if (DAOWorker.SqlDAO.Execute(sql, data.MachineId, data.QcTypeId)
                        && IsNotNullOrEmpty(data.MaterialNormations))
                    {
                        List<HIS_QC_NORMATION> qcNormations = data.MaterialNormations.Select(o => new HIS_QC_NORMATION
                        {
                            AMOUNT = o.Amount,
                            MACHINE_ID = data.MachineId,
                            QC_TYPE_ID = data.QcTypeId,
                            MATERIAL_TYPE_ID = o.MaterialTypeId
                        }).ToList();

                        //Tao du lieu moi
                        if (!DAOWorker.HisQcNormationDAO.CreateList(qcNormations))
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisQcNormation_ThemMoiThatBai);
                            throw new Exception("Them moi thong tin HisQcNormation that bai." + LogUtil.TraceData("data", data));
                        }
                    }					
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
    }
}

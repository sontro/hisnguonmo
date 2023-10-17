using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisDepartmentTran.Create;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisKskContract.Import
{
    class DepartmentTranProcessor : BusinessBase
    {
        private HisDepartmentTranCreate hisDepartmentTranCreate;

        internal DepartmentTranProcessor()
            : base()
        {
            this.hisDepartmentTranCreate = new HisDepartmentTranCreate(param);
        }

        internal DepartmentTranProcessor(CommonParam param)
            : base(param)
        {
            this.hisDepartmentTranCreate = new HisDepartmentTranCreate(param);
        }

        internal bool Run(PrepareData prepareData, WorkPlaceSDO workPlace, ref HIS_DEPARTMENT_TRAN departmentTran, ref string desc)
        {
            bool result = false;
            try
            {
                HisDepartmentTranSDO sdo = new HisDepartmentTranSDO();
                sdo.TreatmentId = prepareData.Treatment.ID;
                sdo.IsReceive = true;
                sdo.Time = prepareData.Treatment.IN_TIME;
                sdo.DepartmentId = workPlace.DepartmentId;
                sdo.RequestRoomId = workPlace.RoomId;

                HIS_DEPARTMENT_TRAN resultData = new HIS_DEPARTMENT_TRAN();

                if (!this.hisDepartmentTranCreate.Create(sdo, true, ref resultData))
                {
                    desc = !String.IsNullOrWhiteSpace(param.GetMessage()) ? param.GetMessage() : param.GetBugCode();
                    throw new Exception("Tao DepartmentTran that bai");
                }
                result = true;
                departmentTran = resultData;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            this.hisDepartmentTranCreate.RollbackData();
        }
    }
}

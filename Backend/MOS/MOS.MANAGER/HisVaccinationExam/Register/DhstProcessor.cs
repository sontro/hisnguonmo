using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDhst;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccinationExam.Register
{
    class DhstProcessor : BusinessBase
    {
        private HisDhstCreate hisDhstCreate;

        internal DhstProcessor()
            : base()
        {
            this.hisDhstCreate = new HisDhstCreate(param);
        }

        internal DhstProcessor(CommonParam param)
            : base(param)
        {
            this.hisDhstCreate = new HisDhstCreate(param);
        }

        internal bool Run(HisPatientVaccinationSDO data, WorkPlaceSDO workPlace)
        {
            bool result = false;
            try
            {
                if (data.HisDhst != null)
                {
                    data.HisDhst.VACCINATION_EXAM_ID = data.HisVaccinationExam.ID;
                    if (!data.HisDhst.EXECUTE_TIME.HasValue)
                    {
                        data.HisDhst.EXECUTE_TIME = Inventec.Common.DateTime.Get.Now().Value;
                    }
                    data.HisDhst.EXECUTE_ROOM_ID = workPlace.RoomId;
                    data.HisDhst.EXECUTE_DEPARTMENT_ID = workPlace.DepartmentId;
                    data.HisDhst.EXECUTE_LOGINNAME = data.RequestLoginname;
                    data.HisDhst.EXECUTE_USERNAME = data.RequestUsername;

                    if (String.IsNullOrWhiteSpace(data.RequestLoginname))
                    {
                        data.HisDhst.EXECUTE_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        data.HisDhst.EXECUTE_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    }

                    if (!this.hisDhstCreate.Create(data.HisDhst))
                    {
                        throw new Exception("hisVaccinationExamCreate. Ket thuc nghiep vu");
                    }
                }
                result = true;
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
            try
            {
                this.hisDhstCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}

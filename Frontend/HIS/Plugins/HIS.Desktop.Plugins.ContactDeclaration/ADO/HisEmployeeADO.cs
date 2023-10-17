using ACS.EFMODEL.DataModels;
using HIS.Desktop.LocalStorage.BackendData;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ContactDeclaration.ADO
{
   public class HisEmployeeADO : V_HIS_EMPLOYEE
    {
        public string USERNAME { get; set; }

        public HisEmployeeADO() { }

        public HisEmployeeADO(V_HIS_EMPLOYEE data, ACS_USER dataUser) 
        {
            try
            {
                if (data != null)
                {

                    this.DEPARTMENT_CODE = data.DEPARTMENT_CODE;
                    this.DEPARTMENT_ID = data.DEPARTMENT_ID;
                    this.DEPARTMENT_NAME = data.DEPARTMENT_NAME;
                    this.DIPLOMA = data.DIPLOMA;
                    this.DOB = data.DOB;
                    this.ID = data.ID;
                    this.IS_ACTIVE = data.IS_ACTIVE;
                    this.IS_ADMIN = data.IS_ADMIN;
                    this.IS_DELETE = data.IS_DELETE;
                    this.IS_DOCTOR = data.IS_DOCTOR;
                    this.IS_NURSE = data.IS_NURSE;
                    this.LOGINNAME = data.LOGINNAME;

                    this.USERNAME = dataUser != null ? dataUser.USERNAME : "";

                    //Inventec.Common.Mapper.DataObjectMapper.Map<HisEmployeeADO>(this, data);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);                
            }
        }
    }
}

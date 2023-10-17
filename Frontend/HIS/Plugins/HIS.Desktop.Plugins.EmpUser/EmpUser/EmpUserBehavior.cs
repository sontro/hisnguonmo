using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;


namespace HIS.Desktop.Plugins.EmpUser.EmpUser
{
    class EmpUserBehavior : IEmpUser
    {
        object[] entity;
        internal EmpUserBehavior(CommonParam param,object[] entity) : base()
        {
            this.entity = entity;
        }
        //override
        object IEmpUser.Run()
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                //            
                List<long> listLong = null;
                List<string> listString =null;
                List<Action> listAction = null;
                Action<Type> ActionType = null;
                List<object> listObj = null;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }                     
                        if (entity[i] is List<long>)
                        {
                            listLong = (List<long>)entity[i];
                        }
                        if (entity[i] is List<string>)
                        {
                            listString = (List<string>)entity[i];
                        }
                        if (entity[i] is List<Action>)
                        {
                            listAction = (List<Action>)entity[i];
                        }
                        if (entity[i] is Action<Type>)
                        {
                            ActionType = (Action<Type>)entity[i];
                        }
                        if (entity[i] is List<string>)
                        {
                            listString = (List<string>)entity[i];
                        }
                        if (entity[i] is List<object>)
                        {
                            listObj = (List<object>)entity[i];
                        }
                    }
                }
                result = new frmEmpUser(moduleData, ActionType);

            }
            catch(Exception e)
            {
                Inventec.Common.Logging.LogSystem.Warn(e);
            }
            return result;            
        }
    }
}

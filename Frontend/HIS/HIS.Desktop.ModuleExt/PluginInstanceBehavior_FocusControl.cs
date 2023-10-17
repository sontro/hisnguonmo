using Inventec.Common.Logging;
using Inventec.Desktop.Common.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ModuleExt
{
    public partial class PluginInstanceBehavior
    {
        public void FocusToControl()
        {
            try
            {
                var currentpPage = HIS.Desktop.Controls.Session.SessionManager.GetCurrentPage();
                if (currentpPage != null)
                {
                    WaitingManager.Show();
                    //Focus vào control được khai báo trong UC
                    MethodFocusToControlThread(currentpPage);
                    WaitingManager.Hide();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        void MethodFocusToControlThread(object formMain)
        {
            try
            {
                Type type = formMain.GetType();
                if (type != null)
                {
                    MethodInfo methodInfo__FocusControl = type.GetMethod("FocusControl");
                    if (methodInfo__FocusControl != null)
                        methodInfo__FocusControl.Invoke(formMain, null);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}

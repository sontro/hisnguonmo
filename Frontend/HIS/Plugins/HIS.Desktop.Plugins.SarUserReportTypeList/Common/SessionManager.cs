using DevExpress.XtraTab;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
using Inventec.Desktop.Common.Token;
using System;
using System.Reflection;
using System.Windows.Forms;
using HIS.Desktop.Plugins.SarUserReportTypeList;
using Inventec.Desktop.Controls;

namespace HIS.Desktop.Plugins.SarUserReportTypeList
{
    public class SessionManager
    {
        const string frmLoginName = "frmLogin";
        const string frmMainName = "frmMain";
        const int timeWatingForError = 3000;
        static DevExpress.Utils.WaitDialogForm waitLoad = null;

        public static void ActionLostToken()
        {
            try
            {
                CommonParam param = new CommonParam();
                param.HasException = true;
                ProcessTokenLost(param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public static void ProcessTokenLost(CommonParam param)
        {
            try
            {
                if (param != null && param.HasException)
                {
                    GlobalVariables.isLogouter = false;
                    if (!GlobalVariables.IsLostToken)
                    {
                        WaitingManager.Show();
                        System.Threading.Thread.Sleep(timeWatingForError);

                        if (Application.OpenForms != null && Application.OpenForms.Count > 0)
                        {
                            for (int i = 0; i < Application.OpenForms.Count; i++)
                            {
                                Form f = Application.OpenForms[i];
                                if (f.Name != frmLoginName)
                                {
                                    try
                                    {
                                        if (!f.IsDisposed) f.Invoke(new MethodInvoker(delegate() { f.Hide(); }));
                                    }
                                    catch (Exception ex)
                                    {
                                        Inventec.Common.Logging.LogSystem.Debug("Dispose Form fail.", ex);
                                    }
                                }
                            }

                            for (int i = 0; i < Application.OpenForms.Count; i++)
                            {
                                Form f = Application.OpenForms[i];
                                if (f.Name == frmLoginName)
                                {
                                    f.Show();
                                    f.Activate();
                                    f.Focus();
                                    try
                                    {
                                        Type classType = f.GetType();
                                        MethodInfo methodInfo = classType.GetMethod("LoadOnFocus");
                                        methodInfo.Invoke(f, null);
                                    }
                                    catch { }
                                }
                            }

                           
                        }

                        WaitingManager.Hide();
                    }
                    GlobalVariables.IsLostToken = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn("ProcessHasException fail.", ex);
                WaitingManager.Hide();
            }
        }

        public static Form GetFormMain()
        {
            try
            {
                if (Application.OpenForms != null && Application.OpenForms.Count > 0)
                {
                    for (int i = 0; i < Application.OpenForms.Count; i++)
                    {
                        Form f = Application.OpenForms[i];
                        if (f.Name == frmMainName)
                        {
                            return f;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn("GetFormMain fail.", ex);
            }
            return null;
        }

        public static XtraTabControl GetTabControlMain()
        {
            return (XtraTabControl)(ControlWorker.GetControlByNameInParentControl("tabControlMain", GetFormMain()));
        }

        public static Form GetFormLogin()
        {
            try
            {
                if (Application.OpenForms != null && Application.OpenForms.Count > 0)
                {
                    for (int i = 0; i < Application.OpenForms.Count; i++)
                    {
                        Form f = Application.OpenForms[i];
                        if (f.Name == frmLoginName)
                        {
                            return (Form)f;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn("GetFormLogin fail.", ex);
            }
            return null;
        }
    }
}

﻿using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InvoiceBook
{
    //[KeyboardAction("FindShortcut1", "HIS.Desktop.Plugins.InvoiceBook.UCInvoiceBook", "FindShortcut1", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("BtnNewShortcut", "HIS.Desktop.Plugins.InvoiceBook.UCInvoiceBook", "BtnNewShortcut", KeyStroke = XKeys.Control | XKeys.N)]
    [KeyboardAction("BtnSaveShortcut", "HIS.Desktop.Plugins.InvoiceBook.UCInvoiceBook", "BtnSaveShortcut", KeyStroke = XKeys.Control | XKeys.S)]
    [ExtensionOf(typeof(DesktopToolExtensionPoint))]
    public sealed class KeyboardWorker : Tool<IDesktopToolContext>
    {
        public KeyboardWorker() : base() { }

        public override IActionSet Actions
        {
            get
            {
                return base.Actions;
            }
        }

        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HIS.Desktop.Plugins.Library.ConnectBloodPressure.ConnectService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ConnectService.IServiceConnect")]
    public interface IServiceConnect {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceConnect/GetData", ReplyAction="http://tempuri.org/IServiceConnect/GetDataResponse")]
        string GetData();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceConnect/GetData", ReplyAction="http://tempuri.org/IServiceConnect/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceConnectChannel : HIS.Desktop.Plugins.Library.ConnectBloodPressure.ConnectService.IServiceConnect, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceConnectClient : System.ServiceModel.ClientBase<HIS.Desktop.Plugins.Library.ConnectBloodPressure.ConnectService.IServiceConnect>, HIS.Desktop.Plugins.Library.ConnectBloodPressure.ConnectService.IServiceConnect {
        
        public ServiceConnectClient() {
        }
        
        public ServiceConnectClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceConnectClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceConnectClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceConnectClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetData() {
            return base.Channel.GetData();
        }
        
        public System.Threading.Tasks.Task<string> GetDataAsync() {
            return base.Channel.GetDataAsync();
        }
    }
}
﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.18408
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Evoluciona.Dialogo.Web.WsPlanDesarrollo {
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WsPlanDesarrollo.WsInterfaceFFVVSoap")]
    public interface WsInterfaceFFVVSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ConsultaPlanDesarrollo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet ConsultaPlanDesarrollo(int pAnioCurso, string pCub);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ConsultaPlanDesarrollo", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> ConsultaPlanDesarrolloAsync(int pAnioCurso, string pCub);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ConsultaPorcentajeAvanceCompetencia", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet ConsultaPorcentajeAvanceCompetencia(int pAnioCurso, string pCUB);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ConsultaPorcentajeAvanceCompetencia", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> ConsultaPorcentajeAvanceCompetenciaAsync(int pAnioCurso, string pCUB);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WsInterfaceFFVVSoapChannel : Evoluciona.Dialogo.Web.WsPlanDesarrollo.WsInterfaceFFVVSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WsInterfaceFFVVSoapClient : System.ServiceModel.ClientBase<Evoluciona.Dialogo.Web.WsPlanDesarrollo.WsInterfaceFFVVSoap>, Evoluciona.Dialogo.Web.WsPlanDesarrollo.WsInterfaceFFVVSoap {
        
        public WsInterfaceFFVVSoapClient() {
        }
        
        public WsInterfaceFFVVSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WsInterfaceFFVVSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WsInterfaceFFVVSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WsInterfaceFFVVSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Data.DataSet ConsultaPlanDesarrollo(int pAnioCurso, string pCub) {
            return base.Channel.ConsultaPlanDesarrollo(pAnioCurso, pCub);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> ConsultaPlanDesarrolloAsync(int pAnioCurso, string pCub) {
            return base.Channel.ConsultaPlanDesarrolloAsync(pAnioCurso, pCub);
        }
        
        public System.Data.DataSet ConsultaPorcentajeAvanceCompetencia(int pAnioCurso, string pCUB) {
            return base.Channel.ConsultaPorcentajeAvanceCompetencia(pAnioCurso, pCUB);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> ConsultaPorcentajeAvanceCompetenciaAsync(int pAnioCurso, string pCUB) {
            return base.Channel.ConsultaPorcentajeAvanceCompetenciaAsync(pAnioCurso, pCUB);
        }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18010
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Forseti.TFSBuildActivities.IdentityManagementService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://microsoft.com/webservices/", ConfigurationName="IdentityManagementService.IdentityManagementWebServiceSoap")]
    public interface IdentityManagementWebServiceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://microsoft.com/webservices/ReadIdentitiesByDescriptor", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Forseti.TFSBuildActivities.IdentityManagementService.TeamFoundationIdentity[] ReadIdentitiesByDescriptor(Forseti.TFSBuildActivities.IdentityManagementService.IdentityDescriptor[] descriptors, int queryMembership, int options);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://microsoft.com/webservices/ReadIdentitiesById", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Forseti.TFSBuildActivities.IdentityManagementService.TeamFoundationIdentity[] ReadIdentitiesById(System.Guid[] teamFoundationIds, int queryMembership);
        
        // CODEGEN: Parameter 'ReadIdentitiesResult' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlArrayItemAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="http://microsoft.com/webservices/ReadIdentities", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Forseti.TFSBuildActivities.IdentityManagementService.ReadIdentitiesResponse ReadIdentities(Forseti.TFSBuildActivities.IdentityManagementService.ReadIdentitiesRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://microsoft.com/webservices/CreateApplicationGroup", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Forseti.TFSBuildActivities.IdentityManagementService.IdentityDescriptor CreateApplicationGroup(string projectUri, string groupName, string groupDescription);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://microsoft.com/webservices/ListApplicationGroups", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Forseti.TFSBuildActivities.IdentityManagementService.TeamFoundationIdentity[] ListApplicationGroups(string projectUri, int options);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://microsoft.com/webservices/UpdateApplicationGroup", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void UpdateApplicationGroup(Forseti.TFSBuildActivities.IdentityManagementService.IdentityDescriptor groupDescriptor, int groupProperty, string newValue);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://microsoft.com/webservices/DeleteApplicationGroup", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void DeleteApplicationGroup(Forseti.TFSBuildActivities.IdentityManagementService.IdentityDescriptor groupDescriptor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://microsoft.com/webservices/AddMemberToApplicationGroup", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void AddMemberToApplicationGroup(Forseti.TFSBuildActivities.IdentityManagementService.IdentityDescriptor groupDescriptor, Forseti.TFSBuildActivities.IdentityManagementService.IdentityDescriptor descriptor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://microsoft.com/webservices/RemoveMemberFromApplicationGroup", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void RemoveMemberFromApplicationGroup(Forseti.TFSBuildActivities.IdentityManagementService.IdentityDescriptor groupDescriptor, Forseti.TFSBuildActivities.IdentityManagementService.IdentityDescriptor descriptor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://microsoft.com/webservices/IsMember", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool IsMember(Forseti.TFSBuildActivities.IdentityManagementService.IdentityDescriptor groupDescriptor, Forseti.TFSBuildActivities.IdentityManagementService.IdentityDescriptor descriptor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://microsoft.com/webservices/RefreshIdentity", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool RefreshIdentity(Forseti.TFSBuildActivities.IdentityManagementService.IdentityDescriptor descriptor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://microsoft.com/webservices/GetScopeName", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string GetScopeName(string scopeId);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://microsoft.com/webservices/")]
    public partial class IdentityDescriptor : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string identityTypeField;
        
        private string identifierField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string identityType {
            get {
                return this.identityTypeField;
            }
            set {
                this.identityTypeField = value;
                this.RaisePropertyChanged("identityType");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string identifier {
            get {
                return this.identifierField;
            }
            set {
                this.identifierField = value;
                this.RaisePropertyChanged("identifier");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://microsoft.com/webservices/")]
    public partial class KeyValueOfStringString : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string keyField;
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string Key {
            get {
                return this.keyField;
            }
            set {
                this.keyField = value;
                this.RaisePropertyChanged("Key");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
                this.RaisePropertyChanged("Value");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://microsoft.com/webservices/")]
    public partial class TeamFoundationIdentity : object, System.ComponentModel.INotifyPropertyChanged {
        
        private IdentityDescriptor descriptorField;
        
        private KeyValueOfStringString[] attributesField;
        
        private IdentityDescriptor[] membersField;
        
        private IdentityDescriptor[] memberOfField;
        
        private string displayNameField;
        
        private bool isContainerField;
        
        private bool isActiveField;
        
        private System.Guid teamFoundationIdField;
        
        private int uniqueUserIdField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public IdentityDescriptor Descriptor {
            get {
                return this.descriptorField;
            }
            set {
                this.descriptorField = value;
                this.RaisePropertyChanged("Descriptor");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=1)]
        public KeyValueOfStringString[] Attributes {
            get {
                return this.attributesField;
            }
            set {
                this.attributesField = value;
                this.RaisePropertyChanged("Attributes");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=2)]
        public IdentityDescriptor[] Members {
            get {
                return this.membersField;
            }
            set {
                this.membersField = value;
                this.RaisePropertyChanged("Members");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=3)]
        public IdentityDescriptor[] MemberOf {
            get {
                return this.memberOfField;
            }
            set {
                this.memberOfField = value;
                this.RaisePropertyChanged("MemberOf");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DisplayName {
            get {
                return this.displayNameField;
            }
            set {
                this.displayNameField = value;
                this.RaisePropertyChanged("DisplayName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool IsContainer {
            get {
                return this.isContainerField;
            }
            set {
                this.isContainerField = value;
                this.RaisePropertyChanged("IsContainer");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool IsActive {
            get {
                return this.isActiveField;
            }
            set {
                this.isActiveField = value;
                this.RaisePropertyChanged("IsActive");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.Guid TeamFoundationId {
            get {
                return this.teamFoundationIdField;
            }
            set {
                this.teamFoundationIdField = value;
                this.RaisePropertyChanged("TeamFoundationId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int UniqueUserId {
            get {
                return this.uniqueUserIdField;
            }
            set {
                this.uniqueUserIdField = value;
                this.RaisePropertyChanged("UniqueUserId");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ReadIdentities", WrapperNamespace="http://microsoft.com/webservices/", IsWrapped=true)]
    public partial class ReadIdentitiesRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://microsoft.com/webservices/", Order=0)]
        public int searchFactor;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://microsoft.com/webservices/", Order=1)]
        public string[] factorValues;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://microsoft.com/webservices/", Order=2)]
        public int queryMembership;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://microsoft.com/webservices/", Order=3)]
        public int options;
        
        public ReadIdentitiesRequest() {
        }
        
        public ReadIdentitiesRequest(int searchFactor, string[] factorValues, int queryMembership, int options) {
            this.searchFactor = searchFactor;
            this.factorValues = factorValues;
            this.queryMembership = queryMembership;
            this.options = options;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ReadIdentitiesResponse", WrapperNamespace="http://microsoft.com/webservices/", IsWrapped=true)]
    public partial class ReadIdentitiesResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://microsoft.com/webservices/", Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("ArrayOfTeamFoundationIdentity")]
        [System.Xml.Serialization.XmlArrayItemAttribute(NestingLevel=1)]
        public Forseti.TFSBuildActivities.IdentityManagementService.TeamFoundationIdentity[][] ReadIdentitiesResult;
        
        public ReadIdentitiesResponse() {
        }
        
        public ReadIdentitiesResponse(Forseti.TFSBuildActivities.IdentityManagementService.TeamFoundationIdentity[][] ReadIdentitiesResult) {
            this.ReadIdentitiesResult = ReadIdentitiesResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IdentityManagementWebServiceSoapChannel : Forseti.TFSBuildActivities.IdentityManagementService.IdentityManagementWebServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class IdentityManagementWebServiceSoapClient : System.ServiceModel.ClientBase<Forseti.TFSBuildActivities.IdentityManagementService.IdentityManagementWebServiceSoap>, Forseti.TFSBuildActivities.IdentityManagementService.IdentityManagementWebServiceSoap {
        
        public IdentityManagementWebServiceSoapClient() {
        }
        
        public IdentityManagementWebServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public IdentityManagementWebServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IdentityManagementWebServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IdentityManagementWebServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Forseti.TFSBuildActivities.IdentityManagementService.TeamFoundationIdentity[] ReadIdentitiesByDescriptor(Forseti.TFSBuildActivities.IdentityManagementService.IdentityDescriptor[] descriptors, int queryMembership, int options) {
            return base.Channel.ReadIdentitiesByDescriptor(descriptors, queryMembership, options);
        }
        
        public Forseti.TFSBuildActivities.IdentityManagementService.TeamFoundationIdentity[] ReadIdentitiesById(System.Guid[] teamFoundationIds, int queryMembership) {
            return base.Channel.ReadIdentitiesById(teamFoundationIds, queryMembership);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Forseti.TFSBuildActivities.IdentityManagementService.ReadIdentitiesResponse Forseti.TFSBuildActivities.IdentityManagementService.IdentityManagementWebServiceSoap.ReadIdentities(Forseti.TFSBuildActivities.IdentityManagementService.ReadIdentitiesRequest request) {
            return base.Channel.ReadIdentities(request);
        }
        
        public Forseti.TFSBuildActivities.IdentityManagementService.TeamFoundationIdentity[][] ReadIdentities(int searchFactor, string[] factorValues, int queryMembership, int options) {
            Forseti.TFSBuildActivities.IdentityManagementService.ReadIdentitiesRequest inValue = new Forseti.TFSBuildActivities.IdentityManagementService.ReadIdentitiesRequest();
            inValue.searchFactor = searchFactor;
            inValue.factorValues = factorValues;
            inValue.queryMembership = queryMembership;
            inValue.options = options;
            Forseti.TFSBuildActivities.IdentityManagementService.ReadIdentitiesResponse retVal = ((Forseti.TFSBuildActivities.IdentityManagementService.IdentityManagementWebServiceSoap)(this)).ReadIdentities(inValue);
            return retVal.ReadIdentitiesResult;
        }
        
        public Forseti.TFSBuildActivities.IdentityManagementService.IdentityDescriptor CreateApplicationGroup(string projectUri, string groupName, string groupDescription) {
            return base.Channel.CreateApplicationGroup(projectUri, groupName, groupDescription);
        }
        
        public Forseti.TFSBuildActivities.IdentityManagementService.TeamFoundationIdentity[] ListApplicationGroups(string projectUri, int options) {
            return base.Channel.ListApplicationGroups(projectUri, options);
        }
        
        public void UpdateApplicationGroup(Forseti.TFSBuildActivities.IdentityManagementService.IdentityDescriptor groupDescriptor, int groupProperty, string newValue) {
            base.Channel.UpdateApplicationGroup(groupDescriptor, groupProperty, newValue);
        }
        
        public void DeleteApplicationGroup(Forseti.TFSBuildActivities.IdentityManagementService.IdentityDescriptor groupDescriptor) {
            base.Channel.DeleteApplicationGroup(groupDescriptor);
        }
        
        public void AddMemberToApplicationGroup(Forseti.TFSBuildActivities.IdentityManagementService.IdentityDescriptor groupDescriptor, Forseti.TFSBuildActivities.IdentityManagementService.IdentityDescriptor descriptor) {
            base.Channel.AddMemberToApplicationGroup(groupDescriptor, descriptor);
        }
        
        public void RemoveMemberFromApplicationGroup(Forseti.TFSBuildActivities.IdentityManagementService.IdentityDescriptor groupDescriptor, Forseti.TFSBuildActivities.IdentityManagementService.IdentityDescriptor descriptor) {
            base.Channel.RemoveMemberFromApplicationGroup(groupDescriptor, descriptor);
        }
        
        public bool IsMember(Forseti.TFSBuildActivities.IdentityManagementService.IdentityDescriptor groupDescriptor, Forseti.TFSBuildActivities.IdentityManagementService.IdentityDescriptor descriptor) {
            return base.Channel.IsMember(groupDescriptor, descriptor);
        }
        
        public bool RefreshIdentity(Forseti.TFSBuildActivities.IdentityManagementService.IdentityDescriptor descriptor) {
            return base.Channel.RefreshIdentity(descriptor);
        }
        
        public string GetScopeName(string scopeId) {
            return base.Channel.GetScopeName(scopeId);
        }
    }
}

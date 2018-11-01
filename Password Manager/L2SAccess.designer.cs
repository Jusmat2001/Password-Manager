﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Password_Manager
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="master")]
	public partial class L2SAccessDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertPMUser(PMUser instance);
    partial void UpdatePMUser(PMUser instance);
    partial void DeletePMUser(PMUser instance);
    partial void InsertPMSite(PMSite instance);
    partial void UpdatePMSite(PMSite instance);
    partial void DeletePMSite(PMSite instance);
    #endregion
		
		public L2SAccessDataContext() : 
				base(global::Password_Manager.Properties.Settings.Default.masterConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public L2SAccessDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public L2SAccessDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public L2SAccessDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public L2SAccessDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<PMUser> PMUsers
		{
			get
			{
				return this.GetTable<PMUser>();
			}
		}
		
		public System.Data.Linq.Table<PMSite> PMSites
		{
			get
			{
				return this.GetTable<PMSite>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.PMUsers")]
	public partial class PMUser : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _PMUsername;
		
		private string _PMPassword;
		
		private System.DateTime _DateRegistered;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnPMUsernameChanging(string value);
    partial void OnPMUsernameChanged();
    partial void OnPMPasswordChanging(string value);
    partial void OnPMPasswordChanged();
    partial void OnDateRegisteredChanging(System.DateTime value);
    partial void OnDateRegisteredChanged();
    #endregion
		
		public PMUser()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PMUsername", DbType="NVarChar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string PMUsername
		{
			get
			{
				return this._PMUsername;
			}
			set
			{
				if ((this._PMUsername != value))
				{
					this.OnPMUsernameChanging(value);
					this.SendPropertyChanging();
					this._PMUsername = value;
					this.SendPropertyChanged("PMUsername");
					this.OnPMUsernameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PMPassword", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string PMPassword
		{
			get
			{
				return this._PMPassword;
			}
			set
			{
				if ((this._PMPassword != value))
				{
					this.OnPMPasswordChanging(value);
					this.SendPropertyChanging();
					this._PMPassword = value;
					this.SendPropertyChanged("PMPassword");
					this.OnPMPasswordChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateRegistered", DbType="DateTime NOT NULL")]
		public System.DateTime DateRegistered
		{
			get
			{
				return this._DateRegistered;
			}
			set
			{
				if ((this._DateRegistered != value))
				{
					this.OnDateRegisteredChanging(value);
					this.SendPropertyChanging();
					this._DateRegistered = value;
					this.SendPropertyChanged("DateRegistered");
					this.OnDateRegisteredChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.PMSites")]
	public partial class PMSite : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _siteName;
		
		private string _siteUrl;
		
		private string _siteId;
		
		private string _sitePass;
		
		private System.DateTime _lastChanged;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnsiteNameChanging(string value);
    partial void OnsiteNameChanged();
    partial void OnsiteUrlChanging(string value);
    partial void OnsiteUrlChanged();
    partial void OnsiteIdChanging(string value);
    partial void OnsiteIdChanged();
    partial void OnsitePassChanging(string value);
    partial void OnsitePassChanged();
    partial void OnlastChangedChanging(System.DateTime value);
    partial void OnlastChangedChanged();
    #endregion
		
		public PMSite()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_siteName", DbType="NVarChar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string siteName
		{
			get
			{
				return this._siteName;
			}
			set
			{
				if ((this._siteName != value))
				{
					this.OnsiteNameChanging(value);
					this.SendPropertyChanging();
					this._siteName = value;
					this.SendPropertyChanged("siteName");
					this.OnsiteNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_siteUrl", DbType="NVarChar(MAX)")]
		public string siteUrl
		{
			get
			{
				return this._siteUrl;
			}
			set
			{
				if ((this._siteUrl != value))
				{
					this.OnsiteUrlChanging(value);
					this.SendPropertyChanging();
					this._siteUrl = value;
					this.SendPropertyChanged("siteUrl");
					this.OnsiteUrlChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_siteId", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string siteId
		{
			get
			{
				return this._siteId;
			}
			set
			{
				if ((this._siteId != value))
				{
					this.OnsiteIdChanging(value);
					this.SendPropertyChanging();
					this._siteId = value;
					this.SendPropertyChanged("siteId");
					this.OnsiteIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_sitePass", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string sitePass
		{
			get
			{
				return this._sitePass;
			}
			set
			{
				if ((this._sitePass != value))
				{
					this.OnsitePassChanging(value);
					this.SendPropertyChanging();
					this._sitePass = value;
					this.SendPropertyChanged("sitePass");
					this.OnsitePassChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_lastChanged", DbType="DateTime NOT NULL")]
		public System.DateTime lastChanged
		{
			get
			{
				return this._lastChanged;
			}
			set
			{
				if ((this._lastChanged != value))
				{
					this.OnlastChangedChanging(value);
					this.SendPropertyChanging();
					this._lastChanged = value;
					this.SendPropertyChanged("lastChanged");
					this.OnlastChangedChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591

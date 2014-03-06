using System;
using System.Data.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;

namespace 
{
	public partial interface IgiltdbDataContext
	{
		void CreateDatabase();
		bool DatabaseExists();
		void DeleteDatabase();
		void Dispose();
		int ExecuteCommand(string command, params object[] parameters);
		IEnumerable<TResult> ExecuteQuery<TResult>(string query, params object[] parameters);
		IEnumerable ExecuteQuery(Type elementType, string query, params object[] parameters);
		ChangeSet GetChangeSet();
		DbCommand GetCommand(IQueryable query);
		ITable<TEntity> GetTable<TEntity>() where TEntity : class;
		ITable GetTable(Type type);
		void Refresh(RefreshMode mode, params object[] entities);
		void Refresh(RefreshMode mode, IEnumerable entities);
		void Refresh(RefreshMode mode, object entity);
		void SubmitChanges();
		void SubmitChanges(ConflictMode failureMode);
		IEnumerable<TResult> Translate<TResult>(DbDataReader reader);
		IMultipleResults Translate(DbDataReader reader);
		IEnumerable Translate(Type elementType, DbDataReader reader);
		ChangeConflictCollection ChangeConflicts { get; }
		int CommandTimeout { get; set; }
		DbConnection Connection { get; }
		bool DeferredLoadingEnabled { get; set; }
		DataLoadOptions LoadOptions { get; set; }
		TextWriter Log { get; set; }
		MetaModel Mapping { get; }
		bool ObjectTrackingEnabled { get; set; }
		DbTransaction Transaction { get; set; }
		ITable<Event> Events { get; }
		ITable<Data> Datas { get; }
		ITable<Encoding> Encodings { get; }
		ITable<Detail> Details { get; }
		ITable<Flag> Flags { get; }
		ITable<UDPHeader> UDPHeaders { get; }
		ITable<GeoIp> GeoIps { get; }
		ITable<ICMPHeader> ICMPHeaders { get; }
		ITable<IPHeader> IPHeaders { get; }
		ITable<LogEntry> LogEntries { get; }
		ITable<LogHistory> LogHistories { get; }
		ITable<ProtocolOption> ProtocolOptions { get; }
		ITable<Protocol> Protocols { get; }
		ITable<Reference> References { get; }
		ITable<ReferenceSystem> ReferenceSystems { get; }
		ITable<Schema> Schemas { get; }
		ITable<Sensor> Sensors { get; }
		ITable<Service> Services { get; }
		ITable<Signature> Signatures { get; }
		ITable<SignatureClassification> SignatureClassifications { get; }
		ITable<SignatureReference> SignatureReferences { get; }
		ITable<TCPHeader> TCPHeaders { get; }
		ITable<GeoLocation> GeoLocations { get; }
		ITable<CountryCode> CountryCodes { get; }
		ITable<EventsByCountry> EventsByCountries { get; }
		ITable<UniqueDestinationPort> UniqueDestinationPorts { get; }
		ITable<UniqueSourcePort> UniqueSourcePorts { get; }
		ITable<EventsByIp> EventsByIps { get; }
		ITable<Search> Searches { get; }
		ITable<User> Users { get; }
		ITable<NLog> NLogs { get; }
		System.String VarBinaryToIpString(System.Data.Linq.Binary? param1);
	}

	public partial class giltdbDataContextProxy : giltdbDataContext, IgiltdbDataContext
	{
		public giltdbDataContextProxy(string connectionString) : base(connectionString)
		{
		}

		ITable<TEntity> IgiltdbDataContext.GetTable<TEntity>()
		{
			return new TableProxy<TEntity>(GetTable<TEntity>());
		}

		ITable<Event> IgiltdbDataContext.Events
		{
			get { return new TableProxy<Event>(Events); }
		}

		ITable<Data> IgiltdbDataContext.Datas
		{
			get { return new TableProxy<Data>(Datas); }
		}

		ITable<Encoding> IgiltdbDataContext.Encodings
		{
			get { return new TableProxy<Encoding>(Encodings); }
		}

		ITable<Detail> IgiltdbDataContext.Details
		{
			get { return new TableProxy<Detail>(Details); }
		}

		ITable<Flag> IgiltdbDataContext.Flags
		{
			get { return new TableProxy<Flag>(Flags); }
		}

		ITable<UDPHeader> IgiltdbDataContext.UDPHeaders
		{
			get { return new TableProxy<UDPHeader>(UDPHeaders); }
		}

		ITable<GeoIp> IgiltdbDataContext.GeoIps
		{
			get { return new TableProxy<GeoIp>(GeoIps); }
		}

		ITable<ICMPHeader> IgiltdbDataContext.ICMPHeaders
		{
			get { return new TableProxy<ICMPHeader>(ICMPHeaders); }
		}

		ITable<IPHeader> IgiltdbDataContext.IPHeaders
		{
			get { return new TableProxy<IPHeader>(IPHeaders); }
		}

		ITable<LogEntry> IgiltdbDataContext.LogEntries
		{
			get { return new TableProxy<LogEntry>(LogEntries); }
		}

		ITable<LogHistory> IgiltdbDataContext.LogHistories
		{
			get { return new TableProxy<LogHistory>(LogHistories); }
		}

		ITable<ProtocolOption> IgiltdbDataContext.ProtocolOptions
		{
			get { return new TableProxy<ProtocolOption>(ProtocolOptions); }
		}

		ITable<Protocol> IgiltdbDataContext.Protocols
		{
			get { return new TableProxy<Protocol>(Protocols); }
		}

		ITable<Reference> IgiltdbDataContext.References
		{
			get { return new TableProxy<Reference>(References); }
		}

		ITable<ReferenceSystem> IgiltdbDataContext.ReferenceSystems
		{
			get { return new TableProxy<ReferenceSystem>(ReferenceSystems); }
		}

		ITable<Schema> IgiltdbDataContext.Schemas
		{
			get { return new TableProxy<Schema>(Schemas); }
		}

		ITable<Sensor> IgiltdbDataContext.Sensors
		{
			get { return new TableProxy<Sensor>(Sensors); }
		}

		ITable<Service> IgiltdbDataContext.Services
		{
			get { return new TableProxy<Service>(Services); }
		}

		ITable<Signature> IgiltdbDataContext.Signatures
		{
			get { return new TableProxy<Signature>(Signatures); }
		}

		ITable<SignatureClassification> IgiltdbDataContext.SignatureClassifications
		{
			get { return new TableProxy<SignatureClassification>(SignatureClassifications); }
		}

		ITable<SignatureReference> IgiltdbDataContext.SignatureReferences
		{
			get { return new TableProxy<SignatureReference>(SignatureReferences); }
		}

		ITable<TCPHeader> IgiltdbDataContext.TCPHeaders
		{
			get { return new TableProxy<TCPHeader>(TCPHeaders); }
		}

		ITable<GeoLocation> IgiltdbDataContext.GeoLocations
		{
			get { return new TableProxy<GeoLocation>(GeoLocations); }
		}

		ITable<CountryCode> IgiltdbDataContext.CountryCodes
		{
			get { return new TableProxy<CountryCode>(CountryCodes); }
		}

		ITable<EventsByCountry> IgiltdbDataContext.EventsByCountries
		{
			get { return new TableProxy<EventsByCountry>(EventsByCountries); }
		}

		ITable<UniqueDestinationPort> IgiltdbDataContext.UniqueDestinationPorts
		{
			get { return new TableProxy<UniqueDestinationPort>(UniqueDestinationPorts); }
		}

		ITable<UniqueSourcePort> IgiltdbDataContext.UniqueSourcePorts
		{
			get { return new TableProxy<UniqueSourcePort>(UniqueSourcePorts); }
		}

		ITable<EventsByIp> IgiltdbDataContext.EventsByIps
		{
			get { return new TableProxy<EventsByIp>(EventsByIps); }
		}

		ITable<Search> IgiltdbDataContext.Searches
		{
			get { return new TableProxy<Search>(Searches); }
		}

		ITable<User> IgiltdbDataContext.Users
		{
			get { return new TableProxy<User>(Users); }
		}

		ITable<NLog> IgiltdbDataContext.NLogs
		{
			get { return new TableProxy<NLog>(NLogs); }
		}
	}
}

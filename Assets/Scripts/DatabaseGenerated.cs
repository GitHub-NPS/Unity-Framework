using System;
using System.Collections.Generic;
using BansheeGz.BGDatabase;

//=============================================================
//||                   Generated by BansheeGz Code Generator ||
//=============================================================

#pragma warning disable 414

//=============================================================
//||                   Generated by BansheeGz Code Generator ||
//=============================================================

public partial class DB_General : BGEntity
{

	//=============================================================
	//||                   Generated by BansheeGz Code Generator ||
	//=============================================================

	public class Factory : BGEntity.EntityFactory
	{
		public BGEntity NewEntity(BGMetaEntity meta)
		{
			return new DB_General(meta);
		}
		public BGEntity NewEntity(BGMetaEntity meta, BGId id)
		{
			return new DB_General(meta, id);
		}
	}
	private static BansheeGz.BGDatabase.BGMetaRow _metaDefault;
	public static BansheeGz.BGDatabase.BGMetaRow MetaDefault
	{
		get
		{
			if(_metaDefault==null || _metaDefault.IsDeleted) _metaDefault=BGRepo.I.GetMeta<BansheeGz.BGDatabase.BGMetaRow>(new BGId(5033057752812805597,6501790984039646337));
			return _metaDefault;
		}
	}
	public static BansheeGz.BGDatabase.BGRepoEvents Events
	{
		get
		{
			return BGRepo.I.Events;
		}
	}
	private static readonly List<BGEntity> _find_Entities_Result = new List<BGEntity>();
	public static int CountEntities
	{
		get
		{
			return MetaDefault.CountEntities;
		}
	}
	public System.String name
	{
		get
		{
			return _name[Index];
		}
		set
		{
			_name[Index] = value;
		}
	}
	public System.String Name
	{
		get
		{
			return _Name[Index];
		}
		set
		{
			_Name[Index] = value;
		}
	}
	public System.String Value
	{
		get
		{
			return _Value[Index];
		}
		set
		{
			_Value[Index] = value;
		}
	}
	private static BansheeGz.BGDatabase.BGFieldEntityName _ufle12jhs77_name;
	public static BansheeGz.BGDatabase.BGFieldEntityName _name
	{
		get
		{
			if(_ufle12jhs77_name==null || _ufle12jhs77_name.IsDeleted) _ufle12jhs77_name=(BansheeGz.BGDatabase.BGFieldEntityName) MetaDefault.GetField(new BGId(5200394703058699942,5807765251162682273));
			return _ufle12jhs77_name;
		}
	}
	private static BansheeGz.BGDatabase.BGFieldString _ufle12jhs77_Name;
	public static BansheeGz.BGDatabase.BGFieldString _Name
	{
		get
		{
			if(_ufle12jhs77_Name==null || _ufle12jhs77_Name.IsDeleted) _ufle12jhs77_Name=(BansheeGz.BGDatabase.BGFieldString) MetaDefault.GetField(new BGId(5080954793424731471,14907295612307905155));
			return _ufle12jhs77_Name;
		}
	}
	private static BansheeGz.BGDatabase.BGFieldString _ufle12jhs77_Value;
	public static BansheeGz.BGDatabase.BGFieldString _Value
	{
		get
		{
			if(_ufle12jhs77_Value==null || _ufle12jhs77_Value.IsDeleted) _ufle12jhs77_Value=(BansheeGz.BGDatabase.BGFieldString) MetaDefault.GetField(new BGId(4895618618559419384,6977720910322627772));
			return _ufle12jhs77_Value;
		}
	}
	private static readonly DB_General.Factory _factory0_PFS = new DB_General.Factory();
	private static readonly DB_Language.Factory _factory1_PFS = new DB_Language.Factory();
	private static readonly DB_Gem.Factory _factory2_PFS = new DB_Gem.Factory();
	private static readonly DB_RandomName.Factory _factory3_PFS = new DB_RandomName.Factory();
	private DB_General() : base(MetaDefault)
	{
	}
	private DB_General(BGId id) : base(MetaDefault, id)
	{
	}
	private DB_General(BGMetaEntity meta) : base(meta)
	{
	}
	private DB_General(BGMetaEntity meta, BGId id) : base(meta, id)
	{
	}
	public static DB_General FindEntity(Predicate<DB_General> filter)
	{
		return MetaDefault.FindEntity(entity => filter==null || filter((DB_General) entity)) as DB_General;
	}
	public static List<DB_General> FindEntities(Predicate<DB_General> filter, List<DB_General> result=null, Comparison<DB_General> sort=null)
	{
		result = result ?? new List<DB_General>();
		_find_Entities_Result.Clear();
		MetaDefault.FindEntities(filter == null ? (Predicate<BGEntity>) null: e => filter((DB_General) e), _find_Entities_Result, sort == null ? (Comparison<BGEntity>) null : (e1, e2) => sort((DB_General) e1, (DB_General) e2));
		if (_find_Entities_Result.Count != 0)
		{
			for (var i = 0; i < _find_Entities_Result.Count; i++) result.Add((DB_General) _find_Entities_Result[i]);
			_find_Entities_Result.Clear();
		}
		return result;
	}
	public static void ForEachEntity(Action<DB_General> action, Predicate<DB_General> filter=null, Comparison<DB_General> sort=null)
	{
		MetaDefault.ForEachEntity(entity => action((DB_General) entity), filter == null ? null : (Predicate<BGEntity>) (entity => filter((DB_General) entity)), sort==null?(Comparison<BGEntity>) null:(e1,e2) => sort((DB_General)e1,(DB_General)e2));
	}
	public static DB_General GetEntity(BGId entityId)
	{
		return (DB_General) MetaDefault.GetEntity(entityId);
	}
	public static DB_General GetEntity(int index)
	{
		return (DB_General) MetaDefault[index];
	}
	public static DB_General GetEntity(string entityName)
	{
		return (DB_General) MetaDefault.GetEntity(entityName);
	}
	public static DB_General NewEntity()
	{
		return (DB_General) MetaDefault.NewEntity();
	}
}

//=============================================================
//||                   Generated by BansheeGz Code Generator ||
//=============================================================

public partial class DB_Language : BGEntity
{

	//=============================================================
	//||                   Generated by BansheeGz Code Generator ||
	//=============================================================

	public class Factory : BGEntity.EntityFactory
	{
		public BGEntity NewEntity(BGMetaEntity meta)
		{
			return new DB_Language(meta);
		}
		public BGEntity NewEntity(BGMetaEntity meta, BGId id)
		{
			return new DB_Language(meta, id);
		}
	}
	private static BansheeGz.BGDatabase.BGMetaRow _metaDefault;
	public static BansheeGz.BGDatabase.BGMetaRow MetaDefault
	{
		get
		{
			if(_metaDefault==null || _metaDefault.IsDeleted) _metaDefault=BGRepo.I.GetMeta<BansheeGz.BGDatabase.BGMetaRow>(new BGId(4971458878996820377,6018456447831788946));
			return _metaDefault;
		}
	}
	public static BansheeGz.BGDatabase.BGRepoEvents Events
	{
		get
		{
			return BGRepo.I.Events;
		}
	}
	private static readonly List<BGEntity> _find_Entities_Result = new List<BGEntity>();
	public static int CountEntities
	{
		get
		{
			return MetaDefault.CountEntities;
		}
	}
	public System.String name
	{
		get
		{
			return _name[Index];
		}
		set
		{
			_name[Index] = value;
		}
	}
	public System.String Language
	{
		get
		{
			return _Language[Index];
		}
		set
		{
			_Language[Index] = value;
		}
	}
	private static BansheeGz.BGDatabase.BGFieldEntityName _ufle12jhs77_name;
	public static BansheeGz.BGDatabase.BGFieldEntityName _name
	{
		get
		{
			if(_ufle12jhs77_name==null || _ufle12jhs77_name.IsDeleted) _ufle12jhs77_name=(BansheeGz.BGDatabase.BGFieldEntityName) MetaDefault.GetField(new BGId(4631761300566090688,925235198337617343));
			return _ufle12jhs77_name;
		}
	}
	private static BansheeGz.BGDatabase.BGFieldString _ufle12jhs77_Language;
	public static BansheeGz.BGDatabase.BGFieldString _Language
	{
		get
		{
			if(_ufle12jhs77_Language==null || _ufle12jhs77_Language.IsDeleted) _ufle12jhs77_Language=(BansheeGz.BGDatabase.BGFieldString) MetaDefault.GetField(new BGId(5094291148714323459,5058398219490259094));
			return _ufle12jhs77_Language;
		}
	}
	private static readonly DB_General.Factory _factory0_PFS = new DB_General.Factory();
	private static readonly DB_Language.Factory _factory1_PFS = new DB_Language.Factory();
	private static readonly DB_Gem.Factory _factory2_PFS = new DB_Gem.Factory();
	private static readonly DB_RandomName.Factory _factory3_PFS = new DB_RandomName.Factory();
	private DB_Language() : base(MetaDefault)
	{
	}
	private DB_Language(BGId id) : base(MetaDefault, id)
	{
	}
	private DB_Language(BGMetaEntity meta) : base(meta)
	{
	}
	private DB_Language(BGMetaEntity meta, BGId id) : base(meta, id)
	{
	}
	public static DB_Language FindEntity(Predicate<DB_Language> filter)
	{
		return MetaDefault.FindEntity(entity => filter==null || filter((DB_Language) entity)) as DB_Language;
	}
	public static List<DB_Language> FindEntities(Predicate<DB_Language> filter, List<DB_Language> result=null, Comparison<DB_Language> sort=null)
	{
		result = result ?? new List<DB_Language>();
		_find_Entities_Result.Clear();
		MetaDefault.FindEntities(filter == null ? (Predicate<BGEntity>) null: e => filter((DB_Language) e), _find_Entities_Result, sort == null ? (Comparison<BGEntity>) null : (e1, e2) => sort((DB_Language) e1, (DB_Language) e2));
		if (_find_Entities_Result.Count != 0)
		{
			for (var i = 0; i < _find_Entities_Result.Count; i++) result.Add((DB_Language) _find_Entities_Result[i]);
			_find_Entities_Result.Clear();
		}
		return result;
	}
	public static void ForEachEntity(Action<DB_Language> action, Predicate<DB_Language> filter=null, Comparison<DB_Language> sort=null)
	{
		MetaDefault.ForEachEntity(entity => action((DB_Language) entity), filter == null ? null : (Predicate<BGEntity>) (entity => filter((DB_Language) entity)), sort==null?(Comparison<BGEntity>) null:(e1,e2) => sort((DB_Language)e1,(DB_Language)e2));
	}
	public static DB_Language GetEntity(BGId entityId)
	{
		return (DB_Language) MetaDefault.GetEntity(entityId);
	}
	public static DB_Language GetEntity(int index)
	{
		return (DB_Language) MetaDefault[index];
	}
	public static DB_Language GetEntity(string entityName)
	{
		return (DB_Language) MetaDefault.GetEntity(entityName);
	}
	public static DB_Language NewEntity()
	{
		return (DB_Language) MetaDefault.NewEntity();
	}
}

//=============================================================
//||                   Generated by BansheeGz Code Generator ||
//=============================================================

public partial class DB_Gem : BGEntity
{

	//=============================================================
	//||                   Generated by BansheeGz Code Generator ||
	//=============================================================

	public class Factory : BGEntity.EntityFactory
	{
		public BGEntity NewEntity(BGMetaEntity meta)
		{
			return new DB_Gem(meta);
		}
		public BGEntity NewEntity(BGMetaEntity meta, BGId id)
		{
			return new DB_Gem(meta, id);
		}
	}
	private static BansheeGz.BGDatabase.BGMetaRow _metaDefault;
	public static BansheeGz.BGDatabase.BGMetaRow MetaDefault
	{
		get
		{
			if(_metaDefault==null || _metaDefault.IsDeleted) _metaDefault=BGRepo.I.GetMeta<BansheeGz.BGDatabase.BGMetaRow>(new BGId(5471218549534542138,10507169471023126418));
			return _metaDefault;
		}
	}
	public static BansheeGz.BGDatabase.BGRepoEvents Events
	{
		get
		{
			return BGRepo.I.Events;
		}
	}
	private static readonly List<BGEntity> _find_Entities_Result = new List<BGEntity>();
	public static int CountEntities
	{
		get
		{
			return MetaDefault.CountEntities;
		}
	}
	public System.String name
	{
		get
		{
			return _name[Index];
		}
		set
		{
			_name[Index] = value;
		}
	}
	public System.Int32 Quantity
	{
		get
		{
			return _Quantity[Index];
		}
		set
		{
			_Quantity[Index] = value;
		}
	}
	public System.String Product
	{
		get
		{
			return _Product[Index];
		}
		set
		{
			_Product[Index] = value;
		}
	}
	private static BansheeGz.BGDatabase.BGFieldEntityName _ufle12jhs77_name;
	public static BansheeGz.BGDatabase.BGFieldEntityName _name
	{
		get
		{
			if(_ufle12jhs77_name==null || _ufle12jhs77_name.IsDeleted) _ufle12jhs77_name=(BansheeGz.BGDatabase.BGFieldEntityName) MetaDefault.GetField(new BGId(5525161711632267786,9947925700708098197));
			return _ufle12jhs77_name;
		}
	}
	private static BansheeGz.BGDatabase.BGFieldInt _ufle12jhs77_Quantity;
	public static BansheeGz.BGDatabase.BGFieldInt _Quantity
	{
		get
		{
			if(_ufle12jhs77_Quantity==null || _ufle12jhs77_Quantity.IsDeleted) _ufle12jhs77_Quantity=(BansheeGz.BGDatabase.BGFieldInt) MetaDefault.GetField(new BGId(5259901390497371244,16715801534541446578));
			return _ufle12jhs77_Quantity;
		}
	}
	private static BansheeGz.BGDatabase.BGFieldString _ufle12jhs77_Product;
	public static BansheeGz.BGDatabase.BGFieldString _Product
	{
		get
		{
			if(_ufle12jhs77_Product==null || _ufle12jhs77_Product.IsDeleted) _ufle12jhs77_Product=(BansheeGz.BGDatabase.BGFieldString) MetaDefault.GetField(new BGId(5141175180105941664,661462108217686696));
			return _ufle12jhs77_Product;
		}
	}
	private static readonly DB_General.Factory _factory0_PFS = new DB_General.Factory();
	private static readonly DB_Language.Factory _factory1_PFS = new DB_Language.Factory();
	private static readonly DB_Gem.Factory _factory2_PFS = new DB_Gem.Factory();
	private static readonly DB_RandomName.Factory _factory3_PFS = new DB_RandomName.Factory();
	private DB_Gem() : base(MetaDefault)
	{
	}
	private DB_Gem(BGId id) : base(MetaDefault, id)
	{
	}
	private DB_Gem(BGMetaEntity meta) : base(meta)
	{
	}
	private DB_Gem(BGMetaEntity meta, BGId id) : base(meta, id)
	{
	}
	public static DB_Gem FindEntity(Predicate<DB_Gem> filter)
	{
		return MetaDefault.FindEntity(entity => filter==null || filter((DB_Gem) entity)) as DB_Gem;
	}
	public static List<DB_Gem> FindEntities(Predicate<DB_Gem> filter, List<DB_Gem> result=null, Comparison<DB_Gem> sort=null)
	{
		result = result ?? new List<DB_Gem>();
		_find_Entities_Result.Clear();
		MetaDefault.FindEntities(filter == null ? (Predicate<BGEntity>) null: e => filter((DB_Gem) e), _find_Entities_Result, sort == null ? (Comparison<BGEntity>) null : (e1, e2) => sort((DB_Gem) e1, (DB_Gem) e2));
		if (_find_Entities_Result.Count != 0)
		{
			for (var i = 0; i < _find_Entities_Result.Count; i++) result.Add((DB_Gem) _find_Entities_Result[i]);
			_find_Entities_Result.Clear();
		}
		return result;
	}
	public static void ForEachEntity(Action<DB_Gem> action, Predicate<DB_Gem> filter=null, Comparison<DB_Gem> sort=null)
	{
		MetaDefault.ForEachEntity(entity => action((DB_Gem) entity), filter == null ? null : (Predicate<BGEntity>) (entity => filter((DB_Gem) entity)), sort==null?(Comparison<BGEntity>) null:(e1,e2) => sort((DB_Gem)e1,(DB_Gem)e2));
	}
	public static DB_Gem GetEntity(BGId entityId)
	{
		return (DB_Gem) MetaDefault.GetEntity(entityId);
	}
	public static DB_Gem GetEntity(int index)
	{
		return (DB_Gem) MetaDefault[index];
	}
	public static DB_Gem GetEntity(string entityName)
	{
		return (DB_Gem) MetaDefault.GetEntity(entityName);
	}
	public static DB_Gem NewEntity()
	{
		return (DB_Gem) MetaDefault.NewEntity();
	}
}

//=============================================================
//||                   Generated by BansheeGz Code Generator ||
//=============================================================

public partial class DB_RandomName : BGEntity
{

	//=============================================================
	//||                   Generated by BansheeGz Code Generator ||
	//=============================================================

	public class Factory : BGEntity.EntityFactory
	{
		public BGEntity NewEntity(BGMetaEntity meta)
		{
			return new DB_RandomName(meta);
		}
		public BGEntity NewEntity(BGMetaEntity meta, BGId id)
		{
			return new DB_RandomName(meta, id);
		}
	}
	private static BansheeGz.BGDatabase.BGMetaRow _metaDefault;
	public static BansheeGz.BGDatabase.BGMetaRow MetaDefault
	{
		get
		{
			if(_metaDefault==null || _metaDefault.IsDeleted) _metaDefault=BGRepo.I.GetMeta<BansheeGz.BGDatabase.BGMetaRow>(new BGId(5528312628932659571,10040242879832507308));
			return _metaDefault;
		}
	}
	public static BansheeGz.BGDatabase.BGRepoEvents Events
	{
		get
		{
			return BGRepo.I.Events;
		}
	}
	private static readonly List<BGEntity> _find_Entities_Result = new List<BGEntity>();
	public static int CountEntities
	{
		get
		{
			return MetaDefault.CountEntities;
		}
	}
	public System.String name
	{
		get
		{
			return _name[Index];
		}
		set
		{
			_name[Index] = value;
		}
	}
	public System.String Name
	{
		get
		{
			return _Name[Index];
		}
		set
		{
			_Name[Index] = value;
		}
	}
	private static BansheeGz.BGDatabase.BGFieldEntityName _ufle12jhs77_name;
	public static BansheeGz.BGDatabase.BGFieldEntityName _name
	{
		get
		{
			if(_ufle12jhs77_name==null || _ufle12jhs77_name.IsDeleted) _ufle12jhs77_name=(BansheeGz.BGDatabase.BGFieldEntityName) MetaDefault.GetField(new BGId(5691049533536003882,15859213100176334736));
			return _ufle12jhs77_name;
		}
	}
	private static BansheeGz.BGDatabase.BGFieldString _ufle12jhs77_Name;
	public static BansheeGz.BGDatabase.BGFieldString _Name
	{
		get
		{
			if(_ufle12jhs77_Name==null || _ufle12jhs77_Name.IsDeleted) _ufle12jhs77_Name=(BansheeGz.BGDatabase.BGFieldString) MetaDefault.GetField(new BGId(5552476612138803495,15961498539121609391));
			return _ufle12jhs77_Name;
		}
	}
	private static readonly DB_General.Factory _factory0_PFS = new DB_General.Factory();
	private static readonly DB_Language.Factory _factory1_PFS = new DB_Language.Factory();
	private static readonly DB_Gem.Factory _factory2_PFS = new DB_Gem.Factory();
	private static readonly DB_RandomName.Factory _factory3_PFS = new DB_RandomName.Factory();
	private DB_RandomName() : base(MetaDefault)
	{
	}
	private DB_RandomName(BGId id) : base(MetaDefault, id)
	{
	}
	private DB_RandomName(BGMetaEntity meta) : base(meta)
	{
	}
	private DB_RandomName(BGMetaEntity meta, BGId id) : base(meta, id)
	{
	}
	public static DB_RandomName FindEntity(Predicate<DB_RandomName> filter)
	{
		return MetaDefault.FindEntity(entity => filter==null || filter((DB_RandomName) entity)) as DB_RandomName;
	}
	public static List<DB_RandomName> FindEntities(Predicate<DB_RandomName> filter, List<DB_RandomName> result=null, Comparison<DB_RandomName> sort=null)
	{
		result = result ?? new List<DB_RandomName>();
		_find_Entities_Result.Clear();
		MetaDefault.FindEntities(filter == null ? (Predicate<BGEntity>) null: e => filter((DB_RandomName) e), _find_Entities_Result, sort == null ? (Comparison<BGEntity>) null : (e1, e2) => sort((DB_RandomName) e1, (DB_RandomName) e2));
		if (_find_Entities_Result.Count != 0)
		{
			for (var i = 0; i < _find_Entities_Result.Count; i++) result.Add((DB_RandomName) _find_Entities_Result[i]);
			_find_Entities_Result.Clear();
		}
		return result;
	}
	public static void ForEachEntity(Action<DB_RandomName> action, Predicate<DB_RandomName> filter=null, Comparison<DB_RandomName> sort=null)
	{
		MetaDefault.ForEachEntity(entity => action((DB_RandomName) entity), filter == null ? null : (Predicate<BGEntity>) (entity => filter((DB_RandomName) entity)), sort==null?(Comparison<BGEntity>) null:(e1,e2) => sort((DB_RandomName)e1,(DB_RandomName)e2));
	}
	public static DB_RandomName GetEntity(BGId entityId)
	{
		return (DB_RandomName) MetaDefault.GetEntity(entityId);
	}
	public static DB_RandomName GetEntity(int index)
	{
		return (DB_RandomName) MetaDefault[index];
	}
	public static DB_RandomName GetEntity(string entityName)
	{
		return (DB_RandomName) MetaDefault.GetEntity(entityName);
	}
	public static DB_RandomName NewEntity()
	{
		return (DB_RandomName) MetaDefault.NewEntity();
	}
}
#pragma warning restore 414

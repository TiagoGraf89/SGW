using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGW.Common.DataContract;

namespace SGW.Portal.Models
{
    public class EntityModel
    {
		public bool EditMode { get; set; }

		[Required]
		[Display(Name = "Id")]
		public Guid Id { get; set; }

		[Required]
		[Display(Name = "Nome")]
		public string Name { get; set; }

		[Required]
		[Display(Name = "Tipo de Entidade")]
		public string EntityType { get; set; }

		[Required]
		[Display(Name = "Nome da Tabela SQL")]
		public string SQLTableName { get; set; }

		[Required]
		[Display(Name = "Campo de Status")]
		public string StatusField { get; set; }

		[Required]
		[Display(Name = "Campo Identificador")]
		public string UserDefinedCodeField { get; set; }
		

		public List<EntityStatusModel> EntityStatus { get; set; }
		public List<EntityFieldModel> EntityFields { get; set; }
		public List<SQLTableModel> SQLTables { get; set; }
		public List<EntityTypeModel> EntityTypes { get; set; }
		public List<EntityFieldTypeModel> EntityFieldTypes { get; set; }
	}

	public class SQLTableModel
	{
		public string Name { get; set; }
	}

	public class EntityTypeModel
	{
		public string Id { get; set; }

		public string Name { get; set; }
	}

	public class EntityFieldTypeModel
	{
		public string Id { get; set; }

		public string Name { get; set; }
	}

	public class EntityStatusModel
	{
		public bool EditMode { get; set; }

		public string Description { get; set; }

		public string Code { get; set; }
	}

	public class EntityFieldModel
	{
		public bool EditMode { get; set; }

		public string Name { get; set; }

		public string Type { get; set; }

		public bool UserDefined { get; set; }
	}
}

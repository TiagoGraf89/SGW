using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGW.Common.DataContract;

namespace SGW.Portal.Models
{
    public class ConditionModel
    {
		public bool EditMode { get; set; }

		[Required]
		[Display(Name = "Id")]
		public Guid Id { get; set; }

		[Required]
		[Display(Name = "Nome")]
		public string Name { get; set; }

		[Display(Name = "Comando SQL")]
		public string SQLCommand { get; set; }

		[Display(Name = "Procedure SQL")]
		public string SQLProcedure { get; set; }

		[Display(Name = "Tipo de Condição")]
		public string ConditionType { get; set; }

		[Display(Name = "Descrição Entidade")]
		public string EntityDesciption { get; set; }

		[Display(Name = "Entidade")]
		public Guid EntityId { get; set; }

		public string ConditionDisplayText { get; set; }

		public List<ConditionDetailModel> ConditionDetails { get; set; }

		public List<SelectListItem> ConditionTypes { get; set; }
		public List<SelectListItem> Entities { get; set; }
		public List<SelectListItem> Procedures { get; set; }
		public List<SelectListItem> Fields { get; set; }
		public List<SelectListItem> Operators { get; set; }
	}

	public class ConditionDetailModel
	{
		public bool EditMode { get; set; }

		public Guid ConditionDetailId { get; set; }

		public string GroupIdentifier { get; set; }
	
		public string Field { get; set; }
		
		public string Operator { get; set; }
		
		public string Value1 { get; set; }
		
		public string Value2 { get; set; }

	}
}

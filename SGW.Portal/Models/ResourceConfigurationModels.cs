using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGW.Common.DataContract;

namespace SGW.Portal.Models
{
    public class ResourceConfigurationModel
    {
		[Required]
		[Display(Name = "Id")]
		public Guid Id { get; set; }

		[Required]
		[Display(Name = "Grupo/Setor")]
		public Guid Workgroup { get; set; }

		[Required]
		[Display(Name = "Nome")]
		public string Name { get; set; }

		[Required]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[Display(Name = "Senha")]
		public string Password { get; set; }

		[Required]
		[Display(Name = "Confirmar Senha")]
		public string ConfirmPassword { get; set; }

		[Required]
		[Display(Name = "Ativo")]
		public bool Active { get; set; }

		public List<RoleDataContract> Roles { get; set; }
		public List<WorkgroupDataContract> WorkgroupList { get; set; }

		public bool EditMode { get; set; }

	}
	
	public class RoleDataModel
	{
		public Guid Id { get; set; }
		public string Description { get; set; }
		public bool Checked { get; set; }
	}

	public class WorkgroupRoleListModel
	{
		public IEnumerable<WorkgroupDataContract> Workgroups { get; set; }
		public IEnumerable<RoleDataContract> Roles { get; set; }

		public IEnumerable<SelectListItem> WorkgroupsListItem
		{
			get
			{
				List<SelectListItem> list;
				if (Workgroups == null || !Workgroups.Any())
					list = new List<SelectListItem>();
				else
					list = this.Workgroups.Select(w => new SelectListItem() { Text = w.Description, Value = w.Id.ToString() }).ToList();
				list.Insert(0, new SelectListItem() { Text = "Nenhum", Value = Guid.Empty.ToString() });
				return list;
			}
		}

		[Display(Name = "Descrição Cargo")]
		public string RoleDescription { get; set; }

		[Display(Name = "Id Cargo")]
		public Guid RoleId { get; set; }

		[Display(Name = "Descrição Grupo")]
		public string WorkgroupDescription { get; set; }

		[Display(Name = "Grupo Pai")]
		public Guid ParentWorkgroupId { get; set; }

		[Display(Name = "Id Grupo")]
		public Guid WorkgroupId { get; set; }

		public string Message { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace SGW.Portal.Models
{
	public class UsersContext : DbContext
	{
		public UsersContext()
			: base("DefaultConnection")
		{
		}

		public DbSet<UserProfile> UserProfiles { get; set; }
	}

	[Table("UserProfile")]
	public class UserProfile
	{
		[Key]
		[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
		public int UserId { get; set; }
		public string UserName { get; set; }
	}

	public class LoginModel
	{
		[Required]
		[Display(Name = "Email")]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Senha")]
		public string Password { get; set; }

		[Display(Name = "Lembrar-me?")]
		public bool RememberMe { get; set; }
	}

	public class ForgotPasswordModel
	{
		[Required]
		[Display(Name = "Email")]
		public string Email { get; set; }

	}

	public class ReminderModel
	{
		[Required]
		[Display(Name = "Entidade")]
		public string Entity { get; set; }

		[Required]
		[Display(Name = "Número")]
		public string Number { get; set; }

		[Required]
		[Display(Name = "Usuário Assinalado")]
		public string Assigned { get; set; }
	}
}

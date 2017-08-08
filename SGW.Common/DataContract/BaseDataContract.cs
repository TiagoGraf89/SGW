using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace SGW.Common.DataContract
{
	[DataContract]
	public class BaseDataContract
	{
		[Required]
		public Guid Id { get; set; }
		public string Description { get; set; }

		public ValidationResults Validate()
		{
			try
			{
				Validator.ValidateObject(this, new ValidationContext(this));
				return new ValidationResults();
			}
			catch (ValidationException ex)
			{
				ValidationResults results = new ValidationResults();
				foreach (var m in ex.ValidationResult.MemberNames)
					results.Add(new ValidationResult() { Field = m, Message = ex.ValidationResult.ErrorMessage });
				return results;
			}
		}

	}
}

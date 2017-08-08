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
	public class EntityInstanceValueDataContract : BaseDataContract
	{

		public string FieldName { get; set; }

		public decimal? NumberValue { get; set; }
		public string TextValue { get; set; }
		public DateTime? DateValue { get; set; }
		public Guid? IdValue { get; set; }

		public string Value
		{
			get
			{
				if (IdValue.HasValue)
					return IdValue.Value.ToString();
				else if (DateValue.HasValue)
					return DateValue.Value.ToString();
				else if (NumberValue.HasValue)
					return NumberValue.Value.ToString();
				else
					return TextValue;
			}
		}
		
		public Guid EntityInstanceId { get; set; }

		public Guid EntityId  { get; set; }

	}
}

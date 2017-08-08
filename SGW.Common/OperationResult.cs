using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGW.Common
{
    public class OperationResult
    {

		public OperationResult()
		{
			this.Status = OperationResultStatus.Succesfull;
		}
		public OperationResult(OperationResultStatus status, string message)
		{
			this.Status = status;
			this.Message = message;
		}
		public OperationResult(Exception ex)
		{
			this.Status = OperationResultStatus.UnexpectedError;
			this.Message = ex.Message;
			this.Exception = ex;
		}
		public OperationResult(ValidationResults validationResults)
		{
			this.Status = OperationResultStatus.ValidationFailure;
			this.Message = validationResults.ToString();
			this.Exception = null;
			this.ValidationResults = validationResults;
		}

		public OperationResultStatus Status { get; set; }
		public string Message { get; set; }
		public List<ValidationResult> ValidationResults { get; private set; }
		public Exception Exception { get; set; }
	}
	public class ValidationResults : List<ValidationResult>
	{
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			foreach (var item in this)
			{
				sb.AppendLine(item.ToString());
			}
			return sb.ToString();
		}

		public bool IsValid 
		{ 
			get 
			{
				return this.Count == 0;
			} 
		}
		public string ToHtmlString()
		{
			StringBuilder sb = new StringBuilder();
			foreach (var item in this)
			{
				sb.AppendLine(string.Format("{0}<br />",item.ToString()));
			}
			return sb.ToString();
		}
	}
	public class ValidationResult
	{
		public string Field { get; set; }
		public string Message { get; set; }
		public override string ToString()
		{
			return string.Format("{0} - {1}.", Field, Message);
		}
	}
	public enum OperationResultStatus
	{
		Succesfull,
		ValidationFailure,
		UnexpectedError,
	}
}

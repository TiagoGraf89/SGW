using System.Reflection;
namespace SGW.DataAccess
{
	partial class SGWDataContext
	{
		public void RevertChanges()
		{
			var cs = GetChangeSet();

			foreach (var item in cs.Inserts)
			{
				var t = item.GetType();
				GetTable(t).DeleteOnSubmit(item);
			}

			foreach (var item in cs.Deletes)
			{
				var t = item.GetType();
				GetTable(t).InsertOnSubmit(item);
			}

			foreach (var item in cs.Updates)
			{
				var t = item.GetType();
				foreach (var mm in GetTable(t).GetModifiedMembers(item))
				{
					var pi = mm.Member as PropertyInfo;
					if (pi != null)
					{
						pi.SetValue(item, mm.OriginalValue, null);
					}

					var fi = mm.Member as FieldInfo;
					if (fi != null)
					{
						fi.SetValue(item, mm.OriginalValue);
					}
				}
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGW.Common
{
	public class BaseFactory
	{
		private List<InterfaceReference> Container;

		public void Add(Type interfaceType, Type classType)
		{
			Container.Add(new InterfaceReference(interfaceType, classType, null));
		}

		public BaseFactory()
		{
			Container = new List<InterfaceReference>();
		}

		public T GetInstance<T>()
		{
			var obj = Container.Where(c => c.InterfaceType.Equals(typeof(T))).FirstOrDefault();
			if (obj == null)
				throw new Exception(string.Format("Interface initializer not found: {0}.", typeof(T).Name));

			if (obj.Instance == null)
				obj.Instance = System.Activator.CreateInstance(obj.ClassType);

			return (T)obj.Instance;
		}
	}
	public class InterfaceReference
	{
		public Type InterfaceType {get; set;}
		public Type ClassType {get; set;}
		public object Instance {get; set;}
		public InterfaceReference(Type interfaceType, Type classType, object instance)
		{
			this.InterfaceType = interfaceType;
			this.ClassType = classType;
			this.Instance = instance;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Csla;
using Csla.Core;
using Csla.Reflection;

namespace Business {
	[Serializable]
	public abstract class LogObject<T> : BusinessBase<T> where T : LogObject<T> {
		protected Dictionary<string, object> TakeSnapshot() {
			var currentType = GetType();
			var snapshot = new Dictionary<string, object>();

			using (BypassPropertyChecks) {
				do {
					if (currentType == null) continue;
					var props = currentType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

					foreach (var prop in props) {
						var propName = prop.Name.ToLower();
						var customAttributes = prop.GetCustomAttributes().ToList();

						if (
							prop.DeclaringType == currentType && 
							!snapshot.ContainsKey(propName) && 
							!typeof(IUndoableObject).IsAssignableFrom(prop.PropertyType) &&
							prop.GetIndexParameters().Length == 0
						) {
							snapshot.Add(propName, prop.GetValue(this, null));
						}
					}

#if !XAMARIN
					currentType = currentType.BaseType;
#else
					currentType = currentType.BaseType();
#endif

				} while (currentType != typeof(LogObject<T>));
			}
			return snapshot;
		}
	}
}
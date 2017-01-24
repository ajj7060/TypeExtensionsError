using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Csla;

namespace Business {
	[Serializable]
	public class BusinessObject : LogObject<BusinessObject> {
		public static readonly PropertyInfo<string> SomeProperty = RegisterProperty<string>(x => x.Some);
		public string Some {
			get { return GetProperty(SomeProperty); }
			set { SetProperty(SomeProperty, value); }
		}

		public static Task<BusinessObject> NewBusinessObjectAsync() {
			return DataPortal.CreateAsync<BusinessObject>();
		}

		[RunLocal]
		protected override void DataPortal_Create() {
			Some = "some value";

			foreach (var snapshotValue in TakeSnapshot()) {
				Debug.WriteLine($"{snapshotValue.Key} = {snapshotValue.Value}");
			}
		}
	}
}
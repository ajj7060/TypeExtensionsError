using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business;
using Xamarin.Forms;

#if WINDOWS_APP || WINDOWS_PHONE_APP || WINDOWS_UWP
using Csla.Reflection;
#endif

namespace TypeExtensionsError {
	public partial class App : Application {
		public App() {
			InitializeComponent();

			var busObjTypes = new[] {
				GetType(),
				typeof(BusinessObject)
			};

#if WINDOWS_APP || WINDOWS_PHONE_APP || WINDOWS_UWP
			var assemblies = busObjTypes.Select(x => x.Assembly());
#else
			var assemblies = busObjTypes.Select(x => x.Assembly);
#endif

			var list = "";
			foreach (var assembly in assemblies) {
				list += $"{assembly.FullName};";
			}

			MainPage = new TypeExtensionsError.MainPage();
		}

		protected override void OnStart() {
			// Handle when your app starts
		}

		protected override void OnSleep() {
			// Handle when your app sleeps
		}

		protected override void OnResume() {
			// Handle when your app resumes
		}
	}
}

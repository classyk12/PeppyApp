using FormsControls.Base;
using Peppy2.Helpers;
using Peppy2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Peppy2.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PeppyMasterDetail : MasterDetailPage
	{
		public PeppyMasterDetail ()
		{
			InitializeComponent ();
            mPage.listView.ItemSelected += OnItemSelected;

        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItems;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                mPage.listView.SelectedItem = null;
                IsPresented = false;
            }

        }

      



    }
}
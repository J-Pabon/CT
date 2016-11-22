using System;

using Xamarin.Forms;

namespace CT
{
	public class MainPage : ContentPage
	{
		Label lblTitle, lblMessage;
		ListView lstvCases;
		Button btnAdd, btnUpdate, btnDelete;

		public MainPageViewModel _vm
		{
			get { return this.BindingContext as MainPageViewModel; }
		}

		public MainPage()
		{
			this.BindingContext = new MainPageViewModel();

			lblTitle = new Label
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				HorizontalTextAlignment = TextAlignment.Center,
				Text = "Current cases"
			};

			var viewTemplateCaseItem = new DataTemplate(typeof(CaseItem));

			lstvCases = new ListView
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.FillAndExpand,
				HasUnevenRows = true,
				Margin = new Thickness(2),
				ItemTemplate = viewTemplateCaseItem
			};
			lstvCases.SetBinding(ListView.ItemsSourceProperty, new Binding("Cases"));
			lstvCases.ItemTapped += (sender, e) => {
				var case_item = (Case)e.Item;

				if (_vm.CaseSelected != null)
				{
					if (_vm.CaseSelected.Id == case_item.Id)
					{
						((ListView)sender).SelectedItem = null; // de-select the row

						_vm.IsCaseSelected = false;
						_vm.CaseSelected = null;

						return;
					}
					else
					{
						_vm.IsCaseSelected = true;
						_vm.CaseSelected = case_item;
					}
				}
				else
				{
					_vm.IsCaseSelected = true;
					_vm.CaseSelected = case_item;
				}
			};

			lblMessage = new Label
			{
				HorizontalOptions = LayoutOptions.StartAndExpand,
				HorizontalTextAlignment = TextAlignment.Start,
				FontAttributes = FontAttributes.Italic
			};
			lblMessage.SetBinding(Label.TextProperty, new Binding("Message"));

			btnAdd = new Button
			{
				HorizontalOptions = LayoutOptions.Fill,
				Text = "Add"
			};
			btnAdd.SetBinding(Button.CommandProperty, new Binding("AddCaseCommand"));

			btnUpdate = new Button
			{
				HorizontalOptions = LayoutOptions.Fill,
				Text = "Update"
			};
			btnUpdate.SetBinding(Button.CommandProperty, new Binding("UpdateCaseCommand"));
			btnUpdate.SetBinding(IsEnabledProperty, new Binding("IsCaseSelected"));

			btnDelete = new Button
			{
				HorizontalOptions = LayoutOptions.Fill,
				Text = "Delete"
			};
			btnDelete.SetBinding(Button.CommandProperty, new Binding("DeleteCaseCommand"));
			btnDelete.SetBinding(IsEnabledProperty, new Binding("IsCaseSelected"));

			StackLayout stackButtons = new StackLayout
			{
				HorizontalOptions = LayoutOptions.Fill,
				Orientation = StackOrientation.Horizontal,
				Children = { 
					btnAdd, btnUpdate, btnDelete
				}
			};

			StackLayout stackMain = new StackLayout
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Margin = new Thickness(2),
				Children = {
					lblTitle,
					lstvCases,
					lblMessage,
					stackButtons
				}
			};

			Content = stackMain;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			_vm.GetCasesList();
		}

		protected override bool OnBackButtonPressed()
		{//Si no se controla el evento, la aplicación se cierra y al reiniciar, saca error (?)
			return true;
		}
	}
}


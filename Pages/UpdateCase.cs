using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CT
{
	public class UpdateCase : ContentPage
	{
		Label lblTitle, lblMessage;
		Entry entName, entDescription;
		Button btnUpdate;

		Case old_case;

		public UpdateCase(Case case_info)
		{
			old_case = case_info;

			lblTitle = new Label
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				HorizontalTextAlignment = TextAlignment.Center,
				FontSize = 16,
				FontAttributes = FontAttributes.Bold,
				Text = "Update this case"
			};

			entName = new Entry
			{
				Keyboard = Keyboard.Text,
				Placeholder = "Enter the new name for the case"
			};
			entName.Text = old_case.Name;

			entDescription = new Entry
			{
				Keyboard = Keyboard.Text,
				Placeholder = "Enter the new description"
			};
			entDescription.Text = old_case.Description;

			btnUpdate = new Button
			{
				HorizontalOptions = LayoutOptions.Center,
				Text = "Update"
			};
			btnUpdate.Command = new Command(async () =>
			{
				if (entName.Text != null && entDescription.Text != null)
				{
					lblMessage.Text = "";

					old_case.Name = entName.Text;
					old_case.Description = entDescription.Text;


					var json = JsonConvert.SerializeObject(old_case);
					var content = new StringContent(json, Encoding.UTF8, "application/json");

					var httpClient = new HttpClient();
					httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.Token);


					HttpResponseMessage response = await httpClient.PutAsync(new Uri(Constants.URI_CASES+"/"+old_case.Id), content);

					if (response.IsSuccessStatusCode)
					{ await ((NavigationPage)Application.Current.MainPage).PopToRootAsync(); }
					else { lblMessage.Text = string.Format("Error ({0}): {1}", response.StatusCode, response.ReasonPhrase); }
				}
				else { lblMessage.Text = "Please enter a name and a description for the case."; }
			});

			lblMessage = new Label
			{
				HorizontalOptions = LayoutOptions.StartAndExpand,
				HorizontalTextAlignment = TextAlignment.Start,
				FontAttributes = FontAttributes.Italic
			};

			StackLayout stackUpdateCase = new StackLayout
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Margin = new Thickness(2),
				Spacing = 4,
				Children = {
					lblTitle,
					entName,
					entDescription,
					btnUpdate,
					lblMessage
				}
			};

			Content = stackUpdateCase;
		}
	}
}
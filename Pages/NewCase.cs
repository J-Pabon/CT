using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CT
{
	public class NewCase : ContentPage
	{
		Label lblTitle, lblMessage;
		Entry entName, entDescription;
		Button btnSave;

		public NewCase()
		{
			lblTitle = new Label
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				HorizontalTextAlignment = TextAlignment.Center,
				FontSize = 16,
				FontAttributes = FontAttributes.Bold,
				Text = "Add a new case"
			};

			entName = new Entry
			{
				Keyboard = Keyboard.Text,
				Placeholder = "Enter the name of the new case"
			};

			entDescription = new Entry
			{
				Keyboard = Keyboard.Text,
				Placeholder = "Enter the description"
			};

			btnSave = new Button
			{
				HorizontalOptions = LayoutOptions.Center,
				Text = "Save"
			};
			btnSave.Command = new Command(async() => {
				if (entName.Text != null && entDescription.Text != null)
				{
					lblMessage.Text = "";

					Case new_case = new Case
					{
						Id = Guid.NewGuid(),
						Name = entName.Text,
						Description = entDescription.Text
					};


					var json = JsonConvert.SerializeObject(new_case);
					var content = new StringContent(json, Encoding.UTF8, "application/json");

					var httpClient = new HttpClient();
					httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.Token);

					HttpResponseMessage response = await httpClient.PostAsync(new Uri(Constants.URI_CASES), content);

					if (response.IsSuccessStatusCode)
					{ await ((NavigationPage)Application.Current.MainPage).PopToRootAsync(); }
					else { lblMessage.Text = string.Format("Error ({0}): {1}", response.StatusCode, response.ReasonPhrase); }
				}
				else { lblMessage.Text = "Please enter a name and a description for the new case."; }
			});

			lblMessage = new Label
			{ 
				HorizontalOptions = LayoutOptions.StartAndExpand,
				HorizontalTextAlignment = TextAlignment.Start,
				FontAttributes = FontAttributes.Italic
			};

			StackLayout stackNewCase = new StackLayout
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Margin = new Thickness(2),
				Spacing = 4,
				Children = { 
					lblTitle,
					entName,
					entDescription,
					btnSave,
					lblMessage
				}
			};

			Content = stackNewCase;
		}
	}
}


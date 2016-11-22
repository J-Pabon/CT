using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace CT
{
	public class LoginViewModel : BaseViewModel
	{
		private string username;
		public string UserName
		{
			get { return username; }
			set
			{
				username = value;
				OnPropertyChanged();
			}
		}

		private string password;
		public string Password
		{
			get { return password; }
			set
			{
				password = value;
				OnPropertyChanged();
			}
		}

		private string message;
		public string Message
		{
			get { return message; }
			set
			{
				message = value;
				OnPropertyChanged();
			}
		}

		public LoginViewModel()
		{
			UserName = "";
			Password = "";

			Message = "";
		}

		private Command<object> loginCommand;
		public Command<object> LoginCommand
		{
			get
			{
				return loginCommand ?? (new Command<object>(ExecuteLoginCommand));
			}
		}

		public virtual async void ExecuteLoginCommand(object sender)
		{
			if (ValidateUser() && ValidatePassword())
			{
				var httpClient = new HttpClient();
				HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Constants.URI_LOGIN);
				var parameters = new Dictionary<string, string>
				{
					{ Constants.USERNAME, UserName },
					{ Constants.PASSWORD, Password }
				};

				request.Content = new FormUrlEncodedContent(parameters);

				var response = await httpClient.SendAsync(request);
				var token = await response.Content.ReadAsStringAsync();

				if (token.Contains(Constants.ACCESS_TOKEN))
				{
					dynamic json = JObject.Parse(token);
					App.Token = json.AccessToken;

					Message = "";

					Application.Current.MainPage = new NavigationPage(new MainPage());
				}
				else
				{ Message = token;}
			} else
			{
				if (!ValidateUser() && !ValidatePassword())
				{ Message = "Please enter your username and password"; }
				else {
					if (!ValidateUser())
					{ Message = "Please enter your username"; }
					else { Message = "Please enter your password"; }
				}
			}
		}

		private bool ValidateUser()
		{
			return !this.UserName.Equals("");
		}

		private bool ValidatePassword()
		{
			return !this.Password.Equals("");
		}
	}
}

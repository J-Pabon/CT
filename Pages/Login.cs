using System;

using Xamarin.Forms;

namespace CT
{
	public class Login : ContentPage
	{
		Entry entUser, entPassword;
		Label lblMessage;

		public LoginViewModel _vm
		{
			get { return this.BindingContext as LoginViewModel; }
		}

		public Login()
		{
			this.BindingContext = new LoginViewModel();

			entUser = new Entry
			{
				Keyboard = Keyboard.Plain,
				Placeholder = "Enter your user name"
			};
			entUser.SetBinding(Entry.TextProperty, new Binding("UserName"));

			entPassword = new Entry
			{
				Keyboard = Keyboard.Plain,
				Placeholder = "Enter your password",
				IsPassword = true
			};
			entPassword.SetBinding(Entry.TextProperty, new Binding("Password"));

			Button btnLogin = new Button
			{
				HorizontalOptions = LayoutOptions.Center,
				BackgroundColor = Color.Transparent,
				Text = "Login",
				FontAttributes = FontAttributes.Bold
			};
			btnLogin.SetBinding(Button.CommandProperty, new Binding("LoginCommand"));

			lblMessage = new Label
			{
				HorizontalOptions = LayoutOptions.StartAndExpand,
				HorizontalTextAlignment = TextAlignment.Start,
				FontAttributes = FontAttributes.Italic
			};
			lblMessage.SetBinding(Label.TextProperty, new Binding("Message"));

			StackLayout stackLogin = new StackLayout
			{
				IsClippedToBounds = true,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Fill,
				Padding = new Thickness(0, 8, 0, 12),
				Spacing = 10,
				Children = {
					entUser,
					entPassword,
					btnLogin,
					lblMessage
				}
			};


			Content = stackLogin;
		}
	}
}
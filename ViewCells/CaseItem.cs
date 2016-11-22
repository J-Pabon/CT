using System;
using Xamarin.Forms;

namespace CT
{
	public class CaseItem : ViewCell
	{
		public CaseItem()
		{
			Label lblName = new Label
			{
				HorizontalTextAlignment = TextAlignment.Start,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				FontSize = 10,
				FontAttributes = FontAttributes.Bold
			};
			lblName.SetBinding(Label.TextProperty, new Binding("Name"));

			Label lblDescription = new Label
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalTextAlignment = TextAlignment.Start,
				FontSize = 12
			};
			lblDescription.SetBinding(Label.TextProperty, new Binding("Description"));

			StackLayout stackCaseItem = new StackLayout
			{
				VerticalOptions = LayoutOptions.Fill,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness(2),
				Spacing = 5,
				Children = {
					lblName,
					lblDescription
				}
			};

			this.View = stackCaseItem;
		}
	}
}
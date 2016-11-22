using System;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CT
{
	public class Case
	{
		[JsonProperty("id")]
		public Guid Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("description")]
		public string Description { get; set; }

		public static implicit operator Case(SelectedItemChangedEventArgs v)
		{
			throw new NotImplementedException();
		}
	}
}

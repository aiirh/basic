using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Aiirh.WebTools.Http {
	public class HttpRequestDescription {
		public string FromSystem { get; set; }
		public string ToSystem { get; set; }
		public string Reference1 { get; set; }
		public string Reference2 { get; set; }
		public string Action { get; set; }
		public string Url { get; set; }
		public RequestMethod Method { get; set; }
		public AuthenticationHeaderValue AuthenticationHeader { get; set; }
		public Dictionary<string, string> DefaultHttpHeaders { get; set; }
		public bool IsResponseNotExpected { get; set; }

		public HttpRequestDescription Clone() {
			return (HttpRequestDescription)MemberwiseClone();
		}
	}

	public enum RequestMethod : short {
		Get = 1,
		Post = 2,
		Put = 3,
		Delete = 4
	}
}

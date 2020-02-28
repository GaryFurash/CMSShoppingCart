using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSShoppingCart.Infrastructure
{
	public static class SessionExtensions
	{
		// this ... sets up this method as an extension method. in this example
		// using this ISession as the first parameter means that you'll be able
		// to use the syntax [ISession Object].SetJson as if SetJson was a method
		// for ISession
		public static void SetJson(this ISession session, string key, object value)
		{
			session.SetString(key, JsonConvert.SerializeObject(value));
		}

		public static T GetJson<T>(this ISession session, string key)
		{
			var sessionData = session.GetString(key);
			return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
		}
	}
}

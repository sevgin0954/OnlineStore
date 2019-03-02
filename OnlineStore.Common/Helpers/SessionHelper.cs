using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OnlineStore.Common.Constants;
using System.Collections.Generic;

namespace OnlineStore.Common.Helpers
{
    public static class SessionHelper
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            if (value == null)
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}

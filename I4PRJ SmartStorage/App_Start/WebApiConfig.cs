﻿using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace I4PRJ_SmartStorage.UI
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      // Web API configuration and services
      var settings = config.Formatters.JsonFormatter.SerializerSettings;
      settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
      settings.Formatting = Formatting.Indented;

      // Web API routes
      config.MapHttpAttributeRoutes();

      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/{controller}/{action}/{id}",
          defaults: new { action = "DefaultAction", id = RouteParameter.Optional }
      );
    }
  }
}
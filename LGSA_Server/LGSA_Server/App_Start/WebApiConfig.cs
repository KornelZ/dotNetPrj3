﻿using LGSA.Model.UnitOfWork;
using LGSA_Server.App_Start;
using LGSA_Server.Model.Services.TransactionLogic;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LGSA_Server
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            var container = new UnityContainer();
            container.RegisterType<IUnitOfWorkFactory, DbUnitOfWorkFactory>(new HierarchicalLifetimeManager());
            container.RegisterType<IRatingUpdater, RatingUpdater>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            var jsonSettings = config.Formatters.JsonFormatter;
            jsonSettings.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Filters.Add(new GlobalExceptionFilterAttribute());
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

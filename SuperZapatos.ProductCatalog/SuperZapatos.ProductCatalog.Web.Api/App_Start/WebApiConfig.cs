using Microsoft.Practices.Unity;
using SuperZapatos.ProductCatalog.Entity;
using SuperZapatos.ProductCatalog.Service;
using SuperZapatos.ProductCatalog.Web.Api.ActionFilters;
using SuperZapatos.ProductCatalog.Web.Api.Resolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SuperZapatos.ProductCatalog.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            container.RegisterType<IArticleService, ArticleService>(new HierarchicalLifetimeManager());
            container.RegisterType<IStoreService, StoreService>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);


            // Web API routes
            config.MapHttpAttributeRoutes();
            config.MessageHandlers.Add(new WrappingHandler());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "services/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "2",
                routeTemplate: "services/{controller}/{action}/{id}",
                defaults: new { action = "get", id = RouteParameter.Optional }
            );
        }
    }
}

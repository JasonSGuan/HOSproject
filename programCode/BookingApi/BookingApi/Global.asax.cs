using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Routing;
using Unity;
using BookingApi.IRepository;
using BookingApi.Repository;

namespace BookingApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //  πapi∑µªÿŒ™json 
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            //ioc ≈‰÷√ 
            IUnityContainer unityContainer = new UnityContainer();

            unityContainer.RegisterType<LoginIRepository, LoginRepository>();
            unityContainer.RegisterType<RecordIRepository, RecordRepository>();
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new Filters.UnityHttpControllerActivator(unityContainer));
        }
    }
}

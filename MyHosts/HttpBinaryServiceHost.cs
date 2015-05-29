using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using Claudiu.ServicesHostFactory.MyBindings.MyBinarryHttp;
using System.Diagnostics;
using System.Xml.Linq;
using System.Configuration;
using System.Web.Configuration;
using ClaudiuHostFactory.Conf;

namespace ClaudiuHostFactory.MyHosts
{
    public class HttpBinarryServiceHost : ServiceHost
    {
        Type serviceType;

        ServiceDebugBehavior serviceDebugBehavior = null;
        ServiceMetadataBehavior serviceMetadataBehavior = null;

        public HttpBinarryServiceHost(Type serviceType, params Uri[] addresses)
        {
            this.serviceType = serviceType;

            base.InitializeDescription(serviceType, new UriSchemeKeyedCollection(addresses));

#if DEBUG
            //Debugger.Break();

            #region ServiceDebugBehavior
            if (base.Description.Behaviors.Contains(typeof(ServiceDebugBehavior)))
            {
                IServiceBehavior iDebugBehaviour = base.Description.Behaviors.Where(it => it.GetType() == typeof(ServiceDebugBehavior)).FirstOrDefault();
                serviceDebugBehavior = iDebugBehaviour as ServiceDebugBehavior;
            }
            else
            {
                serviceDebugBehavior = new ServiceDebugBehavior();
                base.Description.Behaviors.Add(serviceDebugBehavior);
            }
            serviceDebugBehavior.IncludeExceptionDetailInFaults = true;
            #endregion

            #region ServiceMetadataBehavior

            if (base.Description.Behaviors.Contains(typeof(ServiceMetadataBehavior)))
            {
                IServiceBehavior iServiceMetadataBehavior = base.Description.Behaviors.Where(it => it.GetType() == typeof(ServiceMetadataBehavior)).FirstOrDefault();
                serviceMetadataBehavior = iServiceMetadataBehavior as ServiceMetadataBehavior;
            }
            else
            {
                serviceMetadataBehavior = new ServiceMetadataBehavior();
                base.Description.Behaviors.Add(serviceMetadataBehavior);
            }
            serviceMetadataBehavior.HttpGetEnabled = true;

            #endregion


          //  CompilationSection configSection =
          //(CompilationSection)ConfigurationManager.GetSection("system.web/compilation");
          //  configSection.Debug = false;
#else
            #region ServiceMetadataBehavior
             if (base.Description.Behaviors.Contains(typeof(ServiceMetadataBehavior)))
            {
                IServiceBehavior iServiceMetadataBehavior = base.Description.Behaviors.Where(it => it.GetType() == typeof(ServiceMetadataBehavior)).FirstOrDefault();
                serviceMetadataBehavior = iServiceMetadataBehavior as ServiceMetadataBehavior;
            }
            else
            {
                serviceMetadataBehavior = new ServiceMetadataBehavior();
                base.Description.Behaviors.Add(serviceMetadataBehavior);
            }
            serviceMetadataBehavior.HttpGetEnabled = false;
            #endregion
#endif


        }

        protected override void InitializeRuntime()
        {
            //TimeSpan bigTimeSpan = new TimeSpan(10, 0, 0);
            //TimeSpan smallTimeSpan = new TimeSpan(0, 0, 5);
            //TimeSpan delayTimeSpan = new TimeSpan(0, 0, 0);


            // Add an endpoint for the given service contract.
            List<Type> interfaces = serviceType.GetInterfaces().ToList();

            this.AddServiceEndpoint(
             interfaces[0],

             new BasicHttpBinding()
             {

             },

             "basic"
             );

            this.AddServiceEndpoint(
               interfaces[0],

               new CustomHttpBinaryBinding()
               {

               },

               "httpBinarry"
               );

            this.AddServiceEndpoint(
               interfaces[0],

               new CustomBasicGZipHttpBinding(false, true, false)
               {

               },

               "basicHttpGZip"
               );

            bool includenetTcp = ConfigurationHelpers.GetAppSettingsValueOrDefault<bool>("includenettcp", false);
           
            if (includenetTcp)
            {
                this.AddServiceEndpoint(
                 interfaces[0],
                 new NetTcpBinding(SecurityMode.None)
                 {

                 },

                 "netTcp"
                 );

                this.AddServiceEndpoint(
               typeof(IMetadataExchange),
               MetadataExchangeBindings.CreateMexTcpBinding(),
               "mex");
            }
            else {
                this.AddServiceEndpoint(
                   typeof(IMetadataExchange),
                   MetadataExchangeBindings.CreateMexHttpBinding(),
                   "mex");
            }

            // Add a metadata endpoint.

            #if DEBUG
            
            
            #else
           
            #endif



            base.InitializeRuntime();
        }
    }

}

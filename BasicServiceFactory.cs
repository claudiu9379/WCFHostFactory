using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using Claudiu.ServicesHostFactory.MyBindings.MyBinarryHttp;
using System.ServiceModel.Configuration;
using ClaudiuHostFactory.MyHosts;

namespace Claudiu.ServicesHostFactory
{
    //v1.2 10 may 2013
    public class BasicServiceFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
           HttpBinarryServiceHost host = new HttpBinarryServiceHost(serviceType,baseAddresses);
           return host;
        }
    }

   
}

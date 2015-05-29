using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Diagnostics;

namespace Claudiu.ServicesHostFactory.MyBindings.MyBinarryHttp
{
    public class CustomHttpBinaryBinding : CustomBinding
    {
        BinaryMessageEncodingBindingElement encoding;
        public override BindingElementCollection CreateBindingElements()
        {
            BindingElementCollection bindingElementCollection = base.CreateBindingElements();

            //Debugger.Break();
            BindingElement encodingElement = bindingElementCollection.FirstOrDefault(bindingElement => bindingElement is BinaryMessageEncodingBindingElement);

            if (encodingElement == null)
            {
                encoding = new BinaryMessageEncodingBindingElement();

                bindingElementCollection.Insert(0,encoding);

               
            }
            else
            {
                //_log.Warn("Encoding not found");
            }

            return bindingElementCollection;
        }

        public CustomHttpBinaryBinding()
        {

            HttpTransportBindingElement transport = new HttpTransportBindingElement();
            this.Elements.Add(transport);
        }
    }
}

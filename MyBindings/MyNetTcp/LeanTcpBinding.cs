using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Claudiu.ServicesHostFactory.MyBindings.MyNetTcp
{
    public class LeanTcpBinding : NetTcpBinding
    {
        public override BindingElementCollection CreateBindingElements()
        {
            BindingElementCollection bindingElementCollection = base.CreateBindingElements();
            BindingElement encodingElement = bindingElementCollection.FirstOrDefault(
                bindingElement => bindingElement is BinaryMessageEncodingBindingElement);

            if (encodingElement != null)
            {
                int index = bindingElementCollection.IndexOf(encodingElement);
                bindingElementCollection.RemoveAt(index);
                bindingElementCollection.Insert(index, new LeanBinaryMessageEncodingBindingElement());
            }
            else
            {
                //_log.Warn("Encoding not found");
            }

            return bindingElementCollection;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Diagnostics;
using Microsoft.Samples.GZipEncoder;

namespace Claudiu.ServicesHostFactory.MyBindings.MyBinarryHttp
{
    public class CustomBasicGZipHttpBinding : CustomBinding
    {
        private readonly bool useHttps;
        private readonly bool useBinaryEncoding = true;
        private readonly bool useCompression = false;
        private readonly HttpTransportBindingElement transport;

        public CustomBasicGZipHttpBinding(bool useHttps, bool binaryEncoding, bool compressMessages)
        {
            this.useHttps = useHttps;
            transport = useHttps ? new HttpsTransportBindingElement() : new HttpTransportBindingElement();
            useBinaryEncoding = binaryEncoding;
            useCompression = compressMessages;

            this.Elements.Add(transport);
            this.Name = "CustomBasicGZipHttpBinding";
        }

        public long MaxMessageSize
        {
            set
            {
                transport.MaxReceivedMessageSize = value;
                transport.MaxBufferSize = (int)value;
            }
        }

        public override BindingElementCollection CreateBindingElements()
        {
            BindingElementCollection bindingElementCollection = base.CreateBindingElements();

            BindingElement security;
            if (useHttps)
            {
                security = SecurityBindingElement.CreateSecureConversationBindingElement(
                    SecurityBindingElement.CreateUserNameOverTransportBindingElement());
            }
            else
            {
                security = SecurityBindingElement.CreateSecureConversationBindingElement(
                    SecurityBindingElement.CreateUserNameForSslBindingElement(true));
            }

            MessageEncodingBindingElement encoding;
            if (useCompression)
            {
                encoding = new GZipMessageEncodingBindingElement(useBinaryEncoding
                                                                    ? (MessageEncodingBindingElement)
                                                                      new BinaryMessageEncodingBindingElement()
                                                                    : new TextMessageEncodingBindingElement());
            }
            else
            {
                encoding = useBinaryEncoding
                            ? (MessageEncodingBindingElement)new BinaryMessageEncodingBindingElement()
                            : new TextMessageEncodingBindingElement();
            }

            //bindingElementCollection.Insert(0, security);
            bindingElementCollection.Insert(0, encoding);
            //bindingElementCollection.Insert(0, transport);


            return bindingElementCollection;
            //return new BindingElementCollection(new[]
            //{
            //    security,
            //    encoding,
            //    transport,
            //});
        }
    }
}



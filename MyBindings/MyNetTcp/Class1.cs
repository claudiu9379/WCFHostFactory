using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;

namespace Claudiu.ServicesHostFactory.MyBindings.MyNetTcp
{
    internal class LeanBinaryMessageEncodingBindingElement : MessageEncodingBindingElement
    {
        private readonly BinaryMessageEncodingBindingElement _bindingElement;

        /// 
        /// Initializes a new instance of the  class.
        /// 
        public LeanBinaryMessageEncodingBindingElement()
        {
            _bindingElement = new BinaryMessageEncodingBindingElement();
        }

        /// 
        /// Initializes a new instance of the  class.
        /// 
        /// The binding element.
        public LeanBinaryMessageEncodingBindingElement(BinaryMessageEncodingBindingElement bindingElement)
        {
            _bindingElement = bindingElement;
        }

        /// 
        /// Initializes a new instance of the  class.
        /// 
        /// The element to be cloned.
        /// The binding element.
        public LeanBinaryMessageEncodingBindingElement(MessageEncodingBindingElement elementToBeCloned, BinaryMessageEncodingBindingElement bindingElement)
            : base(elementToBeCloned)
        {
            _bindingElement = bindingElement;
        }

        /// 
        /// When overridden in a derived class, returns a copy of the binding element object.
        /// 
        /// 
        /// A  object that is a deep clone of the original.
        /// 
        public override BindingElement Clone()
        {
            return new LeanBinaryMessageEncodingBindingElement(
                (BinaryMessageEncodingBindingElement)_bindingElement.Clone());
        }

        /// 
        /// When overridden in a derived class, creates a factory for producing message encoders.
        /// 
        /// 
        /// The  used to produce message encoders.
        /// 
        //public override MessageEncoderFactory CreateMessageEncoderFactory()
        //{
        //    return new LeanBinaryMessageEncoderFactory(_bindingElement.CreateMessageEncoderFactory());
        //}

        /// 
        /// When overridden in a derived class, gets or sets the message version that can be handled by the message encoders produced by the message encoder factory.
        /// 
        /// 
        /// The  used by the encoders produced by the message encoder factory.
        /// 
        public override MessageVersion MessageVersion
        {
            get { return _bindingElement.MessageVersion; }
            set { _bindingElement.MessageVersion = value; }
        }
    }

}

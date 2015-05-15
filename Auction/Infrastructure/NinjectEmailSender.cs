using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Auction.Domain.Abstract;
using Auction.Domain.EmailSender;
using Ninject.Modules;

namespace Auction.Infrastructure
{
    public class NinjectEmailSender:NinjectModule
    {
        public override void Load()
        {
            this.Bind<IEmailSender>().To<GoogleEmailSender>();
        }

    }
}
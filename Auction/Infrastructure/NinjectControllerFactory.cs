using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;
using Moq;
using Ninject;
using Auction.Domain.DBase;
using Auction.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Auction.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        public IKernel ninjectKernel { get; private set; }

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
              ? null
              : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            ninjectKernel.Bind<ILotsRepository>().To<LotRepository>();
            ninjectKernel.Bind<IBidsRepository>().To<BidRepository>();

        }
    }
}
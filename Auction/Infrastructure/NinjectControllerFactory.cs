using System;
using System.Web.Mvc;
using System.Web.Routing;
using Auction.Domain.Abstract;
using Ninject;
using Auction.Domain.DBase;

namespace Auction.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel NinjectKernel { get; set; }

        public NinjectControllerFactory()
        {
            NinjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
              ? null
              : (IController)NinjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            NinjectKernel.Bind<ILotsRepository>().To<LotRepository>();
            NinjectKernel.Bind<IBidsRepository>().To<BidRepository>();
            NinjectKernel.Bind<ICategoriesRepository>().To<CategoryRepository>();

        }
    }
}
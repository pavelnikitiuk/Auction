using System;
using System.Linq;
using Auction.Domain.Abstract;
using Auction.Domain.Entities;
using Auction.Infrastructure;
using Ninject;

namespace Auction.Workflow
{
    public static class LotEndChecker
    {
        private static readonly ILotsRepository lotsRepository;
        static LotEndChecker()
        {
            NinjectControllerFactory factory = new NinjectControllerFactory();
            IKernel appKernel = factory.ninjectKernel;
            lotsRepository = appKernel.Get<ILotsRepository>();
        }

        public static void SearchEndedLots()
        {
            var lots = lotsRepository.Lots.Where(x => x.EndTime <= DateTime.Now).ToArray();
            if (lots.Any())
            {
                foreach (var lot in lots)
                {

                    lotsRepository.EndLot(lot);
                }
            }
        }
    }
}
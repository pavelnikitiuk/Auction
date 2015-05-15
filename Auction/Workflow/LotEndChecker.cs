using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using Auction.Controllers;
using Auction.Domain.Abstract;
using Auction.Domain.EmailSender;
using Auction.Domain.Entities;
using Auction.Infrastructure;
using Auction.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using Ninject.Activation;

namespace Auction.Workflow
{
    public static class LotEndChecker
    {
        private static readonly ILotsRepository lotsRepository;
        private static UserManager<ApplicationUser> userManager;
        private static IEmailSender emailSender;
        private static EmailModel emailModel;
        static LotEndChecker()
        {
            NinjectControllerFactory factory = new NinjectControllerFactory();
            IKernel appKernel = factory.ninjectKernel;
            lotsRepository = appKernel.Get<ILotsRepository>();
            appKernel= new StandardKernel(new NinjectEmailSender());
            emailSender = appKernel.Get<IEmailSender>();
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            emailModel = new EmailModel
            {
                From = "auctionsuport@gmail.com",
                Pass = "somepassword",
                Subject = "Thanks for by from Auction"
            };

        }

        public static void SearchEndedLots()
        {
            var lots = lotsRepository.Lots.Where(x => x.EndTime <= DateTime.Now && x.IsCompleted == false).ToArray();
            if (lots.Any())
            {
                foreach (var lot in lots)
                {
                    lotsRepository.EndLot(lot);
                    var winerId = lot.Bids.Last().UserId;
                    var winer = userManager.FindById(winerId);
                    if (winer != null)
                    {
                        emailModel.Body = String.Format("Dear {0}\n\nYou has been wined Auction Lot {1}, it's cost {2}\n\nThe Auction Support", winer.FirstName,lot.Name,lot.CurrentPrice);
                        emailModel.To = winer.Email;
                        emailSender.Send(emailModel);
                    }
                    
                }
            }
        }
    }
}
using System;
using System.Linq;
using System.Net.Mail;
using Auction.Domain.DBase;
using Auction.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Quartz;

namespace Auction.Scheduler
{
    public class CheckEndJob : IJob, IDisposable
    {
        private bool disposed;
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        private AuctionDbContext context = new AuctionDbContext();
        /// <summary>
        /// Execute job
        /// </summary>
        /// <param name="con"></param>
        public void Execute(IJobExecutionContext con)
        {
            var lots = context.Lots.Where(x => x.EndTime <= DateTime.Now && x.IsCompleted == false);
            foreach (var lot in lots.ToList())
            {
                lot.IsCompleted = true;
                context.SaveChanges();
                if (lot.Bids.Any())
                {
                    var winerId = lot.Bids.Last().UserId;
                    var winer = userManager.FindById(winerId);
                    if (winer != null)
                    {
                        using (var message = new MailMessage())
                        {
                            message.To.Add(winer.Email);
                            message.Subject = "Test";
                            message.Body =
                                String.Format(
                                    "Dear {0},\n\n thank you for purchase of {1} at the auction price {2}\n\nAuction support",
                                    winer.FirstName, lot.Name, lot.CurrentPrice);
                            SmtpClient smtp = new SmtpClient();
                            smtp.Send(message);
                        }
                    }
                }
            }

        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
            {
                userManager.Dispose();
                context.Dispose();
            }

            disposed = true;
        }

        ~CheckEndJob()
        {
            Dispose(false);
        }
    }
}
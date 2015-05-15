using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auction.Domain.EmailSender;

namespace Auction.Domain.Abstract
{
     public interface IEmailSender
    {
       void Send(EmailModel model);
    }
}

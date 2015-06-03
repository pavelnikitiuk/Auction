using System;

namespace Auction.Domain.DBase
{
    public class BaseRepository:IDisposable
    {
        
        private bool disposed;
        protected  readonly AuctionDbContext Context = new AuctionDbContext();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
                Context.Dispose();
            disposed = true;
        }

        ~BaseRepository()
        {
            Dispose(false);
        }
    }
}

using Model;

namespace DataBaseService.Infrastructure
{
    public class UnitOfWork : IDisposable
    {
        private SprinklerAppDbContext context;
        private GenericRepository<Tank>? tankRepository;

        public GenericRepository<Tank> TankRepository
        {
            get
            {

                if (tankRepository == null)
                {
                    tankRepository = new GenericRepository<Tank>(context);
                }
                return tankRepository;
            }
        }

        private GenericRepository<Sprinkler>? sprinklerRepository;
        public GenericRepository<Sprinkler> SprinklerRepository
        {
            get
            {
                if (sprinklerRepository == null)
                {
                    sprinklerRepository = new GenericRepository<Sprinkler>(context);
                }
                return sprinklerRepository;
            }
        }

        public UnitOfWork(SprinklerAppDbContext sprinklerAppDbContext)
        {
            context = sprinklerAppDbContext;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

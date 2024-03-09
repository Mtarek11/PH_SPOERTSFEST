using Models;

namespace Reposatiory
{
    public class UnitOfWork(RamadanOlympicsContext _dBContext)
    {
        private readonly RamadanOlympicsContext dBContext = _dBContext;

        public async Task CommitAsync()
        {
            await dBContext.SaveChangesAsync();
        }
    }
}

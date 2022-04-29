
using DBMonitor.BLL;
using DBMonitor.DAL.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace DBMonitor.DAL.Services
{
    public class LaunchHistoryDBService : IDBService<LaunchHistory>
    {
        private readonly ApplicationDbContext _context;
        public int Add(LaunchHistory launchHistory)
        {
            var created = _context!.History!.Add(launchHistory);
            _context.SaveChanges();
            return created.Entity.Id;
        }
        public void Delete(int Id)
        {
            if (Id == 0)
            {
                return;
            }

            _context!.History!.Remove(_context!.History!.Find(Id));
            _context!.SaveChanges();
        }
        public LaunchHistory? Get(int Id) => _context!.History!.Find(Id);
        public IEnumerable<LaunchHistory>? GetAll() => _context.History.Include(x => x.Rule).AsEnumerable();

        public async Task<int> AddAsync(LaunchHistory item)
        {
            var ent = await _context!.History!.AddAsync(item);
            await _context.SaveChangesAsync();
            return ent.Entity.Id;
        }
        public async Task DeleteAsync(int Id)
        {
            if (Id == 0)
            {
                return;
            }

            _context!.History.Remove(await _context!.History!.FindAsync(Id));
            await _context!.SaveChangesAsync();
        }
        public async Task<IEnumerable<LaunchHistory>> GetAllAsync() => await Task.Run(() => _context!.History.Include(x => x.Rule).AsEnumerable());
        public async Task<LaunchHistory?> GetAsync(int Id) => await _context!.History!.FindAsync(Id);

        public void Save() => _context.SaveChanges();
        public async Task SaveAsync() => await _context.SaveChangesAsync();
        public LaunchHistory Edit(LaunchHistory changed) => throw new NotImplementedException();
        public Task<LaunchHistory> EditAsync(LaunchHistory changed) => throw new NotImplementedException();

        public LaunchHistoryDBService(ApplicationDbContext dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

    }
}

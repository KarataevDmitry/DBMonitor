
using DBMonitor.BLL;
using DBMonitor.DAL.Interfaces;

namespace DBMonitor.DAL.Services
{
    public class RuleDBService : IDBService<Rule>
    {
        private readonly ApplicationDbContext _context;
        public int Add(Rule rule)
        {
            var ent = _context.Rules.Add(rule);
            _context.SaveChanges();
            return ent.Entity.Id;
        }
        public async Task<int> AddAsync(Rule rule)
        {
            var ent = await _context.Rules.AddAsync(rule);
            await _context.SaveChangesAsync();
            return ent.Entity.Id;
        }
        public void Delete(int Id)
        {
            if (Id == 0)
            {
                return;
            }

            var rule = _context.Rules.Find(Id);
            rule.DeletedAt = DateTime.Now;
            _context.SaveChanges();
        }
        public Rule? Get(int Id) => _context.Rules.Find(Id);
        public IEnumerable<Rule>? GetAll() => _context.Rules.AsEnumerable();

        public async Task DeleteAsync(int Id)
        {
            if (Id == 0)
            {
                return;
            }

            var rule = await _context.Rules.FindAsync(Id);
            rule.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Rule>> GetAllAsync() => await Task.Run(() => _context!.Rules.AsEnumerable());
        public async Task<Rule?> GetAsync(int Id) => await _context!.Rules!.FindAsync(Id);

        public void Save() => _context.SaveChanges();
        public async Task SaveAsync() => await _context.SaveChangesAsync();
        public Rule Edit(Rule value)
        {
            var ent = _context.Rules.Find(value.Id);
            if (ent == null)
            {
                throw new EntityNotFoundException("Rule not found in DB");
            }
            ent.AddedByUser = value.AddedByUser;
            ent.Description = value.Description;
            ent.QueryText = value.QueryText;
            ent.RunAt = value.RunAt;
            ent.Name = value.Name;
            Save();
            return ent;
        }
        public async Task<Rule> EditAsync(Rule value)
        {
            var ent = await _context.Rules.FindAsync(value.Id);
            if (ent == null)
            {
                throw new EntityNotFoundException("Rule not found in DB");
            }
            ent.AddedByUser = value.AddedByUser;
            ent.Description = value.Description;
            ent.QueryText = value.QueryText;
            ent.RunAt = value.RunAt;
            ent.Name = value.Name;
            Save();
            return ent;
        }
        public RuleDBService(ApplicationDbContext dbContext)
        {
            dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _context = dbContext;
        }
    }
}

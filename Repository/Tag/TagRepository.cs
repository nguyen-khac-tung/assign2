using Models;

namespace FUNewsManagement.Repository
{
    public class TagRepository: ITagRepository
    {
        private readonly NewsDBContext _context;

        public TagRepository(NewsDBContext dbContext)
        {
            _context = dbContext;
        }

        public List<Tag> GetAllTag()
        {
            return _context.Tags.ToList();
        }
    }
}

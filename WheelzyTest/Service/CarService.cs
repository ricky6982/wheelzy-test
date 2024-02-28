using Domain;

namespace Service
{
    public class CarService
    {
        @Autowired
        public IApplicationDbContext _context;
        @Autowired
        public IMapper _mapper;

        public IList<CarHistoryDto> GetCarHistory(int carId)
        {
            var results = _context.Query<CarHistory>
                .Include(x => x.User)
                .Include(x => x.Car)
                .Where(x => x.Id == carId && x.Current);

            return results.Select(mapper.map<CarHistoryDto>);
        }
    }

    public class CarHistoryDto
    {
        public int CarId { get; set; }
        public int CarYear { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public string CarSubmodel { get; set; }

        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public bool Current { get; set; }
    }

    public interface IApplicationDbContext
    {
        IQueryable<TEntity> Query<TEntity>() where TEntity : class;
        IQueryable<TEntity> Read<TEntity>() where TEntity : class;
    }
}
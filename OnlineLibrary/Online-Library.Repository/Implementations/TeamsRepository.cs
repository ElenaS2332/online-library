using Online_Library.Domain.Entities;
using Online_Library.Repository.Interfaces;

namespace Online_Library.Repository.Implementations;

public class TeamsRepository : ITeamsRepository
{
    private readonly ApplicationDbContext _context;

    public TeamsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Team> getAllTeams()
    {
        _context.SwitchToPartherDb(true);
        var teams = _context.Teams.ToList();
        _context.SwitchToPartherDb(false);
        return teams;

    }
}
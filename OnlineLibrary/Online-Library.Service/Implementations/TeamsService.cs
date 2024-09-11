using Online_Library.Domain.Entities;
using Online_Library.Repository.Interfaces;

namespace Online_Library.Service.Implementations;

public class TeamsService : ITeamsService
{
    private readonly ITeamsRepository repository;

    public TeamsService(ITeamsRepository repository)
    {
        this.repository = repository;
    }

    public List<Team> GetAllTeams()
    {
        return repository.getAllTeams();
    }
}
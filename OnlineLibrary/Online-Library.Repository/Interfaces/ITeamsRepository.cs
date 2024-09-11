using Online_Library.Domain.Entities;

namespace Online_Library.Repository.Interfaces;

public interface ITeamsRepository
{
    List<Team> getAllTeams();
}
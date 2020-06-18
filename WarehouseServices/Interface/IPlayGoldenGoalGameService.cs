using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Entities;

namespace Warehouse.Services.Interface
{
    public interface IPlayGoldenGoalGameService
    {
        PlayGoldenGoalGame GetById(int Id);
        List<PlayGoldenGoalGame> GetByGoldenGoalGameId(int goldenGoalGameId);
        List<PlayGoldenGoalGame> GetByUserName(string userName);
        void Add(PlayGoldenGoalGame playGoldenGoalGame);
        void Update(PlayGoldenGoalGame playGoldenGoalGame);
        void Delete(PlayGoldenGoalGame playGoldenGoalGame);
    }
}

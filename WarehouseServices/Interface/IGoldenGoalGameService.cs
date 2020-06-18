using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Entities;

namespace Warehouse.Services.Interface
{
    public interface IGoldenGoalGameService
    {
        List<GoldenGoalGame> GetAll();
        GoldenGoalGame GetGoldenGoalGameById(int Id);
        GoldenGoalGame GetGoldenGoalGameByRound(int Round);
        GoldenGoalGame GetGoldenGoalGameLastRound();
        GoldenGoalGame GetGoldenGoalGameByMatchId(long match_id);
        bool CheckUserHavePlayed(int Id, string userName);
        bool CheckIPHavePlayed(int Id, string IP);
        void Add(GoldenGoalGame goldenGoalGame);
        void Update(GoldenGoalGame goldenGoalGame);
        void Awarded(int Id);
        int GetNewRound();

    }
}

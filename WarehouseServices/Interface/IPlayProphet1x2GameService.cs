using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Entities;

namespace Warehouse.Services.Interface
{
    public interface IPlayProphet1x2GameService
    {
        List<PlayProphet1x2Game> GetByProphet1x2GameId(int Prophet1x2GameId);
        List<PlayProphet1x2Game> GetByProphet1x2GameDetailId(int Prophet1x2GameDetailId);
        PlayProphet1x2Game GetById(int Id);
        List<PlayProphet1x2Game> GetByUserName(string userName);
        int CountAnswerCorrect(int Prophet1x2GameId);
        void Add(PlayProphet1x2Game playGoldenGoalGame);
        void Update(PlayProphet1x2Game playGoldenGoalGame);
        void Delete(PlayProphet1x2Game playGoldenGoalGame);
        void DeleteRangeByUserName(string userName);

    }
}

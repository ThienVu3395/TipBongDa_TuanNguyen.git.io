using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Entities;

namespace Warehouse.Services.Interface
{
    public interface IProphet1x2GameService
    {
        List<Prophet1x2Game> GetAll();
        Prophet1x2Game GetById(int Id);
        Prophet1x2Game GetProphet1x2GameByRound(int Round);
        Prophet1x2Game GetProphet1x2GameActive();
        void Add(Prophet1x2Game prophet1x2Game);
        void Update(Prophet1x2Game prophet1x2Game);
        void Delete(Prophet1x2Game prophet1x2Game);
        void Awarded(int Id);
        void Active(Prophet1x2Game prophet1x2Game);
    }
}

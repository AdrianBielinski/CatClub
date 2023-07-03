using CatClub_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatClub_Api.Controllers
{
    [ApiController]
    public class CatClubAPIController : ControllerBase
    {
        public IEnumerable<Cat> GetCats()
        {
            return new List<Cat>()
            {
new Cat{Id=0, Name="Pumczak", Age=1, Description="Pumczak to najgrubszy kot ze stada, lubi się bardzo przytulać i jest mocno zazdrosna."},
new Cat{Id=1,Name="Kita", Age=1, Description="Kita jest najstarszą kocicą, to matka polka stada."},
new Cat{Id=2, Name="Plamka", Age=1, Description="Plamka jest najmłodsza ze stada, była najbardziej schorowana. To troll."}
            };

        }
    }
}

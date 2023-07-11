using CatClub_Api.Models.Dto;

namespace CatClub_Api.Data
{
    public class CatStorage
    {
        public static List<CatDTO> catList = new List<CatDTO>
        {
                new CatDTO{Id=1, Name="Pumczak", Description="Grubasek"},
                new CatDTO{Id=2,Name="Kita", Description="Matka Kotka"},
                new CatDTO{Id=3, Name="Plamka", Description="Trollinho"}
        };
    }
}

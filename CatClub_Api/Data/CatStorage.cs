using CatClub_Api.Models.Dto;

namespace CatClub_Api.Data
{
    public class CatStorage
    {
        public static List<CatDTO> catList = new List<CatDTO>
        {
                new CatDTO{Id=0, Name="Pumczak"},
                new CatDTO{Id=1,Name="Kita"},
                new CatDTO{Id=2, Name="Plamka"}
        };
    }
}

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CatClub_Api.Models.Dto
{
    public class CatDTO
    {
        public int Id { get; set; }

        // Bez właściwości [ApiController] w kontrolerze, walidacja nie zadziała
        // Zamiast [ApiController] możemy w metodzie użyć walidacji za pomocą ModelState.IsValid
        // Jeżeli !ModelState.IsValid zwracamy BadRequest(ModelState)
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Description { get; set; }

    }
}

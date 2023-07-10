using CatClub_Api.Data;
using CatClub_Api.Models;
using CatClub_Api.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;

namespace CatClub_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatClubAPIController : ControllerBase
    {
        #region HttpGet

        [HttpGet("GetCats")]
        public IEnumerable<Cat> GetCats()
        {
            return new List<Cat>()
            {
                new Cat{Id=0, Name="Pumczak", Age=1, Description="Pumczak to najgrubszy kot ze stada, lubi się bardzo przytulać i jest mocno zazdrosna."},
                new Cat{Id=1,Name="Kita", Age=1, Description="Kita jest najstarszą kocicą, to matka polka stada."},
                new Cat{Id=2, Name="Plamka", Age=1, Description="Plamka jest najmłodsza ze stada, była najbardziej schorowana. To troll."}
            };

        }
        
        [HttpGet("GetCatsDTO")]
        public IEnumerable<CatDTO> GetCatsDTO()
        {
            return new List<CatDTO>
            {
                new CatDTO{Id=0, Name="Pumczak"},
                new CatDTO{Id=1,Name="Kita"},
                new CatDTO{Id=2, Name="Plamka"}
            };
        }
        
        [HttpGet("GetCatsDTO_LocalStorage")]
        public IEnumerable<CatDTO> GetCatsDTOFromStorage()
        {
            return CatStorage.catList;
        }

        //Swagger throws exception with custom route annotation
        //HttpGet: /api/CatClubAPI/{id}
        //Route: /api/CatClubAPI/{id}
        //It looks like it works in the same way
        [HttpGet("GetCatsDTO_LocalStorage_ById/{id:int}", Name ="GetCatById")]
        public CatDTO GetCatsDTOFromStorage_ById(int id)
        {
            return CatStorage.catList.FirstOrDefault(x => x.Id == id);
        }

        //Action Results
        //Using action result I can use methods like Ok(), BadRequest() etc..

        [HttpGet("GetCats_ActionResult")]
        public ActionResult<IEnumerable<Cat>> GetCats_ActionResult()
        {
            return Ok(new List<Cat>()
            {
                new Cat{Id=0, Name="Pumczak", Age=1, Description="Pumczak to najgrubszy kot ze stada, lubi się bardzo przytulać i jest mocno zazdrosna."},
                new Cat{Id=1,Name="Kita", Age=1, Description="Kita jest najstarszą kocicą, to matka polka stada."},
                new Cat{Id=2, Name="Plamka", Age=1, Description="Plamka jest najmłodsza ze stada, była najbardziej schorowana. To troll."}
            });

        }
       
        [HttpGet("GetCatsDTO_ActionResult")]
        public ActionResult<IEnumerable<CatDTO>> GetCatsDTO_ActionResult()
        {
            return Ok(new List<CatDTO>
            {
                new CatDTO{Id=0, Name="Pumczak"},
                new CatDTO{Id=1,Name="Kita"},
                new CatDTO{Id=2, Name="Plamka"}
            });
        }

        [HttpGet("GetCatsDTO_LocalStorage_ActionResult")]
        public ActionResult<IEnumerable<CatDTO>> GetCatsDTOFromStorage_ActionResult()
        {
            return Ok(CatStorage.catList);
        }

        [HttpGet("GetCatsDTO_LocalStorage_GetCatById_ActionResultById/{id:int}")]
        [ProducesResponseType(200, Type = typeof(CatDTO))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status201Created)]
        // Jako, że przy ActionResult podaliśmy <CatDTO>, oczekiwany rezultat będzie typu CatDTO
        // Możemy natomiast zastosować silną typizację przy zwracaniu konkretnego kodu z pomocą Type = Type of, w adnotacji
        public ActionResult<CatDTO> GetCatsDTOFromStorage_ActionResult(int id)
        {
            // 3 cats hardcoded starting from 0 index, indexes below 0 not allowed
            if (id < 0)
            {
                return BadRequest();
            }

            var cat = CatStorage.catList.FirstOrDefault(x => x.Id == id);

            if (cat == null)
            {
                return NotFound();
            }

            return Ok(cat);
        }
        #endregion

        #region HttpPost
        [HttpPost("CreateCatDTO")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CatDTO> CreateCatDTO([FromBody] CatDTO catDTO)
        {
            if (catDTO == null)
            {
                return BadRequest(catDTO);
            }

            if (catDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            catDTO.Id = CatStorage.catList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            CatStorage.catList.Add(catDTO);

            return Ok(catDTO);
        }

        [HttpPost("CreateCatDTOCreatedAtRoute")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CatDTO> CreateCatDTOCreatedAtRoute([FromBody] CatDTO catDTO)
        {
            if (catDTO == null)
            {
                return BadRequest(catDTO);
            }

            if (catDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            catDTO.Id = CatStorage.catList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            CatStorage.catList.Add(catDTO);

            return CreatedAtRoute("GetCatById", new { id = catDTO.Id }, catDTO);
        }

        #endregion
    }
}

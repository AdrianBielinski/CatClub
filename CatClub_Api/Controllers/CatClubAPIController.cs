﻿using CatClub_Api.Data;
using CatClub_Api.Models;
using CatClub_Api.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CatClub_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatClubAPIController : ControllerBase
    {
        [Route("GetCats")]
        [HttpGet]
        public IEnumerable<Cat> GetCats()
        {
            return new List<Cat>()
            {
                new Cat{Id=0, Name="Pumczak", Age=1, Description="Pumczak to najgrubszy kot ze stada, lubi się bardzo przytulać i jest mocno zazdrosna."},
                new Cat{Id=1,Name="Kita", Age=1, Description="Kita jest najstarszą kocicą, to matka polka stada."},
                new Cat{Id=2, Name="Plamka", Age=1, Description="Plamka jest najmłodsza ze stada, była najbardziej schorowana. To troll."}
            };

        }
        [Route("GetCatsDTO")]
        [HttpGet]
        public IEnumerable<CatDTO> GetCatsDTO()
        {
            return new List<CatDTO>
            {
                new CatDTO{Id=0, Name="Pumczak"},
                new CatDTO{Id=1,Name="Kita"},
                new CatDTO{Id=2, Name="Plamka"}
            };
        }

        [Route("GetCatsDTO_LocalStorage")]
        [HttpGet]
        public IEnumerable<CatDTO> GetCatsDTOFromStorage()
        {
            return CatStorage.catList;
        }

        [Route("GetCatsDTO_LocalStorage_ById/{id:int}")]
        //Swagger throws exception with custom route annotation
        //HttpGet: /api/CatClubAPI/{id}
        //Route: /api/CatClubAPI/{id}
        //It looks like it works in the same way
        [HttpGet]
        public CatDTO GetCatsDTOFromStorage(int id)
        {
            return CatStorage.catList.FirstOrDefault(x => x.Id == id);
        }

        //Action Results
        //Using action result I can use methods like Ok(), BadRequest() etc..

        [Route("GetCats_ActionResult")]
        [HttpGet]
        public ActionResult<IEnumerable<Cat>> GetCats_ActionResult()
        {
            return Ok(new List<Cat>()
            {
                new Cat{Id=0, Name="Pumczak", Age=1, Description="Pumczak to najgrubszy kot ze stada, lubi się bardzo przytulać i jest mocno zazdrosna."},
                new Cat{Id=1,Name="Kita", Age=1, Description="Kita jest najstarszą kocicą, to matka polka stada."},
                new Cat{Id=2, Name="Plamka", Age=1, Description="Plamka jest najmłodsza ze stada, była najbardziej schorowana. To troll."}
            });

        }
        [Route("GetCatsDTO_ActionResult")]
        [HttpGet]
        public ActionResult<IEnumerable<CatDTO>> GetCatsDTO_ActionResult()
        {
            return Ok(new List<CatDTO>
            {
                new CatDTO{Id=0, Name="Pumczak"},
                new CatDTO{Id=1,Name="Kita"},
                new CatDTO{Id=2, Name="Plamka"}
            });
        }

        [Route("GetCatsDTO_LocalStorage_ActionResult")]
        [HttpGet]
        public ActionResult<IEnumerable<CatDTO>> GetCatsDTOFromStorage_ActionResult()
        {
            return Ok(CatStorage.catList);
        }

        [Route("GetCatsDTO_LocalStorage_GetCatById_ActionResultById/{id:int}")]
        [HttpGet]
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
    }
}

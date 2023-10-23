/**
 * @Author H.M.S.Y Nanayakkara
 * @Created 10/1/2023
 * @Description Implement Trains API Controllers
 **/
using AED_BE.Models;
using AED_BE.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using AED_BE.DTO;

namespace AED_BE.Controllers
{
    [Route("api/train")] //Route
    [ApiController]
    public class TrainsController : ControllerBase
    {
        private readonly TrainService _trainService;

        public TrainsController(TrainService trainService) //Constructor
        {
            _trainService = trainService;
        }

        public async Task<ActionResult<Trains>> Post(Trains newtrains) //Create Train
        {
            await _trainService.CreateAsync(newtrains);
            return CreatedAtAction(nameof(Get), new { id = newtrains.Id }, newtrains);
        }

        [HttpGet]
        public async Task<List<Trains>> Get() //Get all trains
        {
            return await _trainService.GetAllTrains();
        }


        [HttpGet("stationlist")]
        public async Task<ActionResult<Trains>> GetTrainList() // Get one train by number
        {
            DataStation teachers;
            var serializer = new JsonSerializer();
            using (StreamReader reader = new("./stations.json"))
            using (var textReader = new JsonTextReader(reader))
            {
                teachers = serializer.Deserialize<DataStation>(textReader);
            }

            if (teachers == null)
            {
                return NotFound();
            }

            return Ok(teachers);
        }

        [HttpGet("{trainNumber}")]
        public async Task<ActionResult<Trains>> GetOneTrainByNumber(int number) // Get one train by number
        {
            Trains trains = await _trainService.GetTrainsByNumber(number);
            if (trains == null)
            {
                return NotFound();
            }
            return Ok(trains);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Trains>> Put(string id, Trains updatedTrain) //Update train
        {

            await _trainService.UpdateTrain(id, updatedTrain);
            return Ok(updatedTrain);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id) //Delete train
        {
            await _trainService.DeleteTrain(id);
            return Ok("Deleted successfully");
        }


    }
}

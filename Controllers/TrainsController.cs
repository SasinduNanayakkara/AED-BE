using AED_BE.Models;
using AED_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace AED_BE.Controllers
{
        [Route("api/train")]
        [ApiController]
    public class TrainsController : ControllerBase
    {
        private readonly TrainService _trainService;

        public TrainsController(TrainService trainService)
        {
            _trainService = trainService;
        }

        public async Task<ActionResult<Trains>> Post(Trains newtrains)
        {
            await _trainService.CreateAsync(newtrains);
            return CreatedAtAction(nameof(Get), new { id = newtrains.Id }, newtrains);
        }

        [HttpGet]
        public async Task<List<Trains>> Get()
        {
            return await _trainService.GetAllTrains();
        }

        [HttpGet("{trainNumber}")]
        public async Task<ActionResult<Trains>> GetOneTrainByNumber(int number)
        {
            Trains trains = await _trainService.GetTrainsByNumber(number);
            if (trains == null)
            {
                return NotFound();
            }
            return Ok(trains);
        }

        //public async Task<List<Trains>> FilterTrains(string date)
        //{
        //    Trains trains = await _trainService.FilterTrains(date);
        //    if (trains == null)
        //    {
        //        return trains;
        //    return trains;
        //}


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, Trains updatedTrain)
        {
            Trains trains = await _trainService.GetOneTrain(id);
            if (trains == null)
            {
                return NotFound();
            }
            
            updatedTrain.Id = id;
            await _trainService.UpdateTrain(id, updatedTrain);
            return Ok(updatedTrain);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            Trains trains = await _trainService.GetOneTrain(id);
            if (trains == null)
            {
                return NotFound();
            }
            await _trainService.DeleteTrain(id);
            return Ok("Deleted successfully");
        }


    }
}

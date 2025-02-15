using DiplomskiRAD.Repository;
using DiplomskiRAD.Services;
using Microsoft.AspNetCore.Mvc;
using static DiplomskiRAD.DTOs.ServiceDTO;
using static DiplomskiRAD.DTOs.SparePartDTO;
using static DiplomskiRAD.DTOs.TaskServiceDTO;

namespace DiplomskiRAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskServiceService _taskService;

        public TaskController(TaskServiceService service)
        {
            _taskService = service;
            
        }

        [HttpPost("create/{serviceId}/{userId}")]
        public async Task<ActionResult> CreateTask(Guid serviceId, Guid userId, [FromBody] CreateTaskDto dto)
        {
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdTask = await _taskService.CreateTask(serviceId, userId, dto);

                if (createdTask == null) return NotFound("Id not found");

                return Ok(createdTask);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{taskId}")]
        public async Task<ActionResult<TaskServiceInfo>> GetTaskById(Guid taskId)
        {
            var existingTask = await _taskService.GetTaskById(taskId);
            if (existingTask == null)
            {
                return NotFound();
            }

            return Ok(existingTask);

        }


        [HttpPut("accept/{taskId}/{userId}")]
        public async Task<ActionResult> AcceptTask(Guid taskId, Guid userId)
        {
                await _taskService.AcceptTask(taskId, userId);
                return NoContent();
        }

        [HttpPut("decline/{taskId}")]
        public async Task<ActionResult> DeclineTask(Guid taskId, [FromBody] RejectTaskDto dto)
        {

            await _taskService.DeclineTask(taskId, dto);
            return NoContent();
        }

        [HttpPut("finish/{taskId}")]
        public async Task<ActionResult> FinishTask(Guid taskId, [FromBody] UsedSparePartDto dto)
        {
                await _taskService.FinishTask(taskId, dto);
                return NoContent();
        }

        [HttpGet("all-service-tasks/{serviceId}")]
        public async Task<ActionResult> GetAllTasksForService(Guid serviceId)
        {
            try
            {
                var result = await _taskService.GetAllTasksForService(serviceId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving tasks: {ex.Message}");
            }
        }

        [HttpGet("tasks-for-worker/{workerId}")]
        public async Task<ActionResult> GetTasksForUser(Guid workerId)
        {
            try
            {
                var result = await _taskService.GetTasksForUser(workerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving user tasks: {ex.Message}");
            }
        }

        [HttpGet("InProgress-declined-tasks")]
        public async Task<ActionResult> GetAllInProgressDeclinedTasks()
        {
            try
            {
                var result = await _taskService.GetAllInProgressDeclinedTasks();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving declined tasks: {ex.Message}");
            }
        }


    }
}

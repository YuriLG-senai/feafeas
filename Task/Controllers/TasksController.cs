using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.Models;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private static List<ModelTask> modelTasks =
            new List<ModelTask>();

        [HttpGet]
        public ActionResult<List<ModelTask>> SearchTasks()
        {
            return Ok(modelTasks);
        }

        [HttpPost]
        public ActionResult<List<ModelTask>> AddTasks(ModelTask newTask)
        {
            if (newTask is null)
            {
                return BadRequest("Dados invalidos.");

            }
           
            if (newTask.Description.Length < 10 )
            {
                return BadRequest("Descrição deve ter mais de 10 caracteres.");
            }
            newTask.id = modelTasks.Count > 0 
                                            ? modelTasks[modelTasks.Count - 1].id + 1 
                                            : 1;
            modelTasks.Add(newTask);
            return Ok(modelTasks);
        }

        [HttpDelete("{id}")]
        public ActionResult <List<ModelTask>> DeleteTask(int id)
        {
            var task = modelTasks.Find(x => x.id == id);

            if (id == null)
            {
                return BadRequest($"id:{id} invalido.");
            }
            modelTasks.Remove(task);

            return Ok();


        }
    }
}

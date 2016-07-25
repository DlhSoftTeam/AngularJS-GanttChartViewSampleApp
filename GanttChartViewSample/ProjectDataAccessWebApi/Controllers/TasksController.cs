using ProjectDataAccessWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectDataAccessWebApi.Controllers
{
    public class TasksController : ApiController
    {
        ProjectDataEntities context = new ProjectDataEntities();

        public IEnumerable<TaskDto> GetAllTasks()
        {
            return from t in context.Tasks
                   orderby t.OrderIndex
                   select new TaskDto
                   {
                       Id = t.Id,
                       Name = t.Name,
                       Start = t.Start,
                       Finish = t.Finish,
                       CompletedFinish = t.CompletedFinish,
                       Assignments = t.Assignments,
                       Indentation = t.Indentation,
                       Predecessors = from p in t.TargetPredecessors
                                      select new PredecessorDto
                                      {
                                          SourceTaskId = p.SourceTaskId,
                                          DependencyType = p.DependencyType
                                      }
                   };
        }

        [HttpPut]
        public IHttpActionResult CreateTask(TaskDto taskDto)
        {
            taskDto.Id = 0;
            var task = UpdateTask(taskDto);
            task.OrderIndex = (!context.Tasks.Any() ? 0 : context.Tasks.Max(t => t.OrderIndex)) + 1;
            context.SaveChanges();
            taskDto.Id = task.Id;
            return Ok(taskDto);
        }

        [HttpPost]
        public IHttpActionResult UpdateTask(int id, TaskDto taskDto)
        {
            taskDto.Id = id;
            UpdateTask(taskDto);
            context.SaveChanges();
            return Ok(taskDto);
        }

        private Task UpdateTask(TaskDto taskDto)
        {
            var id = taskDto.Id;
            var task = id > 0 ? context.Tasks.Single(t => t.Id == id) : null;
            if (task == null)
            {
                task = new Task();
                context.Tasks.Add(task);
            }
            task.Name = taskDto.Name;
            task.Start = taskDto.Start;
            task.Finish = taskDto.Finish;
            task.CompletedFinish = taskDto.CompletedFinish;
            task.Assignments = taskDto.Assignments;
            task.Indentation = taskDto.Indentation;
            foreach (var predecessor in task.TargetPredecessors.ToArray())
                context.Predecessors.Remove(predecessor);
            if (taskDto.Predecessors != null)
            {
                foreach (var predecessorDto in taskDto.Predecessors)
                    task.TargetPredecessors.Add(new Predecessor { SourceTaskId = predecessorDto.SourceTaskId, DependencyType = predecessorDto.DependencyType });
            }
            return task;
        }

        [HttpDelete]
        public IHttpActionResult DeleteTask(int id)
        {
            var task = context.Tasks.Single(t => t.Id == id);
            foreach (var predecessor in task.TargetPredecessors.ToArray())
                context.Predecessors.Remove(predecessor);
            foreach (var predecessor in task.SourcePredecessors.ToArray())
                context.Predecessors.Remove(predecessor);
            context.Tasks.Remove(task);
            context.SaveChanges();
            return Ok();
        }
    }
}

using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StoryServer
{
    [Route("/stories")]
    public class StoryController : ControllerBase
    {
        private readonly IStoriesRepository repo;
        public StoryController(IStoriesRepository storiesRepository)
        {
            repo = storiesRepository;
        }

        [HttpGet("{id}", Name = "GetStory")]
        public IActionResult Get(long id)
        {
            if (repo.Contains(id))
            {
                return Ok(repo.Find(id));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(repo.List());
        }

        [HttpPost]
        public IActionResult Create([FromBody] Story toCreate)
        {
            if (IsProjectActive(toCreate.projectId).GetAwaiter().GetResult())
            {
                var created = repo.Create(toCreate);
                return CreatedAtRoute("GetStory", new { id = created.id }, created);
            }
            else
            {
                return new StatusCodeResult(422);
                /* //Another way of doing it
                var result = new JsonResult(null);
                result.StatusCode = 422;
                return result;
                */
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Story toUpdate)
        {
            if (repo.Contains(id))
            {
                var updated = repo.Update(id, toUpdate);
                return Ok(updated);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (repo.Contains(id))
            {
                repo.Delete(id);
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        private async Task<bool> IsProjectActive(long id)
        {

            string url = "https://project-server-fearless-wallaby.cfapps.io/projects/" + id;
            HttpClient http = new HttpClient();
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsAsync(typeof(ProjectInfo)) as ProjectInfo;
            return result.active;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StoryServer{
    public class InMemoryStoryRepository : IStoriesRepository
    {
        private IDictionary<long,Story> repo = new Dictionary<long,Story>();

        public bool Contains(long id)=> repo.ContainsKey(id);

        public Story Create(Story toCreate)
        {
            var id = repo.Count+1;
            toCreate.id = id;
            repo.Add(id, toCreate);
            return repo[id];
        }

        public void Delete(long id) => repo.Remove(id);

        public Story Find(long id)=> repo[id];

        public IEnumerable List() => repo.Values.ToList();

        public Story Update(long id, Story toUpdate)
        {
            toUpdate.id = id;
            repo[id] = toUpdate;
            return toUpdate;
        }
    }
}
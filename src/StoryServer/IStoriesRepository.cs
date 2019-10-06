using System.Collections;


namespace StoryServer
{
    public interface IStoriesRepository
    {
        Story Create(Story toCreate);
        Story Update(long id, Story toUpdate);
        Story Find(long id);
        bool Contains(long id);
        IEnumerable List();
        void Delete(long id);        
    }
}
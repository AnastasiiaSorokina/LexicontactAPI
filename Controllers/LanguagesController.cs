using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LexicontactAPI.Controllers
{
    public class LanguagesController : ApiController
    {
        private readonly ApplicationDbContext _ctx = new ApplicationDbContext();

        // GET api/languages
        public IEnumerable<Language> Get()
        {
            return _ctx.Language.ToList();
        }

        // GET api/languages/076BE845-86F0-43C8-A50F-07A038FD2108
        public Language Get(string id)
        {
            Guid languageGuid = new Guid(id);
            // TODO Error handling?
            Language result = _ctx.Language.Find(languageGuid);
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace LexicontactAPI.Controllers
{
    public class LanguagesController : ApiController
    {
        private readonly ApplicationDbContext _ctx = new ApplicationDbContext();

        // GET api/languages
        public async Task<IEnumerable<Language>> Get()
        {
            return await _ctx.Language.ToListAsync();
        }

        // GET api/languages/076be845-86f0-43c8-a50f-07a038fd2108
        public async Task<IHttpActionResult> Get(string id)
        {
            Guid languageGuid = new Guid(id);
            var result = await _ctx.Language.FindAsync(languageGuid);

            if (result == null)
            {
                return Ok(Enumerable.Empty<Language>());
            }

            return Ok(result);
        }
    }
}

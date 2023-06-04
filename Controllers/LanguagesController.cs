using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net;
using System.Net.Http;

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
        /// <summary>
        /// Looks for a language in the database and returns it if found.
        /// The GUID in the URL parameter must be a valid GUID, otherwise an
        /// HTTP 400 Bad Request error is sent to the client.
        /// If the language is not found in the database, an empty JSON object
        /// is sent as a response.
        /// </summary>
        /// <param name="guid">The GUID of the language.</param>
        /// <returns>A task that returns a HTTP response.</returns>
        public async Task<IHttpActionResult> Get(string guid)
        {
            Guid languageGuid;

            if (!Guid.TryParse(guid, out languageGuid))
            {
                System.Diagnostics.Debug.WriteLine($"Failed to parse GUID: '{guid}'");
                return BadRequest("Invalid GUID format.");
            }

            var result = await _ctx.Language.FindAsync(languageGuid);

            if (result == null)
            {
                return Ok(ImmutableDictionary<int, int>.Empty);
            }

            return Ok(result);
        }
    }
}

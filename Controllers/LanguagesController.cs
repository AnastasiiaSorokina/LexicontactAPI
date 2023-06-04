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
        private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();

        /// <summary>
        /// Gets all languages from the database.
        ///
        /// Example request: GET api/languages
        /// </summary>
        /// <returns>A task that returns a list of languages.</returns>
        public async Task<IEnumerable<Language>> Get()
        {
            return await _dbContext.Language.ToListAsync();
        }

        /// <summary>
        /// Looks for a language in the database and returns it if found.
        /// The GUID in the URL parameter must be a valid GUID, otherwise an
        /// HTTP 400 Bad Request error is sent to the client.
        /// If the language is not found in the database, an empty JSON object
        /// is sent as a response.
        ///
        /// Example request:
        /// GET api/languages/076be845-86f0-43c8-a50f-07a038fd2108
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

            var result = await _dbContext.Language.FindAsync(languageGuid);
            if (result == null)
            {
                return Ok(ImmutableDictionary<int, int>.Empty);
            }

            return Ok(result);
        }

        /// <summary>
        /// Creates a new language in the database.
        ///
        /// Example request: POST api/languages
        /// Example body: Content-Type must be application/json
        /// {
        ///   "code": "ua",
        ///   "name": "Ukrainian"
        /// }
        /// </summary>
        /// <param name="language">A Language object with language code and name.</param>
        /// <returns>A task that returns a HTTP action result.</returns>
        public async Task<IHttpActionResult> Post([FromBody] Language language)
        {
            if (language == null || language.Name == null || language.Code == null)
            {
                return BadRequest("Request body must contain language code and name.");
            }

            Guid languageGuid = Guid.NewGuid();
            language.Guid = languageGuid;
            System.Diagnostics.Debug.WriteLine(language.Guid);
            System.Diagnostics.Debug.WriteLine(language.Code);
            System.Diagnostics.Debug.WriteLine(language.Name);

            try
            {
                var result = _dbContext.Language.Add(language);
                await _dbContext.SaveChangesAsync();
                return CreatedAtRoute("LanguagesApi", new { guid = result.Guid }, result);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return InternalServerError(ex);
            }
        }
    }
}

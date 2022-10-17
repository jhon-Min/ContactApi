using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Mvc;
//using ContactApi.Models;

namespace ContactApi.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly DataContext _context;

        public ContactController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<Contact>>> Get()
        {
            var contacts = await _context.Contacts.ToListAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> Get(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if(contact == null){
                return BadRequest("Contact Not Found!");
            }

            return Ok(contact);
        }
        
        [HttpPost]
        public async Task<ActionResult<List<Contact>>> AddContact(Contact contact)
        {
           _context.Contacts.Add(contact);
           await _context.SaveChangesAsync();

           return Ok(await _context.Contacts.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Contact>> > UpdateHero(Contact request)
        {
           var contact = await _context.Contacts.FindAsync(request.Id);
           if(contact == null){
            return BadRequest("Contact Not Found!");
           }else{
            contact.Name = request.Name;
            contact.Mobile = request.Mobile;
            await _context.SaveChangesAsync();

            return Ok(await _context.Contacts.ToListAsync());
           }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Contact>>> Delete(int id)
        {
           var contact = await _context.Contacts.FindAsync(id);
           if(contact == null){
            return BadRequest("Contact Not Found!");
           }

           _context.Contacts.Remove(contact);
           await _context.SaveChangesAsync();

           return Ok(await _context.Contacts.ToListAsync());
        }
        
    }
}
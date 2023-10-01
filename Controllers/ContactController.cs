using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ContactController : ControllerBase



    {
        private readonly ContactContext _context;

        public ContactController(ContactContext context)
        {
            _context = context;
        }
        // GET all contacts
        [HttpGet]
        public IActionResult Get()
        {
            var contacts = _context.Contacts.Include(c => c.ContactCategory).Include(c => c.ContactSubCategory).ToList();

            return Ok(contacts);
        }
        // GET contact by id
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var contact = _context.Contacts.Include(c => c.ContactCategory).Include(c => c.ContactSubCategory).FirstOrDefault(c => c.ContactId == id);
            return Ok(contact);
        }
        // POST contact
        [HttpPost]
        public IActionResult Post([FromBody] Contact contactDto)
        {
            var contact = new Contact
            {
                FirstName = contactDto.FirstName,
                LastName = contactDto.LastName,
                Email = contactDto.Email,
                Password = contactDto.Password,
                CategoryId = contactDto.CategoryId,
                SubCategoryId = contactDto.SubCategoryId,
                PhoneNumber = contactDto.PhoneNumber,
                DateOfBirth = contactDto.DateOfBirth,
                ContactCategory = contactDto.ContactCategory,
                ContactSubCategory = contactDto.ContactSubCategory
            };
            // if there is already a ContactCategory with the same name, use that one
            var existingContactCategory = _context.ContactCategory.FirstOrDefault(c => c.CategoryId == contactDto.ContactCategory.CategoryId);
            if (existingContactCategory != null)
            {
                contact.ContactCategory = existingContactCategory;
            }
            // if there is already a ContactSubCategory with the same name, use that one
            var existingContactSubCategory = _context.ContactSubCategory.FirstOrDefault(c => c.SubCategoryId == contactDto.ContactSubCategory.SubCategoryId);
            if (existingContactSubCategory != null)
            {
                contact.ContactSubCategory = existingContactSubCategory;
            }
            //check if email is unique, if not, return error
            var existingContact = _context.Contacts.FirstOrDefault(c => c.Email == contactDto.Email);
            if (existingContact != null)
            {
                return BadRequest(new { message = "Email is already taken." });
            }
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return Ok(contact);
        }
        // PUT contact
        [HttpPut("{id}")]
        public IActionResult Put(Contact contact, int id)
        {
            contact = _context.Contacts.FirstOrDefault(c => c.ContactId == id);
            _context.Contacts.Update(contact);
            _context.SaveChanges();
            return Ok(contact);
        }
        // DELETE contact
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(c => c.ContactId == id);
            _context.Contacts.Remove(contact);
            _context.SaveChanges();
            return Ok(contact);
        }
        [HttpGet]
        [Route("GetBiggestId")]
        public IActionResult GetBiggestId()
        {
            //Get the biggest SubCategory ID from the database
            var biggestId = _context.ContactSubCategory.Max(c => c.SubCategoryId);
            return Ok(biggestId);
        }
    }
}
